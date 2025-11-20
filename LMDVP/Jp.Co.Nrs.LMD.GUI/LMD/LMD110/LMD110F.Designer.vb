<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMD110F
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
        Dim sprFurrikae_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprFurrikae_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.sprFurrikae = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.grpSeikyuSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.chkSelectByNrsB = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmYoukiKBN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmFurikaeKBN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSakiCustNM_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSakiCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSakiCustNM_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSakiCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbWare = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdFurikaeDate_E = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.imdFurikaeDate_S = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleFurikaeDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblMotoCustNM_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtMotoCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblMotoCustNM_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtMotoCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprFurrikae_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprFurrikae_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprFurrikae, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSeikyuSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpSeikyuSearch)
        Me.pnlViewAria.Controls.Add(Me.sprFurrikae)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'sprFurrikae
        '
        Me.sprFurrikae.AccessibleDescription = ""
        Me.sprFurrikae.AllowUserZoom = False
        Me.sprFurrikae.AutoImeMode = False
        Me.sprFurrikae.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFurrikae.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFurrikae.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprFurrikae.CellClickEventArgs = Nothing
        Me.sprFurrikae.CheckToCheckBox = True
        Me.sprFurrikae.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprFurrikae.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFurrikae.EditModeReplace = True
        Me.sprFurrikae.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFurrikae.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFurrikae.ForeColorDef = System.Drawing.Color.Empty
        Me.sprFurrikae.HeightDef = 650
        Me.sprFurrikae.KeyboardCheckBoxOn = False
        Me.sprFurrikae.Location = New System.Drawing.Point(10, 212)
        Me.sprFurrikae.Name = "sprFurrikae"
        Me.sprFurrikae.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFurrikae.Size = New System.Drawing.Size(1250, 650)
        Me.sprFurrikae.SortColumn = True
        Me.sprFurrikae.SpanColumnLock = True
        Me.sprFurrikae.SpreadDoubleClicked = False
        Me.sprFurrikae.TabIndex = 15
        Me.sprFurrikae.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Bottom
        Me.sprFurrikae.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprFurrikae.TextValue = Nothing
        Me.sprFurrikae.UseGrouping = False
        Me.sprFurrikae.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprFurrikae.WidthDef = 1250
        sprFurrikae_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprFurrikae_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprFurrikae.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprFurrikae_InputMapWhenFocusedNormal)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprFurrikae_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprFurrikae.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprFurrikae_InputMapWhenAncestorOfFocusedNormal)
        Me.sprFurrikae.SetViewportTopRow(0, 0, 1)
        Me.sprFurrikae.SetActiveViewport(0, -1, 0)
        '
        'grpSeikyuSearch
        '
        Me.grpSeikyuSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSeikyuSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSeikyuSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSeikyuSearch.Controls.Add(Me.chkSelectByNrsB)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel8)
        Me.grpSeikyuSearch.Controls.Add(Me.cmYoukiKBN)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel2)
        Me.grpSeikyuSearch.Controls.Add(Me.cmFurikaeKBN)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel1)
        Me.grpSeikyuSearch.Controls.Add(Me.lblSakiCustNM_M)
        Me.grpSeikyuSearch.Controls.Add(Me.txtSakiCustCD_M)
        Me.grpSeikyuSearch.Controls.Add(Me.lblSakiCustNM_L)
        Me.grpSeikyuSearch.Controls.Add(Me.txtSakiCustCD_L)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel7)
        Me.grpSeikyuSearch.Controls.Add(Me.btnPrint)
        Me.grpSeikyuSearch.Controls.Add(Me.cmbWare)
        Me.grpSeikyuSearch.Controls.Add(Me.cmbPrint)
        Me.grpSeikyuSearch.Controls.Add(Me.cmbEigyo)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel6)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel5)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel4)
        Me.grpSeikyuSearch.Controls.Add(Me.LmTitleLabel3)
        Me.grpSeikyuSearch.Controls.Add(Me.imdFurikaeDate_E)
        Me.grpSeikyuSearch.Controls.Add(Me.imdFurikaeDate_S)
        Me.grpSeikyuSearch.Controls.Add(Me.lblTitleFurikaeDate)
        Me.grpSeikyuSearch.Controls.Add(Me.lblMotoCustNM_M)
        Me.grpSeikyuSearch.Controls.Add(Me.txtMotoCustCD_M)
        Me.grpSeikyuSearch.Controls.Add(Me.lblMotoCustNM_L)
        Me.grpSeikyuSearch.Controls.Add(Me.txtMotoCustCD_L)
        Me.grpSeikyuSearch.EnableStatus = False
        Me.grpSeikyuSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSeikyuSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSeikyuSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSeikyuSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSeikyuSearch.HeightDef = 182
        Me.grpSeikyuSearch.Location = New System.Drawing.Point(10, 24)
        Me.grpSeikyuSearch.Name = "grpSeikyuSearch"
        Me.grpSeikyuSearch.Size = New System.Drawing.Size(1250, 182)
        Me.grpSeikyuSearch.TabIndex = 33
        Me.grpSeikyuSearch.TabStop = False
        Me.grpSeikyuSearch.Text = "検索条件"
        Me.grpSeikyuSearch.TextValue = "検索条件"
        Me.grpSeikyuSearch.WidthDef = 1250
        '
        'chkSelectByNrsB
        '
        Me.chkSelectByNrsB.AutoSize = True
        Me.chkSelectByNrsB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSelectByNrsB.EnableStatus = True
        Me.chkSelectByNrsB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.HeightDef = 17
        Me.chkSelectByNrsB.Location = New System.Drawing.Point(470, 142)
        Me.chkSelectByNrsB.Name = "chkSelectByNrsB"
        Me.chkSelectByNrsB.Size = New System.Drawing.Size(96, 17)
        Me.chkSelectByNrsB.TabIndex = 418
        Me.chkSelectByNrsB.TabStopSetting = True
        Me.chkSelectByNrsB.Text = "私の作成分"
        Me.chkSelectByNrsB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSelectByNrsB.TextValue = "私の作成分"
        Me.chkSelectByNrsB.UseVisualStyleBackColor = True
        Me.chkSelectByNrsB.WidthDef = 96
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSize = True
        Me.LmTitleLabel8.AutoSizeDef = True
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 13
        Me.LmTitleLabel8.Location = New System.Drawing.Point(218, 147)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel8.TabIndex = 417
        Me.LmTitleLabel8.Text = "容器変更"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "容器変更"
        Me.LmTitleLabel8.WidthDef = 63
        '
        'cmYoukiKBN
        '
        Me.cmYoukiKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmYoukiKBN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmYoukiKBN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmYoukiKBN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmYoukiKBN.DataCode = "U009"
        Me.cmYoukiKBN.DataSource = Nothing
        Me.cmYoukiKBN.DisplayMember = Nothing
        Me.cmYoukiKBN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmYoukiKBN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmYoukiKBN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmYoukiKBN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmYoukiKBN.HeightDef = 18
        Me.cmYoukiKBN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmYoukiKBN.HissuLabelVisible = False
        Me.cmYoukiKBN.InsertWildCard = True
        Me.cmYoukiKBN.IsForbiddenWordsCheck = False
        Me.cmYoukiKBN.IsHissuCheck = False
        Me.cmYoukiKBN.ItemName = ""
        Me.cmYoukiKBN.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmYoukiKBN.Location = New System.Drawing.Point(301, 142)
        Me.cmYoukiKBN.Name = "cmYoukiKBN"
        Me.cmYoukiKBN.ReadOnly = False
        Me.cmYoukiKBN.SelectedIndex = -1
        Me.cmYoukiKBN.SelectedItem = Nothing
        Me.cmYoukiKBN.SelectedText = ""
        Me.cmYoukiKBN.SelectedValue = ""
        Me.cmYoukiKBN.Size = New System.Drawing.Size(116, 18)
        Me.cmYoukiKBN.TabIndex = 416
        Me.cmYoukiKBN.TabStopSetting = True
        Me.cmYoukiKBN.TextValue = ""
        Me.cmYoukiKBN.Value1 = Nothing
        Me.cmYoukiKBN.Value2 = Nothing
        Me.cmYoukiKBN.Value3 = Nothing
        Me.cmYoukiKBN.ValueMember = Nothing
        Me.cmYoukiKBN.WidthDef = 116
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(16, 146)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel2.TabIndex = 415
        Me.LmTitleLabel2.Text = "振替区分"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "振替区分"
        Me.LmTitleLabel2.WidthDef = 63
        '
        'cmFurikaeKBN
        '
        Me.cmFurikaeKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmFurikaeKBN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmFurikaeKBN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmFurikaeKBN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmFurikaeKBN.DataCode = "H004"
        Me.cmFurikaeKBN.DataSource = Nothing
        Me.cmFurikaeKBN.DisplayMember = Nothing
        Me.cmFurikaeKBN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmFurikaeKBN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmFurikaeKBN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmFurikaeKBN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmFurikaeKBN.HeightDef = 18
        Me.cmFurikaeKBN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmFurikaeKBN.HissuLabelVisible = False
        Me.cmFurikaeKBN.InsertWildCard = True
        Me.cmFurikaeKBN.IsForbiddenWordsCheck = False
        Me.cmFurikaeKBN.IsHissuCheck = False
        Me.cmFurikaeKBN.ItemName = ""
        Me.cmFurikaeKBN.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmFurikaeKBN.Location = New System.Drawing.Point(85, 142)
        Me.cmFurikaeKBN.Name = "cmFurikaeKBN"
        Me.cmFurikaeKBN.ReadOnly = False
        Me.cmFurikaeKBN.SelectedIndex = -1
        Me.cmFurikaeKBN.SelectedItem = Nothing
        Me.cmFurikaeKBN.SelectedText = ""
        Me.cmFurikaeKBN.SelectedValue = ""
        Me.cmFurikaeKBN.Size = New System.Drawing.Size(132, 18)
        Me.cmFurikaeKBN.TabIndex = 414
        Me.cmFurikaeKBN.TabStopSetting = True
        Me.cmFurikaeKBN.TextValue = ""
        Me.cmFurikaeKBN.Value1 = Nothing
        Me.cmFurikaeKBN.Value2 = Nothing
        Me.cmFurikaeKBN.Value3 = Nothing
        Me.cmFurikaeKBN.ValueMember = Nothing
        Me.cmFurikaeKBN.WidthDef = 132
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(2, 95)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel1.TabIndex = 412
        Me.LmTitleLabel1.Text = "振替先荷主"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "振替先荷主"
        Me.LmTitleLabel1.WidthDef = 77
        '
        'lblSakiCustNM_M
        '
        Me.lblSakiCustNM_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSakiCustNM_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSakiCustNM_M.CountWrappedLine = False
        Me.lblSakiCustNM_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSakiCustNM_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSakiCustNM_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSakiCustNM_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSakiCustNM_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSakiCustNM_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSakiCustNM_M.HeightDef = 19
        Me.lblSakiCustNM_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_M.HissuLabelVisible = False
        Me.lblSakiCustNM_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSakiCustNM_M.IsByteCheck = 0
        Me.lblSakiCustNM_M.IsCalendarCheck = False
        Me.lblSakiCustNM_M.IsDakutenCheck = False
        Me.lblSakiCustNM_M.IsEisuCheck = False
        Me.lblSakiCustNM_M.IsForbiddenWordsCheck = False
        Me.lblSakiCustNM_M.IsFullByteCheck = 0
        Me.lblSakiCustNM_M.IsHankakuCheck = False
        Me.lblSakiCustNM_M.IsHissuCheck = False
        Me.lblSakiCustNM_M.IsKanaCheck = False
        Me.lblSakiCustNM_M.IsMiddleSpace = False
        Me.lblSakiCustNM_M.IsNumericCheck = False
        Me.lblSakiCustNM_M.IsSujiCheck = False
        Me.lblSakiCustNM_M.IsZenkakuCheck = False
        Me.lblSakiCustNM_M.ItemName = ""
        Me.lblSakiCustNM_M.LineSpace = 0
        Me.lblSakiCustNM_M.Location = New System.Drawing.Point(166, 116)
        Me.lblSakiCustNM_M.MaxLength = 0
        Me.lblSakiCustNM_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSakiCustNM_M.MaxLineCount = 0
        Me.lblSakiCustNM_M.Multiline = False
        Me.lblSakiCustNM_M.Name = "lblSakiCustNM_M"
        Me.lblSakiCustNM_M.ReadOnly = True
        Me.lblSakiCustNM_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSakiCustNM_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSakiCustNM_M.Size = New System.Drawing.Size(488, 19)
        Me.lblSakiCustNM_M.TabIndex = 413
        Me.lblSakiCustNM_M.TabStop = False
        Me.lblSakiCustNM_M.TabStopSetting = False
        Me.lblSakiCustNM_M.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSakiCustNM_M.UseSystemPasswordChar = False
        Me.lblSakiCustNM_M.WidthDef = 488
        Me.lblSakiCustNM_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSakiCustCD_M
        '
        Me.txtSakiCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSakiCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSakiCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSakiCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSakiCustCD_M.CountWrappedLine = False
        Me.txtSakiCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSakiCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSakiCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSakiCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSakiCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSakiCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSakiCustCD_M.HeightDef = 19
        Me.txtSakiCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSakiCustCD_M.HissuLabelVisible = False
        Me.txtSakiCustCD_M.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtSakiCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSakiCustCD_M.IsByteCheck = 2
        Me.txtSakiCustCD_M.IsCalendarCheck = False
        Me.txtSakiCustCD_M.IsDakutenCheck = False
        Me.txtSakiCustCD_M.IsEisuCheck = False
        Me.txtSakiCustCD_M.IsForbiddenWordsCheck = False
        Me.txtSakiCustCD_M.IsFullByteCheck = 0
        Me.txtSakiCustCD_M.IsHankakuCheck = False
        Me.txtSakiCustCD_M.IsHissuCheck = False
        Me.txtSakiCustCD_M.IsKanaCheck = False
        Me.txtSakiCustCD_M.IsMiddleSpace = False
        Me.txtSakiCustCD_M.IsNumericCheck = False
        Me.txtSakiCustCD_M.IsSujiCheck = False
        Me.txtSakiCustCD_M.IsZenkakuCheck = False
        Me.txtSakiCustCD_M.ItemName = ""
        Me.txtSakiCustCD_M.LineSpace = 0
        Me.txtSakiCustCD_M.Location = New System.Drawing.Point(130, 116)
        Me.txtSakiCustCD_M.MaxLength = 2
        Me.txtSakiCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSakiCustCD_M.MaxLineCount = 0
        Me.txtSakiCustCD_M.Multiline = False
        Me.txtSakiCustCD_M.Name = "txtSakiCustCD_M"
        Me.txtSakiCustCD_M.ReadOnly = False
        Me.txtSakiCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSakiCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSakiCustCD_M.Size = New System.Drawing.Size(52, 19)
        Me.txtSakiCustCD_M.TabIndex = 410
        Me.txtSakiCustCD_M.TabStopSetting = True
        Me.txtSakiCustCD_M.TextValue = ""
        Me.txtSakiCustCD_M.UseSystemPasswordChar = False
        Me.txtSakiCustCD_M.WidthDef = 52
        Me.txtSakiCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSakiCustNM_L
        '
        Me.lblSakiCustNM_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSakiCustNM_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSakiCustNM_L.CountWrappedLine = False
        Me.lblSakiCustNM_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSakiCustNM_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSakiCustNM_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSakiCustNM_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSakiCustNM_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSakiCustNM_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSakiCustNM_L.HeightDef = 19
        Me.lblSakiCustNM_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSakiCustNM_L.HissuLabelVisible = False
        Me.lblSakiCustNM_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSakiCustNM_L.IsByteCheck = 0
        Me.lblSakiCustNM_L.IsCalendarCheck = False
        Me.lblSakiCustNM_L.IsDakutenCheck = False
        Me.lblSakiCustNM_L.IsEisuCheck = False
        Me.lblSakiCustNM_L.IsForbiddenWordsCheck = False
        Me.lblSakiCustNM_L.IsFullByteCheck = 0
        Me.lblSakiCustNM_L.IsHankakuCheck = False
        Me.lblSakiCustNM_L.IsHissuCheck = False
        Me.lblSakiCustNM_L.IsKanaCheck = False
        Me.lblSakiCustNM_L.IsMiddleSpace = False
        Me.lblSakiCustNM_L.IsNumericCheck = False
        Me.lblSakiCustNM_L.IsSujiCheck = False
        Me.lblSakiCustNM_L.IsZenkakuCheck = False
        Me.lblSakiCustNM_L.ItemName = ""
        Me.lblSakiCustNM_L.LineSpace = 0
        Me.lblSakiCustNM_L.Location = New System.Drawing.Point(166, 95)
        Me.lblSakiCustNM_L.MaxLength = 0
        Me.lblSakiCustNM_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSakiCustNM_L.MaxLineCount = 0
        Me.lblSakiCustNM_L.Multiline = False
        Me.lblSakiCustNM_L.Name = "lblSakiCustNM_L"
        Me.lblSakiCustNM_L.ReadOnly = True
        Me.lblSakiCustNM_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSakiCustNM_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSakiCustNM_L.Size = New System.Drawing.Size(488, 19)
        Me.lblSakiCustNM_L.TabIndex = 411
        Me.lblSakiCustNM_L.TabStop = False
        Me.lblSakiCustNM_L.TabStopSetting = False
        Me.lblSakiCustNM_L.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSakiCustNM_L.UseSystemPasswordChar = False
        Me.lblSakiCustNM_L.WidthDef = 488
        Me.lblSakiCustNM_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSakiCustCD_L
        '
        Me.txtSakiCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSakiCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSakiCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSakiCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSakiCustCD_L.CountWrappedLine = False
        Me.txtSakiCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSakiCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSakiCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSakiCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSakiCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSakiCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSakiCustCD_L.HeightDef = 19
        Me.txtSakiCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSakiCustCD_L.HissuLabelVisible = False
        Me.txtSakiCustCD_L.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtSakiCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSakiCustCD_L.IsByteCheck = 5
        Me.txtSakiCustCD_L.IsCalendarCheck = False
        Me.txtSakiCustCD_L.IsDakutenCheck = False
        Me.txtSakiCustCD_L.IsEisuCheck = False
        Me.txtSakiCustCD_L.IsForbiddenWordsCheck = False
        Me.txtSakiCustCD_L.IsFullByteCheck = 0
        Me.txtSakiCustCD_L.IsHankakuCheck = False
        Me.txtSakiCustCD_L.IsHissuCheck = False
        Me.txtSakiCustCD_L.IsKanaCheck = False
        Me.txtSakiCustCD_L.IsMiddleSpace = False
        Me.txtSakiCustCD_L.IsNumericCheck = False
        Me.txtSakiCustCD_L.IsSujiCheck = False
        Me.txtSakiCustCD_L.IsZenkakuCheck = False
        Me.txtSakiCustCD_L.ItemName = ""
        Me.txtSakiCustCD_L.LineSpace = 0
        Me.txtSakiCustCD_L.Location = New System.Drawing.Point(85, 95)
        Me.txtSakiCustCD_L.MaxLength = 5
        Me.txtSakiCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSakiCustCD_L.MaxLineCount = 0
        Me.txtSakiCustCD_L.Multiline = False
        Me.txtSakiCustCD_L.Name = "txtSakiCustCD_L"
        Me.txtSakiCustCD_L.ReadOnly = False
        Me.txtSakiCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSakiCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSakiCustCD_L.Size = New System.Drawing.Size(97, 19)
        Me.txtSakiCustCD_L.TabIndex = 409
        Me.txtSakiCustCD_L.TabStopSetting = True
        Me.txtSakiCustCD_L.TextValue = ""
        Me.txtSakiCustCD_L.UseSystemPasswordChar = False
        Me.txtSakiCustCD_L.WidthDef = 97
        Me.txtSakiCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSize = True
        Me.LmTitleLabel7.AutoSizeDef = True
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 13
        Me.LmTitleLabel7.Location = New System.Drawing.Point(730, 86)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel7.TabIndex = 408
        Me.LmTitleLabel7.Text = "印刷種別"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "印刷種別"
        Me.LmTitleLabel7.WidthDef = 63
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnPrint.EnableStatus = True
        Me.btnPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.HeightDef = 22
        Me.btnPrint.Location = New System.Drawing.Point(1048, 81)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 123
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
        '
        'cmbWare
        '
        Me.cmbWare.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWare.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWare.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbWare.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbWare.DataSource = Nothing
        Me.cmbWare.DisplayMember = Nothing
        Me.cmbWare.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWare.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWare.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWare.HeightDef = 18
        Me.cmbWare.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWare.HissuLabelVisible = True
        Me.cmbWare.InsertWildCard = True
        Me.cmbWare.IsForbiddenWordsCheck = False
        Me.cmbWare.IsHissuCheck = True
        Me.cmbWare.ItemName = ""
        Me.cmbWare.Location = New System.Drawing.Point(801, 41)
        Me.cmbWare.Name = "cmbWare"
        Me.cmbWare.ReadOnly = False
        Me.cmbWare.SelectedIndex = -1
        Me.cmbWare.SelectedItem = Nothing
        Me.cmbWare.SelectedText = ""
        Me.cmbWare.SelectedValue = ""
        Me.cmbWare.Size = New System.Drawing.Size(250, 18)
        Me.cmbWare.TabIndex = 246
        Me.cmbWare.TabStopSetting = True
        Me.cmbWare.TextValue = ""
        Me.cmbWare.ValueMember = Nothing
        Me.cmbWare.WidthDef = 250
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "F025"
        Me.cmbPrint.DataSource = Nothing
        Me.cmbPrint.DisplayMember = Nothing
        Me.cmbPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.HeightDef = 18
        Me.cmbPrint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrint.HissuLabelVisible = False
        Me.cmbPrint.InsertWildCard = True
        Me.cmbPrint.IsForbiddenWordsCheck = False
        Me.cmbPrint.IsHissuCheck = False
        Me.cmbPrint.ItemName = ""
        Me.cmbPrint.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrint.Location = New System.Drawing.Point(801, 83)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(250, 18)
        Me.cmbPrint.TabIndex = 407
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 250
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbEigyo.Location = New System.Drawing.Point(801, 20)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = False
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(250, 18)
        Me.cmbEigyo.TabIndex = 245
        Me.cmbEigyo.TabStopSetting = True
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 250
        '
        'LmTitleLabel6
        '
        Me.LmTitleLabel6.AutoSize = True
        Me.LmTitleLabel6.AutoSizeDef = True
        Me.LmTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel6.EnableStatus = False
        Me.LmTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.HeightDef = 13
        Me.LmTitleLabel6.Location = New System.Drawing.Point(2, 49)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel6.TabIndex = 215
        Me.LmTitleLabel6.Text = "振替元荷主"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "振替元荷主"
        Me.LmTitleLabel6.WidthDef = 77
        '
        'LmTitleLabel5
        '
        Me.LmTitleLabel5.AutoSize = True
        Me.LmTitleLabel5.AutoSizeDef = True
        Me.LmTitleLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel5.EnableStatus = False
        Me.LmTitleLabel5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.HeightDef = 13
        Me.LmTitleLabel5.Location = New System.Drawing.Point(758, 43)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel5.TabIndex = 233
        Me.LmTitleLabel5.Text = "倉庫"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "倉庫"
        Me.LmTitleLabel5.WidthDef = 35
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSize = True
        Me.LmTitleLabel4.AutoSizeDef = True
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 13
        Me.LmTitleLabel4.Location = New System.Drawing.Point(744, 24)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel4.TabIndex = 231
        Me.LmTitleLabel4.Text = "営業所"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "営業所"
        Me.LmTitleLabel4.WidthDef = 49
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(191, 23)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel3.TabIndex = 229
        Me.LmTitleLabel3.Text = "～"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "～"
        Me.LmTitleLabel3.WidthDef = 21
        '
        'imdFurikaeDate_E
        '
        Me.imdFurikaeDate_E.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate_E.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate_E.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdFurikaeDate_E.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdFurikaeDate_E.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFurikaeDate_E.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate_E.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate_E.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate_E.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate_E.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdFurikaeDate_E.HeightDef = 18
        Me.imdFurikaeDate_E.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdFurikaeDate_E.HissuLabelVisible = False
        Me.imdFurikaeDate_E.Holiday = True
        Me.imdFurikaeDate_E.IsAfterDateCheck = False
        Me.imdFurikaeDate_E.IsBeforeDateCheck = False
        Me.imdFurikaeDate_E.IsHissuCheck = False
        Me.imdFurikaeDate_E.IsMinDateCheck = "1900/01/01"
        Me.imdFurikaeDate_E.ItemName = ""
        Me.imdFurikaeDate_E.Location = New System.Drawing.Point(218, 19)
        Me.imdFurikaeDate_E.Name = "imdFurikaeDate_E"
        Me.imdFurikaeDate_E.Number = CType(10101000000, Long)
        Me.imdFurikaeDate_E.ReadOnly = False
        Me.imdFurikaeDate_E.Size = New System.Drawing.Size(115, 18)
        Me.imdFurikaeDate_E.TabIndex = 228
        Me.imdFurikaeDate_E.TabStopSetting = True
        Me.imdFurikaeDate_E.TextValue = ""
        Me.imdFurikaeDate_E.Value = New Date(CType(0, Long))
        Me.imdFurikaeDate_E.WidthDef = 115
        '
        'imdFurikaeDate_S
        '
        Me.imdFurikaeDate_S.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate_S.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate_S.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdFurikaeDate_S.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdFurikaeDate_S.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFurikaeDate_S.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate_S.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate_S.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate_S.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate_S.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdFurikaeDate_S.HeightDef = 18
        Me.imdFurikaeDate_S.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdFurikaeDate_S.HissuLabelVisible = False
        Me.imdFurikaeDate_S.Holiday = True
        Me.imdFurikaeDate_S.IsAfterDateCheck = False
        Me.imdFurikaeDate_S.IsBeforeDateCheck = False
        Me.imdFurikaeDate_S.IsHissuCheck = False
        Me.imdFurikaeDate_S.IsMinDateCheck = "1900/01/01"
        Me.imdFurikaeDate_S.ItemName = ""
        Me.imdFurikaeDate_S.Location = New System.Drawing.Point(85, 19)
        Me.imdFurikaeDate_S.Name = "imdFurikaeDate_S"
        Me.imdFurikaeDate_S.Number = CType(10101000000, Long)
        Me.imdFurikaeDate_S.ReadOnly = False
        Me.imdFurikaeDate_S.Size = New System.Drawing.Size(115, 18)
        Me.imdFurikaeDate_S.TabIndex = 227
        Me.imdFurikaeDate_S.TabStopSetting = True
        Me.imdFurikaeDate_S.TextValue = ""
        Me.imdFurikaeDate_S.Value = New Date(CType(0, Long))
        Me.imdFurikaeDate_S.WidthDef = 115
        '
        'lblTitleFurikaeDate
        '
        Me.lblTitleFurikaeDate.AutoSize = True
        Me.lblTitleFurikaeDate.AutoSizeDef = True
        Me.lblTitleFurikaeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFurikaeDate.EnableStatus = False
        Me.lblTitleFurikaeDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeDate.HeightDef = 13
        Me.lblTitleFurikaeDate.Location = New System.Drawing.Point(30, 22)
        Me.lblTitleFurikaeDate.Name = "lblTitleFurikaeDate"
        Me.lblTitleFurikaeDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleFurikaeDate.TabIndex = 226
        Me.lblTitleFurikaeDate.Text = "振替日"
        Me.lblTitleFurikaeDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFurikaeDate.TextValue = "振替日"
        Me.lblTitleFurikaeDate.WidthDef = 49
        '
        'lblMotoCustNM_M
        '
        Me.lblMotoCustNM_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMotoCustNM_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMotoCustNM_M.CountWrappedLine = False
        Me.lblMotoCustNM_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMotoCustNM_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoCustNM_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoCustNM_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoCustNM_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoCustNM_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMotoCustNM_M.HeightDef = 19
        Me.lblMotoCustNM_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_M.HissuLabelVisible = False
        Me.lblMotoCustNM_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMotoCustNM_M.IsByteCheck = 0
        Me.lblMotoCustNM_M.IsCalendarCheck = False
        Me.lblMotoCustNM_M.IsDakutenCheck = False
        Me.lblMotoCustNM_M.IsEisuCheck = False
        Me.lblMotoCustNM_M.IsForbiddenWordsCheck = False
        Me.lblMotoCustNM_M.IsFullByteCheck = 0
        Me.lblMotoCustNM_M.IsHankakuCheck = False
        Me.lblMotoCustNM_M.IsHissuCheck = False
        Me.lblMotoCustNM_M.IsKanaCheck = False
        Me.lblMotoCustNM_M.IsMiddleSpace = False
        Me.lblMotoCustNM_M.IsNumericCheck = False
        Me.lblMotoCustNM_M.IsSujiCheck = False
        Me.lblMotoCustNM_M.IsZenkakuCheck = False
        Me.lblMotoCustNM_M.ItemName = ""
        Me.lblMotoCustNM_M.LineSpace = 0
        Me.lblMotoCustNM_M.Location = New System.Drawing.Point(166, 70)
        Me.lblMotoCustNM_M.MaxLength = 0
        Me.lblMotoCustNM_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMotoCustNM_M.MaxLineCount = 0
        Me.lblMotoCustNM_M.Multiline = False
        Me.lblMotoCustNM_M.Name = "lblMotoCustNM_M"
        Me.lblMotoCustNM_M.ReadOnly = True
        Me.lblMotoCustNM_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMotoCustNM_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMotoCustNM_M.Size = New System.Drawing.Size(488, 19)
        Me.lblMotoCustNM_M.TabIndex = 216
        Me.lblMotoCustNM_M.TabStop = False
        Me.lblMotoCustNM_M.TabStopSetting = False
        Me.lblMotoCustNM_M.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblMotoCustNM_M.UseSystemPasswordChar = False
        Me.lblMotoCustNM_M.WidthDef = 488
        Me.lblMotoCustNM_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtMotoCustCD_M
        '
        Me.txtMotoCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMotoCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMotoCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMotoCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtMotoCustCD_M.CountWrappedLine = False
        Me.txtMotoCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtMotoCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMotoCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMotoCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMotoCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMotoCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtMotoCustCD_M.HeightDef = 19
        Me.txtMotoCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtMotoCustCD_M.HissuLabelVisible = False
        Me.txtMotoCustCD_M.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtMotoCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtMotoCustCD_M.IsByteCheck = 2
        Me.txtMotoCustCD_M.IsCalendarCheck = False
        Me.txtMotoCustCD_M.IsDakutenCheck = False
        Me.txtMotoCustCD_M.IsEisuCheck = False
        Me.txtMotoCustCD_M.IsForbiddenWordsCheck = False
        Me.txtMotoCustCD_M.IsFullByteCheck = 0
        Me.txtMotoCustCD_M.IsHankakuCheck = False
        Me.txtMotoCustCD_M.IsHissuCheck = False
        Me.txtMotoCustCD_M.IsKanaCheck = False
        Me.txtMotoCustCD_M.IsMiddleSpace = False
        Me.txtMotoCustCD_M.IsNumericCheck = False
        Me.txtMotoCustCD_M.IsSujiCheck = False
        Me.txtMotoCustCD_M.IsZenkakuCheck = False
        Me.txtMotoCustCD_M.ItemName = ""
        Me.txtMotoCustCD_M.LineSpace = 0
        Me.txtMotoCustCD_M.Location = New System.Drawing.Point(130, 70)
        Me.txtMotoCustCD_M.MaxLength = 2
        Me.txtMotoCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtMotoCustCD_M.MaxLineCount = 0
        Me.txtMotoCustCD_M.Multiline = False
        Me.txtMotoCustCD_M.Name = "txtMotoCustCD_M"
        Me.txtMotoCustCD_M.ReadOnly = False
        Me.txtMotoCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtMotoCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMotoCustCD_M.Size = New System.Drawing.Size(52, 19)
        Me.txtMotoCustCD_M.TabIndex = 213
        Me.txtMotoCustCD_M.TabStopSetting = True
        Me.txtMotoCustCD_M.TextValue = ""
        Me.txtMotoCustCD_M.UseSystemPasswordChar = False
        Me.txtMotoCustCD_M.WidthDef = 52
        Me.txtMotoCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblMotoCustNM_L
        '
        Me.lblMotoCustNM_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMotoCustNM_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMotoCustNM_L.CountWrappedLine = False
        Me.lblMotoCustNM_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMotoCustNM_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoCustNM_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoCustNM_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoCustNM_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoCustNM_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMotoCustNM_L.HeightDef = 19
        Me.lblMotoCustNM_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoCustNM_L.HissuLabelVisible = False
        Me.lblMotoCustNM_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMotoCustNM_L.IsByteCheck = 0
        Me.lblMotoCustNM_L.IsCalendarCheck = False
        Me.lblMotoCustNM_L.IsDakutenCheck = False
        Me.lblMotoCustNM_L.IsEisuCheck = False
        Me.lblMotoCustNM_L.IsForbiddenWordsCheck = False
        Me.lblMotoCustNM_L.IsFullByteCheck = 0
        Me.lblMotoCustNM_L.IsHankakuCheck = False
        Me.lblMotoCustNM_L.IsHissuCheck = False
        Me.lblMotoCustNM_L.IsKanaCheck = False
        Me.lblMotoCustNM_L.IsMiddleSpace = False
        Me.lblMotoCustNM_L.IsNumericCheck = False
        Me.lblMotoCustNM_L.IsSujiCheck = False
        Me.lblMotoCustNM_L.IsZenkakuCheck = False
        Me.lblMotoCustNM_L.ItemName = ""
        Me.lblMotoCustNM_L.LineSpace = 0
        Me.lblMotoCustNM_L.Location = New System.Drawing.Point(166, 49)
        Me.lblMotoCustNM_L.MaxLength = 0
        Me.lblMotoCustNM_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMotoCustNM_L.MaxLineCount = 0
        Me.lblMotoCustNM_L.Multiline = False
        Me.lblMotoCustNM_L.Name = "lblMotoCustNM_L"
        Me.lblMotoCustNM_L.ReadOnly = True
        Me.lblMotoCustNM_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMotoCustNM_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMotoCustNM_L.Size = New System.Drawing.Size(488, 19)
        Me.lblMotoCustNM_L.TabIndex = 214
        Me.lblMotoCustNM_L.TabStop = False
        Me.lblMotoCustNM_L.TabStopSetting = False
        Me.lblMotoCustNM_L.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblMotoCustNM_L.UseSystemPasswordChar = False
        Me.lblMotoCustNM_L.WidthDef = 488
        Me.lblMotoCustNM_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtMotoCustCD_L
        '
        Me.txtMotoCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMotoCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtMotoCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMotoCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtMotoCustCD_L.CountWrappedLine = False
        Me.txtMotoCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtMotoCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMotoCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMotoCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMotoCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMotoCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtMotoCustCD_L.HeightDef = 19
        Me.txtMotoCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtMotoCustCD_L.HissuLabelVisible = False
        Me.txtMotoCustCD_L.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtMotoCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtMotoCustCD_L.IsByteCheck = 5
        Me.txtMotoCustCD_L.IsCalendarCheck = False
        Me.txtMotoCustCD_L.IsDakutenCheck = False
        Me.txtMotoCustCD_L.IsEisuCheck = False
        Me.txtMotoCustCD_L.IsForbiddenWordsCheck = False
        Me.txtMotoCustCD_L.IsFullByteCheck = 0
        Me.txtMotoCustCD_L.IsHankakuCheck = False
        Me.txtMotoCustCD_L.IsHissuCheck = False
        Me.txtMotoCustCD_L.IsKanaCheck = False
        Me.txtMotoCustCD_L.IsMiddleSpace = False
        Me.txtMotoCustCD_L.IsNumericCheck = False
        Me.txtMotoCustCD_L.IsSujiCheck = False
        Me.txtMotoCustCD_L.IsZenkakuCheck = False
        Me.txtMotoCustCD_L.ItemName = ""
        Me.txtMotoCustCD_L.LineSpace = 0
        Me.txtMotoCustCD_L.Location = New System.Drawing.Point(85, 49)
        Me.txtMotoCustCD_L.MaxLength = 5
        Me.txtMotoCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtMotoCustCD_L.MaxLineCount = 0
        Me.txtMotoCustCD_L.Multiline = False
        Me.txtMotoCustCD_L.Name = "txtMotoCustCD_L"
        Me.txtMotoCustCD_L.ReadOnly = False
        Me.txtMotoCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtMotoCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMotoCustCD_L.Size = New System.Drawing.Size(97, 19)
        Me.txtMotoCustCD_L.TabIndex = 212
        Me.txtMotoCustCD_L.TabStopSetting = True
        Me.txtMotoCustCD_L.TextValue = ""
        Me.txtMotoCustCD_L.UseSystemPasswordChar = False
        Me.txtMotoCustCD_L.WidthDef = 97
        Me.txtMotoCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMD110F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMD110F"
        Me.Text = "【LMD110】 作業料明細書作成"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprFurrikae, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSeikyuSearch.ResumeLayout(False)
        Me.grpSeikyuSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprFurrikae As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents grpSeikyuSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblMotoCustNM_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblMotoCustNM_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtMotoCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtMotoCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdFurikaeDate_E As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdFurikaeDate_S As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleFurikaeDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbWare As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSakiCustNM_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSakiCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSakiCustNM_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSakiCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmYoukiKBN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmFurikaeKBN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents chkSelectByNrsB As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox

End Class
