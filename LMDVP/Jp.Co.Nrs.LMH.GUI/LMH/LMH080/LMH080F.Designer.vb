<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMH080F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMH080F))
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.sprEdiList = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpSTATUS = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.optMikakunin = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optKakuninZumi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optSoshinZumi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optSakujoZumi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optAll = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.lblStatus = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdEdiDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.imdEdiDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprEdiList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSTATUS.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblCustNM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD_M)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD_L)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.imdEdiDateTo)
        Me.pnlViewAria.Controls.Add(Me.imdEdiDateFrom)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.grpSTATUS)
        Me.pnlViewAria.Controls.Add(Me.lblStatus)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCust)
        Me.pnlViewAria.Controls.Add(Me.sprEdiList)
        '
        'sprEdiList
        '
        Me.sprEdiList.AccessibleDescription = ""
        Me.sprEdiList.AllowUserZoom = False
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
        Me.sprEdiList.HeightDef = 770
        Me.sprEdiList.KeyboardCheckBoxOn = False
        Me.sprEdiList.Location = New System.Drawing.Point(12, 91)
        Me.sprEdiList.Name = "sprEdiList"
        Me.sprEdiList.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprEdiList.ScrollBarMaxAlign = False
        Me.sprEdiList.SetViewportTopRow(0, 0, 1)
        Me.sprEdiList.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprEdiList.SetViewportTopRow(0, 0, 1)
        Me.sprEdiList.SetActiveViewport(0, -1, 0)
        Me.sprEdiList.Size = New System.Drawing.Size(1250, 770)
        Me.sprEdiList.SortColumn = True
        Me.sprEdiList.SpanColumnLock = True
        Me.sprEdiList.SpreadDoubleClicked = False
        Me.sprEdiList.TabIndex = 15
        Me.sprEdiList.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprEdiList.TextValue = Nothing
        Me.sprEdiList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprEdiList.WidthDef = 1250
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
        Me.lblTitleCust.Location = New System.Drawing.Point(472, 17)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 144
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(27, 51)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleEigyo.TabIndex = 141
        Me.lblTitleEigyo.Text = "データ取込日"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "データ取込日"
        Me.lblTitleEigyo.WidthDef = 91
        '
        'grpSTATUS
        '
        Me.grpSTATUS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSTATUS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSTATUS.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSTATUS.Controls.Add(Me.optMikakunin)
        Me.grpSTATUS.Controls.Add(Me.optKakuninZumi)
        Me.grpSTATUS.Controls.Add(Me.optSoshinZumi)
        Me.grpSTATUS.Controls.Add(Me.optSakujoZumi)
        Me.grpSTATUS.Controls.Add(Me.optAll)
        Me.grpSTATUS.EnableStatus = False
        Me.grpSTATUS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSTATUS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSTATUS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSTATUS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSTATUS.HeightDef = 34
        Me.grpSTATUS.Location = New System.Drawing.Point(523, 40)
        Me.grpSTATUS.Name = "grpSTATUS"
        Me.grpSTATUS.Size = New System.Drawing.Size(402, 34)
        Me.grpSTATUS.TabIndex = 191
        Me.grpSTATUS.TabStop = False
        Me.grpSTATUS.TextValue = ""
        Me.grpSTATUS.WidthDef = 402
        '
        'optMikakunin
        '
        Me.optMikakunin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optMikakunin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optMikakunin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optMikakunin.EnableStatus = True
        Me.optMikakunin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optMikakunin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optMikakunin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optMikakunin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optMikakunin.HeightDef = 18
        Me.optMikakunin.Location = New System.Drawing.Point(6, 10)
        Me.optMikakunin.Name = "optMikakunin"
        Me.optMikakunin.Size = New System.Drawing.Size(67, 18)
        Me.optMikakunin.TabIndex = 326
        Me.optMikakunin.TabStop = True
        Me.optMikakunin.TabStopSetting = True
        Me.optMikakunin.Text = "未確認"
        Me.optMikakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optMikakunin.TextValue = "未確認"
        Me.optMikakunin.UseVisualStyleBackColor = True
        Me.optMikakunin.WidthDef = 67
        '
        'optKakuninZumi
        '
        Me.optKakuninZumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKakuninZumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKakuninZumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKakuninZumi.EnableStatus = True
        Me.optKakuninZumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKakuninZumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKakuninZumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKakuninZumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKakuninZumi.HeightDef = 18
        Me.optKakuninZumi.Location = New System.Drawing.Point(79, 11)
        Me.optKakuninZumi.Name = "optKakuninZumi"
        Me.optKakuninZumi.Size = New System.Drawing.Size(81, 18)
        Me.optKakuninZumi.TabIndex = 327
        Me.optKakuninZumi.TabStop = True
        Me.optKakuninZumi.TabStopSetting = True
        Me.optKakuninZumi.Text = "確認済"
        Me.optKakuninZumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKakuninZumi.TextValue = "確認済"
        Me.optKakuninZumi.UseVisualStyleBackColor = True
        Me.optKakuninZumi.WidthDef = 81
        '
        'optSoshinZumi
        '
        Me.optSoshinZumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSoshinZumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSoshinZumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSoshinZumi.EnableStatus = True
        Me.optSoshinZumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSoshinZumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSoshinZumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSoshinZumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSoshinZumi.HeightDef = 18
        Me.optSoshinZumi.Location = New System.Drawing.Point(166, 11)
        Me.optSoshinZumi.Name = "optSoshinZumi"
        Me.optSoshinZumi.Size = New System.Drawing.Size(71, 18)
        Me.optSoshinZumi.TabIndex = 328
        Me.optSoshinZumi.TabStop = True
        Me.optSoshinZumi.TabStopSetting = True
        Me.optSoshinZumi.Text = "送信済"
        Me.optSoshinZumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSoshinZumi.TextValue = "送信済"
        Me.optSoshinZumi.UseVisualStyleBackColor = True
        Me.optSoshinZumi.WidthDef = 71
        '
        'optSakujoZumi
        '
        Me.optSakujoZumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSakujoZumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSakujoZumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSakujoZumi.EnableStatus = True
        Me.optSakujoZumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSakujoZumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSakujoZumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSakujoZumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSakujoZumi.HeightDef = 18
        Me.optSakujoZumi.Location = New System.Drawing.Point(243, 11)
        Me.optSakujoZumi.Name = "optSakujoZumi"
        Me.optSakujoZumi.Size = New System.Drawing.Size(76, 18)
        Me.optSakujoZumi.TabIndex = 329
        Me.optSakujoZumi.TabStop = True
        Me.optSakujoZumi.TabStopSetting = True
        Me.optSakujoZumi.Text = "削除済"
        Me.optSakujoZumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSakujoZumi.TextValue = "削除済"
        Me.optSakujoZumi.UseVisualStyleBackColor = True
        Me.optSakujoZumi.WidthDef = 76
        '
        'optAll
        '
        Me.optAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optAll.EnableStatus = True
        Me.optAll.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.HeightDef = 18
        Me.optAll.Location = New System.Drawing.Point(325, 11)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(53, 18)
        Me.optAll.TabIndex = 330
        Me.optAll.TabStop = True
        Me.optAll.TabStopSetting = True
        Me.optAll.Text = "全部"
        Me.optAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optAll.TextValue = "全部"
        Me.optAll.UseVisualStyleBackColor = True
        Me.optAll.WidthDef = 53
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.AutoSizeDef = True
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStatus.EnableStatus = False
        Me.lblStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.HeightDef = 13
        Me.lblStatus.Location = New System.Drawing.Point(444, 51)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(63, 13)
        Me.lblStatus.TabIndex = 190
        Me.lblStatus.Text = "処理区分"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.TextValue = "処理区分"
        Me.lblStatus.WidthDef = 63
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
        Me.cmbEigyo.Location = New System.Drawing.Point(124, 17)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 207
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(235, 49)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel2.TabIndex = 291
        Me.LmTitleLabel2.Text = "～"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "～"
        Me.LmTitleLabel2.WidthDef = 21
        '
        'imdEdiDateTo
        '
        Me.imdEdiDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdEdiDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdEdiDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdEdiDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdEdiDateTo.HeightDef = 18
        Me.imdEdiDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdEdiDateTo.HissuLabelVisible = False
        Me.imdEdiDateTo.Holiday = True
        Me.imdEdiDateTo.IsAfterDateCheck = False
        Me.imdEdiDateTo.IsBeforeDateCheck = False
        Me.imdEdiDateTo.IsHissuCheck = False
        Me.imdEdiDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdEdiDateTo.ItemName = ""
        Me.imdEdiDateTo.Location = New System.Drawing.Point(262, 46)
        Me.imdEdiDateTo.Name = "imdEdiDateTo"
        Me.imdEdiDateTo.Number = CType(0, Long)
        Me.imdEdiDateTo.ReadOnly = False
        Me.imdEdiDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdEdiDateTo.TabIndex = 290
        Me.imdEdiDateTo.TabStopSetting = True
        Me.imdEdiDateTo.TextValue = ""
        Me.imdEdiDateTo.Value = New Date(CType(0, Long))
        Me.imdEdiDateTo.WidthDef = 118
        '
        'imdEdiDateFrom
        '
        Me.imdEdiDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdEdiDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdEdiDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdEdiDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdEdiDateFrom.HeightDef = 18
        Me.imdEdiDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdEdiDateFrom.HissuLabelVisible = False
        Me.imdEdiDateFrom.Holiday = True
        Me.imdEdiDateFrom.IsAfterDateCheck = False
        Me.imdEdiDateFrom.IsBeforeDateCheck = False
        Me.imdEdiDateFrom.IsHissuCheck = False
        Me.imdEdiDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdEdiDateFrom.ItemName = ""
        Me.imdEdiDateFrom.Location = New System.Drawing.Point(123, 46)
        Me.imdEdiDateFrom.Name = "imdEdiDateFrom"
        Me.imdEdiDateFrom.Number = CType(0, Long)
        Me.imdEdiDateFrom.ReadOnly = False
        Me.imdEdiDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdEdiDateFrom.TabIndex = 288
        Me.imdEdiDateFrom.TabStopSetting = True
        Me.imdEdiDateFrom.TextValue = ""
        Me.imdEdiDateFrom.Value = New Date(CType(0, Long))
        Me.imdEdiDateFrom.WidthDef = 118
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(69, 17)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 292
        Me.LmTitleLabel1.Text = "営業所"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "営業所"
        Me.LmTitleLabel1.WidthDef = 49
        '
        'lblCustNM
        '
        Me.lblCustNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM.CountWrappedLine = False
        Me.lblCustNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM.HeightDef = 18
        Me.lblCustNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.HissuLabelVisible = False
        Me.lblCustNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM.IsByteCheck = 0
        Me.lblCustNM.IsCalendarCheck = False
        Me.lblCustNM.IsDakutenCheck = False
        Me.lblCustNM.IsEisuCheck = False
        Me.lblCustNM.IsForbiddenWordsCheck = False
        Me.lblCustNM.IsFullByteCheck = 0
        Me.lblCustNM.IsHankakuCheck = False
        Me.lblCustNM.IsHissuCheck = False
        Me.lblCustNM.IsKanaCheck = False
        Me.lblCustNM.IsMiddleSpace = False
        Me.lblCustNM.IsNumericCheck = False
        Me.lblCustNM.IsSujiCheck = False
        Me.lblCustNM.IsZenkakuCheck = False
        Me.lblCustNM.ItemName = ""
        Me.lblCustNM.LineSpace = 0
        Me.lblCustNM.Location = New System.Drawing.Point(628, 16)
        Me.lblCustNM.MaxLength = 0
        Me.lblCustNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM.MaxLineCount = 0
        Me.lblCustNM.Multiline = False
        Me.lblCustNM.Name = "lblCustNM"
        Me.lblCustNM.ReadOnly = True
        Me.lblCustNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM.Size = New System.Drawing.Size(465, 18)
        Me.lblCustNM.TabIndex = 294
        Me.lblCustNM.TabStop = False
        Me.lblCustNM.TabStopSetting = False
        Me.lblCustNM.TextValue = ""
        Me.lblCustNM.UseSystemPasswordChar = False
        Me.lblCustNM.WidthDef = 465
        Me.lblCustNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCD_L
        '
        Me.txtCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_L.CountWrappedLine = False
        Me.txtCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_L.HeightDef = 18
        Me.txtCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.HissuLabelVisible = False
        Me.txtCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD_L.IsByteCheck = 5
        Me.txtCustCD_L.IsCalendarCheck = False
        Me.txtCustCD_L.IsDakutenCheck = False
        Me.txtCustCD_L.IsEisuCheck = False
        Me.txtCustCD_L.IsForbiddenWordsCheck = False
        Me.txtCustCD_L.IsFullByteCheck = 0
        Me.txtCustCD_L.IsHankakuCheck = False
        Me.txtCustCD_L.IsHissuCheck = False
        Me.txtCustCD_L.IsKanaCheck = False
        Me.txtCustCD_L.IsMiddleSpace = False
        Me.txtCustCD_L.IsNumericCheck = False
        Me.txtCustCD_L.IsSujiCheck = False
        Me.txtCustCD_L.IsZenkakuCheck = False
        Me.txtCustCD_L.ItemName = ""
        Me.txtCustCD_L.LineSpace = 0
        Me.txtCustCD_L.Location = New System.Drawing.Point(518, 16)
        Me.txtCustCD_L.MaxLength = 5
        Me.txtCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_L.MaxLineCount = 0
        Me.txtCustCD_L.Multiline = False
        Me.txtCustCD_L.Name = "txtCustCD_L"
        Me.txtCustCD_L.ReadOnly = True
        Me.txtCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_L.Size = New System.Drawing.Size(93, 18)
        Me.txtCustCD_L.TabIndex = 293
        Me.txtCustCD_L.TabStop = False
        Me.txtCustCD_L.TabStopSetting = False
        Me.txtCustCD_L.TextValue = ""
        Me.txtCustCD_L.UseSystemPasswordChar = False
        Me.txtCustCD_L.WidthDef = 93
        Me.txtCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCD_M
        '
        Me.txtCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_M.CountWrappedLine = False
        Me.txtCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_M.HeightDef = 18
        Me.txtCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.HissuLabelVisible = False
        Me.txtCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD_M.IsByteCheck = 5
        Me.txtCustCD_M.IsCalendarCheck = False
        Me.txtCustCD_M.IsDakutenCheck = False
        Me.txtCustCD_M.IsEisuCheck = False
        Me.txtCustCD_M.IsForbiddenWordsCheck = False
        Me.txtCustCD_M.IsFullByteCheck = 0
        Me.txtCustCD_M.IsHankakuCheck = False
        Me.txtCustCD_M.IsHissuCheck = False
        Me.txtCustCD_M.IsKanaCheck = False
        Me.txtCustCD_M.IsMiddleSpace = False
        Me.txtCustCD_M.IsNumericCheck = False
        Me.txtCustCD_M.IsSujiCheck = False
        Me.txtCustCD_M.IsZenkakuCheck = False
        Me.txtCustCD_M.ItemName = ""
        Me.txtCustCD_M.LineSpace = 0
        Me.txtCustCD_M.Location = New System.Drawing.Point(595, 16)
        Me.txtCustCD_M.MaxLength = 5
        Me.txtCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_M.MaxLineCount = 0
        Me.txtCustCD_M.Multiline = False
        Me.txtCustCD_M.Name = "txtCustCD_M"
        Me.txtCustCD_M.ReadOnly = True
        Me.txtCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_M.Size = New System.Drawing.Size(49, 18)
        Me.txtCustCD_M.TabIndex = 295
        Me.txtCustCD_M.TabStop = False
        Me.txtCustCD_M.TabStopSetting = False
        Me.txtCustCD_M.TextValue = ""
        Me.txtCustCD_M.UseSystemPasswordChar = False
        Me.txtCustCD_M.WidthDef = 49
        Me.txtCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMH080F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMH080F"
        Me.Text = "【LMH080】 東レUTI実績確認データ削除"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprEdiList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSTATUS.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprEdiList As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSTATUS As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblStatus As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdEdiDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdEdiDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents optMikakunin As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optKakuninZumi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optSoshinZumi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optSakujoZumi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optAll As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton

End Class
