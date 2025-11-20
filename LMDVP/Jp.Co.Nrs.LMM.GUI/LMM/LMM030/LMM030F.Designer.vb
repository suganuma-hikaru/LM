<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM030F
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
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoUpCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSagyoUp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.txtSagyoRyak = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRem = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleZei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleFlwp = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleTan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleInvT = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKosu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleInvYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSpl = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleRyak = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.TitlelblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbInvYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbZeiKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbKosuBai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbInvTani = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbFlwpYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbSplRpt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblWHSagyoYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbWHSagyoYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtWhSagyoRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSijiRemark = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtWhSagyoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSijiSagyoNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSagyoSub = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoSubCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoSubNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustSubNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustSubCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.lblCustSubNmL)
        Me.pnlViewAria.Controls.Add(Me.lblCustSubCdL)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoSubNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoSub)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoSubCd)
        Me.pnlViewAria.Controls.Add(Me.txtWhSagyoNm)
        Me.pnlViewAria.Controls.Add(Me.lblSijiSagyoNm)
        Me.pnlViewAria.Controls.Add(Me.txtWhSagyoRemark)
        Me.pnlViewAria.Controls.Add(Me.lblSijiRemark)
        Me.pnlViewAria.Controls.Add(Me.lblWHSagyoYn)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.cmbSplRpt)
        Me.pnlViewAria.Controls.Add(Me.cmbWHSagyoYn)
        Me.pnlViewAria.Controls.Add(Me.cmbFlwpYn)
        Me.pnlViewAria.Controls.Add(Me.cmbInvTani)
        Me.pnlViewAria.Controls.Add(Me.cmbKosuBai)
        Me.pnlViewAria.Controls.Add(Me.cmbZeiKbn)
        Me.pnlViewAria.Controls.Add(Me.cmbInvYn)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.TitlelblEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKosu)
        Me.pnlViewAria.Controls.Add(Me.txtRemark)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoUpCurrCd)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.numSagyoUp)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoRyak)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoNm)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRem)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblTitleZei)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCust)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmL)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleFlwp)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRyak)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTan)
        Me.pnlViewAria.Controls.Add(Me.lblTitleInvT)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSpl)
        Me.pnlViewAria.Controls.Add(Me.lblTitleInvYn)
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
        Me.sprDetail.HeightDef = 405
        Me.sprDetail.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.HorizontalScrollBar.Name = ""
        Me.sprDetail.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 23)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail_Sheet1})
        Me.sprDetail.Size = New System.Drawing.Size(1248, 405)
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
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1038, 482)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(84, 18)
        Me.LmTitleLabel7.TabIndex = 0
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 84
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1130, 451)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 218
        Me.lblSituation.TabStop = False
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1123, 484)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 220
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1041, 504)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(81, 18)
        Me.LmTitleLabel8.TabIndex = 221
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 81
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1123, 505)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 222
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1041, 525)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(81, 18)
        Me.LmTitleLabel9.TabIndex = 223
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 81
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1123, 526)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 224
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1041, 547)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(81, 18)
        Me.LmTitleLabel10.TabIndex = 225
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 81
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1123, 547)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 226
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtRemark.CountWrappedLine = False
        Me.txtRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark.HeightDef = 18
        Me.txtRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.HissuLabelVisible = False
        Me.txtRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark.IsByteCheck = 100
        Me.txtRemark.IsCalendarCheck = False
        Me.txtRemark.IsDakutenCheck = False
        Me.txtRemark.IsEisuCheck = False
        Me.txtRemark.IsForbiddenWordsCheck = False
        Me.txtRemark.IsFullByteCheck = 0
        Me.txtRemark.IsHankakuCheck = False
        Me.txtRemark.IsHissuCheck = False
        Me.txtRemark.IsKanaCheck = False
        Me.txtRemark.IsMiddleSpace = False
        Me.txtRemark.IsNumericCheck = False
        Me.txtRemark.IsSujiCheck = False
        Me.txtRemark.IsZenkakuCheck = False
        Me.txtRemark.ItemName = ""
        Me.txtRemark.LineSpace = 0
        Me.txtRemark.Location = New System.Drawing.Point(139, 661)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(724, 18)
        Me.txtRemark.TabIndex = 322
        Me.txtRemark.TabStopSetting = True
        Me.txtRemark.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" &
    "---10---XX---10---X"
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 724
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoUpCurrCd
        '
        Me.lblSagyoUpCurrCd.AutoSizeDef = False
        Me.lblSagyoUpCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoUpCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoUpCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoUpCurrCd.EnableStatus = False
        Me.lblSagyoUpCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoUpCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoUpCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoUpCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoUpCurrCd.HeightDef = 18
        Me.lblSagyoUpCurrCd.Location = New System.Drawing.Point(305, 577)
        Me.lblSagyoUpCurrCd.Name = "lblSagyoUpCurrCd"
        Me.lblSagyoUpCurrCd.Size = New System.Drawing.Size(30, 18)
        Me.lblSagyoUpCurrCd.TabIndex = 316
        Me.lblSagyoUpCurrCd.Text = "XXX"
        Me.lblSagyoUpCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoUpCurrCd.TextValue = "XXX"
        Me.lblSagyoUpCurrCd.WidthDef = 30
        '
        'numSagyoUp
        '
        Me.numSagyoUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoUp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoUp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoUp.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoUp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoUp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoUp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoUp.HeightDef = 18
        Me.numSagyoUp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoUp.HissuLabelVisible = False
        Me.numSagyoUp.IsHissuCheck = False
        Me.numSagyoUp.IsRangeCheck = False
        Me.numSagyoUp.ItemName = ""
        Me.numSagyoUp.Location = New System.Drawing.Point(139, 577)
        Me.numSagyoUp.Name = "numSagyoUp"
        Me.numSagyoUp.ReadOnly = False
        Me.numSagyoUp.Size = New System.Drawing.Size(180, 18)
        Me.numSagyoUp.TabIndex = 319
        Me.numSagyoUp.TabStopSetting = True
        Me.numSagyoUp.TextValue = "0"
        Me.numSagyoUp.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoUp.WidthDef = 180
        '
        'txtSagyoRyak
        '
        Me.txtSagyoRyak.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRyak.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRyak.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRyak.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRyak.CountWrappedLine = False
        Me.txtSagyoRyak.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRyak.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRyak.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRyak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRyak.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRyak.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRyak.HeightDef = 18
        Me.txtSagyoRyak.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRyak.HissuLabelVisible = True
        Me.txtSagyoRyak.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRyak.IsByteCheck = 6
        Me.txtSagyoRyak.IsCalendarCheck = False
        Me.txtSagyoRyak.IsDakutenCheck = False
        Me.txtSagyoRyak.IsEisuCheck = False
        Me.txtSagyoRyak.IsForbiddenWordsCheck = False
        Me.txtSagyoRyak.IsFullByteCheck = 0
        Me.txtSagyoRyak.IsHankakuCheck = False
        Me.txtSagyoRyak.IsHissuCheck = True
        Me.txtSagyoRyak.IsKanaCheck = False
        Me.txtSagyoRyak.IsMiddleSpace = False
        Me.txtSagyoRyak.IsNumericCheck = False
        Me.txtSagyoRyak.IsSujiCheck = False
        Me.txtSagyoRyak.IsZenkakuCheck = False
        Me.txtSagyoRyak.ItemName = ""
        Me.txtSagyoRyak.LineSpace = 0
        Me.txtSagyoRyak.Location = New System.Drawing.Point(139, 514)
        Me.txtSagyoRyak.MaxLength = 6
        Me.txtSagyoRyak.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRyak.MaxLineCount = 0
        Me.txtSagyoRyak.Multiline = False
        Me.txtSagyoRyak.Name = "txtSagyoRyak"
        Me.txtSagyoRyak.ReadOnly = False
        Me.txtSagyoRyak.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRyak.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRyak.Size = New System.Drawing.Size(97, 18)
        Me.txtSagyoRyak.TabIndex = 318
        Me.txtSagyoRyak.TabStopSetting = True
        Me.txtSagyoRyak.TextValue = "X---1"
        Me.txtSagyoRyak.UseSystemPasswordChar = False
        Me.txtSagyoRyak.WidthDef = 97
        Me.txtSagyoRyak.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoNm
        '
        Me.txtSagyoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoNm.CountWrappedLine = False
        Me.txtSagyoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoNm.HeightDef = 18
        Me.txtSagyoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoNm.HissuLabelVisible = True
        Me.txtSagyoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoNm.IsByteCheck = 60
        Me.txtSagyoNm.IsCalendarCheck = False
        Me.txtSagyoNm.IsDakutenCheck = False
        Me.txtSagyoNm.IsEisuCheck = False
        Me.txtSagyoNm.IsForbiddenWordsCheck = False
        Me.txtSagyoNm.IsFullByteCheck = 0
        Me.txtSagyoNm.IsHankakuCheck = False
        Me.txtSagyoNm.IsHissuCheck = True
        Me.txtSagyoNm.IsKanaCheck = False
        Me.txtSagyoNm.IsMiddleSpace = False
        Me.txtSagyoNm.IsNumericCheck = False
        Me.txtSagyoNm.IsSujiCheck = False
        Me.txtSagyoNm.IsZenkakuCheck = False
        Me.txtSagyoNm.ItemName = ""
        Me.txtSagyoNm.LineSpace = 0
        Me.txtSagyoNm.Location = New System.Drawing.Point(188, 493)
        Me.txtSagyoNm.MaxLength = 60
        Me.txtSagyoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoNm.MaxLineCount = 0
        Me.txtSagyoNm.Multiline = False
        Me.txtSagyoNm.Name = "txtSagyoNm"
        Me.txtSagyoNm.ReadOnly = False
        Me.txtSagyoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoNm.Size = New System.Drawing.Size(505, 18)
        Me.txtSagyoNm.TabIndex = 317
        Me.txtSagyoNm.TabStopSetting = True
        Me.txtSagyoNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtSagyoNm.UseSystemPasswordChar = False
        Me.txtSagyoNm.WidthDef = 505
        Me.txtSagyoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRem
        '
        Me.lblTitleRem.AutoSizeDef = False
        Me.lblTitleRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRem.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRem.EnableStatus = False
        Me.lblTitleRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRem.HeightDef = 13
        Me.lblTitleRem.Location = New System.Drawing.Point(59, 663)
        Me.lblTitleRem.Name = "lblTitleRem"
        Me.lblTitleRem.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleRem.TabIndex = 316
        Me.lblTitleRem.Text = "備考"
        Me.lblTitleRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRem.TextValue = "備考"
        Me.lblTitleRem.WidthDef = 78
        '
        'lblTitleZei
        '
        Me.lblTitleZei.AutoSizeDef = False
        Me.lblTitleZei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZei.EnableStatus = False
        Me.lblTitleZei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZei.HeightDef = 13
        Me.lblTitleZei.Location = New System.Drawing.Point(43, 601)
        Me.lblTitleZei.Name = "lblTitleZei"
        Me.lblTitleZei.Size = New System.Drawing.Size(94, 13)
        Me.lblTitleZei.TabIndex = 315
        Me.lblTitleZei.Text = "課税区分"
        Me.lblTitleZei.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZei.TextValue = "課税区分"
        Me.lblTitleZei.WidthDef = 94
        '
        'lblTitleCust
        '
        Me.lblTitleCust.AutoSizeDef = False
        Me.lblTitleCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCust.EnableStatus = False
        Me.lblTitleCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.HeightDef = 13
        Me.lblTitleCust.Location = New System.Drawing.Point(80, 474)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(57, 13)
        Me.lblTitleCust.TabIndex = 314
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 57
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
        Me.lblCustNmL.IsByteCheck = 60
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
        Me.lblCustNmL.Location = New System.Drawing.Point(220, 472)
        Me.lblCustNmL.MaxLength = 60
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 313
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 473
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdL.Location = New System.Drawing.Point(139, 472)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 312
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "X1"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleFlwp
        '
        Me.lblTitleFlwp.AutoSizeDef = False
        Me.lblTitleFlwp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFlwp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFlwp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFlwp.EnableStatus = False
        Me.lblTitleFlwp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFlwp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFlwp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFlwp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFlwp.HeightDef = 13
        Me.lblTitleFlwp.Location = New System.Drawing.Point(25, 642)
        Me.lblTitleFlwp.Name = "lblTitleFlwp"
        Me.lblTitleFlwp.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleFlwp.TabIndex = 308
        Me.lblTitleFlwp.Text = "進捗管理の有/無"
        Me.lblTitleFlwp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFlwp.TextValue = "進捗管理の有/無"
        Me.lblTitleFlwp.WidthDef = 112
        '
        'lblTitleTan
        '
        Me.lblTitleTan.AutoSizeDef = False
        Me.lblTitleTan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTan.EnableStatus = False
        Me.lblTitleTan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTan.HeightDef = 13
        Me.lblTitleTan.Location = New System.Drawing.Point(56, 579)
        Me.lblTitleTan.Name = "lblTitleTan"
        Me.lblTitleTan.Size = New System.Drawing.Size(81, 13)
        Me.lblTitleTan.TabIndex = 304
        Me.lblTitleTan.Text = "単価"
        Me.lblTitleTan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTan.TextValue = "単価"
        Me.lblTitleTan.WidthDef = 81
        '
        'lblTitleInvT
        '
        Me.lblTitleInvT.AutoSizeDef = False
        Me.lblTitleInvT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvT.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInvT.EnableStatus = False
        Me.lblTitleInvT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvT.HeightDef = 13
        Me.lblTitleInvT.Location = New System.Drawing.Point(74, 558)
        Me.lblTitleInvT.Name = "lblTitleInvT"
        Me.lblTitleInvT.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleInvT.TabIndex = 303
        Me.lblTitleInvT.Text = "請求単位"
        Me.lblTitleInvT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInvT.TextValue = "請求単位"
        Me.lblTitleInvT.WidthDef = 63
        '
        'lblTitleKosu
        '
        Me.lblTitleKosu.AutoSizeDef = False
        Me.lblTitleKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKosu.EnableStatus = False
        Me.lblTitleKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.HeightDef = 13
        Me.lblTitleKosu.Location = New System.Drawing.Point(247, 558)
        Me.lblTitleKosu.Name = "lblTitleKosu"
        Me.lblTitleKosu.Size = New System.Drawing.Size(175, 13)
        Me.lblTitleKosu.TabIndex = 301
        Me.lblTitleKosu.Text = "請求書作成時個数で乗する"
        Me.lblTitleKosu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKosu.TextValue = "請求書作成時個数で乗する"
        Me.lblTitleKosu.WidthDef = 175
        '
        'lblTitleInvYn
        '
        Me.lblTitleInvYn.AutoSize = True
        Me.lblTitleInvYn.AutoSizeDef = True
        Me.lblTitleInvYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInvYn.EnableStatus = False
        Me.lblTitleInvYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvYn.HeightDef = 13
        Me.lblTitleInvYn.Location = New System.Drawing.Point(53, 537)
        Me.lblTitleInvYn.Name = "lblTitleInvYn"
        Me.lblTitleInvYn.Size = New System.Drawing.Size(84, 13)
        Me.lblTitleInvYn.TabIndex = 301
        Me.lblTitleInvYn.Text = "請求の有/無"
        Me.lblTitleInvYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInvYn.TextValue = "請求の有/無"
        Me.lblTitleInvYn.WidthDef = 84
        '
        'lblTitleSpl
        '
        Me.lblTitleSpl.AutoSizeDef = False
        Me.lblTitleSpl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSpl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSpl.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSpl.EnableStatus = False
        Me.lblTitleSpl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSpl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSpl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSpl.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSpl.HeightDef = 13
        Me.lblTitleSpl.Location = New System.Drawing.Point(12, 621)
        Me.lblTitleSpl.Name = "lblTitleSpl"
        Me.lblTitleSpl.Size = New System.Drawing.Size(125, 13)
        Me.lblTitleSpl.TabIndex = 277
        Me.lblTitleSpl.Text = "作業指示明細"
        Me.lblTitleSpl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSpl.TextValue = "作業指示明細"
        Me.lblTitleSpl.WidthDef = 125
        '
        'lblTitleRyak
        '
        Me.lblTitleRyak.AutoSizeDef = False
        Me.lblTitleRyak.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRyak.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRyak.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRyak.EnableStatus = False
        Me.lblTitleRyak.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRyak.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRyak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRyak.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRyak.HeightDef = 13
        Me.lblTitleRyak.Location = New System.Drawing.Point(46, 516)
        Me.lblTitleRyak.Name = "lblTitleRyak"
        Me.lblTitleRyak.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleRyak.TabIndex = 300
        Me.lblTitleRyak.Text = "作業項目略称"
        Me.lblTitleRyak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRyak.TextValue = "作業項目略称"
        Me.lblTitleRyak.WidthDef = 91
        '
        'txtSagyoCd
        '
        Me.txtSagyoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCd.CountWrappedLine = False
        Me.txtSagyoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCd.HeightDef = 18
        Me.txtSagyoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.HissuLabelVisible = True
        Me.txtSagyoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCd.IsByteCheck = 5
        Me.txtSagyoCd.IsCalendarCheck = False
        Me.txtSagyoCd.IsDakutenCheck = False
        Me.txtSagyoCd.IsEisuCheck = False
        Me.txtSagyoCd.IsForbiddenWordsCheck = False
        Me.txtSagyoCd.IsFullByteCheck = 0
        Me.txtSagyoCd.IsHankakuCheck = False
        Me.txtSagyoCd.IsHissuCheck = True
        Me.txtSagyoCd.IsKanaCheck = False
        Me.txtSagyoCd.IsMiddleSpace = False
        Me.txtSagyoCd.IsNumericCheck = False
        Me.txtSagyoCd.IsSujiCheck = False
        Me.txtSagyoCd.IsZenkakuCheck = False
        Me.txtSagyoCd.ItemName = ""
        Me.txtSagyoCd.LineSpace = 0
        Me.txtSagyoCd.Location = New System.Drawing.Point(139, 493)
        Me.txtSagyoCd.MaxLength = 5
        Me.txtSagyoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCd.MaxLineCount = 0
        Me.txtSagyoCd.Multiline = False
        Me.txtSagyoCd.Name = "txtSagyoCd"
        Me.txtSagyoCd.ReadOnly = True
        Me.txtSagyoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCd.Size = New System.Drawing.Size(65, 18)
        Me.txtSagyoCd.TabIndex = 229
        Me.txtSagyoCd.TabStop = False
        Me.txtSagyoCd.TabStopSetting = False
        Me.txtSagyoCd.TextValue = "XXXXX"
        Me.txtSagyoCd.UseSystemPasswordChar = False
        Me.txtSagyoCd.WidthDef = 65
        Me.txtSagyoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo
        '
        Me.lblTitleSagyo.AutoSizeDef = False
        Me.lblTitleSagyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo.EnableStatus = False
        Me.lblTitleSagyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo.HeightDef = 13
        Me.lblTitleSagyo.Location = New System.Drawing.Point(49, 495)
        Me.lblTitleSagyo.Name = "lblTitleSagyo"
        Me.lblTitleSagyo.Size = New System.Drawing.Size(88, 13)
        Me.lblTitleSagyo.TabIndex = 234
        Me.lblTitleSagyo.Text = "作業項目"
        Me.lblTitleSagyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo.TextValue = "作業項目"
        Me.lblTitleSagyo.WidthDef = 88
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(139, 451)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 605
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'TitlelblEigyo
        '
        Me.TitlelblEigyo.AutoSizeDef = False
        Me.TitlelblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblEigyo.EnableStatus = False
        Me.TitlelblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.HeightDef = 13
        Me.TitlelblEigyo.Location = New System.Drawing.Point(77, 453)
        Me.TitlelblEigyo.Name = "TitlelblEigyo"
        Me.TitlelblEigyo.Size = New System.Drawing.Size(60, 13)
        Me.TitlelblEigyo.TabIndex = 604
        Me.TitlelblEigyo.Text = "営業所"
        Me.TitlelblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblEigyo.TextValue = "営業所"
        Me.TitlelblEigyo.WidthDef = 60
        '
        'cmbInvYn
        '
        Me.cmbInvYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbInvYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbInvYn.DataCode = "U009"
        Me.cmbInvYn.DataSource = Nothing
        Me.cmbInvYn.DisplayMember = Nothing
        Me.cmbInvYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvYn.HeightDef = 18
        Me.cmbInvYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbInvYn.HissuLabelVisible = True
        Me.cmbInvYn.InsertWildCard = True
        Me.cmbInvYn.IsForbiddenWordsCheck = False
        Me.cmbInvYn.IsHissuCheck = True
        Me.cmbInvYn.ItemName = ""
        Me.cmbInvYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM2
        Me.cmbInvYn.Location = New System.Drawing.Point(139, 535)
        Me.cmbInvYn.Name = "cmbInvYn"
        Me.cmbInvYn.ReadOnly = False
        Me.cmbInvYn.SelectedIndex = -1
        Me.cmbInvYn.SelectedItem = Nothing
        Me.cmbInvYn.SelectedText = ""
        Me.cmbInvYn.SelectedValue = ""
        Me.cmbInvYn.Size = New System.Drawing.Size(97, 18)
        Me.cmbInvYn.TabIndex = 606
        Me.cmbInvYn.TabStopSetting = True
        Me.cmbInvYn.TextValue = ""
        Me.cmbInvYn.Value1 = Nothing
        Me.cmbInvYn.Value2 = Nothing
        Me.cmbInvYn.Value3 = Nothing
        Me.cmbInvYn.ValueMember = Nothing
        Me.cmbInvYn.WidthDef = 97
        '
        'cmbZeiKbn
        '
        Me.cmbZeiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZeiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZeiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbZeiKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbZeiKbn.DataCode = "Z001"
        Me.cmbZeiKbn.DataSource = Nothing
        Me.cmbZeiKbn.DisplayMember = Nothing
        Me.cmbZeiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZeiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZeiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZeiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZeiKbn.HeightDef = 18
        Me.cmbZeiKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbZeiKbn.HissuLabelVisible = True
        Me.cmbZeiKbn.InsertWildCard = True
        Me.cmbZeiKbn.IsForbiddenWordsCheck = False
        Me.cmbZeiKbn.IsHissuCheck = True
        Me.cmbZeiKbn.ItemName = ""
        Me.cmbZeiKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbZeiKbn.Location = New System.Drawing.Point(139, 598)
        Me.cmbZeiKbn.Name = "cmbZeiKbn"
        Me.cmbZeiKbn.ReadOnly = False
        Me.cmbZeiKbn.SelectedIndex = -1
        Me.cmbZeiKbn.SelectedItem = Nothing
        Me.cmbZeiKbn.SelectedText = ""
        Me.cmbZeiKbn.SelectedValue = ""
        Me.cmbZeiKbn.Size = New System.Drawing.Size(97, 18)
        Me.cmbZeiKbn.TabIndex = 608
        Me.cmbZeiKbn.TabStopSetting = True
        Me.cmbZeiKbn.TextValue = ""
        Me.cmbZeiKbn.Value1 = Nothing
        Me.cmbZeiKbn.Value2 = Nothing
        Me.cmbZeiKbn.Value3 = Nothing
        Me.cmbZeiKbn.ValueMember = Nothing
        Me.cmbZeiKbn.WidthDef = 97
        '
        'cmbKosuBai
        '
        Me.cmbKosuBai.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKosuBai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKosuBai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKosuBai.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKosuBai.DataCode = "S049"
        Me.cmbKosuBai.DataSource = Nothing
        Me.cmbKosuBai.DisplayMember = Nothing
        Me.cmbKosuBai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKosuBai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKosuBai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKosuBai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKosuBai.HeightDef = 18
        Me.cmbKosuBai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKosuBai.HissuLabelVisible = True
        Me.cmbKosuBai.InsertWildCard = True
        Me.cmbKosuBai.IsForbiddenWordsCheck = False
        Me.cmbKosuBai.IsHissuCheck = True
        Me.cmbKosuBai.ItemName = ""
        Me.cmbKosuBai.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbKosuBai.Location = New System.Drawing.Point(424, 556)
        Me.cmbKosuBai.Name = "cmbKosuBai"
        Me.cmbKosuBai.ReadOnly = False
        Me.cmbKosuBai.SelectedIndex = -1
        Me.cmbKosuBai.SelectedItem = Nothing
        Me.cmbKosuBai.SelectedText = ""
        Me.cmbKosuBai.SelectedValue = ""
        Me.cmbKosuBai.Size = New System.Drawing.Size(269, 18)
        Me.cmbKosuBai.TabIndex = 609
        Me.cmbKosuBai.TabStopSetting = True
        Me.cmbKosuBai.TextValue = ""
        Me.cmbKosuBai.Value1 = Nothing
        Me.cmbKosuBai.Value2 = Nothing
        Me.cmbKosuBai.Value3 = Nothing
        Me.cmbKosuBai.ValueMember = Nothing
        Me.cmbKosuBai.WidthDef = 269
        '
        'cmbInvTani
        '
        Me.cmbInvTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvTani.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbInvTani.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbInvTani.DataCode = "S027"
        Me.cmbInvTani.DataSource = Nothing
        Me.cmbInvTani.DisplayMember = Nothing
        Me.cmbInvTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvTani.HeightDef = 18
        Me.cmbInvTani.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbInvTani.HissuLabelVisible = False
        Me.cmbInvTani.InsertWildCard = True
        Me.cmbInvTani.IsForbiddenWordsCheck = False
        Me.cmbInvTani.IsHissuCheck = False
        Me.cmbInvTani.ItemName = ""
        Me.cmbInvTani.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbInvTani.Location = New System.Drawing.Point(139, 556)
        Me.cmbInvTani.Name = "cmbInvTani"
        Me.cmbInvTani.ReadOnly = False
        Me.cmbInvTani.SelectedIndex = -1
        Me.cmbInvTani.SelectedItem = Nothing
        Me.cmbInvTani.SelectedText = ""
        Me.cmbInvTani.SelectedValue = ""
        Me.cmbInvTani.Size = New System.Drawing.Size(97, 18)
        Me.cmbInvTani.TabIndex = 610
        Me.cmbInvTani.TabStopSetting = True
        Me.cmbInvTani.TextValue = ""
        Me.cmbInvTani.Value1 = Nothing
        Me.cmbInvTani.Value2 = Nothing
        Me.cmbInvTani.Value3 = Nothing
        Me.cmbInvTani.ValueMember = Nothing
        Me.cmbInvTani.WidthDef = 97
        '
        'cmbFlwpYn
        '
        Me.cmbFlwpYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbFlwpYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbFlwpYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbFlwpYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbFlwpYn.DataCode = "U009"
        Me.cmbFlwpYn.DataSource = Nothing
        Me.cmbFlwpYn.DisplayMember = Nothing
        Me.cmbFlwpYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlwpYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFlwpYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbFlwpYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbFlwpYn.HeightDef = 18
        Me.cmbFlwpYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbFlwpYn.HissuLabelVisible = True
        Me.cmbFlwpYn.InsertWildCard = True
        Me.cmbFlwpYn.IsForbiddenWordsCheck = False
        Me.cmbFlwpYn.IsHissuCheck = True
        Me.cmbFlwpYn.ItemName = ""
        Me.cmbFlwpYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM2
        Me.cmbFlwpYn.Location = New System.Drawing.Point(139, 640)
        Me.cmbFlwpYn.Name = "cmbFlwpYn"
        Me.cmbFlwpYn.ReadOnly = False
        Me.cmbFlwpYn.SelectedIndex = -1
        Me.cmbFlwpYn.SelectedItem = Nothing
        Me.cmbFlwpYn.SelectedText = ""
        Me.cmbFlwpYn.SelectedValue = ""
        Me.cmbFlwpYn.Size = New System.Drawing.Size(97, 18)
        Me.cmbFlwpYn.TabIndex = 611
        Me.cmbFlwpYn.TabStopSetting = True
        Me.cmbFlwpYn.TextValue = ""
        Me.cmbFlwpYn.Value1 = Nothing
        Me.cmbFlwpYn.Value2 = Nothing
        Me.cmbFlwpYn.Value3 = Nothing
        Me.cmbFlwpYn.ValueMember = Nothing
        Me.cmbFlwpYn.WidthDef = 97
        '
        'cmbSplRpt
        '
        Me.cmbSplRpt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSplRpt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSplRpt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSplRpt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSplRpt.DataCode = "S045"
        Me.cmbSplRpt.DataSource = Nothing
        Me.cmbSplRpt.DisplayMember = Nothing
        Me.cmbSplRpt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSplRpt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSplRpt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSplRpt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSplRpt.HeightDef = 18
        Me.cmbSplRpt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSplRpt.HissuLabelVisible = True
        Me.cmbSplRpt.InsertWildCard = True
        Me.cmbSplRpt.IsForbiddenWordsCheck = False
        Me.cmbSplRpt.IsHissuCheck = True
        Me.cmbSplRpt.ItemName = ""
        Me.cmbSplRpt.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSplRpt.Location = New System.Drawing.Point(139, 619)
        Me.cmbSplRpt.Name = "cmbSplRpt"
        Me.cmbSplRpt.ReadOnly = False
        Me.cmbSplRpt.SelectedIndex = -1
        Me.cmbSplRpt.SelectedItem = Nothing
        Me.cmbSplRpt.SelectedText = ""
        Me.cmbSplRpt.SelectedValue = ""
        Me.cmbSplRpt.Size = New System.Drawing.Size(137, 18)
        Me.cmbSplRpt.TabIndex = 612
        Me.cmbSplRpt.TabStopSetting = True
        Me.cmbSplRpt.TextValue = ""
        Me.cmbSplRpt.Value1 = Nothing
        Me.cmbSplRpt.Value2 = Nothing
        Me.cmbSplRpt.Value3 = Nothing
        Me.cmbSplRpt.ValueMember = Nothing
        Me.cmbSplRpt.WidthDef = 137
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1123, 589)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 624
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1123, 568)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 623
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblWHSagyoYn
        '
        Me.lblWHSagyoYn.AutoSizeDef = False
        Me.lblWHSagyoYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWHSagyoYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWHSagyoYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblWHSagyoYn.EnableStatus = False
        Me.lblWHSagyoYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWHSagyoYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWHSagyoYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWHSagyoYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWHSagyoYn.HeightDef = 13
        Me.lblWHSagyoYn.Location = New System.Drawing.Point(25, 684)
        Me.lblWHSagyoYn.Name = "lblWHSagyoYn"
        Me.lblWHSagyoYn.Size = New System.Drawing.Size(112, 13)
        Me.lblWHSagyoYn.TabIndex = 625
        Me.lblWHSagyoYn.Text = "現場作業の有/無"
        Me.lblWHSagyoYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblWHSagyoYn.TextValue = "現場作業の有/無"
        Me.lblWHSagyoYn.WidthDef = 112
        '
        'cmbWHSagyoYn
        '
        Me.cmbWHSagyoYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWHSagyoYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWHSagyoYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbWHSagyoYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbWHSagyoYn.DataCode = "U009"
        Me.cmbWHSagyoYn.DataSource = Nothing
        Me.cmbWHSagyoYn.DisplayMember = Nothing
        Me.cmbWHSagyoYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWHSagyoYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWHSagyoYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWHSagyoYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWHSagyoYn.HeightDef = 18
        Me.cmbWHSagyoYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWHSagyoYn.HissuLabelVisible = False
        Me.cmbWHSagyoYn.InsertWildCard = True
        Me.cmbWHSagyoYn.IsForbiddenWordsCheck = False
        Me.cmbWHSagyoYn.IsHissuCheck = False
        Me.cmbWHSagyoYn.ItemName = ""
        Me.cmbWHSagyoYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM2
        Me.cmbWHSagyoYn.Location = New System.Drawing.Point(139, 682)
        Me.cmbWHSagyoYn.Name = "cmbWHSagyoYn"
        Me.cmbWHSagyoYn.ReadOnly = False
        Me.cmbWHSagyoYn.SelectedIndex = -1
        Me.cmbWHSagyoYn.SelectedItem = Nothing
        Me.cmbWHSagyoYn.SelectedText = ""
        Me.cmbWHSagyoYn.SelectedValue = ""
        Me.cmbWHSagyoYn.Size = New System.Drawing.Size(97, 18)
        Me.cmbWHSagyoYn.TabIndex = 611
        Me.cmbWHSagyoYn.TabStopSetting = True
        Me.cmbWHSagyoYn.TextValue = ""
        Me.cmbWHSagyoYn.Value1 = Nothing
        Me.cmbWHSagyoYn.Value2 = Nothing
        Me.cmbWHSagyoYn.Value3 = Nothing
        Me.cmbWHSagyoYn.ValueMember = Nothing
        Me.cmbWHSagyoYn.WidthDef = 97
        '
        'txtWhSagyoRemark
        '
        Me.txtWhSagyoRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWhSagyoRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWhSagyoRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWhSagyoRemark.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtWhSagyoRemark.CountWrappedLine = False
        Me.txtWhSagyoRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWhSagyoRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWhSagyoRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWhSagyoRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWhSagyoRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWhSagyoRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWhSagyoRemark.HeightDef = 18
        Me.txtWhSagyoRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWhSagyoRemark.HissuLabelVisible = False
        Me.txtWhSagyoRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtWhSagyoRemark.IsByteCheck = 100
        Me.txtWhSagyoRemark.IsCalendarCheck = False
        Me.txtWhSagyoRemark.IsDakutenCheck = False
        Me.txtWhSagyoRemark.IsEisuCheck = False
        Me.txtWhSagyoRemark.IsForbiddenWordsCheck = False
        Me.txtWhSagyoRemark.IsFullByteCheck = 0
        Me.txtWhSagyoRemark.IsHankakuCheck = False
        Me.txtWhSagyoRemark.IsHissuCheck = False
        Me.txtWhSagyoRemark.IsKanaCheck = False
        Me.txtWhSagyoRemark.IsMiddleSpace = False
        Me.txtWhSagyoRemark.IsNumericCheck = False
        Me.txtWhSagyoRemark.IsSujiCheck = False
        Me.txtWhSagyoRemark.IsZenkakuCheck = False
        Me.txtWhSagyoRemark.ItemName = ""
        Me.txtWhSagyoRemark.LineSpace = 0
        Me.txtWhSagyoRemark.Location = New System.Drawing.Point(139, 725)
        Me.txtWhSagyoRemark.MaxLength = 100
        Me.txtWhSagyoRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWhSagyoRemark.MaxLineCount = 0
        Me.txtWhSagyoRemark.Multiline = False
        Me.txtWhSagyoRemark.Name = "txtWhSagyoRemark"
        Me.txtWhSagyoRemark.ReadOnly = False
        Me.txtWhSagyoRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWhSagyoRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWhSagyoRemark.Size = New System.Drawing.Size(724, 18)
        Me.txtWhSagyoRemark.TabIndex = 627
        Me.txtWhSagyoRemark.TabStopSetting = True
        Me.txtWhSagyoRemark.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" &
    "---10---XX---10---X"
        Me.txtWhSagyoRemark.UseSystemPasswordChar = False
        Me.txtWhSagyoRemark.WidthDef = 724
        Me.txtWhSagyoRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSijiRemark
        '
        Me.lblSijiRemark.AutoSizeDef = False
        Me.lblSijiRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSijiRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSijiRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSijiRemark.EnableStatus = False
        Me.lblSijiRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSijiRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSijiRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSijiRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSijiRemark.HeightDef = 16
        Me.lblSijiRemark.Location = New System.Drawing.Point(17, 727)
        Me.lblSijiRemark.Name = "lblSijiRemark"
        Me.lblSijiRemark.Size = New System.Drawing.Size(120, 16)
        Me.lblSijiRemark.TabIndex = 626
        Me.lblSijiRemark.Text = "現場作業備考"
        Me.lblSijiRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSijiRemark.TextValue = "現場作業備考"
        Me.lblSijiRemark.WidthDef = 120
        '
        'txtWhSagyoNm
        '
        Me.txtWhSagyoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWhSagyoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWhSagyoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWhSagyoNm.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtWhSagyoNm.CountWrappedLine = False
        Me.txtWhSagyoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWhSagyoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWhSagyoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWhSagyoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWhSagyoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWhSagyoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWhSagyoNm.HeightDef = 18
        Me.txtWhSagyoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWhSagyoNm.HissuLabelVisible = False
        Me.txtWhSagyoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtWhSagyoNm.IsByteCheck = 60
        Me.txtWhSagyoNm.IsCalendarCheck = False
        Me.txtWhSagyoNm.IsDakutenCheck = False
        Me.txtWhSagyoNm.IsEisuCheck = False
        Me.txtWhSagyoNm.IsForbiddenWordsCheck = False
        Me.txtWhSagyoNm.IsFullByteCheck = 0
        Me.txtWhSagyoNm.IsHankakuCheck = False
        Me.txtWhSagyoNm.IsHissuCheck = False
        Me.txtWhSagyoNm.IsKanaCheck = False
        Me.txtWhSagyoNm.IsMiddleSpace = False
        Me.txtWhSagyoNm.IsNumericCheck = False
        Me.txtWhSagyoNm.IsSujiCheck = False
        Me.txtWhSagyoNm.IsZenkakuCheck = False
        Me.txtWhSagyoNm.ItemName = ""
        Me.txtWhSagyoNm.LineSpace = 0
        Me.txtWhSagyoNm.Location = New System.Drawing.Point(139, 703)
        Me.txtWhSagyoNm.MaxLength = 60
        Me.txtWhSagyoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWhSagyoNm.MaxLineCount = 0
        Me.txtWhSagyoNm.Multiline = False
        Me.txtWhSagyoNm.Name = "txtWhSagyoNm"
        Me.txtWhSagyoNm.ReadOnly = False
        Me.txtWhSagyoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWhSagyoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWhSagyoNm.Size = New System.Drawing.Size(724, 18)
        Me.txtWhSagyoNm.TabIndex = 629
        Me.txtWhSagyoNm.TabStopSetting = True
        Me.txtWhSagyoNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtWhSagyoNm.UseSystemPasswordChar = False
        Me.txtWhSagyoNm.WidthDef = 724
        Me.txtWhSagyoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSijiSagyoNm
        '
        Me.lblSijiSagyoNm.AutoSizeDef = False
        Me.lblSijiSagyoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSijiSagyoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSijiSagyoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSijiSagyoNm.EnableStatus = False
        Me.lblSijiSagyoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSijiSagyoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSijiSagyoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSijiSagyoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSijiSagyoNm.HeightDef = 16
        Me.lblSijiSagyoNm.Location = New System.Drawing.Point(17, 705)
        Me.lblSijiSagyoNm.Name = "lblSijiSagyoNm"
        Me.lblSijiSagyoNm.Size = New System.Drawing.Size(120, 16)
        Me.lblSijiSagyoNm.TabIndex = 628
        Me.lblSijiSagyoNm.Text = "現場作業名"
        Me.lblSijiSagyoNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSijiSagyoNm.TextValue = "現場作業名"
        Me.lblSijiSagyoNm.WidthDef = 120
        '
        'lblTitleSagyoSub
        '
        Me.lblTitleSagyoSub.AutoSizeDef = False
        Me.lblTitleSagyoSub.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSub.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSub.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoSub.EnableStatus = False
        Me.lblTitleSagyoSub.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSub.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSub.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSub.HeightDef = 16
        Me.lblTitleSagyoSub.Location = New System.Drawing.Point(12, 750)
        Me.lblTitleSagyoSub.Name = "lblTitleSagyoSub"
        Me.lblTitleSagyoSub.Size = New System.Drawing.Size(125, 16)
        Me.lblTitleSagyoSub.TabIndex = 631
        Me.lblTitleSagyoSub.Text = "協力会社作業項目"
        Me.lblTitleSagyoSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoSub.TextValue = "協力会社作業項目"
        Me.lblTitleSagyoSub.WidthDef = 125
        '
        'txtSagyoSubCd
        '
        Me.txtSagyoSubCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoSubCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoSubCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoSubCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoSubCd.CountWrappedLine = False
        Me.txtSagyoSubCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoSubCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSubCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSubCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSubCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSubCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoSubCd.HeightDef = 18
        Me.txtSagyoSubCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoSubCd.HissuLabelVisible = False
        Me.txtSagyoSubCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoSubCd.IsByteCheck = 5
        Me.txtSagyoSubCd.IsCalendarCheck = False
        Me.txtSagyoSubCd.IsDakutenCheck = False
        Me.txtSagyoSubCd.IsEisuCheck = False
        Me.txtSagyoSubCd.IsForbiddenWordsCheck = False
        Me.txtSagyoSubCd.IsFullByteCheck = 0
        Me.txtSagyoSubCd.IsHankakuCheck = False
        Me.txtSagyoSubCd.IsHissuCheck = False
        Me.txtSagyoSubCd.IsKanaCheck = False
        Me.txtSagyoSubCd.IsMiddleSpace = False
        Me.txtSagyoSubCd.IsNumericCheck = False
        Me.txtSagyoSubCd.IsSujiCheck = False
        Me.txtSagyoSubCd.IsZenkakuCheck = False
        Me.txtSagyoSubCd.ItemName = ""
        Me.txtSagyoSubCd.LineSpace = 0
        Me.txtSagyoSubCd.Location = New System.Drawing.Point(139, 748)
        Me.txtSagyoSubCd.MaxLength = 5
        Me.txtSagyoSubCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoSubCd.MaxLineCount = 0
        Me.txtSagyoSubCd.Multiline = False
        Me.txtSagyoSubCd.Name = "txtSagyoSubCd"
        Me.txtSagyoSubCd.ReadOnly = False
        Me.txtSagyoSubCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoSubCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoSubCd.Size = New System.Drawing.Size(65, 18)
        Me.txtSagyoSubCd.TabIndex = 630
        Me.txtSagyoSubCd.TabStopSetting = True
        Me.txtSagyoSubCd.TextValue = "XXXXX"
        Me.txtSagyoSubCd.UseSystemPasswordChar = False
        Me.txtSagyoSubCd.WidthDef = 65
        Me.txtSagyoSubCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoSubNm
        '
        Me.lblSagyoSubNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoSubNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoSubNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoSubNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoSubNm.CountWrappedLine = False
        Me.lblSagyoSubNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoSubNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoSubNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoSubNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoSubNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoSubNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoSubNm.HeightDef = 18
        Me.lblSagyoSubNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoSubNm.HissuLabelVisible = False
        Me.lblSagyoSubNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoSubNm.IsByteCheck = 60
        Me.lblSagyoSubNm.IsCalendarCheck = False
        Me.lblSagyoSubNm.IsDakutenCheck = False
        Me.lblSagyoSubNm.IsEisuCheck = False
        Me.lblSagyoSubNm.IsForbiddenWordsCheck = False
        Me.lblSagyoSubNm.IsFullByteCheck = 0
        Me.lblSagyoSubNm.IsHankakuCheck = False
        Me.lblSagyoSubNm.IsHissuCheck = False
        Me.lblSagyoSubNm.IsKanaCheck = False
        Me.lblSagyoSubNm.IsMiddleSpace = False
        Me.lblSagyoSubNm.IsNumericCheck = False
        Me.lblSagyoSubNm.IsSujiCheck = False
        Me.lblSagyoSubNm.IsZenkakuCheck = False
        Me.lblSagyoSubNm.ItemName = ""
        Me.lblSagyoSubNm.LineSpace = 0
        Me.lblSagyoSubNm.Location = New System.Drawing.Point(188, 748)
        Me.lblSagyoSubNm.MaxLength = 60
        Me.lblSagyoSubNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoSubNm.MaxLineCount = 0
        Me.lblSagyoSubNm.Multiline = False
        Me.lblSagyoSubNm.Name = "lblSagyoSubNm"
        Me.lblSagyoSubNm.ReadOnly = True
        Me.lblSagyoSubNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoSubNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoSubNm.Size = New System.Drawing.Size(505, 18)
        Me.lblSagyoSubNm.TabIndex = 632
        Me.lblSagyoSubNm.TabStop = False
        Me.lblSagyoSubNm.TabStopSetting = False
        Me.lblSagyoSubNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSagyoSubNm.UseSystemPasswordChar = False
        Me.lblSagyoSubNm.WidthDef = 505
        Me.lblSagyoSubNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustSubNmL
        '
        Me.lblCustSubNmL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustSubNmL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustSubNmL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustSubNmL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustSubNmL.CountWrappedLine = False
        Me.lblCustSubNmL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustSubNmL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustSubNmL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustSubNmL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustSubNmL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustSubNmL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustSubNmL.HeightDef = 18
        Me.lblCustSubNmL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustSubNmL.HissuLabelVisible = False
        Me.lblCustSubNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustSubNmL.IsByteCheck = 60
        Me.lblCustSubNmL.IsCalendarCheck = False
        Me.lblCustSubNmL.IsDakutenCheck = False
        Me.lblCustSubNmL.IsEisuCheck = False
        Me.lblCustSubNmL.IsForbiddenWordsCheck = False
        Me.lblCustSubNmL.IsFullByteCheck = 0
        Me.lblCustSubNmL.IsHankakuCheck = False
        Me.lblCustSubNmL.IsHissuCheck = False
        Me.lblCustSubNmL.IsKanaCheck = False
        Me.lblCustSubNmL.IsMiddleSpace = False
        Me.lblCustSubNmL.IsNumericCheck = False
        Me.lblCustSubNmL.IsSujiCheck = False
        Me.lblCustSubNmL.IsZenkakuCheck = False
        Me.lblCustSubNmL.ItemName = ""
        Me.lblCustSubNmL.LineSpace = 0
        Me.lblCustSubNmL.Location = New System.Drawing.Point(237, 765)
        Me.lblCustSubNmL.MaxLength = 60
        Me.lblCustSubNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustSubNmL.MaxLineCount = 0
        Me.lblCustSubNmL.Multiline = False
        Me.lblCustSubNmL.Name = "lblCustSubNmL"
        Me.lblCustSubNmL.ReadOnly = True
        Me.lblCustSubNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustSubNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustSubNmL.Size = New System.Drawing.Size(456, 18)
        Me.lblCustSubNmL.TabIndex = 634
        Me.lblCustSubNmL.TabStop = False
        Me.lblCustSubNmL.TabStopSetting = False
        Me.lblCustSubNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustSubNmL.UseSystemPasswordChar = False
        Me.lblCustSubNmL.WidthDef = 456
        Me.lblCustSubNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustSubCdL
        '
        Me.lblCustSubCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustSubCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustSubCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustSubCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustSubCdL.CountWrappedLine = False
        Me.lblCustSubCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustSubCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustSubCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustSubCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustSubCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustSubCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustSubCdL.HeightDef = 18
        Me.lblCustSubCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustSubCdL.HissuLabelVisible = False
        Me.lblCustSubCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.lblCustSubCdL.IsByteCheck = 5
        Me.lblCustSubCdL.IsCalendarCheck = False
        Me.lblCustSubCdL.IsDakutenCheck = False
        Me.lblCustSubCdL.IsEisuCheck = False
        Me.lblCustSubCdL.IsForbiddenWordsCheck = False
        Me.lblCustSubCdL.IsFullByteCheck = 0
        Me.lblCustSubCdL.IsHankakuCheck = False
        Me.lblCustSubCdL.IsHissuCheck = False
        Me.lblCustSubCdL.IsKanaCheck = False
        Me.lblCustSubCdL.IsMiddleSpace = False
        Me.lblCustSubCdL.IsNumericCheck = False
        Me.lblCustSubCdL.IsSujiCheck = False
        Me.lblCustSubCdL.IsZenkakuCheck = False
        Me.lblCustSubCdL.ItemName = ""
        Me.lblCustSubCdL.LineSpace = 0
        Me.lblCustSubCdL.Location = New System.Drawing.Point(188, 765)
        Me.lblCustSubCdL.MaxLength = 5
        Me.lblCustSubCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustSubCdL.MaxLineCount = 0
        Me.lblCustSubCdL.Multiline = False
        Me.lblCustSubCdL.Name = "lblCustSubCdL"
        Me.lblCustSubCdL.ReadOnly = True
        Me.lblCustSubCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustSubCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustSubCdL.Size = New System.Drawing.Size(65, 18)
        Me.lblCustSubCdL.TabIndex = 633
        Me.lblCustSubCdL.TabStop = False
        Me.lblCustSubCdL.TabStopSetting = False
        Me.lblCustSubCdL.TextValue = "XXXXX"
        Me.lblCustSubCdL.UseSystemPasswordChar = False
        Me.lblCustSubCdL.WidthDef = 65
        Me.lblCustSubCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMM030F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.FocusedControlName = "LMImTextBox"
        Me.Name = "LMM030F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSpl As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleInvYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleRyak As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleInvT As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleFlwp As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleZei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numSagyoUp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtSagyoRyak As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRem As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSagyoUpCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKosu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents TitlelblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSplRpt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbFlwpYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbInvTani As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbKosuBai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbZeiKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbInvYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblWHSagyoYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbWHSagyoYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtWhSagyoRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSijiRemark As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtWhSagyoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSijiSagyoNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyoSub As Win.LMTitleLabel
    Friend WithEvents txtSagyoSubCd As Win.InputMan.LMImTextBox
    Friend WithEvents sprDetail_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents lblSagyoSubNm As Win.InputMan.LMImTextBox
    Friend WithEvents lblCustSubNmL As Win.InputMan.LMImTextBox
    Friend WithEvents lblCustSubCdL As Win.InputMan.LMImTextBox
End Class
