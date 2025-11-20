<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMH090F
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
        Dim sprEdiList_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprEdiList_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.sprEdiList = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        sprEdiList_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprEdiList_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprEdiList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprEdiList)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'sprEdiList
        '
        Me.sprEdiList.AccessibleDescription = ""
        Me.sprEdiList.AllowUserZoom = False
        Me.sprEdiList.AutoImeMode = False
        Me.sprEdiList.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprEdiList.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprEdiList.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprEdiList.CellClickEventArgs = Nothing
        Me.sprEdiList.CheckToCheckBox = True
        Me.sprEdiList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprEdiList.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprEdiList.EditModeReplace = True
        Me.sprEdiList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprEdiList.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprEdiList.ForeColorDef = System.Drawing.Color.Empty
        Me.sprEdiList.HeightDef = 867
        Me.sprEdiList.KeyboardCheckBoxOn = False
        Me.sprEdiList.Location = New System.Drawing.Point(9, 9)
        Me.sprEdiList.Name = "sprEdiList"
        Me.sprEdiList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprEdiList.Size = New System.Drawing.Size(1257, 867)
        Me.sprEdiList.SortColumn = True
        Me.sprEdiList.SpanColumnLock = True
        Me.sprEdiList.SpreadDoubleClicked = False
        Me.sprEdiList.TabIndex = 16
        Me.sprEdiList.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprEdiList.TextValue = Nothing
        Me.sprEdiList.UseGrouping = False
        Me.sprEdiList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprEdiList.WidthDef = 1257
        Me.sprEdiList.SetViewportTopRow(0, 0, 1)
        Me.sprEdiList.SetActiveViewport(0, -1, 0)
        '
        'LMH090F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMH090F"
        Me.Text = "【LMH090】 現品票印刷"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprEdiList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprEdiList As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch

End Class
