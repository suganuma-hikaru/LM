<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMD100F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

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
        Dim sprZai_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprZai_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.sprZai = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.lblCustNM_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNM_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        sprZai_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprZai_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprZai_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprZai, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbSoko)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.sprZai)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM_M)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM_L)
        Me.pnlViewAria.Controls.Add(Me.lblCustCD_M)
        Me.pnlViewAria.Controls.Add(Me.lblCustCD_L)
        Me.pnlViewAria.Controls.Add(Me.lblCust)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.lblEigyo)
        Me.pnlViewAria.Size = New System.Drawing.Size(1018, 626)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "ロット指定"
        Me.FunctionKey.Location = New System.Drawing.Point(685, 1)
        '
        'sprZai
        '
        Me.sprZai.AccessibleDescription = ""
        Me.sprZai.AllowUserZoom = False
        Me.sprZai.AutoImeMode = False
        Me.sprZai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZai.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprZai.CellClickEventArgs = Nothing
        Me.sprZai.CheckToCheckBox = True
        Me.sprZai.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprZai.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZai.EditModeReplace = True
        Me.sprZai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZai.ForeColorDef = System.Drawing.Color.Empty
        Me.sprZai.HeightDef = 548
        Me.sprZai.KeyboardCheckBoxOn = False
        Me.sprZai.Location = New System.Drawing.Point(12, 72)
        Me.sprZai.Name = "sprZai"
        Me.sprZai.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZai.Size = New System.Drawing.Size(994, 548)
        Me.sprZai.SortColumn = True
        Me.sprZai.SpanColumnLock = True
        Me.sprZai.SpreadDoubleClicked = False
        Me.sprZai.TabIndex = 271
        Me.sprZai.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprZai.TextValue = Nothing
        Me.sprZai.UseGrouping = False
        Me.sprZai.WidthDef = 994
        sprZai_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprZai_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprZai.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprZai_InputMapWhenFocusedNormal)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprZai_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprZai.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprZai_InputMapWhenAncestorOfFocusedNormal)
        Me.sprZai.SetViewportTopRow(0, 0, 1)
        Me.sprZai.SetActiveViewport(0, -1, 0)
        '
        'lblCustNM_M
        '
        Me.lblCustNM_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_M.CountWrappedLine = False
        Me.lblCustNM_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_M.HeightDef = 18
        Me.lblCustNM_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.HissuLabelVisible = True
        Me.lblCustNM_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_M.IsByteCheck = 0
        Me.lblCustNM_M.IsCalendarCheck = False
        Me.lblCustNM_M.IsDakutenCheck = False
        Me.lblCustNM_M.IsEisuCheck = False
        Me.lblCustNM_M.IsForbiddenWordsCheck = False
        Me.lblCustNM_M.IsFullByteCheck = 0
        Me.lblCustNM_M.IsHankakuCheck = False
        Me.lblCustNM_M.IsHissuCheck = True
        Me.lblCustNM_M.IsKanaCheck = False
        Me.lblCustNM_M.IsMiddleSpace = False
        Me.lblCustNM_M.IsNumericCheck = False
        Me.lblCustNM_M.IsSujiCheck = False
        Me.lblCustNM_M.IsZenkakuCheck = False
        Me.lblCustNM_M.ItemName = ""
        Me.lblCustNM_M.LineSpace = 0
        Me.lblCustNM_M.Location = New System.Drawing.Point(134, 48)
        Me.lblCustNM_M.MaxLength = 0
        Me.lblCustNM_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_M.MaxLineCount = 0
        Me.lblCustNM_M.Multiline = False
        Me.lblCustNM_M.Name = "lblCustNM_M"
        Me.lblCustNM_M.ReadOnly = True
        Me.lblCustNM_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_M.Size = New System.Drawing.Size(492, 18)
        Me.lblCustNM_M.TabIndex = 268
        Me.lblCustNM_M.TabStop = False
        Me.lblCustNM_M.TabStopSetting = False
        Me.lblCustNM_M.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_M.UseSystemPasswordChar = False
        Me.lblCustNM_M.WidthDef = 492
        Me.lblCustNM_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNM_L
        '
        Me.lblCustNM_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_L.CountWrappedLine = False
        Me.lblCustNM_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_L.HeightDef = 18
        Me.lblCustNM_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.HissuLabelVisible = True
        Me.lblCustNM_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_L.IsByteCheck = 0
        Me.lblCustNM_L.IsCalendarCheck = False
        Me.lblCustNM_L.IsDakutenCheck = False
        Me.lblCustNM_L.IsEisuCheck = False
        Me.lblCustNM_L.IsForbiddenWordsCheck = False
        Me.lblCustNM_L.IsFullByteCheck = 0
        Me.lblCustNM_L.IsHankakuCheck = False
        Me.lblCustNM_L.IsHissuCheck = True
        Me.lblCustNM_L.IsKanaCheck = False
        Me.lblCustNM_L.IsMiddleSpace = False
        Me.lblCustNM_L.IsNumericCheck = False
        Me.lblCustNM_L.IsSujiCheck = False
        Me.lblCustNM_L.IsZenkakuCheck = False
        Me.lblCustNM_L.ItemName = ""
        Me.lblCustNM_L.LineSpace = 0
        Me.lblCustNM_L.Location = New System.Drawing.Point(134, 27)
        Me.lblCustNM_L.MaxLength = 0
        Me.lblCustNM_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_L.MaxLineCount = 0
        Me.lblCustNM_L.Multiline = False
        Me.lblCustNM_L.Name = "lblCustNM_L"
        Me.lblCustNM_L.ReadOnly = True
        Me.lblCustNM_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_L.Size = New System.Drawing.Size(492, 18)
        Me.lblCustNM_L.TabIndex = 267
        Me.lblCustNM_L.TabStop = False
        Me.lblCustNM_L.TabStopSetting = False
        Me.lblCustNM_L.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_L.UseSystemPasswordChar = False
        Me.lblCustNM_L.WidthDef = 492
        Me.lblCustNM_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCD_M
        '
        Me.lblCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCD_M.CountWrappedLine = False
        Me.lblCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCD_M.HeightDef = 18
        Me.lblCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.HissuLabelVisible = True
        Me.lblCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCD_M.IsByteCheck = 7
        Me.lblCustCD_M.IsCalendarCheck = False
        Me.lblCustCD_M.IsDakutenCheck = False
        Me.lblCustCD_M.IsEisuCheck = False
        Me.lblCustCD_M.IsForbiddenWordsCheck = False
        Me.lblCustCD_M.IsFullByteCheck = 0
        Me.lblCustCD_M.IsHankakuCheck = False
        Me.lblCustCD_M.IsHissuCheck = True
        Me.lblCustCD_M.IsKanaCheck = False
        Me.lblCustCD_M.IsMiddleSpace = False
        Me.lblCustCD_M.IsNumericCheck = False
        Me.lblCustCD_M.IsSujiCheck = False
        Me.lblCustCD_M.IsZenkakuCheck = False
        Me.lblCustCD_M.ItemName = ""
        Me.lblCustCD_M.LineSpace = 0
        Me.lblCustCD_M.Location = New System.Drawing.Point(101, 48)
        Me.lblCustCD_M.MaxLength = 7
        Me.lblCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCD_M.MaxLineCount = 0
        Me.lblCustCD_M.Multiline = False
        Me.lblCustCD_M.Name = "lblCustCD_M"
        Me.lblCustCD_M.ReadOnly = True
        Me.lblCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCD_M.Size = New System.Drawing.Size(49, 18)
        Me.lblCustCD_M.TabIndex = 270
        Me.lblCustCD_M.TabStop = False
        Me.lblCustCD_M.TabStopSetting = False
        Me.lblCustCD_M.TextValue = ""
        Me.lblCustCD_M.UseSystemPasswordChar = False
        Me.lblCustCD_M.WidthDef = 49
        Me.lblCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCD_L
        '
        Me.lblCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCD_L.CountWrappedLine = False
        Me.lblCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCD_L.HeightDef = 18
        Me.lblCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.HissuLabelVisible = True
        Me.lblCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCD_L.IsByteCheck = 7
        Me.lblCustCD_L.IsCalendarCheck = False
        Me.lblCustCD_L.IsDakutenCheck = False
        Me.lblCustCD_L.IsEisuCheck = False
        Me.lblCustCD_L.IsForbiddenWordsCheck = False
        Me.lblCustCD_L.IsFullByteCheck = 0
        Me.lblCustCD_L.IsHankakuCheck = False
        Me.lblCustCD_L.IsHissuCheck = True
        Me.lblCustCD_L.IsKanaCheck = False
        Me.lblCustCD_L.IsMiddleSpace = False
        Me.lblCustCD_L.IsNumericCheck = False
        Me.lblCustCD_L.IsSujiCheck = False
        Me.lblCustCD_L.IsZenkakuCheck = False
        Me.lblCustCD_L.ItemName = ""
        Me.lblCustCD_L.LineSpace = 0
        Me.lblCustCD_L.Location = New System.Drawing.Point(79, 27)
        Me.lblCustCD_L.MaxLength = 7
        Me.lblCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCD_L.MaxLineCount = 0
        Me.lblCustCD_L.Multiline = False
        Me.lblCustCD_L.Name = "lblCustCD_L"
        Me.lblCustCD_L.ReadOnly = True
        Me.lblCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCD_L.Size = New System.Drawing.Size(71, 18)
        Me.lblCustCD_L.TabIndex = 269
        Me.lblCustCD_L.TabStop = False
        Me.lblCustCD_L.TabStopSetting = False
        Me.lblCustCD_L.TextValue = ""
        Me.lblCustCD_L.UseSystemPasswordChar = False
        Me.lblCustCD_L.WidthDef = 71
        Me.lblCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.AutoSizeDef = True
        Me.lblCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCust.EnableStatus = False
        Me.lblCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.HeightDef = 13
        Me.lblCust.Location = New System.Drawing.Point(9, 30)
        Me.lblCust.MinimumSize = New System.Drawing.Size(68, 0)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.Size = New System.Drawing.Size(68, 13)
        Me.lblCust.TabIndex = 266
        Me.lblCust.Text = "荷主"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCust.TextValue = "荷主"
        Me.lblCust.WidthDef = 68
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(335, 9)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel1.TabIndex = 264
        Me.LmTitleLabel1.Text = "倉庫"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "倉庫"
        Me.LmTitleLabel1.WidthDef = 35
        '
        'lblEigyo
        '
        Me.lblEigyo.AutoSize = True
        Me.lblEigyo.AutoSizeDef = True
        Me.lblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblEigyo.EnableStatus = False
        Me.lblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.HeightDef = 13
        Me.lblEigyo.Location = New System.Drawing.Point(9, 9)
        Me.lblEigyo.MinimumSize = New System.Drawing.Size(68, 0)
        Me.lblEigyo.Name = "lblEigyo"
        Me.lblEigyo.Size = New System.Drawing.Size(68, 13)
        Me.lblEigyo.TabIndex = 262
        Me.lblEigyo.Text = "営業所"
        Me.lblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEigyo.TextValue = "営業所"
        Me.lblEigyo.WidthDef = 68
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbEigyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbEigyo.DataSource = Nothing
        Me.cmbEigyo.DisplayMember = Nothing
        Me.cmbEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbEigyo.HeightDef = 18
        Me.cmbEigyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.HissuLabelVisible = True
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = True
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(79, 6)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(250, 18)
        Me.cmbEigyo.TabIndex = 272
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 250
        '
        'cmbSoko
        '
        Me.cmbSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.cmbSoko.HissuLabelVisible = True
        Me.cmbSoko.InsertWildCard = True
        Me.cmbSoko.IsForbiddenWordsCheck = False
        Me.cmbSoko.IsHissuCheck = True
        Me.cmbSoko.ItemName = ""
        Me.cmbSoko.Location = New System.Drawing.Point(376, 6)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = True
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(250, 18)
        Me.cmbSoko.TabIndex = 273
        Me.cmbSoko.TabStop = False
        Me.cmbSoko.TabStopSetting = False
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 250
        '
        'LMD100F
        '
        Me.ClientSize = New System.Drawing.Size(1018, 706)
        Me.Name = "LMD100F"
        Me.Text = "【LMD100】 在庫テーブル照会"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprZai, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprZai As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblCustNM_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNM_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko

End Class
