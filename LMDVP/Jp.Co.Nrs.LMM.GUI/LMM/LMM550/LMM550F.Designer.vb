<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM550F
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
        Dim EnhancedScrollBarRenderer5 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer6 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail2_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail2_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail3_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail3_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.sprDetail_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblShiharaiTariffCd2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtShiharaiTariffCd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblShiharaiTariffRem = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtShiharaiTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail2 = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprDetail2_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblShiharaiTariffCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtShiharaiTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblStrDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDataTp = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTableTp = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnColAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnColDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnLock = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnUnLock = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbDataTp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbTableTp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblMaxEda = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblAddCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblExcelDt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkCarTp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblWtLv = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkKyori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkCityExtcA = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkCityExtcB = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkWintExtcA = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkWintExtcB = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkRelyExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkInsu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkFrryExtc10kg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkFrryExtcPart = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.imdStrDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblChkDataTp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblChkTableTp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblDefAddCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.sprDetail3 = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprDetail3_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.SheetView2 = New FarPoint.Win.Spread.SheetView()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail3_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail3_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail2_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail3_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SheetView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprDetail3)
        Me.pnlViewAria.Controls.Add(Me.lblDefAddCnt)
        Me.pnlViewAria.Controls.Add(Me.lblChkTableTp)
        Me.pnlViewAria.Controls.Add(Me.lblChkDataTp)
        Me.pnlViewAria.Controls.Add(Me.imdStrDate)
        Me.pnlViewAria.Controls.Add(Me.lblChkFrryExtcPart)
        Me.pnlViewAria.Controls.Add(Me.lblChkFrryExtc10kg)
        Me.pnlViewAria.Controls.Add(Me.lblChkInsu)
        Me.pnlViewAria.Controls.Add(Me.lblChkRelyExtc)
        Me.pnlViewAria.Controls.Add(Me.lblChkWintExtcB)
        Me.pnlViewAria.Controls.Add(Me.lblChkWintExtcA)
        Me.pnlViewAria.Controls.Add(Me.lblChkCityExtcB)
        Me.pnlViewAria.Controls.Add(Me.lblChkCityExtcA)
        Me.pnlViewAria.Controls.Add(Me.lblChkKyori)
        Me.pnlViewAria.Controls.Add(Me.lblWtLv)
        Me.pnlViewAria.Controls.Add(Me.lblChkCarTp)
        Me.pnlViewAria.Controls.Add(Me.lblExcelDt)
        Me.pnlViewAria.Controls.Add(Me.lblAddCnt)
        Me.pnlViewAria.Controls.Add(Me.lblMaxEda)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.cmbTableTp)
        Me.pnlViewAria.Controls.Add(Me.cmbDataTp)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.btnUnLock)
        Me.pnlViewAria.Controls.Add(Me.btnLock)
        Me.pnlViewAria.Controls.Add(Me.btnColDel)
        Me.pnlViewAria.Controls.Add(Me.btnColAdd)
        Me.pnlViewAria.Controls.Add(Me.lblTableTp)
        Me.pnlViewAria.Controls.Add(Me.lblDataTp)
        Me.pnlViewAria.Controls.Add(Me.lblStrDate)
        Me.pnlViewAria.Controls.Add(Me.lblShiharaiTariffCd)
        Me.pnlViewAria.Controls.Add(Me.txtShiharaiTariffCd)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.sprDetail2)
        Me.pnlViewAria.Controls.Add(Me.lblNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.txtShiharaiTariffRem)
        Me.pnlViewAria.Controls.Add(Me.lblShiharaiTariffRem)
        Me.pnlViewAria.Controls.Add(Me.lblShiharaiTariffCd2)
        Me.pnlViewAria.Controls.Add(Me.txtShiharaiTariffCd2)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
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
        Me.sprDetail.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail.HeightDef = 255
        Me.sprDetail.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.HorizontalScrollBar.Name = ""
        Me.sprDetail.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer5
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 31)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail_Sheet1})
        Me.sprDetail.Size = New System.Drawing.Size(1245, 255)
        Me.sprDetail.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.VerticalScrollBar.Name = ""
        Me.sprDetail.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer6
        Me.sprDetail.WidthDef = 1245
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
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
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
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
        Me.sprDetail.SetViewportTopRow(1, 0, 1)
        '
        'sprDetail_Sheet1
        '
        Me.sprDetail_Sheet1.Reset()
        Me.sprDetail_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprDetail_Sheet1.RowCount = 1
        Me.sprDetail_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprDetail_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.sprDetail_Sheet1.Cells.Get(0, 0).Locked = True
        Me.sprDetail_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " "
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(0).Label = " "
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        Me.sprDetail_Sheet1.Columns.Get(0).Label = " "
        Me.sprDetail_Sheet1.Columns.Get(0).Width = 20.0!
        TextCellType3.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDetail_Sheet1.Columns.Get(1).CellType = TextCellType3
        Me.sprDetail_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.FrozenRowCount = 1
        Me.sprDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail_Sheet1.RowHeader.Cells.Get(0, 0).Value = "ｸﾘｱ"
        Me.sprDetail_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprDetail_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprDetail_Sheet1.Rows.Default.Height = 18.0!
        Me.sprDetail_Sheet1.Rows.Default.Resizable = False
        Me.sprDetail_Sheet1.Rows.Default.Visible = True
        Me.sprDetail_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail_Sheet1.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprDetail_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprDetail_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprDetail_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.SheetCornerStyle.Locked = True
        Me.sprDetail_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprDetail_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.StartingRowNumber = 0
        Me.sprDetail_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1118, 413)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 235
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1019, 413)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(93, 18)
        Me.LmTitleLabel10.TabIndex = 234
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 93
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1118, 391)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 233
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel1
        '
        Me.LmTitleLabel1.AutoSizeDef = False
        Me.LmTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel1.EnableStatus = False
        Me.LmTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.HeightDef = 18
        Me.LmTitleLabel1.Location = New System.Drawing.Point(1016, 391)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(96, 18)
        Me.LmTitleLabel1.TabIndex = 232
        Me.LmTitleLabel1.Text = "更新者"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "更新者"
        Me.LmTitleLabel1.WidthDef = 96
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1118, 369)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 231
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1030, 369)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(82, 18)
        Me.LmTitleLabel8.TabIndex = 230
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 82
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1118, 347)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 229
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1125, 315)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 228
        Me.lblSituation.TabStop = False
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
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1030, 347)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(82, 18)
        Me.LmTitleLabel7.TabIndex = 227
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 82
        '
        'lblShiharaiTariffCd2
        '
        Me.lblShiharaiTariffCd2.AutoSizeDef = False
        Me.lblShiharaiTariffCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffCd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffCd2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblShiharaiTariffCd2.EnableStatus = False
        Me.lblShiharaiTariffCd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffCd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffCd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffCd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffCd2.HeightDef = 18
        Me.lblShiharaiTariffCd2.Location = New System.Drawing.Point(28, 433)
        Me.lblShiharaiTariffCd2.Name = "lblShiharaiTariffCd2"
        Me.lblShiharaiTariffCd2.Size = New System.Drawing.Size(131, 18)
        Me.lblShiharaiTariffCd2.TabIndex = 241
        Me.lblShiharaiTariffCd2.Text = "2次タリフコード"
        Me.lblShiharaiTariffCd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblShiharaiTariffCd2.TextValue = "2次タリフコード"
        Me.lblShiharaiTariffCd2.WidthDef = 131
        '
        'txtShiharaiTariffCd2
        '
        Me.txtShiharaiTariffCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffCd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffCd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShiharaiTariffCd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShiharaiTariffCd2.CountWrappedLine = False
        Me.txtShiharaiTariffCd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShiharaiTariffCd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffCd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffCd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffCd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffCd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShiharaiTariffCd2.HeightDef = 18
        Me.txtShiharaiTariffCd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShiharaiTariffCd2.HissuLabelVisible = False
        Me.txtShiharaiTariffCd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtShiharaiTariffCd2.IsByteCheck = 10
        Me.txtShiharaiTariffCd2.IsCalendarCheck = False
        Me.txtShiharaiTariffCd2.IsDakutenCheck = False
        Me.txtShiharaiTariffCd2.IsEisuCheck = False
        Me.txtShiharaiTariffCd2.IsForbiddenWordsCheck = False
        Me.txtShiharaiTariffCd2.IsFullByteCheck = 0
        Me.txtShiharaiTariffCd2.IsHankakuCheck = False
        Me.txtShiharaiTariffCd2.IsHissuCheck = False
        Me.txtShiharaiTariffCd2.IsKanaCheck = False
        Me.txtShiharaiTariffCd2.IsMiddleSpace = False
        Me.txtShiharaiTariffCd2.IsNumericCheck = False
        Me.txtShiharaiTariffCd2.IsSujiCheck = False
        Me.txtShiharaiTariffCd2.IsZenkakuCheck = False
        Me.txtShiharaiTariffCd2.ItemName = ""
        Me.txtShiharaiTariffCd2.LineSpace = 0
        Me.txtShiharaiTariffCd2.Location = New System.Drawing.Point(165, 433)
        Me.txtShiharaiTariffCd2.MaxLength = 10
        Me.txtShiharaiTariffCd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShiharaiTariffCd2.MaxLineCount = 0
        Me.txtShiharaiTariffCd2.Multiline = False
        Me.txtShiharaiTariffCd2.Name = "txtShiharaiTariffCd2"
        Me.txtShiharaiTariffCd2.ReadOnly = False
        Me.txtShiharaiTariffCd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShiharaiTariffCd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShiharaiTariffCd2.Size = New System.Drawing.Size(150, 18)
        Me.txtShiharaiTariffCd2.TabIndex = 240
        Me.txtShiharaiTariffCd2.TabStopSetting = True
        Me.txtShiharaiTariffCd2.Tag = ""
        Me.txtShiharaiTariffCd2.TextValue = "X---10---X"
        Me.txtShiharaiTariffCd2.UseSystemPasswordChar = False
        Me.txtShiharaiTariffCd2.WidthDef = 150
        Me.txtShiharaiTariffCd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblShiharaiTariffRem
        '
        Me.lblShiharaiTariffRem.AutoSizeDef = False
        Me.lblShiharaiTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblShiharaiTariffRem.EnableStatus = False
        Me.lblShiharaiTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffRem.HeightDef = 18
        Me.lblShiharaiTariffRem.Location = New System.Drawing.Point(28, 413)
        Me.lblShiharaiTariffRem.Name = "lblShiharaiTariffRem"
        Me.lblShiharaiTariffRem.Size = New System.Drawing.Size(131, 18)
        Me.lblShiharaiTariffRem.TabIndex = 242
        Me.lblShiharaiTariffRem.Text = "備考"
        Me.lblShiharaiTariffRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblShiharaiTariffRem.TextValue = "備考"
        Me.lblShiharaiTariffRem.WidthDef = 131
        '
        'txtShiharaiTariffRem
        '
        Me.txtShiharaiTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShiharaiTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShiharaiTariffRem.CountWrappedLine = False
        Me.txtShiharaiTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShiharaiTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShiharaiTariffRem.HeightDef = 18
        Me.txtShiharaiTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShiharaiTariffRem.HissuLabelVisible = True
        Me.txtShiharaiTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtShiharaiTariffRem.IsByteCheck = 100
        Me.txtShiharaiTariffRem.IsCalendarCheck = False
        Me.txtShiharaiTariffRem.IsDakutenCheck = False
        Me.txtShiharaiTariffRem.IsEisuCheck = False
        Me.txtShiharaiTariffRem.IsForbiddenWordsCheck = False
        Me.txtShiharaiTariffRem.IsFullByteCheck = 0
        Me.txtShiharaiTariffRem.IsHankakuCheck = False
        Me.txtShiharaiTariffRem.IsHissuCheck = True
        Me.txtShiharaiTariffRem.IsKanaCheck = False
        Me.txtShiharaiTariffRem.IsMiddleSpace = False
        Me.txtShiharaiTariffRem.IsNumericCheck = False
        Me.txtShiharaiTariffRem.IsSujiCheck = False
        Me.txtShiharaiTariffRem.IsZenkakuCheck = False
        Me.txtShiharaiTariffRem.ItemName = ""
        Me.txtShiharaiTariffRem.LineSpace = 0
        Me.txtShiharaiTariffRem.Location = New System.Drawing.Point(165, 412)
        Me.txtShiharaiTariffRem.MaxLength = 100
        Me.txtShiharaiTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShiharaiTariffRem.MaxLineCount = 0
        Me.txtShiharaiTariffRem.Multiline = False
        Me.txtShiharaiTariffRem.Name = "txtShiharaiTariffRem"
        Me.txtShiharaiTariffRem.ReadOnly = False
        Me.txtShiharaiTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShiharaiTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShiharaiTariffRem.Size = New System.Drawing.Size(724, 18)
        Me.txtShiharaiTariffRem.TabIndex = 243
        Me.txtShiharaiTariffRem.TabStopSetting = True
        Me.txtShiharaiTariffRem.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" &
    "---10---XX---10---X"
        Me.txtShiharaiTariffRem.UseSystemPasswordChar = False
        Me.txtShiharaiTariffRem.WidthDef = 724
        Me.txtShiharaiTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblNrsBrCd
        '
        Me.lblNrsBrCd.AutoSizeDef = False
        Me.lblNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblNrsBrCd.EnableStatus = False
        Me.lblNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNrsBrCd.HeightDef = 18
        Me.lblNrsBrCd.Location = New System.Drawing.Point(39, 307)
        Me.lblNrsBrCd.Name = "lblNrsBrCd"
        Me.lblNrsBrCd.Size = New System.Drawing.Size(120, 18)
        Me.lblNrsBrCd.TabIndex = 418
        Me.lblNrsBrCd.Text = "営業所"
        Me.lblNrsBrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNrsBrCd.TextValue = "営業所"
        Me.lblNrsBrCd.WidthDef = 120
        '
        'sprDetail2
        '
        Me.sprDetail2.AccessibleDescription = ""
        Me.sprDetail2.AllowUserZoom = False
        Me.sprDetail2.AutoImeMode = False
        Me.sprDetail2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail2.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetail2.CellClickEventArgs = Nothing
        Me.sprDetail2.CheckToCheckBox = True
        Me.sprDetail2.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetail2.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail2.EditModeReplace = True
        Me.sprDetail2.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprDetail2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail2.HeightDef = 380
        Me.sprDetail2.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail2.HorizontalScrollBar.Name = ""
        Me.sprDetail2.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.sprDetail2.KeyboardCheckBoxOn = False
        Me.sprDetail2.Location = New System.Drawing.Point(15, 496)
        Me.sprDetail2.Name = "sprDetail2"
        Me.sprDetail2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail2.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail2_Sheet1})
        Me.sprDetail2.Size = New System.Drawing.Size(1245, 380)
        Me.sprDetail2.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDetail2.SortColumn = True
        Me.sprDetail2.SpanColumnLock = True
        Me.sprDetail2.SpreadDoubleClicked = False
        Me.sprDetail2.TabIndex = 423
        Me.sprDetail2.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail2.TextValue = Nothing
        Me.sprDetail2.UseGrouping = False
        Me.sprDetail2.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail2.VerticalScrollBar.Name = ""
        Me.sprDetail2.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.sprDetail2.WidthDef = 1245
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail2_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDetail2_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDetail2.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail2_InputMapWhenFocusedNormal)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDetail2.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail2_InputMapWhenAncestorOfFocusedNormal)
        '
        'sprDetail2_Sheet1
        '
        Me.sprDetail2_Sheet1.Reset()
        Me.sprDetail2_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprDetail2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprDetail2_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprDetail2_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprDetail2_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprDetail2_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprDetail2_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail2_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprDetail2_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprDetail2_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDetail2_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.sprDetail2_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.DefaultStyle.Locked = False
        Me.sprDetail2_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprDetail2_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprDetail2_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprDetail2_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprDetail2_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail2_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail2_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail2_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprDetail2_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprDetail2_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail2_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprDetail2_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprDetail2_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.Rows.Default.Height = 18.0!
        Me.sprDetail2_Sheet1.Rows.Default.Resizable = False
        Me.sprDetail2_Sheet1.Rows.Default.Visible = True
        Me.sprDetail2_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail2_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprDetail2_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprDetail2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail2_Sheet1.SheetCornerStyle.Locked = True
        Me.sprDetail2_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail2_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprDetail2_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail2_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnRowDel
        '
        Me.btnRowDel.AutoSize = True
        Me.btnRowDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowDel.EnableStatus = True
        Me.btnRowDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.HeightDef = 23
        Me.btnRowDel.Location = New System.Drawing.Point(165, 467)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(66, 23)
        Me.btnRowDel.TabIndex = 425
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 66
        '
        'btnRowAdd
        '
        Me.btnRowAdd.AutoSize = True
        Me.btnRowAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowAdd.EnableStatus = True
        Me.btnRowAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.HeightDef = 23
        Me.btnRowAdd.Location = New System.Drawing.Point(93, 467)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(66, 23)
        Me.btnRowAdd.TabIndex = 426
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 66
        '
        'lblShiharaiTariffCd
        '
        Me.lblShiharaiTariffCd.AutoSizeDef = False
        Me.lblShiharaiTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblShiharaiTariffCd.EnableStatus = False
        Me.lblShiharaiTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiTariffCd.HeightDef = 18
        Me.lblShiharaiTariffCd.Location = New System.Drawing.Point(28, 328)
        Me.lblShiharaiTariffCd.Name = "lblShiharaiTariffCd"
        Me.lblShiharaiTariffCd.Size = New System.Drawing.Size(131, 18)
        Me.lblShiharaiTariffCd.TabIndex = 428
        Me.lblShiharaiTariffCd.Text = "支払タリフコード"
        Me.lblShiharaiTariffCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblShiharaiTariffCd.TextValue = "支払タリフコード"
        Me.lblShiharaiTariffCd.WidthDef = 131
        '
        'txtShiharaiTariffCd
        '
        Me.txtShiharaiTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShiharaiTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShiharaiTariffCd.CountWrappedLine = False
        Me.txtShiharaiTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShiharaiTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShiharaiTariffCd.HeightDef = 18
        Me.txtShiharaiTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShiharaiTariffCd.HissuLabelVisible = True
        Me.txtShiharaiTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtShiharaiTariffCd.IsByteCheck = 10
        Me.txtShiharaiTariffCd.IsCalendarCheck = False
        Me.txtShiharaiTariffCd.IsDakutenCheck = False
        Me.txtShiharaiTariffCd.IsEisuCheck = False
        Me.txtShiharaiTariffCd.IsForbiddenWordsCheck = False
        Me.txtShiharaiTariffCd.IsFullByteCheck = 0
        Me.txtShiharaiTariffCd.IsHankakuCheck = False
        Me.txtShiharaiTariffCd.IsHissuCheck = True
        Me.txtShiharaiTariffCd.IsKanaCheck = False
        Me.txtShiharaiTariffCd.IsMiddleSpace = False
        Me.txtShiharaiTariffCd.IsNumericCheck = False
        Me.txtShiharaiTariffCd.IsSujiCheck = False
        Me.txtShiharaiTariffCd.IsZenkakuCheck = False
        Me.txtShiharaiTariffCd.ItemName = ""
        Me.txtShiharaiTariffCd.LineSpace = 0
        Me.txtShiharaiTariffCd.Location = New System.Drawing.Point(165, 328)
        Me.txtShiharaiTariffCd.MaxLength = 10
        Me.txtShiharaiTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShiharaiTariffCd.MaxLineCount = 0
        Me.txtShiharaiTariffCd.Multiline = False
        Me.txtShiharaiTariffCd.Name = "txtShiharaiTariffCd"
        Me.txtShiharaiTariffCd.ReadOnly = False
        Me.txtShiharaiTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShiharaiTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShiharaiTariffCd.Size = New System.Drawing.Size(150, 18)
        Me.txtShiharaiTariffCd.TabIndex = 427
        Me.txtShiharaiTariffCd.TabStopSetting = True
        Me.txtShiharaiTariffCd.Tag = ""
        Me.txtShiharaiTariffCd.TextValue = ""
        Me.txtShiharaiTariffCd.UseSystemPasswordChar = False
        Me.txtShiharaiTariffCd.WidthDef = 150
        Me.txtShiharaiTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblStrDate
        '
        Me.lblStrDate.AutoSizeDef = False
        Me.lblStrDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStrDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStrDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStrDate.EnableStatus = False
        Me.lblStrDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStrDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStrDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStrDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStrDate.HeightDef = 18
        Me.lblStrDate.Location = New System.Drawing.Point(28, 349)
        Me.lblStrDate.Name = "lblStrDate"
        Me.lblStrDate.Size = New System.Drawing.Size(131, 18)
        Me.lblStrDate.TabIndex = 431
        Me.lblStrDate.Text = "適用開始日"
        Me.lblStrDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStrDate.TextValue = "適用開始日"
        Me.lblStrDate.WidthDef = 131
        '
        'lblDataTp
        '
        Me.lblDataTp.AutoSizeDef = False
        Me.lblDataTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDataTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDataTp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblDataTp.EnableStatus = False
        Me.lblDataTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDataTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDataTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDataTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDataTp.HeightDef = 18
        Me.lblDataTp.Location = New System.Drawing.Point(28, 370)
        Me.lblDataTp.Name = "lblDataTp"
        Me.lblDataTp.Size = New System.Drawing.Size(131, 18)
        Me.lblDataTp.TabIndex = 435
        Me.lblDataTp.Text = "データタイプ"
        Me.lblDataTp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDataTp.TextValue = "データタイプ"
        Me.lblDataTp.WidthDef = 131
        '
        'lblTableTp
        '
        Me.lblTableTp.AutoSizeDef = False
        Me.lblTableTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTableTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTableTp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTableTp.EnableStatus = False
        Me.lblTableTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTableTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTableTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTableTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTableTp.HeightDef = 18
        Me.lblTableTp.Location = New System.Drawing.Point(28, 391)
        Me.lblTableTp.Name = "lblTableTp"
        Me.lblTableTp.Size = New System.Drawing.Size(131, 18)
        Me.lblTableTp.TabIndex = 436
        Me.lblTableTp.Text = "計算種別"
        Me.lblTableTp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTableTp.TextValue = "計算種別"
        Me.lblTableTp.WidthDef = 131
        '
        'btnColAdd
        '
        Me.btnColAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnColAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnColAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnColAdd.EnableStatus = True
        Me.btnColAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnColAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnColAdd.HeightDef = 22
        Me.btnColAdd.Location = New System.Drawing.Point(236, 468)
        Me.btnColAdd.Name = "btnColAdd"
        Me.btnColAdd.Size = New System.Drawing.Size(80, 22)
        Me.btnColAdd.TabIndex = 437
        Me.btnColAdd.TabStopSetting = True
        Me.btnColAdd.Text = "列挿入"
        Me.btnColAdd.TextValue = "列挿入"
        Me.btnColAdd.UseVisualStyleBackColor = True
        Me.btnColAdd.WidthDef = 80
        '
        'btnColDel
        '
        Me.btnColDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnColDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnColDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnColDel.EnableStatus = True
        Me.btnColDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnColDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnColDel.HeightDef = 22
        Me.btnColDel.Location = New System.Drawing.Point(322, 468)
        Me.btnColDel.Name = "btnColDel"
        Me.btnColDel.Size = New System.Drawing.Size(80, 22)
        Me.btnColDel.TabIndex = 438
        Me.btnColDel.TabStopSetting = True
        Me.btnColDel.Text = "列削除"
        Me.btnColDel.TextValue = "列削除"
        Me.btnColDel.UseVisualStyleBackColor = True
        Me.btnColDel.WidthDef = 80
        '
        'btnLock
        '
        Me.btnLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnLock.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnLock.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnLock.EnableStatus = True
        Me.btnLock.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLock.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLock.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLock.HeightDef = 22
        Me.btnLock.Location = New System.Drawing.Point(409, 468)
        Me.btnLock.Name = "btnLock"
        Me.btnLock.Size = New System.Drawing.Size(80, 22)
        Me.btnLock.TabIndex = 439
        Me.btnLock.TabStopSetting = True
        Me.btnLock.Text = "ロック"
        Me.btnLock.TextValue = "ロック"
        Me.btnLock.UseVisualStyleBackColor = True
        Me.btnLock.WidthDef = 80
        '
        'btnUnLock
        '
        Me.btnUnLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnUnLock.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnUnLock.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnUnLock.EnableStatus = True
        Me.btnUnLock.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUnLock.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUnLock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUnLock.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUnLock.HeightDef = 22
        Me.btnUnLock.Location = New System.Drawing.Point(495, 468)
        Me.btnUnLock.Name = "btnUnLock"
        Me.btnUnLock.Size = New System.Drawing.Size(92, 22)
        Me.btnUnLock.TabIndex = 440
        Me.btnUnLock.TabStopSetting = True
        Me.btnUnLock.Text = "アンロック"
        Me.btnUnLock.TextValue = "アンロック"
        Me.btnUnLock.UseVisualStyleBackColor = True
        Me.btnUnLock.WidthDef = 92
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(165, 307)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(309, 18)
        Me.cmbNrsBrCd.TabIndex = 461
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 309
        '
        'cmbDataTp
        '
        Me.cmbDataTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDataTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDataTp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbDataTp.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbDataTp.DataCode = "U010"
        Me.cmbDataTp.DataSource = Nothing
        Me.cmbDataTp.DisplayMember = Nothing
        Me.cmbDataTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDataTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDataTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDataTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDataTp.HeightDef = 18
        Me.cmbDataTp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbDataTp.HissuLabelVisible = True
        Me.cmbDataTp.InsertWildCard = True
        Me.cmbDataTp.IsForbiddenWordsCheck = False
        Me.cmbDataTp.IsHissuCheck = True
        Me.cmbDataTp.ItemName = ""
        Me.cmbDataTp.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbDataTp.Location = New System.Drawing.Point(165, 370)
        Me.cmbDataTp.Name = "cmbDataTp"
        Me.cmbDataTp.ReadOnly = False
        Me.cmbDataTp.SelectedIndex = -1
        Me.cmbDataTp.SelectedItem = Nothing
        Me.cmbDataTp.SelectedText = ""
        Me.cmbDataTp.SelectedValue = ""
        Me.cmbDataTp.Size = New System.Drawing.Size(118, 18)
        Me.cmbDataTp.TabIndex = 559
        Me.cmbDataTp.TabStopSetting = True
        Me.cmbDataTp.TextValue = ""
        Me.cmbDataTp.Value1 = Nothing
        Me.cmbDataTp.Value2 = Nothing
        Me.cmbDataTp.Value3 = Nothing
        Me.cmbDataTp.ValueMember = Nothing
        Me.cmbDataTp.WidthDef = 118
        '
        'cmbTableTp
        '
        Me.cmbTableTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTableTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTableTp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTableTp.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTableTp.DataCode = "U040"
        Me.cmbTableTp.DataSource = Nothing
        Me.cmbTableTp.DisplayMember = Nothing
        Me.cmbTableTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTableTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTableTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTableTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTableTp.HeightDef = 18
        Me.cmbTableTp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTableTp.HissuLabelVisible = True
        Me.cmbTableTp.InsertWildCard = True
        Me.cmbTableTp.IsForbiddenWordsCheck = False
        Me.cmbTableTp.IsHissuCheck = True
        Me.cmbTableTp.ItemName = ""
        Me.cmbTableTp.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTableTp.Location = New System.Drawing.Point(165, 391)
        Me.cmbTableTp.Name = "cmbTableTp"
        Me.cmbTableTp.ReadOnly = False
        Me.cmbTableTp.SelectedIndex = -1
        Me.cmbTableTp.SelectedItem = Nothing
        Me.cmbTableTp.SelectedText = ""
        Me.cmbTableTp.SelectedValue = ""
        Me.cmbTableTp.Size = New System.Drawing.Size(240, 18)
        Me.cmbTableTp.TabIndex = 560
        Me.cmbTableTp.TabStopSetting = True
        Me.cmbTableTp.TextValue = ""
        Me.cmbTableTp.Value1 = Nothing
        Me.cmbTableTp.Value2 = Nothing
        Me.cmbTableTp.Value3 = Nothing
        Me.cmbTableTp.ValueMember = Nothing
        Me.cmbTableTp.WidthDef = 240
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1118, 456)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 613
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1118, 435)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 612
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblMaxEda
        '
        Me.lblMaxEda.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEda.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEda.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMaxEda.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMaxEda.CountWrappedLine = False
        Me.lblMaxEda.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMaxEda.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxEda.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxEda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMaxEda.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMaxEda.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMaxEda.HeightDef = 15
        Me.lblMaxEda.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEda.HissuLabelVisible = False
        Me.lblMaxEda.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMaxEda.IsByteCheck = 0
        Me.lblMaxEda.IsCalendarCheck = False
        Me.lblMaxEda.IsDakutenCheck = False
        Me.lblMaxEda.IsEisuCheck = False
        Me.lblMaxEda.IsForbiddenWordsCheck = False
        Me.lblMaxEda.IsFullByteCheck = 0
        Me.lblMaxEda.IsHankakuCheck = False
        Me.lblMaxEda.IsHissuCheck = False
        Me.lblMaxEda.IsKanaCheck = False
        Me.lblMaxEda.IsMiddleSpace = False
        Me.lblMaxEda.IsNumericCheck = False
        Me.lblMaxEda.IsSujiCheck = False
        Me.lblMaxEda.IsZenkakuCheck = False
        Me.lblMaxEda.ItemName = ""
        Me.lblMaxEda.LineSpace = 0
        Me.lblMaxEda.Location = New System.Drawing.Point(1061, 475)
        Me.lblMaxEda.MaxLength = 0
        Me.lblMaxEda.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMaxEda.MaxLineCount = 0
        Me.lblMaxEda.Multiline = False
        Me.lblMaxEda.Name = "lblMaxEda"
        Me.lblMaxEda.ReadOnly = True
        Me.lblMaxEda.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMaxEda.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMaxEda.Size = New System.Drawing.Size(30, 15)
        Me.lblMaxEda.TabIndex = 614
        Me.lblMaxEda.TabStop = False
        Me.lblMaxEda.TabStopSetting = False
        Me.lblMaxEda.TextValue = ""
        Me.lblMaxEda.UseSystemPasswordChar = False
        Me.lblMaxEda.Visible = False
        Me.lblMaxEda.WidthDef = 30
        Me.lblMaxEda.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblAddCnt
        '
        Me.lblAddCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAddCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAddCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAddCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAddCnt.CountWrappedLine = False
        Me.lblAddCnt.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblAddCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAddCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAddCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAddCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAddCnt.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblAddCnt.HeightDef = 15
        Me.lblAddCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAddCnt.HissuLabelVisible = False
        Me.lblAddCnt.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblAddCnt.IsByteCheck = 0
        Me.lblAddCnt.IsCalendarCheck = False
        Me.lblAddCnt.IsDakutenCheck = False
        Me.lblAddCnt.IsEisuCheck = False
        Me.lblAddCnt.IsForbiddenWordsCheck = False
        Me.lblAddCnt.IsFullByteCheck = 0
        Me.lblAddCnt.IsHankakuCheck = False
        Me.lblAddCnt.IsHissuCheck = False
        Me.lblAddCnt.IsKanaCheck = False
        Me.lblAddCnt.IsMiddleSpace = False
        Me.lblAddCnt.IsNumericCheck = False
        Me.lblAddCnt.IsSujiCheck = False
        Me.lblAddCnt.IsZenkakuCheck = False
        Me.lblAddCnt.ItemName = ""
        Me.lblAddCnt.LineSpace = 0
        Me.lblAddCnt.Location = New System.Drawing.Point(1030, 475)
        Me.lblAddCnt.MaxLength = 0
        Me.lblAddCnt.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblAddCnt.MaxLineCount = 0
        Me.lblAddCnt.Multiline = False
        Me.lblAddCnt.Name = "lblAddCnt"
        Me.lblAddCnt.ReadOnly = True
        Me.lblAddCnt.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblAddCnt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblAddCnt.Size = New System.Drawing.Size(30, 15)
        Me.lblAddCnt.TabIndex = 615
        Me.lblAddCnt.TabStop = False
        Me.lblAddCnt.TabStopSetting = False
        Me.lblAddCnt.TextValue = ""
        Me.lblAddCnt.UseSystemPasswordChar = False
        Me.lblAddCnt.Visible = False
        Me.lblAddCnt.WidthDef = 30
        Me.lblAddCnt.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblExcelDt
        '
        Me.lblExcelDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExcelDt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExcelDt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExcelDt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExcelDt.CountWrappedLine = False
        Me.lblExcelDt.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblExcelDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExcelDt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExcelDt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExcelDt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExcelDt.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblExcelDt.HeightDef = 15
        Me.lblExcelDt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExcelDt.HissuLabelVisible = False
        Me.lblExcelDt.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblExcelDt.IsByteCheck = 0
        Me.lblExcelDt.IsCalendarCheck = False
        Me.lblExcelDt.IsDakutenCheck = False
        Me.lblExcelDt.IsEisuCheck = False
        Me.lblExcelDt.IsForbiddenWordsCheck = False
        Me.lblExcelDt.IsFullByteCheck = 0
        Me.lblExcelDt.IsHankakuCheck = False
        Me.lblExcelDt.IsHissuCheck = False
        Me.lblExcelDt.IsKanaCheck = False
        Me.lblExcelDt.IsMiddleSpace = False
        Me.lblExcelDt.IsNumericCheck = False
        Me.lblExcelDt.IsSujiCheck = False
        Me.lblExcelDt.IsZenkakuCheck = False
        Me.lblExcelDt.ItemName = ""
        Me.lblExcelDt.LineSpace = 0
        Me.lblExcelDt.Location = New System.Drawing.Point(994, 475)
        Me.lblExcelDt.MaxLength = 0
        Me.lblExcelDt.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblExcelDt.MaxLineCount = 0
        Me.lblExcelDt.Multiline = False
        Me.lblExcelDt.Name = "lblExcelDt"
        Me.lblExcelDt.ReadOnly = True
        Me.lblExcelDt.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblExcelDt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblExcelDt.Size = New System.Drawing.Size(30, 15)
        Me.lblExcelDt.TabIndex = 616
        Me.lblExcelDt.TabStop = False
        Me.lblExcelDt.TabStopSetting = False
        Me.lblExcelDt.TextValue = ""
        Me.lblExcelDt.UseSystemPasswordChar = False
        Me.lblExcelDt.Visible = False
        Me.lblExcelDt.WidthDef = 30
        Me.lblExcelDt.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkCarTp
        '
        Me.lblChkCarTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCarTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCarTp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkCarTp.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkCarTp.CountWrappedLine = False
        Me.lblChkCarTp.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkCarTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCarTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCarTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCarTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCarTp.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkCarTp.HeightDef = 15
        Me.lblChkCarTp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCarTp.HissuLabelVisible = False
        Me.lblChkCarTp.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkCarTp.IsByteCheck = 0
        Me.lblChkCarTp.IsCalendarCheck = False
        Me.lblChkCarTp.IsDakutenCheck = False
        Me.lblChkCarTp.IsEisuCheck = False
        Me.lblChkCarTp.IsForbiddenWordsCheck = False
        Me.lblChkCarTp.IsFullByteCheck = 0
        Me.lblChkCarTp.IsHankakuCheck = False
        Me.lblChkCarTp.IsHissuCheck = False
        Me.lblChkCarTp.IsKanaCheck = False
        Me.lblChkCarTp.IsMiddleSpace = False
        Me.lblChkCarTp.IsNumericCheck = False
        Me.lblChkCarTp.IsSujiCheck = False
        Me.lblChkCarTp.IsZenkakuCheck = False
        Me.lblChkCarTp.ItemName = ""
        Me.lblChkCarTp.LineSpace = 0
        Me.lblChkCarTp.Location = New System.Drawing.Point(726, 475)
        Me.lblChkCarTp.MaxLength = 0
        Me.lblChkCarTp.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkCarTp.MaxLineCount = 0
        Me.lblChkCarTp.Multiline = False
        Me.lblChkCarTp.Name = "lblChkCarTp"
        Me.lblChkCarTp.ReadOnly = True
        Me.lblChkCarTp.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkCarTp.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkCarTp.Size = New System.Drawing.Size(30, 15)
        Me.lblChkCarTp.TabIndex = 617
        Me.lblChkCarTp.TabStop = False
        Me.lblChkCarTp.TabStopSetting = False
        Me.lblChkCarTp.TextValue = ""
        Me.lblChkCarTp.UseSystemPasswordChar = False
        Me.lblChkCarTp.Visible = False
        Me.lblChkCarTp.WidthDef = 30
        Me.lblChkCarTp.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblWtLv
        '
        Me.lblWtLv.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWtLv.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWtLv.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWtLv.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWtLv.CountWrappedLine = False
        Me.lblWtLv.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWtLv.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWtLv.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWtLv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWtLv.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWtLv.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWtLv.HeightDef = 15
        Me.lblWtLv.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWtLv.HissuLabelVisible = False
        Me.lblWtLv.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWtLv.IsByteCheck = 0
        Me.lblWtLv.IsCalendarCheck = False
        Me.lblWtLv.IsDakutenCheck = False
        Me.lblWtLv.IsEisuCheck = False
        Me.lblWtLv.IsForbiddenWordsCheck = False
        Me.lblWtLv.IsFullByteCheck = 0
        Me.lblWtLv.IsHankakuCheck = False
        Me.lblWtLv.IsHissuCheck = False
        Me.lblWtLv.IsKanaCheck = False
        Me.lblWtLv.IsMiddleSpace = False
        Me.lblWtLv.IsNumericCheck = False
        Me.lblWtLv.IsSujiCheck = False
        Me.lblWtLv.IsZenkakuCheck = False
        Me.lblWtLv.ItemName = ""
        Me.lblWtLv.LineSpace = 0
        Me.lblWtLv.Location = New System.Drawing.Point(762, 475)
        Me.lblWtLv.MaxLength = 0
        Me.lblWtLv.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWtLv.MaxLineCount = 0
        Me.lblWtLv.Multiline = False
        Me.lblWtLv.Name = "lblWtLv"
        Me.lblWtLv.ReadOnly = True
        Me.lblWtLv.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWtLv.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWtLv.Size = New System.Drawing.Size(30, 15)
        Me.lblWtLv.TabIndex = 618
        Me.lblWtLv.TabStop = False
        Me.lblWtLv.TabStopSetting = False
        Me.lblWtLv.TextValue = ""
        Me.lblWtLv.UseSystemPasswordChar = False
        Me.lblWtLv.Visible = False
        Me.lblWtLv.WidthDef = 30
        Me.lblWtLv.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkKyori
        '
        Me.lblChkKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkKyori.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkKyori.CountWrappedLine = False
        Me.lblChkKyori.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkKyori.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkKyori.HeightDef = 15
        Me.lblChkKyori.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkKyori.HissuLabelVisible = False
        Me.lblChkKyori.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkKyori.IsByteCheck = 0
        Me.lblChkKyori.IsCalendarCheck = False
        Me.lblChkKyori.IsDakutenCheck = False
        Me.lblChkKyori.IsEisuCheck = False
        Me.lblChkKyori.IsForbiddenWordsCheck = False
        Me.lblChkKyori.IsFullByteCheck = 0
        Me.lblChkKyori.IsHankakuCheck = False
        Me.lblChkKyori.IsHissuCheck = False
        Me.lblChkKyori.IsKanaCheck = False
        Me.lblChkKyori.IsMiddleSpace = False
        Me.lblChkKyori.IsNumericCheck = False
        Me.lblChkKyori.IsSujiCheck = False
        Me.lblChkKyori.IsZenkakuCheck = False
        Me.lblChkKyori.ItemName = ""
        Me.lblChkKyori.LineSpace = 0
        Me.lblChkKyori.Location = New System.Drawing.Point(790, 475)
        Me.lblChkKyori.MaxLength = 0
        Me.lblChkKyori.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkKyori.MaxLineCount = 0
        Me.lblChkKyori.Multiline = False
        Me.lblChkKyori.Name = "lblChkKyori"
        Me.lblChkKyori.ReadOnly = True
        Me.lblChkKyori.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkKyori.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkKyori.Size = New System.Drawing.Size(30, 15)
        Me.lblChkKyori.TabIndex = 619
        Me.lblChkKyori.TabStop = False
        Me.lblChkKyori.TabStopSetting = False
        Me.lblChkKyori.TextValue = ""
        Me.lblChkKyori.UseSystemPasswordChar = False
        Me.lblChkKyori.Visible = False
        Me.lblChkKyori.WidthDef = 30
        Me.lblChkKyori.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkCityExtcA
        '
        Me.lblChkCityExtcA.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcA.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcA.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkCityExtcA.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkCityExtcA.CountWrappedLine = False
        Me.lblChkCityExtcA.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkCityExtcA.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCityExtcA.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCityExtcA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCityExtcA.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCityExtcA.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkCityExtcA.HeightDef = 15
        Me.lblChkCityExtcA.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcA.HissuLabelVisible = False
        Me.lblChkCityExtcA.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkCityExtcA.IsByteCheck = 0
        Me.lblChkCityExtcA.IsCalendarCheck = False
        Me.lblChkCityExtcA.IsDakutenCheck = False
        Me.lblChkCityExtcA.IsEisuCheck = False
        Me.lblChkCityExtcA.IsForbiddenWordsCheck = False
        Me.lblChkCityExtcA.IsFullByteCheck = 0
        Me.lblChkCityExtcA.IsHankakuCheck = False
        Me.lblChkCityExtcA.IsHissuCheck = False
        Me.lblChkCityExtcA.IsKanaCheck = False
        Me.lblChkCityExtcA.IsMiddleSpace = False
        Me.lblChkCityExtcA.IsNumericCheck = False
        Me.lblChkCityExtcA.IsSujiCheck = False
        Me.lblChkCityExtcA.IsZenkakuCheck = False
        Me.lblChkCityExtcA.ItemName = ""
        Me.lblChkCityExtcA.LineSpace = 0
        Me.lblChkCityExtcA.Location = New System.Drawing.Point(821, 475)
        Me.lblChkCityExtcA.MaxLength = 0
        Me.lblChkCityExtcA.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkCityExtcA.MaxLineCount = 0
        Me.lblChkCityExtcA.Multiline = False
        Me.lblChkCityExtcA.Name = "lblChkCityExtcA"
        Me.lblChkCityExtcA.ReadOnly = True
        Me.lblChkCityExtcA.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkCityExtcA.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkCityExtcA.Size = New System.Drawing.Size(30, 15)
        Me.lblChkCityExtcA.TabIndex = 620
        Me.lblChkCityExtcA.TabStop = False
        Me.lblChkCityExtcA.TabStopSetting = False
        Me.lblChkCityExtcA.TextValue = ""
        Me.lblChkCityExtcA.UseSystemPasswordChar = False
        Me.lblChkCityExtcA.Visible = False
        Me.lblChkCityExtcA.WidthDef = 30
        Me.lblChkCityExtcA.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkCityExtcB
        '
        Me.lblChkCityExtcB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcB.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkCityExtcB.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkCityExtcB.CountWrappedLine = False
        Me.lblChkCityExtcB.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkCityExtcB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCityExtcB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkCityExtcB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCityExtcB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkCityExtcB.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkCityExtcB.HeightDef = 15
        Me.lblChkCityExtcB.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkCityExtcB.HissuLabelVisible = False
        Me.lblChkCityExtcB.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkCityExtcB.IsByteCheck = 0
        Me.lblChkCityExtcB.IsCalendarCheck = False
        Me.lblChkCityExtcB.IsDakutenCheck = False
        Me.lblChkCityExtcB.IsEisuCheck = False
        Me.lblChkCityExtcB.IsForbiddenWordsCheck = False
        Me.lblChkCityExtcB.IsFullByteCheck = 0
        Me.lblChkCityExtcB.IsHankakuCheck = False
        Me.lblChkCityExtcB.IsHissuCheck = False
        Me.lblChkCityExtcB.IsKanaCheck = False
        Me.lblChkCityExtcB.IsMiddleSpace = False
        Me.lblChkCityExtcB.IsNumericCheck = False
        Me.lblChkCityExtcB.IsSujiCheck = False
        Me.lblChkCityExtcB.IsZenkakuCheck = False
        Me.lblChkCityExtcB.ItemName = ""
        Me.lblChkCityExtcB.LineSpace = 0
        Me.lblChkCityExtcB.Location = New System.Drawing.Point(846, 475)
        Me.lblChkCityExtcB.MaxLength = 0
        Me.lblChkCityExtcB.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkCityExtcB.MaxLineCount = 0
        Me.lblChkCityExtcB.Multiline = False
        Me.lblChkCityExtcB.Name = "lblChkCityExtcB"
        Me.lblChkCityExtcB.ReadOnly = True
        Me.lblChkCityExtcB.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkCityExtcB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkCityExtcB.Size = New System.Drawing.Size(30, 15)
        Me.lblChkCityExtcB.TabIndex = 621
        Me.lblChkCityExtcB.TabStop = False
        Me.lblChkCityExtcB.TabStopSetting = False
        Me.lblChkCityExtcB.TextValue = ""
        Me.lblChkCityExtcB.UseSystemPasswordChar = False
        Me.lblChkCityExtcB.Visible = False
        Me.lblChkCityExtcB.WidthDef = 30
        Me.lblChkCityExtcB.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkWintExtcA
        '
        Me.lblChkWintExtcA.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcA.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcA.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkWintExtcA.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkWintExtcA.CountWrappedLine = False
        Me.lblChkWintExtcA.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkWintExtcA.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkWintExtcA.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkWintExtcA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkWintExtcA.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkWintExtcA.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkWintExtcA.HeightDef = 15
        Me.lblChkWintExtcA.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcA.HissuLabelVisible = False
        Me.lblChkWintExtcA.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkWintExtcA.IsByteCheck = 0
        Me.lblChkWintExtcA.IsCalendarCheck = False
        Me.lblChkWintExtcA.IsDakutenCheck = False
        Me.lblChkWintExtcA.IsEisuCheck = False
        Me.lblChkWintExtcA.IsForbiddenWordsCheck = False
        Me.lblChkWintExtcA.IsFullByteCheck = 0
        Me.lblChkWintExtcA.IsHankakuCheck = False
        Me.lblChkWintExtcA.IsHissuCheck = False
        Me.lblChkWintExtcA.IsKanaCheck = False
        Me.lblChkWintExtcA.IsMiddleSpace = False
        Me.lblChkWintExtcA.IsNumericCheck = False
        Me.lblChkWintExtcA.IsSujiCheck = False
        Me.lblChkWintExtcA.IsZenkakuCheck = False
        Me.lblChkWintExtcA.ItemName = ""
        Me.lblChkWintExtcA.LineSpace = 0
        Me.lblChkWintExtcA.Location = New System.Drawing.Point(877, 475)
        Me.lblChkWintExtcA.MaxLength = 0
        Me.lblChkWintExtcA.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkWintExtcA.MaxLineCount = 0
        Me.lblChkWintExtcA.Multiline = False
        Me.lblChkWintExtcA.Name = "lblChkWintExtcA"
        Me.lblChkWintExtcA.ReadOnly = True
        Me.lblChkWintExtcA.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkWintExtcA.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkWintExtcA.Size = New System.Drawing.Size(30, 15)
        Me.lblChkWintExtcA.TabIndex = 622
        Me.lblChkWintExtcA.TabStop = False
        Me.lblChkWintExtcA.TabStopSetting = False
        Me.lblChkWintExtcA.TextValue = ""
        Me.lblChkWintExtcA.UseSystemPasswordChar = False
        Me.lblChkWintExtcA.Visible = False
        Me.lblChkWintExtcA.WidthDef = 30
        Me.lblChkWintExtcA.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkWintExtcB
        '
        Me.lblChkWintExtcB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcB.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkWintExtcB.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkWintExtcB.CountWrappedLine = False
        Me.lblChkWintExtcB.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkWintExtcB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkWintExtcB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkWintExtcB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkWintExtcB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkWintExtcB.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkWintExtcB.HeightDef = 15
        Me.lblChkWintExtcB.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkWintExtcB.HissuLabelVisible = False
        Me.lblChkWintExtcB.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkWintExtcB.IsByteCheck = 0
        Me.lblChkWintExtcB.IsCalendarCheck = False
        Me.lblChkWintExtcB.IsDakutenCheck = False
        Me.lblChkWintExtcB.IsEisuCheck = False
        Me.lblChkWintExtcB.IsForbiddenWordsCheck = False
        Me.lblChkWintExtcB.IsFullByteCheck = 0
        Me.lblChkWintExtcB.IsHankakuCheck = False
        Me.lblChkWintExtcB.IsHissuCheck = False
        Me.lblChkWintExtcB.IsKanaCheck = False
        Me.lblChkWintExtcB.IsMiddleSpace = False
        Me.lblChkWintExtcB.IsNumericCheck = False
        Me.lblChkWintExtcB.IsSujiCheck = False
        Me.lblChkWintExtcB.IsZenkakuCheck = False
        Me.lblChkWintExtcB.ItemName = ""
        Me.lblChkWintExtcB.LineSpace = 0
        Me.lblChkWintExtcB.Location = New System.Drawing.Point(908, 475)
        Me.lblChkWintExtcB.MaxLength = 0
        Me.lblChkWintExtcB.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkWintExtcB.MaxLineCount = 0
        Me.lblChkWintExtcB.Multiline = False
        Me.lblChkWintExtcB.Name = "lblChkWintExtcB"
        Me.lblChkWintExtcB.ReadOnly = True
        Me.lblChkWintExtcB.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkWintExtcB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkWintExtcB.Size = New System.Drawing.Size(30, 15)
        Me.lblChkWintExtcB.TabIndex = 623
        Me.lblChkWintExtcB.TabStop = False
        Me.lblChkWintExtcB.TabStopSetting = False
        Me.lblChkWintExtcB.TextValue = ""
        Me.lblChkWintExtcB.UseSystemPasswordChar = False
        Me.lblChkWintExtcB.Visible = False
        Me.lblChkWintExtcB.WidthDef = 30
        Me.lblChkWintExtcB.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkRelyExtc
        '
        Me.lblChkRelyExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkRelyExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkRelyExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkRelyExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkRelyExtc.CountWrappedLine = False
        Me.lblChkRelyExtc.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkRelyExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkRelyExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkRelyExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkRelyExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkRelyExtc.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkRelyExtc.HeightDef = 15
        Me.lblChkRelyExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkRelyExtc.HissuLabelVisible = False
        Me.lblChkRelyExtc.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkRelyExtc.IsByteCheck = 0
        Me.lblChkRelyExtc.IsCalendarCheck = False
        Me.lblChkRelyExtc.IsDakutenCheck = False
        Me.lblChkRelyExtc.IsEisuCheck = False
        Me.lblChkRelyExtc.IsForbiddenWordsCheck = False
        Me.lblChkRelyExtc.IsFullByteCheck = 0
        Me.lblChkRelyExtc.IsHankakuCheck = False
        Me.lblChkRelyExtc.IsHissuCheck = False
        Me.lblChkRelyExtc.IsKanaCheck = False
        Me.lblChkRelyExtc.IsMiddleSpace = False
        Me.lblChkRelyExtc.IsNumericCheck = False
        Me.lblChkRelyExtc.IsSujiCheck = False
        Me.lblChkRelyExtc.IsZenkakuCheck = False
        Me.lblChkRelyExtc.ItemName = ""
        Me.lblChkRelyExtc.LineSpace = 0
        Me.lblChkRelyExtc.Location = New System.Drawing.Point(935, 475)
        Me.lblChkRelyExtc.MaxLength = 0
        Me.lblChkRelyExtc.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkRelyExtc.MaxLineCount = 0
        Me.lblChkRelyExtc.Multiline = False
        Me.lblChkRelyExtc.Name = "lblChkRelyExtc"
        Me.lblChkRelyExtc.ReadOnly = True
        Me.lblChkRelyExtc.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkRelyExtc.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkRelyExtc.Size = New System.Drawing.Size(30, 15)
        Me.lblChkRelyExtc.TabIndex = 624
        Me.lblChkRelyExtc.TabStop = False
        Me.lblChkRelyExtc.TabStopSetting = False
        Me.lblChkRelyExtc.TextValue = ""
        Me.lblChkRelyExtc.UseSystemPasswordChar = False
        Me.lblChkRelyExtc.Visible = False
        Me.lblChkRelyExtc.WidthDef = 30
        Me.lblChkRelyExtc.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkInsu
        '
        Me.lblChkInsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkInsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkInsu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkInsu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkInsu.CountWrappedLine = False
        Me.lblChkInsu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkInsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkInsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkInsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkInsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkInsu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkInsu.HeightDef = 15
        Me.lblChkInsu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkInsu.HissuLabelVisible = False
        Me.lblChkInsu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkInsu.IsByteCheck = 0
        Me.lblChkInsu.IsCalendarCheck = False
        Me.lblChkInsu.IsDakutenCheck = False
        Me.lblChkInsu.IsEisuCheck = False
        Me.lblChkInsu.IsForbiddenWordsCheck = False
        Me.lblChkInsu.IsFullByteCheck = 0
        Me.lblChkInsu.IsHankakuCheck = False
        Me.lblChkInsu.IsHissuCheck = False
        Me.lblChkInsu.IsKanaCheck = False
        Me.lblChkInsu.IsMiddleSpace = False
        Me.lblChkInsu.IsNumericCheck = False
        Me.lblChkInsu.IsSujiCheck = False
        Me.lblChkInsu.IsZenkakuCheck = False
        Me.lblChkInsu.ItemName = ""
        Me.lblChkInsu.LineSpace = 0
        Me.lblChkInsu.Location = New System.Drawing.Point(963, 475)
        Me.lblChkInsu.MaxLength = 0
        Me.lblChkInsu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkInsu.MaxLineCount = 0
        Me.lblChkInsu.Multiline = False
        Me.lblChkInsu.Name = "lblChkInsu"
        Me.lblChkInsu.ReadOnly = True
        Me.lblChkInsu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkInsu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkInsu.Size = New System.Drawing.Size(30, 15)
        Me.lblChkInsu.TabIndex = 625
        Me.lblChkInsu.TabStop = False
        Me.lblChkInsu.TabStopSetting = False
        Me.lblChkInsu.TextValue = ""
        Me.lblChkInsu.UseSystemPasswordChar = False
        Me.lblChkInsu.Visible = False
        Me.lblChkInsu.WidthDef = 30
        Me.lblChkInsu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkFrryExtc10kg
        '
        Me.lblChkFrryExtc10kg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtc10kg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtc10kg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkFrryExtc10kg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkFrryExtc10kg.CountWrappedLine = False
        Me.lblChkFrryExtc10kg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkFrryExtc10kg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkFrryExtc10kg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkFrryExtc10kg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkFrryExtc10kg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkFrryExtc10kg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkFrryExtc10kg.HeightDef = 15
        Me.lblChkFrryExtc10kg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtc10kg.HissuLabelVisible = False
        Me.lblChkFrryExtc10kg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkFrryExtc10kg.IsByteCheck = 0
        Me.lblChkFrryExtc10kg.IsCalendarCheck = False
        Me.lblChkFrryExtc10kg.IsDakutenCheck = False
        Me.lblChkFrryExtc10kg.IsEisuCheck = False
        Me.lblChkFrryExtc10kg.IsForbiddenWordsCheck = False
        Me.lblChkFrryExtc10kg.IsFullByteCheck = 0
        Me.lblChkFrryExtc10kg.IsHankakuCheck = False
        Me.lblChkFrryExtc10kg.IsHissuCheck = False
        Me.lblChkFrryExtc10kg.IsKanaCheck = False
        Me.lblChkFrryExtc10kg.IsMiddleSpace = False
        Me.lblChkFrryExtc10kg.IsNumericCheck = False
        Me.lblChkFrryExtc10kg.IsSujiCheck = False
        Me.lblChkFrryExtc10kg.IsZenkakuCheck = False
        Me.lblChkFrryExtc10kg.ItemName = ""
        Me.lblChkFrryExtc10kg.LineSpace = 0
        Me.lblChkFrryExtc10kg.Location = New System.Drawing.Point(726, 454)
        Me.lblChkFrryExtc10kg.MaxLength = 0
        Me.lblChkFrryExtc10kg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkFrryExtc10kg.MaxLineCount = 0
        Me.lblChkFrryExtc10kg.Multiline = False
        Me.lblChkFrryExtc10kg.Name = "lblChkFrryExtc10kg"
        Me.lblChkFrryExtc10kg.ReadOnly = True
        Me.lblChkFrryExtc10kg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkFrryExtc10kg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkFrryExtc10kg.Size = New System.Drawing.Size(30, 15)
        Me.lblChkFrryExtc10kg.TabIndex = 626
        Me.lblChkFrryExtc10kg.TabStop = False
        Me.lblChkFrryExtc10kg.TabStopSetting = False
        Me.lblChkFrryExtc10kg.TextValue = ""
        Me.lblChkFrryExtc10kg.UseSystemPasswordChar = False
        Me.lblChkFrryExtc10kg.Visible = False
        Me.lblChkFrryExtc10kg.WidthDef = 30
        Me.lblChkFrryExtc10kg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkFrryExtcPart
        '
        Me.lblChkFrryExtcPart.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtcPart.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtcPart.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkFrryExtcPart.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkFrryExtcPart.CountWrappedLine = False
        Me.lblChkFrryExtcPart.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkFrryExtcPart.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkFrryExtcPart.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkFrryExtcPart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkFrryExtcPart.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkFrryExtcPart.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkFrryExtcPart.HeightDef = 15
        Me.lblChkFrryExtcPart.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkFrryExtcPart.HissuLabelVisible = False
        Me.lblChkFrryExtcPart.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkFrryExtcPart.IsByteCheck = 0
        Me.lblChkFrryExtcPart.IsCalendarCheck = False
        Me.lblChkFrryExtcPart.IsDakutenCheck = False
        Me.lblChkFrryExtcPart.IsEisuCheck = False
        Me.lblChkFrryExtcPart.IsForbiddenWordsCheck = False
        Me.lblChkFrryExtcPart.IsFullByteCheck = 0
        Me.lblChkFrryExtcPart.IsHankakuCheck = False
        Me.lblChkFrryExtcPart.IsHissuCheck = False
        Me.lblChkFrryExtcPart.IsKanaCheck = False
        Me.lblChkFrryExtcPart.IsMiddleSpace = False
        Me.lblChkFrryExtcPart.IsNumericCheck = False
        Me.lblChkFrryExtcPart.IsSujiCheck = False
        Me.lblChkFrryExtcPart.IsZenkakuCheck = False
        Me.lblChkFrryExtcPart.ItemName = ""
        Me.lblChkFrryExtcPart.LineSpace = 0
        Me.lblChkFrryExtcPart.Location = New System.Drawing.Point(762, 454)
        Me.lblChkFrryExtcPart.MaxLength = 0
        Me.lblChkFrryExtcPart.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkFrryExtcPart.MaxLineCount = 0
        Me.lblChkFrryExtcPart.Multiline = False
        Me.lblChkFrryExtcPart.Name = "lblChkFrryExtcPart"
        Me.lblChkFrryExtcPart.ReadOnly = True
        Me.lblChkFrryExtcPart.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkFrryExtcPart.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkFrryExtcPart.Size = New System.Drawing.Size(30, 15)
        Me.lblChkFrryExtcPart.TabIndex = 627
        Me.lblChkFrryExtcPart.TabStop = False
        Me.lblChkFrryExtcPart.TabStopSetting = False
        Me.lblChkFrryExtcPart.TextValue = ""
        Me.lblChkFrryExtcPart.UseSystemPasswordChar = False
        Me.lblChkFrryExtcPart.Visible = False
        Me.lblChkFrryExtcPart.WidthDef = 30
        Me.lblChkFrryExtcPart.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdStrDate
        '
        Me.imdStrDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdStrDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdStrDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdStrDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdStrDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdStrDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdStrDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdStrDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdStrDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdStrDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdStrDate.HeightDef = 18
        Me.imdStrDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdStrDate.HissuLabelVisible = True
        Me.imdStrDate.Holiday = True
        Me.imdStrDate.IsAfterDateCheck = False
        Me.imdStrDate.IsBeforeDateCheck = False
        Me.imdStrDate.IsHissuCheck = True
        Me.imdStrDate.IsMinDateCheck = "1900/01/01"
        Me.imdStrDate.ItemName = ""
        Me.imdStrDate.Location = New System.Drawing.Point(165, 349)
        Me.imdStrDate.Name = "imdStrDate"
        Me.imdStrDate.Number = CType(10101000000, Long)
        Me.imdStrDate.ReadOnly = False
        Me.imdStrDate.Size = New System.Drawing.Size(118, 18)
        Me.imdStrDate.TabIndex = 628
        Me.imdStrDate.TabStopSetting = True
        Me.imdStrDate.TextValue = ""
        Me.imdStrDate.Value = New Date(CType(0, Long))
        Me.imdStrDate.WidthDef = 118
        '
        'lblChkDataTp
        '
        Me.lblChkDataTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkDataTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkDataTp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkDataTp.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkDataTp.CountWrappedLine = False
        Me.lblChkDataTp.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkDataTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkDataTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkDataTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkDataTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkDataTp.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkDataTp.HeightDef = 15
        Me.lblChkDataTp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkDataTp.HissuLabelVisible = False
        Me.lblChkDataTp.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkDataTp.IsByteCheck = 0
        Me.lblChkDataTp.IsCalendarCheck = False
        Me.lblChkDataTp.IsDakutenCheck = False
        Me.lblChkDataTp.IsEisuCheck = False
        Me.lblChkDataTp.IsForbiddenWordsCheck = False
        Me.lblChkDataTp.IsFullByteCheck = 0
        Me.lblChkDataTp.IsHankakuCheck = False
        Me.lblChkDataTp.IsHissuCheck = False
        Me.lblChkDataTp.IsKanaCheck = False
        Me.lblChkDataTp.IsMiddleSpace = False
        Me.lblChkDataTp.IsNumericCheck = False
        Me.lblChkDataTp.IsSujiCheck = False
        Me.lblChkDataTp.IsZenkakuCheck = False
        Me.lblChkDataTp.ItemName = ""
        Me.lblChkDataTp.LineSpace = 0
        Me.lblChkDataTp.Location = New System.Drawing.Point(659, 475)
        Me.lblChkDataTp.MaxLength = 0
        Me.lblChkDataTp.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkDataTp.MaxLineCount = 0
        Me.lblChkDataTp.Multiline = False
        Me.lblChkDataTp.Name = "lblChkDataTp"
        Me.lblChkDataTp.ReadOnly = True
        Me.lblChkDataTp.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkDataTp.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkDataTp.Size = New System.Drawing.Size(30, 15)
        Me.lblChkDataTp.TabIndex = 629
        Me.lblChkDataTp.TabStop = False
        Me.lblChkDataTp.TabStopSetting = False
        Me.lblChkDataTp.TextValue = ""
        Me.lblChkDataTp.UseSystemPasswordChar = False
        Me.lblChkDataTp.Visible = False
        Me.lblChkDataTp.WidthDef = 30
        Me.lblChkDataTp.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblChkTableTp
        '
        Me.lblChkTableTp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkTableTp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkTableTp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChkTableTp.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChkTableTp.CountWrappedLine = False
        Me.lblChkTableTp.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblChkTableTp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkTableTp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChkTableTp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkTableTp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChkTableTp.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblChkTableTp.HeightDef = 15
        Me.lblChkTableTp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblChkTableTp.HissuLabelVisible = False
        Me.lblChkTableTp.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblChkTableTp.IsByteCheck = 0
        Me.lblChkTableTp.IsCalendarCheck = False
        Me.lblChkTableTp.IsDakutenCheck = False
        Me.lblChkTableTp.IsEisuCheck = False
        Me.lblChkTableTp.IsForbiddenWordsCheck = False
        Me.lblChkTableTp.IsFullByteCheck = 0
        Me.lblChkTableTp.IsHankakuCheck = False
        Me.lblChkTableTp.IsHissuCheck = False
        Me.lblChkTableTp.IsKanaCheck = False
        Me.lblChkTableTp.IsMiddleSpace = False
        Me.lblChkTableTp.IsNumericCheck = False
        Me.lblChkTableTp.IsSujiCheck = False
        Me.lblChkTableTp.IsZenkakuCheck = False
        Me.lblChkTableTp.ItemName = ""
        Me.lblChkTableTp.LineSpace = 0
        Me.lblChkTableTp.Location = New System.Drawing.Point(690, 475)
        Me.lblChkTableTp.MaxLength = 0
        Me.lblChkTableTp.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblChkTableTp.MaxLineCount = 0
        Me.lblChkTableTp.Multiline = False
        Me.lblChkTableTp.Name = "lblChkTableTp"
        Me.lblChkTableTp.ReadOnly = True
        Me.lblChkTableTp.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblChkTableTp.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblChkTableTp.Size = New System.Drawing.Size(30, 15)
        Me.lblChkTableTp.TabIndex = 630
        Me.lblChkTableTp.TabStop = False
        Me.lblChkTableTp.TabStopSetting = False
        Me.lblChkTableTp.TextValue = ""
        Me.lblChkTableTp.UseSystemPasswordChar = False
        Me.lblChkTableTp.Visible = False
        Me.lblChkTableTp.WidthDef = 30
        Me.lblChkTableTp.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblDefAddCnt
        '
        Me.lblDefAddCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDefAddCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDefAddCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDefAddCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDefAddCnt.CountWrappedLine = False
        Me.lblDefAddCnt.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDefAddCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDefAddCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDefAddCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDefAddCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDefAddCnt.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDefAddCnt.HeightDef = 15
        Me.lblDefAddCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDefAddCnt.HissuLabelVisible = False
        Me.lblDefAddCnt.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDefAddCnt.IsByteCheck = 0
        Me.lblDefAddCnt.IsCalendarCheck = False
        Me.lblDefAddCnt.IsDakutenCheck = False
        Me.lblDefAddCnt.IsEisuCheck = False
        Me.lblDefAddCnt.IsForbiddenWordsCheck = False
        Me.lblDefAddCnt.IsFullByteCheck = 0
        Me.lblDefAddCnt.IsHankakuCheck = False
        Me.lblDefAddCnt.IsHissuCheck = False
        Me.lblDefAddCnt.IsKanaCheck = False
        Me.lblDefAddCnt.IsMiddleSpace = False
        Me.lblDefAddCnt.IsNumericCheck = False
        Me.lblDefAddCnt.IsSujiCheck = False
        Me.lblDefAddCnt.IsZenkakuCheck = False
        Me.lblDefAddCnt.ItemName = ""
        Me.lblDefAddCnt.LineSpace = 0
        Me.lblDefAddCnt.Location = New System.Drawing.Point(1088, 475)
        Me.lblDefAddCnt.MaxLength = 0
        Me.lblDefAddCnt.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDefAddCnt.MaxLineCount = 0
        Me.lblDefAddCnt.Multiline = False
        Me.lblDefAddCnt.Name = "lblDefAddCnt"
        Me.lblDefAddCnt.ReadOnly = True
        Me.lblDefAddCnt.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDefAddCnt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDefAddCnt.Size = New System.Drawing.Size(30, 15)
        Me.lblDefAddCnt.TabIndex = 631
        Me.lblDefAddCnt.TabStop = False
        Me.lblDefAddCnt.TabStopSetting = False
        Me.lblDefAddCnt.TextValue = ""
        Me.lblDefAddCnt.UseSystemPasswordChar = False
        Me.lblDefAddCnt.Visible = False
        Me.lblDefAddCnt.WidthDef = 30
        Me.lblDefAddCnt.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'sprDetail3
        '
        Me.sprDetail3.AccessibleDescription = ""
        Me.sprDetail3.AllowUserZoom = False
        Me.sprDetail3.AutoImeMode = False
        Me.sprDetail3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail3.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetail3.CellClickEventArgs = Nothing
        Me.sprDetail3.CheckToCheckBox = True
        Me.sprDetail3.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetail3.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail3.EditModeReplace = True
        Me.sprDetail3.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprDetail3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail3.HeightDef = 183
        Me.sprDetail3.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail3.HorizontalScrollBar.Name = ""
        Me.sprDetail3.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprDetail3.KeyboardCheckBoxOn = False
        Me.sprDetail3.Location = New System.Drawing.Point(15, 496)
        Me.sprDetail3.Name = "sprDetail3"
        Me.sprDetail3.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail3.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail3_Sheet1})
        Me.sprDetail3.Size = New System.Drawing.Size(604, 183)
        Me.sprDetail3.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDetail3.SortColumn = True
        Me.sprDetail3.SpanColumnLock = True
        Me.sprDetail3.SpreadDoubleClicked = False
        Me.sprDetail3.TabIndex = 632
        Me.sprDetail3.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail3.TextValue = Nothing
        Me.sprDetail3.UseGrouping = False
        Me.sprDetail3.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail3.VerticalScrollBar.Name = ""
        Me.sprDetail3.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprDetail3.WidthDef = 604
        sprDetail3_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail3_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDetail3_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDetail3.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail3_InputMapWhenFocusedNormal)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDetail3_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDetail3.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail3_InputMapWhenAncestorOfFocusedNormal)
        '
        'sprDetail3_Sheet1
        '
        Me.sprDetail3_Sheet1.Reset()
        Me.sprDetail3_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprDetail3_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprDetail3_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprDetail3_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprDetail3_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprDetail3_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprDetail3_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail3_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail3_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprDetail3_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprDetail3_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDetail3_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.sprDetail3_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.DefaultStyle.Locked = False
        Me.sprDetail3_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprDetail3_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprDetail3_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprDetail3_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprDetail3_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail3_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail3_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail3_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprDetail3_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprDetail3_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail3_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprDetail3_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprDetail3_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.Rows.Default.Height = 18.0!
        Me.sprDetail3_Sheet1.Rows.Default.Resizable = False
        Me.sprDetail3_Sheet1.Rows.Default.Visible = True
        Me.sprDetail3_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail3_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprDetail3_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprDetail3_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail3_Sheet1.SheetCornerStyle.Locked = True
        Me.sprDetail3_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail3_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprDetail3_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail3_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail3_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'SheetView2
        '
        Me.SheetView2.Reset()
        Me.SheetView2.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.SheetView2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.SheetView2.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnFooter.DefaultStyle.Locked = False
        Me.SheetView2.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.SheetView2.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnFooterSheetCornerStyle.Locked = False
        Me.SheetView2.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.SheetView2.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SheetView2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.ColumnHeader.DefaultStyle.Locked = False
        Me.SheetView2.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.SheetView2.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType4.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.SheetView2.Columns.Get(1).CellType = TextCellType4
        Me.SheetView2.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.DefaultStyle.Locked = False
        Me.SheetView2.DefaultStyle.Parent = "DataAreaDefault"
        Me.SheetView2.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.FilterBar.DefaultStyle.Locked = False
        Me.SheetView2.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.SheetView2.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.FilterBarHeaderStyle.Locked = False
        Me.SheetView2.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.SheetView2.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.SheetView2.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.SheetView2.RowHeader.Columns.Default.Resizable = True
        Me.SheetView2.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.RowHeader.Columns.Default.Width = -1.0!
        Me.SheetView2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.RowHeader.DefaultStyle.Locked = False
        Me.SheetView2.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.SheetView2.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.RowHeader.Rows.Default.Resizable = False
        Me.SheetView2.RowHeader.Rows.Default.Visible = True
        Me.SheetView2.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.Rows.Default.Height = 18.0!
        Me.SheetView2.Rows.Default.Resizable = False
        Me.SheetView2.Rows.Default.Visible = True
        Me.SheetView2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SheetView2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SheetView2.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.SheetView2.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.SheetView2.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.SheetView2.SheetCornerStyle.Locked = True
        Me.SheetView2.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.SheetView2.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.SheetView2.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.SheetView2.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.SheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'LMM550F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM550F"
        Me.Text = "【LMM550】 下払いタリフマスタメンテ"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail2_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail3_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SheetView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblShiharaiTariffCd2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtShiharaiTariffCd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblShiharaiTariffRem As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtShiharaiTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail2 As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblShiharaiTariffCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtShiharaiTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblStrDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTableTp As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDataTp As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnColAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnColDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnLock As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnUnLock As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbDataTp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbTableTp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblMaxEda As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblAddCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblExcelDt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkCarTp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkInsu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkRelyExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkWintExtcB As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkWintExtcA As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkCityExtcB As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkCityExtcA As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkKyori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblWtLv As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkFrryExtcPart As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkFrryExtc10kg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdStrDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblChkTableTp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblChkDataTp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDefAddCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents sprDetail2_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents sprDetail_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents sprDetail3 As Win.Spread.LMSpread
    Friend WithEvents sprDetail3_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents SheetView2 As FarPoint.Win.Spread.SheetView
End Class
