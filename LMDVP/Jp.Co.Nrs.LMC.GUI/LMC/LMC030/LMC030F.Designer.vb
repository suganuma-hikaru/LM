<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMC030F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL8B

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMC030F))
        Me.sprOkuriList = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtDenpNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtSyukkaLNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprOkuriList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprOkuriList)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.txtDenpNo)
        Me.pnlViewAria.Controls.Add(Me.txtSyukkaLNo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        '
        'sprOkuriList
        '
        Me.sprOkuriList.AccessibleDescription = ""
        Me.sprOkuriList.AllowUserZoom = False
        Me.sprOkuriList.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprOkuriList.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprOkuriList.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprOkuriList.CellClickEventArgs = Nothing
        Me.sprOkuriList.CheckToCheckBox = True
        Me.sprOkuriList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprOkuriList.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprOkuriList.EditModeReplace = True
        Me.sprOkuriList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprOkuriList.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprOkuriList.ForeColorDef = System.Drawing.Color.Empty
        Me.sprOkuriList.HeightDef = 420
        Me.sprOkuriList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprOkuriList.KeyboardCheckBoxOn = False
        Me.sprOkuriList.Location = New System.Drawing.Point(12, 62)
        Me.sprOkuriList.Name = "sprOkuriList"
        Me.sprOkuriList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        Me.sprOkuriList.Size = New System.Drawing.Size(770, 420)
        Me.sprOkuriList.SortColumn = True
        Me.sprOkuriList.SpanColumnLock = True
        Me.sprOkuriList.SpreadDoubleClicked = False
        Me.sprOkuriList.TabIndex = 237
        Me.sprOkuriList.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprOkuriList.TextValue = Nothing
        Me.sprOkuriList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprOkuriList.WidthDef = 770
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(12, 20)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel1.TabIndex = 233
        Me.LmTitleLabel1.Text = "出荷管理番号(大)"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "出荷管理番号(大)"
        Me.LmTitleLabel1.WidthDef = 119
        '
        'txtDenpNo
        '
        Me.txtDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDenpNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDenpNo.CountWrappedLine = False
        Me.txtDenpNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDenpNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDenpNo.HeightDef = 18
        Me.txtDenpNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDenpNo.HissuLabelVisible = True
        Me.txtDenpNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtDenpNo.IsByteCheck = 0
        Me.txtDenpNo.IsCalendarCheck = False
        Me.txtDenpNo.IsDakutenCheck = False
        Me.txtDenpNo.IsEisuCheck = False
        Me.txtDenpNo.IsForbiddenWordsCheck = False
        Me.txtDenpNo.IsFullByteCheck = 0
        Me.txtDenpNo.IsHankakuCheck = False
        Me.txtDenpNo.IsHissuCheck = True
        Me.txtDenpNo.IsKanaCheck = False
        Me.txtDenpNo.IsMiddleSpace = False
        Me.txtDenpNo.IsNumericCheck = False
        Me.txtDenpNo.IsSujiCheck = False
        Me.txtDenpNo.IsZenkakuCheck = False
        Me.txtDenpNo.ItemName = ""
        Me.txtDenpNo.LineSpace = 0
        Me.txtDenpNo.Location = New System.Drawing.Point(135, 38)
        Me.txtDenpNo.MaxLength = 0
        Me.txtDenpNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDenpNo.MaxLineCount = 0
        Me.txtDenpNo.Multiline = False
        Me.txtDenpNo.Name = "txtDenpNo"
        Me.txtDenpNo.ReadOnly = False
        Me.txtDenpNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDenpNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDenpNo.Size = New System.Drawing.Size(241, 18)
        Me.txtDenpNo.TabIndex = 236
        Me.txtDenpNo.TabStopSetting = True
        Me.txtDenpNo.TextValue = ""
        Me.txtDenpNo.UseSystemPasswordChar = False
        Me.txtDenpNo.WidthDef = 241
        Me.txtDenpNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSyukkaLNo
        '
        Me.txtSyukkaLNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaLNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaLNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSyukkaLNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSyukkaLNo.CountWrappedLine = False
        Me.txtSyukkaLNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSyukkaLNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaLNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaLNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaLNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaLNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSyukkaLNo.HeightDef = 18
        Me.txtSyukkaLNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaLNo.HissuLabelVisible = True
        Me.txtSyukkaLNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSyukkaLNo.IsByteCheck = 0
        Me.txtSyukkaLNo.IsCalendarCheck = False
        Me.txtSyukkaLNo.IsDakutenCheck = False
        Me.txtSyukkaLNo.IsEisuCheck = False
        Me.txtSyukkaLNo.IsForbiddenWordsCheck = False
        Me.txtSyukkaLNo.IsFullByteCheck = 0
        Me.txtSyukkaLNo.IsHankakuCheck = False
        Me.txtSyukkaLNo.IsHissuCheck = True
        Me.txtSyukkaLNo.IsKanaCheck = False
        Me.txtSyukkaLNo.IsMiddleSpace = False
        Me.txtSyukkaLNo.IsNumericCheck = False
        Me.txtSyukkaLNo.IsSujiCheck = False
        Me.txtSyukkaLNo.IsZenkakuCheck = False
        Me.txtSyukkaLNo.ItemName = ""
        Me.txtSyukkaLNo.LineSpace = 0
        Me.txtSyukkaLNo.Location = New System.Drawing.Point(135, 17)
        Me.txtSyukkaLNo.MaxLength = 0
        Me.txtSyukkaLNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSyukkaLNo.MaxLineCount = 0
        Me.txtSyukkaLNo.Multiline = False
        Me.txtSyukkaLNo.Name = "txtSyukkaLNo"
        Me.txtSyukkaLNo.ReadOnly = False
        Me.txtSyukkaLNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSyukkaLNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSyukkaLNo.Size = New System.Drawing.Size(97, 18)
        Me.txtSyukkaLNo.TabIndex = 234
        Me.txtSyukkaLNo.TabStopSetting = True
        Me.txtSyukkaLNo.TextValue = ""
        Me.txtSyukkaLNo.UseSystemPasswordChar = False
        Me.txtSyukkaLNo.WidthDef = 97
        Me.txtSyukkaLNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(54, 41)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel3.TabIndex = 235
        Me.LmTitleLabel3.Text = "送り状番号"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "送り状番号"
        Me.LmTitleLabel3.WidthDef = 77
        '
        'LMC030F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMC030F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprOkuriList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprOkuriList As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDenpNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSyukkaLNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
