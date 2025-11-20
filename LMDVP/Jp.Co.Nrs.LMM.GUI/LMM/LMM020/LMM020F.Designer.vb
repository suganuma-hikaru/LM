<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM020F
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
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.chkRec = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCoaLink = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCoaName = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnRowAdd_M = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.TitlelblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblGoodsKey = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDest = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSvFileNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSvPath = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnClear = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblTitleInkaDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdInkaDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.chkInkaDateVersFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblInkaDateHissu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblInkaDateVersFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblInkaDateEditableFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.lblInkaDateEditableFlg)
        Me.pnlViewAria.Controls.Add(Me.lblInkaDateVersFlg)
        Me.pnlViewAria.Controls.Add(Me.lblInkaDateHissu)
        Me.pnlViewAria.Controls.Add(Me.chkInkaDateVersFlg)
        Me.pnlViewAria.Controls.Add(Me.imdInkaDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleInkaDate)
        Me.pnlViewAria.Controls.Add(Me.btnClear)
        Me.pnlViewAria.Controls.Add(Me.lblSvFileNm)
        Me.pnlViewAria.Controls.Add(Me.lblSvPath)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblDestNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleDest)
        Me.pnlViewAria.Controls.Add(Me.txtDestCd)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsKey)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNm)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleGoods)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmM)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmL)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCust)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.TitlelblEigyo)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd_M)
        Me.pnlViewAria.Controls.Add(Me.lblCoaName)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.lblCoaLink)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.txtLotNo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.chkRec)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
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
        Me.sprDetail.HeightDef = 399
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 43)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1245, 399)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.WidthDef = 1245
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
        'chkRec
        '
        Me.chkRec.AutoSize = True
        Me.chkRec.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkRec.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkRec.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkRec.EnableStatus = True
        Me.chkRec.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkRec.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkRec.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkRec.HeightDef = 17
        Me.chkRec.Location = New System.Drawing.Point(17, 16)
        Me.chkRec.Name = "chkRec"
        Me.chkRec.Size = New System.Drawing.Size(222, 17)
        Me.chkRec.TabIndex = 209
        Me.chkRec.TabStopSetting = True
        Me.chkRec.Text = "分析票が未設定のレコードのみ"
        Me.chkRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkRec.TextValue = "分析票が未設定のレコードのみ"
        Me.chkRec.UseVisualStyleBackColor = True
        Me.chkRec.WidthDef = 222
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSizeDef = False
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 18
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1064, 495)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel7.TabIndex = 0
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 49
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1121, 464)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 218
        Me.lblSituation.TabStop = False
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSizeDef = False
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 18
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1064, 518)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel8.TabIndex = 221
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 49
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSizeDef = False
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 18
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1064, 540)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel9.TabIndex = 223
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 49
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSizeDef = False
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 18
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1064, 562)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel10.TabIndex = 225
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 49
        '
        'txtLotNo
        '
        Me.txtLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLotNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLotNo.CountWrappedLine = False
        Me.txtLotNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLotNo.HeightDef = 18
        Me.txtLotNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLotNo.HissuLabelVisible = True
        Me.txtLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLotNo.IsByteCheck = 40
        Me.txtLotNo.IsCalendarCheck = False
        Me.txtLotNo.IsDakutenCheck = False
        Me.txtLotNo.IsEisuCheck = False
        Me.txtLotNo.IsForbiddenWordsCheck = False
        Me.txtLotNo.IsFullByteCheck = 0
        Me.txtLotNo.IsHankakuCheck = False
        Me.txtLotNo.IsHissuCheck = True
        Me.txtLotNo.IsKanaCheck = False
        Me.txtLotNo.IsMiddleSpace = False
        Me.txtLotNo.IsNumericCheck = False
        Me.txtLotNo.IsSujiCheck = False
        Me.txtLotNo.IsZenkakuCheck = False
        Me.txtLotNo.ItemName = ""
        Me.txtLotNo.LineSpace = 0
        Me.txtLotNo.Location = New System.Drawing.Point(168, 548)
        Me.txtLotNo.MaxLength = 40
        Me.txtLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLotNo.MaxLineCount = 0
        Me.txtLotNo.Multiline = False
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ReadOnly = False
        Me.txtLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLotNo.Size = New System.Drawing.Size(312, 18)
        Me.txtLotNo.TabIndex = 238
        Me.txtLotNo.TabStopSetting = True
        Me.txtLotNo.Tag = ""
        Me.txtLotNo.TextValue = "X10XX10XX10XX10X"
        Me.txtLotNo.UseSystemPasswordChar = False
        Me.txtLotNo.WidthDef = 312
        Me.txtLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel4.Location = New System.Drawing.Point(103, 549)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel4.TabIndex = 239
        Me.LmTitleLabel4.Text = "ロット№"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "ロット№"
        Me.LmTitleLabel4.WidthDef = 63
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
        Me.LmTitleLabel6.Location = New System.Drawing.Point(19, 612)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(147, 13)
        Me.LmTitleLabel6.TabIndex = 241
        Me.LmTitleLabel6.Text = "分析票ファイルパス名"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "分析票ファイルパス名"
        Me.LmTitleLabel6.WidthDef = 147
        '
        'lblCoaLink
        '
        Me.lblCoaLink.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaLink.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaLink.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCoaLink.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCoaLink.CountWrappedLine = False
        Me.lblCoaLink.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCoaLink.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaLink.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaLink.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaLink.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaLink.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCoaLink.HeightDef = 18
        Me.lblCoaLink.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaLink.HissuLabelVisible = False
        Me.lblCoaLink.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCoaLink.IsByteCheck = 100
        Me.lblCoaLink.IsCalendarCheck = False
        Me.lblCoaLink.IsDakutenCheck = False
        Me.lblCoaLink.IsEisuCheck = False
        Me.lblCoaLink.IsForbiddenWordsCheck = False
        Me.lblCoaLink.IsFullByteCheck = 0
        Me.lblCoaLink.IsHankakuCheck = False
        Me.lblCoaLink.IsHissuCheck = False
        Me.lblCoaLink.IsKanaCheck = False
        Me.lblCoaLink.IsMiddleSpace = False
        Me.lblCoaLink.IsNumericCheck = False
        Me.lblCoaLink.IsSujiCheck = False
        Me.lblCoaLink.IsZenkakuCheck = False
        Me.lblCoaLink.ItemName = ""
        Me.lblCoaLink.LineSpace = 0
        Me.lblCoaLink.Location = New System.Drawing.Point(168, 611)
        Me.lblCoaLink.MaxLength = 100
        Me.lblCoaLink.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCoaLink.MaxLineCount = 0
        Me.lblCoaLink.Multiline = False
        Me.lblCoaLink.Name = "lblCoaLink"
        Me.lblCoaLink.ReadOnly = True
        Me.lblCoaLink.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCoaLink.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCoaLink.Size = New System.Drawing.Size(755, 18)
        Me.lblCoaLink.TabIndex = 242
        Me.lblCoaLink.TabStop = False
        Me.lblCoaLink.TabStopSetting = False
        Me.lblCoaLink.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
    "---10---XX---10---X"
        Me.lblCoaLink.UseSystemPasswordChar = False
        Me.lblCoaLink.WidthDef = 755
        Me.lblCoaLink.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCoaName
        '
        Me.lblCoaName.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaName.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaName.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCoaName.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCoaName.CountWrappedLine = False
        Me.lblCoaName.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCoaName.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaName.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaName.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaName.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCoaName.HeightDef = 18
        Me.lblCoaName.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaName.HissuLabelVisible = False
        Me.lblCoaName.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCoaName.IsByteCheck = 100
        Me.lblCoaName.IsCalendarCheck = False
        Me.lblCoaName.IsDakutenCheck = False
        Me.lblCoaName.IsEisuCheck = False
        Me.lblCoaName.IsForbiddenWordsCheck = False
        Me.lblCoaName.IsFullByteCheck = 0
        Me.lblCoaName.IsHankakuCheck = False
        Me.lblCoaName.IsHissuCheck = False
        Me.lblCoaName.IsKanaCheck = False
        Me.lblCoaName.IsMiddleSpace = False
        Me.lblCoaName.IsNumericCheck = False
        Me.lblCoaName.IsSujiCheck = False
        Me.lblCoaName.IsZenkakuCheck = False
        Me.lblCoaName.ItemName = ""
        Me.lblCoaName.LineSpace = 0
        Me.lblCoaName.Location = New System.Drawing.Point(168, 632)
        Me.lblCoaName.MaxLength = 100
        Me.lblCoaName.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCoaName.MaxLineCount = 0
        Me.lblCoaName.Multiline = False
        Me.lblCoaName.Name = "lblCoaName"
        Me.lblCoaName.ReadOnly = True
        Me.lblCoaName.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCoaName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCoaName.Size = New System.Drawing.Size(755, 18)
        Me.lblCoaName.TabIndex = 348
        Me.lblCoaName.TabStop = False
        Me.lblCoaName.TabStopSetting = False
        Me.lblCoaName.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
    "---10---XX---10---X"
        Me.lblCoaName.UseSystemPasswordChar = False
        Me.lblCoaName.WidthDef = 755
        Me.lblCoaName.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(47, 633)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel3.TabIndex = 347
        Me.LmTitleLabel3.Text = "分析票ファイル名"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "分析票ファイル名"
        Me.LmTitleLabel3.WidthDef = 119
        '
        'btnRowAdd_M
        '
        Me.btnRowAdd_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd_M.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowAdd_M.EnableStatus = True
        Me.btnRowAdd_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd_M.HeightDef = 22
        Me.btnRowAdd_M.Location = New System.Drawing.Point(915, 630)
        Me.btnRowAdd_M.Name = "btnRowAdd_M"
        Me.btnRowAdd_M.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd_M.TabIndex = 349
        Me.btnRowAdd_M.TabStopSetting = True
        Me.btnRowAdd_M.Text = "追加"
        Me.btnRowAdd_M.TextValue = "追加"
        Me.btnRowAdd_M.UseVisualStyleBackColor = True
        Me.btnRowAdd_M.WidthDef = 70
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(168, 464)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 603
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'TitlelblEigyo
        '
        Me.TitlelblEigyo.AutoSize = True
        Me.TitlelblEigyo.AutoSizeDef = True
        Me.TitlelblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblEigyo.EnableStatus = False
        Me.TitlelblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.HeightDef = 13
        Me.TitlelblEigyo.Location = New System.Drawing.Point(117, 466)
        Me.TitlelblEigyo.Name = "TitlelblEigyo"
        Me.TitlelblEigyo.Size = New System.Drawing.Size(49, 13)
        Me.TitlelblEigyo.TabIndex = 602
        Me.TitlelblEigyo.Text = "営業所"
        Me.TitlelblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblEigyo.TextValue = "営業所"
        Me.TitlelblEigyo.WidthDef = 49
        '
        'lblCustNmM
        '
        Me.lblCustNmM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmM.CountWrappedLine = False
        Me.lblCustNmM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmM.HeightDef = 18
        Me.lblCustNmM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.HissuLabelVisible = True
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = True
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(249, 506)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(452, 18)
        Me.lblCustNmM.TabIndex = 609
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 452
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNmL
        '
        Me.lblCustNmL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmL.CountWrappedLine = False
        Me.lblCustNmL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmL.HeightDef = 18
        Me.lblCustNmL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.HissuLabelVisible = True
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = True
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(249, 485)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(452, 18)
        Me.lblCustNmL.TabIndex = 608
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 452
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(131, 487)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 607
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
        Me.txtCustCdM.Location = New System.Drawing.Point(213, 506)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(69, 18)
        Me.txtCustCdM.TabIndex = 606
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = "12"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 69
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdL.Location = New System.Drawing.Point(168, 485)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(114, 18)
        Me.txtCustCdL.TabIndex = 605
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "12345"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 114
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsKey
        '
        Me.lblGoodsKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsKey.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsKey.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsKey.CountWrappedLine = False
        Me.lblGoodsKey.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsKey.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsKey.HeightDef = 18
        Me.lblGoodsKey.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsKey.HissuLabelVisible = True
        Me.lblGoodsKey.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.lblGoodsKey.IsByteCheck = 0
        Me.lblGoodsKey.IsCalendarCheck = False
        Me.lblGoodsKey.IsDakutenCheck = False
        Me.lblGoodsKey.IsEisuCheck = False
        Me.lblGoodsKey.IsForbiddenWordsCheck = False
        Me.lblGoodsKey.IsFullByteCheck = 0
        Me.lblGoodsKey.IsHankakuCheck = False
        Me.lblGoodsKey.IsHissuCheck = True
        Me.lblGoodsKey.IsKanaCheck = False
        Me.lblGoodsKey.IsMiddleSpace = False
        Me.lblGoodsKey.IsNumericCheck = False
        Me.lblGoodsKey.IsSujiCheck = False
        Me.lblGoodsKey.IsZenkakuCheck = False
        Me.lblGoodsKey.ItemName = ""
        Me.lblGoodsKey.LineSpace = 0
        Me.lblGoodsKey.Location = New System.Drawing.Point(672, 527)
        Me.lblGoodsKey.MaxLength = 0
        Me.lblGoodsKey.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsKey.MaxLineCount = 0
        Me.lblGoodsKey.Multiline = False
        Me.lblGoodsKey.Name = "lblGoodsKey"
        Me.lblGoodsKey.ReadOnly = True
        Me.lblGoodsKey.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsKey.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsKey.Size = New System.Drawing.Size(167, 18)
        Me.lblGoodsKey.TabIndex = 612
        Me.lblGoodsKey.TabStop = False
        Me.lblGoodsKey.TabStopSetting = False
        Me.lblGoodsKey.TextValue = ""
        Me.lblGoodsKey.UseSystemPasswordChar = False
        Me.lblGoodsKey.WidthDef = 167
        Me.lblGoodsKey.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsNm
        '
        Me.lblGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNm.CountWrappedLine = False
        Me.lblGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNm.HeightDef = 18
        Me.lblGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.HissuLabelVisible = False
        Me.lblGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.lblGoodsNm.IsByteCheck = 0
        Me.lblGoodsNm.IsCalendarCheck = False
        Me.lblGoodsNm.IsDakutenCheck = False
        Me.lblGoodsNm.IsEisuCheck = False
        Me.lblGoodsNm.IsForbiddenWordsCheck = False
        Me.lblGoodsNm.IsFullByteCheck = 0
        Me.lblGoodsNm.IsHankakuCheck = False
        Me.lblGoodsNm.IsHissuCheck = False
        Me.lblGoodsNm.IsKanaCheck = False
        Me.lblGoodsNm.IsMiddleSpace = False
        Me.lblGoodsNm.IsNumericCheck = False
        Me.lblGoodsNm.IsSujiCheck = False
        Me.lblGoodsNm.IsZenkakuCheck = False
        Me.lblGoodsNm.ItemName = ""
        Me.lblGoodsNm.LineSpace = 0
        Me.lblGoodsNm.Location = New System.Drawing.Point(319, 527)
        Me.lblGoodsNm.MaxLength = 0
        Me.lblGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNm.MaxLineCount = 0
        Me.lblGoodsNm.Multiline = False
        Me.lblGoodsNm.Name = "lblGoodsNm"
        Me.lblGoodsNm.ReadOnly = True
        Me.lblGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNm.Size = New System.Drawing.Size(372, 18)
        Me.lblGoodsNm.TabIndex = 613
        Me.lblGoodsNm.TabStop = False
        Me.lblGoodsNm.TabStopSetting = False
        Me.lblGoodsNm.TextValue = ""
        Me.lblGoodsNm.UseSystemPasswordChar = False
        Me.lblGoodsNm.WidthDef = 372
        Me.lblGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCd.CountWrappedLine = False
        Me.txtGoodsCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCd.HeightDef = 18
        Me.txtGoodsCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.HissuLabelVisible = True
        Me.txtGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtGoodsCd.IsByteCheck = 20
        Me.txtGoodsCd.IsCalendarCheck = False
        Me.txtGoodsCd.IsDakutenCheck = False
        Me.txtGoodsCd.IsEisuCheck = False
        Me.txtGoodsCd.IsForbiddenWordsCheck = False
        Me.txtGoodsCd.IsFullByteCheck = 0
        Me.txtGoodsCd.IsHankakuCheck = False
        Me.txtGoodsCd.IsHissuCheck = True
        Me.txtGoodsCd.IsKanaCheck = False
        Me.txtGoodsCd.IsMiddleSpace = False
        Me.txtGoodsCd.IsNumericCheck = False
        Me.txtGoodsCd.IsSujiCheck = False
        Me.txtGoodsCd.IsZenkakuCheck = False
        Me.txtGoodsCd.ItemName = ""
        Me.txtGoodsCd.LineSpace = 0
        Me.txtGoodsCd.Location = New System.Drawing.Point(168, 527)
        Me.txtGoodsCd.MaxLength = 20
        Me.txtGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCd.MaxLineCount = 0
        Me.txtGoodsCd.Multiline = False
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.ReadOnly = False
        Me.txtGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCd.Size = New System.Drawing.Size(167, 18)
        Me.txtGoodsCd.TabIndex = 611
        Me.txtGoodsCd.TabStopSetting = True
        Me.txtGoodsCd.TextValue = "xxxxxxxxxxxxxxx"
        Me.txtGoodsCd.UseSystemPasswordChar = False
        Me.txtGoodsCd.WidthDef = 167
        Me.txtGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoods
        '
        Me.lblTitleGoods.AutoSize = True
        Me.lblTitleGoods.AutoSizeDef = True
        Me.lblTitleGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoods.EnableStatus = False
        Me.lblTitleGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoods.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoods.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoods.HeightDef = 13
        Me.lblTitleGoods.Location = New System.Drawing.Point(131, 528)
        Me.lblTitleGoods.Name = "lblTitleGoods"
        Me.lblTitleGoods.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoods.TabIndex = 610
        Me.lblTitleGoods.Text = "商品"
        Me.lblTitleGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoods.TextValue = "商品"
        Me.lblTitleGoods.WidthDef = 35
        '
        'lblDestNm
        '
        Me.lblDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDestNm.CountWrappedLine = False
        Me.lblDestNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDestNm.HeightDef = 18
        Me.lblDestNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.HissuLabelVisible = True
        Me.lblDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestNm.IsByteCheck = 0
        Me.lblDestNm.IsCalendarCheck = False
        Me.lblDestNm.IsDakutenCheck = False
        Me.lblDestNm.IsEisuCheck = False
        Me.lblDestNm.IsForbiddenWordsCheck = False
        Me.lblDestNm.IsFullByteCheck = 0
        Me.lblDestNm.IsHankakuCheck = False
        Me.lblDestNm.IsHissuCheck = True
        Me.lblDestNm.IsKanaCheck = False
        Me.lblDestNm.IsMiddleSpace = False
        Me.lblDestNm.IsNumericCheck = False
        Me.lblDestNm.IsSujiCheck = False
        Me.lblDestNm.IsZenkakuCheck = False
        Me.lblDestNm.ItemName = ""
        Me.lblDestNm.LineSpace = 0
        Me.lblDestNm.Location = New System.Drawing.Point(285, 569)
        Me.lblDestNm.MaxLength = 0
        Me.lblDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestNm.MaxLineCount = 0
        Me.lblDestNm.Multiline = False
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.ReadOnly = True
        Me.lblDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestNm.Size = New System.Drawing.Size(554, 18)
        Me.lblDestNm.TabIndex = 614
        Me.lblDestNm.TabStop = False
        Me.lblDestNm.TabStopSetting = False
        Me.lblDestNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.lblDestNm.UseSystemPasswordChar = False
        Me.lblDestNm.WidthDef = 554
        Me.lblDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDest
        '
        Me.lblTitleDest.AutoSize = True
        Me.lblTitleDest.AutoSizeDef = True
        Me.lblTitleDest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDest.EnableStatus = False
        Me.lblTitleDest.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.HeightDef = 13
        Me.lblTitleDest.Location = New System.Drawing.Point(131, 570)
        Me.lblTitleDest.Name = "lblTitleDest"
        Me.lblTitleDest.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleDest.TabIndex = 615
        Me.lblTitleDest.Text = "届先"
        Me.lblTitleDest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDest.TextValue = "届先"
        Me.lblTitleDest.WidthDef = 35
        '
        'txtDestCd
        '
        Me.txtDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestCd.CountWrappedLine = False
        Me.txtDestCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestCd.HeightDef = 18
        Me.txtDestCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestCd.HissuLabelVisible = True
        Me.txtDestCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtDestCd.IsByteCheck = 15
        Me.txtDestCd.IsCalendarCheck = False
        Me.txtDestCd.IsDakutenCheck = False
        Me.txtDestCd.IsEisuCheck = False
        Me.txtDestCd.IsForbiddenWordsCheck = False
        Me.txtDestCd.IsFullByteCheck = 0
        Me.txtDestCd.IsHankakuCheck = False
        Me.txtDestCd.IsHissuCheck = True
        Me.txtDestCd.IsKanaCheck = False
        Me.txtDestCd.IsMiddleSpace = False
        Me.txtDestCd.IsNumericCheck = False
        Me.txtDestCd.IsSujiCheck = False
        Me.txtDestCd.IsZenkakuCheck = False
        Me.txtDestCd.ItemName = ""
        Me.txtDestCd.LineSpace = 0
        Me.txtDestCd.Location = New System.Drawing.Point(168, 569)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(133, 18)
        Me.txtDestCd.TabIndex = 616
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = ""
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 133
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysDelFlg
        '
        Me.lblSysDelFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysDelFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysDelFlg.CountWrappedLine = False
        Me.lblSysDelFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysDelFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysDelFlg.HeightDef = 18
        Me.lblSysDelFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.HissuLabelVisible = False
        Me.lblSysDelFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysDelFlg.IsByteCheck = 0
        Me.lblSysDelFlg.IsCalendarCheck = False
        Me.lblSysDelFlg.IsDakutenCheck = False
        Me.lblSysDelFlg.IsEisuCheck = False
        Me.lblSysDelFlg.IsForbiddenWordsCheck = False
        Me.lblSysDelFlg.IsFullByteCheck = 0
        Me.lblSysDelFlg.IsHankakuCheck = False
        Me.lblSysDelFlg.IsHissuCheck = False
        Me.lblSysDelFlg.IsKanaCheck = False
        Me.lblSysDelFlg.IsMiddleSpace = False
        Me.lblSysDelFlg.IsNumericCheck = False
        Me.lblSysDelFlg.IsSujiCheck = False
        Me.lblSysDelFlg.IsZenkakuCheck = False
        Me.lblSysDelFlg.ItemName = ""
        Me.lblSysDelFlg.LineSpace = 0
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1115, 604)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 622
        Me.lblSysDelFlg.TabStop = False
        Me.lblSysDelFlg.TabStopSetting = False
        Me.lblSysDelFlg.TextValue = ""
        Me.lblSysDelFlg.UseSystemPasswordChar = False
        Me.lblSysDelFlg.Visible = False
        Me.lblSysDelFlg.WidthDef = 157
        Me.lblSysDelFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdTime
        '
        Me.lblUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdTime.CountWrappedLine = False
        Me.lblUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdTime.HeightDef = 18
        Me.lblUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.HissuLabelVisible = False
        Me.lblUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdTime.IsByteCheck = 0
        Me.lblUpdTime.IsCalendarCheck = False
        Me.lblUpdTime.IsDakutenCheck = False
        Me.lblUpdTime.IsEisuCheck = False
        Me.lblUpdTime.IsForbiddenWordsCheck = False
        Me.lblUpdTime.IsFullByteCheck = 0
        Me.lblUpdTime.IsHankakuCheck = False
        Me.lblUpdTime.IsHissuCheck = False
        Me.lblUpdTime.IsKanaCheck = False
        Me.lblUpdTime.IsMiddleSpace = False
        Me.lblUpdTime.IsNumericCheck = False
        Me.lblUpdTime.IsSujiCheck = False
        Me.lblUpdTime.IsZenkakuCheck = False
        Me.lblUpdTime.ItemName = ""
        Me.lblUpdTime.LineSpace = 0
        Me.lblUpdTime.Location = New System.Drawing.Point(1115, 583)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 621
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCrtUser
        '
        Me.lblCrtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtUser.CountWrappedLine = False
        Me.lblCrtUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtUser.HeightDef = 18
        Me.lblCrtUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.HissuLabelVisible = False
        Me.lblCrtUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtUser.IsByteCheck = 0
        Me.lblCrtUser.IsCalendarCheck = False
        Me.lblCrtUser.IsDakutenCheck = False
        Me.lblCrtUser.IsEisuCheck = False
        Me.lblCrtUser.IsForbiddenWordsCheck = False
        Me.lblCrtUser.IsFullByteCheck = 0
        Me.lblCrtUser.IsHankakuCheck = False
        Me.lblCrtUser.IsHissuCheck = False
        Me.lblCrtUser.IsKanaCheck = False
        Me.lblCrtUser.IsMiddleSpace = False
        Me.lblCrtUser.IsNumericCheck = False
        Me.lblCrtUser.IsSujiCheck = False
        Me.lblCrtUser.IsZenkakuCheck = False
        Me.lblCrtUser.ItemName = ""
        Me.lblCrtUser.LineSpace = 0
        Me.lblCrtUser.Location = New System.Drawing.Point(1115, 496)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 617
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCrtDate
        '
        Me.lblCrtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtDate.CountWrappedLine = False
        Me.lblCrtDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtDate.HeightDef = 18
        Me.lblCrtDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.HissuLabelVisible = False
        Me.lblCrtDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtDate.IsByteCheck = 0
        Me.lblCrtDate.IsCalendarCheck = False
        Me.lblCrtDate.IsDakutenCheck = False
        Me.lblCrtDate.IsEisuCheck = False
        Me.lblCrtDate.IsForbiddenWordsCheck = False
        Me.lblCrtDate.IsFullByteCheck = 0
        Me.lblCrtDate.IsHankakuCheck = False
        Me.lblCrtDate.IsHissuCheck = False
        Me.lblCrtDate.IsKanaCheck = False
        Me.lblCrtDate.IsMiddleSpace = False
        Me.lblCrtDate.IsNumericCheck = False
        Me.lblCrtDate.IsSujiCheck = False
        Me.lblCrtDate.IsZenkakuCheck = False
        Me.lblCrtDate.ItemName = ""
        Me.lblCrtDate.LineSpace = 0
        Me.lblCrtDate.Location = New System.Drawing.Point(1115, 518)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 618
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdUser
        '
        Me.lblUpdUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdUser.CountWrappedLine = False
        Me.lblUpdUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdUser.HeightDef = 18
        Me.lblUpdUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.HissuLabelVisible = False
        Me.lblUpdUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdUser.IsByteCheck = 0
        Me.lblUpdUser.IsCalendarCheck = False
        Me.lblUpdUser.IsDakutenCheck = False
        Me.lblUpdUser.IsEisuCheck = False
        Me.lblUpdUser.IsForbiddenWordsCheck = False
        Me.lblUpdUser.IsFullByteCheck = 0
        Me.lblUpdUser.IsHankakuCheck = False
        Me.lblUpdUser.IsHissuCheck = False
        Me.lblUpdUser.IsKanaCheck = False
        Me.lblUpdUser.IsMiddleSpace = False
        Me.lblUpdUser.IsNumericCheck = False
        Me.lblUpdUser.IsSujiCheck = False
        Me.lblUpdUser.IsZenkakuCheck = False
        Me.lblUpdUser.ItemName = ""
        Me.lblUpdUser.LineSpace = 0
        Me.lblUpdUser.Location = New System.Drawing.Point(1115, 540)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 619
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdDate
        '
        Me.lblUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdDate.CountWrappedLine = False
        Me.lblUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdDate.HeightDef = 18
        Me.lblUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.HissuLabelVisible = False
        Me.lblUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdDate.IsByteCheck = 0
        Me.lblUpdDate.IsCalendarCheck = False
        Me.lblUpdDate.IsDakutenCheck = False
        Me.lblUpdDate.IsEisuCheck = False
        Me.lblUpdDate.IsForbiddenWordsCheck = False
        Me.lblUpdDate.IsFullByteCheck = 0
        Me.lblUpdDate.IsHankakuCheck = False
        Me.lblUpdDate.IsHissuCheck = False
        Me.lblUpdDate.IsKanaCheck = False
        Me.lblUpdDate.IsMiddleSpace = False
        Me.lblUpdDate.IsNumericCheck = False
        Me.lblUpdDate.IsSujiCheck = False
        Me.lblUpdDate.IsZenkakuCheck = False
        Me.lblUpdDate.ItemName = ""
        Me.lblUpdDate.LineSpace = 0
        Me.lblUpdDate.Location = New System.Drawing.Point(1115, 562)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 620
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSvFileNm
        '
        Me.lblSvFileNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvFileNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvFileNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSvFileNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSvFileNm.CountWrappedLine = False
        Me.lblSvFileNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSvFileNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSvFileNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSvFileNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSvFileNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSvFileNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSvFileNm.HeightDef = 18
        Me.lblSvFileNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvFileNm.HissuLabelVisible = False
        Me.lblSvFileNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSvFileNm.IsByteCheck = 100
        Me.lblSvFileNm.IsCalendarCheck = False
        Me.lblSvFileNm.IsDakutenCheck = False
        Me.lblSvFileNm.IsEisuCheck = False
        Me.lblSvFileNm.IsForbiddenWordsCheck = False
        Me.lblSvFileNm.IsFullByteCheck = 0
        Me.lblSvFileNm.IsHankakuCheck = False
        Me.lblSvFileNm.IsHissuCheck = False
        Me.lblSvFileNm.IsKanaCheck = False
        Me.lblSvFileNm.IsMiddleSpace = False
        Me.lblSvFileNm.IsNumericCheck = False
        Me.lblSvFileNm.IsSujiCheck = False
        Me.lblSvFileNm.IsZenkakuCheck = False
        Me.lblSvFileNm.ItemName = ""
        Me.lblSvFileNm.LineSpace = 0
        Me.lblSvFileNm.Location = New System.Drawing.Point(168, 674)
        Me.lblSvFileNm.MaxLength = 100
        Me.lblSvFileNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSvFileNm.MaxLineCount = 0
        Me.lblSvFileNm.Multiline = False
        Me.lblSvFileNm.Name = "lblSvFileNm"
        Me.lblSvFileNm.ReadOnly = True
        Me.lblSvFileNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSvFileNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSvFileNm.Size = New System.Drawing.Size(755, 18)
        Me.lblSvFileNm.TabIndex = 624
        Me.lblSvFileNm.TabStop = False
        Me.lblSvFileNm.TabStopSetting = False
        Me.lblSvFileNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
    "---10---XX---10---X"
        Me.lblSvFileNm.UseSystemPasswordChar = False
        Me.lblSvFileNm.Visible = False
        Me.lblSvFileNm.WidthDef = 755
        Me.lblSvFileNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSvPath
        '
        Me.lblSvPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvPath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvPath.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSvPath.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSvPath.CountWrappedLine = False
        Me.lblSvPath.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSvPath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSvPath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSvPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSvPath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSvPath.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSvPath.HeightDef = 18
        Me.lblSvPath.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSvPath.HissuLabelVisible = False
        Me.lblSvPath.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSvPath.IsByteCheck = 100
        Me.lblSvPath.IsCalendarCheck = False
        Me.lblSvPath.IsDakutenCheck = False
        Me.lblSvPath.IsEisuCheck = False
        Me.lblSvPath.IsForbiddenWordsCheck = False
        Me.lblSvPath.IsFullByteCheck = 0
        Me.lblSvPath.IsHankakuCheck = False
        Me.lblSvPath.IsHissuCheck = False
        Me.lblSvPath.IsKanaCheck = False
        Me.lblSvPath.IsMiddleSpace = False
        Me.lblSvPath.IsNumericCheck = False
        Me.lblSvPath.IsSujiCheck = False
        Me.lblSvPath.IsZenkakuCheck = False
        Me.lblSvPath.ItemName = ""
        Me.lblSvPath.LineSpace = 0
        Me.lblSvPath.Location = New System.Drawing.Point(168, 653)
        Me.lblSvPath.MaxLength = 100
        Me.lblSvPath.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSvPath.MaxLineCount = 0
        Me.lblSvPath.Multiline = False
        Me.lblSvPath.Name = "lblSvPath"
        Me.lblSvPath.ReadOnly = True
        Me.lblSvPath.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSvPath.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSvPath.Size = New System.Drawing.Size(755, 18)
        Me.lblSvPath.TabIndex = 623
        Me.lblSvPath.TabStop = False
        Me.lblSvPath.TabStopSetting = False
        Me.lblSvPath.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
    "---10---XX---10---X"
        Me.lblSvPath.UseSystemPasswordChar = False
        Me.lblSvPath.Visible = False
        Me.lblSvPath.WidthDef = 755
        Me.lblSvPath.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnClear.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnClear.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnClear.EnableStatus = True
        Me.btnClear.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.HeightDef = 22
        Me.btnClear.Location = New System.Drawing.Point(991, 630)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(70, 22)
        Me.btnClear.TabIndex = 625
        Me.btnClear.TabStopSetting = True
        Me.btnClear.Text = "クリア"
        Me.btnClear.TextValue = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        Me.btnClear.WidthDef = 70
        '
        'lblTitleInkaDate
        '
        Me.lblTitleInkaDate.AutoSize = True
        Me.lblTitleInkaDate.AutoSizeDef = True
        Me.lblTitleInkaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInkaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInkaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInkaDate.EnableStatus = False
        Me.lblTitleInkaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInkaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInkaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInkaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInkaDate.HeightDef = 13
        Me.lblTitleInkaDate.Location = New System.Drawing.Point(117, 592)
        Me.lblTitleInkaDate.Name = "lblTitleInkaDate"
        Me.lblTitleInkaDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleInkaDate.TabIndex = 626
        Me.lblTitleInkaDate.Text = "入荷日"
        Me.lblTitleInkaDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInkaDate.TextValue = "入荷日"
        Me.lblTitleInkaDate.WidthDef = 49
        '
        'imdInkaDate
        '
        Me.imdInkaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInkaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInkaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInkaDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdInkaDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInkaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInkaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInkaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInkaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInkaDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInkaDate.HeightDef = 18
        Me.imdInkaDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInkaDate.HissuLabelVisible = False
        Me.imdInkaDate.Holiday = True
        Me.imdInkaDate.IsAfterDateCheck = False
        Me.imdInkaDate.IsBeforeDateCheck = False
        Me.imdInkaDate.IsHissuCheck = False
        Me.imdInkaDate.IsMinDateCheck = "1900/01/01"
        Me.imdInkaDate.ItemName = ""
        Me.imdInkaDate.Location = New System.Drawing.Point(168, 590)
        Me.imdInkaDate.Name = "imdInkaDate"
        Me.imdInkaDate.Number = CType(10101000000, Long)
        Me.imdInkaDate.ReadOnly = False
        Me.imdInkaDate.Size = New System.Drawing.Size(118, 18)
        Me.imdInkaDate.TabIndex = 627
        Me.imdInkaDate.TabStopSetting = True
        Me.imdInkaDate.TextValue = ""
        Me.imdInkaDate.Value = New Date(CType(0, Long))
        Me.imdInkaDate.WidthDef = 118
        '
        'chkInkaDateVersFlg
        '
        Me.chkInkaDateVersFlg.AutoSize = True
        Me.chkInkaDateVersFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkInkaDateVersFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkInkaDateVersFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkInkaDateVersFlg.EnableStatus = True
        Me.chkInkaDateVersFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkInkaDateVersFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkInkaDateVersFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkInkaDateVersFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkInkaDateVersFlg.HeightDef = 17
        Me.chkInkaDateVersFlg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkInkaDateVersFlg.Location = New System.Drawing.Point(289, 591)
        Me.chkInkaDateVersFlg.Margin = New System.Windows.Forms.Padding(0)
        Me.chkInkaDateVersFlg.Name = "chkInkaDateVersFlg"
        Me.chkInkaDateVersFlg.Size = New System.Drawing.Size(54, 17)
        Me.chkInkaDateVersFlg.TabIndex = 628
        Me.chkInkaDateVersFlg.TabStopSetting = True
        Me.chkInkaDateVersFlg.Text = "汎用"
        Me.chkInkaDateVersFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkInkaDateVersFlg.TextValue = "汎用"
        Me.chkInkaDateVersFlg.UseVisualStyleBackColor = True
        Me.chkInkaDateVersFlg.WidthDef = 54
        '
        'lblInkaDateHissu
        '
        Me.lblInkaDateHissu.AutoSize = True
        Me.lblInkaDateHissu.AutoSizeDef = True
        Me.lblInkaDateHissu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateHissu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateHissu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblInkaDateHissu.EnableStatus = False
        Me.lblInkaDateHissu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateHissu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateHissu.ForeColor = System.Drawing.Color.Red
        Me.lblInkaDateHissu.ForeColorDef = System.Drawing.Color.Red
        Me.lblInkaDateHissu.HeightDef = 13
        Me.lblInkaDateHissu.Location = New System.Drawing.Point(360, 592)
        Me.lblInkaDateHissu.Name = "lblInkaDateHissu"
        Me.lblInkaDateHissu.Size = New System.Drawing.Size(14, 13)
        Me.lblInkaDateHissu.TabIndex = 629
        Me.lblInkaDateHissu.Text = "*"
        Me.lblInkaDateHissu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInkaDateHissu.TextValue = "*"
        Me.lblInkaDateHissu.WidthDef = 14
        '
        'lblInkaDateVersFlg
        '
        Me.lblInkaDateVersFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateVersFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateVersFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInkaDateVersFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInkaDateVersFlg.CountWrappedLine = False
        Me.lblInkaDateVersFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblInkaDateVersFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateVersFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateVersFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaDateVersFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaDateVersFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblInkaDateVersFlg.HeightDef = 18
        Me.lblInkaDateVersFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateVersFlg.HissuLabelVisible = False
        Me.lblInkaDateVersFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblInkaDateVersFlg.IsByteCheck = 0
        Me.lblInkaDateVersFlg.IsCalendarCheck = False
        Me.lblInkaDateVersFlg.IsDakutenCheck = False
        Me.lblInkaDateVersFlg.IsEisuCheck = False
        Me.lblInkaDateVersFlg.IsForbiddenWordsCheck = False
        Me.lblInkaDateVersFlg.IsFullByteCheck = 0
        Me.lblInkaDateVersFlg.IsHankakuCheck = False
        Me.lblInkaDateVersFlg.IsHissuCheck = False
        Me.lblInkaDateVersFlg.IsKanaCheck = False
        Me.lblInkaDateVersFlg.IsMiddleSpace = False
        Me.lblInkaDateVersFlg.IsNumericCheck = False
        Me.lblInkaDateVersFlg.IsSujiCheck = False
        Me.lblInkaDateVersFlg.IsZenkakuCheck = False
        Me.lblInkaDateVersFlg.ItemName = ""
        Me.lblInkaDateVersFlg.LineSpace = 0
        Me.lblInkaDateVersFlg.Location = New System.Drawing.Point(380, 590)
        Me.lblInkaDateVersFlg.MaxLength = 0
        Me.lblInkaDateVersFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblInkaDateVersFlg.MaxLineCount = 0
        Me.lblInkaDateVersFlg.Multiline = False
        Me.lblInkaDateVersFlg.Name = "lblInkaDateVersFlg"
        Me.lblInkaDateVersFlg.ReadOnly = True
        Me.lblInkaDateVersFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblInkaDateVersFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblInkaDateVersFlg.Size = New System.Drawing.Size(50, 18)
        Me.lblInkaDateVersFlg.TabIndex = 630
        Me.lblInkaDateVersFlg.TabStop = False
        Me.lblInkaDateVersFlg.TabStopSetting = False
        Me.lblInkaDateVersFlg.TextValue = ""
        Me.lblInkaDateVersFlg.UseSystemPasswordChar = False
        Me.lblInkaDateVersFlg.Visible = False
        Me.lblInkaDateVersFlg.WidthDef = 50
        Me.lblInkaDateVersFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblInkaDateEditableFlg
        '
        Me.lblInkaDateEditableFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateEditableFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateEditableFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInkaDateEditableFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInkaDateEditableFlg.CountWrappedLine = False
        Me.lblInkaDateEditableFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblInkaDateEditableFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateEditableFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaDateEditableFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaDateEditableFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaDateEditableFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblInkaDateEditableFlg.HeightDef = 18
        Me.lblInkaDateEditableFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaDateEditableFlg.HissuLabelVisible = False
        Me.lblInkaDateEditableFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblInkaDateEditableFlg.IsByteCheck = 0
        Me.lblInkaDateEditableFlg.IsCalendarCheck = False
        Me.lblInkaDateEditableFlg.IsDakutenCheck = False
        Me.lblInkaDateEditableFlg.IsEisuCheck = False
        Me.lblInkaDateEditableFlg.IsForbiddenWordsCheck = False
        Me.lblInkaDateEditableFlg.IsFullByteCheck = 0
        Me.lblInkaDateEditableFlg.IsHankakuCheck = False
        Me.lblInkaDateEditableFlg.IsHissuCheck = False
        Me.lblInkaDateEditableFlg.IsKanaCheck = False
        Me.lblInkaDateEditableFlg.IsMiddleSpace = False
        Me.lblInkaDateEditableFlg.IsNumericCheck = False
        Me.lblInkaDateEditableFlg.IsSujiCheck = False
        Me.lblInkaDateEditableFlg.IsZenkakuCheck = False
        Me.lblInkaDateEditableFlg.ItemName = ""
        Me.lblInkaDateEditableFlg.LineSpace = 0
        Me.lblInkaDateEditableFlg.Location = New System.Drawing.Point(418, 590)
        Me.lblInkaDateEditableFlg.MaxLength = 0
        Me.lblInkaDateEditableFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblInkaDateEditableFlg.MaxLineCount = 0
        Me.lblInkaDateEditableFlg.Multiline = False
        Me.lblInkaDateEditableFlg.Name = "lblInkaDateEditableFlg"
        Me.lblInkaDateEditableFlg.ReadOnly = True
        Me.lblInkaDateEditableFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblInkaDateEditableFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblInkaDateEditableFlg.Size = New System.Drawing.Size(50, 18)
        Me.lblInkaDateEditableFlg.TabIndex = 631
        Me.lblInkaDateEditableFlg.TabStop = False
        Me.lblInkaDateEditableFlg.TabStopSetting = False
        Me.lblInkaDateEditableFlg.TextValue = ""
        Me.lblInkaDateEditableFlg.UseSystemPasswordChar = False
        Me.lblInkaDateEditableFlg.Visible = False
        Me.lblInkaDateEditableFlg.WidthDef = 50
        Me.lblInkaDateEditableFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMM020F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM020F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents chkRec As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtLotNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCoaLink As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCoaName As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnRowAdd_M As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents TitlelblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsKey As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDest As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSvFileNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSvPath As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnClear As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleInkaDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdInkaDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents chkInkaDateVersFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblInkaDateHissu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblInkaDateVersFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblInkaDateEditableFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
