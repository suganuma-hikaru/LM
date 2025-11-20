<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMZ010F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopM

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMZ010F))
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUserNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUserCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblUserNm)
        Me.pnlViewAria.Controls.Add(Me.lblUserCd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(258, 1)
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
        Me.sprDetail.HeightDef = 299
        Me.sprDetail.Location = New System.Drawing.Point(12, 33)
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
        Me.sprDetail.Size = New System.Drawing.Size(570, 299)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 236
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 570
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(11, 12)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel2.TabIndex = 239
        Me.LmTitleLabel2.Text = "ユーザー"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "ユーザー"
        Me.LmTitleLabel2.WidthDef = 63
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUserNm.CountWrappedLine = False
        Me.lblUserNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUserNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUserNm.HeightDef = 18
        Me.lblUserNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserNm.HissuLabelVisible = True
        Me.lblUserNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUserNm.IsByteCheck = 0
        Me.lblUserNm.IsCalendarCheck = False
        Me.lblUserNm.IsDakutenCheck = False
        Me.lblUserNm.IsEisuCheck = False
        Me.lblUserNm.IsForbiddenWordsCheck = False
        Me.lblUserNm.IsFullByteCheck = 0
        Me.lblUserNm.IsHankakuCheck = False
        Me.lblUserNm.IsHissuCheck = True
        Me.lblUserNm.IsKanaCheck = False
        Me.lblUserNm.IsMiddleSpace = False
        Me.lblUserNm.IsNumericCheck = False
        Me.lblUserNm.IsSujiCheck = False
        Me.lblUserNm.IsZenkakuCheck = False
        Me.lblUserNm.ItemName = ""
        Me.lblUserNm.LineSpace = 0
        Me.lblUserNm.Location = New System.Drawing.Point(158, 9)
        Me.lblUserNm.MaxLength = 0
        Me.lblUserNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUserNm.MaxLineCount = 0
        Me.lblUserNm.Multiline = False
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.ReadOnly = True
        Me.lblUserNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUserNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUserNm.Size = New System.Drawing.Size(194, 18)
        Me.lblUserNm.TabIndex = 604
        Me.lblUserNm.TabStop = False
        Me.lblUserNm.TabStopSetting = False
        Me.lblUserNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblUserNm.UseSystemPasswordChar = False
        Me.lblUserNm.WidthDef = 194
        Me.lblUserNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUserCd
        '
        Me.lblUserCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUserCd.CountWrappedLine = False
        Me.lblUserCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUserCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUserCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUserCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUserCd.HeightDef = 18
        Me.lblUserCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUserCd.HissuLabelVisible = False
        Me.lblUserCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUserCd.IsByteCheck = 0
        Me.lblUserCd.IsCalendarCheck = False
        Me.lblUserCd.IsDakutenCheck = False
        Me.lblUserCd.IsEisuCheck = False
        Me.lblUserCd.IsForbiddenWordsCheck = False
        Me.lblUserCd.IsFullByteCheck = 0
        Me.lblUserCd.IsHankakuCheck = False
        Me.lblUserCd.IsHissuCheck = False
        Me.lblUserCd.IsKanaCheck = False
        Me.lblUserCd.IsMiddleSpace = False
        Me.lblUserCd.IsNumericCheck = False
        Me.lblUserCd.IsSujiCheck = False
        Me.lblUserCd.IsZenkakuCheck = False
        Me.lblUserCd.ItemName = ""
        Me.lblUserCd.LineSpace = 0
        Me.lblUserCd.Location = New System.Drawing.Point(77, 9)
        Me.lblUserCd.MaxLength = 0
        Me.lblUserCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUserCd.MaxLineCount = 0
        Me.lblUserCd.Multiline = False
        Me.lblUserCd.Name = "lblUserCd"
        Me.lblUserCd.ReadOnly = True
        Me.lblUserCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUserCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUserCd.Size = New System.Drawing.Size(97, 18)
        Me.lblUserCd.TabIndex = 603
        Me.lblUserCd.TabStop = False
        Me.lblUserCd.TabStopSetting = False
        Me.lblUserCd.TextValue = ""
        Me.lblUserCd.UseSystemPasswordChar = False
        Me.lblUserCd.WidthDef = 97
        Me.lblUserCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMZ010F
        '
        Me.ClientSize = New System.Drawing.Size(594, 418)
        'Me.Name = "LMZ010F"
        'Me.Text = "【LMZ010】 初期荷主変更"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUserNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUserCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
