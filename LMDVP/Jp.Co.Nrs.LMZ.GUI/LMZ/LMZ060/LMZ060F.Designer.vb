<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMZ060F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMZ060F))
        Me.sprZip = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprZip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprZip)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(445, 1)
        '
        'sprZip
        '
        Me.sprZip.AccessibleDescription = ""
        Me.sprZip.AllowUserZoom = False
        Me.sprZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZip.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprZip.CellClickEventArgs = Nothing
        Me.sprZip.CheckToCheckBox = True
        Me.sprZip.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprZip.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZip.EditModeReplace = True
        Me.sprZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZip.ForeColorDef = System.Drawing.Color.Empty
        Me.sprZip.HeightDef = 476
        Me.sprZip.Location = New System.Drawing.Point(12, 6)
        Me.sprZip.Name = "sprZip"
        Me.sprZip.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZip.SetViewportTopRow(0, 0, 1)
        Me.sprZip.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprZip.SetViewportTopRow(0, 0, 1)
        Me.sprZip.SetActiveViewport(0, -1, 0)
        Me.sprZip.Size = New System.Drawing.Size(770, 476)
        Me.sprZip.SortColumn = True
        Me.sprZip.SpreadDoubleClicked = False
        Me.sprZip.TabIndex = 4
        Me.sprZip.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprZip.TextValue = Nothing
        Me.sprZip.WidthDef = 770
        '
        'LMZ060F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMZ060F"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprZip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprZip As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch

End Class
