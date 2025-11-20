<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMU020F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.sprFileImport = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprFileImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprFileImport)
        Me.pnlViewAria.Size = New System.Drawing.Size(1016, 624)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1016, 40)
        Me.FunctionKey.WidthDef = 1016
        '
        'sprFileImport
        '
        Me.sprFileImport.AccessibleDescription = ""
        Me.sprFileImport.AllowUserZoom = False
        Me.sprFileImport.AutoImeMode = False
        Me.sprFileImport.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFileImport.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprFileImport.CellClickEventArgs = Nothing
        Me.sprFileImport.CheckToCheckBox = True
        Me.sprFileImport.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprFileImport.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFileImport.EditModeReplace = True
        Me.sprFileImport.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFileImport.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFileImport.ForeColorDef = System.Drawing.Color.Empty
        Me.sprFileImport.HeightDef = 355
        Me.sprFileImport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sprFileImport.Location = New System.Drawing.Point(12, 24)
        Me.sprFileImport.Name = "sprFileImport"
        Me.sprFileImport.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFileImport.Size = New System.Drawing.Size(579, 355)
        Me.sprFileImport.SortColumn = True
        Me.sprFileImport.SpreadDoubleClicked = False
        Me.sprFileImport.TabIndex = 0
        Me.sprFileImport.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprFileImport.TextValue = Nothing
        Me.sprFileImport.WidthDef = 579
        '
        'NFD300F
        '
        Me.ClientSize = New System.Drawing.Size(1016, 704)
        Me.Name = "NFD300F"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprFileImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprFileImport As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread

End Class
