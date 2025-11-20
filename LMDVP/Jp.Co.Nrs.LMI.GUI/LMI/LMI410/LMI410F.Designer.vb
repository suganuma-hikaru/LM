<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI410F
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
        Dim sprIdoList_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprIdoList_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim DateYearDisplayField6 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField11 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField6 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField12 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField6 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField6 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField6 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField6 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField5 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField9 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField5 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField10 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField5 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField5 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField5 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField5 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField4 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField4 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.sprIdoList = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleFromTo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdSearchDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.imdSearchDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.pnlSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtUserNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUserCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbHoukoku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.btnJikkou = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbSearchDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.pnlIkkatu = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblIkkatuCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnIkkatu = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbIkkatuKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtIkkatuCustM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtIkkatuCustL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.imdIkkatuDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        sprIdoList_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprIdoList_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprIdoList_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprIdoList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlIkkatu.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.pnlIkkatu)
        Me.pnlViewAria.Controls.Add(Me.sprIdoList)
        Me.pnlViewAria.Controls.Add(Me.pnlSearch)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'sprIdoList
        '
        Me.sprIdoList.AccessibleDescription = ""
        Me.sprIdoList.AllowUserZoom = False
        Me.sprIdoList.AutoImeMode = False
        Me.sprIdoList.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprIdoList.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprIdoList.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprIdoList.CellClickEventArgs = Nothing
        Me.sprIdoList.CheckToCheckBox = True
        Me.sprIdoList.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprIdoList.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprIdoList.EditModeReplace = True
        Me.sprIdoList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprIdoList.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprIdoList.ForeColorDef = System.Drawing.Color.Empty
        Me.sprIdoList.HeightDef = 710
        Me.sprIdoList.KeyboardCheckBoxOn = False
        Me.sprIdoList.Location = New System.Drawing.Point(12, 166)
        Me.sprIdoList.Name = "sprIdoList"
        Me.sprIdoList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprIdoList.Size = New System.Drawing.Size(1250, 710)
        Me.sprIdoList.SortColumn = True
        Me.sprIdoList.SpanColumnLock = True
        Me.sprIdoList.SpreadDoubleClicked = False
        Me.sprIdoList.TabIndex = 15
        Me.sprIdoList.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprIdoList.TextValue = Nothing
        Me.sprIdoList.UseGrouping = False
        Me.sprIdoList.WidthDef = 1250
        sprIdoList_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprIdoList_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprIdoList.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprIdoList_InputMapWhenFocusedNormal)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprIdoList_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprIdoList.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprIdoList_InputMapWhenAncestorOfFocusedNormal)
        Me.sprIdoList.SetViewportTopRow(0, 0, 1)
        Me.sprIdoList.SetActiveViewport(0, -1, 0)
        '
        'lblTitleEigyo
        '
        Me.lblTitleEigyo.AutoSize = True
        Me.lblTitleEigyo.AutoSizeDef = True
        Me.lblTitleEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo.EnableStatus = False
        Me.lblTitleEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.HeightDef = 13
        Me.lblTitleEigyo.Location = New System.Drawing.Point(18, 26)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 535
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdL.CountWrappedLine = False
        Me.txtCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdL.HeightDef = 18
        Me.txtCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.HissuLabelVisible = False
        Me.txtCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdL.IsByteCheck = 5
        Me.txtCustCdL.IsCalendarCheck = False
        Me.txtCustCdL.IsDakutenCheck = False
        Me.txtCustCdL.IsEisuCheck = False
        Me.txtCustCdL.IsForbiddenWordsCheck = False
        Me.txtCustCdL.IsFullByteCheck = 0
        Me.txtCustCdL.IsHankakuCheck = False
        Me.txtCustCdL.IsHissuCheck = False
        Me.txtCustCdL.IsKanaCheck = False
        Me.txtCustCdL.IsMiddleSpace = False
        Me.txtCustCdL.IsNumericCheck = False
        Me.txtCustCdL.IsSujiCheck = False
        Me.txtCustCdL.IsZenkakuCheck = False
        Me.txtCustCdL.ItemName = ""
        Me.txtCustCdL.LineSpace = 0
        Me.txtCustCdL.Location = New System.Drawing.Point(70, 44)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(66, 18)
        Me.txtCustCdL.TabIndex = 538
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.Tag = ""
        Me.txtCustCdL.TextValue = "XXXXX"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 66
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCust
        '
        Me.lblTitleCust.AutoSize = True
        Me.lblTitleCust.AutoSizeDef = True
        Me.lblTitleCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCust.EnableStatus = False
        Me.lblTitleCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.HeightDef = 13
        Me.lblTitleCust.Location = New System.Drawing.Point(33, 47)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 537
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdM.CountWrappedLine = False
        Me.txtCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdM.HeightDef = 18
        Me.txtCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.HissuLabelVisible = False
        Me.txtCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdM.IsByteCheck = 2
        Me.txtCustCdM.IsCalendarCheck = False
        Me.txtCustCdM.IsDakutenCheck = False
        Me.txtCustCdM.IsEisuCheck = False
        Me.txtCustCdM.IsForbiddenWordsCheck = False
        Me.txtCustCdM.IsFullByteCheck = 0
        Me.txtCustCdM.IsHankakuCheck = False
        Me.txtCustCdM.IsHissuCheck = False
        Me.txtCustCdM.IsKanaCheck = False
        Me.txtCustCdM.IsMiddleSpace = False
        Me.txtCustCdM.IsNumericCheck = False
        Me.txtCustCdM.IsSujiCheck = False
        Me.txtCustCdM.IsZenkakuCheck = False
        Me.txtCustCdM.ItemName = ""
        Me.txtCustCdM.LineSpace = 0
        Me.txtCustCdM.Location = New System.Drawing.Point(120, 44)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(43, 18)
        Me.txtCustCdM.TabIndex = 539
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.Tag = ""
        Me.txtCustCdM.TextValue = "XX"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 43
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleFromTo
        '
        Me.lblTitleFromTo.AutoSize = True
        Me.lblTitleFromTo.AutoSizeDef = True
        Me.lblTitleFromTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFromTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFromTo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFromTo.EnableStatus = False
        Me.lblTitleFromTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFromTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFromTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFromTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFromTo.HeightDef = 13
        Me.lblTitleFromTo.Location = New System.Drawing.Point(284, 116)
        Me.lblTitleFromTo.Name = "lblTitleFromTo"
        Me.lblTitleFromTo.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleFromTo.TabIndex = 544
        Me.lblTitleFromTo.Text = "～"
        Me.lblTitleFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFromTo.TextValue = "～"
        Me.lblTitleFromTo.WidthDef = 21
        '
        'imdSearchDateTo
        '
        Me.imdSearchDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSearchDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSearchDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSearchDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField6.ShowLeadingZero = True
        DateLiteralDisplayField11.Text = "/"
        DateMonthDisplayField6.ShowLeadingZero = True
        DateLiteralDisplayField12.Text = "/"
        DateDayDisplayField6.ShowLeadingZero = True
        Me.imdSearchDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField11, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField12, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSearchDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSearchDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSearchDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSearchDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSearchDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField6, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField6, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField6, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSearchDateTo.HeightDef = 18
        Me.imdSearchDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSearchDateTo.HissuLabelVisible = False
        Me.imdSearchDateTo.Holiday = True
        Me.imdSearchDateTo.IsAfterDateCheck = False
        Me.imdSearchDateTo.IsBeforeDateCheck = False
        Me.imdSearchDateTo.IsHissuCheck = False
        Me.imdSearchDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdSearchDateTo.ItemName = ""
        Me.imdSearchDateTo.Location = New System.Drawing.Point(311, 113)
        Me.imdSearchDateTo.Name = "imdSearchDateTo"
        Me.imdSearchDateTo.Number = CType(0, Long)
        Me.imdSearchDateTo.ReadOnly = False
        Me.imdSearchDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdSearchDateTo.TabIndex = 543
        Me.imdSearchDateTo.TabStopSetting = True
        Me.imdSearchDateTo.TextValue = ""
        Me.imdSearchDateTo.Value = New Date(CType(0, Long))
        Me.imdSearchDateTo.WidthDef = 118
        '
        'imdSearchDateFrom
        '
        Me.imdSearchDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSearchDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSearchDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSearchDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField5.ShowLeadingZero = True
        DateLiteralDisplayField9.Text = "/"
        DateMonthDisplayField5.ShowLeadingZero = True
        DateLiteralDisplayField10.Text = "/"
        DateDayDisplayField5.ShowLeadingZero = True
        Me.imdSearchDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField9, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField10, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSearchDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSearchDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSearchDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSearchDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSearchDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField5, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField5, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField5, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSearchDateFrom.HeightDef = 18
        Me.imdSearchDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSearchDateFrom.HissuLabelVisible = False
        Me.imdSearchDateFrom.Holiday = True
        Me.imdSearchDateFrom.IsAfterDateCheck = False
        Me.imdSearchDateFrom.IsBeforeDateCheck = False
        Me.imdSearchDateFrom.IsHissuCheck = False
        Me.imdSearchDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdSearchDateFrom.ItemName = ""
        Me.imdSearchDateFrom.Location = New System.Drawing.Point(171, 113)
        Me.imdSearchDateFrom.Name = "imdSearchDateFrom"
        Me.imdSearchDateFrom.Number = CType(0, Long)
        Me.imdSearchDateFrom.ReadOnly = False
        Me.imdSearchDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdSearchDateFrom.TabIndex = 541
        Me.imdSearchDateFrom.TabStopSetting = True
        Me.imdSearchDateFrom.TextValue = ""
        Me.imdSearchDateFrom.Value = New Date(CType(0, Long))
        Me.imdSearchDateFrom.WidthDef = 118
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSearch.Controls.Add(Me.btnJikkou)
        Me.pnlSearch.Controls.Add(Me.txtUserNm)
        Me.pnlSearch.Controls.Add(Me.txtUserCd)
        Me.pnlSearch.Controls.Add(Me.lblTitleSagyo)
        Me.pnlSearch.Controls.Add(Me.cmbHoukoku)
        Me.pnlSearch.Controls.Add(Me.cmbSearchDate)
        Me.pnlSearch.Controls.Add(Me.txtCustNm)
        Me.pnlSearch.Controls.Add(Me.cmbEigyo)
        Me.pnlSearch.Controls.Add(Me.txtCustCdM)
        Me.pnlSearch.Controls.Add(Me.txtCustCdL)
        Me.pnlSearch.Controls.Add(Me.lblTitleCust)
        Me.pnlSearch.Controls.Add(Me.lblTitleFromTo)
        Me.pnlSearch.Controls.Add(Me.lblTitleEigyo)
        Me.pnlSearch.Controls.Add(Me.imdSearchDateFrom)
        Me.pnlSearch.Controls.Add(Me.imdSearchDateTo)
        Me.pnlSearch.EnableStatus = False
        Me.pnlSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.HeightDef = 139
        Me.pnlSearch.Location = New System.Drawing.Point(12, 21)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(589, 139)
        Me.pnlSearch.TabIndex = 546
        Me.pnlSearch.TabStop = False
        Me.pnlSearch.Text = "検索・実績報告条件"
        Me.pnlSearch.TextValue = "検索・実績報告条件"
        Me.pnlSearch.WidthDef = 589
        '
        'txtUserNm
        '
        Me.txtUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUserNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUserNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUserNm.CountWrappedLine = False
        Me.txtUserNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUserNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUserNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUserNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUserNm.HeightDef = 18
        Me.txtUserNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUserNm.HissuLabelVisible = False
        Me.txtUserNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtUserNm.IsByteCheck = 0
        Me.txtUserNm.IsCalendarCheck = False
        Me.txtUserNm.IsDakutenCheck = False
        Me.txtUserNm.IsEisuCheck = False
        Me.txtUserNm.IsForbiddenWordsCheck = False
        Me.txtUserNm.IsFullByteCheck = 0
        Me.txtUserNm.IsHankakuCheck = False
        Me.txtUserNm.IsHissuCheck = False
        Me.txtUserNm.IsKanaCheck = False
        Me.txtUserNm.IsMiddleSpace = False
        Me.txtUserNm.IsNumericCheck = False
        Me.txtUserNm.IsSujiCheck = False
        Me.txtUserNm.IsZenkakuCheck = False
        Me.txtUserNm.ItemName = ""
        Me.txtUserNm.LineSpace = 0
        Me.txtUserNm.Location = New System.Drawing.Point(120, 67)
        Me.txtUserNm.MaxLength = 0
        Me.txtUserNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUserNm.MaxLineCount = 30
        Me.txtUserNm.Multiline = False
        Me.txtUserNm.Name = "txtUserNm"
        Me.txtUserNm.ReadOnly = True
        Me.txtUserNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUserNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUserNm.Size = New System.Drawing.Size(251, 18)
        Me.txtUserNm.TabIndex = 557
        Me.txtUserNm.TabStop = False
        Me.txtUserNm.TabStopSetting = False
        Me.txtUserNm.TextValue = ""
        Me.txtUserNm.UseSystemPasswordChar = False
        Me.txtUserNm.WidthDef = 251
        Me.txtUserNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUserCd
        '
        Me.txtUserCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUserCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUserCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUserCd.CountWrappedLine = False
        Me.txtUserCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUserCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUserCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUserCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUserCd.HeightDef = 18
        Me.txtUserCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUserCd.HissuLabelVisible = False
        Me.txtUserCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUserCd.IsByteCheck = 5
        Me.txtUserCd.IsCalendarCheck = False
        Me.txtUserCd.IsDakutenCheck = False
        Me.txtUserCd.IsEisuCheck = False
        Me.txtUserCd.IsForbiddenWordsCheck = False
        Me.txtUserCd.IsFullByteCheck = 0
        Me.txtUserCd.IsHankakuCheck = False
        Me.txtUserCd.IsHissuCheck = False
        Me.txtUserCd.IsKanaCheck = False
        Me.txtUserCd.IsMiddleSpace = False
        Me.txtUserCd.IsNumericCheck = False
        Me.txtUserCd.IsSujiCheck = False
        Me.txtUserCd.IsZenkakuCheck = False
        Me.txtUserCd.ItemName = ""
        Me.txtUserCd.LineSpace = 0
        Me.txtUserCd.Location = New System.Drawing.Point(70, 67)
        Me.txtUserCd.MaxLength = 5
        Me.txtUserCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUserCd.MaxLineCount = 0
        Me.txtUserCd.Multiline = False
        Me.txtUserCd.Name = "txtUserCd"
        Me.txtUserCd.ReadOnly = False
        Me.txtUserCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUserCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUserCd.Size = New System.Drawing.Size(66, 18)
        Me.txtUserCd.TabIndex = 556
        Me.txtUserCd.TabStopSetting = True
        Me.txtUserCd.Tag = ""
        Me.txtUserCd.TextValue = "XXXXX"
        Me.txtUserCd.UseSystemPasswordChar = False
        Me.txtUserCd.WidthDef = 66
        Me.txtUserCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo
        '
        Me.lblTitleSagyo.AutoSize = True
        Me.lblTitleSagyo.AutoSizeDef = True
        Me.lblTitleSagyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo.EnableStatus = False
        Me.lblTitleSagyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo.HeightDef = 13
        Me.lblTitleSagyo.Location = New System.Drawing.Point(19, 69)
        Me.lblTitleSagyo.Name = "lblTitleSagyo"
        Me.lblTitleSagyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo.TabIndex = 555
        Me.lblTitleSagyo.Text = "作業者"
        Me.lblTitleSagyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo.TextValue = "作業者"
        Me.lblTitleSagyo.WidthDef = 49
        '
        'cmbHoukoku
        '
        Me.cmbHoukoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHoukoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHoukoku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbHoukoku.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbHoukoku.DataCode = "B025"
        Me.cmbHoukoku.DataSource = Nothing
        Me.cmbHoukoku.DisplayMember = Nothing
        Me.cmbHoukoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHoukoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHoukoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHoukoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHoukoku.HeightDef = 18
        Me.cmbHoukoku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHoukoku.HissuLabelVisible = False
        Me.cmbHoukoku.InsertWildCard = True
        Me.cmbHoukoku.IsForbiddenWordsCheck = False
        Me.cmbHoukoku.IsHissuCheck = False
        Me.cmbHoukoku.ItemName = ""
        Me.cmbHoukoku.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbHoukoku.Location = New System.Drawing.Point(14, 89)
        Me.cmbHoukoku.Name = "cmbHoukoku"
        Me.cmbHoukoku.ReadOnly = False
        Me.cmbHoukoku.SelectedIndex = -1
        Me.cmbHoukoku.SelectedItem = Nothing
        Me.cmbHoukoku.SelectedText = ""
        Me.cmbHoukoku.SelectedValue = ""
        Me.cmbHoukoku.Size = New System.Drawing.Size(415, 18)
        Me.cmbHoukoku.TabIndex = 553
        Me.cmbHoukoku.TabStopSetting = True
        Me.cmbHoukoku.TextValue = ""
        Me.cmbHoukoku.Value1 = Nothing
        Me.cmbHoukoku.Value2 = Nothing
        Me.cmbHoukoku.Value3 = Nothing
        Me.cmbHoukoku.ValueMember = Nothing
        Me.cmbHoukoku.WidthDef = 415
        '
        'btnJikkou
        '
        Me.btnJikkou.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnJikkou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnJikkou.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnJikkou.EnableStatus = True
        Me.btnJikkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJikkou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJikkou.HeightDef = 24
        Me.btnJikkou.Location = New System.Drawing.Point(422, 86)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(100, 24)
        Me.btnJikkou.TabIndex = 554
        Me.btnJikkou.TabStopSetting = True
        Me.btnJikkou.Text = "実行"
        Me.btnJikkou.TextValue = "実行"
        Me.btnJikkou.UseVisualStyleBackColor = True
        Me.btnJikkou.WidthDef = 100
        '
        'cmbSearchDate
        '
        Me.cmbSearchDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSearchDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSearchDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSearchDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSearchDate.DataCode = "B024"
        Me.cmbSearchDate.DataSource = Nothing
        Me.cmbSearchDate.DisplayMember = Nothing
        Me.cmbSearchDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSearchDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSearchDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSearchDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSearchDate.HeightDef = 18
        Me.cmbSearchDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSearchDate.HissuLabelVisible = False
        Me.cmbSearchDate.InsertWildCard = True
        Me.cmbSearchDate.IsForbiddenWordsCheck = False
        Me.cmbSearchDate.IsHissuCheck = False
        Me.cmbSearchDate.ItemName = ""
        Me.cmbSearchDate.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSearchDate.Location = New System.Drawing.Point(14, 113)
        Me.cmbSearchDate.Name = "cmbSearchDate"
        Me.cmbSearchDate.ReadOnly = False
        Me.cmbSearchDate.SelectedIndex = -1
        Me.cmbSearchDate.SelectedItem = Nothing
        Me.cmbSearchDate.SelectedText = ""
        Me.cmbSearchDate.SelectedValue = ""
        Me.cmbSearchDate.Size = New System.Drawing.Size(150, 18)
        Me.cmbSearchDate.TabIndex = 552
        Me.cmbSearchDate.TabStopSetting = True
        Me.cmbSearchDate.TextValue = ""
        Me.cmbSearchDate.Value1 = Nothing
        Me.cmbSearchDate.Value2 = Nothing
        Me.cmbSearchDate.Value3 = Nothing
        Me.cmbSearchDate.ValueMember = Nothing
        Me.cmbSearchDate.WidthDef = 150
        '
        'txtCustNm
        '
        Me.txtCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustNm.CountWrappedLine = False
        Me.txtCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustNm.HeightDef = 18
        Me.txtCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.HissuLabelVisible = True
        Me.txtCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustNm.IsByteCheck = 0
        Me.txtCustNm.IsCalendarCheck = False
        Me.txtCustNm.IsDakutenCheck = False
        Me.txtCustNm.IsEisuCheck = False
        Me.txtCustNm.IsForbiddenWordsCheck = False
        Me.txtCustNm.IsFullByteCheck = 0
        Me.txtCustNm.IsHankakuCheck = False
        Me.txtCustNm.IsHissuCheck = True
        Me.txtCustNm.IsKanaCheck = False
        Me.txtCustNm.IsMiddleSpace = False
        Me.txtCustNm.IsNumericCheck = False
        Me.txtCustNm.IsSujiCheck = False
        Me.txtCustNm.IsZenkakuCheck = False
        Me.txtCustNm.ItemName = ""
        Me.txtCustNm.LineSpace = 0
        Me.txtCustNm.Location = New System.Drawing.Point(147, 44)
        Me.txtCustNm.MaxLength = 0
        Me.txtCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustNm.MaxLineCount = 30
        Me.txtCustNm.Multiline = False
        Me.txtCustNm.Name = "txtCustNm"
        Me.txtCustNm.ReadOnly = True
        Me.txtCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustNm.Size = New System.Drawing.Size(398, 18)
        Me.txtCustNm.TabIndex = 551
        Me.txtCustNm.TabStop = False
        Me.txtCustNm.TabStopSetting = False
        Me.txtCustNm.TextValue = ""
        Me.txtCustNm.UseSystemPasswordChar = False
        Me.txtCustNm.WidthDef = 398
        Me.txtCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.cmbEigyo.HissuLabelVisible = False
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = False
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(71, 22)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 549
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'pnlIkkatu
        '
        Me.pnlIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlIkkatu.Controls.Add(Me.btnIkkatu)
        Me.pnlIkkatu.Controls.Add(Me.lblIkkatuCustNm)
        Me.pnlIkkatu.Controls.Add(Me.cmbIkkatuKbn)
        Me.pnlIkkatu.Controls.Add(Me.txtIkkatuCustM)
        Me.pnlIkkatu.Controls.Add(Me.txtIkkatuCustL)
        Me.pnlIkkatu.Controls.Add(Me.imdIkkatuDate)
        Me.pnlIkkatu.EnableStatus = False
        Me.pnlIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlIkkatu.HeightDef = 57
        Me.pnlIkkatu.Location = New System.Drawing.Point(607, 103)
        Me.pnlIkkatu.Name = "pnlIkkatu"
        Me.pnlIkkatu.Size = New System.Drawing.Size(655, 57)
        Me.pnlIkkatu.TabIndex = 555
        Me.pnlIkkatu.TabStop = False
        Me.pnlIkkatu.Text = "一括変更条件"
        Me.pnlIkkatu.TextValue = "一括変更条件"
        Me.pnlIkkatu.WidthDef = 655
        '
        'lblIkkatuCustNm
        '
        Me.lblIkkatuCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIkkatuCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIkkatuCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIkkatuCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIkkatuCustNm.CountWrappedLine = False
        Me.lblIkkatuCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblIkkatuCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIkkatuCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIkkatuCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIkkatuCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIkkatuCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblIkkatuCustNm.HeightDef = 18
        Me.lblIkkatuCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIkkatuCustNm.HissuLabelVisible = False
        Me.lblIkkatuCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblIkkatuCustNm.IsByteCheck = 0
        Me.lblIkkatuCustNm.IsCalendarCheck = False
        Me.lblIkkatuCustNm.IsDakutenCheck = False
        Me.lblIkkatuCustNm.IsEisuCheck = False
        Me.lblIkkatuCustNm.IsForbiddenWordsCheck = False
        Me.lblIkkatuCustNm.IsFullByteCheck = 0
        Me.lblIkkatuCustNm.IsHankakuCheck = False
        Me.lblIkkatuCustNm.IsHissuCheck = False
        Me.lblIkkatuCustNm.IsKanaCheck = False
        Me.lblIkkatuCustNm.IsMiddleSpace = False
        Me.lblIkkatuCustNm.IsNumericCheck = False
        Me.lblIkkatuCustNm.IsSujiCheck = False
        Me.lblIkkatuCustNm.IsZenkakuCheck = False
        Me.lblIkkatuCustNm.ItemName = ""
        Me.lblIkkatuCustNm.LineSpace = 0
        Me.lblIkkatuCustNm.Location = New System.Drawing.Point(245, 26)
        Me.lblIkkatuCustNm.MaxLength = 0
        Me.lblIkkatuCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblIkkatuCustNm.MaxLineCount = 30
        Me.lblIkkatuCustNm.Multiline = False
        Me.lblIkkatuCustNm.Name = "lblIkkatuCustNm"
        Me.lblIkkatuCustNm.ReadOnly = True
        Me.lblIkkatuCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblIkkatuCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblIkkatuCustNm.Size = New System.Drawing.Size(299, 18)
        Me.lblIkkatuCustNm.TabIndex = 551
        Me.lblIkkatuCustNm.TabStop = False
        Me.lblIkkatuCustNm.TabStopSetting = False
        Me.lblIkkatuCustNm.TextValue = ""
        Me.lblIkkatuCustNm.UseSystemPasswordChar = False
        Me.lblIkkatuCustNm.WidthDef = 299
        Me.lblIkkatuCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnIkkatu
        '
        Me.btnIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnIkkatu.EnableStatus = True
        Me.btnIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatu.HeightDef = 24
        Me.btnIkkatu.Location = New System.Drawing.Point(536, 23)
        Me.btnIkkatu.Name = "btnIkkatu"
        Me.btnIkkatu.Size = New System.Drawing.Size(100, 24)
        Me.btnIkkatu.TabIndex = 554
        Me.btnIkkatu.TabStopSetting = True
        Me.btnIkkatu.Text = "一括変更"
        Me.btnIkkatu.TextValue = "一括変更"
        Me.btnIkkatu.UseVisualStyleBackColor = True
        Me.btnIkkatu.WidthDef = 100
        '
        'cmbIkkatuKbn
        '
        Me.cmbIkkatuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIkkatuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIkkatuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbIkkatuKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbIkkatuKbn.DataCode = "B026"
        Me.cmbIkkatuKbn.DataSource = Nothing
        Me.cmbIkkatuKbn.DisplayMember = Nothing
        Me.cmbIkkatuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIkkatuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIkkatuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIkkatuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIkkatuKbn.HeightDef = 18
        Me.cmbIkkatuKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIkkatuKbn.HissuLabelVisible = False
        Me.cmbIkkatuKbn.InsertWildCard = True
        Me.cmbIkkatuKbn.IsForbiddenWordsCheck = False
        Me.cmbIkkatuKbn.IsHissuCheck = False
        Me.cmbIkkatuKbn.ItemName = ""
        Me.cmbIkkatuKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbIkkatuKbn.Location = New System.Drawing.Point(12, 26)
        Me.cmbIkkatuKbn.Name = "cmbIkkatuKbn"
        Me.cmbIkkatuKbn.ReadOnly = False
        Me.cmbIkkatuKbn.SelectedIndex = -1
        Me.cmbIkkatuKbn.SelectedItem = Nothing
        Me.cmbIkkatuKbn.SelectedText = ""
        Me.cmbIkkatuKbn.SelectedValue = ""
        Me.cmbIkkatuKbn.Size = New System.Drawing.Size(150, 18)
        Me.cmbIkkatuKbn.TabIndex = 552
        Me.cmbIkkatuKbn.TabStopSetting = True
        Me.cmbIkkatuKbn.TextValue = ""
        Me.cmbIkkatuKbn.Value1 = Nothing
        Me.cmbIkkatuKbn.Value2 = Nothing
        Me.cmbIkkatuKbn.Value3 = Nothing
        Me.cmbIkkatuKbn.ValueMember = Nothing
        Me.cmbIkkatuKbn.WidthDef = 150
        '
        'txtIkkatuCustM
        '
        Me.txtIkkatuCustM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIkkatuCustM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIkkatuCustM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIkkatuCustM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtIkkatuCustM.CountWrappedLine = False
        Me.txtIkkatuCustM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtIkkatuCustM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIkkatuCustM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIkkatuCustM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIkkatuCustM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIkkatuCustM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtIkkatuCustM.HeightDef = 18
        Me.txtIkkatuCustM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtIkkatuCustM.HissuLabelVisible = False
        Me.txtIkkatuCustM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtIkkatuCustM.IsByteCheck = 2
        Me.txtIkkatuCustM.IsCalendarCheck = False
        Me.txtIkkatuCustM.IsDakutenCheck = False
        Me.txtIkkatuCustM.IsEisuCheck = False
        Me.txtIkkatuCustM.IsForbiddenWordsCheck = False
        Me.txtIkkatuCustM.IsFullByteCheck = 0
        Me.txtIkkatuCustM.IsHankakuCheck = False
        Me.txtIkkatuCustM.IsHissuCheck = False
        Me.txtIkkatuCustM.IsKanaCheck = False
        Me.txtIkkatuCustM.IsMiddleSpace = False
        Me.txtIkkatuCustM.IsNumericCheck = False
        Me.txtIkkatuCustM.IsSujiCheck = False
        Me.txtIkkatuCustM.IsZenkakuCheck = False
        Me.txtIkkatuCustM.ItemName = ""
        Me.txtIkkatuCustM.LineSpace = 0
        Me.txtIkkatuCustM.Location = New System.Drawing.Point(218, 26)
        Me.txtIkkatuCustM.MaxLength = 2
        Me.txtIkkatuCustM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtIkkatuCustM.MaxLineCount = 0
        Me.txtIkkatuCustM.Multiline = False
        Me.txtIkkatuCustM.Name = "txtIkkatuCustM"
        Me.txtIkkatuCustM.ReadOnly = False
        Me.txtIkkatuCustM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtIkkatuCustM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtIkkatuCustM.Size = New System.Drawing.Size(43, 18)
        Me.txtIkkatuCustM.TabIndex = 539
        Me.txtIkkatuCustM.TabStopSetting = True
        Me.txtIkkatuCustM.Tag = ""
        Me.txtIkkatuCustM.TextValue = "XX"
        Me.txtIkkatuCustM.UseSystemPasswordChar = False
        Me.txtIkkatuCustM.WidthDef = 43
        Me.txtIkkatuCustM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtIkkatuCustL
        '
        Me.txtIkkatuCustL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIkkatuCustL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIkkatuCustL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIkkatuCustL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtIkkatuCustL.CountWrappedLine = False
        Me.txtIkkatuCustL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtIkkatuCustL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIkkatuCustL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIkkatuCustL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIkkatuCustL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIkkatuCustL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtIkkatuCustL.HeightDef = 18
        Me.txtIkkatuCustL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtIkkatuCustL.HissuLabelVisible = False
        Me.txtIkkatuCustL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtIkkatuCustL.IsByteCheck = 5
        Me.txtIkkatuCustL.IsCalendarCheck = False
        Me.txtIkkatuCustL.IsDakutenCheck = False
        Me.txtIkkatuCustL.IsEisuCheck = False
        Me.txtIkkatuCustL.IsForbiddenWordsCheck = False
        Me.txtIkkatuCustL.IsFullByteCheck = 0
        Me.txtIkkatuCustL.IsHankakuCheck = False
        Me.txtIkkatuCustL.IsHissuCheck = False
        Me.txtIkkatuCustL.IsKanaCheck = False
        Me.txtIkkatuCustL.IsMiddleSpace = False
        Me.txtIkkatuCustL.IsNumericCheck = False
        Me.txtIkkatuCustL.IsSujiCheck = False
        Me.txtIkkatuCustL.IsZenkakuCheck = False
        Me.txtIkkatuCustL.ItemName = ""
        Me.txtIkkatuCustL.LineSpace = 0
        Me.txtIkkatuCustL.Location = New System.Drawing.Point(168, 26)
        Me.txtIkkatuCustL.MaxLength = 5
        Me.txtIkkatuCustL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtIkkatuCustL.MaxLineCount = 0
        Me.txtIkkatuCustL.Multiline = False
        Me.txtIkkatuCustL.Name = "txtIkkatuCustL"
        Me.txtIkkatuCustL.ReadOnly = False
        Me.txtIkkatuCustL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtIkkatuCustL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtIkkatuCustL.Size = New System.Drawing.Size(66, 18)
        Me.txtIkkatuCustL.TabIndex = 538
        Me.txtIkkatuCustL.TabStopSetting = True
        Me.txtIkkatuCustL.Tag = ""
        Me.txtIkkatuCustL.TextValue = "XXXXX"
        Me.txtIkkatuCustL.UseSystemPasswordChar = False
        Me.txtIkkatuCustL.WidthDef = 66
        Me.txtIkkatuCustL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdIkkatuDate
        '
        Me.imdIkkatuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdIkkatuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdIkkatuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdIkkatuDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField7.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = "/"
        DateDayDisplayField4.ShowLeadingZero = True
        Me.imdIkkatuDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField7, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField8, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdIkkatuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdIkkatuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdIkkatuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdIkkatuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdIkkatuDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField4, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdIkkatuDate.HeightDef = 18
        Me.imdIkkatuDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdIkkatuDate.HissuLabelVisible = False
        Me.imdIkkatuDate.Holiday = True
        Me.imdIkkatuDate.IsAfterDateCheck = False
        Me.imdIkkatuDate.IsBeforeDateCheck = False
        Me.imdIkkatuDate.IsHissuCheck = False
        Me.imdIkkatuDate.IsMinDateCheck = "1900/01/01"
        Me.imdIkkatuDate.ItemName = ""
        Me.imdIkkatuDate.Location = New System.Drawing.Point(168, 26)
        Me.imdIkkatuDate.Name = "imdIkkatuDate"
        Me.imdIkkatuDate.Number = CType(0, Long)
        Me.imdIkkatuDate.ReadOnly = False
        Me.imdIkkatuDate.Size = New System.Drawing.Size(118, 18)
        Me.imdIkkatuDate.TabIndex = 541
        Me.imdIkkatuDate.TabStopSetting = True
        Me.imdIkkatuDate.TextValue = ""
        Me.imdIkkatuDate.Value = New Date(CType(0, Long))
        Me.imdIkkatuDate.WidthDef = 118
        '
        'LMI410F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.FocusedControlName = "sprGoodsDetail"
        Me.Name = "LMI410F"
        Me.Text = "【LMI410】ビックケミー取込データ確認／報告"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprIdoList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlIkkatu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprIdoList As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleFromTo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSearchDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdSearchDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents pnlSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents txtCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbHoukoku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbSearchDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents pnlIkkatu As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnIkkatu As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbIkkatuKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblIkkatuCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtIkkatuCustM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtIkkatuCustL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdIkkatuDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents btnJikkou As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents txtUserNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUserCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
