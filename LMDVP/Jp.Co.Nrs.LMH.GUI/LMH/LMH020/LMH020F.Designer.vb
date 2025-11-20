<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMH020F
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
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField4 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField4 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMH020F))
        Me.lblTitleNyukaDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdNyukaDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleWh = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitlelblKanriNoL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleToukiHo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtNyubanL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNyubanL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNyukaCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleZenkiHo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleFree = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdHokanDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleHokanDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPlanQtUt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbTax = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleTax = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleIrisuTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbNyukaType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtBuyerOrdNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleBuyerOrdNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNiyakuUmu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtNyukaComment = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNyukaComment = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtOrderNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleOrderNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNyukaKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblKanriNoL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNyukaType = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleShinshokuKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.tabMiddle = New Jp.Co.Nrs.LM.GUI.Win.LMTab
        Me.tabGoods = New System.Windows.Forms.TabPage
        Me.lblSumCntTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleHasuTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblJotai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblGoodsKey = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbOndo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtSerial = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtLot = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleSerial = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleLot = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleIrimeTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numIrime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleIrime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnRevival = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleTareTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numTare = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleTare = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleOndo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHasu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleHasu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKonsuTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numKosu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleKonsu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblKanriNoM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleKanriNoM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.sprGoodsDef = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.txtGoodsComment = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel32 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleGoodsComment = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel34 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblStdIrimeTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtBuyerOrdNoM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleBuyerOrdNoM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numStdIrime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.txtOrderNoM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numIrisu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleOrderNoM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleIrisu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numSumBetu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleStdIrime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblSumAntTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleSumAnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleSumCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numSumAnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numSumCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.tabUnso = New System.Windows.Forms.TabPage
        Me.pnlUnso = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleUnchinTehai = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleYen = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblShukkaMotoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleShukkaMotoCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUnsoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUnso = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtUnsoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleOnkan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbOnkan = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbUnchinTehai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleSharyoKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbSharyoKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtUnsoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtShukkaMotoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.tabFreeL = New System.Windows.Forms.TabPage
        Me.sprFreeL = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.LmImTextBox1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleOrder_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblHikiate_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel60 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel67 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmImNumber1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTARE_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.LmTitleLabel68 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel72 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel73 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtHasu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.LmTitleLabel74 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel78 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtKosu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleKonsu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmImTextBox18 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmImTextBox19 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel80 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtKanriNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel81 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsComment_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel89 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblIrisuTani1_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblStdIrimeTani_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtBuyerOrdNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel92 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblIrisuTani2_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numStdIrime_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.txtOrderNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numIrisu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.LmTitleLabel94 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel95 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel96 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel97 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel98 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel99 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel100 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel101 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numSumAnt_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numSumCnt_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numNyukaCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numPlanQt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numFree = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.tabInka = New Jp.Co.Nrs.LM.GUI.Win.LMTab
        Me.tabNyuka = New System.Windows.Forms.TabPage
        Me.LmTitleLabel24 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbNyukaKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblEdiKanriNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleStatus = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleEdiKanriNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbStatus = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.cmbWh = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
        Me.cmbShinshokuKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbToukiHo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.cmbZenkiHo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.cmbNiyakuUmu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.tabFree = New Jp.Co.Nrs.LM.GUI.Win.LMTab
        Me.tabFreeM = New System.Windows.Forms.TabPage
        Me.sprFreeM = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.LmTitleLabel19 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        Me.tabMiddle.SuspendLayout()
        Me.tabGoods.SuspendLayout()
        CType(Me.sprGoodsDef, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUnso.SuspendLayout()
        Me.pnlUnso.SuspendLayout()
        Me.tabFreeL.SuspendLayout()
        CType(Me.sprFreeL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabInka.SuspendLayout()
        Me.tabNyuka.SuspendLayout()
        Me.tabFree.SuspendLayout()
        Me.tabFreeM.SuspendLayout()
        CType(Me.sprFreeM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.tabFree)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.tabInka)
        Me.pnlViewAria.Controls.Add(Me.tabMiddle)
        '
        'lblTitleNyukaDate
        '
        Me.lblTitleNyukaDate.AutoSize = True
        Me.lblTitleNyukaDate.AutoSizeDef = True
        Me.lblTitleNyukaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaDate.EnableStatus = False
        Me.lblTitleNyukaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaDate.HeightDef = 13
        Me.lblTitleNyukaDate.Location = New System.Drawing.Point(75, 75)
        Me.lblTitleNyukaDate.Name = "lblTitleNyukaDate"
        Me.lblTitleNyukaDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNyukaDate.TabIndex = 8
        Me.lblTitleNyukaDate.Text = "入荷日"
        Me.lblTitleNyukaDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaDate.TextValue = "入荷日"
        Me.lblTitleNyukaDate.WidthDef = 49
        '
        'imdNyukaDate
        '
        Me.imdNyukaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdNyukaDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate.HeightDef = 18
        Me.imdNyukaDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate.HissuLabelVisible = True
        Me.imdNyukaDate.Holiday = False
        Me.imdNyukaDate.IsAfterDateCheck = False
        Me.imdNyukaDate.IsBeforeDateCheck = False
        Me.imdNyukaDate.IsHissuCheck = True
        Me.imdNyukaDate.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate.ItemName = ""
        Me.imdNyukaDate.Location = New System.Drawing.Point(124, 72)
        Me.imdNyukaDate.Name = "imdNyukaDate"
        Me.imdNyukaDate.Number = CType(0, Long)
        Me.imdNyukaDate.ReadOnly = False
        Me.imdNyukaDate.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate.TabIndex = 9
        Me.imdNyukaDate.TabStopSetting = True
        Me.imdNyukaDate.TextValue = ""
        Me.imdNyukaDate.Value = New Date(CType(0, Long))
        Me.imdNyukaDate.WidthDef = 118
        '
        'lblTitleWh
        '
        Me.lblTitleWh.AutoSize = True
        Me.lblTitleWh.AutoSizeDef = True
        Me.lblTitleWh.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWh.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWh.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWh.EnableStatus = False
        Me.lblTitleWh.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWh.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWh.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWh.HeightDef = 13
        Me.lblTitleWh.Location = New System.Drawing.Point(842, 33)
        Me.lblTitleWh.Name = "lblTitleWh"
        Me.lblTitleWh.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleWh.TabIndex = 3
        Me.lblTitleWh.Text = "倉庫"
        Me.lblTitleWh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWh.TextValue = "倉庫"
        Me.lblTitleWh.WidthDef = 35
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(473, 33)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 3
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblTitlelblKanriNoL
        '
        Me.lblTitlelblKanriNoL.AutoSize = True
        Me.lblTitlelblKanriNoL.AutoSizeDef = True
        Me.lblTitlelblKanriNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlelblKanriNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlelblKanriNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlelblKanriNoL.EnableStatus = False
        Me.lblTitlelblKanriNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlelblKanriNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlelblKanriNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlelblKanriNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlelblKanriNoL.HeightDef = 13
        Me.lblTitlelblKanriNoL.Location = New System.Drawing.Point(5, 33)
        Me.lblTitlelblKanriNoL.Name = "lblTitlelblKanriNoL"
        Me.lblTitlelblKanriNoL.Size = New System.Drawing.Size(119, 13)
        Me.lblTitlelblKanriNoL.TabIndex = 5
        Me.lblTitlelblKanriNoL.Text = "入荷管理番号(大)"
        Me.lblTitlelblKanriNoL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlelblKanriNoL.TextValue = "入荷管理番号(大)"
        Me.lblTitlelblKanriNoL.WidthDef = 119
        '
        'lblTitleToukiHo
        '
        Me.lblTitleToukiHo.AutoSize = True
        Me.lblTitleToukiHo.AutoSizeDef = True
        Me.lblTitleToukiHo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleToukiHo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleToukiHo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleToukiHo.EnableStatus = False
        Me.lblTitleToukiHo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleToukiHo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleToukiHo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleToukiHo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleToukiHo.HeightDef = 13
        Me.lblTitleToukiHo.Location = New System.Drawing.Point(47, 138)
        Me.lblTitleToukiHo.Name = "lblTitleToukiHo"
        Me.lblTitleToukiHo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleToukiHo.TabIndex = 90
        Me.lblTitleToukiHo.Text = "当期保管料"
        Me.lblTitleToukiHo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleToukiHo.TextValue = "当期保管料"
        Me.lblTitleToukiHo.WidthDef = 77
        '
        'txtNyubanL
        '
        Me.txtNyubanL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyubanL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyubanL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNyubanL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNyubanL.CountWrappedLine = False
        Me.txtNyubanL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNyubanL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyubanL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyubanL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyubanL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyubanL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNyubanL.HeightDef = 18
        Me.txtNyubanL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNyubanL.HissuLabelVisible = False
        Me.txtNyubanL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtNyubanL.IsByteCheck = 15
        Me.txtNyubanL.IsCalendarCheck = False
        Me.txtNyubanL.IsDakutenCheck = False
        Me.txtNyubanL.IsEisuCheck = False
        Me.txtNyubanL.IsForbiddenWordsCheck = False
        Me.txtNyubanL.IsFullByteCheck = 0
        Me.txtNyubanL.IsHankakuCheck = False
        Me.txtNyubanL.IsHissuCheck = False
        Me.txtNyubanL.IsKanaCheck = False
        Me.txtNyubanL.IsMiddleSpace = False
        Me.txtNyubanL.IsNumericCheck = False
        Me.txtNyubanL.IsSujiCheck = False
        Me.txtNyubanL.IsZenkakuCheck = False
        Me.txtNyubanL.ItemName = ""
        Me.txtNyubanL.LineSpace = 0
        Me.txtNyubanL.Location = New System.Drawing.Point(877, 156)
        Me.txtNyubanL.MaxLength = 15
        Me.txtNyubanL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNyubanL.MaxLineCount = 0
        Me.txtNyubanL.Multiline = False
        Me.txtNyubanL.Name = "txtNyubanL"
        Me.txtNyubanL.ReadOnly = False
        Me.txtNyubanL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNyubanL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNyubanL.Size = New System.Drawing.Size(156, 18)
        Me.txtNyubanL.TabIndex = 69
        Me.txtNyubanL.TabStopSetting = True
        Me.txtNyubanL.TextValue = "X---10---XX-5-X"
        Me.txtNyubanL.UseSystemPasswordChar = False
        Me.txtNyubanL.WidthDef = 156
        Me.txtNyubanL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNyubanL
        '
        Me.lblTitleNyubanL.AutoSize = True
        Me.lblTitleNyubanL.AutoSizeDef = True
        Me.lblTitleNyubanL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyubanL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyubanL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyubanL.EnableStatus = False
        Me.lblTitleNyubanL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyubanL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyubanL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyubanL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyubanL.HeightDef = 13
        Me.lblTitleNyubanL.Location = New System.Drawing.Point(786, 159)
        Me.lblTitleNyubanL.Name = "lblTitleNyubanL"
        Me.lblTitleNyubanL.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleNyubanL.TabIndex = 68
        Me.lblTitleNyubanL.Text = "備考大(社外)"
        Me.lblTitleNyubanL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyubanL.TextValue = "備考大(社外)"
        Me.lblTitleNyubanL.WidthDef = 91
        '
        'lblTitleNyukaCnt
        '
        Me.lblTitleNyukaCnt.AutoSize = True
        Me.lblTitleNyukaCnt.AutoSizeDef = True
        Me.lblTitleNyukaCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaCnt.EnableStatus = False
        Me.lblTitleNyukaCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaCnt.HeightDef = 13
        Me.lblTitleNyukaCnt.Location = New System.Drawing.Point(445, 159)
        Me.lblTitleNyukaCnt.Name = "lblTitleNyukaCnt"
        Me.lblTitleNyukaCnt.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleNyukaCnt.TabIndex = 65
        Me.lblTitleNyukaCnt.Text = "予定総個数"
        Me.lblTitleNyukaCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaCnt.TextValue = "予定総個数"
        Me.lblTitleNyukaCnt.WidthDef = 77
        '
        'lblTitleZenkiHo
        '
        Me.lblTitleZenkiHo.AutoSize = True
        Me.lblTitleZenkiHo.AutoSizeDef = True
        Me.lblTitleZenkiHo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZenkiHo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZenkiHo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZenkiHo.EnableStatus = False
        Me.lblTitleZenkiHo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZenkiHo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZenkiHo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZenkiHo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZenkiHo.HeightDef = 13
        Me.lblTitleZenkiHo.Location = New System.Drawing.Point(445, 138)
        Me.lblTitleZenkiHo.Name = "lblTitleZenkiHo"
        Me.lblTitleZenkiHo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleZenkiHo.TabIndex = 62
        Me.lblTitleZenkiHo.Text = "全期保管料"
        Me.lblTitleZenkiHo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZenkiHo.TextValue = "全期保管料"
        Me.lblTitleZenkiHo.WidthDef = 77
        '
        'lblTitleFree
        '
        Me.lblTitleFree.AutoSize = True
        Me.lblTitleFree.AutoSizeDef = True
        Me.lblTitleFree.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFree.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFree.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFree.EnableStatus = False
        Me.lblTitleFree.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFree.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFree.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFree.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFree.HeightDef = 13
        Me.lblTitleFree.Location = New System.Drawing.Point(403, 96)
        Me.lblTitleFree.Name = "lblTitleFree"
        Me.lblTitleFree.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleFree.TabIndex = 59
        Me.lblTitleFree.Text = "保管料フリー期間"
        Me.lblTitleFree.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFree.TextValue = "保管料フリー期間"
        Me.lblTitleFree.WidthDef = 119
        '
        'imdHokanDate
        '
        Me.imdHokanDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokanDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokanDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdHokanDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField7.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = "/"
        DateDayDisplayField4.ShowLeadingZero = True
        Me.imdHokanDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField7, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField8, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdHokanDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokanDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokanDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokanDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokanDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField4, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdHokanDate.HeightDef = 18
        Me.imdHokanDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdHokanDate.HissuLabelVisible = False
        Me.imdHokanDate.Holiday = False
        Me.imdHokanDate.IsAfterDateCheck = False
        Me.imdHokanDate.IsBeforeDateCheck = False
        Me.imdHokanDate.IsHissuCheck = False
        Me.imdHokanDate.IsMinDateCheck = "1900/01/01"
        Me.imdHokanDate.ItemName = ""
        Me.imdHokanDate.Location = New System.Drawing.Point(877, 93)
        Me.imdHokanDate.Name = "imdHokanDate"
        Me.imdHokanDate.Number = CType(0, Long)
        Me.imdHokanDate.ReadOnly = False
        Me.imdHokanDate.Size = New System.Drawing.Size(117, 18)
        Me.imdHokanDate.TabIndex = 58
        Me.imdHokanDate.TabStopSetting = True
        Me.imdHokanDate.TextValue = ""
        Me.imdHokanDate.Value = New Date(CType(0, Long))
        Me.imdHokanDate.WidthDef = 117
        '
        'lblTitleHokanDate
        '
        Me.lblTitleHokanDate.AutoSize = True
        Me.lblTitleHokanDate.AutoSizeDef = True
        Me.lblTitleHokanDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanDate.EnableStatus = False
        Me.lblTitleHokanDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanDate.HeightDef = 13
        Me.lblTitleHokanDate.Location = New System.Drawing.Point(786, 96)
        Me.lblTitleHokanDate.Name = "lblTitleHokanDate"
        Me.lblTitleHokanDate.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleHokanDate.TabIndex = 57
        Me.lblTitleHokanDate.Text = "保管料起算日"
        Me.lblTitleHokanDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanDate.TextValue = "保管料起算日"
        Me.lblTitleHokanDate.WidthDef = 91
        '
        'cmbPlanQtUt
        '
        Me.cmbPlanQtUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPlanQtUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPlanQtUt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPlanQtUt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPlanQtUt.DataCode = "N001"
        Me.cmbPlanQtUt.DataSource = Nothing
        Me.cmbPlanQtUt.DisplayMember = Nothing
        Me.cmbPlanQtUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPlanQtUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPlanQtUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPlanQtUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPlanQtUt.HeightDef = 18
        Me.cmbPlanQtUt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPlanQtUt.HissuLabelVisible = False
        Me.cmbPlanQtUt.InsertWildCard = True
        Me.cmbPlanQtUt.IsForbiddenWordsCheck = False
        Me.cmbPlanQtUt.IsHissuCheck = False
        Me.cmbPlanQtUt.ItemName = ""
        Me.cmbPlanQtUt.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPlanQtUt.Location = New System.Drawing.Point(260, 156)
        Me.cmbPlanQtUt.Name = "cmbPlanQtUt"
        Me.cmbPlanQtUt.ReadOnly = False
        Me.cmbPlanQtUt.SelectedIndex = -1
        Me.cmbPlanQtUt.SelectedItem = Nothing
        Me.cmbPlanQtUt.SelectedText = ""
        Me.cmbPlanQtUt.SelectedValue = ""
        Me.cmbPlanQtUt.Size = New System.Drawing.Size(163, 18)
        Me.cmbPlanQtUt.TabIndex = 52
        Me.cmbPlanQtUt.TabStopSetting = True
        Me.cmbPlanQtUt.TextValue = ""
        Me.cmbPlanQtUt.Value1 = Nothing
        Me.cmbPlanQtUt.Value2 = Nothing
        Me.cmbPlanQtUt.Value3 = Nothing
        Me.cmbPlanQtUt.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPlanQtUt.ValueMember = Nothing
        Me.cmbPlanQtUt.WidthDef = 163
        '
        'cmbTax
        '
        Me.cmbTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTax.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTax.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTax.DataCode = "Z001"
        Me.cmbTax.DataSource = Nothing
        Me.cmbTax.DisplayMember = Nothing
        Me.cmbTax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTax.HeightDef = 18
        Me.cmbTax.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTax.HissuLabelVisible = False
        Me.cmbTax.InsertWildCard = True
        Me.cmbTax.IsForbiddenWordsCheck = False
        Me.cmbTax.IsHissuCheck = False
        Me.cmbTax.ItemName = ""
        Me.cmbTax.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTax.Location = New System.Drawing.Point(877, 114)
        Me.cmbTax.Name = "cmbTax"
        Me.cmbTax.ReadOnly = False
        Me.cmbTax.SelectedIndex = -1
        Me.cmbTax.SelectedItem = Nothing
        Me.cmbTax.SelectedText = ""
        Me.cmbTax.SelectedValue = ""
        Me.cmbTax.Size = New System.Drawing.Size(93, 18)
        Me.cmbTax.TabIndex = 49
        Me.cmbTax.TabStopSetting = True
        Me.cmbTax.TextValue = ""
        Me.cmbTax.Value1 = Nothing
        Me.cmbTax.Value2 = Nothing
        Me.cmbTax.Value3 = Nothing
        Me.cmbTax.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbTax.ValueMember = Nothing
        Me.cmbTax.WidthDef = 93
        '
        'lblTitleTax
        '
        Me.lblTitleTax.AutoSize = True
        Me.lblTitleTax.AutoSizeDef = True
        Me.lblTitleTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTax.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTax.EnableStatus = False
        Me.lblTitleTax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTax.HeightDef = 13
        Me.lblTitleTax.Location = New System.Drawing.Point(814, 117)
        Me.lblTitleTax.Name = "lblTitleTax"
        Me.lblTitleTax.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTax.TabIndex = 48
        Me.lblTitleTax.Text = "課税区分"
        Me.lblTitleTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTax.TextValue = "課税区分"
        Me.lblTitleTax.WidthDef = 63
        '
        'lblTitleIrisuTani
        '
        Me.lblTitleIrisuTani.AutoSize = True
        Me.lblTitleIrisuTani.AutoSizeDef = True
        Me.lblTitleIrisuTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrisuTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrisuTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrisuTani.EnableStatus = False
        Me.lblTitleIrisuTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrisuTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrisuTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrisuTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrisuTani.HeightDef = 13
        Me.lblTitleIrisuTani.Location = New System.Drawing.Point(26, 159)
        Me.lblTitleIrisuTani.Name = "lblTitleIrisuTani"
        Me.lblTitleIrisuTani.Size = New System.Drawing.Size(98, 13)
        Me.lblTitleIrisuTani.TabIndex = 47
        Me.lblTitleIrisuTani.Text = "予定数量/単位"
        Me.lblTitleIrisuTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrisuTani.TextValue = "予定数量/単位"
        Me.lblTitleIrisuTani.WidthDef = 98
        '
        'cmbNyukaType
        '
        Me.cmbNyukaType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNyukaType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNyukaType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNyukaType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNyukaType.DataCode = "N007"
        Me.cmbNyukaType.DataSource = Nothing
        Me.cmbNyukaType.DisplayMember = Nothing
        Me.cmbNyukaType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNyukaType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNyukaType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNyukaType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNyukaType.HeightDef = 18
        Me.cmbNyukaType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNyukaType.HissuLabelVisible = False
        Me.cmbNyukaType.InsertWildCard = True
        Me.cmbNyukaType.IsForbiddenWordsCheck = False
        Me.cmbNyukaType.IsHissuCheck = False
        Me.cmbNyukaType.ItemName = ""
        Me.cmbNyukaType.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNyukaType.Location = New System.Drawing.Point(124, 51)
        Me.cmbNyukaType.Name = "cmbNyukaType"
        Me.cmbNyukaType.ReadOnly = False
        Me.cmbNyukaType.SelectedIndex = -1
        Me.cmbNyukaType.SelectedItem = Nothing
        Me.cmbNyukaType.SelectedText = ""
        Me.cmbNyukaType.SelectedValue = ""
        Me.cmbNyukaType.Size = New System.Drawing.Size(118, 18)
        Me.cmbNyukaType.TabIndex = 41
        Me.cmbNyukaType.TabStopSetting = True
        Me.cmbNyukaType.TextValue = ""
        Me.cmbNyukaType.Value1 = Nothing
        Me.cmbNyukaType.Value2 = Nothing
        Me.cmbNyukaType.Value3 = Nothing
        Me.cmbNyukaType.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbNyukaType.ValueMember = Nothing
        Me.cmbNyukaType.WidthDef = 118
        '
        'txtBuyerOrdNo
        '
        Me.txtBuyerOrdNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuyerOrdNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuyerOrdNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerOrdNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBuyerOrdNo.CountWrappedLine = False
        Me.txtBuyerOrdNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtBuyerOrdNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtBuyerOrdNo.HeightDef = 18
        Me.txtBuyerOrdNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNo.HissuLabelVisible = False
        Me.txtBuyerOrdNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtBuyerOrdNo.IsByteCheck = 30
        Me.txtBuyerOrdNo.IsCalendarCheck = False
        Me.txtBuyerOrdNo.IsDakutenCheck = False
        Me.txtBuyerOrdNo.IsEisuCheck = False
        Me.txtBuyerOrdNo.IsForbiddenWordsCheck = False
        Me.txtBuyerOrdNo.IsFullByteCheck = 0
        Me.txtBuyerOrdNo.IsHankakuCheck = False
        Me.txtBuyerOrdNo.IsHissuCheck = False
        Me.txtBuyerOrdNo.IsKanaCheck = False
        Me.txtBuyerOrdNo.IsMiddleSpace = False
        Me.txtBuyerOrdNo.IsNumericCheck = False
        Me.txtBuyerOrdNo.IsSujiCheck = False
        Me.txtBuyerOrdNo.IsZenkakuCheck = False
        Me.txtBuyerOrdNo.ItemName = ""
        Me.txtBuyerOrdNo.LineSpace = 0
        Me.txtBuyerOrdNo.Location = New System.Drawing.Point(522, 72)
        Me.txtBuyerOrdNo.MaxLength = 30
        Me.txtBuyerOrdNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBuyerOrdNo.MaxLineCount = 0
        Me.txtBuyerOrdNo.Multiline = False
        Me.txtBuyerOrdNo.Name = "txtBuyerOrdNo"
        Me.txtBuyerOrdNo.ReadOnly = False
        Me.txtBuyerOrdNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBuyerOrdNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBuyerOrdNo.Size = New System.Drawing.Size(252, 18)
        Me.txtBuyerOrdNo.TabIndex = 40
        Me.txtBuyerOrdNo.TabStopSetting = True
        Me.txtBuyerOrdNo.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtBuyerOrdNo.UseSystemPasswordChar = False
        Me.txtBuyerOrdNo.WidthDef = 252
        Me.txtBuyerOrdNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleBuyerOrdNo
        '
        Me.lblTitleBuyerOrdNo.AutoSize = True
        Me.lblTitleBuyerOrdNo.AutoSizeDef = True
        Me.lblTitleBuyerOrdNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBuyerOrdNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBuyerOrdNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBuyerOrdNo.EnableStatus = False
        Me.lblTitleBuyerOrdNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBuyerOrdNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBuyerOrdNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBuyerOrdNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBuyerOrdNo.HeightDef = 13
        Me.lblTitleBuyerOrdNo.Location = New System.Drawing.Point(459, 75)
        Me.lblTitleBuyerOrdNo.Name = "lblTitleBuyerOrdNo"
        Me.lblTitleBuyerOrdNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleBuyerOrdNo.TabIndex = 39
        Me.lblTitleBuyerOrdNo.Text = "注文番号"
        Me.lblTitleBuyerOrdNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBuyerOrdNo.TextValue = "注文番号"
        Me.lblTitleBuyerOrdNo.WidthDef = 63
        '
        'lblTitleNiyakuUmu
        '
        Me.lblTitleNiyakuUmu.AutoSize = True
        Me.lblTitleNiyakuUmu.AutoSizeDef = True
        Me.lblTitleNiyakuUmu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuUmu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuUmu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuUmu.EnableStatus = False
        Me.lblTitleNiyakuUmu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuUmu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuUmu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuUmu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuUmu.HeightDef = 13
        Me.lblTitleNiyakuUmu.Location = New System.Drawing.Point(828, 138)
        Me.lblTitleNiyakuUmu.Name = "lblTitleNiyakuUmu"
        Me.lblTitleNiyakuUmu.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNiyakuUmu.TabIndex = 37
        Me.lblTitleNiyakuUmu.Text = "荷役料"
        Me.lblTitleNiyakuUmu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuUmu.TextValue = "荷役料"
        Me.lblTitleNiyakuUmu.WidthDef = 49
        '
        'txtNyukaComment
        '
        Me.txtNyukaComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyukaComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyukaComment.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNyukaComment.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNyukaComment.CountWrappedLine = False
        Me.txtNyukaComment.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNyukaComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukaComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukaComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyukaComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyukaComment.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNyukaComment.HeightDef = 18
        Me.txtNyukaComment.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNyukaComment.HissuLabelVisible = False
        Me.txtNyukaComment.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtNyukaComment.IsByteCheck = 100
        Me.txtNyukaComment.IsCalendarCheck = False
        Me.txtNyukaComment.IsDakutenCheck = False
        Me.txtNyukaComment.IsEisuCheck = False
        Me.txtNyukaComment.IsForbiddenWordsCheck = False
        Me.txtNyukaComment.IsFullByteCheck = 0
        Me.txtNyukaComment.IsHankakuCheck = False
        Me.txtNyukaComment.IsHissuCheck = False
        Me.txtNyukaComment.IsKanaCheck = False
        Me.txtNyukaComment.IsMiddleSpace = False
        Me.txtNyukaComment.IsNumericCheck = False
        Me.txtNyukaComment.IsSujiCheck = False
        Me.txtNyukaComment.IsZenkakuCheck = False
        Me.txtNyukaComment.ItemName = ""
        Me.txtNyukaComment.LineSpace = 0
        Me.txtNyukaComment.Location = New System.Drawing.Point(124, 177)
        Me.txtNyukaComment.MaxLength = 100
        Me.txtNyukaComment.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNyukaComment.MaxLineCount = 0
        Me.txtNyukaComment.Multiline = False
        Me.txtNyukaComment.Name = "txtNyukaComment"
        Me.txtNyukaComment.ReadOnly = False
        Me.txtNyukaComment.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNyukaComment.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNyukaComment.Size = New System.Drawing.Size(909, 18)
        Me.txtNyukaComment.TabIndex = 34
        Me.txtNyukaComment.TabStopSetting = True
        Me.txtNyukaComment.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtNyukaComment.UseSystemPasswordChar = False
        Me.txtNyukaComment.WidthDef = 909
        Me.txtNyukaComment.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNyukaComment
        '
        Me.lblTitleNyukaComment.AutoSize = True
        Me.lblTitleNyukaComment.AutoSizeDef = True
        Me.lblTitleNyukaComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaComment.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaComment.EnableStatus = False
        Me.lblTitleNyukaComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaComment.HeightDef = 13
        Me.lblTitleNyukaComment.Location = New System.Drawing.Point(33, 179)
        Me.lblTitleNyukaComment.Name = "lblTitleNyukaComment"
        Me.lblTitleNyukaComment.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleNyukaComment.TabIndex = 33
        Me.lblTitleNyukaComment.Text = "備考大(社内)"
        Me.lblTitleNyukaComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaComment.TextValue = "備考大(社内)"
        Me.lblTitleNyukaComment.WidthDef = 91
        '
        'lblCustNm
        '
        Me.lblCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNm.CountWrappedLine = False
        Me.lblCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNm.HeightDef = 18
        Me.lblCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.HissuLabelVisible = False
        Me.lblCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNm.IsByteCheck = 0
        Me.lblCustNm.IsCalendarCheck = False
        Me.lblCustNm.IsDakutenCheck = False
        Me.lblCustNm.IsEisuCheck = False
        Me.lblCustNm.IsForbiddenWordsCheck = False
        Me.lblCustNm.IsFullByteCheck = 0
        Me.lblCustNm.IsHankakuCheck = False
        Me.lblCustNm.IsHissuCheck = False
        Me.lblCustNm.IsKanaCheck = False
        Me.lblCustNm.IsMiddleSpace = False
        Me.lblCustNm.IsNumericCheck = False
        Me.lblCustNm.IsSujiCheck = False
        Me.lblCustNm.IsZenkakuCheck = False
        Me.lblCustNm.ItemName = ""
        Me.lblCustNm.LineSpace = 0
        Me.lblCustNm.Location = New System.Drawing.Point(214, 114)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNm.TabIndex = 28
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 473
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustCdM.IsByteCheck = 0
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
        Me.txtCustCdM.Location = New System.Drawing.Point(178, 114)
        Me.txtCustCdM.MaxLength = 0
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = True
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 27
        Me.txtCustCdM.TabStop = False
        Me.txtCustCdM.TabStopSetting = False
        Me.txtCustCdM.TextValue = "XXX"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(89, 117)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 25
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrderNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrderNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNo.CountWrappedLine = False
        Me.txtOrderNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNo.HeightDef = 18
        Me.txtOrderNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNo.HissuLabelVisible = False
        Me.txtOrderNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtOrderNo.IsByteCheck = 30
        Me.txtOrderNo.IsCalendarCheck = False
        Me.txtOrderNo.IsDakutenCheck = False
        Me.txtOrderNo.IsEisuCheck = False
        Me.txtOrderNo.IsForbiddenWordsCheck = False
        Me.txtOrderNo.IsFullByteCheck = 0
        Me.txtOrderNo.IsHankakuCheck = False
        Me.txtOrderNo.IsHissuCheck = False
        Me.txtOrderNo.IsKanaCheck = False
        Me.txtOrderNo.IsMiddleSpace = False
        Me.txtOrderNo.IsNumericCheck = False
        Me.txtOrderNo.IsSujiCheck = False
        Me.txtOrderNo.IsZenkakuCheck = False
        Me.txtOrderNo.ItemName = ""
        Me.txtOrderNo.LineSpace = 0
        Me.txtOrderNo.Location = New System.Drawing.Point(124, 93)
        Me.txtOrderNo.MaxLength = 30
        Me.txtOrderNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNo.MaxLineCount = 0
        Me.txtOrderNo.Multiline = False
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.ReadOnly = False
        Me.txtOrderNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNo.Size = New System.Drawing.Size(265, 18)
        Me.txtOrderNo.TabIndex = 24
        Me.txtOrderNo.TabStopSetting = True
        Me.txtOrderNo.Tag = ""
        Me.txtOrderNo.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtOrderNo.UseSystemPasswordChar = False
        Me.txtOrderNo.WidthDef = 265
        Me.txtOrderNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrderNo
        '
        Me.lblTitleOrderNo.AutoSize = True
        Me.lblTitleOrderNo.AutoSizeDef = True
        Me.lblTitleOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrderNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrderNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrderNo.EnableStatus = False
        Me.lblTitleOrderNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrderNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrderNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrderNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrderNo.HeightDef = 13
        Me.lblTitleOrderNo.Location = New System.Drawing.Point(33, 96)
        Me.lblTitleOrderNo.Name = "lblTitleOrderNo"
        Me.lblTitleOrderNo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleOrderNo.TabIndex = 23
        Me.lblTitleOrderNo.Text = "オーダー番号"
        Me.lblTitleOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrderNo.TextValue = "オーダー番号"
        Me.lblTitleOrderNo.WidthDef = 91
        '
        'lblTitleNyukaKbn
        '
        Me.lblTitleNyukaKbn.AutoSize = True
        Me.lblTitleNyukaKbn.AutoSizeDef = True
        Me.lblTitleNyukaKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaKbn.EnableStatus = False
        Me.lblTitleNyukaKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaKbn.HeightDef = 13
        Me.lblTitleNyukaKbn.Location = New System.Drawing.Point(459, 54)
        Me.lblTitleNyukaKbn.Name = "lblTitleNyukaKbn"
        Me.lblTitleNyukaKbn.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleNyukaKbn.TabIndex = 21
        Me.lblTitleNyukaKbn.Text = "入荷区分"
        Me.lblTitleNyukaKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaKbn.TextValue = "入荷区分"
        Me.lblTitleNyukaKbn.WidthDef = 63
        '
        'lblKanriNoL
        '
        Me.lblKanriNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKanriNoL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKanriNoL.CountWrappedLine = False
        Me.lblKanriNoL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblKanriNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNoL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblKanriNoL.HeightDef = 18
        Me.lblKanriNoL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoL.HissuLabelVisible = False
        Me.lblKanriNoL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblKanriNoL.IsByteCheck = 0
        Me.lblKanriNoL.IsCalendarCheck = False
        Me.lblKanriNoL.IsDakutenCheck = False
        Me.lblKanriNoL.IsEisuCheck = False
        Me.lblKanriNoL.IsForbiddenWordsCheck = False
        Me.lblKanriNoL.IsFullByteCheck = 0
        Me.lblKanriNoL.IsHankakuCheck = False
        Me.lblKanriNoL.IsHissuCheck = False
        Me.lblKanriNoL.IsKanaCheck = False
        Me.lblKanriNoL.IsMiddleSpace = False
        Me.lblKanriNoL.IsNumericCheck = False
        Me.lblKanriNoL.IsSujiCheck = False
        Me.lblKanriNoL.IsZenkakuCheck = False
        Me.lblKanriNoL.ItemName = ""
        Me.lblKanriNoL.LineSpace = 0
        Me.lblKanriNoL.Location = New System.Drawing.Point(124, 30)
        Me.lblKanriNoL.MaxLength = 0
        Me.lblKanriNoL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblKanriNoL.MaxLineCount = 0
        Me.lblKanriNoL.Multiline = False
        Me.lblKanriNoL.Name = "lblKanriNoL"
        Me.lblKanriNoL.ReadOnly = True
        Me.lblKanriNoL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblKanriNoL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblKanriNoL.Size = New System.Drawing.Size(90, 18)
        Me.lblKanriNoL.TabIndex = 18
        Me.lblKanriNoL.TabStop = False
        Me.lblKanriNoL.TabStopSetting = False
        Me.lblKanriNoL.TextValue = "XXXXXXX"
        Me.lblKanriNoL.UseSystemPasswordChar = False
        Me.lblKanriNoL.WidthDef = 90
        Me.lblKanriNoL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNyukaType
        '
        Me.lblTitleNyukaType.AutoSize = True
        Me.lblTitleNyukaType.AutoSizeDef = True
        Me.lblTitleNyukaType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaType.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaType.EnableStatus = False
        Me.lblTitleNyukaType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaType.HeightDef = 13
        Me.lblTitleNyukaType.Location = New System.Drawing.Point(61, 54)
        Me.lblTitleNyukaType.Name = "lblTitleNyukaType"
        Me.lblTitleNyukaType.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleNyukaType.TabIndex = 14
        Me.lblTitleNyukaType.Text = "入荷種別"
        Me.lblTitleNyukaType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaType.TextValue = "入荷種別"
        Me.lblTitleNyukaType.WidthDef = 63
        '
        'lblTitleShinshokuKbn
        '
        Me.lblTitleShinshokuKbn.AutoSize = True
        Me.lblTitleShinshokuKbn.AutoSizeDef = True
        Me.lblTitleShinshokuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShinshokuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShinshokuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShinshokuKbn.EnableStatus = False
        Me.lblTitleShinshokuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShinshokuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShinshokuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShinshokuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShinshokuKbn.HeightDef = 13
        Me.lblTitleShinshokuKbn.Location = New System.Drawing.Point(814, 54)
        Me.lblTitleShinshokuKbn.Name = "lblTitleShinshokuKbn"
        Me.lblTitleShinshokuKbn.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleShinshokuKbn.TabIndex = 10
        Me.lblTitleShinshokuKbn.Text = "進捗区分"
        Me.lblTitleShinshokuKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShinshokuKbn.TextValue = "進捗区分"
        Me.lblTitleShinshokuKbn.WidthDef = 63
        '
        'tabMiddle
        '
        Me.tabMiddle.BackColorDef = System.Drawing.SystemColors.Control
        Me.tabMiddle.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.tabMiddle.Controls.Add(Me.tabGoods)
        Me.tabMiddle.Controls.Add(Me.tabUnso)
        Me.tabMiddle.Controls.Add(Me.tabFreeL)
        Me.tabMiddle.EnableStatus = True
        Me.tabMiddle.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabMiddle.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabMiddle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabMiddle.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabMiddle.HeightDef = 411
        Me.tabMiddle.Location = New System.Drawing.Point(13, 258)
        Me.tabMiddle.Name = "tabMiddle"
        Me.tabMiddle.SelectedIndex = 0
        Me.tabMiddle.Size = New System.Drawing.Size(1249, 411)
        Me.tabMiddle.TabIndex = 116
        Me.tabMiddle.TabStopSetting = True
        Me.tabMiddle.Text = "商品別情報"
        Me.tabMiddle.TextValue = "商品別情報"
        Me.tabMiddle.WidthDef = 1249
        '
        'tabGoods
        '
        Me.tabGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.tabGoods.Controls.Add(Me.lblSumCntTani)
        Me.tabGoods.Controls.Add(Me.lblTitleHasuTani)
        Me.tabGoods.Controls.Add(Me.lblJotai)
        Me.tabGoods.Controls.Add(Me.lblGoodsKey)
        Me.tabGoods.Controls.Add(Me.cmbOndo)
        Me.tabGoods.Controls.Add(Me.txtSerial)
        Me.tabGoods.Controls.Add(Me.txtLot)
        Me.tabGoods.Controls.Add(Me.lblTitleSerial)
        Me.tabGoods.Controls.Add(Me.lblTitleLot)
        Me.tabGoods.Controls.Add(Me.lblTitleIrimeTani)
        Me.tabGoods.Controls.Add(Me.numIrime)
        Me.tabGoods.Controls.Add(Me.lblTitleIrime)
        Me.tabGoods.Controls.Add(Me.btnRevival)
        Me.tabGoods.Controls.Add(Me.lblTitleGoods)
        Me.tabGoods.Controls.Add(Me.lblTitleTareTani)
        Me.tabGoods.Controls.Add(Me.numTare)
        Me.tabGoods.Controls.Add(Me.lblTitleTare)
        Me.tabGoods.Controls.Add(Me.lblTitleOndo)
        Me.tabGoods.Controls.Add(Me.numHasu)
        Me.tabGoods.Controls.Add(Me.lblTitleHasu)
        Me.tabGoods.Controls.Add(Me.lblTitleKonsuTani)
        Me.tabGoods.Controls.Add(Me.numKosu)
        Me.tabGoods.Controls.Add(Me.lblTitleKonsu)
        Me.tabGoods.Controls.Add(Me.lblGoodsNm)
        Me.tabGoods.Controls.Add(Me.txtGoodsCd)
        Me.tabGoods.Controls.Add(Me.lblKanriNoM)
        Me.tabGoods.Controls.Add(Me.lblTitleKanriNoM)
        Me.tabGoods.Controls.Add(Me.btnDel)
        Me.tabGoods.Controls.Add(Me.sprGoodsDef)
        Me.tabGoods.Controls.Add(Me.txtGoodsComment)
        Me.tabGoods.Controls.Add(Me.LmTitleLabel32)
        Me.tabGoods.Controls.Add(Me.lblTitleGoodsComment)
        Me.tabGoods.Controls.Add(Me.LmTitleLabel34)
        Me.tabGoods.Controls.Add(Me.lblStdIrimeTani)
        Me.tabGoods.Controls.Add(Me.txtBuyerOrdNoM)
        Me.tabGoods.Controls.Add(Me.lblTitleBuyerOrdNoM)
        Me.tabGoods.Controls.Add(Me.numStdIrime)
        Me.tabGoods.Controls.Add(Me.txtOrderNoM)
        Me.tabGoods.Controls.Add(Me.numIrisu)
        Me.tabGoods.Controls.Add(Me.lblTitleOrderNoM)
        Me.tabGoods.Controls.Add(Me.lblTitleIrisu)
        Me.tabGoods.Controls.Add(Me.numSumBetu)
        Me.tabGoods.Controls.Add(Me.lblTitleStdIrime)
        Me.tabGoods.Controls.Add(Me.lblSumAntTani)
        Me.tabGoods.Controls.Add(Me.lblTitleSumAnt)
        Me.tabGoods.Controls.Add(Me.lblTitleSumCnt)
        Me.tabGoods.Controls.Add(Me.numSumAnt)
        Me.tabGoods.Controls.Add(Me.numSumCnt)
        Me.tabGoods.Location = New System.Drawing.Point(4, 22)
        Me.tabGoods.Name = "tabGoods"
        Me.tabGoods.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGoods.Size = New System.Drawing.Size(1241, 385)
        Me.tabGoods.TabIndex = 1
        Me.tabGoods.Text = "商品別情報"
        '
        'lblSumCntTani
        '
        Me.lblSumCntTani.AutoSize = True
        Me.lblSumCntTani.AutoSizeDef = True
        Me.lblSumCntTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSumCntTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSumCntTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSumCntTani.EnableStatus = False
        Me.lblSumCntTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSumCntTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSumCntTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSumCntTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSumCntTani.HeightDef = 13
        Me.lblSumCntTani.Location = New System.Drawing.Point(492, 320)
        Me.lblSumCntTani.Name = "lblSumCntTani"
        Me.lblSumCntTani.Size = New System.Drawing.Size(119, 13)
        Me.lblSumCntTani.TabIndex = 89
        Me.lblSumCntTani.Text = "ファイバードラム"
        Me.lblSumCntTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSumCntTani.TextValue = "ファイバードラム"
        Me.lblSumCntTani.WidthDef = 119
        '
        'lblTitleHasuTani
        '
        Me.lblTitleHasuTani.AutoSize = True
        Me.lblTitleHasuTani.AutoSizeDef = True
        Me.lblTitleHasuTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHasuTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHasuTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHasuTani.EnableStatus = False
        Me.lblTitleHasuTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHasuTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHasuTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHasuTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHasuTani.HeightDef = 13
        Me.lblTitleHasuTani.Location = New System.Drawing.Point(492, 299)
        Me.lblTitleHasuTani.Name = "lblTitleHasuTani"
        Me.lblTitleHasuTani.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleHasuTani.TabIndex = 228
        Me.lblTitleHasuTani.Text = "ファイバードラム"
        Me.lblTitleHasuTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHasuTani.TextValue = "ファイバードラム"
        Me.lblTitleHasuTani.WidthDef = 119
        '
        'lblJotai
        '
        Me.lblJotai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJotai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJotai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblJotai.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJotai.CountWrappedLine = False
        Me.lblJotai.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblJotai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJotai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJotai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJotai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJotai.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblJotai.HeightDef = 18
        Me.lblJotai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJotai.HissuLabelVisible = False
        Me.lblJotai.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblJotai.IsByteCheck = 0
        Me.lblJotai.IsCalendarCheck = False
        Me.lblJotai.IsDakutenCheck = False
        Me.lblJotai.IsEisuCheck = False
        Me.lblJotai.IsForbiddenWordsCheck = False
        Me.lblJotai.IsFullByteCheck = 0
        Me.lblJotai.IsHankakuCheck = False
        Me.lblJotai.IsHissuCheck = False
        Me.lblJotai.IsKanaCheck = False
        Me.lblJotai.IsMiddleSpace = False
        Me.lblJotai.IsNumericCheck = False
        Me.lblJotai.IsSujiCheck = False
        Me.lblJotai.IsZenkakuCheck = False
        Me.lblJotai.ItemName = ""
        Me.lblJotai.LineSpace = 0
        Me.lblJotai.Location = New System.Drawing.Point(1129, 317)
        Me.lblJotai.MaxLength = 0
        Me.lblJotai.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblJotai.MaxLineCount = 0
        Me.lblJotai.Multiline = False
        Me.lblJotai.Name = "lblJotai"
        Me.lblJotai.ReadOnly = True
        Me.lblJotai.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblJotai.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblJotai.Size = New System.Drawing.Size(84, 18)
        Me.lblJotai.TabIndex = 263
        Me.lblJotai.TabStop = False
        Me.lblJotai.TabStopSetting = False
        Me.lblJotai.TextValue = "XXXXXXXXXXXXXXXXXXXZ"
        Me.lblJotai.UseSystemPasswordChar = False
        Me.lblJotai.Visible = False
        Me.lblJotai.WidthDef = 84
        Me.lblJotai.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblGoodsKey.HissuLabelVisible = False
        Me.lblGoodsKey.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsKey.IsByteCheck = 0
        Me.lblGoodsKey.IsCalendarCheck = False
        Me.lblGoodsKey.IsDakutenCheck = False
        Me.lblGoodsKey.IsEisuCheck = False
        Me.lblGoodsKey.IsForbiddenWordsCheck = False
        Me.lblGoodsKey.IsFullByteCheck = 0
        Me.lblGoodsKey.IsHankakuCheck = False
        Me.lblGoodsKey.IsHissuCheck = False
        Me.lblGoodsKey.IsKanaCheck = False
        Me.lblGoodsKey.IsMiddleSpace = False
        Me.lblGoodsKey.IsNumericCheck = False
        Me.lblGoodsKey.IsSujiCheck = False
        Me.lblGoodsKey.IsZenkakuCheck = False
        Me.lblGoodsKey.ItemName = ""
        Me.lblGoodsKey.LineSpace = 0
        Me.lblGoodsKey.Location = New System.Drawing.Point(1036, 275)
        Me.lblGoodsKey.MaxLength = 0
        Me.lblGoodsKey.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsKey.MaxLineCount = 0
        Me.lblGoodsKey.Multiline = False
        Me.lblGoodsKey.Name = "lblGoodsKey"
        Me.lblGoodsKey.ReadOnly = True
        Me.lblGoodsKey.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsKey.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsKey.Size = New System.Drawing.Size(177, 18)
        Me.lblGoodsKey.TabIndex = 262
        Me.lblGoodsKey.TabStop = False
        Me.lblGoodsKey.TabStopSetting = False
        Me.lblGoodsKey.TextValue = "XXXXXXXXXXXXXXXXXXXZ"
        Me.lblGoodsKey.UseSystemPasswordChar = False
        Me.lblGoodsKey.WidthDef = 177
        Me.lblGoodsKey.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbOndo
        '
        Me.cmbOndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbOndo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbOndo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbOndo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbOndo.DataCode = "O002"
        Me.cmbOndo.DataSource = Nothing
        Me.cmbOndo.DisplayMember = Nothing
        Me.cmbOndo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOndo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOndo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOndo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOndo.HeightDef = 18
        Me.cmbOndo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbOndo.HissuLabelVisible = False
        Me.cmbOndo.InsertWildCard = True
        Me.cmbOndo.IsForbiddenWordsCheck = False
        Me.cmbOndo.IsHissuCheck = False
        Me.cmbOndo.ItemName = ""
        Me.cmbOndo.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbOndo.Location = New System.Drawing.Point(1129, 296)
        Me.cmbOndo.Name = "cmbOndo"
        Me.cmbOndo.ReadOnly = True
        Me.cmbOndo.SelectedIndex = -1
        Me.cmbOndo.SelectedItem = Nothing
        Me.cmbOndo.SelectedText = ""
        Me.cmbOndo.SelectedValue = ""
        Me.cmbOndo.Size = New System.Drawing.Size(84, 18)
        Me.cmbOndo.TabIndex = 261
        Me.cmbOndo.TabStop = False
        Me.cmbOndo.TabStopSetting = False
        Me.cmbOndo.TextValue = ""
        Me.cmbOndo.Value1 = Nothing
        Me.cmbOndo.Value2 = Nothing
        Me.cmbOndo.Value3 = Nothing
        Me.cmbOndo.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbOndo.ValueMember = Nothing
        Me.cmbOndo.WidthDef = 84
        '
        'txtSerial
        '
        Me.txtSerial.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerial.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerial.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerial.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerial.CountWrappedLine = False
        Me.txtSerial.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerial.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerial.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerial.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerial.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerial.HeightDef = 18
        Me.txtSerial.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerial.HissuLabelVisible = False
        Me.txtSerial.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtSerial.IsByteCheck = 40
        Me.txtSerial.IsCalendarCheck = False
        Me.txtSerial.IsDakutenCheck = False
        Me.txtSerial.IsEisuCheck = False
        Me.txtSerial.IsForbiddenWordsCheck = False
        Me.txtSerial.IsFullByteCheck = 0
        Me.txtSerial.IsHankakuCheck = False
        Me.txtSerial.IsHissuCheck = False
        Me.txtSerial.IsKanaCheck = False
        Me.txtSerial.IsMiddleSpace = False
        Me.txtSerial.IsNumericCheck = False
        Me.txtSerial.IsSujiCheck = False
        Me.txtSerial.IsZenkakuCheck = False
        Me.txtSerial.ItemName = ""
        Me.txtSerial.LineSpace = 0
        Me.txtSerial.Location = New System.Drawing.Point(907, 359)
        Me.txtSerial.MaxLength = 40
        Me.txtSerial.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerial.MaxLineCount = 0
        Me.txtSerial.Multiline = False
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.ReadOnly = False
        Me.txtSerial.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerial.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerial.Size = New System.Drawing.Size(306, 18)
        Me.txtSerial.TabIndex = 260
        Me.txtSerial.TabStopSetting = True
        Me.txtSerial.TextValue = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXA"
        Me.txtSerial.UseSystemPasswordChar = False
        Me.txtSerial.WidthDef = 306
        Me.txtSerial.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtLot
        '
        Me.txtLot.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLot.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLot.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLot.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLot.CountWrappedLine = False
        Me.txtLot.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLot.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLot.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLot.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLot.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLot.HeightDef = 18
        Me.txtLot.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLot.HissuLabelVisible = False
        Me.txtLot.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLot.IsByteCheck = 40
        Me.txtLot.IsCalendarCheck = False
        Me.txtLot.IsDakutenCheck = False
        Me.txtLot.IsEisuCheck = False
        Me.txtLot.IsForbiddenWordsCheck = False
        Me.txtLot.IsFullByteCheck = 0
        Me.txtLot.IsHankakuCheck = False
        Me.txtLot.IsHissuCheck = False
        Me.txtLot.IsKanaCheck = False
        Me.txtLot.IsMiddleSpace = False
        Me.txtLot.IsNumericCheck = False
        Me.txtLot.IsSujiCheck = False
        Me.txtLot.IsZenkakuCheck = False
        Me.txtLot.ItemName = ""
        Me.txtLot.LineSpace = 0
        Me.txtLot.Location = New System.Drawing.Point(907, 338)
        Me.txtLot.MaxLength = 40
        Me.txtLot.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLot.MaxLineCount = 0
        Me.txtLot.Multiline = False
        Me.txtLot.Name = "txtLot"
        Me.txtLot.ReadOnly = False
        Me.txtLot.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLot.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLot.Size = New System.Drawing.Size(306, 18)
        Me.txtLot.TabIndex = 259
        Me.txtLot.TabStopSetting = True
        Me.txtLot.TextValue = ""
        Me.txtLot.UseSystemPasswordChar = False
        Me.txtLot.WidthDef = 306
        Me.txtLot.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSerial
        '
        Me.lblTitleSerial.AutoSize = True
        Me.lblTitleSerial.AutoSizeDef = True
        Me.lblTitleSerial.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerial.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerial.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSerial.EnableStatus = False
        Me.lblTitleSerial.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerial.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerial.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerial.HeightDef = 13
        Me.lblTitleSerial.Location = New System.Drawing.Point(844, 362)
        Me.lblTitleSerial.Name = "lblTitleSerial"
        Me.lblTitleSerial.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSerial.TabIndex = 258
        Me.lblTitleSerial.Text = "シリアル"
        Me.lblTitleSerial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSerial.TextValue = "シリアル"
        Me.lblTitleSerial.WidthDef = 63
        '
        'lblTitleLot
        '
        Me.lblTitleLot.AutoSize = True
        Me.lblTitleLot.AutoSizeDef = True
        Me.lblTitleLot.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLot.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLot.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLot.EnableStatus = False
        Me.lblTitleLot.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLot.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLot.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLot.HeightDef = 13
        Me.lblTitleLot.Location = New System.Drawing.Point(844, 341)
        Me.lblTitleLot.Name = "lblTitleLot"
        Me.lblTitleLot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitleLot.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleLot.TabIndex = 256
        Me.lblTitleLot.Text = "ロット№"
        Me.lblTitleLot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLot.TextValue = "ロット№"
        Me.lblTitleLot.WidthDef = 63
        '
        'lblTitleIrimeTani
        '
        Me.lblTitleIrimeTani.AutoSize = True
        Me.lblTitleIrimeTani.AutoSizeDef = True
        Me.lblTitleIrimeTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrimeTani.EnableStatus = False
        Me.lblTitleIrimeTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeTani.HeightDef = 13
        Me.lblTitleIrimeTani.Location = New System.Drawing.Point(992, 320)
        Me.lblTitleIrimeTani.Name = "lblTitleIrimeTani"
        Me.lblTitleIrimeTani.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleIrimeTani.TabIndex = 254
        Me.lblTitleIrimeTani.Text = "マイクログラム"
        Me.lblTitleIrimeTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrimeTani.TextValue = "マイクログラム"
        Me.lblTitleIrimeTani.WidthDef = 105
        '
        'numIrime
        '
        Me.numIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrime.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrime.HeightDef = 18
        Me.numIrime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrime.HissuLabelVisible = False
        Me.numIrime.IsHissuCheck = False
        Me.numIrime.IsRangeCheck = False
        Me.numIrime.ItemName = ""
        Me.numIrime.Location = New System.Drawing.Point(907, 317)
        Me.numIrime.Name = "numIrime"
        Me.numIrime.ReadOnly = False
        Me.numIrime.Size = New System.Drawing.Size(100, 18)
        Me.numIrime.TabIndex = 255
        Me.numIrime.TabStopSetting = True
        Me.numIrime.TextValue = "0"
        Me.numIrime.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrime.WidthDef = 100
        '
        'lblTitleIrime
        '
        Me.lblTitleIrime.AutoSize = True
        Me.lblTitleIrime.AutoSizeDef = True
        Me.lblTitleIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrime.EnableStatus = False
        Me.lblTitleIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrime.HeightDef = 13
        Me.lblTitleIrime.Location = New System.Drawing.Point(872, 320)
        Me.lblTitleIrime.Name = "lblTitleIrime"
        Me.lblTitleIrime.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleIrime.TabIndex = 253
        Me.lblTitleIrime.Text = "入目"
        Me.lblTitleIrime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrime.TextValue = "入目"
        Me.lblTitleIrime.WidthDef = 35
        '
        'btnRevival
        '
        Me.btnRevival.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRevival.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRevival.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRevival.EnableStatus = True
        Me.btnRevival.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRevival.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRevival.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRevival.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRevival.HeightDef = 22
        Me.btnRevival.Location = New System.Drawing.Point(75, 5)
        Me.btnRevival.Name = "btnRevival"
        Me.btnRevival.Size = New System.Drawing.Size(70, 22)
        Me.btnRevival.TabIndex = 252
        Me.btnRevival.TabStopSetting = True
        Me.btnRevival.Text = "行復活"
        Me.btnRevival.TextValue = "行復活"
        Me.btnRevival.UseVisualStyleBackColor = True
        Me.btnRevival.WidthDef = 70
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
        Me.lblTitleGoods.Location = New System.Drawing.Point(347, 278)
        Me.lblTitleGoods.Name = "lblTitleGoods"
        Me.lblTitleGoods.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoods.TabIndex = 248
        Me.lblTitleGoods.Text = "商品"
        Me.lblTitleGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoods.TextValue = "商品"
        Me.lblTitleGoods.WidthDef = 35
        '
        'lblTitleTareTani
        '
        Me.lblTitleTareTani.AutoSize = True
        Me.lblTitleTareTani.AutoSizeDef = True
        Me.lblTitleTareTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTareTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTareTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTareTani.EnableStatus = False
        Me.lblTitleTareTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTareTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTareTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTareTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTareTani.HeightDef = 13
        Me.lblTitleTareTani.Location = New System.Drawing.Point(788, 320)
        Me.lblTitleTareTani.Name = "lblTitleTareTani"
        Me.lblTitleTareTani.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleTareTani.TabIndex = 241
        Me.lblTitleTareTani.Text = "KG"
        Me.lblTitleTareTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTareTani.TextValue = "KG"
        Me.lblTitleTareTani.WidthDef = 21
        '
        'numTare
        '
        Me.numTare.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTare.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTare.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numTare.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numTare.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTare.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTare.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTare.HeightDef = 18
        Me.numTare.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTare.HissuLabelVisible = False
        Me.numTare.IsHissuCheck = False
        Me.numTare.IsRangeCheck = False
        Me.numTare.ItemName = ""
        Me.numTare.Location = New System.Drawing.Point(673, 317)
        Me.numTare.Name = "numTare"
        Me.numTare.ReadOnly = True
        Me.numTare.Size = New System.Drawing.Size(130, 18)
        Me.numTare.TabIndex = 239
        Me.numTare.TabStop = False
        Me.numTare.TabStopSetting = False
        Me.numTare.TextValue = "0"
        Me.numTare.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numTare.WidthDef = 130
        '
        'lblTitleTare
        '
        Me.lblTitleTare.AutoSize = True
        Me.lblTitleTare.AutoSizeDef = True
        Me.lblTitleTare.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTare.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTare.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTare.EnableStatus = False
        Me.lblTitleTare.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTare.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTare.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTare.HeightDef = 13
        Me.lblTitleTare.Location = New System.Drawing.Point(611, 320)
        Me.lblTitleTare.Name = "lblTitleTare"
        Me.lblTitleTare.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTare.TabIndex = 237
        Me.lblTitleTare.Text = "個別重量"
        Me.lblTitleTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTare.TextValue = "個別重量"
        Me.lblTitleTare.WidthDef = 63
        '
        'lblTitleOndo
        '
        Me.lblTitleOndo.AutoSize = True
        Me.lblTitleOndo.AutoSizeDef = True
        Me.lblTitleOndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOndo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOndo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOndo.EnableStatus = False
        Me.lblTitleOndo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOndo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOndo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOndo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOndo.HeightDef = 13
        Me.lblTitleOndo.Location = New System.Drawing.Point(1094, 299)
        Me.lblTitleOndo.Name = "lblTitleOndo"
        Me.lblTitleOndo.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleOndo.TabIndex = 235
        Me.lblTitleOndo.Text = "温度"
        Me.lblTitleOndo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOndo.TextValue = "温度"
        Me.lblTitleOndo.WidthDef = 35
        '
        'numHasu
        '
        Me.numHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHasu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHasu.HeightDef = 18
        Me.numHasu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHasu.HissuLabelVisible = False
        Me.numHasu.IsHissuCheck = False
        Me.numHasu.IsRangeCheck = False
        Me.numHasu.ItemName = ""
        Me.numHasu.Location = New System.Drawing.Point(382, 296)
        Me.numHasu.Name = "numHasu"
        Me.numHasu.ReadOnly = False
        Me.numHasu.Size = New System.Drawing.Size(125, 18)
        Me.numHasu.TabIndex = 227
        Me.numHasu.TabStopSetting = True
        Me.numHasu.TextValue = "0"
        Me.numHasu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHasu.WidthDef = 125
        '
        'lblTitleHasu
        '
        Me.lblTitleHasu.AutoSize = True
        Me.lblTitleHasu.AutoSizeDef = True
        Me.lblTitleHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHasu.EnableStatus = False
        Me.lblTitleHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHasu.HeightDef = 13
        Me.lblTitleHasu.Location = New System.Drawing.Point(347, 299)
        Me.lblTitleHasu.Name = "lblTitleHasu"
        Me.lblTitleHasu.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleHasu.TabIndex = 226
        Me.lblTitleHasu.Text = "端数"
        Me.lblTitleHasu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHasu.TextValue = "端数"
        Me.lblTitleHasu.WidthDef = 35
        '
        'lblTitleKonsuTani
        '
        Me.lblTitleKonsuTani.AutoSize = True
        Me.lblTitleKonsuTani.AutoSizeDef = True
        Me.lblTitleKonsuTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsuTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsuTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKonsuTani.EnableStatus = False
        Me.lblTitleKonsuTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsuTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsuTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsuTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsuTani.HeightDef = 13
        Me.lblTitleKonsuTani.Location = New System.Drawing.Point(225, 299)
        Me.lblTitleKonsuTani.Name = "lblTitleKonsuTani"
        Me.lblTitleKonsuTani.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleKonsuTani.TabIndex = 225
        Me.lblTitleKonsuTani.Text = "ファイバードラム"
        Me.lblTitleKonsuTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKonsuTani.TextValue = "ファイバードラム"
        Me.lblTitleKonsuTani.WidthDef = 119
        '
        'numKosu
        '
        Me.numKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKosu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKosu.HeightDef = 18
        Me.numKosu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKosu.HissuLabelVisible = False
        Me.numKosu.IsHissuCheck = False
        Me.numKosu.IsRangeCheck = False
        Me.numKosu.ItemName = ""
        Me.numKosu.Location = New System.Drawing.Point(115, 296)
        Me.numKosu.Name = "numKosu"
        Me.numKosu.ReadOnly = False
        Me.numKosu.Size = New System.Drawing.Size(125, 18)
        Me.numKosu.TabIndex = 224
        Me.numKosu.TabStopSetting = True
        Me.numKosu.TextValue = "0"
        Me.numKosu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKosu.WidthDef = 125
        '
        'lblTitleKonsu
        '
        Me.lblTitleKonsu.AutoSize = True
        Me.lblTitleKonsu.AutoSizeDef = True
        Me.lblTitleKonsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKonsu.EnableStatus = False
        Me.lblTitleKonsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu.HeightDef = 13
        Me.lblTitleKonsu.Location = New System.Drawing.Point(80, 299)
        Me.lblTitleKonsu.Name = "lblTitleKonsu"
        Me.lblTitleKonsu.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKonsu.TabIndex = 223
        Me.lblTitleKonsu.Text = "梱数"
        Me.lblTitleKonsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKonsu.TextValue = "梱数"
        Me.lblTitleKonsu.WidthDef = 35
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
        Me.lblGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
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
        Me.lblGoodsNm.Location = New System.Drawing.Point(550, 275)
        Me.lblGoodsNm.MaxLength = 0
        Me.lblGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNm.MaxLineCount = 0
        Me.lblGoodsNm.Multiline = False
        Me.lblGoodsNm.Name = "lblGoodsNm"
        Me.lblGoodsNm.ReadOnly = True
        Me.lblGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNm.Size = New System.Drawing.Size(502, 18)
        Me.lblGoodsNm.TabIndex = 221
        Me.lblGoodsNm.TabStop = False
        Me.lblGoodsNm.TabStopSetting = False
        Me.lblGoodsNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblGoodsNm.UseSystemPasswordChar = False
        Me.lblGoodsNm.WidthDef = 502
        Me.lblGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.BackColor = System.Drawing.Color.White
        Me.txtGoodsCd.BackColorDef = System.Drawing.Color.White
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
        Me.txtGoodsCd.HissuLabelVisible = False
        Me.txtGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtGoodsCd.IsByteCheck = 20
        Me.txtGoodsCd.IsCalendarCheck = False
        Me.txtGoodsCd.IsDakutenCheck = False
        Me.txtGoodsCd.IsEisuCheck = False
        Me.txtGoodsCd.IsForbiddenWordsCheck = False
        Me.txtGoodsCd.IsFullByteCheck = 0
        Me.txtGoodsCd.IsHankakuCheck = False
        Me.txtGoodsCd.IsHissuCheck = False
        Me.txtGoodsCd.IsKanaCheck = False
        Me.txtGoodsCd.IsMiddleSpace = False
        Me.txtGoodsCd.IsNumericCheck = False
        Me.txtGoodsCd.IsSujiCheck = False
        Me.txtGoodsCd.IsZenkakuCheck = False
        Me.txtGoodsCd.ItemName = ""
        Me.txtGoodsCd.LineSpace = 0
        Me.txtGoodsCd.Location = New System.Drawing.Point(382, 275)
        Me.txtGoodsCd.MaxLength = 20
        Me.txtGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCd.MaxLineCount = 0
        Me.txtGoodsCd.Multiline = False
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.ReadOnly = False
        Me.txtGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCd.Size = New System.Drawing.Size(208, 18)
        Me.txtGoodsCd.TabIndex = 220
        Me.txtGoodsCd.TabStopSetting = True
        Me.txtGoodsCd.TextValue = ""
        Me.txtGoodsCd.UseSystemPasswordChar = False
        Me.txtGoodsCd.WidthDef = 208
        Me.txtGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKanriNoM
        '
        Me.lblKanriNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKanriNoM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKanriNoM.CountWrappedLine = False
        Me.lblKanriNoM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblKanriNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNoM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblKanriNoM.HeightDef = 18
        Me.lblKanriNoM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNoM.HissuLabelVisible = False
        Me.lblKanriNoM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblKanriNoM.IsByteCheck = 30
        Me.lblKanriNoM.IsCalendarCheck = False
        Me.lblKanriNoM.IsDakutenCheck = False
        Me.lblKanriNoM.IsEisuCheck = False
        Me.lblKanriNoM.IsForbiddenWordsCheck = False
        Me.lblKanriNoM.IsFullByteCheck = 0
        Me.lblKanriNoM.IsHankakuCheck = False
        Me.lblKanriNoM.IsHissuCheck = False
        Me.lblKanriNoM.IsKanaCheck = False
        Me.lblKanriNoM.IsMiddleSpace = False
        Me.lblKanriNoM.IsNumericCheck = False
        Me.lblKanriNoM.IsSujiCheck = False
        Me.lblKanriNoM.IsZenkakuCheck = False
        Me.lblKanriNoM.ItemName = ""
        Me.lblKanriNoM.LineSpace = 0
        Me.lblKanriNoM.Location = New System.Drawing.Point(115, 275)
        Me.lblKanriNoM.MaxLength = 30
        Me.lblKanriNoM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblKanriNoM.MaxLineCount = 0
        Me.lblKanriNoM.Multiline = False
        Me.lblKanriNoM.Name = "lblKanriNoM"
        Me.lblKanriNoM.ReadOnly = True
        Me.lblKanriNoM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblKanriNoM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblKanriNoM.Size = New System.Drawing.Size(45, 18)
        Me.lblKanriNoM.TabIndex = 118
        Me.lblKanriNoM.TabStop = False
        Me.lblKanriNoM.TabStopSetting = False
        Me.lblKanriNoM.TextValue = ""
        Me.lblKanriNoM.UseSystemPasswordChar = False
        Me.lblKanriNoM.WidthDef = 45
        Me.lblKanriNoM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleKanriNoM
        '
        Me.lblTitleKanriNoM.AutoSize = True
        Me.lblTitleKanriNoM.AutoSizeDef = True
        Me.lblTitleKanriNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKanriNoM.EnableStatus = False
        Me.lblTitleKanriNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNoM.HeightDef = 13
        Me.lblTitleKanriNoM.Location = New System.Drawing.Point(4, 278)
        Me.lblTitleKanriNoM.Name = "lblTitleKanriNoM"
        Me.lblTitleKanriNoM.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleKanriNoM.TabIndex = 117
        Me.lblTitleKanriNoM.Text = "EDI管理番号(中)"
        Me.lblTitleKanriNoM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKanriNoM.TextValue = "EDI管理番号(中)"
        Me.lblTitleKanriNoM.WidthDef = 112
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnDel.EnableStatus = True
        Me.btnDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.HeightDef = 22
        Me.btnDel.Location = New System.Drawing.Point(160, 5)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 22)
        Me.btnDel.TabIndex = 116
        Me.btnDel.TabStopSetting = True
        Me.btnDel.Text = "行削除"
        Me.btnDel.TextValue = "行削除"
        Me.btnDel.UseVisualStyleBackColor = True
        Me.btnDel.WidthDef = 70
        '
        'sprGoodsDef
        '
        Me.sprGoodsDef.AccessibleDescription = ""
        Me.sprGoodsDef.AllowUserZoom = False
        Me.sprGoodsDef.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprGoodsDef.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprGoodsDef.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprGoodsDef.CellClickEventArgs = Nothing
        Me.sprGoodsDef.CheckToCheckBox = True
        Me.sprGoodsDef.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprGoodsDef.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprGoodsDef.EditModeReplace = True
        Me.sprGoodsDef.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprGoodsDef.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprGoodsDef.ForeColorDef = System.Drawing.Color.Empty
        Me.sprGoodsDef.HeightDef = 231
        Me.sprGoodsDef.KeyboardCheckBoxOn = False
        Me.sprGoodsDef.Location = New System.Drawing.Point(5, 31)
        Me.sprGoodsDef.Name = "sprGoodsDef"
        Me.sprGoodsDef.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprGoodsDef.Size = New System.Drawing.Size(1230, 231)
        Me.sprGoodsDef.SortColumn = True
        Me.sprGoodsDef.SpanColumnLock = True
        Me.sprGoodsDef.SpreadDoubleClicked = False
        Me.sprGoodsDef.TabIndex = 115
        Me.sprGoodsDef.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprGoodsDef.TextValue = Nothing
        Me.sprGoodsDef.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprGoodsDef.WidthDef = 1230
        '
        'txtGoodsComment
        '
        Me.txtGoodsComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsComment.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsComment.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsComment.CountWrappedLine = False
        Me.txtGoodsComment.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsComment.HeightDef = 18
        Me.txtGoodsComment.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment.HissuLabelVisible = False
        Me.txtGoodsComment.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsComment.IsByteCheck = 100
        Me.txtGoodsComment.IsCalendarCheck = False
        Me.txtGoodsComment.IsDakutenCheck = False
        Me.txtGoodsComment.IsEisuCheck = False
        Me.txtGoodsComment.IsForbiddenWordsCheck = False
        Me.txtGoodsComment.IsFullByteCheck = 0
        Me.txtGoodsComment.IsHankakuCheck = False
        Me.txtGoodsComment.IsHissuCheck = False
        Me.txtGoodsComment.IsKanaCheck = False
        Me.txtGoodsComment.IsMiddleSpace = False
        Me.txtGoodsComment.IsNumericCheck = False
        Me.txtGoodsComment.IsSujiCheck = False
        Me.txtGoodsComment.IsZenkakuCheck = False
        Me.txtGoodsComment.ItemName = ""
        Me.txtGoodsComment.LineSpace = 0
        Me.txtGoodsComment.Location = New System.Drawing.Point(115, 359)
        Me.txtGoodsComment.MaxLength = 100
        Me.txtGoodsComment.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsComment.MaxLineCount = 0
        Me.txtGoodsComment.Multiline = False
        Me.txtGoodsComment.Name = "txtGoodsComment"
        Me.txtGoodsComment.ReadOnly = False
        Me.txtGoodsComment.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsComment.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsComment.Size = New System.Drawing.Size(722, 18)
        Me.txtGoodsComment.TabIndex = 26
        Me.txtGoodsComment.TabStopSetting = True
        Me.txtGoodsComment.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsComment.UseSystemPasswordChar = False
        Me.txtGoodsComment.WidthDef = 722
        Me.txtGoodsComment.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel32
        '
        Me.LmTitleLabel32.AutoSize = True
        Me.LmTitleLabel32.AutoSizeDef = True
        Me.LmTitleLabel32.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel32.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel32.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel32.EnableStatus = False
        Me.LmTitleLabel32.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel32.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel32.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel32.HeightDef = 13
        Me.LmTitleLabel32.Location = New System.Drawing.Point(1001, 55)
        Me.LmTitleLabel32.Name = "LmTitleLabel32"
        Me.LmTitleLabel32.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel32.TabIndex = 75
        Me.LmTitleLabel32.Text = "KG"
        Me.LmTitleLabel32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel32.TextValue = "KG"
        Me.LmTitleLabel32.WidthDef = 21
        '
        'lblTitleGoodsComment
        '
        Me.lblTitleGoodsComment.AutoSize = True
        Me.lblTitleGoodsComment.AutoSizeDef = True
        Me.lblTitleGoodsComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsComment.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsComment.EnableStatus = False
        Me.lblTitleGoodsComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsComment.HeightDef = 13
        Me.lblTitleGoodsComment.Location = New System.Drawing.Point(10, 362)
        Me.lblTitleGoodsComment.Name = "lblTitleGoodsComment"
        Me.lblTitleGoodsComment.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleGoodsComment.TabIndex = 25
        Me.lblTitleGoodsComment.Text = "商品別注意事項"
        Me.lblTitleGoodsComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsComment.TextValue = "商品別注意事項"
        Me.lblTitleGoodsComment.WidthDef = 105
        '
        'LmTitleLabel34
        '
        Me.LmTitleLabel34.AutoSize = True
        Me.LmTitleLabel34.AutoSizeDef = True
        Me.LmTitleLabel34.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel34.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel34.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel34.EnableStatus = False
        Me.LmTitleLabel34.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel34.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel34.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel34.HeightDef = 13
        Me.LmTitleLabel34.Location = New System.Drawing.Point(840, 55)
        Me.LmTitleLabel34.Name = "LmTitleLabel34"
        Me.LmTitleLabel34.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel34.TabIndex = 55
        Me.LmTitleLabel34.Text = "総重量"
        Me.LmTitleLabel34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel34.TextValue = "総重量"
        Me.LmTitleLabel34.WidthDef = 49
        '
        'lblStdIrimeTani
        '
        Me.lblStdIrimeTani.AutoSize = True
        Me.lblStdIrimeTani.AutoSizeDef = True
        Me.lblStdIrimeTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStdIrimeTani.EnableStatus = False
        Me.lblStdIrimeTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani.HeightDef = 13
        Me.lblStdIrimeTani.Location = New System.Drawing.Point(992, 299)
        Me.lblStdIrimeTani.Name = "lblStdIrimeTani"
        Me.lblStdIrimeTani.Size = New System.Drawing.Size(105, 13)
        Me.lblStdIrimeTani.TabIndex = 92
        Me.lblStdIrimeTani.Text = "マイクログラム"
        Me.lblStdIrimeTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStdIrimeTani.TextValue = "マイクログラム"
        Me.lblStdIrimeTani.WidthDef = 105
        '
        'txtBuyerOrdNoM
        '
        Me.txtBuyerOrdNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuyerOrdNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBuyerOrdNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerOrdNoM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBuyerOrdNoM.CountWrappedLine = False
        Me.txtBuyerOrdNoM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtBuyerOrdNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNoM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtBuyerOrdNoM.HeightDef = 18
        Me.txtBuyerOrdNoM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNoM.HissuLabelVisible = False
        Me.txtBuyerOrdNoM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtBuyerOrdNoM.IsByteCheck = 30
        Me.txtBuyerOrdNoM.IsCalendarCheck = False
        Me.txtBuyerOrdNoM.IsDakutenCheck = False
        Me.txtBuyerOrdNoM.IsEisuCheck = False
        Me.txtBuyerOrdNoM.IsForbiddenWordsCheck = False
        Me.txtBuyerOrdNoM.IsFullByteCheck = 0
        Me.txtBuyerOrdNoM.IsHankakuCheck = False
        Me.txtBuyerOrdNoM.IsHissuCheck = False
        Me.txtBuyerOrdNoM.IsKanaCheck = False
        Me.txtBuyerOrdNoM.IsMiddleSpace = False
        Me.txtBuyerOrdNoM.IsNumericCheck = False
        Me.txtBuyerOrdNoM.IsSujiCheck = False
        Me.txtBuyerOrdNoM.IsZenkakuCheck = False
        Me.txtBuyerOrdNoM.ItemName = ""
        Me.txtBuyerOrdNoM.LineSpace = 0
        Me.txtBuyerOrdNoM.Location = New System.Drawing.Point(552, 338)
        Me.txtBuyerOrdNoM.MaxLength = 30
        Me.txtBuyerOrdNoM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBuyerOrdNoM.MaxLineCount = 0
        Me.txtBuyerOrdNoM.Multiline = False
        Me.txtBuyerOrdNoM.Name = "txtBuyerOrdNoM"
        Me.txtBuyerOrdNoM.ReadOnly = False
        Me.txtBuyerOrdNoM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBuyerOrdNoM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBuyerOrdNoM.Size = New System.Drawing.Size(285, 18)
        Me.txtBuyerOrdNoM.TabIndex = 40
        Me.txtBuyerOrdNoM.TabStopSetting = True
        Me.txtBuyerOrdNoM.TextValue = ""
        Me.txtBuyerOrdNoM.UseSystemPasswordChar = False
        Me.txtBuyerOrdNoM.WidthDef = 285
        Me.txtBuyerOrdNoM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleBuyerOrdNoM
        '
        Me.lblTitleBuyerOrdNoM.AutoSize = True
        Me.lblTitleBuyerOrdNoM.AutoSizeDef = True
        Me.lblTitleBuyerOrdNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBuyerOrdNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBuyerOrdNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBuyerOrdNoM.EnableStatus = False
        Me.lblTitleBuyerOrdNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBuyerOrdNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBuyerOrdNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBuyerOrdNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBuyerOrdNoM.HeightDef = 13
        Me.lblTitleBuyerOrdNoM.Location = New System.Drawing.Point(490, 341)
        Me.lblTitleBuyerOrdNoM.Name = "lblTitleBuyerOrdNoM"
        Me.lblTitleBuyerOrdNoM.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleBuyerOrdNoM.TabIndex = 39
        Me.lblTitleBuyerOrdNoM.Text = "注文番号"
        Me.lblTitleBuyerOrdNoM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBuyerOrdNoM.TextValue = "注文番号"
        Me.lblTitleBuyerOrdNoM.WidthDef = 63
        '
        'numStdIrime
        '
        Me.numStdIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStdIrime.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStdIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime.HeightDef = 18
        Me.numStdIrime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime.HissuLabelVisible = False
        Me.numStdIrime.IsHissuCheck = False
        Me.numStdIrime.IsRangeCheck = False
        Me.numStdIrime.ItemName = ""
        Me.numStdIrime.Location = New System.Drawing.Point(907, 296)
        Me.numStdIrime.Name = "numStdIrime"
        Me.numStdIrime.ReadOnly = True
        Me.numStdIrime.Size = New System.Drawing.Size(100, 18)
        Me.numStdIrime.TabIndex = 108
        Me.numStdIrime.TabStop = False
        Me.numStdIrime.TabStopSetting = False
        Me.numStdIrime.TextValue = "0"
        Me.numStdIrime.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStdIrime.WidthDef = 100
        '
        'txtOrderNoM
        '
        Me.txtOrderNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrderNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrderNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNoM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNoM.CountWrappedLine = False
        Me.txtOrderNoM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNoM.HeightDef = 18
        Me.txtOrderNoM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoM.HissuLabelVisible = False
        Me.txtOrderNoM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtOrderNoM.IsByteCheck = 30
        Me.txtOrderNoM.IsCalendarCheck = False
        Me.txtOrderNoM.IsDakutenCheck = False
        Me.txtOrderNoM.IsEisuCheck = False
        Me.txtOrderNoM.IsForbiddenWordsCheck = False
        Me.txtOrderNoM.IsFullByteCheck = 0
        Me.txtOrderNoM.IsHankakuCheck = False
        Me.txtOrderNoM.IsHissuCheck = False
        Me.txtOrderNoM.IsKanaCheck = False
        Me.txtOrderNoM.IsMiddleSpace = False
        Me.txtOrderNoM.IsNumericCheck = False
        Me.txtOrderNoM.IsSujiCheck = False
        Me.txtOrderNoM.IsZenkakuCheck = False
        Me.txtOrderNoM.ItemName = ""
        Me.txtOrderNoM.LineSpace = 0
        Me.txtOrderNoM.Location = New System.Drawing.Point(115, 338)
        Me.txtOrderNoM.MaxLength = 30
        Me.txtOrderNoM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNoM.MaxLineCount = 0
        Me.txtOrderNoM.Multiline = False
        Me.txtOrderNoM.Name = "txtOrderNoM"
        Me.txtOrderNoM.ReadOnly = False
        Me.txtOrderNoM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNoM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNoM.Size = New System.Drawing.Size(285, 18)
        Me.txtOrderNoM.TabIndex = 24
        Me.txtOrderNoM.TabStopSetting = True
        Me.txtOrderNoM.TextValue = ""
        Me.txtOrderNoM.UseSystemPasswordChar = False
        Me.txtOrderNoM.WidthDef = 285
        Me.txtOrderNoM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numIrisu
        '
        Me.numIrisu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrisu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrisu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu.HeightDef = 18
        Me.numIrisu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.HissuLabelVisible = False
        Me.numIrisu.IsHissuCheck = False
        Me.numIrisu.IsRangeCheck = False
        Me.numIrisu.ItemName = ""
        Me.numIrisu.Location = New System.Drawing.Point(673, 296)
        Me.numIrisu.Name = "numIrisu"
        Me.numIrisu.ReadOnly = True
        Me.numIrisu.Size = New System.Drawing.Size(130, 18)
        Me.numIrisu.TabIndex = 106
        Me.numIrisu.TabStop = False
        Me.numIrisu.TabStopSetting = False
        Me.numIrisu.TextValue = "0"
        Me.numIrisu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrisu.WidthDef = 130
        '
        'lblTitleOrderNoM
        '
        Me.lblTitleOrderNoM.AutoSize = True
        Me.lblTitleOrderNoM.AutoSizeDef = True
        Me.lblTitleOrderNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrderNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrderNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrderNoM.EnableStatus = False
        Me.lblTitleOrderNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrderNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrderNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrderNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrderNoM.HeightDef = 13
        Me.lblTitleOrderNoM.Location = New System.Drawing.Point(24, 341)
        Me.lblTitleOrderNoM.Name = "lblTitleOrderNoM"
        Me.lblTitleOrderNoM.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleOrderNoM.TabIndex = 23
        Me.lblTitleOrderNoM.Text = "オーダー番号"
        Me.lblTitleOrderNoM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrderNoM.TextValue = "オーダー番号"
        Me.lblTitleOrderNoM.WidthDef = 91
        '
        'lblTitleIrisu
        '
        Me.lblTitleIrisu.AutoSize = True
        Me.lblTitleIrisu.AutoSizeDef = True
        Me.lblTitleIrisu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrisu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrisu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrisu.EnableStatus = False
        Me.lblTitleIrisu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrisu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrisu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrisu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrisu.HeightDef = 13
        Me.lblTitleIrisu.Location = New System.Drawing.Point(639, 299)
        Me.lblTitleIrisu.Name = "lblTitleIrisu"
        Me.lblTitleIrisu.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleIrisu.TabIndex = 10
        Me.lblTitleIrisu.Text = "入数"
        Me.lblTitleIrisu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrisu.TextValue = "入数"
        Me.lblTitleIrisu.WidthDef = 35
        '
        'numSumBetu
        '
        Me.numSumBetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumBetu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumBetu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumBetu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumBetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumBetu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumBetu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumBetu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumBetu.HeightDef = 18
        Me.numSumBetu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumBetu.HissuLabelVisible = False
        Me.numSumBetu.IsHissuCheck = False
        Me.numSumBetu.IsRangeCheck = False
        Me.numSumBetu.ItemName = ""
        Me.numSumBetu.Location = New System.Drawing.Point(895, 52)
        Me.numSumBetu.Name = "numSumBetu"
        Me.numSumBetu.ReadOnly = True
        Me.numSumBetu.Size = New System.Drawing.Size(118, 18)
        Me.numSumBetu.TabIndex = 114
        Me.numSumBetu.TabStop = False
        Me.numSumBetu.TabStopSetting = False
        Me.numSumBetu.TextValue = "0"
        Me.numSumBetu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumBetu.WidthDef = 118
        '
        'lblTitleStdIrime
        '
        Me.lblTitleStdIrime.AutoSize = True
        Me.lblTitleStdIrime.AutoSizeDef = True
        Me.lblTitleStdIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleStdIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleStdIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleStdIrime.EnableStatus = False
        Me.lblTitleStdIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleStdIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleStdIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleStdIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleStdIrime.HeightDef = 13
        Me.lblTitleStdIrime.Location = New System.Drawing.Point(844, 299)
        Me.lblTitleStdIrime.Name = "lblTitleStdIrime"
        Me.lblTitleStdIrime.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleStdIrime.TabIndex = 29
        Me.lblTitleStdIrime.Text = "標準入目"
        Me.lblTitleStdIrime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleStdIrime.TextValue = "標準入目"
        Me.lblTitleStdIrime.WidthDef = 63
        '
        'lblSumAntTani
        '
        Me.lblSumAntTani.AutoSize = True
        Me.lblSumAntTani.AutoSizeDef = True
        Me.lblSumAntTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSumAntTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSumAntTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSumAntTani.EnableStatus = False
        Me.lblSumAntTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSumAntTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSumAntTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSumAntTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSumAntTani.HeightDef = 13
        Me.lblSumAntTani.Location = New System.Drawing.Point(225, 320)
        Me.lblSumAntTani.Name = "lblSumAntTani"
        Me.lblSumAntTani.Size = New System.Drawing.Size(105, 13)
        Me.lblSumAntTani.TabIndex = 88
        Me.lblSumAntTani.Text = "マイクログラム"
        Me.lblSumAntTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSumAntTani.TextValue = "マイクログラム"
        Me.lblSumAntTani.WidthDef = 105
        '
        'lblTitleSumAnt
        '
        Me.lblTitleSumAnt.AutoSize = True
        Me.lblTitleSumAnt.AutoSizeDef = True
        Me.lblTitleSumAnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSumAnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSumAnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSumAnt.EnableStatus = False
        Me.lblTitleSumAnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSumAnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSumAnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSumAnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSumAnt.HeightDef = 13
        Me.lblTitleSumAnt.Location = New System.Drawing.Point(80, 320)
        Me.lblTitleSumAnt.Name = "lblTitleSumAnt"
        Me.lblTitleSumAnt.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSumAnt.TabIndex = 31
        Me.lblTitleSumAnt.Text = "数量"
        Me.lblTitleSumAnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSumAnt.TextValue = "数量"
        Me.lblTitleSumAnt.WidthDef = 35
        '
        'lblTitleSumCnt
        '
        Me.lblTitleSumCnt.AutoSize = True
        Me.lblTitleSumCnt.AutoSizeDef = True
        Me.lblTitleSumCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSumCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSumCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSumCnt.EnableStatus = False
        Me.lblTitleSumCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSumCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSumCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSumCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSumCnt.HeightDef = 13
        Me.lblTitleSumCnt.Location = New System.Drawing.Point(347, 320)
        Me.lblTitleSumCnt.Name = "lblTitleSumCnt"
        Me.lblTitleSumCnt.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSumCnt.TabIndex = 48
        Me.lblTitleSumCnt.Text = "個数"
        Me.lblTitleSumCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSumCnt.TextValue = "個数"
        Me.lblTitleSumCnt.WidthDef = 35
        '
        'numSumAnt
        '
        Me.numSumAnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumAnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumAnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt.HeightDef = 18
        Me.numSumAnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt.HissuLabelVisible = False
        Me.numSumAnt.IsHissuCheck = False
        Me.numSumAnt.IsRangeCheck = False
        Me.numSumAnt.ItemName = ""
        Me.numSumAnt.Location = New System.Drawing.Point(115, 317)
        Me.numSumAnt.Name = "numSumAnt"
        Me.numSumAnt.ReadOnly = True
        Me.numSumAnt.Size = New System.Drawing.Size(125, 18)
        Me.numSumAnt.TabIndex = 112
        Me.numSumAnt.TabStop = False
        Me.numSumAnt.TabStopSetting = False
        Me.numSumAnt.TextValue = "0"
        Me.numSumAnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumAnt.WidthDef = 125
        '
        'numSumCnt
        '
        Me.numSumCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSumCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSumCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt.HeightDef = 18
        Me.numSumCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt.HissuLabelVisible = False
        Me.numSumCnt.IsHissuCheck = False
        Me.numSumCnt.IsRangeCheck = False
        Me.numSumCnt.ItemName = ""
        Me.numSumCnt.Location = New System.Drawing.Point(382, 317)
        Me.numSumCnt.Name = "numSumCnt"
        Me.numSumCnt.ReadOnly = False
        Me.numSumCnt.Size = New System.Drawing.Size(125, 18)
        Me.numSumCnt.TabIndex = 113
        Me.numSumCnt.TabStopSetting = True
        Me.numSumCnt.TextValue = "0"
        Me.numSumCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumCnt.WidthDef = 125
        '
        'tabUnso
        '
        Me.tabUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.tabUnso.Controls.Add(Me.pnlUnso)
        Me.tabUnso.Location = New System.Drawing.Point(4, 22)
        Me.tabUnso.Name = "tabUnso"
        Me.tabUnso.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUnso.Size = New System.Drawing.Size(1241, 385)
        Me.tabUnso.TabIndex = 0
        Me.tabUnso.Text = "その他・手配情報"
        '
        'pnlUnso
        '
        Me.pnlUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlUnso.Controls.Add(Me.lblTitleUnchinTehai)
        Me.pnlUnso.Controls.Add(Me.lblTitleYen)
        Me.pnlUnso.Controls.Add(Me.lblShukkaMotoNm)
        Me.pnlUnso.Controls.Add(Me.lblTitleShukkaMotoCd)
        Me.pnlUnso.Controls.Add(Me.lblTariffNm)
        Me.pnlUnso.Controls.Add(Me.txtTariffCd)
        Me.pnlUnso.Controls.Add(Me.numUnchin)
        Me.pnlUnso.Controls.Add(Me.lblTitleTariff)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnchin)
        Me.pnlUnso.Controls.Add(Me.lblUnsoNm)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnso)
        Me.pnlUnso.Controls.Add(Me.txtUnsoBrCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleOnkan)
        Me.pnlUnso.Controls.Add(Me.cmbOnkan)
        Me.pnlUnso.Controls.Add(Me.cmbUnchinTehai)
        Me.pnlUnso.Controls.Add(Me.lblTitleSharyoKbn)
        Me.pnlUnso.Controls.Add(Me.cmbSharyoKbn)
        Me.pnlUnso.Controls.Add(Me.cmbTariffKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleTariffKbn)
        Me.pnlUnso.Controls.Add(Me.txtUnsoCd)
        Me.pnlUnso.Controls.Add(Me.txtShukkaMotoCd)
        Me.pnlUnso.EnableStatus = False
        Me.pnlUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.HeightDef = 195
        Me.pnlUnso.Location = New System.Drawing.Point(21, 16)
        Me.pnlUnso.Name = "pnlUnso"
        Me.pnlUnso.Size = New System.Drawing.Size(1011, 195)
        Me.pnlUnso.TabIndex = 119
        Me.pnlUnso.TabStop = False
        Me.pnlUnso.Text = "運送情報"
        Me.pnlUnso.TextValue = "運送情報"
        Me.pnlUnso.WidthDef = 1011
        '
        'lblTitleUnchinTehai
        '
        Me.lblTitleUnchinTehai.AutoSize = True
        Me.lblTitleUnchinTehai.AutoSizeDef = True
        Me.lblTitleUnchinTehai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchinTehai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchinTehai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnchinTehai.EnableStatus = False
        Me.lblTitleUnchinTehai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchinTehai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchinTehai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchinTehai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchinTehai.HeightDef = 13
        Me.lblTitleUnchinTehai.Location = New System.Drawing.Point(32, 26)
        Me.lblTitleUnchinTehai.Name = "lblTitleUnchinTehai"
        Me.lblTitleUnchinTehai.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnchinTehai.TabIndex = 35
        Me.lblTitleUnchinTehai.Text = "運送手配"
        Me.lblTitleUnchinTehai.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnchinTehai.TextValue = "運送手配"
        Me.lblTitleUnchinTehai.WidthDef = 63
        '
        'lblTitleYen
        '
        Me.lblTitleYen.AutoSize = True
        Me.lblTitleYen.AutoSizeDef = True
        Me.lblTitleYen.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen.EnableStatus = False
        Me.lblTitleYen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen.HeightDef = 13
        Me.lblTitleYen.Location = New System.Drawing.Point(959, 26)
        Me.lblTitleYen.Name = "lblTitleYen"
        Me.lblTitleYen.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen.TabIndex = 117
        Me.lblTitleYen.Text = "円"
        Me.lblTitleYen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen.TextValue = "円"
        Me.lblTitleYen.WidthDef = 21
        '
        'lblShukkaMotoNm
        '
        Me.lblShukkaMotoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShukkaMotoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShukkaMotoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShukkaMotoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShukkaMotoNm.CountWrappedLine = False
        Me.lblShukkaMotoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblShukkaMotoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaMotoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaMotoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShukkaMotoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShukkaMotoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblShukkaMotoNm.HeightDef = 18
        Me.lblShukkaMotoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShukkaMotoNm.HissuLabelVisible = False
        Me.lblShukkaMotoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblShukkaMotoNm.IsByteCheck = 0
        Me.lblShukkaMotoNm.IsCalendarCheck = False
        Me.lblShukkaMotoNm.IsDakutenCheck = False
        Me.lblShukkaMotoNm.IsEisuCheck = False
        Me.lblShukkaMotoNm.IsForbiddenWordsCheck = False
        Me.lblShukkaMotoNm.IsFullByteCheck = 0
        Me.lblShukkaMotoNm.IsHankakuCheck = False
        Me.lblShukkaMotoNm.IsHissuCheck = False
        Me.lblShukkaMotoNm.IsKanaCheck = False
        Me.lblShukkaMotoNm.IsMiddleSpace = False
        Me.lblShukkaMotoNm.IsNumericCheck = False
        Me.lblShukkaMotoNm.IsSujiCheck = False
        Me.lblShukkaMotoNm.IsZenkakuCheck = False
        Me.lblShukkaMotoNm.ItemName = ""
        Me.lblShukkaMotoNm.LineSpace = 0
        Me.lblShukkaMotoNm.Location = New System.Drawing.Point(222, 93)
        Me.lblShukkaMotoNm.MaxLength = 0
        Me.lblShukkaMotoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblShukkaMotoNm.MaxLineCount = 0
        Me.lblShukkaMotoNm.Multiline = False
        Me.lblShukkaMotoNm.Name = "lblShukkaMotoNm"
        Me.lblShukkaMotoNm.ReadOnly = True
        Me.lblShukkaMotoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblShukkaMotoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblShukkaMotoNm.Size = New System.Drawing.Size(442, 18)
        Me.lblShukkaMotoNm.TabIndex = 67
        Me.lblShukkaMotoNm.TabStop = False
        Me.lblShukkaMotoNm.TabStopSetting = False
        Me.lblShukkaMotoNm.TextValue = ""
        Me.lblShukkaMotoNm.UseSystemPasswordChar = False
        Me.lblShukkaMotoNm.WidthDef = 442
        Me.lblShukkaMotoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleShukkaMotoCd
        '
        Me.lblTitleShukkaMotoCd.AutoSize = True
        Me.lblTitleShukkaMotoCd.AutoSizeDef = True
        Me.lblTitleShukkaMotoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShukkaMotoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShukkaMotoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShukkaMotoCd.EnableStatus = False
        Me.lblTitleShukkaMotoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShukkaMotoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShukkaMotoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShukkaMotoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShukkaMotoCd.HeightDef = 13
        Me.lblTitleShukkaMotoCd.Location = New System.Drawing.Point(46, 96)
        Me.lblTitleShukkaMotoCd.Name = "lblTitleShukkaMotoCd"
        Me.lblTitleShukkaMotoCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleShukkaMotoCd.TabIndex = 73
        Me.lblTitleShukkaMotoCd.Text = "出荷元"
        Me.lblTitleShukkaMotoCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShukkaMotoCd.TextValue = "出荷元"
        Me.lblTitleShukkaMotoCd.WidthDef = 49
        '
        'lblTariffNm
        '
        Me.lblTariffNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTariffNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTariffNm.CountWrappedLine = False
        Me.lblTariffNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTariffNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTariffNm.HeightDef = 18
        Me.lblTariffNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.HissuLabelVisible = False
        Me.lblTariffNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTariffNm.IsByteCheck = 0
        Me.lblTariffNm.IsCalendarCheck = False
        Me.lblTariffNm.IsDakutenCheck = False
        Me.lblTariffNm.IsEisuCheck = False
        Me.lblTariffNm.IsForbiddenWordsCheck = False
        Me.lblTariffNm.IsFullByteCheck = 0
        Me.lblTariffNm.IsHankakuCheck = False
        Me.lblTariffNm.IsHissuCheck = False
        Me.lblTariffNm.IsKanaCheck = False
        Me.lblTariffNm.IsMiddleSpace = False
        Me.lblTariffNm.IsNumericCheck = False
        Me.lblTariffNm.IsSujiCheck = False
        Me.lblTariffNm.IsZenkakuCheck = False
        Me.lblTariffNm.ItemName = ""
        Me.lblTariffNm.LineSpace = 0
        Me.lblTariffNm.Location = New System.Drawing.Point(181, 70)
        Me.lblTariffNm.MaxLength = 0
        Me.lblTariffNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffNm.MaxLineCount = 0
        Me.lblTariffNm.Multiline = False
        Me.lblTariffNm.Name = "lblTariffNm"
        Me.lblTariffNm.ReadOnly = True
        Me.lblTariffNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffNm.Size = New System.Drawing.Size(482, 18)
        Me.lblTariffNm.TabIndex = 113
        Me.lblTariffNm.TabStop = False
        Me.lblTariffNm.TabStopSetting = False
        Me.lblTariffNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblTariffNm.UseSystemPasswordChar = False
        Me.lblTariffNm.WidthDef = 482
        Me.lblTariffNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtTariffCd
        '
        Me.txtTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTariffCd.CountWrappedLine = False
        Me.txtTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTariffCd.HeightDef = 18
        Me.txtTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTariffCd.HissuLabelVisible = False
        Me.txtTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtTariffCd.IsByteCheck = 10
        Me.txtTariffCd.IsCalendarCheck = False
        Me.txtTariffCd.IsDakutenCheck = False
        Me.txtTariffCd.IsEisuCheck = False
        Me.txtTariffCd.IsForbiddenWordsCheck = False
        Me.txtTariffCd.IsFullByteCheck = 0
        Me.txtTariffCd.IsHankakuCheck = False
        Me.txtTariffCd.IsHissuCheck = False
        Me.txtTariffCd.IsKanaCheck = False
        Me.txtTariffCd.IsMiddleSpace = False
        Me.txtTariffCd.IsNumericCheck = False
        Me.txtTariffCd.IsSujiCheck = False
        Me.txtTariffCd.IsZenkakuCheck = False
        Me.txtTariffCd.ItemName = ""
        Me.txtTariffCd.LineSpace = 0
        Me.txtTariffCd.Location = New System.Drawing.Point(101, 70)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(97, 18)
        Me.txtTariffCd.TabIndex = 112
        Me.txtTariffCd.TabStopSetting = True
        Me.txtTariffCd.TextValue = "X---10---X"
        Me.txtTariffCd.UseSystemPasswordChar = False
        Me.txtTariffCd.WidthDef = 97
        Me.txtTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numUnchin
        '
        Me.numUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchin.HeightDef = 18
        Me.numUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchin.HissuLabelVisible = False
        Me.numUnchin.IsHissuCheck = False
        Me.numUnchin.IsRangeCheck = False
        Me.numUnchin.ItemName = ""
        Me.numUnchin.Location = New System.Drawing.Point(853, 24)
        Me.numUnchin.Name = "numUnchin"
        Me.numUnchin.ReadOnly = False
        Me.numUnchin.Size = New System.Drawing.Size(117, 18)
        Me.numUnchin.TabIndex = 110
        Me.numUnchin.TabStopSetting = True
        Me.numUnchin.TextValue = "0"
        Me.numUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnchin.WidthDef = 117
        '
        'lblTitleTariff
        '
        Me.lblTitleTariff.AutoSize = True
        Me.lblTitleTariff.AutoSizeDef = True
        Me.lblTitleTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariff.EnableStatus = False
        Me.lblTitleTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.HeightDef = 13
        Me.lblTitleTariff.Location = New System.Drawing.Point(18, 74)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariff.TabIndex = 111
        Me.lblTitleTariff.Text = "運送タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "運送タリフ"
        Me.lblTitleTariff.WidthDef = 77
        '
        'lblTitleUnchin
        '
        Me.lblTitleUnchin.AutoSize = True
        Me.lblTitleUnchin.AutoSizeDef = True
        Me.lblTitleUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnchin.EnableStatus = False
        Me.lblTitleUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchin.HeightDef = 13
        Me.lblTitleUnchin.Location = New System.Drawing.Point(812, 26)
        Me.lblTitleUnchin.Name = "lblTitleUnchin"
        Me.lblTitleUnchin.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleUnchin.TabIndex = 85
        Me.lblTitleUnchin.Text = "運賃"
        Me.lblTitleUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnchin.TextValue = "運賃"
        Me.lblTitleUnchin.WidthDef = 35
        '
        'lblUnsoNm
        '
        Me.lblUnsoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoNm.CountWrappedLine = False
        Me.lblUnsoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoNm.HeightDef = 18
        Me.lblUnsoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.HissuLabelVisible = False
        Me.lblUnsoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsoNm.IsByteCheck = 0
        Me.lblUnsoNm.IsCalendarCheck = False
        Me.lblUnsoNm.IsDakutenCheck = False
        Me.lblUnsoNm.IsEisuCheck = False
        Me.lblUnsoNm.IsForbiddenWordsCheck = False
        Me.lblUnsoNm.IsFullByteCheck = 0
        Me.lblUnsoNm.IsHankakuCheck = False
        Me.lblUnsoNm.IsHissuCheck = False
        Me.lblUnsoNm.IsKanaCheck = False
        Me.lblUnsoNm.IsMiddleSpace = False
        Me.lblUnsoNm.IsNumericCheck = False
        Me.lblUnsoNm.IsSujiCheck = False
        Me.lblUnsoNm.IsZenkakuCheck = False
        Me.lblUnsoNm.ItemName = ""
        Me.lblUnsoNm.LineSpace = 0
        Me.lblUnsoNm.Location = New System.Drawing.Point(182, 47)
        Me.lblUnsoNm.MaxLength = 0
        Me.lblUnsoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoNm.MaxLineCount = 0
        Me.lblUnsoNm.Multiline = False
        Me.lblUnsoNm.Name = "lblUnsoNm"
        Me.lblUnsoNm.ReadOnly = True
        Me.lblUnsoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoNm.Size = New System.Drawing.Size(482, 18)
        Me.lblUnsoNm.TabIndex = 82
        Me.lblUnsoNm.TabStop = False
        Me.lblUnsoNm.TabStopSetting = False
        Me.lblUnsoNm.TextValue = ""
        Me.lblUnsoNm.UseSystemPasswordChar = False
        Me.lblUnsoNm.WidthDef = 482
        Me.lblUnsoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnso
        '
        Me.lblTitleUnso.AutoSize = True
        Me.lblTitleUnso.AutoSizeDef = True
        Me.lblTitleUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnso.EnableStatus = False
        Me.lblTitleUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnso.HeightDef = 13
        Me.lblTitleUnso.Location = New System.Drawing.Point(32, 49)
        Me.lblTitleUnso.Name = "lblTitleUnso"
        Me.lblTitleUnso.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnso.TabIndex = 83
        Me.lblTitleUnso.Text = "運送会社"
        Me.lblTitleUnso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnso.TextValue = "運送会社"
        Me.lblTitleUnso.WidthDef = 63
        '
        'txtUnsoBrCd
        '
        Me.txtUnsoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsoBrCd.CountWrappedLine = False
        Me.txtUnsoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsoBrCd.HeightDef = 18
        Me.txtUnsoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoBrCd.HissuLabelVisible = False
        Me.txtUnsoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsoBrCd.IsByteCheck = 3
        Me.txtUnsoBrCd.IsCalendarCheck = False
        Me.txtUnsoBrCd.IsDakutenCheck = False
        Me.txtUnsoBrCd.IsEisuCheck = False
        Me.txtUnsoBrCd.IsForbiddenWordsCheck = False
        Me.txtUnsoBrCd.IsFullByteCheck = 0
        Me.txtUnsoBrCd.IsHankakuCheck = False
        Me.txtUnsoBrCd.IsHissuCheck = False
        Me.txtUnsoBrCd.IsKanaCheck = False
        Me.txtUnsoBrCd.IsMiddleSpace = False
        Me.txtUnsoBrCd.IsNumericCheck = False
        Me.txtUnsoBrCd.IsSujiCheck = False
        Me.txtUnsoBrCd.IsZenkakuCheck = False
        Me.txtUnsoBrCd.ItemName = ""
        Me.txtUnsoBrCd.LineSpace = 0
        Me.txtUnsoBrCd.Location = New System.Drawing.Point(155, 47)
        Me.txtUnsoBrCd.MaxLength = 3
        Me.txtUnsoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsoBrCd.MaxLineCount = 0
        Me.txtUnsoBrCd.Multiline = False
        Me.txtUnsoBrCd.Name = "txtUnsoBrCd"
        Me.txtUnsoBrCd.ReadOnly = False
        Me.txtUnsoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsoBrCd.Size = New System.Drawing.Size(43, 18)
        Me.txtUnsoBrCd.TabIndex = 81
        Me.txtUnsoBrCd.TabStopSetting = True
        Me.txtUnsoBrCd.TextValue = "XXX"
        Me.txtUnsoBrCd.UseSystemPasswordChar = False
        Me.txtUnsoBrCd.WidthDef = 43
        Me.txtUnsoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOnkan
        '
        Me.lblTitleOnkan.AutoSize = True
        Me.lblTitleOnkan.AutoSizeDef = True
        Me.lblTitleOnkan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOnkan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOnkan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOnkan.EnableStatus = False
        Me.lblTitleOnkan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOnkan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOnkan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOnkan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOnkan.HeightDef = 13
        Me.lblTitleOnkan.Location = New System.Drawing.Point(608, 26)
        Me.lblTitleOnkan.Name = "lblTitleOnkan"
        Me.lblTitleOnkan.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleOnkan.TabIndex = 79
        Me.lblTitleOnkan.Text = "温度"
        Me.lblTitleOnkan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOnkan.TextValue = "温度"
        Me.lblTitleOnkan.WidthDef = 35
        '
        'cmbOnkan
        '
        Me.cmbOnkan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbOnkan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbOnkan.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbOnkan.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbOnkan.DataCode = "U006"
        Me.cmbOnkan.DataSource = Nothing
        Me.cmbOnkan.DisplayMember = Nothing
        Me.cmbOnkan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOnkan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOnkan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOnkan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOnkan.HeightDef = 18
        Me.cmbOnkan.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbOnkan.HissuLabelVisible = False
        Me.cmbOnkan.InsertWildCard = True
        Me.cmbOnkan.IsForbiddenWordsCheck = False
        Me.cmbOnkan.IsHissuCheck = False
        Me.cmbOnkan.ItemName = ""
        Me.cmbOnkan.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbOnkan.Location = New System.Drawing.Point(649, 24)
        Me.cmbOnkan.Name = "cmbOnkan"
        Me.cmbOnkan.ReadOnly = False
        Me.cmbOnkan.SelectedIndex = -1
        Me.cmbOnkan.SelectedItem = Nothing
        Me.cmbOnkan.SelectedText = ""
        Me.cmbOnkan.SelectedValue = ""
        Me.cmbOnkan.Size = New System.Drawing.Size(157, 18)
        Me.cmbOnkan.TabIndex = 78
        Me.cmbOnkan.TabStopSetting = True
        Me.cmbOnkan.TextValue = ""
        Me.cmbOnkan.Value1 = Nothing
        Me.cmbOnkan.Value2 = Nothing
        Me.cmbOnkan.Value3 = Nothing
        Me.cmbOnkan.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbOnkan.ValueMember = Nothing
        Me.cmbOnkan.WidthDef = 157
        '
        'cmbUnchinTehai
        '
        Me.cmbUnchinTehai.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnchinTehai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnchinTehai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbUnchinTehai.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbUnchinTehai.DataCode = "U005"
        Me.cmbUnchinTehai.DataSource = Nothing
        Me.cmbUnchinTehai.DisplayMember = Nothing
        Me.cmbUnchinTehai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnchinTehai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnchinTehai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnchinTehai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnchinTehai.HeightDef = 18
        Me.cmbUnchinTehai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbUnchinTehai.HissuLabelVisible = False
        Me.cmbUnchinTehai.InsertWildCard = True
        Me.cmbUnchinTehai.IsForbiddenWordsCheck = False
        Me.cmbUnchinTehai.IsHissuCheck = False
        Me.cmbUnchinTehai.ItemName = ""
        Me.cmbUnchinTehai.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbUnchinTehai.Location = New System.Drawing.Point(101, 24)
        Me.cmbUnchinTehai.Name = "cmbUnchinTehai"
        Me.cmbUnchinTehai.ReadOnly = False
        Me.cmbUnchinTehai.SelectedIndex = -1
        Me.cmbUnchinTehai.SelectedItem = Nothing
        Me.cmbUnchinTehai.SelectedText = ""
        Me.cmbUnchinTehai.SelectedValue = ""
        Me.cmbUnchinTehai.Size = New System.Drawing.Size(97, 18)
        Me.cmbUnchinTehai.TabIndex = 42
        Me.cmbUnchinTehai.TabStopSetting = True
        Me.cmbUnchinTehai.TextValue = ""
        Me.cmbUnchinTehai.Value1 = Nothing
        Me.cmbUnchinTehai.Value2 = Nothing
        Me.cmbUnchinTehai.Value3 = Nothing
        Me.cmbUnchinTehai.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbUnchinTehai.ValueMember = Nothing
        Me.cmbUnchinTehai.WidthDef = 97
        '
        'lblTitleSharyoKbn
        '
        Me.lblTitleSharyoKbn.AutoSize = True
        Me.lblTitleSharyoKbn.AutoSizeDef = True
        Me.lblTitleSharyoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSharyoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSharyoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSharyoKbn.EnableStatus = False
        Me.lblTitleSharyoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSharyoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSharyoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSharyoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSharyoKbn.HeightDef = 13
        Me.lblTitleSharyoKbn.Location = New System.Drawing.Point(431, 26)
        Me.lblTitleSharyoKbn.Name = "lblTitleSharyoKbn"
        Me.lblTitleSharyoKbn.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSharyoKbn.TabIndex = 77
        Me.lblTitleSharyoKbn.Text = "車輌"
        Me.lblTitleSharyoKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSharyoKbn.TextValue = "車輌"
        Me.lblTitleSharyoKbn.WidthDef = 35
        '
        'cmbSharyoKbn
        '
        Me.cmbSharyoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSharyoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSharyoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSharyoKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSharyoKbn.DataCode = "S012"
        Me.cmbSharyoKbn.DataSource = Nothing
        Me.cmbSharyoKbn.DisplayMember = Nothing
        Me.cmbSharyoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSharyoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSharyoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSharyoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSharyoKbn.HeightDef = 18
        Me.cmbSharyoKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSharyoKbn.HissuLabelVisible = False
        Me.cmbSharyoKbn.InsertWildCard = True
        Me.cmbSharyoKbn.IsForbiddenWordsCheck = False
        Me.cmbSharyoKbn.IsHissuCheck = False
        Me.cmbSharyoKbn.ItemName = ""
        Me.cmbSharyoKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSharyoKbn.Location = New System.Drawing.Point(472, 24)
        Me.cmbSharyoKbn.Name = "cmbSharyoKbn"
        Me.cmbSharyoKbn.ReadOnly = False
        Me.cmbSharyoKbn.SelectedIndex = -1
        Me.cmbSharyoKbn.SelectedItem = Nothing
        Me.cmbSharyoKbn.SelectedText = ""
        Me.cmbSharyoKbn.SelectedValue = ""
        Me.cmbSharyoKbn.Size = New System.Drawing.Size(132, 18)
        Me.cmbSharyoKbn.TabIndex = 70
        Me.cmbSharyoKbn.TabStopSetting = True
        Me.cmbSharyoKbn.TextValue = ""
        Me.cmbSharyoKbn.Value1 = Nothing
        Me.cmbSharyoKbn.Value2 = Nothing
        Me.cmbSharyoKbn.Value3 = Nothing
        Me.cmbSharyoKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSharyoKbn.ValueMember = Nothing
        Me.cmbSharyoKbn.WidthDef = 132
        '
        'cmbTariffKbn
        '
        Me.cmbTariffKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTariffKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTariffKbn.DataCode = "T015"
        Me.cmbTariffKbn.DataSource = Nothing
        Me.cmbTariffKbn.DisplayMember = Nothing
        Me.cmbTariffKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffKbn.HeightDef = 18
        Me.cmbTariffKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTariffKbn.HissuLabelVisible = False
        Me.cmbTariffKbn.InsertWildCard = True
        Me.cmbTariffKbn.IsForbiddenWordsCheck = False
        Me.cmbTariffKbn.IsHissuCheck = False
        Me.cmbTariffKbn.ItemName = ""
        Me.cmbTariffKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTariffKbn.Location = New System.Drawing.Point(304, 24)
        Me.cmbTariffKbn.Name = "cmbTariffKbn"
        Me.cmbTariffKbn.ReadOnly = False
        Me.cmbTariffKbn.SelectedIndex = -1
        Me.cmbTariffKbn.SelectedItem = Nothing
        Me.cmbTariffKbn.SelectedText = ""
        Me.cmbTariffKbn.SelectedValue = ""
        Me.cmbTariffKbn.Size = New System.Drawing.Size(121, 18)
        Me.cmbTariffKbn.TabIndex = 72
        Me.cmbTariffKbn.TabStopSetting = True
        Me.cmbTariffKbn.TextValue = ""
        Me.cmbTariffKbn.Value1 = Nothing
        Me.cmbTariffKbn.Value2 = Nothing
        Me.cmbTariffKbn.Value3 = Nothing
        Me.cmbTariffKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbTariffKbn.ValueMember = Nothing
        Me.cmbTariffKbn.WidthDef = 121
        '
        'lblTitleTariffKbn
        '
        Me.lblTitleTariffKbn.AutoSize = True
        Me.lblTitleTariffKbn.AutoSizeDef = True
        Me.lblTitleTariffKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariffKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariffKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariffKbn.EnableStatus = False
        Me.lblTitleTariffKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariffKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariffKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariffKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariffKbn.HeightDef = 13
        Me.lblTitleTariffKbn.Location = New System.Drawing.Point(198, 26)
        Me.lblTitleTariffKbn.Name = "lblTitleTariffKbn"
        Me.lblTitleTariffKbn.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleTariffKbn.TabIndex = 71
        Me.lblTitleTariffKbn.Text = "タリフ分類区分"
        Me.lblTitleTariffKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariffKbn.TextValue = "タリフ分類区分"
        Me.lblTitleTariffKbn.WidthDef = 105
        '
        'txtUnsoCd
        '
        Me.txtUnsoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsoCd.CountWrappedLine = False
        Me.txtUnsoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsoCd.HeightDef = 18
        Me.txtUnsoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoCd.HissuLabelVisible = False
        Me.txtUnsoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsoCd.IsByteCheck = 5
        Me.txtUnsoCd.IsCalendarCheck = False
        Me.txtUnsoCd.IsDakutenCheck = False
        Me.txtUnsoCd.IsEisuCheck = False
        Me.txtUnsoCd.IsForbiddenWordsCheck = False
        Me.txtUnsoCd.IsFullByteCheck = 0
        Me.txtUnsoCd.IsHankakuCheck = False
        Me.txtUnsoCd.IsHissuCheck = False
        Me.txtUnsoCd.IsKanaCheck = False
        Me.txtUnsoCd.IsMiddleSpace = False
        Me.txtUnsoCd.IsNumericCheck = False
        Me.txtUnsoCd.IsSujiCheck = False
        Me.txtUnsoCd.IsZenkakuCheck = False
        Me.txtUnsoCd.ItemName = ""
        Me.txtUnsoCd.LineSpace = 0
        Me.txtUnsoCd.Location = New System.Drawing.Point(101, 47)
        Me.txtUnsoCd.MaxLength = 5
        Me.txtUnsoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsoCd.MaxLineCount = 0
        Me.txtUnsoCd.Multiline = False
        Me.txtUnsoCd.Name = "txtUnsoCd"
        Me.txtUnsoCd.ReadOnly = False
        Me.txtUnsoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsoCd.Size = New System.Drawing.Size(70, 18)
        Me.txtUnsoCd.TabIndex = 80
        Me.txtUnsoCd.TabStopSetting = True
        Me.txtUnsoCd.TextValue = "XXXXX"
        Me.txtUnsoCd.UseSystemPasswordChar = False
        Me.txtUnsoCd.WidthDef = 70
        Me.txtUnsoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtShukkaMotoCd
        '
        Me.txtShukkaMotoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShukkaMotoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShukkaMotoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShukkaMotoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShukkaMotoCd.CountWrappedLine = False
        Me.txtShukkaMotoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShukkaMotoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShukkaMotoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShukkaMotoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShukkaMotoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShukkaMotoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShukkaMotoCd.HeightDef = 18
        Me.txtShukkaMotoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShukkaMotoCd.HissuLabelVisible = False
        Me.txtShukkaMotoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtShukkaMotoCd.IsByteCheck = 15
        Me.txtShukkaMotoCd.IsCalendarCheck = False
        Me.txtShukkaMotoCd.IsDakutenCheck = False
        Me.txtShukkaMotoCd.IsEisuCheck = False
        Me.txtShukkaMotoCd.IsForbiddenWordsCheck = False
        Me.txtShukkaMotoCd.IsFullByteCheck = 0
        Me.txtShukkaMotoCd.IsHankakuCheck = False
        Me.txtShukkaMotoCd.IsHissuCheck = False
        Me.txtShukkaMotoCd.IsKanaCheck = False
        Me.txtShukkaMotoCd.IsMiddleSpace = False
        Me.txtShukkaMotoCd.IsNumericCheck = False
        Me.txtShukkaMotoCd.IsSujiCheck = False
        Me.txtShukkaMotoCd.IsZenkakuCheck = False
        Me.txtShukkaMotoCd.ItemName = ""
        Me.txtShukkaMotoCd.LineSpace = 0
        Me.txtShukkaMotoCd.Location = New System.Drawing.Point(101, 93)
        Me.txtShukkaMotoCd.MaxLength = 15
        Me.txtShukkaMotoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShukkaMotoCd.MaxLineCount = 0
        Me.txtShukkaMotoCd.Multiline = False
        Me.txtShukkaMotoCd.Name = "txtShukkaMotoCd"
        Me.txtShukkaMotoCd.ReadOnly = False
        Me.txtShukkaMotoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShukkaMotoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShukkaMotoCd.Size = New System.Drawing.Size(137, 18)
        Me.txtShukkaMotoCd.TabIndex = 66
        Me.txtShukkaMotoCd.TabStopSetting = True
        Me.txtShukkaMotoCd.TextValue = "X---10---XX-5-X"
        Me.txtShukkaMotoCd.UseSystemPasswordChar = False
        Me.txtShukkaMotoCd.WidthDef = 137
        Me.txtShukkaMotoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'tabFreeL
        '
        Me.tabFreeL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.tabFreeL.Controls.Add(Me.sprFreeL)
        Me.tabFreeL.Location = New System.Drawing.Point(4, 22)
        Me.tabFreeL.Name = "tabFreeL"
        Me.tabFreeL.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFreeL.Size = New System.Drawing.Size(1241, 385)
        Me.tabFreeL.TabIndex = 2
        Me.tabFreeL.Text = "自由設定項目(大)"
        '
        'sprFreeL
        '
        Me.sprFreeL.AccessibleDescription = ""
        Me.sprFreeL.AllowUserZoom = False
        Me.sprFreeL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFreeL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFreeL.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprFreeL.CellClickEventArgs = Nothing
        Me.sprFreeL.CheckToCheckBox = True
        Me.sprFreeL.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprFreeL.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFreeL.EditModeReplace = True
        Me.sprFreeL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFreeL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFreeL.ForeColorDef = System.Drawing.Color.Empty
        Me.sprFreeL.HeightDef = 358
        Me.sprFreeL.KeyboardCheckBoxOn = False
        Me.sprFreeL.Location = New System.Drawing.Point(5, 5)
        Me.sprFreeL.Name = "sprFreeL"
        Me.sprFreeL.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFreeL.Size = New System.Drawing.Size(1230, 358)
        Me.sprFreeL.SortColumn = True
        Me.sprFreeL.SpanColumnLock = True
        Me.sprFreeL.SpreadDoubleClicked = False
        Me.sprFreeL.TabIndex = 294
        Me.sprFreeL.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprFreeL.TextValue = Nothing
        Me.sprFreeL.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprFreeL.WidthDef = 1230
        '
        'LmImTextBox1
        '
        Me.LmImTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox1.CountWrappedLine = False
        Me.LmImTextBox1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox1.HeightDef = 18
        Me.LmImTextBox1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.HissuLabelVisible = True
        Me.LmImTextBox1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox1.IsByteCheck = 30
        Me.LmImTextBox1.IsCalendarCheck = False
        Me.LmImTextBox1.IsDakutenCheck = False
        Me.LmImTextBox1.IsEisuCheck = False
        Me.LmImTextBox1.IsForbiddenWordsCheck = False
        Me.LmImTextBox1.IsFullByteCheck = 0
        Me.LmImTextBox1.IsHankakuCheck = False
        Me.LmImTextBox1.IsHissuCheck = True
        Me.LmImTextBox1.IsKanaCheck = False
        Me.LmImTextBox1.IsMiddleSpace = False
        Me.LmImTextBox1.IsNumericCheck = False
        Me.LmImTextBox1.IsSujiCheck = False
        Me.LmImTextBox1.IsZenkakuCheck = False
        Me.LmImTextBox1.ItemName = ""
        Me.LmImTextBox1.LineSpace = 0
        Me.LmImTextBox1.Location = New System.Drawing.Point(132, 30)
        Me.LmImTextBox1.MaxLength = 30
        Me.LmImTextBox1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox1.MaxLineCount = 0
        Me.LmImTextBox1.Multiline = False
        Me.LmImTextBox1.Name = "LmImTextBox1"
        Me.LmImTextBox1.ReadOnly = True
        Me.LmImTextBox1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox1.Size = New System.Drawing.Size(45, 18)
        Me.LmImTextBox1.TabIndex = 292
        Me.LmImTextBox1.TabStop = False
        Me.LmImTextBox1.TabStopSetting = False
        Me.LmImTextBox1.TextValue = ""
        Me.LmImTextBox1.UseSystemPasswordChar = False
        Me.LmImTextBox1.WidthDef = 45
        Me.LmImTextBox1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrder_tabFreeM
        '
        Me.lblTitleOrder_tabFreeM.AutoSize = True
        Me.lblTitleOrder_tabFreeM.AutoSizeDef = True
        Me.lblTitleOrder_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrder_tabFreeM.EnableStatus = False
        Me.lblTitleOrder_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrder_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrder_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.HeightDef = 13
        Me.lblTitleOrder_tabFreeM.Location = New System.Drawing.Point(91, 33)
        Me.lblTitleOrder_tabFreeM.Name = "lblTitleOrder_tabFreeM"
        Me.lblTitleOrder_tabFreeM.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleOrder_tabFreeM.TabIndex = 291
        Me.lblTitleOrder_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrder_tabFreeM.TextValue = ""
        Me.lblTitleOrder_tabFreeM.WidthDef = 35
        '
        'lblHikiate_tabFreeM
        '
        Me.lblHikiate_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHikiate_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHikiate_tabFreeM.CountWrappedLine = False
        Me.lblHikiate_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblHikiate_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiate_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiate_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiate_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiate_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblHikiate_tabFreeM.HeightDef = 18
        Me.lblHikiate_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.HissuLabelVisible = False
        Me.lblHikiate_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblHikiate_tabFreeM.IsByteCheck = 0
        Me.lblHikiate_tabFreeM.IsCalendarCheck = False
        Me.lblHikiate_tabFreeM.IsDakutenCheck = False
        Me.lblHikiate_tabFreeM.IsEisuCheck = False
        Me.lblHikiate_tabFreeM.IsForbiddenWordsCheck = False
        Me.lblHikiate_tabFreeM.IsFullByteCheck = 0
        Me.lblHikiate_tabFreeM.IsHankakuCheck = False
        Me.lblHikiate_tabFreeM.IsHissuCheck = False
        Me.lblHikiate_tabFreeM.IsKanaCheck = False
        Me.lblHikiate_tabFreeM.IsMiddleSpace = False
        Me.lblHikiate_tabFreeM.IsNumericCheck = False
        Me.lblHikiate_tabFreeM.IsSujiCheck = False
        Me.lblHikiate_tabFreeM.IsZenkakuCheck = False
        Me.lblHikiate_tabFreeM.ItemName = ""
        Me.lblHikiate_tabFreeM.LineSpace = 0
        Me.lblHikiate_tabFreeM.Location = New System.Drawing.Point(356, 30)
        Me.lblHikiate_tabFreeM.MaxLength = 0
        Me.lblHikiate_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblHikiate_tabFreeM.MaxLineCount = 0
        Me.lblHikiate_tabFreeM.Multiline = False
        Me.lblHikiate_tabFreeM.Name = "lblHikiate_tabFreeM"
        Me.lblHikiate_tabFreeM.ReadOnly = True
        Me.lblHikiate_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblHikiate_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblHikiate_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.lblHikiate_tabFreeM.TabIndex = 290
        Me.lblHikiate_tabFreeM.TabStop = False
        Me.lblHikiate_tabFreeM.TabStopSetting = False
        Me.lblHikiate_tabFreeM.TextValue = ""
        Me.lblHikiate_tabFreeM.UseSystemPasswordChar = False
        Me.lblHikiate_tabFreeM.WidthDef = 100
        Me.lblHikiate_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel60
        '
        Me.LmTitleLabel60.AutoSize = True
        Me.LmTitleLabel60.AutoSizeDef = True
        Me.LmTitleLabel60.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel60.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel60.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel60.EnableStatus = False
        Me.LmTitleLabel60.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel60.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel60.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel60.HeightDef = 13
        Me.LmTitleLabel60.Location = New System.Drawing.Point(259, 11)
        Me.LmTitleLabel60.Name = "LmTitleLabel60"
        Me.LmTitleLabel60.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel60.TabIndex = 289
        Me.LmTitleLabel60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel60.TextValue = ""
        Me.LmTitleLabel60.WidthDef = 91
        '
        'LmTitleLabel67
        '
        Me.LmTitleLabel67.AutoSize = True
        Me.LmTitleLabel67.AutoSizeDef = True
        Me.LmTitleLabel67.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel67.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel67.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel67.EnableStatus = False
        Me.LmTitleLabel67.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel67.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel67.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel67.HeightDef = 13
        Me.LmTitleLabel67.Location = New System.Drawing.Point(675, 75)
        Me.LmTitleLabel67.Name = "LmTitleLabel67"
        Me.LmTitleLabel67.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel67.TabIndex = 287
        Me.LmTitleLabel67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel67.TextValue = ""
        Me.LmTitleLabel67.WidthDef = 21
        '
        'LmImNumber1
        '
        Me.LmImNumber1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImNumber1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LmImNumber1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.HeightDef = 18
        Me.LmImNumber1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.HissuLabelVisible = False
        Me.LmImNumber1.IsHissuCheck = False
        Me.LmImNumber1.IsRangeCheck = False
        Me.LmImNumber1.ItemName = ""
        Me.LmImNumber1.Location = New System.Drawing.Point(570, 30)
        Me.LmImNumber1.Name = "LmImNumber1"
        Me.LmImNumber1.ReadOnly = True
        Me.LmImNumber1.Size = New System.Drawing.Size(79, 18)
        Me.LmImNumber1.TabIndex = 286
        Me.LmImNumber1.TabStop = False
        Me.LmImNumber1.TabStopSetting = False
        Me.LmImNumber1.TextValue = "0"
        Me.LmImNumber1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.LmImNumber1.WidthDef = 79
        '
        'lblTARE_tabFreeM
        '
        Me.lblTARE_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTARE_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTARE_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTARE_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTARE_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTARE_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTARE_tabFreeM.HeightDef = 18
        Me.lblTARE_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.HissuLabelVisible = False
        Me.lblTARE_tabFreeM.IsHissuCheck = False
        Me.lblTARE_tabFreeM.IsRangeCheck = False
        Me.lblTARE_tabFreeM.ItemName = ""
        Me.lblTARE_tabFreeM.Location = New System.Drawing.Point(570, 72)
        Me.lblTARE_tabFreeM.Name = "lblTARE_tabFreeM"
        Me.lblTARE_tabFreeM.ReadOnly = True
        Me.lblTARE_tabFreeM.Size = New System.Drawing.Size(117, 18)
        Me.lblTARE_tabFreeM.TabIndex = 285
        Me.lblTARE_tabFreeM.TabStop = False
        Me.lblTARE_tabFreeM.TabStopSetting = False
        Me.lblTARE_tabFreeM.TextValue = "0"
        Me.lblTARE_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblTARE_tabFreeM.WidthDef = 117
        '
        'LmTitleLabel68
        '
        Me.LmTitleLabel68.AutoSize = True
        Me.LmTitleLabel68.AutoSizeDef = True
        Me.LmTitleLabel68.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel68.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel68.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel68.EnableStatus = False
        Me.LmTitleLabel68.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel68.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel68.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel68.HeightDef = 13
        Me.LmTitleLabel68.Location = New System.Drawing.Point(529, 75)
        Me.LmTitleLabel68.Name = "LmTitleLabel68"
        Me.LmTitleLabel68.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel68.TabIndex = 284
        Me.LmTitleLabel68.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel68.TextValue = ""
        Me.LmTitleLabel68.WidthDef = 35
        '
        'LmTitleLabel72
        '
        Me.LmTitleLabel72.AutoSize = True
        Me.LmTitleLabel72.AutoSizeDef = True
        Me.LmTitleLabel72.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel72.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel72.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel72.EnableStatus = False
        Me.LmTitleLabel72.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel72.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel72.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel72.HeightDef = 13
        Me.LmTitleLabel72.Location = New System.Drawing.Point(529, 33)
        Me.LmTitleLabel72.Name = "LmTitleLabel72"
        Me.LmTitleLabel72.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel72.TabIndex = 283
        Me.LmTitleLabel72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel72.TextValue = ""
        Me.LmTitleLabel72.WidthDef = 35
        '
        'LmTitleLabel73
        '
        Me.LmTitleLabel73.AutoSize = True
        Me.LmTitleLabel73.AutoSizeDef = True
        Me.LmTitleLabel73.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel73.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel73.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel73.EnableStatus = False
        Me.LmTitleLabel73.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel73.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel73.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel73.HeightDef = 13
        Me.LmTitleLabel73.Location = New System.Drawing.Point(444, 54)
        Me.LmTitleLabel73.Name = "LmTitleLabel73"
        Me.LmTitleLabel73.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel73.TabIndex = 282
        Me.LmTitleLabel73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel73.TextValue = ""
        Me.LmTitleLabel73.WidthDef = 77
        '
        'txtHasu_tabFreeM
        '
        Me.txtHasu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHasu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtHasu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHasu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHasu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtHasu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtHasu_tabFreeM.HeightDef = 18
        Me.txtHasu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.HissuLabelVisible = False
        Me.txtHasu_tabFreeM.IsHissuCheck = False
        Me.txtHasu_tabFreeM.IsRangeCheck = False
        Me.txtHasu_tabFreeM.ItemName = ""
        Me.txtHasu_tabFreeM.Location = New System.Drawing.Point(356, 51)
        Me.txtHasu_tabFreeM.Name = "txtHasu_tabFreeM"
        Me.txtHasu_tabFreeM.ReadOnly = True
        Me.txtHasu_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.txtHasu_tabFreeM.TabIndex = 281
        Me.txtHasu_tabFreeM.TabStop = False
        Me.txtHasu_tabFreeM.TabStopSetting = False
        Me.txtHasu_tabFreeM.TextValue = "0"
        Me.txtHasu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtHasu_tabFreeM.WidthDef = 100
        '
        'LmTitleLabel74
        '
        Me.LmTitleLabel74.AutoSize = True
        Me.LmTitleLabel74.AutoSizeDef = True
        Me.LmTitleLabel74.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel74.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel74.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel74.EnableStatus = False
        Me.LmTitleLabel74.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel74.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel74.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel74.HeightDef = 13
        Me.LmTitleLabel74.Location = New System.Drawing.Point(315, 54)
        Me.LmTitleLabel74.Name = "LmTitleLabel74"
        Me.LmTitleLabel74.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel74.TabIndex = 280
        Me.LmTitleLabel74.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel74.TextValue = ""
        Me.LmTitleLabel74.WidthDef = 35
        '
        'LmTitleLabel78
        '
        Me.LmTitleLabel78.AutoSize = True
        Me.LmTitleLabel78.AutoSizeDef = True
        Me.LmTitleLabel78.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel78.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel78.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel78.EnableStatus = False
        Me.LmTitleLabel78.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel78.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel78.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel78.HeightDef = 13
        Me.LmTitleLabel78.Location = New System.Drawing.Point(218, 54)
        Me.LmTitleLabel78.Name = "LmTitleLabel78"
        Me.LmTitleLabel78.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel78.TabIndex = 279
        Me.LmTitleLabel78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel78.TextValue = ""
        Me.LmTitleLabel78.WidthDef = 77
        '
        'txtKosu_tabFreeM
        '
        Me.txtKosu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKosu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtKosu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKosu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKosu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKosu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKosu_tabFreeM.HeightDef = 18
        Me.txtKosu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.HissuLabelVisible = False
        Me.txtKosu_tabFreeM.IsHissuCheck = False
        Me.txtKosu_tabFreeM.IsRangeCheck = False
        Me.txtKosu_tabFreeM.ItemName = ""
        Me.txtKosu_tabFreeM.Location = New System.Drawing.Point(132, 51)
        Me.txtKosu_tabFreeM.Name = "txtKosu_tabFreeM"
        Me.txtKosu_tabFreeM.ReadOnly = True
        Me.txtKosu_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.txtKosu_tabFreeM.TabIndex = 278
        Me.txtKosu_tabFreeM.TabStop = False
        Me.txtKosu_tabFreeM.TabStopSetting = False
        Me.txtKosu_tabFreeM.TextValue = "0"
        Me.txtKosu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtKosu_tabFreeM.WidthDef = 97
        '
        'lblTitleKonsu_tabFreeM
        '
        Me.lblTitleKonsu_tabFreeM.AutoSize = True
        Me.lblTitleKonsu_tabFreeM.AutoSizeDef = True
        Me.lblTitleKonsu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKonsu_tabFreeM.EnableStatus = False
        Me.lblTitleKonsu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.HeightDef = 13
        Me.lblTitleKonsu_tabFreeM.Location = New System.Drawing.Point(91, 54)
        Me.lblTitleKonsu_tabFreeM.Name = "lblTitleKonsu_tabFreeM"
        Me.lblTitleKonsu_tabFreeM.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKonsu_tabFreeM.TabIndex = 277
        Me.lblTitleKonsu_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKonsu_tabFreeM.TextValue = ""
        Me.lblTitleKonsu_tabFreeM.WidthDef = 35
        '
        'LmImTextBox18
        '
        Me.LmImTextBox18.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox18.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox18.CountWrappedLine = False
        Me.LmImTextBox18.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox18.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox18.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox18.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox18.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox18.HeightDef = 18
        Me.LmImTextBox18.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.HissuLabelVisible = False
        Me.LmImTextBox18.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox18.IsByteCheck = 0
        Me.LmImTextBox18.IsCalendarCheck = False
        Me.LmImTextBox18.IsDakutenCheck = False
        Me.LmImTextBox18.IsEisuCheck = False
        Me.LmImTextBox18.IsForbiddenWordsCheck = False
        Me.LmImTextBox18.IsFullByteCheck = 0
        Me.LmImTextBox18.IsHankakuCheck = False
        Me.LmImTextBox18.IsHissuCheck = False
        Me.LmImTextBox18.IsKanaCheck = False
        Me.LmImTextBox18.IsMiddleSpace = False
        Me.LmImTextBox18.IsNumericCheck = False
        Me.LmImTextBox18.IsSujiCheck = False
        Me.LmImTextBox18.IsZenkakuCheck = False
        Me.LmImTextBox18.ItemName = ""
        Me.LmImTextBox18.LineSpace = 0
        Me.LmImTextBox18.Location = New System.Drawing.Point(545, 9)
        Me.LmImTextBox18.MaxLength = 0
        Me.LmImTextBox18.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox18.MaxLineCount = 0
        Me.LmImTextBox18.Multiline = False
        Me.LmImTextBox18.Name = "LmImTextBox18"
        Me.LmImTextBox18.ReadOnly = True
        Me.LmImTextBox18.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox18.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox18.Size = New System.Drawing.Size(443, 18)
        Me.LmImTextBox18.TabIndex = 276
        Me.LmImTextBox18.TabStop = False
        Me.LmImTextBox18.TabStopSetting = False
        Me.LmImTextBox18.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.LmImTextBox18.UseSystemPasswordChar = False
        Me.LmImTextBox18.WidthDef = 443
        Me.LmImTextBox18.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmImTextBox19
        '
        Me.LmImTextBox19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox19.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox19.CountWrappedLine = False
        Me.LmImTextBox19.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox19.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox19.HeightDef = 18
        Me.LmImTextBox19.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.HissuLabelVisible = False
        Me.LmImTextBox19.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox19.IsByteCheck = 0
        Me.LmImTextBox19.IsCalendarCheck = False
        Me.LmImTextBox19.IsDakutenCheck = False
        Me.LmImTextBox19.IsEisuCheck = False
        Me.LmImTextBox19.IsForbiddenWordsCheck = False
        Me.LmImTextBox19.IsFullByteCheck = 0
        Me.LmImTextBox19.IsHankakuCheck = False
        Me.LmImTextBox19.IsHissuCheck = False
        Me.LmImTextBox19.IsKanaCheck = False
        Me.LmImTextBox19.IsMiddleSpace = False
        Me.LmImTextBox19.IsNumericCheck = False
        Me.LmImTextBox19.IsSujiCheck = False
        Me.LmImTextBox19.IsZenkakuCheck = False
        Me.LmImTextBox19.ItemName = ""
        Me.LmImTextBox19.LineSpace = 0
        Me.LmImTextBox19.Location = New System.Drawing.Point(356, 9)
        Me.LmImTextBox19.MaxLength = 0
        Me.LmImTextBox19.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox19.MaxLineCount = 0
        Me.LmImTextBox19.Multiline = False
        Me.LmImTextBox19.Name = "LmImTextBox19"
        Me.LmImTextBox19.ReadOnly = True
        Me.LmImTextBox19.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox19.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox19.Size = New System.Drawing.Size(208, 18)
        Me.LmImTextBox19.TabIndex = 275
        Me.LmImTextBox19.TabStop = False
        Me.LmImTextBox19.TabStopSetting = False
        Me.LmImTextBox19.TextValue = ""
        Me.LmImTextBox19.UseSystemPasswordChar = False
        Me.LmImTextBox19.WidthDef = 208
        Me.LmImTextBox19.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel80
        '
        Me.LmTitleLabel80.AutoSize = True
        Me.LmTitleLabel80.AutoSizeDef = True
        Me.LmTitleLabel80.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel80.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel80.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel80.EnableStatus = False
        Me.LmTitleLabel80.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel80.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel80.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel80.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel80.HeightDef = 13
        Me.LmTitleLabel80.Location = New System.Drawing.Point(287, 33)
        Me.LmTitleLabel80.Name = "LmTitleLabel80"
        Me.LmTitleLabel80.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel80.TabIndex = 274
        Me.LmTitleLabel80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel80.TextValue = ""
        Me.LmTitleLabel80.WidthDef = 63
        '
        'txtKanriNO_M_tabFreeM
        '
        Me.txtKanriNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKanriNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKanriNO_M_tabFreeM.CountWrappedLine = False
        Me.txtKanriNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKanriNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKanriNO_M_tabFreeM.HeightDef = 18
        Me.txtKanriNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtKanriNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKanriNO_M_tabFreeM.IsByteCheck = 30
        Me.txtKanriNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtKanriNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtKanriNO_M_tabFreeM.IsEisuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtKanriNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtKanriNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsHissuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsKanaCheck = False
        Me.txtKanriNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtKanriNO_M_tabFreeM.IsNumericCheck = False
        Me.txtKanriNO_M_tabFreeM.IsSujiCheck = False
        Me.txtKanriNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtKanriNO_M_tabFreeM.ItemName = ""
        Me.txtKanriNO_M_tabFreeM.LineSpace = 0
        Me.txtKanriNO_M_tabFreeM.Location = New System.Drawing.Point(132, 9)
        Me.txtKanriNO_M_tabFreeM.MaxLength = 30
        Me.txtKanriNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKanriNO_M_tabFreeM.MaxLineCount = 0
        Me.txtKanriNO_M_tabFreeM.Multiline = False
        Me.txtKanriNO_M_tabFreeM.Name = "txtKanriNO_M_tabFreeM"
        Me.txtKanriNO_M_tabFreeM.ReadOnly = True
        Me.txtKanriNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKanriNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKanriNO_M_tabFreeM.Size = New System.Drawing.Size(45, 18)
        Me.txtKanriNO_M_tabFreeM.TabIndex = 273
        Me.txtKanriNO_M_tabFreeM.TabStop = False
        Me.txtKanriNO_M_tabFreeM.TabStopSetting = False
        Me.txtKanriNO_M_tabFreeM.TextValue = ""
        Me.txtKanriNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtKanriNO_M_tabFreeM.WidthDef = 45
        Me.txtKanriNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel81
        '
        Me.LmTitleLabel81.AutoSize = True
        Me.LmTitleLabel81.AutoSizeDef = True
        Me.LmTitleLabel81.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel81.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel81.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel81.EnableStatus = False
        Me.LmTitleLabel81.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel81.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel81.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel81.HeightDef = 13
        Me.LmTitleLabel81.Location = New System.Drawing.Point(12, 12)
        Me.LmTitleLabel81.Name = "LmTitleLabel81"
        Me.LmTitleLabel81.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel81.TabIndex = 272
        Me.LmTitleLabel81.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel81.TextValue = ""
        Me.LmTitleLabel81.WidthDef = 119
        '
        'txtGoodsComment_tabFreeM
        '
        Me.txtGoodsComment_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsComment_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsComment_tabFreeM.CountWrappedLine = False
        Me.txtGoodsComment_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsComment_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsComment_tabFreeM.HeightDef = 18
        Me.txtGoodsComment_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.HissuLabelVisible = False
        Me.txtGoodsComment_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsComment_tabFreeM.IsByteCheck = 50
        Me.txtGoodsComment_tabFreeM.IsCalendarCheck = False
        Me.txtGoodsComment_tabFreeM.IsDakutenCheck = False
        Me.txtGoodsComment_tabFreeM.IsEisuCheck = False
        Me.txtGoodsComment_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtGoodsComment_tabFreeM.IsFullByteCheck = 0
        Me.txtGoodsComment_tabFreeM.IsHankakuCheck = False
        Me.txtGoodsComment_tabFreeM.IsHissuCheck = False
        Me.txtGoodsComment_tabFreeM.IsKanaCheck = False
        Me.txtGoodsComment_tabFreeM.IsMiddleSpace = False
        Me.txtGoodsComment_tabFreeM.IsNumericCheck = False
        Me.txtGoodsComment_tabFreeM.IsSujiCheck = False
        Me.txtGoodsComment_tabFreeM.IsZenkakuCheck = False
        Me.txtGoodsComment_tabFreeM.ItemName = ""
        Me.txtGoodsComment_tabFreeM.LineSpace = 0
        Me.txtGoodsComment_tabFreeM.Location = New System.Drawing.Point(132, 114)
        Me.txtGoodsComment_tabFreeM.MaxLength = 50
        Me.txtGoodsComment_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsComment_tabFreeM.MaxLineCount = 0
        Me.txtGoodsComment_tabFreeM.Multiline = False
        Me.txtGoodsComment_tabFreeM.Name = "txtGoodsComment_tabFreeM"
        Me.txtGoodsComment_tabFreeM.ReadOnly = True
        Me.txtGoodsComment_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsComment_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsComment_tabFreeM.Size = New System.Drawing.Size(893, 18)
        Me.txtGoodsComment_tabFreeM.TabIndex = 256
        Me.txtGoodsComment_tabFreeM.TabStop = False
        Me.txtGoodsComment_tabFreeM.TabStopSetting = False
        Me.txtGoodsComment_tabFreeM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsComment_tabFreeM.UseSystemPasswordChar = False
        Me.txtGoodsComment_tabFreeM.WidthDef = 893
        Me.txtGoodsComment_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel89
        '
        Me.LmTitleLabel89.AutoSize = True
        Me.LmTitleLabel89.AutoSizeDef = True
        Me.LmTitleLabel89.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel89.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel89.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel89.EnableStatus = False
        Me.LmTitleLabel89.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel89.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel89.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel89.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel89.HeightDef = 13
        Me.LmTitleLabel89.Location = New System.Drawing.Point(21, 117)
        Me.LmTitleLabel89.Name = "LmTitleLabel89"
        Me.LmTitleLabel89.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel89.TabIndex = 255
        Me.LmTitleLabel89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel89.TextValue = ""
        Me.LmTitleLabel89.WidthDef = 105
        '
        'lblIrisuTani1_tabFreeM
        '
        Me.lblIrisuTani1_tabFreeM.AutoSize = True
        Me.lblIrisuTani1_tabFreeM.AutoSizeDef = True
        Me.lblIrisuTani1_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrisuTani1_tabFreeM.EnableStatus = False
        Me.lblIrisuTani1_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani1_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani1_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.HeightDef = 13
        Me.lblIrisuTani1_tabFreeM.Location = New System.Drawing.Point(654, 54)
        Me.lblIrisuTani1_tabFreeM.Name = "lblIrisuTani1_tabFreeM"
        Me.lblIrisuTani1_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblIrisuTani1_tabFreeM.TabIndex = 266
        Me.lblIrisuTani1_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisuTani1_tabFreeM.TextValue = ""
        Me.lblIrisuTani1_tabFreeM.WidthDef = 21
        '
        'lblStdIrimeTani_tabFreeM
        '
        Me.lblStdIrimeTani_tabFreeM.AutoSize = True
        Me.lblStdIrimeTani_tabFreeM.AutoSizeDef = True
        Me.lblStdIrimeTani_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStdIrimeTani_tabFreeM.EnableStatus = False
        Me.lblStdIrimeTani_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.HeightDef = 13
        Me.lblStdIrimeTani_tabFreeM.Location = New System.Drawing.Point(871, 54)
        Me.lblStdIrimeTani_tabFreeM.Name = "lblStdIrimeTani_tabFreeM"
        Me.lblStdIrimeTani_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblStdIrimeTani_tabFreeM.TabIndex = 267
        Me.lblStdIrimeTani_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStdIrimeTani_tabFreeM.TextValue = ""
        Me.lblStdIrimeTani_tabFreeM.WidthDef = 21
        '
        'txtBuyerOrdNO_M_tabFreeM
        '
        Me.txtBuyerOrdNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerOrdNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBuyerOrdNO_M_tabFreeM.CountWrappedLine = False
        Me.txtBuyerOrdNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtBuyerOrdNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtBuyerOrdNO_M_tabFreeM.HeightDef = 18
        Me.txtBuyerOrdNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtBuyerOrdNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtBuyerOrdNO_M_tabFreeM.IsByteCheck = 30
        Me.txtBuyerOrdNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsEisuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtBuyerOrdNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsHissuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsKanaCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsNumericCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsSujiCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.ItemName = ""
        Me.txtBuyerOrdNO_M_tabFreeM.LineSpace = 0
        Me.txtBuyerOrdNO_M_tabFreeM.Location = New System.Drawing.Point(570, 93)
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLength = 30
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLineCount = 0
        Me.txtBuyerOrdNO_M_tabFreeM.Multiline = False
        Me.txtBuyerOrdNO_M_tabFreeM.Name = "txtBuyerOrdNO_M_tabFreeM"
        Me.txtBuyerOrdNO_M_tabFreeM.ReadOnly = True
        Me.txtBuyerOrdNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBuyerOrdNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBuyerOrdNO_M_tabFreeM.Size = New System.Drawing.Size(282, 18)
        Me.txtBuyerOrdNO_M_tabFreeM.TabIndex = 260
        Me.txtBuyerOrdNO_M_tabFreeM.TabStop = False
        Me.txtBuyerOrdNO_M_tabFreeM.TabStopSetting = False
        Me.txtBuyerOrdNO_M_tabFreeM.TextValue = ""
        Me.txtBuyerOrdNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtBuyerOrdNO_M_tabFreeM.WidthDef = 282
        Me.txtBuyerOrdNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel92
        '
        Me.LmTitleLabel92.AutoSize = True
        Me.LmTitleLabel92.AutoSizeDef = True
        Me.LmTitleLabel92.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel92.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel92.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel92.EnableStatus = False
        Me.LmTitleLabel92.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel92.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel92.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel92.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel92.HeightDef = 13
        Me.LmTitleLabel92.Location = New System.Drawing.Point(501, 96)
        Me.LmTitleLabel92.Name = "LmTitleLabel92"
        Me.LmTitleLabel92.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel92.TabIndex = 259
        Me.LmTitleLabel92.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel92.TextValue = ""
        Me.LmTitleLabel92.WidthDef = 63
        '
        'lblIrisuTani2_tabFreeM
        '
        Me.lblIrisuTani2_tabFreeM.AutoSize = True
        Me.lblIrisuTani2_tabFreeM.AutoSizeDef = True
        Me.lblIrisuTani2_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrisuTani2_tabFreeM.EnableStatus = False
        Me.lblIrisuTani2_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani2_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani2_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.HeightDef = 13
        Me.lblIrisuTani2_tabFreeM.Location = New System.Drawing.Point(686, 54)
        Me.lblIrisuTani2_tabFreeM.Name = "lblIrisuTani2_tabFreeM"
        Me.lblIrisuTani2_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblIrisuTani2_tabFreeM.TabIndex = 263
        Me.lblIrisuTani2_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisuTani2_tabFreeM.TextValue = ""
        Me.lblIrisuTani2_tabFreeM.WidthDef = 21
        '
        'numStdIrime_tabFreeM
        '
        Me.numStdIrime_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStdIrime_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStdIrime_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime_tabFreeM.HeightDef = 18
        Me.numStdIrime_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.HissuLabelVisible = False
        Me.numStdIrime_tabFreeM.IsHissuCheck = False
        Me.numStdIrime_tabFreeM.IsRangeCheck = False
        Me.numStdIrime_tabFreeM.ItemName = ""
        Me.numStdIrime_tabFreeM.Location = New System.Drawing.Point(783, 51)
        Me.numStdIrime_tabFreeM.Name = "numStdIrime_tabFreeM"
        Me.numStdIrime_tabFreeM.ReadOnly = True
        Me.numStdIrime_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.numStdIrime_tabFreeM.TabIndex = 269
        Me.numStdIrime_tabFreeM.TabStop = False
        Me.numStdIrime_tabFreeM.TabStopSetting = False
        Me.numStdIrime_tabFreeM.TextValue = "0"
        Me.numStdIrime_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStdIrime_tabFreeM.WidthDef = 100
        '
        'txtOrderNO_M_tabFreeM
        '
        Me.txtOrderNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNO_M_tabFreeM.CountWrappedLine = False
        Me.txtOrderNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNO_M_tabFreeM.HeightDef = 18
        Me.txtOrderNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtOrderNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOrderNO_M_tabFreeM.IsByteCheck = 30
        Me.txtOrderNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtOrderNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtOrderNO_M_tabFreeM.IsEisuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtOrderNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtOrderNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsHissuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsKanaCheck = False
        Me.txtOrderNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtOrderNO_M_tabFreeM.IsNumericCheck = False
        Me.txtOrderNO_M_tabFreeM.IsSujiCheck = False
        Me.txtOrderNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtOrderNO_M_tabFreeM.ItemName = ""
        Me.txtOrderNO_M_tabFreeM.LineSpace = 0
        Me.txtOrderNO_M_tabFreeM.Location = New System.Drawing.Point(132, 93)
        Me.txtOrderNO_M_tabFreeM.MaxLength = 30
        Me.txtOrderNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNO_M_tabFreeM.MaxLineCount = 0
        Me.txtOrderNO_M_tabFreeM.Multiline = False
        Me.txtOrderNO_M_tabFreeM.Name = "txtOrderNO_M_tabFreeM"
        Me.txtOrderNO_M_tabFreeM.ReadOnly = True
        Me.txtOrderNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNO_M_tabFreeM.Size = New System.Drawing.Size(348, 18)
        Me.txtOrderNO_M_tabFreeM.TabIndex = 254
        Me.txtOrderNO_M_tabFreeM.TabStop = False
        Me.txtOrderNO_M_tabFreeM.TabStopSetting = False
        Me.txtOrderNO_M_tabFreeM.TextValue = ""
        Me.txtOrderNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtOrderNO_M_tabFreeM.WidthDef = 348
        Me.txtOrderNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numIrisu_tabFreeM
        '
        Me.numIrisu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrisu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrisu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu_tabFreeM.HeightDef = 18
        Me.numIrisu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.HissuLabelVisible = False
        Me.numIrisu_tabFreeM.IsHissuCheck = False
        Me.numIrisu_tabFreeM.IsRangeCheck = False
        Me.numIrisu_tabFreeM.ItemName = ""
        Me.numIrisu_tabFreeM.Location = New System.Drawing.Point(570, 51)
        Me.numIrisu_tabFreeM.Name = "numIrisu_tabFreeM"
        Me.numIrisu_tabFreeM.ReadOnly = True
        Me.numIrisu_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numIrisu_tabFreeM.TabIndex = 268
        Me.numIrisu_tabFreeM.TabStop = False
        Me.numIrisu_tabFreeM.TabStopSetting = False
        Me.numIrisu_tabFreeM.TextValue = "0"
        Me.numIrisu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrisu_tabFreeM.WidthDef = 97
        '
        'LmTitleLabel94
        '
        Me.LmTitleLabel94.AutoSize = True
        Me.LmTitleLabel94.AutoSizeDef = True
        Me.LmTitleLabel94.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel94.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel94.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel94.EnableStatus = False
        Me.LmTitleLabel94.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel94.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel94.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel94.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel94.HeightDef = 13
        Me.LmTitleLabel94.Location = New System.Drawing.Point(35, 96)
        Me.LmTitleLabel94.Name = "LmTitleLabel94"
        Me.LmTitleLabel94.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel94.TabIndex = 253
        Me.LmTitleLabel94.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel94.TextValue = ""
        Me.LmTitleLabel94.WidthDef = 91
        '
        'LmTitleLabel95
        '
        Me.LmTitleLabel95.AutoSize = True
        Me.LmTitleLabel95.AutoSizeDef = True
        Me.LmTitleLabel95.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel95.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel95.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel95.EnableStatus = False
        Me.LmTitleLabel95.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel95.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel95.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel95.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel95.HeightDef = 13
        Me.LmTitleLabel95.Location = New System.Drawing.Point(529, 54)
        Me.LmTitleLabel95.Name = "LmTitleLabel95"
        Me.LmTitleLabel95.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel95.TabIndex = 252
        Me.LmTitleLabel95.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel95.TextValue = ""
        Me.LmTitleLabel95.WidthDef = 35
        '
        'LmTitleLabel96
        '
        Me.LmTitleLabel96.AutoSize = True
        Me.LmTitleLabel96.AutoSizeDef = True
        Me.LmTitleLabel96.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel96.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel96.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel96.EnableStatus = False
        Me.LmTitleLabel96.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel96.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel96.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel96.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel96.HeightDef = 13
        Me.LmTitleLabel96.Location = New System.Drawing.Point(444, 75)
        Me.LmTitleLabel96.Name = "LmTitleLabel96"
        Me.LmTitleLabel96.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel96.TabIndex = 265
        Me.LmTitleLabel96.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel96.TextValue = ""
        Me.LmTitleLabel96.WidthDef = 21
        '
        'LmTitleLabel97
        '
        Me.LmTitleLabel97.AutoSize = True
        Me.LmTitleLabel97.AutoSizeDef = True
        Me.LmTitleLabel97.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel97.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel97.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel97.EnableStatus = False
        Me.LmTitleLabel97.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel97.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel97.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel97.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel97.HeightDef = 13
        Me.LmTitleLabel97.Location = New System.Drawing.Point(714, 54)
        Me.LmTitleLabel97.Name = "LmTitleLabel97"
        Me.LmTitleLabel97.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel97.TabIndex = 257
        Me.LmTitleLabel97.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel97.TextValue = ""
        Me.LmTitleLabel97.WidthDef = 63
        '
        'LmTitleLabel98
        '
        Me.LmTitleLabel98.AutoSize = True
        Me.LmTitleLabel98.AutoSizeDef = True
        Me.LmTitleLabel98.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel98.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel98.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel98.EnableStatus = False
        Me.LmTitleLabel98.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel98.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel98.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel98.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel98.HeightDef = 13
        Me.LmTitleLabel98.Location = New System.Drawing.Point(218, 75)
        Me.LmTitleLabel98.Name = "LmTitleLabel98"
        Me.LmTitleLabel98.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel98.TabIndex = 264
        Me.LmTitleLabel98.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel98.TextValue = ""
        Me.LmTitleLabel98.WidthDef = 21
        '
        'LmTitleLabel99
        '
        Me.LmTitleLabel99.AutoSize = True
        Me.LmTitleLabel99.AutoSizeDef = True
        Me.LmTitleLabel99.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel99.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel99.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel99.EnableStatus = False
        Me.LmTitleLabel99.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel99.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel99.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel99.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel99.HeightDef = 13
        Me.LmTitleLabel99.Location = New System.Drawing.Point(91, 75)
        Me.LmTitleLabel99.Name = "LmTitleLabel99"
        Me.LmTitleLabel99.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel99.TabIndex = 258
        Me.LmTitleLabel99.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel99.TextValue = ""
        Me.LmTitleLabel99.WidthDef = 35
        '
        'LmTitleLabel100
        '
        Me.LmTitleLabel100.AutoSize = True
        Me.LmTitleLabel100.AutoSizeDef = True
        Me.LmTitleLabel100.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel100.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel100.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel100.EnableStatus = False
        Me.LmTitleLabel100.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel100.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel100.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel100.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel100.HeightDef = 13
        Me.LmTitleLabel100.Location = New System.Drawing.Point(315, 75)
        Me.LmTitleLabel100.Name = "LmTitleLabel100"
        Me.LmTitleLabel100.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel100.TabIndex = 261
        Me.LmTitleLabel100.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel100.TextValue = ""
        Me.LmTitleLabel100.WidthDef = 35
        '
        'LmTitleLabel101
        '
        Me.LmTitleLabel101.AutoSize = True
        Me.LmTitleLabel101.AutoSizeDef = True
        Me.LmTitleLabel101.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel101.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel101.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel101.EnableStatus = False
        Me.LmTitleLabel101.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel101.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel101.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel101.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel101.HeightDef = 13
        Me.LmTitleLabel101.Location = New System.Drawing.Point(673, 54)
        Me.LmTitleLabel101.Name = "LmTitleLabel101"
        Me.LmTitleLabel101.Size = New System.Drawing.Size(14, 13)
        Me.LmTitleLabel101.TabIndex = 262
        Me.LmTitleLabel101.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel101.TextValue = ""
        Me.LmTitleLabel101.WidthDef = 14
        '
        'numSumAnt_tabFreeM
        '
        Me.numSumAnt_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumAnt_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumAnt_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt_tabFreeM.HeightDef = 18
        Me.numSumAnt_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.HissuLabelVisible = False
        Me.numSumAnt_tabFreeM.IsHissuCheck = False
        Me.numSumAnt_tabFreeM.IsRangeCheck = False
        Me.numSumAnt_tabFreeM.ItemName = ""
        Me.numSumAnt_tabFreeM.Location = New System.Drawing.Point(132, 72)
        Me.numSumAnt_tabFreeM.Name = "numSumAnt_tabFreeM"
        Me.numSumAnt_tabFreeM.ReadOnly = True
        Me.numSumAnt_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numSumAnt_tabFreeM.TabIndex = 270
        Me.numSumAnt_tabFreeM.TabStop = False
        Me.numSumAnt_tabFreeM.TabStopSetting = False
        Me.numSumAnt_tabFreeM.TextValue = "0"
        Me.numSumAnt_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumAnt_tabFreeM.WidthDef = 97
        '
        'numSumCnt_tabFreeM
        '
        Me.numSumCnt_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumCnt_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumCnt_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt_tabFreeM.HeightDef = 18
        Me.numSumCnt_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.HissuLabelVisible = False
        Me.numSumCnt_tabFreeM.IsHissuCheck = False
        Me.numSumCnt_tabFreeM.IsRangeCheck = False
        Me.numSumCnt_tabFreeM.ItemName = ""
        Me.numSumCnt_tabFreeM.Location = New System.Drawing.Point(356, 72)
        Me.numSumCnt_tabFreeM.Name = "numSumCnt_tabFreeM"
        Me.numSumCnt_tabFreeM.ReadOnly = True
        Me.numSumCnt_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numSumCnt_tabFreeM.TabIndex = 271
        Me.numSumCnt_tabFreeM.TabStop = False
        Me.numSumCnt_tabFreeM.TabStopSetting = False
        Me.numSumCnt_tabFreeM.TextValue = "0"
        Me.numSumCnt_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumCnt_tabFreeM.WidthDef = 97
        '
        'numNyukaCnt
        '
        Me.numNyukaCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNyukaCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNyukaCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNyukaCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNyukaCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNyukaCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNyukaCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNyukaCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNyukaCnt.HeightDef = 18
        Me.numNyukaCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNyukaCnt.HissuLabelVisible = False
        Me.numNyukaCnt.IsHissuCheck = False
        Me.numNyukaCnt.IsRangeCheck = False
        Me.numNyukaCnt.ItemName = ""
        Me.numNyukaCnt.Location = New System.Drawing.Point(522, 156)
        Me.numNyukaCnt.Name = "numNyukaCnt"
        Me.numNyukaCnt.ReadOnly = False
        Me.numNyukaCnt.Size = New System.Drawing.Size(130, 18)
        Me.numNyukaCnt.TabIndex = 114
        Me.numNyukaCnt.TabStopSetting = True
        Me.numNyukaCnt.TextValue = "0"
        Me.numNyukaCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNyukaCnt.WidthDef = 130
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustCdL.IsByteCheck = 0
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
        Me.txtCustCdL.Location = New System.Drawing.Point(124, 114)
        Me.txtCustCdL.MaxLength = 0
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = True
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(70, 18)
        Me.txtCustCdL.TabIndex = 95
        Me.txtCustCdL.TabStop = False
        Me.txtCustCdL.TabStopSetting = False
        Me.txtCustCdL.TextValue = "XXXXX"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 70
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numPlanQt
        '
        Me.numPlanQt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPlanQt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPlanQt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPlanQt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPlanQt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPlanQt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPlanQt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPlanQt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPlanQt.HeightDef = 18
        Me.numPlanQt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPlanQt.HissuLabelVisible = False
        Me.numPlanQt.IsHissuCheck = False
        Me.numPlanQt.IsRangeCheck = False
        Me.numPlanQt.ItemName = ""
        Me.numPlanQt.Location = New System.Drawing.Point(124, 156)
        Me.numPlanQt.Name = "numPlanQt"
        Me.numPlanQt.ReadOnly = False
        Me.numPlanQt.Size = New System.Drawing.Size(130, 18)
        Me.numPlanQt.TabIndex = 113
        Me.numPlanQt.TabStopSetting = True
        Me.numPlanQt.TextValue = "0"
        Me.numPlanQt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPlanQt.WidthDef = 130
        '
        'numFree
        '
        Me.numFree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numFree.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numFree.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numFree.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numFree.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numFree.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numFree.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numFree.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numFree.HeightDef = 18
        Me.numFree.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numFree.HissuLabelVisible = False
        Me.numFree.IsHissuCheck = False
        Me.numFree.IsRangeCheck = False
        Me.numFree.ItemName = ""
        Me.numFree.Location = New System.Drawing.Point(522, 93)
        Me.numFree.Name = "numFree"
        Me.numFree.ReadOnly = False
        Me.numFree.Size = New System.Drawing.Size(60, 18)
        Me.numFree.TabIndex = 116
        Me.numFree.TabStopSetting = True
        Me.numFree.TextValue = "0"
        Me.numFree.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numFree.WidthDef = 60
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1119, 10)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 110
        Me.lblSituation.TabStop = False
        '
        'tabInka
        '
        Me.tabInka.BackColorDef = System.Drawing.SystemColors.Control
        Me.tabInka.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.tabInka.Controls.Add(Me.tabNyuka)
        Me.tabInka.EnableStatus = True
        Me.tabInka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabInka.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabInka.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabInka.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabInka.HeightDef = 230
        Me.tabInka.Location = New System.Drawing.Point(13, 22)
        Me.tabInka.Name = "tabInka"
        Me.tabInka.SelectedIndex = 0
        Me.tabInka.Size = New System.Drawing.Size(1249, 230)
        Me.tabInka.TabIndex = 252
        Me.tabInka.TabStopSetting = True
        Me.tabInka.TextValue = ""
        Me.tabInka.WidthDef = 1249
        '
        'tabNyuka
        '
        Me.tabNyuka.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.tabNyuka.Controls.Add(Me.LmTitleLabel19)
        Me.tabNyuka.Controls.Add(Me.LmTitleLabel24)
        Me.tabNyuka.Controls.Add(Me.cmbNyukaKbn)
        Me.tabNyuka.Controls.Add(Me.txtNyukaComment)
        Me.tabNyuka.Controls.Add(Me.txtNyubanL)
        Me.tabNyuka.Controls.Add(Me.lblEdiKanriNo)
        Me.tabNyuka.Controls.Add(Me.lblTitleStatus)
        Me.tabNyuka.Controls.Add(Me.lblTitleEdiKanriNo)
        Me.tabNyuka.Controls.Add(Me.lblTitlelblKanriNoL)
        Me.tabNyuka.Controls.Add(Me.numFree)
        Me.tabNyuka.Controls.Add(Me.numPlanQt)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyukaComment)
        Me.tabNyuka.Controls.Add(Me.lblTitleBuyerOrdNo)
        Me.tabNyuka.Controls.Add(Me.lblTitleCust)
        Me.tabNyuka.Controls.Add(Me.txtOrderNo)
        Me.tabNyuka.Controls.Add(Me.lblTitleOrderNo)
        Me.tabNyuka.Controls.Add(Me.txtBuyerOrdNo)
        Me.tabNyuka.Controls.Add(Me.lblTitleFree)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyubanL)
        Me.tabNyuka.Controls.Add(Me.lblTitleZenkiHo)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyukaDate)
        Me.tabNyuka.Controls.Add(Me.imdNyukaDate)
        Me.tabNyuka.Controls.Add(Me.numNyukaCnt)
        Me.tabNyuka.Controls.Add(Me.cmbNyukaType)
        Me.tabNyuka.Controls.Add(Me.lblCustNm)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyukaKbn)
        Me.tabNyuka.Controls.Add(Me.txtCustCdM)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyukaType)
        Me.tabNyuka.Controls.Add(Me.txtCustCdL)
        Me.tabNyuka.Controls.Add(Me.lblKanriNoL)
        Me.tabNyuka.Controls.Add(Me.lblTitleNiyakuUmu)
        Me.tabNyuka.Controls.Add(Me.lblTitleToukiHo)
        Me.tabNyuka.Controls.Add(Me.lblTitleShinshokuKbn)
        Me.tabNyuka.Controls.Add(Me.lblTitleNyukaCnt)
        Me.tabNyuka.Controls.Add(Me.lblTitleIrisuTani)
        Me.tabNyuka.Controls.Add(Me.imdHokanDate)
        Me.tabNyuka.Controls.Add(Me.lblTitleHokanDate)
        Me.tabNyuka.Controls.Add(Me.lblTitleWh)
        Me.tabNyuka.Controls.Add(Me.lblTitleTax)
        Me.tabNyuka.Controls.Add(Me.cmbPlanQtUt)
        Me.tabNyuka.Controls.Add(Me.lblTitleEigyo)
        Me.tabNyuka.Controls.Add(Me.cmbTax)
        Me.tabNyuka.Controls.Add(Me.cmbStatus)
        Me.tabNyuka.Controls.Add(Me.cmbEigyo)
        Me.tabNyuka.Controls.Add(Me.cmbWh)
        Me.tabNyuka.Controls.Add(Me.cmbShinshokuKbn)
        Me.tabNyuka.Controls.Add(Me.cmbToukiHo)
        Me.tabNyuka.Controls.Add(Me.cmbZenkiHo)
        Me.tabNyuka.Controls.Add(Me.cmbNiyakuUmu)
        Me.tabNyuka.Location = New System.Drawing.Point(4, 22)
        Me.tabNyuka.Name = "tabNyuka"
        Me.tabNyuka.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNyuka.Size = New System.Drawing.Size(1241, 204)
        Me.tabNyuka.TabIndex = 0
        Me.tabNyuka.Text = "入荷情報"
        '
        'LmTitleLabel24
        '
        Me.LmTitleLabel24.AutoSize = True
        Me.LmTitleLabel24.AutoSizeDef = True
        Me.LmTitleLabel24.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel24.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel24.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel24.EnableStatus = False
        Me.LmTitleLabel24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel24.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel24.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel24.HeightDef = 13
        Me.LmTitleLabel24.Location = New System.Drawing.Point(570, 96)
        Me.LmTitleLabel24.Name = "LmTitleLabel24"
        Me.LmTitleLabel24.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel24.TabIndex = 131
        Me.LmTitleLabel24.Text = "日"
        Me.LmTitleLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel24.TextValue = "日"
        Me.LmTitleLabel24.WidthDef = 21
        '
        'cmbNyukaKbn
        '
        Me.cmbNyukaKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNyukaKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNyukaKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNyukaKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNyukaKbn.DataCode = "N006"
        Me.cmbNyukaKbn.DataSource = Nothing
        Me.cmbNyukaKbn.DisplayMember = Nothing
        Me.cmbNyukaKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNyukaKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNyukaKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNyukaKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNyukaKbn.HeightDef = 18
        Me.cmbNyukaKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNyukaKbn.HissuLabelVisible = False
        Me.cmbNyukaKbn.InsertWildCard = True
        Me.cmbNyukaKbn.IsForbiddenWordsCheck = False
        Me.cmbNyukaKbn.IsHissuCheck = False
        Me.cmbNyukaKbn.ItemName = ""
        Me.cmbNyukaKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNyukaKbn.Location = New System.Drawing.Point(522, 51)
        Me.cmbNyukaKbn.Name = "cmbNyukaKbn"
        Me.cmbNyukaKbn.ReadOnly = False
        Me.cmbNyukaKbn.SelectedIndex = -1
        Me.cmbNyukaKbn.SelectedItem = Nothing
        Me.cmbNyukaKbn.SelectedText = ""
        Me.cmbNyukaKbn.SelectedValue = ""
        Me.cmbNyukaKbn.Size = New System.Drawing.Size(150, 18)
        Me.cmbNyukaKbn.TabIndex = 123
        Me.cmbNyukaKbn.TabStopSetting = True
        Me.cmbNyukaKbn.TextValue = ""
        Me.cmbNyukaKbn.Value1 = Nothing
        Me.cmbNyukaKbn.Value2 = Nothing
        Me.cmbNyukaKbn.Value3 = Nothing
        Me.cmbNyukaKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbNyukaKbn.ValueMember = Nothing
        Me.cmbNyukaKbn.WidthDef = 150
        '
        'lblEdiKanriNo
        '
        Me.lblEdiKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEdiKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEdiKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEdiKanriNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEdiKanriNo.CountWrappedLine = False
        Me.lblEdiKanriNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblEdiKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEdiKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEdiKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEdiKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEdiKanriNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblEdiKanriNo.HeightDef = 18
        Me.lblEdiKanriNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEdiKanriNo.HissuLabelVisible = False
        Me.lblEdiKanriNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblEdiKanriNo.IsByteCheck = 0
        Me.lblEdiKanriNo.IsCalendarCheck = False
        Me.lblEdiKanriNo.IsDakutenCheck = False
        Me.lblEdiKanriNo.IsEisuCheck = False
        Me.lblEdiKanriNo.IsForbiddenWordsCheck = False
        Me.lblEdiKanriNo.IsFullByteCheck = 0
        Me.lblEdiKanriNo.IsHankakuCheck = False
        Me.lblEdiKanriNo.IsHissuCheck = False
        Me.lblEdiKanriNo.IsKanaCheck = False
        Me.lblEdiKanriNo.IsMiddleSpace = False
        Me.lblEdiKanriNo.IsNumericCheck = False
        Me.lblEdiKanriNo.IsSujiCheck = False
        Me.lblEdiKanriNo.IsZenkakuCheck = False
        Me.lblEdiKanriNo.ItemName = ""
        Me.lblEdiKanriNo.LineSpace = 0
        Me.lblEdiKanriNo.Location = New System.Drawing.Point(124, 9)
        Me.lblEdiKanriNo.MaxLength = 0
        Me.lblEdiKanriNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblEdiKanriNo.MaxLineCount = 0
        Me.lblEdiKanriNo.Multiline = False
        Me.lblEdiKanriNo.Name = "lblEdiKanriNo"
        Me.lblEdiKanriNo.ReadOnly = True
        Me.lblEdiKanriNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblEdiKanriNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblEdiKanriNo.Size = New System.Drawing.Size(90, 18)
        Me.lblEdiKanriNo.TabIndex = 120
        Me.lblEdiKanriNo.TabStop = False
        Me.lblEdiKanriNo.TabStopSetting = False
        Me.lblEdiKanriNo.TextValue = "XXXXXXX"
        Me.lblEdiKanriNo.UseSystemPasswordChar = False
        Me.lblEdiKanriNo.WidthDef = 90
        Me.lblEdiKanriNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleStatus
        '
        Me.lblTitleStatus.AutoSize = True
        Me.lblTitleStatus.AutoSizeDef = True
        Me.lblTitleStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleStatus.EnableStatus = False
        Me.lblTitleStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleStatus.HeightDef = 13
        Me.lblTitleStatus.Location = New System.Drawing.Point(445, 12)
        Me.lblTitleStatus.Name = "lblTitleStatus"
        Me.lblTitleStatus.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleStatus.TabIndex = 119
        Me.lblTitleStatus.Text = "ステータス"
        Me.lblTitleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleStatus.TextValue = "ステータス"
        Me.lblTitleStatus.WidthDef = 77
        '
        'lblTitleEdiKanriNo
        '
        Me.lblTitleEdiKanriNo.AutoSize = True
        Me.lblTitleEdiKanriNo.AutoSizeDef = True
        Me.lblTitleEdiKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEdiKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEdiKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEdiKanriNo.EnableStatus = False
        Me.lblTitleEdiKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEdiKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEdiKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEdiKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEdiKanriNo.HeightDef = 13
        Me.lblTitleEdiKanriNo.Location = New System.Drawing.Point(12, 12)
        Me.lblTitleEdiKanriNo.Name = "lblTitleEdiKanriNo"
        Me.lblTitleEdiKanriNo.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleEdiKanriNo.TabIndex = 117
        Me.lblTitleEdiKanriNo.Text = "EDI管理番号(大)"
        Me.lblTitleEdiKanriNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEdiKanriNo.TextValue = "EDI管理番号(大)"
        Me.lblTitleEdiKanriNo.WidthDef = 112
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbStatus.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbStatus.DataCode = "E008"
        Me.cmbStatus.DataSource = Nothing
        Me.cmbStatus.DisplayMember = Nothing
        Me.cmbStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStatus.HeightDef = 18
        Me.cmbStatus.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbStatus.HissuLabelVisible = False
        Me.cmbStatus.InsertWildCard = True
        Me.cmbStatus.IsForbiddenWordsCheck = False
        Me.cmbStatus.IsHissuCheck = False
        Me.cmbStatus.ItemName = ""
        Me.cmbStatus.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbStatus.Location = New System.Drawing.Point(522, 9)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.ReadOnly = False
        Me.cmbStatus.SelectedIndex = -1
        Me.cmbStatus.SelectedItem = Nothing
        Me.cmbStatus.SelectedText = ""
        Me.cmbStatus.SelectedValue = ""
        Me.cmbStatus.Size = New System.Drawing.Size(150, 18)
        Me.cmbStatus.TabIndex = 124
        Me.cmbStatus.TabStopSetting = True
        Me.cmbStatus.TextValue = ""
        Me.cmbStatus.Value1 = Nothing
        Me.cmbStatus.Value2 = Nothing
        Me.cmbStatus.Value3 = Nothing
        Me.cmbStatus.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbStatus.ValueMember = Nothing
        Me.cmbStatus.WidthDef = 150
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
        Me.cmbEigyo.HissuLabelVisible = False
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = False
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(522, 30)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = False
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 125
        Me.cmbEigyo.TabStopSetting = True
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'cmbWh
        '
        Me.cmbWh.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWh.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbWh.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbWh.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbWh.DataSource = Nothing
        Me.cmbWh.DisplayMember = Nothing
        Me.cmbWh.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWh.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWh.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWh.HeightDef = 18
        Me.cmbWh.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWh.HissuLabelVisible = False
        Me.cmbWh.InsertWildCard = True
        Me.cmbWh.IsForbiddenWordsCheck = False
        Me.cmbWh.IsHissuCheck = False
        Me.cmbWh.ItemName = ""
        Me.cmbWh.Location = New System.Drawing.Point(877, 30)
        Me.cmbWh.Name = "cmbWh"
        Me.cmbWh.ReadOnly = False
        Me.cmbWh.SelectedIndex = -1
        Me.cmbWh.SelectedItem = Nothing
        Me.cmbWh.SelectedText = ""
        Me.cmbWh.SelectedValue = ""
        Me.cmbWh.Size = New System.Drawing.Size(300, 18)
        Me.cmbWh.TabIndex = 126
        Me.cmbWh.TabStopSetting = True
        Me.cmbWh.TextValue = ""
        Me.cmbWh.ValueMember = Nothing
        Me.cmbWh.WidthDef = 300
        '
        'cmbShinshokuKbn
        '
        Me.cmbShinshokuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShinshokuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShinshokuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShinshokuKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShinshokuKbn.DataCode = "N004"
        Me.cmbShinshokuKbn.DataSource = Nothing
        Me.cmbShinshokuKbn.DisplayMember = Nothing
        Me.cmbShinshokuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShinshokuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShinshokuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShinshokuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShinshokuKbn.HeightDef = 18
        Me.cmbShinshokuKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShinshokuKbn.HissuLabelVisible = False
        Me.cmbShinshokuKbn.InsertWildCard = True
        Me.cmbShinshokuKbn.IsForbiddenWordsCheck = False
        Me.cmbShinshokuKbn.IsHissuCheck = False
        Me.cmbShinshokuKbn.ItemName = ""
        Me.cmbShinshokuKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShinshokuKbn.Location = New System.Drawing.Point(877, 51)
        Me.cmbShinshokuKbn.Name = "cmbShinshokuKbn"
        Me.cmbShinshokuKbn.ReadOnly = False
        Me.cmbShinshokuKbn.SelectedIndex = -1
        Me.cmbShinshokuKbn.SelectedItem = Nothing
        Me.cmbShinshokuKbn.SelectedText = ""
        Me.cmbShinshokuKbn.SelectedValue = ""
        Me.cmbShinshokuKbn.Size = New System.Drawing.Size(137, 18)
        Me.cmbShinshokuKbn.TabIndex = 127
        Me.cmbShinshokuKbn.TabStopSetting = True
        Me.cmbShinshokuKbn.TextValue = ""
        Me.cmbShinshokuKbn.Value1 = Nothing
        Me.cmbShinshokuKbn.Value2 = Nothing
        Me.cmbShinshokuKbn.Value3 = Nothing
        Me.cmbShinshokuKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbShinshokuKbn.ValueMember = Nothing
        Me.cmbShinshokuKbn.WidthDef = 137
        '
        'cmbToukiHo
        '
        Me.cmbToukiHo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbToukiHo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbToukiHo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbToukiHo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbToukiHo.DataSource = Nothing
        Me.cmbToukiHo.DisplayMember = Nothing
        Me.cmbToukiHo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbToukiHo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbToukiHo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbToukiHo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbToukiHo.HeightDef = 18
        Me.cmbToukiHo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbToukiHo.HissuLabelVisible = False
        Me.cmbToukiHo.InsertWildCard = True
        Me.cmbToukiHo.IsForbiddenWordsCheck = False
        Me.cmbToukiHo.IsHissuCheck = False
        Me.cmbToukiHo.ItemName = ""
        Me.cmbToukiHo.Location = New System.Drawing.Point(124, 135)
        Me.cmbToukiHo.Name = "cmbToukiHo"
        Me.cmbToukiHo.ReadOnly = False
        Me.cmbToukiHo.SelectedIndex = -1
        Me.cmbToukiHo.SelectedItem = Nothing
        Me.cmbToukiHo.SelectedText = ""
        Me.cmbToukiHo.SelectedValue = ""
        Me.cmbToukiHo.Size = New System.Drawing.Size(93, 18)
        Me.cmbToukiHo.TabIndex = 128
        Me.cmbToukiHo.TabStopSetting = True
        Me.cmbToukiHo.TextValue = ""
        Me.cmbToukiHo.ValueMember = Nothing
        Me.cmbToukiHo.WidthDef = 93
        '
        'cmbZenkiHo
        '
        Me.cmbZenkiHo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZenkiHo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZenkiHo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbZenkiHo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbZenkiHo.DataSource = Nothing
        Me.cmbZenkiHo.DisplayMember = Nothing
        Me.cmbZenkiHo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZenkiHo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZenkiHo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZenkiHo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZenkiHo.HeightDef = 18
        Me.cmbZenkiHo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbZenkiHo.HissuLabelVisible = False
        Me.cmbZenkiHo.InsertWildCard = True
        Me.cmbZenkiHo.IsForbiddenWordsCheck = False
        Me.cmbZenkiHo.IsHissuCheck = False
        Me.cmbZenkiHo.ItemName = ""
        Me.cmbZenkiHo.Location = New System.Drawing.Point(522, 135)
        Me.cmbZenkiHo.Name = "cmbZenkiHo"
        Me.cmbZenkiHo.ReadOnly = False
        Me.cmbZenkiHo.SelectedIndex = -1
        Me.cmbZenkiHo.SelectedItem = Nothing
        Me.cmbZenkiHo.SelectedText = ""
        Me.cmbZenkiHo.SelectedValue = ""
        Me.cmbZenkiHo.Size = New System.Drawing.Size(93, 18)
        Me.cmbZenkiHo.TabIndex = 129
        Me.cmbZenkiHo.TabStopSetting = True
        Me.cmbZenkiHo.TextValue = ""
        Me.cmbZenkiHo.ValueMember = Nothing
        Me.cmbZenkiHo.WidthDef = 93
        '
        'cmbNiyakuUmu
        '
        Me.cmbNiyakuUmu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyakuUmu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyakuUmu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNiyakuUmu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNiyakuUmu.DataSource = Nothing
        Me.cmbNiyakuUmu.DisplayMember = Nothing
        Me.cmbNiyakuUmu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyakuUmu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyakuUmu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyakuUmu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyakuUmu.HeightDef = 18
        Me.cmbNiyakuUmu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNiyakuUmu.HissuLabelVisible = False
        Me.cmbNiyakuUmu.InsertWildCard = True
        Me.cmbNiyakuUmu.IsForbiddenWordsCheck = False
        Me.cmbNiyakuUmu.IsHissuCheck = False
        Me.cmbNiyakuUmu.ItemName = ""
        Me.cmbNiyakuUmu.Location = New System.Drawing.Point(877, 135)
        Me.cmbNiyakuUmu.Name = "cmbNiyakuUmu"
        Me.cmbNiyakuUmu.ReadOnly = False
        Me.cmbNiyakuUmu.SelectedIndex = -1
        Me.cmbNiyakuUmu.SelectedItem = Nothing
        Me.cmbNiyakuUmu.SelectedText = ""
        Me.cmbNiyakuUmu.SelectedValue = ""
        Me.cmbNiyakuUmu.Size = New System.Drawing.Size(93, 18)
        Me.cmbNiyakuUmu.TabIndex = 130
        Me.cmbNiyakuUmu.TabStopSetting = True
        Me.cmbNiyakuUmu.TextValue = ""
        Me.cmbNiyakuUmu.ValueMember = Nothing
        Me.cmbNiyakuUmu.WidthDef = 93
        '
        'tabFree
        '
        Me.tabFree.BackColorDef = System.Drawing.SystemColors.Control
        Me.tabFree.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.tabFree.Controls.Add(Me.tabFreeM)
        Me.tabFree.EnableStatus = True
        Me.tabFree.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabFree.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabFree.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabFree.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tabFree.HeightDef = 202
        Me.tabFree.Location = New System.Drawing.Point(13, 675)
        Me.tabFree.Name = "tabFree"
        Me.tabFree.SelectedIndex = 0
        Me.tabFree.Size = New System.Drawing.Size(1249, 202)
        Me.tabFree.TabIndex = 253
        Me.tabFree.TabStopSetting = True
        Me.tabFree.TextValue = ""
        Me.tabFree.WidthDef = 1249
        '
        'tabFreeM
        '
        Me.tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.tabFreeM.Controls.Add(Me.sprFreeM)
        Me.tabFreeM.Location = New System.Drawing.Point(4, 22)
        Me.tabFreeM.Name = "tabFreeM"
        Me.tabFreeM.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFreeM.Size = New System.Drawing.Size(1241, 176)
        Me.tabFreeM.TabIndex = 3
        Me.tabFreeM.Text = "自由設定項目(中)"
        Me.tabFreeM.UseVisualStyleBackColor = True
        '
        'sprFreeM
        '
        Me.sprFreeM.AccessibleDescription = ""
        Me.sprFreeM.AllowUserZoom = False
        Me.sprFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprFreeM.CellClickEventArgs = Nothing
        Me.sprFreeM.CheckToCheckBox = True
        Me.sprFreeM.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprFreeM.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprFreeM.EditModeReplace = True
        Me.sprFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprFreeM.ForeColorDef = System.Drawing.Color.Empty
        Me.sprFreeM.HeightDef = 159
        Me.sprFreeM.KeyboardCheckBoxOn = False
        Me.sprFreeM.Location = New System.Drawing.Point(5, 6)
        Me.sprFreeM.Name = "sprFreeM"
        Me.sprFreeM.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        Me.sprFreeM.Size = New System.Drawing.Size(1230, 159)
        Me.sprFreeM.SortColumn = True
        Me.sprFreeM.SpanColumnLock = True
        Me.sprFreeM.SpreadDoubleClicked = False
        Me.sprFreeM.TabIndex = 293
        Me.sprFreeM.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprFreeM.TextValue = Nothing
        Me.sprFreeM.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprFreeM.WidthDef = 1230
        '
        'LmTitleLabel19
        '
        Me.LmTitleLabel19.AutoSize = True
        Me.LmTitleLabel19.AutoSizeDef = True
        Me.LmTitleLabel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel19.EnableStatus = False
        Me.LmTitleLabel19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.HeightDef = 13
        Me.LmTitleLabel19.Location = New System.Drawing.Point(244, 159)
        Me.LmTitleLabel19.Name = "LmTitleLabel19"
        Me.LmTitleLabel19.Size = New System.Drawing.Size(14, 13)
        Me.LmTitleLabel19.TabIndex = 132
        Me.LmTitleLabel19.Text = "/"
        Me.LmTitleLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel19.TextValue = "/"
        Me.LmTitleLabel19.WidthDef = 14
        '
        'LMH020F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.FocusedControlName = "LMImTextBox"
        Me.Name = "LMH020F"
        Me.Text = "【LMH020】 EDI入荷データ編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.tabMiddle.ResumeLayout(False)
        Me.tabGoods.ResumeLayout(False)
        Me.tabGoods.PerformLayout()
        CType(Me.sprGoodsDef, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUnso.ResumeLayout(False)
        Me.pnlUnso.ResumeLayout(False)
        Me.pnlUnso.PerformLayout()
        Me.tabFreeL.ResumeLayout(False)
        CType(Me.sprFreeL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabInka.ResumeLayout(False)
        Me.tabNyuka.ResumeLayout(False)
        Me.tabNyuka.PerformLayout()
        Me.tabFree.ResumeLayout(False)
        Me.tabFreeM.ResumeLayout(False)
        CType(Me.sprFreeM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleShinshokuKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitlelblKanriNoL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleWh As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdNyukaDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleNyukaDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKanriNoL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNyukaType As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrderNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrderNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNyukaKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtBuyerOrdNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleBuyerOrdNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNiyakuUmu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtNyukaComment As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNyukaComment As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNyukaType As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbPlanQtUt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbTax As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTax As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleIrisuTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdHokanDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleHokanDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleFree As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNyukaCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleZenkiHo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel34 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSumCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtBuyerOrdNoM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleBuyerOrdNoM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSumAnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleStdIrime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsComment As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsComment As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrderNoM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrderNoM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleIrisu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtNyubanL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNyubanL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel32 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSumCntTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSumAntTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleToukiHo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStdIrimeTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numStdIrime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numIrisu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSumAnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSumBetu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSumCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents numPlanQt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNyukaCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents tabMiddle As Jp.Co.Nrs.LM.GUI.Win.LMTab
    Friend WithEvents tabGoods As System.Windows.Forms.TabPage
    Friend WithEvents tabUnso As System.Windows.Forms.TabPage
    Friend WithEvents sprGoodsDef As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents lblKanriNoM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleKanriNoM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numKosu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKonsu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleHasuTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHasu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHasu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKonsuTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTare As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleOndo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numTare As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleTareTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numFree As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents tabInka As Jp.Co.Nrs.LM.GUI.Win.LMTab
    Friend WithEvents tabNyuka As System.Windows.Forms.TabPage
    Friend WithEvents lblTitleStatus As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEdiKanriNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblEdiKanriNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlUnso As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleUnchinTehai As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleYen As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblShukkaMotoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleShukkaMotoCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnchin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnsoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnso As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOnkan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbOnkan As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbUnchinTehai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSharyoKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSharyoKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbTariffKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTariffKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtShukkaMotoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmImTextBox1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrder_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHikiate_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel60 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel67 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImNumber1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTARE_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel68 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel72 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel73 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtHasu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel74 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel78 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKosu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKonsu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImTextBox18 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmImTextBox19 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel80 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKanriNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel81 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsComment_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel89 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrisuTani1_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStdIrimeTani_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtBuyerOrdNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel92 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrisuTani2_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStdIrime_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtOrderNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numIrisu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel94 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel95 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel96 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel97 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel98 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel99 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel100 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel101 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSumAnt_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSumCnt_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents tabFree As Jp.Co.Nrs.LM.GUI.Win.LMTab
    Friend WithEvents tabFreeM As System.Windows.Forms.TabPage
    Friend WithEvents sprFreeM As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents tabFreeL As System.Windows.Forms.TabPage
    Friend WithEvents sprFreeL As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnRevival As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleIrimeTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numIrime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleIrime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSerial As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleLot As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSerial As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtLot As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNyukaKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbOndo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbStatus As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbWh As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents cmbShinshokuKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblGoodsKey As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblJotai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbToukiHo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents cmbZenkiHo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents cmbNiyakuUmu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents LmTitleLabel24 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel19 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
