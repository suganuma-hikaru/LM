<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI961F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormSxga

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.pnlCondition = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtEstMakeUserNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSearchRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtFwdUserNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtEstNoEda = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtEstNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSearchRem = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleEstNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleEstMakeUserNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleFwdUserNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkKokunai = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkYunyu = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkYushutsu = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.pnlSakuseiNaiyo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.optKaeri = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optYuki = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlCondition.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSakuseiNaiyo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.pnlSakuseiNaiyo)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.pnlCondition)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'pnlCondition
        '
        Me.pnlCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCondition.Controls.Add(Me.txtEstMakeUserNm)
        Me.pnlCondition.Controls.Add(Me.txtSearchRem)
        Me.pnlCondition.Controls.Add(Me.txtGoodsNm)
        Me.pnlCondition.Controls.Add(Me.txtFwdUserNm)
        Me.pnlCondition.Controls.Add(Me.txtEstNoEda)
        Me.pnlCondition.Controls.Add(Me.txtEstNo)
        Me.pnlCondition.Controls.Add(Me.lblTitleGoodsNm)
        Me.pnlCondition.Controls.Add(Me.lblTitleSearchRem)
        Me.pnlCondition.Controls.Add(Me.lblTitleEstNo)
        Me.pnlCondition.Controls.Add(Me.lblTitleEstMakeUserNm)
        Me.pnlCondition.Controls.Add(Me.lblTitleFwdUserNm)
        Me.pnlCondition.Controls.Add(Me.chkKokunai)
        Me.pnlCondition.Controls.Add(Me.chkYunyu)
        Me.pnlCondition.Controls.Add(Me.chkYushutsu)
        Me.pnlCondition.EnableStatus = False
        Me.pnlCondition.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.HeightDef = 136
        Me.pnlCondition.Location = New System.Drawing.Point(16, 16)
        Me.pnlCondition.Name = "pnlCondition"
        Me.pnlCondition.Size = New System.Drawing.Size(1007, 136)
        Me.pnlCondition.TabIndex = 112
        Me.pnlCondition.TabStop = False
        Me.pnlCondition.Text = "検索条件"
        Me.pnlCondition.TextValue = "検索条件"
        Me.pnlCondition.WidthDef = 1007
        '
        'txtEstMakeUserNm
        '
        Me.txtEstMakeUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstMakeUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstMakeUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstMakeUserNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEstMakeUserNm.CountWrappedLine = False
        Me.txtEstMakeUserNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEstMakeUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstMakeUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstMakeUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstMakeUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstMakeUserNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEstMakeUserNm.HeightDef = 18
        Me.txtEstMakeUserNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEstMakeUserNm.HissuLabelVisible = False
        Me.txtEstMakeUserNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtEstMakeUserNm.IsByteCheck = 30
        Me.txtEstMakeUserNm.IsCalendarCheck = False
        Me.txtEstMakeUserNm.IsDakutenCheck = False
        Me.txtEstMakeUserNm.IsEisuCheck = False
        Me.txtEstMakeUserNm.IsForbiddenWordsCheck = False
        Me.txtEstMakeUserNm.IsFullByteCheck = 0
        Me.txtEstMakeUserNm.IsHankakuCheck = False
        Me.txtEstMakeUserNm.IsHissuCheck = False
        Me.txtEstMakeUserNm.IsKanaCheck = False
        Me.txtEstMakeUserNm.IsMiddleSpace = False
        Me.txtEstMakeUserNm.IsNumericCheck = False
        Me.txtEstMakeUserNm.IsSujiCheck = False
        Me.txtEstMakeUserNm.IsZenkakuCheck = False
        Me.txtEstMakeUserNm.ItemName = ""
        Me.txtEstMakeUserNm.LineSpace = 0
        Me.txtEstMakeUserNm.Location = New System.Drawing.Point(642, 50)
        Me.txtEstMakeUserNm.MaxLength = 30
        Me.txtEstMakeUserNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEstMakeUserNm.MaxLineCount = 0
        Me.txtEstMakeUserNm.Multiline = False
        Me.txtEstMakeUserNm.Name = "txtEstMakeUserNm"
        Me.txtEstMakeUserNm.ReadOnly = False
        Me.txtEstMakeUserNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEstMakeUserNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEstMakeUserNm.Size = New System.Drawing.Size(359, 18)
        Me.txtEstMakeUserNm.TabIndex = 649
        Me.txtEstMakeUserNm.TabStopSetting = True
        Me.txtEstMakeUserNm.TextValue = ""
        Me.txtEstMakeUserNm.UseSystemPasswordChar = False
        Me.txtEstMakeUserNm.WidthDef = 359
        Me.txtEstMakeUserNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSearchRem
        '
        Me.txtSearchRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearchRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearchRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSearchRem.CountWrappedLine = False
        Me.txtSearchRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSearchRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSearchRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSearchRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSearchRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSearchRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSearchRem.HeightDef = 18
        Me.txtSearchRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSearchRem.HissuLabelVisible = False
        Me.txtSearchRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSearchRem.IsByteCheck = 40
        Me.txtSearchRem.IsCalendarCheck = False
        Me.txtSearchRem.IsDakutenCheck = False
        Me.txtSearchRem.IsEisuCheck = False
        Me.txtSearchRem.IsForbiddenWordsCheck = False
        Me.txtSearchRem.IsFullByteCheck = 0
        Me.txtSearchRem.IsHankakuCheck = False
        Me.txtSearchRem.IsHissuCheck = False
        Me.txtSearchRem.IsKanaCheck = False
        Me.txtSearchRem.IsMiddleSpace = False
        Me.txtSearchRem.IsNumericCheck = False
        Me.txtSearchRem.IsSujiCheck = False
        Me.txtSearchRem.IsZenkakuCheck = False
        Me.txtSearchRem.ItemName = ""
        Me.txtSearchRem.LineSpace = 0
        Me.txtSearchRem.Location = New System.Drawing.Point(151, 98)
        Me.txtSearchRem.MaxLength = 40
        Me.txtSearchRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSearchRem.MaxLineCount = 0
        Me.txtSearchRem.Multiline = False
        Me.txtSearchRem.Name = "txtSearchRem"
        Me.txtSearchRem.ReadOnly = False
        Me.txtSearchRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSearchRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSearchRem.Size = New System.Drawing.Size(595, 18)
        Me.txtSearchRem.TabIndex = 648
        Me.txtSearchRem.TabStopSetting = True
        Me.txtSearchRem.TextValue = ""
        Me.txtSearchRem.UseSystemPasswordChar = False
        Me.txtSearchRem.WidthDef = 595
        Me.txtSearchRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtGoodsNm.HissuLabelVisible = False
        Me.txtGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsNm.IsByteCheck = 100
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
        Me.txtGoodsNm.Location = New System.Drawing.Point(151, 74)
        Me.txtGoodsNm.MaxLength = 100
        Me.txtGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNm.MaxLineCount = 0
        Me.txtGoodsNm.Multiline = False
        Me.txtGoodsNm.Name = "txtGoodsNm"
        Me.txtGoodsNm.ReadOnly = False
        Me.txtGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNm.Size = New System.Drawing.Size(595, 18)
        Me.txtGoodsNm.TabIndex = 647
        Me.txtGoodsNm.TabStopSetting = True
        Me.txtGoodsNm.TextValue = ""
        Me.txtGoodsNm.UseSystemPasswordChar = False
        Me.txtGoodsNm.WidthDef = 595
        Me.txtGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtFwdUserNm
        '
        Me.txtFwdUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFwdUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFwdUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFwdUserNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFwdUserNm.CountWrappedLine = False
        Me.txtFwdUserNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFwdUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFwdUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFwdUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFwdUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFwdUserNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFwdUserNm.HeightDef = 18
        Me.txtFwdUserNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFwdUserNm.HissuLabelVisible = False
        Me.txtFwdUserNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtFwdUserNm.IsByteCheck = 30
        Me.txtFwdUserNm.IsCalendarCheck = False
        Me.txtFwdUserNm.IsDakutenCheck = False
        Me.txtFwdUserNm.IsEisuCheck = False
        Me.txtFwdUserNm.IsForbiddenWordsCheck = False
        Me.txtFwdUserNm.IsFullByteCheck = 0
        Me.txtFwdUserNm.IsHankakuCheck = False
        Me.txtFwdUserNm.IsHissuCheck = False
        Me.txtFwdUserNm.IsKanaCheck = False
        Me.txtFwdUserNm.IsMiddleSpace = False
        Me.txtFwdUserNm.IsNumericCheck = False
        Me.txtFwdUserNm.IsSujiCheck = False
        Me.txtFwdUserNm.IsZenkakuCheck = False
        Me.txtFwdUserNm.ItemName = ""
        Me.txtFwdUserNm.LineSpace = 0
        Me.txtFwdUserNm.Location = New System.Drawing.Point(151, 50)
        Me.txtFwdUserNm.MaxLength = 30
        Me.txtFwdUserNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFwdUserNm.MaxLineCount = 0
        Me.txtFwdUserNm.Multiline = False
        Me.txtFwdUserNm.Name = "txtFwdUserNm"
        Me.txtFwdUserNm.ReadOnly = False
        Me.txtFwdUserNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFwdUserNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFwdUserNm.Size = New System.Drawing.Size(397, 18)
        Me.txtFwdUserNm.TabIndex = 646
        Me.txtFwdUserNm.TabStopSetting = True
        Me.txtFwdUserNm.TextValue = ""
        Me.txtFwdUserNm.UseSystemPasswordChar = False
        Me.txtFwdUserNm.WidthDef = 397
        Me.txtFwdUserNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtEstNoEda
        '
        Me.txtEstNoEda.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstNoEda.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstNoEda.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstNoEda.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEstNoEda.CountWrappedLine = False
        Me.txtEstNoEda.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEstNoEda.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstNoEda.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstNoEda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstNoEda.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstNoEda.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEstNoEda.HeightDef = 18
        Me.txtEstNoEda.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEstNoEda.HissuLabelVisible = False
        Me.txtEstNoEda.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtEstNoEda.IsByteCheck = 3
        Me.txtEstNoEda.IsCalendarCheck = False
        Me.txtEstNoEda.IsDakutenCheck = False
        Me.txtEstNoEda.IsEisuCheck = False
        Me.txtEstNoEda.IsForbiddenWordsCheck = False
        Me.txtEstNoEda.IsFullByteCheck = 0
        Me.txtEstNoEda.IsHankakuCheck = False
        Me.txtEstNoEda.IsHissuCheck = False
        Me.txtEstNoEda.IsKanaCheck = False
        Me.txtEstNoEda.IsMiddleSpace = False
        Me.txtEstNoEda.IsNumericCheck = False
        Me.txtEstNoEda.IsSujiCheck = False
        Me.txtEstNoEda.IsZenkakuCheck = False
        Me.txtEstNoEda.ItemName = ""
        Me.txtEstNoEda.LineSpace = 0
        Me.txtEstNoEda.Location = New System.Drawing.Point(902, 26)
        Me.txtEstNoEda.MaxLength = 3
        Me.txtEstNoEda.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEstNoEda.MaxLineCount = 0
        Me.txtEstNoEda.Multiline = False
        Me.txtEstNoEda.Name = "txtEstNoEda"
        Me.txtEstNoEda.ReadOnly = False
        Me.txtEstNoEda.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEstNoEda.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEstNoEda.Size = New System.Drawing.Size(99, 18)
        Me.txtEstNoEda.TabIndex = 644
        Me.txtEstNoEda.TabStopSetting = True
        Me.txtEstNoEda.TextValue = ""
        Me.txtEstNoEda.UseSystemPasswordChar = False
        Me.txtEstNoEda.WidthDef = 99
        Me.txtEstNoEda.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtEstNo
        '
        Me.txtEstNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEstNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEstNo.CountWrappedLine = False
        Me.txtEstNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEstNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEstNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEstNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEstNo.HeightDef = 18
        Me.txtEstNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEstNo.HissuLabelVisible = False
        Me.txtEstNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtEstNo.IsByteCheck = 10
        Me.txtEstNo.IsCalendarCheck = False
        Me.txtEstNo.IsDakutenCheck = False
        Me.txtEstNo.IsEisuCheck = False
        Me.txtEstNo.IsForbiddenWordsCheck = False
        Me.txtEstNo.IsFullByteCheck = 0
        Me.txtEstNo.IsHankakuCheck = False
        Me.txtEstNo.IsHissuCheck = False
        Me.txtEstNo.IsKanaCheck = False
        Me.txtEstNo.IsMiddleSpace = False
        Me.txtEstNo.IsNumericCheck = False
        Me.txtEstNo.IsSujiCheck = False
        Me.txtEstNo.IsZenkakuCheck = False
        Me.txtEstNo.ItemName = ""
        Me.txtEstNo.LineSpace = 0
        Me.txtEstNo.Location = New System.Drawing.Point(642, 26)
        Me.txtEstNo.MaxLength = 10
        Me.txtEstNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEstNo.MaxLineCount = 0
        Me.txtEstNo.Multiline = False
        Me.txtEstNo.Name = "txtEstNo"
        Me.txtEstNo.ReadOnly = False
        Me.txtEstNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEstNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEstNo.Size = New System.Drawing.Size(276, 18)
        Me.txtEstNo.TabIndex = 645
        Me.txtEstNo.TabStopSetting = True
        Me.txtEstNo.TextValue = ""
        Me.txtEstNo.UseSystemPasswordChar = False
        Me.txtEstNo.WidthDef = 276
        Me.txtEstNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsNm
        '
        Me.lblTitleGoodsNm.AutoSizeDef = False
        Me.lblTitleGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsNm.EnableStatus = False
        Me.lblTitleGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm.HeightDef = 18
        Me.lblTitleGoodsNm.Location = New System.Drawing.Point(78, 74)
        Me.lblTitleGoodsNm.Name = "lblTitleGoodsNm"
        Me.lblTitleGoodsNm.Size = New System.Drawing.Size(67, 18)
        Me.lblTitleGoodsNm.TabIndex = 643
        Me.lblTitleGoodsNm.Text = "商品名"
        Me.lblTitleGoodsNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsNm.TextValue = "商品名"
        Me.lblTitleGoodsNm.WidthDef = 67
        '
        'lblTitleSearchRem
        '
        Me.lblTitleSearchRem.AutoSizeDef = False
        Me.lblTitleSearchRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSearchRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSearchRem.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSearchRem.EnableStatus = False
        Me.lblTitleSearchRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSearchRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSearchRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSearchRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSearchRem.HeightDef = 18
        Me.lblTitleSearchRem.Location = New System.Drawing.Point(49, 98)
        Me.lblTitleSearchRem.Name = "lblTitleSearchRem"
        Me.lblTitleSearchRem.Size = New System.Drawing.Size(96, 18)
        Me.lblTitleSearchRem.TabIndex = 642
        Me.lblTitleSearchRem.Text = "見積コメント"
        Me.lblTitleSearchRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSearchRem.TextValue = "見積コメント"
        Me.lblTitleSearchRem.WidthDef = 96
        '
        'lblTitleEstNo
        '
        Me.lblTitleEstNo.AutoSizeDef = False
        Me.lblTitleEstNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEstNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEstNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEstNo.EnableStatus = False
        Me.lblTitleEstNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEstNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEstNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEstNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEstNo.HeightDef = 18
        Me.lblTitleEstNo.Location = New System.Drawing.Point(569, 26)
        Me.lblTitleEstNo.Name = "lblTitleEstNo"
        Me.lblTitleEstNo.Size = New System.Drawing.Size(67, 18)
        Me.lblTitleEstNo.TabIndex = 641
        Me.lblTitleEstNo.Text = "見積番号"
        Me.lblTitleEstNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEstNo.TextValue = "見積番号"
        Me.lblTitleEstNo.WidthDef = 67
        '
        'lblTitleEstMakeUserNm
        '
        Me.lblTitleEstMakeUserNm.AutoSizeDef = False
        Me.lblTitleEstMakeUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEstMakeUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEstMakeUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEstMakeUserNm.EnableStatus = False
        Me.lblTitleEstMakeUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEstMakeUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEstMakeUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEstMakeUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEstMakeUserNm.HeightDef = 18
        Me.lblTitleEstMakeUserNm.Location = New System.Drawing.Point(554, 50)
        Me.lblTitleEstMakeUserNm.Name = "lblTitleEstMakeUserNm"
        Me.lblTitleEstMakeUserNm.Size = New System.Drawing.Size(82, 18)
        Me.lblTitleEstMakeUserNm.TabIndex = 640
        Me.lblTitleEstMakeUserNm.Text = "見積作成者"
        Me.lblTitleEstMakeUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEstMakeUserNm.TextValue = "見積作成者"
        Me.lblTitleEstMakeUserNm.WidthDef = 82
        '
        'lblTitleFwdUserNm
        '
        Me.lblTitleFwdUserNm.AutoSizeDef = False
        Me.lblTitleFwdUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFwdUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFwdUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFwdUserNm.EnableStatus = False
        Me.lblTitleFwdUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFwdUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFwdUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFwdUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFwdUserNm.HeightDef = 18
        Me.lblTitleFwdUserNm.Location = New System.Drawing.Point(6, 50)
        Me.lblTitleFwdUserNm.Name = "lblTitleFwdUserNm"
        Me.lblTitleFwdUserNm.Size = New System.Drawing.Size(139, 18)
        Me.lblTitleFwdUserNm.TabIndex = 348
        Me.lblTitleFwdUserNm.Text = "フォワーダー担当者"
        Me.lblTitleFwdUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFwdUserNm.TextValue = "フォワーダー担当者"
        Me.lblTitleFwdUserNm.WidthDef = 139
        '
        'chkKokunai
        '
        Me.chkKokunai.AutoSize = True
        Me.chkKokunai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKokunai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKokunai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkKokunai.EnableStatus = True
        Me.chkKokunai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKokunai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKokunai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKokunai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKokunai.HeightDef = 17
        Me.chkKokunai.Location = New System.Drawing.Point(301, 26)
        Me.chkKokunai.Name = "chkKokunai"
        Me.chkKokunai.Size = New System.Drawing.Size(54, 17)
        Me.chkKokunai.TabIndex = 639
        Me.chkKokunai.TabStopSetting = True
        Me.chkKokunai.Text = "国内"
        Me.chkKokunai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkKokunai.TextValue = "国内"
        Me.chkKokunai.UseVisualStyleBackColor = True
        Me.chkKokunai.WidthDef = 54
        '
        'chkYunyu
        '
        Me.chkYunyu.AutoSize = True
        Me.chkYunyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYunyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYunyu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkYunyu.EnableStatus = True
        Me.chkYunyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYunyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYunyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYunyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYunyu.HeightDef = 17
        Me.chkYunyu.Location = New System.Drawing.Point(226, 26)
        Me.chkYunyu.Name = "chkYunyu"
        Me.chkYunyu.Size = New System.Drawing.Size(54, 17)
        Me.chkYunyu.TabIndex = 639
        Me.chkYunyu.TabStopSetting = True
        Me.chkYunyu.Text = "輸入"
        Me.chkYunyu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkYunyu.TextValue = "輸入"
        Me.chkYunyu.UseVisualStyleBackColor = True
        Me.chkYunyu.WidthDef = 54
        '
        'chkYushutsu
        '
        Me.chkYushutsu.AutoSize = True
        Me.chkYushutsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYushutsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYushutsu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkYushutsu.EnableStatus = True
        Me.chkYushutsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYushutsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYushutsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYushutsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYushutsu.HeightDef = 17
        Me.chkYushutsu.Location = New System.Drawing.Point(151, 26)
        Me.chkYushutsu.Name = "chkYushutsu"
        Me.chkYushutsu.Size = New System.Drawing.Size(54, 17)
        Me.chkYushutsu.TabIndex = 637
        Me.chkYushutsu.TabStopSetting = True
        Me.chkYushutsu.Text = "輸出"
        Me.chkYushutsu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkYushutsu.TextValue = "輸出"
        Me.chkYushutsu.UseVisualStyleBackColor = True
        Me.chkYushutsu.WidthDef = 54
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
        Me.sprDetail.HeightDef = 694
        Me.sprDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 172)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1244, 694)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 345
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetail.WidthDef = 1244
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
        'pnlSakuseiNaiyo
        '
        Me.pnlSakuseiNaiyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSakuseiNaiyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSakuseiNaiyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSakuseiNaiyo.Controls.Add(Me.optKaeri)
        Me.pnlSakuseiNaiyo.Controls.Add(Me.optYuki)
        Me.pnlSakuseiNaiyo.EnableStatus = False
        Me.pnlSakuseiNaiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSakuseiNaiyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSakuseiNaiyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSakuseiNaiyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSakuseiNaiyo.HeightDef = 60
        Me.pnlSakuseiNaiyo.Location = New System.Drawing.Point(1039, 16)
        Me.pnlSakuseiNaiyo.Name = "pnlSakuseiNaiyo"
        Me.pnlSakuseiNaiyo.Size = New System.Drawing.Size(191, 60)
        Me.pnlSakuseiNaiyo.TabIndex = 346
        Me.pnlSakuseiNaiyo.TabStop = False
        Me.pnlSakuseiNaiyo.Text = "作成内容"
        Me.pnlSakuseiNaiyo.TextValue = "作成内容"
        Me.pnlSakuseiNaiyo.WidthDef = 191
        '
        'optKaeri
        '
        Me.optKaeri.AutoSize = True
        Me.optKaeri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKaeri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKaeri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKaeri.Enabled = False
        Me.optKaeri.EnableStatus = True
        Me.optKaeri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKaeri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKaeri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKaeri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKaeri.HeightDef = 17
        Me.optKaeri.Location = New System.Drawing.Point(98, 26)
        Me.optKaeri.Name = "optKaeri"
        Me.optKaeri.Size = New System.Drawing.Size(53, 17)
        Me.optKaeri.TabIndex = 640
        Me.optKaeri.TabStopSetting = False
        Me.optKaeri.Text = "帰り"
        Me.optKaeri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKaeri.TextValue = "帰り"
        Me.optKaeri.UseVisualStyleBackColor = True
        Me.optKaeri.WidthDef = 53
        '
        'optYuki
        '
        Me.optYuki.AutoSize = True
        Me.optYuki.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optYuki.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optYuki.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optYuki.Checked = True
        Me.optYuki.Enabled = False
        Me.optYuki.EnableStatus = True
        Me.optYuki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optYuki.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optYuki.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optYuki.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optYuki.HeightDef = 17
        Me.optYuki.Location = New System.Drawing.Point(23, 26)
        Me.optYuki.Name = "optYuki"
        Me.optYuki.Size = New System.Drawing.Size(53, 17)
        Me.optYuki.TabIndex = 639
        Me.optYuki.TabStop = True
        Me.optYuki.TabStopSetting = True
        Me.optYuki.Text = "行き"
        Me.optYuki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optYuki.TextValue = "行き"
        Me.optYuki.UseVisualStyleBackColor = True
        Me.optYuki.WidthDef = 53
        '
        'LMI961F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI961F"
        Me.Text = "【LMI961】 GLIS見積情報照会"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlCondition.ResumeLayout(False)
        Me.pnlCondition.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSakuseiNaiyo.ResumeLayout(False)
        Me.pnlSakuseiNaiyo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCondition As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents pnlSakuseiNaiyo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents optKaeri As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optYuki As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents chkKokunai As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkYunyu As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkYushutsu As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleFwdUserNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleGoodsNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSearchRem As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEstNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEstMakeUserNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtEstMakeUserNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSearchRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtFwdUserNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtEstNoEda As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtEstNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
End Class
