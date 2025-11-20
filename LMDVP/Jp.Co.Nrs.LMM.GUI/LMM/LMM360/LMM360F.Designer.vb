<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM360F
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
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.sprDetail_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeiqCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numkeisanTlgk = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePtnCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtPtnCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGroupKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbGroupKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleSeiqkmkCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeiqkmkCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleKeisanTlgk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNebikiRt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNebikiGk = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNebikiRt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleNebikiGt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtTekiyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTekiyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSeiqNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSeiqkmkNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTcustBpNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTcustBpCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtTcustBpCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeiqkmkCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeiqkmkCdS = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtSeiqkmkCdS)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqkmkCdS)
        Me.pnlViewAria.Controls.Add(Me.lblTcustBpNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTcustBpCd)
        Me.pnlViewAria.Controls.Add(Me.txtTcustBpCd)
        Me.pnlViewAria.Controls.Add(Me.lblSeiqkmkNm)
        Me.pnlViewAria.Controls.Add(Me.lblSeiqNm)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTekiyo)
        Me.pnlViewAria.Controls.Add(Me.txtTekiyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNebikiGt)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNebikiRt)
        Me.pnlViewAria.Controls.Add(Me.numNebikiGk)
        Me.pnlViewAria.Controls.Add(Me.numNebikiRt)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKeisanTlgk)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqkmkCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqkmkCd)
        Me.pnlViewAria.Controls.Add(Me.cmbGroupKbn)
        Me.pnlViewAria.Controls.Add(Me.lblTitleGroupKbn)
        Me.pnlViewAria.Controls.Add(Me.txtPtnCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePtnCd)
        Me.pnlViewAria.Controls.Add(Me.numkeisanTlgk)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqCd)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
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
        Me.sprDetail.HeightDef = 333
        Me.sprDetail.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.HorizontalScrollBar.Name = ""
        Me.sprDetail.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 30)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail_Sheet1})
        Me.sprDetail.Size = New System.Drawing.Size(1248, 333)
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
        Me.sprDetail.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprDetail.WidthDef = 1248
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
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.CSEStopEditing)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.OemMinus, CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.AutoSum)
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
        TextCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDetail_Sheet1.Columns.Get(1).CellType = TextCellType1
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1116, 479)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 320
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
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1066, 480)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel10.TabIndex = 319
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 49
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1116, 458)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 318
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1066, 458)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel9.TabIndex = 317
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 49
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1116, 437)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 316
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
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1066, 438)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel8.TabIndex = 315
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 49
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1116, 416)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 314
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
        Me.lblSituation.Location = New System.Drawing.Point(1123, 383)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 313
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
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1066, 416)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel7.TabIndex = 312
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 49
        '
        'txtSeiqtoCd
        '
        Me.txtSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoCd.CountWrappedLine = False
        Me.txtSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoCd.HeightDef = 18
        Me.txtSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoCd.HissuLabelVisible = True
        Me.txtSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSeiqtoCd.IsByteCheck = 7
        Me.txtSeiqtoCd.IsCalendarCheck = False
        Me.txtSeiqtoCd.IsDakutenCheck = False
        Me.txtSeiqtoCd.IsEisuCheck = False
        Me.txtSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtSeiqtoCd.IsFullByteCheck = 0
        Me.txtSeiqtoCd.IsHankakuCheck = False
        Me.txtSeiqtoCd.IsHissuCheck = True
        Me.txtSeiqtoCd.IsKanaCheck = False
        Me.txtSeiqtoCd.IsMiddleSpace = False
        Me.txtSeiqtoCd.IsNumericCheck = False
        Me.txtSeiqtoCd.IsSujiCheck = False
        Me.txtSeiqtoCd.IsZenkakuCheck = False
        Me.txtSeiqtoCd.ItemName = ""
        Me.txtSeiqtoCd.LineSpace = 0
        Me.txtSeiqtoCd.Location = New System.Drawing.Point(189, 404)
        Me.txtSeiqtoCd.MaxLength = 7
        Me.txtSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoCd.MaxLineCount = 0
        Me.txtSeiqtoCd.Multiline = False
        Me.txtSeiqtoCd.Name = "txtSeiqtoCd"
        Me.txtSeiqtoCd.ReadOnly = False
        Me.txtSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoCd.Size = New System.Drawing.Size(82, 18)
        Me.txtSeiqtoCd.TabIndex = 459
        Me.txtSeiqtoCd.TabStopSetting = True
        Me.txtSeiqtoCd.TextValue = "XXXXXXX"
        Me.txtSeiqtoCd.UseSystemPasswordChar = False
        Me.txtSeiqtoCd.WidthDef = 82
        Me.txtSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeiqCd
        '
        Me.lblTitleSeiqCd.AutoSize = True
        Me.lblTitleSeiqCd.AutoSizeDef = True
        Me.lblTitleSeiqCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqCd.EnableStatus = False
        Me.lblTitleSeiqCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqCd.HeightDef = 13
        Me.lblTitleSeiqCd.Location = New System.Drawing.Point(138, 406)
        Me.lblTitleSeiqCd.Name = "lblTitleSeiqCd"
        Me.lblTitleSeiqCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeiqCd.TabIndex = 458
        Me.lblTitleSeiqCd.Text = "請求先"
        Me.lblTitleSeiqCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqCd.TextValue = "請求先"
        Me.lblTitleSeiqCd.WidthDef = 49
        '
        'numkeisanTlgk
        '
        Me.numkeisanTlgk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numkeisanTlgk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numkeisanTlgk.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numkeisanTlgk.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numkeisanTlgk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numkeisanTlgk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numkeisanTlgk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numkeisanTlgk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numkeisanTlgk.HeightDef = 18
        Me.numkeisanTlgk.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numkeisanTlgk.HissuLabelVisible = False
        Me.numkeisanTlgk.IsHissuCheck = False
        Me.numkeisanTlgk.IsRangeCheck = False
        Me.numkeisanTlgk.ItemName = ""
        Me.numkeisanTlgk.Location = New System.Drawing.Point(189, 512)
        Me.numkeisanTlgk.Name = "numkeisanTlgk"
        Me.numkeisanTlgk.ReadOnly = False
        Me.numkeisanTlgk.Size = New System.Drawing.Size(149, 18)
        Me.numkeisanTlgk.TabIndex = 493
        Me.numkeisanTlgk.TabStopSetting = True
        Me.numkeisanTlgk.Tag = ""
        Me.numkeisanTlgk.TextValue = "0"
        Me.numkeisanTlgk.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numkeisanTlgk.WidthDef = 149
        '
        'lblTitlePtnCd
        '
        Me.lblTitlePtnCd.AutoSize = True
        Me.lblTitlePtnCd.AutoSizeDef = True
        Me.lblTitlePtnCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePtnCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePtnCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePtnCd.EnableStatus = False
        Me.lblTitlePtnCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePtnCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePtnCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePtnCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePtnCd.HeightDef = 13
        Me.lblTitlePtnCd.Location = New System.Drawing.Point(54, 428)
        Me.lblTitlePtnCd.Name = "lblTitlePtnCd"
        Me.lblTitlePtnCd.Size = New System.Drawing.Size(133, 13)
        Me.lblTitlePtnCd.TabIndex = 494
        Me.lblTitlePtnCd.Text = "請求パターンコード"
        Me.lblTitlePtnCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePtnCd.TextValue = "請求パターンコード"
        Me.lblTitlePtnCd.WidthDef = 133
        '
        'txtPtnCd
        '
        Me.txtPtnCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPtnCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPtnCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPtnCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPtnCd.CountWrappedLine = False
        Me.txtPtnCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtPtnCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPtnCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPtnCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPtnCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPtnCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtPtnCd.HeightDef = 18
        Me.txtPtnCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtPtnCd.HissuLabelVisible = True
        Me.txtPtnCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtPtnCd.IsByteCheck = 2
        Me.txtPtnCd.IsCalendarCheck = False
        Me.txtPtnCd.IsDakutenCheck = False
        Me.txtPtnCd.IsEisuCheck = False
        Me.txtPtnCd.IsForbiddenWordsCheck = False
        Me.txtPtnCd.IsFullByteCheck = 0
        Me.txtPtnCd.IsHankakuCheck = False
        Me.txtPtnCd.IsHissuCheck = True
        Me.txtPtnCd.IsKanaCheck = False
        Me.txtPtnCd.IsMiddleSpace = False
        Me.txtPtnCd.IsNumericCheck = False
        Me.txtPtnCd.IsSujiCheck = False
        Me.txtPtnCd.IsZenkakuCheck = False
        Me.txtPtnCd.ItemName = ""
        Me.txtPtnCd.LineSpace = 0
        Me.txtPtnCd.Location = New System.Drawing.Point(189, 426)
        Me.txtPtnCd.MaxLength = 2
        Me.txtPtnCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtPtnCd.MaxLineCount = 0
        Me.txtPtnCd.Multiline = False
        Me.txtPtnCd.Name = "txtPtnCd"
        Me.txtPtnCd.ReadOnly = False
        Me.txtPtnCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtPtnCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPtnCd.Size = New System.Drawing.Size(40, 18)
        Me.txtPtnCd.TabIndex = 495
        Me.txtPtnCd.TabStopSetting = True
        Me.txtPtnCd.TextValue = ""
        Me.txtPtnCd.UseSystemPasswordChar = False
        Me.txtPtnCd.WidthDef = 40
        Me.txtPtnCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGroupKbn
        '
        Me.lblTitleGroupKbn.AutoSize = True
        Me.lblTitleGroupKbn.AutoSizeDef = True
        Me.lblTitleGroupKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroupKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroupKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGroupKbn.EnableStatus = False
        Me.lblTitleGroupKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroupKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroupKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroupKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroupKbn.HeightDef = 13
        Me.lblTitleGroupKbn.Location = New System.Drawing.Point(26, 473)
        Me.lblTitleGroupKbn.Name = "lblTitleGroupKbn"
        Me.lblTitleGroupKbn.Size = New System.Drawing.Size(161, 13)
        Me.lblTitleGroupKbn.TabIndex = 496
        Me.lblTitleGroupKbn.Text = "請求グループコード区分"
        Me.lblTitleGroupKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGroupKbn.TextValue = "請求グループコード区分"
        Me.lblTitleGroupKbn.WidthDef = 161
        '
        'cmbGroupKbn
        '
        Me.cmbGroupKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbGroupKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbGroupKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbGroupKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbGroupKbn.DataCode = "S024"
        Me.cmbGroupKbn.DataSource = Nothing
        Me.cmbGroupKbn.DisplayMember = Nothing
        Me.cmbGroupKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbGroupKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbGroupKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbGroupKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbGroupKbn.HeightDef = 18
        Me.cmbGroupKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbGroupKbn.HissuLabelVisible = True
        Me.cmbGroupKbn.InsertWildCard = True
        Me.cmbGroupKbn.IsForbiddenWordsCheck = False
        Me.cmbGroupKbn.IsHissuCheck = True
        Me.cmbGroupKbn.ItemName = ""
        Me.cmbGroupKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbGroupKbn.Location = New System.Drawing.Point(189, 470)
        Me.cmbGroupKbn.Name = "cmbGroupKbn"
        Me.cmbGroupKbn.ReadOnly = False
        Me.cmbGroupKbn.SelectedIndex = -1
        Me.cmbGroupKbn.SelectedItem = Nothing
        Me.cmbGroupKbn.SelectedText = ""
        Me.cmbGroupKbn.SelectedValue = ""
        Me.cmbGroupKbn.Size = New System.Drawing.Size(110, 18)
        Me.cmbGroupKbn.TabIndex = 600
        Me.cmbGroupKbn.TabStopSetting = True
        Me.cmbGroupKbn.TextValue = ""
        Me.cmbGroupKbn.Value1 = Nothing
        Me.cmbGroupKbn.Value2 = Nothing
        Me.cmbGroupKbn.Value3 = Nothing
        Me.cmbGroupKbn.ValueMember = Nothing
        Me.cmbGroupKbn.WidthDef = 110
        '
        'lblTitleSeiqkmkCd
        '
        Me.lblTitleSeiqkmkCd.AutoSize = True
        Me.lblTitleSeiqkmkCd.AutoSizeDef = True
        Me.lblTitleSeiqkmkCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqkmkCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqkmkCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqkmkCd.EnableStatus = False
        Me.lblTitleSeiqkmkCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqkmkCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqkmkCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqkmkCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqkmkCd.HeightDef = 13
        Me.lblTitleSeiqkmkCd.Location = New System.Drawing.Point(82, 494)
        Me.lblTitleSeiqkmkCd.Name = "lblTitleSeiqkmkCd"
        Me.lblTitleSeiqkmkCd.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSeiqkmkCd.TabIndex = 601
        Me.lblTitleSeiqkmkCd.Text = "請求項目コード"
        Me.lblTitleSeiqkmkCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqkmkCd.TextValue = "請求項目コード"
        Me.lblTitleSeiqkmkCd.WidthDef = 105
        '
        'txtSeiqkmkCd
        '
        Me.txtSeiqkmkCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqkmkCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqkmkCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqkmkCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqkmkCd.CountWrappedLine = False
        Me.txtSeiqkmkCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqkmkCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqkmkCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqkmkCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqkmkCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqkmkCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqkmkCd.HeightDef = 18
        Me.txtSeiqkmkCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqkmkCd.HissuLabelVisible = True
        Me.txtSeiqkmkCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtSeiqkmkCd.IsByteCheck = 2
        Me.txtSeiqkmkCd.IsCalendarCheck = False
        Me.txtSeiqkmkCd.IsDakutenCheck = False
        Me.txtSeiqkmkCd.IsEisuCheck = False
        Me.txtSeiqkmkCd.IsForbiddenWordsCheck = False
        Me.txtSeiqkmkCd.IsFullByteCheck = 0
        Me.txtSeiqkmkCd.IsHankakuCheck = False
        Me.txtSeiqkmkCd.IsHissuCheck = True
        Me.txtSeiqkmkCd.IsKanaCheck = False
        Me.txtSeiqkmkCd.IsMiddleSpace = False
        Me.txtSeiqkmkCd.IsNumericCheck = False
        Me.txtSeiqkmkCd.IsSujiCheck = False
        Me.txtSeiqkmkCd.IsZenkakuCheck = False
        Me.txtSeiqkmkCd.ItemName = ""
        Me.txtSeiqkmkCd.LineSpace = 0
        Me.txtSeiqkmkCd.Location = New System.Drawing.Point(189, 491)
        Me.txtSeiqkmkCd.MaxLength = 2
        Me.txtSeiqkmkCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqkmkCd.MaxLineCount = 0
        Me.txtSeiqkmkCd.Multiline = False
        Me.txtSeiqkmkCd.Name = "txtSeiqkmkCd"
        Me.txtSeiqkmkCd.ReadOnly = False
        Me.txtSeiqkmkCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqkmkCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqkmkCd.Size = New System.Drawing.Size(40, 18)
        Me.txtSeiqkmkCd.TabIndex = 602
        Me.txtSeiqkmkCd.TabStopSetting = True
        Me.txtSeiqkmkCd.TextValue = ""
        Me.txtSeiqkmkCd.UseSystemPasswordChar = False
        Me.txtSeiqkmkCd.WidthDef = 40
        Me.txtSeiqkmkCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleKeisanTlgk
        '
        Me.lblTitleKeisanTlgk.AutoSize = True
        Me.lblTitleKeisanTlgk.AutoSizeDef = True
        Me.lblTitleKeisanTlgk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeisanTlgk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeisanTlgk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKeisanTlgk.EnableStatus = False
        Me.lblTitleKeisanTlgk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeisanTlgk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeisanTlgk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeisanTlgk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeisanTlgk.HeightDef = 13
        Me.lblTitleKeisanTlgk.Location = New System.Drawing.Point(138, 515)
        Me.lblTitleKeisanTlgk.Name = "lblTitleKeisanTlgk"
        Me.lblTitleKeisanTlgk.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleKeisanTlgk.TabIndex = 605
        Me.lblTitleKeisanTlgk.Text = "計算額"
        Me.lblTitleKeisanTlgk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKeisanTlgk.TextValue = "計算額"
        Me.lblTitleKeisanTlgk.WidthDef = 49
        '
        'numNebikiRt
        '
        Me.numNebikiRt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiRt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiRt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRt.HeightDef = 18
        Me.numNebikiRt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiRt.HissuLabelVisible = False
        Me.numNebikiRt.IsHissuCheck = False
        Me.numNebikiRt.IsRangeCheck = False
        Me.numNebikiRt.ItemName = ""
        Me.numNebikiRt.Location = New System.Drawing.Point(189, 533)
        Me.numNebikiRt.Name = "numNebikiRt"
        Me.numNebikiRt.ReadOnly = False
        Me.numNebikiRt.Size = New System.Drawing.Size(149, 18)
        Me.numNebikiRt.TabIndex = 606
        Me.numNebikiRt.TabStopSetting = True
        Me.numNebikiRt.Tag = ""
        Me.numNebikiRt.TextValue = "0"
        Me.numNebikiRt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiRt.WidthDef = 149
        '
        'numNebikiGk
        '
        Me.numNebikiGk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGk.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiGk.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiGk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGk.HeightDef = 18
        Me.numNebikiGk.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiGk.HissuLabelVisible = False
        Me.numNebikiGk.IsHissuCheck = False
        Me.numNebikiGk.IsRangeCheck = False
        Me.numNebikiGk.ItemName = ""
        Me.numNebikiGk.Location = New System.Drawing.Point(189, 554)
        Me.numNebikiGk.Name = "numNebikiGk"
        Me.numNebikiGk.ReadOnly = False
        Me.numNebikiGk.Size = New System.Drawing.Size(149, 18)
        Me.numNebikiGk.TabIndex = 607
        Me.numNebikiGk.TabStopSetting = True
        Me.numNebikiGk.Tag = ""
        Me.numNebikiGk.TextValue = "0"
        Me.numNebikiGk.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiGk.WidthDef = 149
        '
        'lblTitleNebikiRt
        '
        Me.lblTitleNebikiRt.AutoSize = True
        Me.lblTitleNebikiRt.AutoSizeDef = True
        Me.lblTitleNebikiRt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiRt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiRt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNebikiRt.EnableStatus = False
        Me.lblTitleNebikiRt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiRt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiRt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiRt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiRt.HeightDef = 13
        Me.lblTitleNebikiRt.Location = New System.Drawing.Point(138, 536)
        Me.lblTitleNebikiRt.Name = "lblTitleNebikiRt"
        Me.lblTitleNebikiRt.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNebikiRt.TabIndex = 608
        Me.lblTitleNebikiRt.Text = "値引率"
        Me.lblTitleNebikiRt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNebikiRt.TextValue = "値引率"
        Me.lblTitleNebikiRt.WidthDef = 49
        '
        'lblTitleNebikiGt
        '
        Me.lblTitleNebikiGt.AutoSize = True
        Me.lblTitleNebikiGt.AutoSizeDef = True
        Me.lblTitleNebikiGt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiGt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiGt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNebikiGt.EnableStatus = False
        Me.lblTitleNebikiGt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiGt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiGt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiGt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiGt.HeightDef = 13
        Me.lblTitleNebikiGt.Location = New System.Drawing.Point(110, 557)
        Me.lblTitleNebikiGt.Name = "lblTitleNebikiGt"
        Me.lblTitleNebikiGt.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleNebikiGt.TabIndex = 609
        Me.lblTitleNebikiGt.Text = "固定値引額"
        Me.lblTitleNebikiGt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNebikiGt.TextValue = "固定値引額"
        Me.lblTitleNebikiGt.WidthDef = 77
        '
        'txtTekiyo
        '
        Me.txtTekiyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTekiyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTekiyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTekiyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTekiyo.CountWrappedLine = False
        Me.txtTekiyo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTekiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTekiyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTekiyo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTekiyo.HeightDef = 18
        Me.txtTekiyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTekiyo.HissuLabelVisible = False
        Me.txtTekiyo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtTekiyo.IsByteCheck = 60
        Me.txtTekiyo.IsCalendarCheck = False
        Me.txtTekiyo.IsDakutenCheck = False
        Me.txtTekiyo.IsEisuCheck = False
        Me.txtTekiyo.IsForbiddenWordsCheck = False
        Me.txtTekiyo.IsFullByteCheck = 0
        Me.txtTekiyo.IsHankakuCheck = False
        Me.txtTekiyo.IsHissuCheck = False
        Me.txtTekiyo.IsKanaCheck = False
        Me.txtTekiyo.IsMiddleSpace = False
        Me.txtTekiyo.IsNumericCheck = False
        Me.txtTekiyo.IsSujiCheck = False
        Me.txtTekiyo.IsZenkakuCheck = False
        Me.txtTekiyo.ItemName = ""
        Me.txtTekiyo.LineSpace = 0
        Me.txtTekiyo.Location = New System.Drawing.Point(189, 575)
        Me.txtTekiyo.MaxLength = 60
        Me.txtTekiyo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTekiyo.MaxLineCount = 0
        Me.txtTekiyo.Multiline = False
        Me.txtTekiyo.Name = "txtTekiyo"
        Me.txtTekiyo.ReadOnly = False
        Me.txtTekiyo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTekiyo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTekiyo.Size = New System.Drawing.Size(452, 18)
        Me.txtTekiyo.TabIndex = 610
        Me.txtTekiyo.TabStopSetting = True
        Me.txtTekiyo.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtTekiyo.UseSystemPasswordChar = False
        Me.txtTekiyo.WidthDef = 452
        Me.txtTekiyo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTekiyo
        '
        Me.lblTitleTekiyo.AutoSize = True
        Me.lblTitleTekiyo.AutoSizeDef = True
        Me.lblTitleTekiyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTekiyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTekiyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTekiyo.EnableStatus = False
        Me.lblTitleTekiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTekiyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTekiyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTekiyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTekiyo.HeightDef = 13
        Me.lblTitleTekiyo.Location = New System.Drawing.Point(152, 578)
        Me.lblTitleTekiyo.Name = "lblTitleTekiyo"
        Me.lblTitleTekiyo.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleTekiyo.TabIndex = 611
        Me.lblTitleTekiyo.Text = "摘要"
        Me.lblTitleTekiyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTekiyo.TextValue = "摘要"
        Me.lblTitleTekiyo.WidthDef = 35
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1116, 521)
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1116, 500)
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(189, 383)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 615
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(138, 385)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 614
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblSeiqNm
        '
        Me.lblSeiqNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeiqNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeiqNm.CountWrappedLine = False
        Me.lblSeiqNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeiqNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeiqNm.HeightDef = 18
        Me.lblSeiqNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.HissuLabelVisible = True
        Me.lblSeiqNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeiqNm.IsByteCheck = 125
        Me.lblSeiqNm.IsCalendarCheck = False
        Me.lblSeiqNm.IsDakutenCheck = False
        Me.lblSeiqNm.IsEisuCheck = False
        Me.lblSeiqNm.IsForbiddenWordsCheck = False
        Me.lblSeiqNm.IsFullByteCheck = 0
        Me.lblSeiqNm.IsHankakuCheck = False
        Me.lblSeiqNm.IsHissuCheck = True
        Me.lblSeiqNm.IsKanaCheck = False
        Me.lblSeiqNm.IsMiddleSpace = False
        Me.lblSeiqNm.IsNumericCheck = False
        Me.lblSeiqNm.IsSujiCheck = False
        Me.lblSeiqNm.IsZenkakuCheck = False
        Me.lblSeiqNm.ItemName = ""
        Me.lblSeiqNm.LineSpace = 0
        Me.lblSeiqNm.Location = New System.Drawing.Point(255, 404)
        Me.lblSeiqNm.MaxLength = 125
        Me.lblSeiqNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeiqNm.MaxLineCount = 0
        Me.lblSeiqNm.Multiline = False
        Me.lblSeiqNm.Name = "lblSeiqNm"
        Me.lblSeiqNm.ReadOnly = True
        Me.lblSeiqNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeiqNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeiqNm.Size = New System.Drawing.Size(611, 18)
        Me.lblSeiqNm.TabIndex = 616
        Me.lblSeiqNm.TabStop = False
        Me.lblSeiqNm.TabStopSetting = False
        Me.lblSeiqNm.TextValue = ""
        Me.lblSeiqNm.UseSystemPasswordChar = False
        Me.lblSeiqNm.WidthDef = 611
        Me.lblSeiqNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSeiqkmkNm
        '
        Me.lblSeiqkmkNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqkmkNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqkmkNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeiqkmkNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeiqkmkNm.CountWrappedLine = False
        Me.lblSeiqkmkNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeiqkmkNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqkmkNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqkmkNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqkmkNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqkmkNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeiqkmkNm.HeightDef = 18
        Me.lblSeiqkmkNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqkmkNm.HissuLabelVisible = True
        Me.lblSeiqkmkNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeiqkmkNm.IsByteCheck = 125
        Me.lblSeiqkmkNm.IsCalendarCheck = False
        Me.lblSeiqkmkNm.IsDakutenCheck = False
        Me.lblSeiqkmkNm.IsEisuCheck = False
        Me.lblSeiqkmkNm.IsForbiddenWordsCheck = False
        Me.lblSeiqkmkNm.IsFullByteCheck = 0
        Me.lblSeiqkmkNm.IsHankakuCheck = False
        Me.lblSeiqkmkNm.IsHissuCheck = True
        Me.lblSeiqkmkNm.IsKanaCheck = False
        Me.lblSeiqkmkNm.IsMiddleSpace = False
        Me.lblSeiqkmkNm.IsNumericCheck = False
        Me.lblSeiqkmkNm.IsSujiCheck = False
        Me.lblSeiqkmkNm.IsZenkakuCheck = False
        Me.lblSeiqkmkNm.ItemName = ""
        Me.lblSeiqkmkNm.LineSpace = 0
        Me.lblSeiqkmkNm.Location = New System.Drawing.Point(213, 491)
        Me.lblSeiqkmkNm.MaxLength = 125
        Me.lblSeiqkmkNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeiqkmkNm.MaxLineCount = 0
        Me.lblSeiqkmkNm.Multiline = False
        Me.lblSeiqkmkNm.Name = "lblSeiqkmkNm"
        Me.lblSeiqkmkNm.ReadOnly = True
        Me.lblSeiqkmkNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeiqkmkNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeiqkmkNm.Size = New System.Drawing.Size(313, 18)
        Me.lblSeiqkmkNm.TabIndex = 617
        Me.lblSeiqkmkNm.TabStop = False
        Me.lblSeiqkmkNm.TabStopSetting = False
        Me.lblSeiqkmkNm.TextValue = "Ｎ－－－－－－－－４０－－－－－－－－Ｎ"
        Me.lblSeiqkmkNm.UseSystemPasswordChar = False
        Me.lblSeiqkmkNm.WidthDef = 313
        Me.lblSeiqkmkNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTcustBpNm
        '
        Me.lblTcustBpNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTcustBpNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTcustBpNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTcustBpNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTcustBpNm.CountWrappedLine = False
        Me.lblTcustBpNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTcustBpNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTcustBpNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTcustBpNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTcustBpNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTcustBpNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTcustBpNm.HeightDef = 18
        Me.lblTcustBpNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTcustBpNm.HissuLabelVisible = True
        Me.lblTcustBpNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTcustBpNm.IsByteCheck = 0
        Me.lblTcustBpNm.IsCalendarCheck = False
        Me.lblTcustBpNm.IsDakutenCheck = False
        Me.lblTcustBpNm.IsEisuCheck = False
        Me.lblTcustBpNm.IsForbiddenWordsCheck = False
        Me.lblTcustBpNm.IsFullByteCheck = 0
        Me.lblTcustBpNm.IsHankakuCheck = False
        Me.lblTcustBpNm.IsHissuCheck = True
        Me.lblTcustBpNm.IsKanaCheck = False
        Me.lblTcustBpNm.IsMiddleSpace = False
        Me.lblTcustBpNm.IsNumericCheck = False
        Me.lblTcustBpNm.IsSujiCheck = False
        Me.lblTcustBpNm.IsZenkakuCheck = False
        Me.lblTcustBpNm.ItemName = ""
        Me.lblTcustBpNm.LineSpace = 0
        Me.lblTcustBpNm.Location = New System.Drawing.Point(266, 448)
        Me.lblTcustBpNm.MaxLength = 0
        Me.lblTcustBpNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTcustBpNm.MaxLineCount = 0
        Me.lblTcustBpNm.Multiline = False
        Me.lblTcustBpNm.Name = "lblTcustBpNm"
        Me.lblTcustBpNm.ReadOnly = True
        Me.lblTcustBpNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTcustBpNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTcustBpNm.Size = New System.Drawing.Size(600, 18)
        Me.lblTcustBpNm.TabIndex = 620
        Me.lblTcustBpNm.TabStop = False
        Me.lblTcustBpNm.TabStopSetting = False
        Me.lblTcustBpNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblTcustBpNm.UseSystemPasswordChar = False
        Me.lblTcustBpNm.WidthDef = 600
        Me.lblTcustBpNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTcustBpCd
        '
        Me.lblTitleTcustBpCd.AutoSizeDef = False
        Me.lblTitleTcustBpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTcustBpCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTcustBpCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTcustBpCd.EnableStatus = False
        Me.lblTitleTcustBpCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTcustBpCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTcustBpCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTcustBpCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTcustBpCd.HeightDef = 16
        Me.lblTitleTcustBpCd.Location = New System.Drawing.Point(131, 448)
        Me.lblTitleTcustBpCd.Name = "lblTitleTcustBpCd"
        Me.lblTitleTcustBpCd.Size = New System.Drawing.Size(56, 16)
        Me.lblTitleTcustBpCd.TabIndex = 618
        Me.lblTitleTcustBpCd.Text = "真荷主"
        Me.lblTitleTcustBpCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTcustBpCd.TextValue = "真荷主"
        Me.lblTitleTcustBpCd.WidthDef = 56
        '
        'txtTcustBpCd
        '
        Me.txtTcustBpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTcustBpCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTcustBpCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTcustBpCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTcustBpCd.CountWrappedLine = False
        Me.txtTcustBpCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTcustBpCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTcustBpCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTcustBpCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTcustBpCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTcustBpCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTcustBpCd.HeightDef = 18
        Me.txtTcustBpCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTcustBpCd.HissuLabelVisible = True
        Me.txtTcustBpCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtTcustBpCd.IsByteCheck = 10
        Me.txtTcustBpCd.IsCalendarCheck = False
        Me.txtTcustBpCd.IsDakutenCheck = False
        Me.txtTcustBpCd.IsEisuCheck = False
        Me.txtTcustBpCd.IsForbiddenWordsCheck = False
        Me.txtTcustBpCd.IsFullByteCheck = 0
        Me.txtTcustBpCd.IsHankakuCheck = False
        Me.txtTcustBpCd.IsHissuCheck = True
        Me.txtTcustBpCd.IsKanaCheck = False
        Me.txtTcustBpCd.IsMiddleSpace = False
        Me.txtTcustBpCd.IsNumericCheck = False
        Me.txtTcustBpCd.IsSujiCheck = False
        Me.txtTcustBpCd.IsZenkakuCheck = False
        Me.txtTcustBpCd.ItemName = ""
        Me.txtTcustBpCd.LineSpace = 0
        Me.txtTcustBpCd.Location = New System.Drawing.Point(189, 448)
        Me.txtTcustBpCd.MaxLength = 10
        Me.txtTcustBpCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTcustBpCd.MaxLineCount = 0
        Me.txtTcustBpCd.Multiline = False
        Me.txtTcustBpCd.Name = "txtTcustBpCd"
        Me.txtTcustBpCd.ReadOnly = False
        Me.txtTcustBpCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTcustBpCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTcustBpCd.Size = New System.Drawing.Size(94, 18)
        Me.txtTcustBpCd.TabIndex = 619
        Me.txtTcustBpCd.TabStopSetting = True
        Me.txtTcustBpCd.TextValue = "1234567890"
        Me.txtTcustBpCd.UseSystemPasswordChar = False
        Me.txtTcustBpCd.WidthDef = 94
        Me.txtTcustBpCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqkmkCdS
        '
        Me.txtSeiqkmkCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqkmkCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqkmkCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqkmkCdS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqkmkCdS.CountWrappedLine = False
        Me.txtSeiqkmkCdS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqkmkCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqkmkCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqkmkCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqkmkCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqkmkCdS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqkmkCdS.HeightDef = 18
        Me.txtSeiqkmkCdS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqkmkCdS.HissuLabelVisible = False
        Me.txtSeiqkmkCdS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtSeiqkmkCdS.IsByteCheck = 2
        Me.txtSeiqkmkCdS.IsCalendarCheck = False
        Me.txtSeiqkmkCdS.IsDakutenCheck = False
        Me.txtSeiqkmkCdS.IsEisuCheck = False
        Me.txtSeiqkmkCdS.IsForbiddenWordsCheck = False
        Me.txtSeiqkmkCdS.IsFullByteCheck = 0
        Me.txtSeiqkmkCdS.IsHankakuCheck = False
        Me.txtSeiqkmkCdS.IsHissuCheck = False
        Me.txtSeiqkmkCdS.IsKanaCheck = False
        Me.txtSeiqkmkCdS.IsMiddleSpace = False
        Me.txtSeiqkmkCdS.IsNumericCheck = False
        Me.txtSeiqkmkCdS.IsSujiCheck = False
        Me.txtSeiqkmkCdS.IsZenkakuCheck = False
        Me.txtSeiqkmkCdS.ItemName = ""
        Me.txtSeiqkmkCdS.LineSpace = 0
        Me.txtSeiqkmkCdS.Location = New System.Drawing.Point(612, 491)
        Me.txtSeiqkmkCdS.MaxLength = 2
        Me.txtSeiqkmkCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqkmkCdS.MaxLineCount = 0
        Me.txtSeiqkmkCdS.Multiline = False
        Me.txtSeiqkmkCdS.Name = "txtSeiqkmkCdS"
        Me.txtSeiqkmkCdS.ReadOnly = False
        Me.txtSeiqkmkCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqkmkCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqkmkCdS.Size = New System.Drawing.Size(40, 18)
        Me.txtSeiqkmkCdS.TabIndex = 622
        Me.txtSeiqkmkCdS.TabStopSetting = True
        Me.txtSeiqkmkCdS.TextValue = ""
        Me.txtSeiqkmkCdS.UseSystemPasswordChar = False
        Me.txtSeiqkmkCdS.WidthDef = 40
        Me.txtSeiqkmkCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeiqkmkCdS
        '
        Me.lblTitleSeiqkmkCdS.AutoSize = True
        Me.lblTitleSeiqkmkCdS.AutoSizeDef = True
        Me.lblTitleSeiqkmkCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqkmkCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqkmkCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqkmkCdS.EnableStatus = False
        Me.lblTitleSeiqkmkCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqkmkCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqkmkCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqkmkCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqkmkCdS.HeightDef = 13
        Me.lblTitleSeiqkmkCdS.Location = New System.Drawing.Point(557, 493)
        Me.lblTitleSeiqkmkCdS.Name = "lblTitleSeiqkmkCdS"
        Me.lblTitleSeiqkmkCdS.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeiqkmkCdS.TabIndex = 621
        Me.lblTitleSeiqkmkCdS.Text = "小分類"
        Me.lblTitleSeiqkmkCdS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqkmkCdS.TextValue = "小分類"
        Me.lblTitleSeiqkmkCdS.WidthDef = 49
        '
        'LMM360F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM360F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeiqCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleGroupKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtPtnCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitlePtnCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numkeisanTlgk As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents cmbGroupKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSeiqkmkCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKeisanTlgk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSeiqkmkCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTekiyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtTekiyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNebikiGt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNebikiRt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNebikiGk As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNebikiRt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSeiqNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSeiqkmkNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTcustBpNm As Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTcustBpCd As Win.LMTitleLabel
    Friend WithEvents txtTcustBpCd As Win.InputMan.LMImTextBox
    Friend WithEvents sprDetail_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents txtSeiqkmkCdS As Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeiqkmkCdS As Win.LMTitleLabel
End Class
