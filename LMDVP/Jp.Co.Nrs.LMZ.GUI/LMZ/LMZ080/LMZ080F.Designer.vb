<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMZ080F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMZ080F))
        Me.sprKyori = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprKyori, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.sprKyori)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(258, 1)
        '
        'sprKyori
        '
        Me.sprKyori.AccessibleDescription = ""
        Me.sprKyori.AllowUserZoom = False
        Me.sprKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprKyori.CellClickEventArgs = Nothing
        Me.sprKyori.CheckToCheckBox = True
        Me.sprKyori.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprKyori.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprKyori.EditModeReplace = True
        Me.sprKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprKyori.ForeColorDef = System.Drawing.Color.Empty
        Me.sprKyori.HeightDef = 300
        Me.sprKyori.Location = New System.Drawing.Point(13, 32)
        Me.sprKyori.Name = "sprKyori"
        Me.sprKyori.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprKyori.SetViewportTopRow(0, 0, 1)
        Me.sprKyori.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprKyori.SetViewportTopRow(0, 0, 1)
        Me.sprKyori.SetActiveViewport(0, -1, 0)
        Me.sprKyori.Size = New System.Drawing.Size(570, 300)
        Me.sprKyori.SortColumn = True
        Me.sprKyori.SpanColumnLock = True
        Me.sprKyori.SpreadDoubleClicked = False
        Me.sprKyori.TabIndex = 5
        Me.sprKyori.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprKyori.TextValue = Nothing
        Me.sprKyori.WidthDef = 570
        '
        'cmbNrsBrCd
        '
        Me.cmbNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsBrCd.DataSource = Nothing
        Me.cmbNrsBrCd.DisplayMember = Nothing
        Me.cmbNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.HeightDef = 18
        Me.cmbNrsBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.HissuLabelVisible = True
        Me.cmbNrsBrCd.InsertWildCard = True
        Me.cmbNrsBrCd.IsForbiddenWordsCheck = False
        Me.cmbNrsBrCd.IsHissuCheck = True
        Me.cmbNrsBrCd.ItemName = ""
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(78, 8)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(240, 18)
        Me.cmbNrsBrCd.TabIndex = 258
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 240
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(23, 13)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel2.TabIndex = 257
        Me.LmTitleLabel2.Text = "営業所"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "営業所"
        Me.LmTitleLabel2.WidthDef = 49
        '
        'LMZ080F
        '
        Me.ClientSize = New System.Drawing.Size(594, 418)
        Me.Name = "LMZ080F"
        Me.Text = "【LMZ080】 距離程マスタ照会"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprKyori, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprKyori As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
