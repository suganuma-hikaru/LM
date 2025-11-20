<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMD010F
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
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim spdDtl_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim spdDtl_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
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
        Dim sprDtlNew_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDtlNew_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.pnlFurikae = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblIrimeTanni = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun()
        Me.lblGoodsCdNrs = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbNiyaku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleNiyaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTaxKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTaxKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlSyukkaRemark = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtSyukkaRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsNmCust = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsCdCust = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlHutaiSagyo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblSagyoNmO1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoNmO2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoNmO3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdO1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdO2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdO3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlKosu = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblCntTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun()
        Me.lblKonsuTanni = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun()
        Me.lblTitleKonsu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numKonsu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHikiZanCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblHikiZanCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleKosu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblKosuCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHikiSumiCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblHikiSumiCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleIrisu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblIrisuCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHasu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleIrimeTanni = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numIrime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleIrime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSerialNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSerialNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleLotNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOrderNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleOrderNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnMotoDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.spdDtl = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.spdDtl_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.lblTitleFurikaeKnariNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleFurikaeKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblFurikaeNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbFurikaeKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSoko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdFurikaeDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleFruikaeDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkYoukiChange = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblTitleToukiHokanKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlFurikaeNew = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblKosuTanniNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun()
        Me.lblIrimeTanniNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun()
        Me.numIrimeNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleGoodsCdNrsNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblGoodsCdNrsNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlNyukaRemark = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtNyukaRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbNiyakuNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtGoodsNmCustNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleNiyakuNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleGoodsNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsCdCustNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.chkInkoDateUmu = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.pnlHutaiSagyoNew = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblSagyoNmN1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoNmN2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoNmN3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo1New = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdN1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo3New = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdN2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo2New = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoCdN3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbTaxKbnNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTaxKbnNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDenpNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDenpNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmMNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCustNew = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmLNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdMNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdLNew = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnSakiAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnSakiDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.sprDtlNew = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprDtlNew_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        Me.cmbToukiHokanKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.lblSagyoInNmO1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoInNmO2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoInNmO3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoInNmN3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoInNmN2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoInNmN1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        spdDtl_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        spdDtl_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDtlNew_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlFurikae.SuspendLayout()
        Me.pnlSyukkaRemark.SuspendLayout()
        Me.pnlHutaiSagyo.SuspendLayout()
        Me.pnlKosu.SuspendLayout()
        CType(Me.spdDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdDtl_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFurikaeNew.SuspendLayout()
        Me.pnlNyukaRemark.SuspendLayout()
        Me.pnlHutaiSagyoNew.SuspendLayout()
        CType(Me.sprDtlNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDtlNew_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmN3)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmN2)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmN1)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmO3)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmO2)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoInNmO1)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.cmbToukiHokanKbn)
        Me.pnlViewAria.Controls.Add(Me.cmbSoko)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.btnSakiAdd)
        Me.pnlViewAria.Controls.Add(Me.btnSakiDel)
        Me.pnlViewAria.Controls.Add(Me.pnlFurikaeNew)
        Me.pnlViewAria.Controls.Add(Me.btnMotoDel)
        Me.pnlViewAria.Controls.Add(Me.sprDtlNew)
        Me.pnlViewAria.Controls.Add(Me.spdDtl)
        Me.pnlViewAria.Controls.Add(Me.lblTitleToukiHokanKbn)
        Me.pnlViewAria.Controls.Add(Me.chkYoukiChange)
        Me.pnlViewAria.Controls.Add(Me.lblTitleFruikaeDate)
        Me.pnlViewAria.Controls.Add(Me.imdFurikaeDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSoko)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.cmbFurikaeKbn)
        Me.pnlViewAria.Controls.Add(Me.lblFurikaeNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleFurikaeKbn)
        Me.pnlViewAria.Controls.Add(Me.lblTitleFurikaeKnariNo)
        Me.pnlViewAria.Controls.Add(Me.pnlFurikae)
        Me.pnlViewAria.MinimumSize = New System.Drawing.Size(160, 0)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'pnlFurikae
        '
        Me.pnlFurikae.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlFurikae.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlFurikae.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlFurikae.Controls.Add(Me.lblIrimeTanni)
        Me.pnlFurikae.Controls.Add(Me.lblGoodsCdNrs)
        Me.pnlFurikae.Controls.Add(Me.cmbNiyaku)
        Me.pnlFurikae.Controls.Add(Me.lblTitleNiyaku)
        Me.pnlFurikae.Controls.Add(Me.cmbTaxKbn)
        Me.pnlFurikae.Controls.Add(Me.lblTitleTaxKbn)
        Me.pnlFurikae.Controls.Add(Me.pnlSyukkaRemark)
        Me.pnlFurikae.Controls.Add(Me.txtGoodsNmCust)
        Me.pnlFurikae.Controls.Add(Me.lblTitleGoods)
        Me.pnlFurikae.Controls.Add(Me.txtGoodsCdCust)
        Me.pnlFurikae.Controls.Add(Me.pnlHutaiSagyo)
        Me.pnlFurikae.Controls.Add(Me.pnlKosu)
        Me.pnlFurikae.Controls.Add(Me.lblTitleIrimeTanni)
        Me.pnlFurikae.Controls.Add(Me.numIrime)
        Me.pnlFurikae.Controls.Add(Me.lblTitleIrime)
        Me.pnlFurikae.Controls.Add(Me.txtSerialNo)
        Me.pnlFurikae.Controls.Add(Me.lblTitleSerialNo)
        Me.pnlFurikae.Controls.Add(Me.txtLotNo)
        Me.pnlFurikae.Controls.Add(Me.lblTitleLotNo)
        Me.pnlFurikae.Controls.Add(Me.txtOrderNo)
        Me.pnlFurikae.Controls.Add(Me.lblTitleOrderNo)
        Me.pnlFurikae.Controls.Add(Me.lblCustNmM)
        Me.pnlFurikae.Controls.Add(Me.lblTitleCust)
        Me.pnlFurikae.Controls.Add(Me.lblCustNmL)
        Me.pnlFurikae.Controls.Add(Me.txtCustCdM)
        Me.pnlFurikae.Controls.Add(Me.txtCustCdL)
        Me.pnlFurikae.EnableStatus = False
        Me.pnlFurikae.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlFurikae.FontDef = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlFurikae.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlFurikae.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlFurikae.HeightDef = 358
        Me.pnlFurikae.Location = New System.Drawing.Point(12, 54)
        Me.pnlFurikae.Name = "pnlFurikae"
        Me.pnlFurikae.Size = New System.Drawing.Size(625, 358)
        Me.pnlFurikae.TabIndex = 205
        Me.pnlFurikae.TabStop = False
        Me.pnlFurikae.Text = "振替元ヘッダー情報"
        Me.pnlFurikae.TextValue = "振替元ヘッダー情報"
        Me.pnlFurikae.WidthDef = 625
        '
        'lblIrimeTanni
        '
        Me.lblIrimeTanni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIrimeTanni.DataCode = "I001"
        Me.lblIrimeTanni.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeTanni.HissuLabelVisible = False
        Me.lblIrimeTanni.KbnValue = ""
        Me.lblIrimeTanni.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun.DISP_MEMBERS.KBN_NM1
        Me.lblIrimeTanni.Location = New System.Drawing.Point(273, 127)
        Me.lblIrimeTanni.Margin = New System.Windows.Forms.Padding(1)
        Me.lblIrimeTanni.Name = "lblIrimeTanni"
        Me.lblIrimeTanni.Size = New System.Drawing.Size(119, 18)
        Me.lblIrimeTanni.TabIndex = 230
        Me.lblIrimeTanni.TabStop = False
        Me.lblIrimeTanni.Value1 = Nothing
        Me.lblIrimeTanni.Value2 = Nothing
        Me.lblIrimeTanni.Value3 = Nothing
        '
        'lblGoodsCdNrs
        '
        Me.lblGoodsCdNrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsCdNrs.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCdNrs.CountWrappedLine = False
        Me.lblGoodsCdNrs.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsCdNrs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrs.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrs.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrs.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsCdNrs.HeightDef = 18
        Me.lblGoodsCdNrs.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.HissuLabelVisible = False
        Me.lblGoodsCdNrs.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsCdNrs.IsByteCheck = 0
        Me.lblGoodsCdNrs.IsCalendarCheck = False
        Me.lblGoodsCdNrs.IsDakutenCheck = False
        Me.lblGoodsCdNrs.IsEisuCheck = False
        Me.lblGoodsCdNrs.IsForbiddenWordsCheck = False
        Me.lblGoodsCdNrs.IsFullByteCheck = 0
        Me.lblGoodsCdNrs.IsHankakuCheck = False
        Me.lblGoodsCdNrs.IsHissuCheck = False
        Me.lblGoodsCdNrs.IsKanaCheck = False
        Me.lblGoodsCdNrs.IsMiddleSpace = False
        Me.lblGoodsCdNrs.IsNumericCheck = False
        Me.lblGoodsCdNrs.IsSujiCheck = False
        Me.lblGoodsCdNrs.IsZenkakuCheck = False
        Me.lblGoodsCdNrs.ItemName = ""
        Me.lblGoodsCdNrs.LineSpace = 0
        Me.lblGoodsCdNrs.Location = New System.Drawing.Point(286, 63)
        Me.lblGoodsCdNrs.MaxLength = 0
        Me.lblGoodsCdNrs.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsCdNrs.MaxLineCount = 0
        Me.lblGoodsCdNrs.Multiline = False
        Me.lblGoodsCdNrs.Name = "lblGoodsCdNrs"
        Me.lblGoodsCdNrs.ReadOnly = True
        Me.lblGoodsCdNrs.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsCdNrs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsCdNrs.Size = New System.Drawing.Size(333, 18)
        Me.lblGoodsCdNrs.TabIndex = 284
        Me.lblGoodsCdNrs.TabStop = False
        Me.lblGoodsCdNrs.TabStopSetting = False
        Me.lblGoodsCdNrs.TextValue = ""
        Me.lblGoodsCdNrs.UseSystemPasswordChar = False
        Me.lblGoodsCdNrs.Visible = False
        Me.lblGoodsCdNrs.WidthDef = 333
        Me.lblGoodsCdNrs.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbNiyaku
        '
        Me.cmbNiyaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNiyaku.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNiyaku.DataCode = "U009"
        Me.cmbNiyaku.DataSource = Nothing
        Me.cmbNiyaku.DisplayMember = Nothing
        Me.cmbNiyaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyaku.HeightDef = 18
        Me.cmbNiyaku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNiyaku.HissuLabelVisible = True
        Me.cmbNiyaku.InsertWildCard = True
        Me.cmbNiyaku.IsForbiddenWordsCheck = False
        Me.cmbNiyaku.IsHissuCheck = True
        Me.cmbNiyaku.ItemName = ""
        Me.cmbNiyaku.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNiyaku.Location = New System.Drawing.Point(98, 148)
        Me.cmbNiyaku.Name = "cmbNiyaku"
        Me.cmbNiyaku.ReadOnly = False
        Me.cmbNiyaku.SelectedIndex = -1
        Me.cmbNiyaku.SelectedItem = Nothing
        Me.cmbNiyaku.SelectedText = ""
        Me.cmbNiyaku.SelectedValue = ""
        Me.cmbNiyaku.Size = New System.Drawing.Size(83, 18)
        Me.cmbNiyaku.TabIndex = 283
        Me.cmbNiyaku.TabStopSetting = True
        Me.cmbNiyaku.TextValue = ""
        Me.cmbNiyaku.Value1 = Nothing
        Me.cmbNiyaku.Value2 = Nothing
        Me.cmbNiyaku.Value3 = Nothing
        Me.cmbNiyaku.ValueMember = Nothing
        Me.cmbNiyaku.WidthDef = 83
        '
        'lblTitleNiyaku
        '
        Me.lblTitleNiyaku.AutoSize = True
        Me.lblTitleNiyaku.AutoSizeDef = True
        Me.lblTitleNiyaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyaku.EnableStatus = False
        Me.lblTitleNiyaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyaku.HeightDef = 13
        Me.lblTitleNiyaku.Location = New System.Drawing.Point(4, 151)
        Me.lblTitleNiyaku.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleNiyaku.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleNiyaku.Name = "lblTitleNiyaku"
        Me.lblTitleNiyaku.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleNiyaku.TabIndex = 282
        Me.lblTitleNiyaku.Text = "荷役料"
        Me.lblTitleNiyaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyaku.TextValue = "荷役料"
        Me.lblTitleNiyaku.WidthDef = 90
        '
        'cmbTaxKbn
        '
        Me.cmbTaxKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTaxKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTaxKbn.DataCode = "Z001"
        Me.cmbTaxKbn.DataSource = Nothing
        Me.cmbTaxKbn.DisplayMember = Nothing
        Me.cmbTaxKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKbn.HeightDef = 18
        Me.cmbTaxKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKbn.HissuLabelVisible = False
        Me.cmbTaxKbn.InsertWildCard = True
        Me.cmbTaxKbn.IsForbiddenWordsCheck = False
        Me.cmbTaxKbn.IsHissuCheck = False
        Me.cmbTaxKbn.ItemName = ""
        Me.cmbTaxKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTaxKbn.Location = New System.Drawing.Point(273, 148)
        Me.cmbTaxKbn.Name = "cmbTaxKbn"
        Me.cmbTaxKbn.ReadOnly = True
        Me.cmbTaxKbn.SelectedIndex = -1
        Me.cmbTaxKbn.SelectedItem = Nothing
        Me.cmbTaxKbn.SelectedText = ""
        Me.cmbTaxKbn.SelectedValue = ""
        Me.cmbTaxKbn.Size = New System.Drawing.Size(83, 18)
        Me.cmbTaxKbn.TabIndex = 280
        Me.cmbTaxKbn.TabStop = False
        Me.cmbTaxKbn.TabStopSetting = False
        Me.cmbTaxKbn.TextValue = ""
        Me.cmbTaxKbn.Value1 = Nothing
        Me.cmbTaxKbn.Value2 = Nothing
        Me.cmbTaxKbn.Value3 = Nothing
        Me.cmbTaxKbn.ValueMember = Nothing
        Me.cmbTaxKbn.WidthDef = 83
        '
        'lblTitleTaxKbn
        '
        Me.lblTitleTaxKbn.AutoSize = True
        Me.lblTitleTaxKbn.AutoSizeDef = True
        Me.lblTitleTaxKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTaxKbn.EnableStatus = False
        Me.lblTitleTaxKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKbn.HeightDef = 13
        Me.lblTitleTaxKbn.Location = New System.Drawing.Point(187, 151)
        Me.lblTitleTaxKbn.MinimumSize = New System.Drawing.Size(80, 0)
        Me.lblTitleTaxKbn.Name = "lblTitleTaxKbn"
        Me.lblTitleTaxKbn.Size = New System.Drawing.Size(80, 13)
        Me.lblTitleTaxKbn.TabIndex = 281
        Me.lblTitleTaxKbn.Text = "課税区分"
        Me.lblTitleTaxKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTaxKbn.TextValue = "課税区分"
        Me.lblTitleTaxKbn.WidthDef = 80
        '
        'pnlSyukkaRemark
        '
        Me.pnlSyukkaRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSyukkaRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSyukkaRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSyukkaRemark.Controls.Add(Me.txtSyukkaRemark)
        Me.pnlSyukkaRemark.EnableStatus = False
        Me.pnlSyukkaRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSyukkaRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSyukkaRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSyukkaRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSyukkaRemark.HeightDef = 88
        Me.pnlSyukkaRemark.Location = New System.Drawing.Point(210, 261)
        Me.pnlSyukkaRemark.Name = "pnlSyukkaRemark"
        Me.pnlSyukkaRemark.Size = New System.Drawing.Size(407, 88)
        Me.pnlSyukkaRemark.TabIndex = 238
        Me.pnlSyukkaRemark.TabStop = False
        Me.pnlSyukkaRemark.Text = "出荷時注意事項"
        Me.pnlSyukkaRemark.TextValue = "出荷時注意事項"
        Me.pnlSyukkaRemark.WidthDef = 407
        '
        'txtSyukkaRemark
        '
        Me.txtSyukkaRemark.AutoScroll = True
        Me.txtSyukkaRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSyukkaRemark.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtSyukkaRemark.CountWrappedLine = False
        Me.txtSyukkaRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSyukkaRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSyukkaRemark.HeightDef = 60
        Me.txtSyukkaRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaRemark.HissuLabelVisible = False
        Me.txtSyukkaRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSyukkaRemark.IsByteCheck = 100
        Me.txtSyukkaRemark.IsCalendarCheck = False
        Me.txtSyukkaRemark.IsDakutenCheck = False
        Me.txtSyukkaRemark.IsEisuCheck = False
        Me.txtSyukkaRemark.IsForbiddenWordsCheck = False
        Me.txtSyukkaRemark.IsFullByteCheck = 0
        Me.txtSyukkaRemark.IsHankakuCheck = False
        Me.txtSyukkaRemark.IsHissuCheck = False
        Me.txtSyukkaRemark.IsKanaCheck = False
        Me.txtSyukkaRemark.IsMiddleSpace = False
        Me.txtSyukkaRemark.IsNumericCheck = False
        Me.txtSyukkaRemark.IsSujiCheck = False
        Me.txtSyukkaRemark.IsZenkakuCheck = False
        Me.txtSyukkaRemark.ItemName = ""
        Me.txtSyukkaRemark.LineSpace = 0
        Me.txtSyukkaRemark.Location = New System.Drawing.Point(9, 19)
        Me.txtSyukkaRemark.MaxLength = 100
        Me.txtSyukkaRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSyukkaRemark.MaxLineCount = 0
        Me.txtSyukkaRemark.Multiline = True
        Me.txtSyukkaRemark.Name = "txtSyukkaRemark"
        Me.txtSyukkaRemark.ReadOnly = False
        Me.txtSyukkaRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSyukkaRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSyukkaRemark.Size = New System.Drawing.Size(395, 60)
        Me.txtSyukkaRemark.TabIndex = 193
        Me.txtSyukkaRemark.TabStopSetting = True
        Me.txtSyukkaRemark.TextValue = ""
        Me.txtSyukkaRemark.UseSystemPasswordChar = False
        Me.txtSyukkaRemark.WidthDef = 395
        Me.txtSyukkaRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsNmCust
        '
        Me.txtGoodsNmCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNmCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNmCust.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsNmCust.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsNmCust.CountWrappedLine = False
        Me.txtGoodsNmCust.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsNmCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNmCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNmCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNmCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNmCust.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsNmCust.HeightDef = 18
        Me.txtGoodsNmCust.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNmCust.HissuLabelVisible = False
        Me.txtGoodsNmCust.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsNmCust.IsByteCheck = 60
        Me.txtGoodsNmCust.IsCalendarCheck = False
        Me.txtGoodsNmCust.IsDakutenCheck = False
        Me.txtGoodsNmCust.IsEisuCheck = False
        Me.txtGoodsNmCust.IsForbiddenWordsCheck = False
        Me.txtGoodsNmCust.IsFullByteCheck = 0
        Me.txtGoodsNmCust.IsHankakuCheck = False
        Me.txtGoodsNmCust.IsHissuCheck = False
        Me.txtGoodsNmCust.IsKanaCheck = False
        Me.txtGoodsNmCust.IsMiddleSpace = False
        Me.txtGoodsNmCust.IsNumericCheck = False
        Me.txtGoodsNmCust.IsSujiCheck = False
        Me.txtGoodsNmCust.IsZenkakuCheck = False
        Me.txtGoodsNmCust.ItemName = ""
        Me.txtGoodsNmCust.LineSpace = 0
        Me.txtGoodsNmCust.Location = New System.Drawing.Point(241, 85)
        Me.txtGoodsNmCust.MaxLength = 60
        Me.txtGoodsNmCust.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNmCust.MaxLineCount = 0
        Me.txtGoodsNmCust.Multiline = False
        Me.txtGoodsNmCust.Name = "txtGoodsNmCust"
        Me.txtGoodsNmCust.ReadOnly = False
        Me.txtGoodsNmCust.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNmCust.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNmCust.Size = New System.Drawing.Size(333, 18)
        Me.txtGoodsNmCust.TabIndex = 272
        Me.txtGoodsNmCust.TabStopSetting = True
        Me.txtGoodsNmCust.TextValue = ""
        Me.txtGoodsNmCust.UseSystemPasswordChar = False
        Me.txtGoodsNmCust.WidthDef = 333
        Me.txtGoodsNmCust.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleGoods.Location = New System.Drawing.Point(4, 87)
        Me.lblTitleGoods.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleGoods.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleGoods.Name = "lblTitleGoods"
        Me.lblTitleGoods.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleGoods.TabIndex = 271
        Me.lblTitleGoods.Text = "商品"
        Me.lblTitleGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoods.TextValue = "商品"
        Me.lblTitleGoods.WidthDef = 90
        '
        'txtGoodsCdCust
        '
        Me.txtGoodsCdCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCust.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdCust.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdCust.CountWrappedLine = False
        Me.txtGoodsCdCust.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCust.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdCust.HeightDef = 18
        Me.txtGoodsCdCust.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCust.HissuLabelVisible = False
        Me.txtGoodsCdCust.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsCdCust.IsByteCheck = 20
        Me.txtGoodsCdCust.IsCalendarCheck = False
        Me.txtGoodsCdCust.IsDakutenCheck = False
        Me.txtGoodsCdCust.IsEisuCheck = False
        Me.txtGoodsCdCust.IsForbiddenWordsCheck = False
        Me.txtGoodsCdCust.IsFullByteCheck = 0
        Me.txtGoodsCdCust.IsHankakuCheck = False
        Me.txtGoodsCdCust.IsHissuCheck = False
        Me.txtGoodsCdCust.IsKanaCheck = False
        Me.txtGoodsCdCust.IsMiddleSpace = False
        Me.txtGoodsCdCust.IsNumericCheck = False
        Me.txtGoodsCdCust.IsSujiCheck = False
        Me.txtGoodsCdCust.IsZenkakuCheck = False
        Me.txtGoodsCdCust.ItemName = ""
        Me.txtGoodsCdCust.LineSpace = 0
        Me.txtGoodsCdCust.Location = New System.Drawing.Point(98, 85)
        Me.txtGoodsCdCust.MaxLength = 20
        Me.txtGoodsCdCust.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdCust.MaxLineCount = 0
        Me.txtGoodsCdCust.Multiline = False
        Me.txtGoodsCdCust.Name = "txtGoodsCdCust"
        Me.txtGoodsCdCust.ReadOnly = False
        Me.txtGoodsCdCust.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdCust.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdCust.Size = New System.Drawing.Size(159, 18)
        Me.txtGoodsCdCust.TabIndex = 270
        Me.txtGoodsCdCust.TabStopSetting = True
        Me.txtGoodsCdCust.TextValue = "XXXXXXXXXXXXXXXXXXXZ"
        Me.txtGoodsCdCust.UseSystemPasswordChar = False
        Me.txtGoodsCdCust.WidthDef = 159
        Me.txtGoodsCdCust.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlHutaiSagyo
        '
        Me.pnlHutaiSagyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHutaiSagyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHutaiSagyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlHutaiSagyo.Controls.Add(Me.lblSagyoNmO1)
        Me.pnlHutaiSagyo.Controls.Add(Me.lblSagyoNmO2)
        Me.pnlHutaiSagyo.Controls.Add(Me.lblSagyoNmO3)
        Me.pnlHutaiSagyo.Controls.Add(Me.lblTitleSagyo1)
        Me.pnlHutaiSagyo.Controls.Add(Me.txtSagyoCdO1)
        Me.pnlHutaiSagyo.Controls.Add(Me.lblTitleSagyo3)
        Me.pnlHutaiSagyo.Controls.Add(Me.txtSagyoCdO2)
        Me.pnlHutaiSagyo.Controls.Add(Me.lblTitleSagyo2)
        Me.pnlHutaiSagyo.Controls.Add(Me.txtSagyoCdO3)
        Me.pnlHutaiSagyo.EnableStatus = False
        Me.pnlHutaiSagyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHutaiSagyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHutaiSagyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHutaiSagyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHutaiSagyo.HeightDef = 88
        Me.pnlHutaiSagyo.Location = New System.Drawing.Point(6, 261)
        Me.pnlHutaiSagyo.Name = "pnlHutaiSagyo"
        Me.pnlHutaiSagyo.Size = New System.Drawing.Size(200, 88)
        Me.pnlHutaiSagyo.TabIndex = 237
        Me.pnlHutaiSagyo.TabStop = False
        Me.pnlHutaiSagyo.Text = "付帯作業"
        Me.pnlHutaiSagyo.TextValue = "付帯作業"
        Me.pnlHutaiSagyo.WidthDef = 200
        '
        'lblSagyoNmO1
        '
        Me.lblSagyoNmO1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmO1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmO1.CountWrappedLine = False
        Me.lblSagyoNmO1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmO1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmO1.HeightDef = 18
        Me.lblSagyoNmO1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO1.HissuLabelVisible = False
        Me.lblSagyoNmO1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmO1.IsByteCheck = 0
        Me.lblSagyoNmO1.IsCalendarCheck = False
        Me.lblSagyoNmO1.IsDakutenCheck = False
        Me.lblSagyoNmO1.IsEisuCheck = False
        Me.lblSagyoNmO1.IsForbiddenWordsCheck = False
        Me.lblSagyoNmO1.IsFullByteCheck = 0
        Me.lblSagyoNmO1.IsHankakuCheck = False
        Me.lblSagyoNmO1.IsHissuCheck = False
        Me.lblSagyoNmO1.IsKanaCheck = False
        Me.lblSagyoNmO1.IsMiddleSpace = False
        Me.lblSagyoNmO1.IsNumericCheck = False
        Me.lblSagyoNmO1.IsSujiCheck = False
        Me.lblSagyoNmO1.IsZenkakuCheck = False
        Me.lblSagyoNmO1.ItemName = ""
        Me.lblSagyoNmO1.LineSpace = 0
        Me.lblSagyoNmO1.Location = New System.Drawing.Point(88, 19)
        Me.lblSagyoNmO1.MaxLength = 0
        Me.lblSagyoNmO1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmO1.MaxLineCount = 0
        Me.lblSagyoNmO1.Multiline = False
        Me.lblSagyoNmO1.Name = "lblSagyoNmO1"
        Me.lblSagyoNmO1.ReadOnly = True
        Me.lblSagyoNmO1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmO1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmO1.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmO1.TabIndex = 194
        Me.lblSagyoNmO1.TabStop = False
        Me.lblSagyoNmO1.TabStopSetting = False
        Me.lblSagyoNmO1.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoNmO1.UseSystemPasswordChar = False
        Me.lblSagyoNmO1.WidthDef = 110
        Me.lblSagyoNmO1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoNmO2
        '
        Me.lblSagyoNmO2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmO2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmO2.CountWrappedLine = False
        Me.lblSagyoNmO2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmO2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmO2.HeightDef = 18
        Me.lblSagyoNmO2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO2.HissuLabelVisible = False
        Me.lblSagyoNmO2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmO2.IsByteCheck = 0
        Me.lblSagyoNmO2.IsCalendarCheck = False
        Me.lblSagyoNmO2.IsDakutenCheck = False
        Me.lblSagyoNmO2.IsEisuCheck = False
        Me.lblSagyoNmO2.IsForbiddenWordsCheck = False
        Me.lblSagyoNmO2.IsFullByteCheck = 0
        Me.lblSagyoNmO2.IsHankakuCheck = False
        Me.lblSagyoNmO2.IsHissuCheck = False
        Me.lblSagyoNmO2.IsKanaCheck = False
        Me.lblSagyoNmO2.IsMiddleSpace = False
        Me.lblSagyoNmO2.IsNumericCheck = False
        Me.lblSagyoNmO2.IsSujiCheck = False
        Me.lblSagyoNmO2.IsZenkakuCheck = False
        Me.lblSagyoNmO2.ItemName = ""
        Me.lblSagyoNmO2.LineSpace = 0
        Me.lblSagyoNmO2.Location = New System.Drawing.Point(88, 41)
        Me.lblSagyoNmO2.MaxLength = 0
        Me.lblSagyoNmO2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmO2.MaxLineCount = 0
        Me.lblSagyoNmO2.Multiline = False
        Me.lblSagyoNmO2.Name = "lblSagyoNmO2"
        Me.lblSagyoNmO2.ReadOnly = True
        Me.lblSagyoNmO2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmO2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmO2.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmO2.TabIndex = 197
        Me.lblSagyoNmO2.TabStop = False
        Me.lblSagyoNmO2.TabStopSetting = False
        Me.lblSagyoNmO2.TextValue = ""
        Me.lblSagyoNmO2.UseSystemPasswordChar = False
        Me.lblSagyoNmO2.WidthDef = 110
        Me.lblSagyoNmO2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoNmO3
        '
        Me.lblSagyoNmO3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmO3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmO3.CountWrappedLine = False
        Me.lblSagyoNmO3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmO3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmO3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmO3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmO3.HeightDef = 18
        Me.lblSagyoNmO3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmO3.HissuLabelVisible = False
        Me.lblSagyoNmO3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmO3.IsByteCheck = 0
        Me.lblSagyoNmO3.IsCalendarCheck = False
        Me.lblSagyoNmO3.IsDakutenCheck = False
        Me.lblSagyoNmO3.IsEisuCheck = False
        Me.lblSagyoNmO3.IsForbiddenWordsCheck = False
        Me.lblSagyoNmO3.IsFullByteCheck = 0
        Me.lblSagyoNmO3.IsHankakuCheck = False
        Me.lblSagyoNmO3.IsHissuCheck = False
        Me.lblSagyoNmO3.IsKanaCheck = False
        Me.lblSagyoNmO3.IsMiddleSpace = False
        Me.lblSagyoNmO3.IsNumericCheck = False
        Me.lblSagyoNmO3.IsSujiCheck = False
        Me.lblSagyoNmO3.IsZenkakuCheck = False
        Me.lblSagyoNmO3.ItemName = ""
        Me.lblSagyoNmO3.LineSpace = 0
        Me.lblSagyoNmO3.Location = New System.Drawing.Point(88, 62)
        Me.lblSagyoNmO3.MaxLength = 0
        Me.lblSagyoNmO3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmO3.MaxLineCount = 0
        Me.lblSagyoNmO3.Multiline = False
        Me.lblSagyoNmO3.Name = "lblSagyoNmO3"
        Me.lblSagyoNmO3.ReadOnly = True
        Me.lblSagyoNmO3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmO3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmO3.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmO3.TabIndex = 200
        Me.lblSagyoNmO3.TabStop = False
        Me.lblSagyoNmO3.TabStopSetting = False
        Me.lblSagyoNmO3.TextValue = ""
        Me.lblSagyoNmO3.UseSystemPasswordChar = False
        Me.lblSagyoNmO3.WidthDef = 110
        Me.lblSagyoNmO3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo1
        '
        Me.lblTitleSagyo1.AutoSize = True
        Me.lblTitleSagyo1.AutoSizeDef = True
        Me.lblTitleSagyo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo1.EnableStatus = False
        Me.lblTitleSagyo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1.HeightDef = 13
        Me.lblTitleSagyo1.Location = New System.Drawing.Point(9, 21)
        Me.lblTitleSagyo1.Name = "lblTitleSagyo1"
        Me.lblTitleSagyo1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo1.TabIndex = 215
        Me.lblTitleSagyo1.Text = "①"
        Me.lblTitleSagyo1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo1.TextValue = "①"
        Me.lblTitleSagyo1.WidthDef = 21
        '
        'txtSagyoCdO1
        '
        Me.txtSagyoCdO1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdO1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdO1.CountWrappedLine = False
        Me.txtSagyoCdO1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdO1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdO1.HeightDef = 18
        Me.txtSagyoCdO1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdO1.HissuLabelVisible = False
        Me.txtSagyoCdO1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdO1.IsByteCheck = 5
        Me.txtSagyoCdO1.IsCalendarCheck = False
        Me.txtSagyoCdO1.IsDakutenCheck = False
        Me.txtSagyoCdO1.IsEisuCheck = False
        Me.txtSagyoCdO1.IsForbiddenWordsCheck = False
        Me.txtSagyoCdO1.IsFullByteCheck = 0
        Me.txtSagyoCdO1.IsHankakuCheck = False
        Me.txtSagyoCdO1.IsHissuCheck = False
        Me.txtSagyoCdO1.IsKanaCheck = False
        Me.txtSagyoCdO1.IsMiddleSpace = False
        Me.txtSagyoCdO1.IsNumericCheck = False
        Me.txtSagyoCdO1.IsSujiCheck = False
        Me.txtSagyoCdO1.IsZenkakuCheck = False
        Me.txtSagyoCdO1.ItemName = ""
        Me.txtSagyoCdO1.LineSpace = 0
        Me.txtSagyoCdO1.Location = New System.Drawing.Point(36, 19)
        Me.txtSagyoCdO1.MaxLength = 5
        Me.txtSagyoCdO1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdO1.MaxLineCount = 0
        Me.txtSagyoCdO1.Multiline = False
        Me.txtSagyoCdO1.Name = "txtSagyoCdO1"
        Me.txtSagyoCdO1.ReadOnly = False
        Me.txtSagyoCdO1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdO1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdO1.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdO1.TabIndex = 193
        Me.txtSagyoCdO1.TabStopSetting = True
        Me.txtSagyoCdO1.TextValue = "XXXXX"
        Me.txtSagyoCdO1.UseSystemPasswordChar = False
        Me.txtSagyoCdO1.WidthDef = 68
        Me.txtSagyoCdO1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo3
        '
        Me.lblTitleSagyo3.AutoSize = True
        Me.lblTitleSagyo3.AutoSizeDef = True
        Me.lblTitleSagyo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo3.EnableStatus = False
        Me.lblTitleSagyo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3.HeightDef = 13
        Me.lblTitleSagyo3.Location = New System.Drawing.Point(9, 65)
        Me.lblTitleSagyo3.Name = "lblTitleSagyo3"
        Me.lblTitleSagyo3.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo3.TabIndex = 217
        Me.lblTitleSagyo3.Text = "③"
        Me.lblTitleSagyo3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo3.TextValue = "③"
        Me.lblTitleSagyo3.WidthDef = 21
        '
        'txtSagyoCdO2
        '
        Me.txtSagyoCdO2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdO2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdO2.CountWrappedLine = False
        Me.txtSagyoCdO2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdO2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdO2.HeightDef = 18
        Me.txtSagyoCdO2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdO2.HissuLabelVisible = False
        Me.txtSagyoCdO2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdO2.IsByteCheck = 5
        Me.txtSagyoCdO2.IsCalendarCheck = False
        Me.txtSagyoCdO2.IsDakutenCheck = False
        Me.txtSagyoCdO2.IsEisuCheck = False
        Me.txtSagyoCdO2.IsForbiddenWordsCheck = False
        Me.txtSagyoCdO2.IsFullByteCheck = 0
        Me.txtSagyoCdO2.IsHankakuCheck = False
        Me.txtSagyoCdO2.IsHissuCheck = False
        Me.txtSagyoCdO2.IsKanaCheck = False
        Me.txtSagyoCdO2.IsMiddleSpace = False
        Me.txtSagyoCdO2.IsNumericCheck = False
        Me.txtSagyoCdO2.IsSujiCheck = False
        Me.txtSagyoCdO2.IsZenkakuCheck = False
        Me.txtSagyoCdO2.ItemName = ""
        Me.txtSagyoCdO2.LineSpace = 0
        Me.txtSagyoCdO2.Location = New System.Drawing.Point(36, 41)
        Me.txtSagyoCdO2.MaxLength = 5
        Me.txtSagyoCdO2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdO2.MaxLineCount = 0
        Me.txtSagyoCdO2.Multiline = False
        Me.txtSagyoCdO2.Name = "txtSagyoCdO2"
        Me.txtSagyoCdO2.ReadOnly = False
        Me.txtSagyoCdO2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdO2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdO2.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdO2.TabIndex = 196
        Me.txtSagyoCdO2.TabStopSetting = True
        Me.txtSagyoCdO2.TextValue = ""
        Me.txtSagyoCdO2.UseSystemPasswordChar = False
        Me.txtSagyoCdO2.WidthDef = 68
        Me.txtSagyoCdO2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo2
        '
        Me.lblTitleSagyo2.AutoSize = True
        Me.lblTitleSagyo2.AutoSizeDef = True
        Me.lblTitleSagyo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo2.EnableStatus = False
        Me.lblTitleSagyo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2.HeightDef = 13
        Me.lblTitleSagyo2.Location = New System.Drawing.Point(9, 43)
        Me.lblTitleSagyo2.Name = "lblTitleSagyo2"
        Me.lblTitleSagyo2.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo2.TabIndex = 216
        Me.lblTitleSagyo2.Text = "②"
        Me.lblTitleSagyo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo2.TextValue = "②"
        Me.lblTitleSagyo2.WidthDef = 21
        '
        'txtSagyoCdO3
        '
        Me.txtSagyoCdO3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdO3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdO3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdO3.CountWrappedLine = False
        Me.txtSagyoCdO3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdO3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdO3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdO3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdO3.HeightDef = 18
        Me.txtSagyoCdO3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdO3.HissuLabelVisible = False
        Me.txtSagyoCdO3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdO3.IsByteCheck = 5
        Me.txtSagyoCdO3.IsCalendarCheck = False
        Me.txtSagyoCdO3.IsDakutenCheck = False
        Me.txtSagyoCdO3.IsEisuCheck = False
        Me.txtSagyoCdO3.IsForbiddenWordsCheck = False
        Me.txtSagyoCdO3.IsFullByteCheck = 0
        Me.txtSagyoCdO3.IsHankakuCheck = False
        Me.txtSagyoCdO3.IsHissuCheck = False
        Me.txtSagyoCdO3.IsKanaCheck = False
        Me.txtSagyoCdO3.IsMiddleSpace = False
        Me.txtSagyoCdO3.IsNumericCheck = False
        Me.txtSagyoCdO3.IsSujiCheck = False
        Me.txtSagyoCdO3.IsZenkakuCheck = False
        Me.txtSagyoCdO3.ItemName = ""
        Me.txtSagyoCdO3.LineSpace = 0
        Me.txtSagyoCdO3.Location = New System.Drawing.Point(36, 62)
        Me.txtSagyoCdO3.MaxLength = 5
        Me.txtSagyoCdO3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdO3.MaxLineCount = 0
        Me.txtSagyoCdO3.Multiline = False
        Me.txtSagyoCdO3.Name = "txtSagyoCdO3"
        Me.txtSagyoCdO3.ReadOnly = False
        Me.txtSagyoCdO3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdO3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdO3.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdO3.TabIndex = 199
        Me.txtSagyoCdO3.TabStopSetting = True
        Me.txtSagyoCdO3.TextValue = ""
        Me.txtSagyoCdO3.UseSystemPasswordChar = False
        Me.txtSagyoCdO3.WidthDef = 68
        Me.txtSagyoCdO3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlKosu
        '
        Me.pnlKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlKosu.Controls.Add(Me.lblCntTani)
        Me.pnlKosu.Controls.Add(Me.lblKonsuTanni)
        Me.pnlKosu.Controls.Add(Me.lblTitleKonsu)
        Me.pnlKosu.Controls.Add(Me.numKonsu)
        Me.pnlKosu.Controls.Add(Me.lblTitleHikiZanCnt)
        Me.pnlKosu.Controls.Add(Me.lblHikiZanCnt)
        Me.pnlKosu.Controls.Add(Me.lblTitleKosu)
        Me.pnlKosu.Controls.Add(Me.lblKosuCnt)
        Me.pnlKosu.Controls.Add(Me.lblTitleHikiSumiCnt)
        Me.pnlKosu.Controls.Add(Me.lblHikiSumiCnt)
        Me.pnlKosu.Controls.Add(Me.lblTitleIrisu)
        Me.pnlKosu.Controls.Add(Me.lblIrisuCnt)
        Me.pnlKosu.Controls.Add(Me.lblTitleHasu)
        Me.pnlKosu.Controls.Add(Me.numCnt)
        Me.pnlKosu.EnableStatus = False
        Me.pnlKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlKosu.HeightDef = 88
        Me.pnlKosu.Location = New System.Drawing.Point(6, 167)
        Me.pnlKosu.Name = "pnlKosu"
        Me.pnlKosu.Size = New System.Drawing.Size(611, 88)
        Me.pnlKosu.TabIndex = 236
        Me.pnlKosu.TabStop = False
        Me.pnlKosu.Text = "個数"
        Me.pnlKosu.TextValue = "個数"
        Me.pnlKosu.WidthDef = 611
        '
        'lblCntTani
        '
        Me.lblCntTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCntTani.DataCode = "K002"
        Me.lblCntTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCntTani.HissuLabelVisible = False
        Me.lblCntTani.KbnValue = ""
        Me.lblCntTani.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun.DISP_MEMBERS.KBN_NM1
        Me.lblCntTani.Location = New System.Drawing.Point(489, 19)
        Me.lblCntTani.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCntTani.Name = "lblCntTani"
        Me.lblCntTani.Size = New System.Drawing.Size(119, 18)
        Me.lblCntTani.TabIndex = 204
        Me.lblCntTani.TabStop = False
        Me.lblCntTani.Value1 = Nothing
        Me.lblCntTani.Value2 = Nothing
        Me.lblCntTani.Value3 = Nothing
        '
        'lblKonsuTanni
        '
        Me.lblKonsuTanni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKonsuTanni.DataCode = "K002"
        Me.lblKonsuTanni.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKonsuTanni.HissuLabelVisible = False
        Me.lblKonsuTanni.KbnValue = ""
        Me.lblKonsuTanni.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun.DISP_MEMBERS.KBN_NM1
        Me.lblKonsuTanni.Location = New System.Drawing.Point(186, 19)
        Me.lblKonsuTanni.Margin = New System.Windows.Forms.Padding(1)
        Me.lblKonsuTanni.Name = "lblKonsuTanni"
        Me.lblKonsuTanni.Size = New System.Drawing.Size(119, 18)
        Me.lblKonsuTanni.TabIndex = 203
        Me.lblKonsuTanni.TabStop = False
        Me.lblKonsuTanni.Value1 = Nothing
        Me.lblKonsuTanni.Value2 = Nothing
        Me.lblKonsuTanni.Value3 = Nothing
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
        Me.lblTitleKonsu.Location = New System.Drawing.Point(4, 21)
        Me.lblTitleKonsu.MinimumSize = New System.Drawing.Size(84, 0)
        Me.lblTitleKonsu.Name = "lblTitleKonsu"
        Me.lblTitleKonsu.Size = New System.Drawing.Size(84, 13)
        Me.lblTitleKonsu.TabIndex = 202
        Me.lblTitleKonsu.Text = "梱数"
        Me.lblTitleKonsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKonsu.TextValue = "梱数"
        Me.lblTitleKonsu.WidthDef = 84
        '
        'numKonsu
        '
        Me.numKonsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKonsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKonsu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKonsu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKonsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKonsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKonsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKonsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKonsu.HeightDef = 18
        Me.numKonsu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKonsu.HissuLabelVisible = False
        Me.numKonsu.IsHissuCheck = False
        Me.numKonsu.IsRangeCheck = False
        Me.numKonsu.ItemName = ""
        Me.numKonsu.Location = New System.Drawing.Point(92, 19)
        Me.numKonsu.Name = "numKonsu"
        Me.numKonsu.ReadOnly = False
        Me.numKonsu.Size = New System.Drawing.Size(110, 18)
        Me.numKonsu.TabIndex = 201
        Me.numKonsu.TabStopSetting = True
        Me.numKonsu.TextValue = "0"
        Me.numKonsu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKonsu.WidthDef = 110
        '
        'lblTitleHikiZanCnt
        '
        Me.lblTitleHikiZanCnt.AutoSize = True
        Me.lblTitleHikiZanCnt.AutoSizeDef = True
        Me.lblTitleHikiZanCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiZanCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiZanCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHikiZanCnt.EnableStatus = False
        Me.lblTitleHikiZanCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiZanCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiZanCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiZanCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiZanCnt.HeightDef = 13
        Me.lblTitleHikiZanCnt.Location = New System.Drawing.Point(271, 65)
        Me.lblTitleHikiZanCnt.MinimumSize = New System.Drawing.Size(120, 0)
        Me.lblTitleHikiZanCnt.Name = "lblTitleHikiZanCnt"
        Me.lblTitleHikiZanCnt.Size = New System.Drawing.Size(120, 13)
        Me.lblTitleHikiZanCnt.TabIndex = 117
        Me.lblTitleHikiZanCnt.Text = "引当残個数"
        Me.lblTitleHikiZanCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHikiZanCnt.TextValue = "引当残個数"
        Me.lblTitleHikiZanCnt.WidthDef = 120
        '
        'lblHikiZanCnt
        '
        Me.lblHikiZanCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHikiZanCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiZanCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanCnt.HeightDef = 18
        Me.lblHikiZanCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanCnt.HissuLabelVisible = False
        Me.lblHikiZanCnt.IsHissuCheck = False
        Me.lblHikiZanCnt.IsRangeCheck = False
        Me.lblHikiZanCnt.ItemName = ""
        Me.lblHikiZanCnt.Location = New System.Drawing.Point(395, 62)
        Me.lblHikiZanCnt.Name = "lblHikiZanCnt"
        Me.lblHikiZanCnt.ReadOnly = True
        Me.lblHikiZanCnt.Size = New System.Drawing.Size(110, 18)
        Me.lblHikiZanCnt.TabIndex = 13
        Me.lblHikiZanCnt.TabStop = False
        Me.lblHikiZanCnt.TabStopSetting = False
        Me.lblHikiZanCnt.TextValue = "0"
        Me.lblHikiZanCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblHikiZanCnt.WidthDef = 110
        '
        'lblTitleKosu
        '
        Me.lblTitleKosu.AutoSize = True
        Me.lblTitleKosu.AutoSizeDef = True
        Me.lblTitleKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKosu.EnableStatus = False
        Me.lblTitleKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.HeightDef = 13
        Me.lblTitleKosu.Location = New System.Drawing.Point(296, 43)
        Me.lblTitleKosu.MinimumSize = New System.Drawing.Size(95, 0)
        Me.lblTitleKosu.Name = "lblTitleKosu"
        Me.lblTitleKosu.Size = New System.Drawing.Size(95, 13)
        Me.lblTitleKosu.TabIndex = 115
        Me.lblTitleKosu.Text = "個数"
        Me.lblTitleKosu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKosu.TextValue = "個数"
        Me.lblTitleKosu.WidthDef = 95
        '
        'lblKosuCnt
        '
        Me.lblKosuCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKosuCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKosuCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKosuCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKosuCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKosuCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKosuCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKosuCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKosuCnt.HeightDef = 18
        Me.lblKosuCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKosuCnt.HissuLabelVisible = False
        Me.lblKosuCnt.IsHissuCheck = False
        Me.lblKosuCnt.IsRangeCheck = False
        Me.lblKosuCnt.ItemName = ""
        Me.lblKosuCnt.Location = New System.Drawing.Point(395, 41)
        Me.lblKosuCnt.Name = "lblKosuCnt"
        Me.lblKosuCnt.ReadOnly = True
        Me.lblKosuCnt.Size = New System.Drawing.Size(110, 18)
        Me.lblKosuCnt.TabIndex = 11
        Me.lblKosuCnt.TabStop = False
        Me.lblKosuCnt.TabStopSetting = False
        Me.lblKosuCnt.TextValue = "0"
        Me.lblKosuCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblKosuCnt.WidthDef = 110
        '
        'lblTitleHikiSumiCnt
        '
        Me.lblTitleHikiSumiCnt.AutoSize = True
        Me.lblTitleHikiSumiCnt.AutoSizeDef = True
        Me.lblTitleHikiSumiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiSumiCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiSumiCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHikiSumiCnt.EnableStatus = False
        Me.lblTitleHikiSumiCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiSumiCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiSumiCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiSumiCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiSumiCnt.HeightDef = 13
        Me.lblTitleHikiSumiCnt.Location = New System.Drawing.Point(4, 65)
        Me.lblTitleHikiSumiCnt.MinimumSize = New System.Drawing.Size(84, 0)
        Me.lblTitleHikiSumiCnt.Name = "lblTitleHikiSumiCnt"
        Me.lblTitleHikiSumiCnt.Size = New System.Drawing.Size(84, 13)
        Me.lblTitleHikiSumiCnt.TabIndex = 111
        Me.lblTitleHikiSumiCnt.Text = "引当済個数"
        Me.lblTitleHikiSumiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHikiSumiCnt.TextValue = "引当済個数"
        Me.lblTitleHikiSumiCnt.WidthDef = 84
        '
        'lblHikiSumiCnt
        '
        Me.lblHikiSumiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHikiSumiCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiSumiCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiCnt.HeightDef = 18
        Me.lblHikiSumiCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiCnt.HissuLabelVisible = False
        Me.lblHikiSumiCnt.IsHissuCheck = False
        Me.lblHikiSumiCnt.IsRangeCheck = False
        Me.lblHikiSumiCnt.ItemName = ""
        Me.lblHikiSumiCnt.Location = New System.Drawing.Point(92, 62)
        Me.lblHikiSumiCnt.Name = "lblHikiSumiCnt"
        Me.lblHikiSumiCnt.ReadOnly = True
        Me.lblHikiSumiCnt.Size = New System.Drawing.Size(110, 18)
        Me.lblHikiSumiCnt.TabIndex = 12
        Me.lblHikiSumiCnt.TabStop = False
        Me.lblHikiSumiCnt.TabStopSetting = False
        Me.lblHikiSumiCnt.TextValue = "0"
        Me.lblHikiSumiCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblHikiSumiCnt.WidthDef = 110
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
        Me.lblTitleIrisu.Location = New System.Drawing.Point(4, 43)
        Me.lblTitleIrisu.MinimumSize = New System.Drawing.Size(84, 0)
        Me.lblTitleIrisu.Name = "lblTitleIrisu"
        Me.lblTitleIrisu.Size = New System.Drawing.Size(84, 13)
        Me.lblTitleIrisu.TabIndex = 109
        Me.lblTitleIrisu.Text = "入数"
        Me.lblTitleIrisu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrisu.TextValue = "入数"
        Me.lblTitleIrisu.WidthDef = 84
        '
        'lblIrisuCnt
        '
        Me.lblIrisuCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIrisuCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisuCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuCnt.HeightDef = 18
        Me.lblIrisuCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuCnt.HissuLabelVisible = False
        Me.lblIrisuCnt.IsHissuCheck = False
        Me.lblIrisuCnt.IsRangeCheck = False
        Me.lblIrisuCnt.ItemName = ""
        Me.lblIrisuCnt.Location = New System.Drawing.Point(92, 41)
        Me.lblIrisuCnt.Name = "lblIrisuCnt"
        Me.lblIrisuCnt.ReadOnly = True
        Me.lblIrisuCnt.Size = New System.Drawing.Size(110, 18)
        Me.lblIrisuCnt.TabIndex = 10
        Me.lblIrisuCnt.TabStop = False
        Me.lblIrisuCnt.TabStopSetting = False
        Me.lblIrisuCnt.TextValue = "0"
        Me.lblIrisuCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblIrisuCnt.WidthDef = 110
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
        Me.lblTitleHasu.Location = New System.Drawing.Point(296, 21)
        Me.lblTitleHasu.MinimumSize = New System.Drawing.Size(95, 0)
        Me.lblTitleHasu.Name = "lblTitleHasu"
        Me.lblTitleHasu.Size = New System.Drawing.Size(95, 13)
        Me.lblTitleHasu.TabIndex = 106
        Me.lblTitleHasu.Text = "端数"
        Me.lblTitleHasu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHasu.TextValue = "端数"
        Me.lblTitleHasu.WidthDef = 95
        '
        'numCnt
        '
        Me.numCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCnt.HeightDef = 18
        Me.numCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCnt.HissuLabelVisible = False
        Me.numCnt.IsHissuCheck = False
        Me.numCnt.IsRangeCheck = False
        Me.numCnt.ItemName = ""
        Me.numCnt.Location = New System.Drawing.Point(395, 19)
        Me.numCnt.Name = "numCnt"
        Me.numCnt.ReadOnly = False
        Me.numCnt.Size = New System.Drawing.Size(110, 18)
        Me.numCnt.TabIndex = 8
        Me.numCnt.TabStopSetting = True
        Me.numCnt.TextValue = "0"
        Me.numCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCnt.WidthDef = 110
        '
        'lblTitleIrimeTanni
        '
        Me.lblTitleIrimeTanni.AutoSize = True
        Me.lblTitleIrimeTanni.AutoSizeDef = True
        Me.lblTitleIrimeTanni.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeTanni.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeTanni.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrimeTanni.EnableStatus = False
        Me.lblTitleIrimeTanni.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeTanni.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeTanni.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeTanni.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeTanni.HeightDef = 13
        Me.lblTitleIrimeTanni.Location = New System.Drawing.Point(187, 130)
        Me.lblTitleIrimeTanni.MinimumSize = New System.Drawing.Size(80, 0)
        Me.lblTitleIrimeTanni.Name = "lblTitleIrimeTanni"
        Me.lblTitleIrimeTanni.Size = New System.Drawing.Size(80, 13)
        Me.lblTitleIrimeTanni.TabIndex = 231
        Me.lblTitleIrimeTanni.Text = "入目単位"
        Me.lblTitleIrimeTanni.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrimeTanni.TextValue = "入目単位"
        Me.lblTitleIrimeTanni.WidthDef = 80
        '
        'numIrime
        '
        Me.numIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.numIrime.Location = New System.Drawing.Point(98, 127)
        Me.numIrime.Name = "numIrime"
        Me.numIrime.ReadOnly = True
        Me.numIrime.Size = New System.Drawing.Size(98, 18)
        Me.numIrime.TabIndex = 229
        Me.numIrime.TabStop = False
        Me.numIrime.TabStopSetting = False
        Me.numIrime.TextValue = "0"
        Me.numIrime.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrime.WidthDef = 98
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
        Me.lblTitleIrime.Location = New System.Drawing.Point(4, 130)
        Me.lblTitleIrime.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleIrime.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleIrime.Name = "lblTitleIrime"
        Me.lblTitleIrime.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleIrime.TabIndex = 228
        Me.lblTitleIrime.Text = "入目"
        Me.lblTitleIrime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrime.TextValue = "入目"
        Me.lblTitleIrime.WidthDef = 90
        '
        'txtSerialNo
        '
        Me.txtSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerialNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerialNo.CountWrappedLine = False
        Me.txtSerialNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerialNo.HeightDef = 18
        Me.txtSerialNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerialNo.HissuLabelVisible = False
        Me.txtSerialNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtSerialNo.IsByteCheck = 40
        Me.txtSerialNo.IsCalendarCheck = False
        Me.txtSerialNo.IsDakutenCheck = False
        Me.txtSerialNo.IsEisuCheck = False
        Me.txtSerialNo.IsForbiddenWordsCheck = False
        Me.txtSerialNo.IsFullByteCheck = 0
        Me.txtSerialNo.IsHankakuCheck = False
        Me.txtSerialNo.IsHissuCheck = False
        Me.txtSerialNo.IsKanaCheck = False
        Me.txtSerialNo.IsMiddleSpace = False
        Me.txtSerialNo.IsNumericCheck = False
        Me.txtSerialNo.IsSujiCheck = False
        Me.txtSerialNo.IsZenkakuCheck = False
        Me.txtSerialNo.ItemName = ""
        Me.txtSerialNo.LineSpace = 0
        Me.txtSerialNo.Location = New System.Drawing.Point(401, 106)
        Me.txtSerialNo.MaxLength = 40
        Me.txtSerialNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerialNo.MaxLineCount = 0
        Me.txtSerialNo.Multiline = False
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.ReadOnly = False
        Me.txtSerialNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerialNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerialNo.Size = New System.Drawing.Size(221, 18)
        Me.txtSerialNo.TabIndex = 223
        Me.txtSerialNo.TabStopSetting = True
        Me.txtSerialNo.TextValue = ""
        Me.txtSerialNo.UseSystemPasswordChar = False
        Me.txtSerialNo.WidthDef = 221
        Me.txtSerialNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSerialNo
        '
        Me.lblTitleSerialNo.AutoSize = True
        Me.lblTitleSerialNo.AutoSizeDef = True
        Me.lblTitleSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSerialNo.EnableStatus = False
        Me.lblTitleSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.HeightDef = 13
        Me.lblTitleSerialNo.Location = New System.Drawing.Point(321, 109)
        Me.lblTitleSerialNo.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleSerialNo.Name = "lblTitleSerialNo"
        Me.lblTitleSerialNo.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleSerialNo.TabIndex = 222
        Me.lblTitleSerialNo.Text = "シリアル№"
        Me.lblTitleSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSerialNo.TextValue = "シリアル№"
        Me.lblTitleSerialNo.WidthDef = 78
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
        Me.txtLotNo.HissuLabelVisible = False
        Me.txtLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLotNo.IsByteCheck = 40
        Me.txtLotNo.IsCalendarCheck = False
        Me.txtLotNo.IsDakutenCheck = False
        Me.txtLotNo.IsEisuCheck = False
        Me.txtLotNo.IsForbiddenWordsCheck = False
        Me.txtLotNo.IsFullByteCheck = 0
        Me.txtLotNo.IsHankakuCheck = False
        Me.txtLotNo.IsHissuCheck = False
        Me.txtLotNo.IsKanaCheck = False
        Me.txtLotNo.IsMiddleSpace = False
        Me.txtLotNo.IsNumericCheck = False
        Me.txtLotNo.IsSujiCheck = False
        Me.txtLotNo.IsZenkakuCheck = False
        Me.txtLotNo.ItemName = ""
        Me.txtLotNo.LineSpace = 0
        Me.txtLotNo.Location = New System.Drawing.Point(98, 106)
        Me.txtLotNo.MaxLength = 40
        Me.txtLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLotNo.MaxLineCount = 0
        Me.txtLotNo.Multiline = False
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ReadOnly = False
        Me.txtLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLotNo.Size = New System.Drawing.Size(233, 18)
        Me.txtLotNo.TabIndex = 221
        Me.txtLotNo.TabStopSetting = True
        Me.txtLotNo.TextValue = ""
        Me.txtLotNo.UseSystemPasswordChar = False
        Me.txtLotNo.WidthDef = 233
        Me.txtLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleLotNo
        '
        Me.lblTitleLotNo.AutoSize = True
        Me.lblTitleLotNo.AutoSizeDef = True
        Me.lblTitleLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLotNo.EnableStatus = False
        Me.lblTitleLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.HeightDef = 13
        Me.lblTitleLotNo.Location = New System.Drawing.Point(4, 109)
        Me.lblTitleLotNo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleLotNo.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleLotNo.Name = "lblTitleLotNo"
        Me.lblTitleLotNo.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleLotNo.TabIndex = 220
        Me.lblTitleLotNo.Text = "ロット№"
        Me.lblTitleLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLotNo.TextValue = "ロット№"
        Me.lblTitleLotNo.WidthDef = 90
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
        Me.txtOrderNo.Location = New System.Drawing.Point(98, 64)
        Me.txtOrderNo.MaxLength = 30
        Me.txtOrderNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNo.MaxLineCount = 0
        Me.txtOrderNo.Multiline = False
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.ReadOnly = False
        Me.txtOrderNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNo.Size = New System.Drawing.Size(199, 18)
        Me.txtOrderNo.TabIndex = 216
        Me.txtOrderNo.TabStopSetting = True
        Me.txtOrderNo.TextValue = ""
        Me.txtOrderNo.UseSystemPasswordChar = False
        Me.txtOrderNo.WidthDef = 199
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
        Me.lblTitleOrderNo.Location = New System.Drawing.Point(4, 67)
        Me.lblTitleOrderNo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleOrderNo.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleOrderNo.Name = "lblTitleOrderNo"
        Me.lblTitleOrderNo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleOrderNo.TabIndex = 214
        Me.lblTitleOrderNo.Text = "オーダー番号"
        Me.lblTitleOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrderNo.TextValue = "オーダー番号"
        Me.lblTitleOrderNo.WidthDef = 91
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
        Me.lblCustNmM.HissuLabelVisible = False
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = False
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(179, 43)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(443, 18)
        Me.lblCustNmM.TabIndex = 211
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 443
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(4, 24)
        Me.lblTitleCust.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleCust.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleCust.TabIndex = 210
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 90
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
        Me.lblCustNmL.HissuLabelVisible = False
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = False
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(179, 22)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(443, 18)
        Me.lblCustNmL.TabIndex = 159
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 443
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdM.Location = New System.Drawing.Point(143, 43)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 158
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
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
        Me.txtCustCdL.Location = New System.Drawing.Point(98, 22)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 157
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnMotoDel
        '
        Me.btnMotoDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnMotoDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnMotoDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnMotoDel.EnableStatus = True
        Me.btnMotoDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMotoDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMotoDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMotoDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMotoDel.HeightDef = 22
        Me.btnMotoDel.Location = New System.Drawing.Point(46, 414)
        Me.btnMotoDel.Name = "btnMotoDel"
        Me.btnMotoDel.Size = New System.Drawing.Size(70, 22)
        Me.btnMotoDel.TabIndex = 276
        Me.btnMotoDel.TabStopSetting = True
        Me.btnMotoDel.Text = "行削除"
        Me.btnMotoDel.TextValue = "行削除"
        Me.btnMotoDel.UseVisualStyleBackColor = True
        Me.btnMotoDel.WidthDef = 70
        '
        'spdDtl
        '
        Me.spdDtl.AccessibleDescription = ""
        Me.spdDtl.AllowUserZoom = False
        Me.spdDtl.AutoImeMode = False
        Me.spdDtl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.spdDtl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.spdDtl.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.spdDtl.CellClickEventArgs = Nothing
        Me.spdDtl.CheckToCheckBox = True
        Me.spdDtl.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.spdDtl.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.spdDtl.EditModeReplace = True
        Me.spdDtl.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.spdDtl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl.ForeColorDef = System.Drawing.Color.Empty
        Me.spdDtl.HeightDef = 425
        Me.spdDtl.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spdDtl.HorizontalScrollBar.Name = ""
        Me.spdDtl.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.spdDtl.KeyboardCheckBoxOn = False
        Me.spdDtl.Location = New System.Drawing.Point(20, 439)
        Me.spdDtl.Name = "spdDtl"
        Me.spdDtl.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.spdDtl.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdDtl_Sheet1})
        Me.spdDtl.Size = New System.Drawing.Size(615, 425)
        Me.spdDtl.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.spdDtl.SortColumn = True
        Me.spdDtl.SpanColumnLock = True
        Me.spdDtl.SpreadDoubleClicked = False
        Me.spdDtl.TabIndex = 275
        Me.spdDtl.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.spdDtl.TextValue = Nothing
        Me.spdDtl.UseGrouping = False
        Me.spdDtl.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spdDtl.VerticalScrollBar.Name = ""
        Me.spdDtl.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.spdDtl.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdDtl.WidthDef = 615
        spdDtl_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        spdDtl_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        spdDtl_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.spdDtl.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, spdDtl_InputMapWhenFocusedNormal)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        spdDtl_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.spdDtl.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, spdDtl_InputMapWhenAncestorOfFocusedNormal)
        '
        'spdDtl_Sheet1
        '
        Me.spdDtl_Sheet1.Reset()
        Me.spdDtl_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdDtl_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdDtl_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.spdDtl_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.spdDtl_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.spdDtl_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.spdDtl_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdDtl_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.spdDtl_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.spdDtl_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.spdDtl_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.spdDtl_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.DefaultStyle.Locked = False
        Me.spdDtl_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.spdDtl_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.spdDtl_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.spdDtl_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.spdDtl_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.spdDtl_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.spdDtl_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.spdDtl_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.spdDtl_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdDtl_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.spdDtl_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.spdDtl_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.spdDtl_Sheet1.Rows.Default.Height = 18.0!
        Me.spdDtl_Sheet1.Rows.Default.Resizable = False
        Me.spdDtl_Sheet1.Rows.Default.Visible = True
        Me.spdDtl_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.spdDtl_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.spdDtl_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.spdDtl_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.spdDtl_Sheet1.SheetCornerStyle.Locked = True
        Me.spdDtl_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.spdDtl_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdDtl_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.spdDtl_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'lblTitleFurikaeKnariNo
        '
        Me.lblTitleFurikaeKnariNo.AutoSize = True
        Me.lblTitleFurikaeKnariNo.AutoSizeDef = True
        Me.lblTitleFurikaeKnariNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeKnariNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeKnariNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFurikaeKnariNo.EnableStatus = False
        Me.lblTitleFurikaeKnariNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeKnariNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeKnariNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeKnariNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeKnariNo.HeightDef = 13
        Me.lblTitleFurikaeKnariNo.Location = New System.Drawing.Point(13, 12)
        Me.lblTitleFurikaeKnariNo.MinimumSize = New System.Drawing.Size(91, 0)
        Me.lblTitleFurikaeKnariNo.Name = "lblTitleFurikaeKnariNo"
        Me.lblTitleFurikaeKnariNo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleFurikaeKnariNo.TabIndex = 208
        Me.lblTitleFurikaeKnariNo.Text = "振替管理番号"
        Me.lblTitleFurikaeKnariNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFurikaeKnariNo.TextValue = "振替管理番号"
        Me.lblTitleFurikaeKnariNo.WidthDef = 91
        '
        'lblTitleFurikaeKbn
        '
        Me.lblTitleFurikaeKbn.AutoSize = True
        Me.lblTitleFurikaeKbn.AutoSizeDef = True
        Me.lblTitleFurikaeKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFurikaeKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFurikaeKbn.EnableStatus = False
        Me.lblTitleFurikaeKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFurikaeKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFurikaeKbn.HeightDef = 13
        Me.lblTitleFurikaeKbn.Location = New System.Drawing.Point(13, 33)
        Me.lblTitleFurikaeKbn.MinimumSize = New System.Drawing.Size(91, 0)
        Me.lblTitleFurikaeKbn.Name = "lblTitleFurikaeKbn"
        Me.lblTitleFurikaeKbn.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleFurikaeKbn.TabIndex = 209
        Me.lblTitleFurikaeKbn.Text = "振替区分"
        Me.lblTitleFurikaeKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFurikaeKbn.TextValue = "振替区分"
        Me.lblTitleFurikaeKbn.WidthDef = 91
        '
        'lblFurikaeNo
        '
        Me.lblFurikaeNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFurikaeNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFurikaeNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFurikaeNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFurikaeNo.CountWrappedLine = False
        Me.lblFurikaeNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblFurikaeNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFurikaeNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFurikaeNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFurikaeNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFurikaeNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblFurikaeNo.HeightDef = 18
        Me.lblFurikaeNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFurikaeNo.HissuLabelVisible = False
        Me.lblFurikaeNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblFurikaeNo.IsByteCheck = 0
        Me.lblFurikaeNo.IsCalendarCheck = False
        Me.lblFurikaeNo.IsDakutenCheck = False
        Me.lblFurikaeNo.IsEisuCheck = False
        Me.lblFurikaeNo.IsForbiddenWordsCheck = False
        Me.lblFurikaeNo.IsFullByteCheck = 0
        Me.lblFurikaeNo.IsHankakuCheck = False
        Me.lblFurikaeNo.IsHissuCheck = False
        Me.lblFurikaeNo.IsKanaCheck = False
        Me.lblFurikaeNo.IsMiddleSpace = False
        Me.lblFurikaeNo.IsNumericCheck = False
        Me.lblFurikaeNo.IsSujiCheck = False
        Me.lblFurikaeNo.IsZenkakuCheck = False
        Me.lblFurikaeNo.ItemName = ""
        Me.lblFurikaeNo.LineSpace = 0
        Me.lblFurikaeNo.Location = New System.Drawing.Point(110, 9)
        Me.lblFurikaeNo.MaxLength = 0
        Me.lblFurikaeNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblFurikaeNo.MaxLineCount = 0
        Me.lblFurikaeNo.Multiline = False
        Me.lblFurikaeNo.Name = "lblFurikaeNo"
        Me.lblFurikaeNo.ReadOnly = True
        Me.lblFurikaeNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblFurikaeNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblFurikaeNo.Size = New System.Drawing.Size(98, 18)
        Me.lblFurikaeNo.TabIndex = 210
        Me.lblFurikaeNo.TabStop = False
        Me.lblFurikaeNo.TabStopSetting = False
        Me.lblFurikaeNo.TextValue = "XXXXXXXX"
        Me.lblFurikaeNo.UseSystemPasswordChar = False
        Me.lblFurikaeNo.WidthDef = 98
        Me.lblFurikaeNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbFurikaeKbn
        '
        Me.cmbFurikaeKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbFurikaeKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbFurikaeKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbFurikaeKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbFurikaeKbn.DataCode = "H004"
        Me.cmbFurikaeKbn.DataSource = Nothing
        Me.cmbFurikaeKbn.DisplayMember = Nothing
        Me.cmbFurikaeKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFurikaeKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFurikaeKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbFurikaeKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbFurikaeKbn.HeightDef = 18
        Me.cmbFurikaeKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbFurikaeKbn.HissuLabelVisible = True
        Me.cmbFurikaeKbn.InsertWildCard = True
        Me.cmbFurikaeKbn.IsForbiddenWordsCheck = False
        Me.cmbFurikaeKbn.IsHissuCheck = True
        Me.cmbFurikaeKbn.ItemName = ""
        Me.cmbFurikaeKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbFurikaeKbn.Location = New System.Drawing.Point(110, 30)
        Me.cmbFurikaeKbn.Name = "cmbFurikaeKbn"
        Me.cmbFurikaeKbn.ReadOnly = False
        Me.cmbFurikaeKbn.SelectedIndex = -1
        Me.cmbFurikaeKbn.SelectedItem = Nothing
        Me.cmbFurikaeKbn.SelectedText = ""
        Me.cmbFurikaeKbn.SelectedValue = ""
        Me.cmbFurikaeKbn.Size = New System.Drawing.Size(137, 18)
        Me.cmbFurikaeKbn.TabIndex = 212
        Me.cmbFurikaeKbn.TabStopSetting = True
        Me.cmbFurikaeKbn.TextValue = ""
        Me.cmbFurikaeKbn.Value1 = Nothing
        Me.cmbFurikaeKbn.Value2 = Nothing
        Me.cmbFurikaeKbn.Value3 = Nothing
        Me.cmbFurikaeKbn.ValueMember = Nothing
        Me.cmbFurikaeKbn.WidthDef = 137
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(268, 12)
        Me.lblTitleEigyo.MinimumSize = New System.Drawing.Size(120, 0)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(120, 13)
        Me.lblTitleEigyo.TabIndex = 214
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 120
        '
        'lblTitleSoko
        '
        Me.lblTitleSoko.AutoSize = True
        Me.lblTitleSoko.AutoSizeDef = True
        Me.lblTitleSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSoko.EnableStatus = False
        Me.lblTitleSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSoko.HeightDef = 13
        Me.lblTitleSoko.Location = New System.Drawing.Point(724, 12)
        Me.lblTitleSoko.MinimumSize = New System.Drawing.Size(160, 0)
        Me.lblTitleSoko.Name = "lblTitleSoko"
        Me.lblTitleSoko.Size = New System.Drawing.Size(160, 13)
        Me.lblTitleSoko.TabIndex = 216
        Me.lblTitleSoko.Text = "倉庫"
        Me.lblTitleSoko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSoko.TextValue = "倉庫"
        Me.lblTitleSoko.WidthDef = 160
        '
        'imdFurikaeDate
        '
        Me.imdFurikaeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFurikaeDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdFurikaeDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdFurikaeDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFurikaeDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFurikaeDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFurikaeDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdFurikaeDate.HeightDef = 18
        Me.imdFurikaeDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdFurikaeDate.HissuLabelVisible = True
        Me.imdFurikaeDate.Holiday = True
        Me.imdFurikaeDate.IsAfterDateCheck = False
        Me.imdFurikaeDate.IsBeforeDateCheck = False
        Me.imdFurikaeDate.IsHissuCheck = True
        Me.imdFurikaeDate.IsMinDateCheck = "1900/01/01"
        Me.imdFurikaeDate.ItemName = ""
        Me.imdFurikaeDate.Location = New System.Drawing.Point(393, 30)
        Me.imdFurikaeDate.Name = "imdFurikaeDate"
        Me.imdFurikaeDate.Number = CType(10101000000, Long)
        Me.imdFurikaeDate.ReadOnly = False
        Me.imdFurikaeDate.Size = New System.Drawing.Size(118, 18)
        Me.imdFurikaeDate.TabIndex = 218
        Me.imdFurikaeDate.TabStopSetting = True
        Me.imdFurikaeDate.TextValue = ""
        Me.imdFurikaeDate.Value = New Date(CType(0, Long))
        Me.imdFurikaeDate.WidthDef = 118
        '
        'lblTitleFruikaeDate
        '
        Me.lblTitleFruikaeDate.AutoSize = True
        Me.lblTitleFruikaeDate.AutoSizeDef = True
        Me.lblTitleFruikaeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFruikaeDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFruikaeDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFruikaeDate.EnableStatus = False
        Me.lblTitleFruikaeDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFruikaeDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFruikaeDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFruikaeDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFruikaeDate.HeightDef = 13
        Me.lblTitleFruikaeDate.Location = New System.Drawing.Point(268, 33)
        Me.lblTitleFruikaeDate.MinimumSize = New System.Drawing.Size(120, 0)
        Me.lblTitleFruikaeDate.Name = "lblTitleFruikaeDate"
        Me.lblTitleFruikaeDate.Size = New System.Drawing.Size(120, 13)
        Me.lblTitleFruikaeDate.TabIndex = 219
        Me.lblTitleFruikaeDate.Text = "振替日"
        Me.lblTitleFruikaeDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFruikaeDate.TextValue = "振替日"
        Me.lblTitleFruikaeDate.WidthDef = 120
        '
        'chkYoukiChange
        '
        Me.chkYoukiChange.AutoSize = True
        Me.chkYoukiChange.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYoukiChange.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYoukiChange.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkYoukiChange.EnableStatus = True
        Me.chkYoukiChange.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYoukiChange.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYoukiChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYoukiChange.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYoukiChange.HeightDef = 17
        Me.chkYoukiChange.Location = New System.Drawing.Point(557, 33)
        Me.chkYoukiChange.Name = "chkYoukiChange"
        Me.chkYoukiChange.Size = New System.Drawing.Size(152, 17)
        Me.chkYoukiChange.TabIndex = 220
        Me.chkYoukiChange.TabStopSetting = True
        Me.chkYoukiChange.Text = "容器変更（有／無）"
        Me.chkYoukiChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkYoukiChange.TextValue = "容器変更（有／無）"
        Me.chkYoukiChange.UseVisualStyleBackColor = True
        Me.chkYoukiChange.WidthDef = 152
        '
        'lblTitleToukiHokanKbn
        '
        Me.lblTitleToukiHokanKbn.AutoSize = True
        Me.lblTitleToukiHokanKbn.AutoSizeDef = True
        Me.lblTitleToukiHokanKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleToukiHokanKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleToukiHokanKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleToukiHokanKbn.EnableStatus = False
        Me.lblTitleToukiHokanKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleToukiHokanKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleToukiHokanKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleToukiHokanKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleToukiHokanKbn.HeightDef = 13
        Me.lblTitleToukiHokanKbn.Location = New System.Drawing.Point(724, 33)
        Me.lblTitleToukiHokanKbn.MinimumSize = New System.Drawing.Size(160, 0)
        Me.lblTitleToukiHokanKbn.Name = "lblTitleToukiHokanKbn"
        Me.lblTitleToukiHokanKbn.Size = New System.Drawing.Size(160, 13)
        Me.lblTitleToukiHokanKbn.TabIndex = 221
        Me.lblTitleToukiHokanKbn.Text = "当期保管料負担区分"
        Me.lblTitleToukiHokanKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleToukiHokanKbn.TextValue = "当期保管料負担区分"
        Me.lblTitleToukiHokanKbn.WidthDef = 160
        '
        'pnlFurikaeNew
        '
        Me.pnlFurikaeNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlFurikaeNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlFurikaeNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlFurikaeNew.Controls.Add(Me.lblKosuTanniNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblIrimeTanniNew)
        Me.pnlFurikaeNew.Controls.Add(Me.numIrimeNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleGoodsCdNrsNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblGoodsCdNrsNew)
        Me.pnlFurikaeNew.Controls.Add(Me.pnlNyukaRemark)
        Me.pnlFurikaeNew.Controls.Add(Me.cmbNiyakuNew)
        Me.pnlFurikaeNew.Controls.Add(Me.txtGoodsNmCustNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleNiyakuNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleGoodsNew)
        Me.pnlFurikaeNew.Controls.Add(Me.txtGoodsCdCustNew)
        Me.pnlFurikaeNew.Controls.Add(Me.chkInkoDateUmu)
        Me.pnlFurikaeNew.Controls.Add(Me.pnlHutaiSagyoNew)
        Me.pnlFurikaeNew.Controls.Add(Me.cmbTaxKbnNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleTaxKbnNew)
        Me.pnlFurikaeNew.Controls.Add(Me.txtDenpNo)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleDenpNo)
        Me.pnlFurikaeNew.Controls.Add(Me.lblCustNmMNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblTitleCustNew)
        Me.pnlFurikaeNew.Controls.Add(Me.lblCustNmLNew)
        Me.pnlFurikaeNew.Controls.Add(Me.txtCustCdMNew)
        Me.pnlFurikaeNew.Controls.Add(Me.txtCustCdLNew)
        Me.pnlFurikaeNew.EnableStatus = False
        Me.pnlFurikaeNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlFurikaeNew.FontDef = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnlFurikaeNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlFurikaeNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlFurikaeNew.HeightDef = 358
        Me.pnlFurikaeNew.Location = New System.Drawing.Point(641, 54)
        Me.pnlFurikaeNew.Name = "pnlFurikaeNew"
        Me.pnlFurikaeNew.Size = New System.Drawing.Size(625, 358)
        Me.pnlFurikaeNew.TabIndex = 222
        Me.pnlFurikaeNew.TabStop = False
        Me.pnlFurikaeNew.Text = "振替先ヘッダー情報"
        Me.pnlFurikaeNew.TextValue = "振替先ヘッダー情報"
        Me.pnlFurikaeNew.WidthDef = 625
        '
        'lblKosuTanniNew
        '
        Me.lblKosuTanniNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKosuTanniNew.DataCode = "K002"
        Me.lblKosuTanniNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKosuTanniNew.HissuLabelVisible = False
        Me.lblKosuTanniNew.KbnValue = ""
        Me.lblKosuTanniNew.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun.DISP_MEMBERS.KBN_NM1
        Me.lblKosuTanniNew.Location = New System.Drawing.Point(465, 111)
        Me.lblKosuTanniNew.Margin = New System.Windows.Forms.Padding(1)
        Me.lblKosuTanniNew.Name = "lblKosuTanniNew"
        Me.lblKosuTanniNew.Size = New System.Drawing.Size(119, 18)
        Me.lblKosuTanniNew.TabIndex = 289
        Me.lblKosuTanniNew.TabStop = False
        Me.lblKosuTanniNew.Value1 = Nothing
        Me.lblKosuTanniNew.Value2 = Nothing
        Me.lblKosuTanniNew.Value3 = Nothing
        Me.lblKosuTanniNew.Visible = False
        '
        'lblIrimeTanniNew
        '
        Me.lblIrimeTanniNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIrimeTanniNew.DataCode = "I001"
        Me.lblIrimeTanniNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeTanniNew.HissuLabelVisible = False
        Me.lblIrimeTanniNew.KbnValue = ""
        Me.lblIrimeTanniNew.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun.DISP_MEMBERS.KBN_NM1
        Me.lblIrimeTanniNew.Location = New System.Drawing.Point(362, 111)
        Me.lblIrimeTanniNew.Margin = New System.Windows.Forms.Padding(1)
        Me.lblIrimeTanniNew.Name = "lblIrimeTanniNew"
        Me.lblIrimeTanniNew.Size = New System.Drawing.Size(119, 18)
        Me.lblIrimeTanniNew.TabIndex = 288
        Me.lblIrimeTanniNew.TabStop = False
        Me.lblIrimeTanniNew.Value1 = Nothing
        Me.lblIrimeTanniNew.Value2 = Nothing
        Me.lblIrimeTanniNew.Value3 = Nothing
        Me.lblIrimeTanniNew.Visible = False
        '
        'numIrimeNew
        '
        Me.numIrimeNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrimeNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrimeNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrimeNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrimeNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrimeNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrimeNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrimeNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrimeNew.HeightDef = 18
        Me.numIrimeNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrimeNew.HissuLabelVisible = False
        Me.numIrimeNew.IsHissuCheck = False
        Me.numIrimeNew.IsRangeCheck = False
        Me.numIrimeNew.ItemName = ""
        Me.numIrimeNew.Location = New System.Drawing.Point(280, 111)
        Me.numIrimeNew.Name = "numIrimeNew"
        Me.numIrimeNew.ReadOnly = True
        Me.numIrimeNew.Size = New System.Drawing.Size(98, 18)
        Me.numIrimeNew.TabIndex = 287
        Me.numIrimeNew.TabStop = False
        Me.numIrimeNew.TabStopSetting = False
        Me.numIrimeNew.TextValue = "0"
        Me.numIrimeNew.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrimeNew.Visible = False
        Me.numIrimeNew.WidthDef = 98
        '
        'lblTitleGoodsCdNrsNew
        '
        Me.lblTitleGoodsCdNrsNew.AutoSize = True
        Me.lblTitleGoodsCdNrsNew.AutoSizeDef = True
        Me.lblTitleGoodsCdNrsNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCdNrsNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCdNrsNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsCdNrsNew.EnableStatus = False
        Me.lblTitleGoodsCdNrsNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCdNrsNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCdNrsNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCdNrsNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCdNrsNew.HeightDef = 13
        Me.lblTitleGoodsCdNrsNew.Location = New System.Drawing.Point(4, 111)
        Me.lblTitleGoodsCdNrsNew.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleGoodsCdNrsNew.Name = "lblTitleGoodsCdNrsNew"
        Me.lblTitleGoodsCdNrsNew.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleGoodsCdNrsNew.TabIndex = 286
        Me.lblTitleGoodsCdNrsNew.Text = "商品KEY"
        Me.lblTitleGoodsCdNrsNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsCdNrsNew.TextValue = "商品KEY"
        Me.lblTitleGoodsCdNrsNew.WidthDef = 78
        '
        'lblGoodsCdNrsNew
        '
        Me.lblGoodsCdNrsNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrsNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrsNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsCdNrsNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCdNrsNew.CountWrappedLine = False
        Me.lblGoodsCdNrsNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsCdNrsNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrsNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrsNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrsNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrsNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsCdNrsNew.HeightDef = 18
        Me.lblGoodsCdNrsNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrsNew.HissuLabelVisible = True
        Me.lblGoodsCdNrsNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsCdNrsNew.IsByteCheck = 0
        Me.lblGoodsCdNrsNew.IsCalendarCheck = False
        Me.lblGoodsCdNrsNew.IsDakutenCheck = False
        Me.lblGoodsCdNrsNew.IsEisuCheck = False
        Me.lblGoodsCdNrsNew.IsForbiddenWordsCheck = False
        Me.lblGoodsCdNrsNew.IsFullByteCheck = 0
        Me.lblGoodsCdNrsNew.IsHankakuCheck = False
        Me.lblGoodsCdNrsNew.IsHissuCheck = True
        Me.lblGoodsCdNrsNew.IsKanaCheck = False
        Me.lblGoodsCdNrsNew.IsMiddleSpace = False
        Me.lblGoodsCdNrsNew.IsNumericCheck = False
        Me.lblGoodsCdNrsNew.IsSujiCheck = False
        Me.lblGoodsCdNrsNew.IsZenkakuCheck = False
        Me.lblGoodsCdNrsNew.ItemName = ""
        Me.lblGoodsCdNrsNew.LineSpace = 0
        Me.lblGoodsCdNrsNew.Location = New System.Drawing.Point(89, 109)
        Me.lblGoodsCdNrsNew.MaxLength = 0
        Me.lblGoodsCdNrsNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsCdNrsNew.MaxLineCount = 0
        Me.lblGoodsCdNrsNew.Multiline = False
        Me.lblGoodsCdNrsNew.Name = "lblGoodsCdNrsNew"
        Me.lblGoodsCdNrsNew.ReadOnly = True
        Me.lblGoodsCdNrsNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsCdNrsNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsCdNrsNew.Size = New System.Drawing.Size(173, 18)
        Me.lblGoodsCdNrsNew.TabIndex = 285
        Me.lblGoodsCdNrsNew.TabStop = False
        Me.lblGoodsCdNrsNew.TabStopSetting = False
        Me.lblGoodsCdNrsNew.TextValue = "XXXXXXXXXXXXXXXXXXXX"
        Me.lblGoodsCdNrsNew.UseSystemPasswordChar = False
        Me.lblGoodsCdNrsNew.WidthDef = 173
        Me.lblGoodsCdNrsNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlNyukaRemark
        '
        Me.pnlNyukaRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNyukaRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNyukaRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlNyukaRemark.Controls.Add(Me.txtNyukaRemark)
        Me.pnlNyukaRemark.EnableStatus = False
        Me.pnlNyukaRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNyukaRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNyukaRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNyukaRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNyukaRemark.HeightDef = 88
        Me.pnlNyukaRemark.Location = New System.Drawing.Point(212, 261)
        Me.pnlNyukaRemark.Name = "pnlNyukaRemark"
        Me.pnlNyukaRemark.Size = New System.Drawing.Size(407, 88)
        Me.pnlNyukaRemark.TabIndex = 277
        Me.pnlNyukaRemark.TabStop = False
        Me.pnlNyukaRemark.Text = "入荷時注意事項"
        Me.pnlNyukaRemark.TextValue = "入荷時注意事項"
        Me.pnlNyukaRemark.WidthDef = 407
        '
        'txtNyukaRemark
        '
        Me.txtNyukaRemark.AutoScroll = True
        Me.txtNyukaRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyukaRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyukaRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNyukaRemark.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtNyukaRemark.CountWrappedLine = False
        Me.txtNyukaRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNyukaRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukaRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukaRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyukaRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNyukaRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNyukaRemark.HeightDef = 60
        Me.txtNyukaRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNyukaRemark.HissuLabelVisible = False
        Me.txtNyukaRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtNyukaRemark.IsByteCheck = 100
        Me.txtNyukaRemark.IsCalendarCheck = False
        Me.txtNyukaRemark.IsDakutenCheck = False
        Me.txtNyukaRemark.IsEisuCheck = False
        Me.txtNyukaRemark.IsForbiddenWordsCheck = False
        Me.txtNyukaRemark.IsFullByteCheck = 0
        Me.txtNyukaRemark.IsHankakuCheck = False
        Me.txtNyukaRemark.IsHissuCheck = False
        Me.txtNyukaRemark.IsKanaCheck = False
        Me.txtNyukaRemark.IsMiddleSpace = False
        Me.txtNyukaRemark.IsNumericCheck = False
        Me.txtNyukaRemark.IsSujiCheck = False
        Me.txtNyukaRemark.IsZenkakuCheck = False
        Me.txtNyukaRemark.ItemName = ""
        Me.txtNyukaRemark.LineSpace = 0
        Me.txtNyukaRemark.Location = New System.Drawing.Point(9, 19)
        Me.txtNyukaRemark.MaxLength = 100
        Me.txtNyukaRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNyukaRemark.MaxLineCount = 0
        Me.txtNyukaRemark.Multiline = True
        Me.txtNyukaRemark.Name = "txtNyukaRemark"
        Me.txtNyukaRemark.ReadOnly = False
        Me.txtNyukaRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNyukaRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNyukaRemark.Size = New System.Drawing.Size(395, 60)
        Me.txtNyukaRemark.TabIndex = 193
        Me.txtNyukaRemark.TabStopSetting = True
        Me.txtNyukaRemark.TextValue = ""
        Me.txtNyukaRemark.UseSystemPasswordChar = False
        Me.txtNyukaRemark.WidthDef = 395
        Me.txtNyukaRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbNiyakuNew
        '
        Me.cmbNiyakuNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyakuNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNiyakuNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNiyakuNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNiyakuNew.DataCode = "U009"
        Me.cmbNiyakuNew.DataSource = Nothing
        Me.cmbNiyakuNew.DisplayMember = Nothing
        Me.cmbNiyakuNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyakuNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNiyakuNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyakuNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNiyakuNew.HeightDef = 18
        Me.cmbNiyakuNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNiyakuNew.HissuLabelVisible = True
        Me.cmbNiyakuNew.InsertWildCard = True
        Me.cmbNiyakuNew.IsForbiddenWordsCheck = False
        Me.cmbNiyakuNew.IsHissuCheck = True
        Me.cmbNiyakuNew.ItemName = ""
        Me.cmbNiyakuNew.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNiyakuNew.Location = New System.Drawing.Point(89, 148)
        Me.cmbNiyakuNew.Name = "cmbNiyakuNew"
        Me.cmbNiyakuNew.ReadOnly = False
        Me.cmbNiyakuNew.SelectedIndex = -1
        Me.cmbNiyakuNew.SelectedItem = Nothing
        Me.cmbNiyakuNew.SelectedText = ""
        Me.cmbNiyakuNew.SelectedValue = ""
        Me.cmbNiyakuNew.Size = New System.Drawing.Size(83, 18)
        Me.cmbNiyakuNew.TabIndex = 276
        Me.cmbNiyakuNew.TabStopSetting = True
        Me.cmbNiyakuNew.TextValue = ""
        Me.cmbNiyakuNew.Value1 = Nothing
        Me.cmbNiyakuNew.Value2 = Nothing
        Me.cmbNiyakuNew.Value3 = Nothing
        Me.cmbNiyakuNew.ValueMember = Nothing
        Me.cmbNiyakuNew.WidthDef = 83
        '
        'txtGoodsNmCustNew
        '
        Me.txtGoodsNmCustNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNmCustNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNmCustNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsNmCustNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsNmCustNew.CountWrappedLine = False
        Me.txtGoodsNmCustNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsNmCustNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNmCustNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNmCustNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNmCustNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNmCustNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsNmCustNew.HeightDef = 18
        Me.txtGoodsNmCustNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNmCustNew.HissuLabelVisible = False
        Me.txtGoodsNmCustNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsNmCustNew.IsByteCheck = 60
        Me.txtGoodsNmCustNew.IsCalendarCheck = False
        Me.txtGoodsNmCustNew.IsDakutenCheck = False
        Me.txtGoodsNmCustNew.IsEisuCheck = False
        Me.txtGoodsNmCustNew.IsForbiddenWordsCheck = False
        Me.txtGoodsNmCustNew.IsFullByteCheck = 0
        Me.txtGoodsNmCustNew.IsHankakuCheck = False
        Me.txtGoodsNmCustNew.IsHissuCheck = False
        Me.txtGoodsNmCustNew.IsKanaCheck = False
        Me.txtGoodsNmCustNew.IsMiddleSpace = False
        Me.txtGoodsNmCustNew.IsNumericCheck = False
        Me.txtGoodsNmCustNew.IsSujiCheck = False
        Me.txtGoodsNmCustNew.IsZenkakuCheck = False
        Me.txtGoodsNmCustNew.ItemName = ""
        Me.txtGoodsNmCustNew.LineSpace = 0
        Me.txtGoodsNmCustNew.Location = New System.Drawing.Point(232, 85)
        Me.txtGoodsNmCustNew.MaxLength = 60
        Me.txtGoodsNmCustNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNmCustNew.MaxLineCount = 0
        Me.txtGoodsNmCustNew.Multiline = False
        Me.txtGoodsNmCustNew.Name = "txtGoodsNmCustNew"
        Me.txtGoodsNmCustNew.ReadOnly = False
        Me.txtGoodsNmCustNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNmCustNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNmCustNew.Size = New System.Drawing.Size(333, 18)
        Me.txtGoodsNmCustNew.TabIndex = 275
        Me.txtGoodsNmCustNew.TabStopSetting = True
        Me.txtGoodsNmCustNew.TextValue = ""
        Me.txtGoodsNmCustNew.UseSystemPasswordChar = False
        Me.txtGoodsNmCustNew.WidthDef = 333
        Me.txtGoodsNmCustNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNiyakuNew
        '
        Me.lblTitleNiyakuNew.AutoSize = True
        Me.lblTitleNiyakuNew.AutoSizeDef = True
        Me.lblTitleNiyakuNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuNew.EnableStatus = False
        Me.lblTitleNiyakuNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuNew.HeightDef = 13
        Me.lblTitleNiyakuNew.Location = New System.Drawing.Point(4, 151)
        Me.lblTitleNiyakuNew.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleNiyakuNew.Name = "lblTitleNiyakuNew"
        Me.lblTitleNiyakuNew.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleNiyakuNew.TabIndex = 275
        Me.lblTitleNiyakuNew.Text = "荷役料"
        Me.lblTitleNiyakuNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuNew.TextValue = "荷役料"
        Me.lblTitleNiyakuNew.WidthDef = 78
        '
        'lblTitleGoodsNew
        '
        Me.lblTitleGoodsNew.AutoSize = True
        Me.lblTitleGoodsNew.AutoSizeDef = True
        Me.lblTitleGoodsNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsNew.EnableStatus = False
        Me.lblTitleGoodsNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNew.HeightDef = 13
        Me.lblTitleGoodsNew.Location = New System.Drawing.Point(4, 87)
        Me.lblTitleGoodsNew.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleGoodsNew.Name = "lblTitleGoodsNew"
        Me.lblTitleGoodsNew.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleGoodsNew.TabIndex = 274
        Me.lblTitleGoodsNew.Text = "商品"
        Me.lblTitleGoodsNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsNew.TextValue = "商品"
        Me.lblTitleGoodsNew.WidthDef = 78
        '
        'txtGoodsCdCustNew
        '
        Me.txtGoodsCdCustNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCustNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCustNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdCustNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdCustNew.CountWrappedLine = False
        Me.txtGoodsCdCustNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdCustNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCustNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCustNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCustNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCustNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdCustNew.HeightDef = 18
        Me.txtGoodsCdCustNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCustNew.HissuLabelVisible = False
        Me.txtGoodsCdCustNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsCdCustNew.IsByteCheck = 20
        Me.txtGoodsCdCustNew.IsCalendarCheck = False
        Me.txtGoodsCdCustNew.IsDakutenCheck = False
        Me.txtGoodsCdCustNew.IsEisuCheck = False
        Me.txtGoodsCdCustNew.IsForbiddenWordsCheck = False
        Me.txtGoodsCdCustNew.IsFullByteCheck = 0
        Me.txtGoodsCdCustNew.IsHankakuCheck = False
        Me.txtGoodsCdCustNew.IsHissuCheck = False
        Me.txtGoodsCdCustNew.IsKanaCheck = False
        Me.txtGoodsCdCustNew.IsMiddleSpace = False
        Me.txtGoodsCdCustNew.IsNumericCheck = False
        Me.txtGoodsCdCustNew.IsSujiCheck = False
        Me.txtGoodsCdCustNew.IsZenkakuCheck = False
        Me.txtGoodsCdCustNew.ItemName = ""
        Me.txtGoodsCdCustNew.LineSpace = 0
        Me.txtGoodsCdCustNew.Location = New System.Drawing.Point(89, 85)
        Me.txtGoodsCdCustNew.MaxLength = 20
        Me.txtGoodsCdCustNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdCustNew.MaxLineCount = 0
        Me.txtGoodsCdCustNew.Multiline = False
        Me.txtGoodsCdCustNew.Name = "txtGoodsCdCustNew"
        Me.txtGoodsCdCustNew.ReadOnly = False
        Me.txtGoodsCdCustNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdCustNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdCustNew.Size = New System.Drawing.Size(159, 18)
        Me.txtGoodsCdCustNew.TabIndex = 273
        Me.txtGoodsCdCustNew.TabStopSetting = True
        Me.txtGoodsCdCustNew.TextValue = "XXXXXXXXXXXXXXXXXXXZ"
        Me.txtGoodsCdCustNew.UseSystemPasswordChar = False
        Me.txtGoodsCdCustNew.WidthDef = 159
        Me.txtGoodsCdCustNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'chkInkoDateUmu
        '
        Me.chkInkoDateUmu.AutoSize = True
        Me.chkInkoDateUmu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkInkoDateUmu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkInkoDateUmu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkInkoDateUmu.Checked = True
        Me.chkInkoDateUmu.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInkoDateUmu.EnableStatus = True
        Me.chkInkoDateUmu.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.75!)
        Me.chkInkoDateUmu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 12.75!)
        Me.chkInkoDateUmu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkInkoDateUmu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkInkoDateUmu.HeightDef = 21
        Me.chkInkoDateUmu.Location = New System.Drawing.Point(89, 180)
        Me.chkInkoDateUmu.Name = "chkInkoDateUmu"
        Me.chkInkoDateUmu.Size = New System.Drawing.Size(351, 21)
        Me.chkInkoDateUmu.TabIndex = 240
        Me.chkInkoDateUmu.TabStopSetting = True
        Me.chkInkoDateUmu.Text = "初期入荷日を引き継いで在庫を振替える"
        Me.chkInkoDateUmu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkInkoDateUmu.TextValue = "初期入荷日を引き継いで在庫を振替える"
        Me.chkInkoDateUmu.UseVisualStyleBackColor = True
        Me.chkInkoDateUmu.WidthDef = 351
        '
        'pnlHutaiSagyoNew
        '
        Me.pnlHutaiSagyoNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHutaiSagyoNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHutaiSagyoNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblSagyoNmN1)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblSagyoNmN2)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblSagyoNmN3)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblTitleSagyo1New)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.txtSagyoCdN1)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblTitleSagyo3New)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.txtSagyoCdN2)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.lblTitleSagyo2New)
        Me.pnlHutaiSagyoNew.Controls.Add(Me.txtSagyoCdN3)
        Me.pnlHutaiSagyoNew.EnableStatus = False
        Me.pnlHutaiSagyoNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHutaiSagyoNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHutaiSagyoNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHutaiSagyoNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHutaiSagyoNew.HeightDef = 88
        Me.pnlHutaiSagyoNew.Location = New System.Drawing.Point(6, 261)
        Me.pnlHutaiSagyoNew.Name = "pnlHutaiSagyoNew"
        Me.pnlHutaiSagyoNew.Size = New System.Drawing.Size(202, 88)
        Me.pnlHutaiSagyoNew.TabIndex = 237
        Me.pnlHutaiSagyoNew.TabStop = False
        Me.pnlHutaiSagyoNew.Text = "付帯作業"
        Me.pnlHutaiSagyoNew.TextValue = "付帯作業"
        Me.pnlHutaiSagyoNew.WidthDef = 202
        '
        'lblSagyoNmN1
        '
        Me.lblSagyoNmN1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmN1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmN1.CountWrappedLine = False
        Me.lblSagyoNmN1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmN1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmN1.HeightDef = 18
        Me.lblSagyoNmN1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN1.HissuLabelVisible = False
        Me.lblSagyoNmN1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmN1.IsByteCheck = 0
        Me.lblSagyoNmN1.IsCalendarCheck = False
        Me.lblSagyoNmN1.IsDakutenCheck = False
        Me.lblSagyoNmN1.IsEisuCheck = False
        Me.lblSagyoNmN1.IsForbiddenWordsCheck = False
        Me.lblSagyoNmN1.IsFullByteCheck = 0
        Me.lblSagyoNmN1.IsHankakuCheck = False
        Me.lblSagyoNmN1.IsHissuCheck = False
        Me.lblSagyoNmN1.IsKanaCheck = False
        Me.lblSagyoNmN1.IsMiddleSpace = False
        Me.lblSagyoNmN1.IsNumericCheck = False
        Me.lblSagyoNmN1.IsSujiCheck = False
        Me.lblSagyoNmN1.IsZenkakuCheck = False
        Me.lblSagyoNmN1.ItemName = ""
        Me.lblSagyoNmN1.LineSpace = 0
        Me.lblSagyoNmN1.Location = New System.Drawing.Point(88, 19)
        Me.lblSagyoNmN1.MaxLength = 0
        Me.lblSagyoNmN1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmN1.MaxLineCount = 0
        Me.lblSagyoNmN1.Multiline = False
        Me.lblSagyoNmN1.Name = "lblSagyoNmN1"
        Me.lblSagyoNmN1.ReadOnly = True
        Me.lblSagyoNmN1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmN1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmN1.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmN1.TabIndex = 194
        Me.lblSagyoNmN1.TabStop = False
        Me.lblSagyoNmN1.TabStopSetting = False
        Me.lblSagyoNmN1.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoNmN1.UseSystemPasswordChar = False
        Me.lblSagyoNmN1.WidthDef = 110
        Me.lblSagyoNmN1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoNmN2
        '
        Me.lblSagyoNmN2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmN2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmN2.CountWrappedLine = False
        Me.lblSagyoNmN2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmN2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmN2.HeightDef = 18
        Me.lblSagyoNmN2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN2.HissuLabelVisible = False
        Me.lblSagyoNmN2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmN2.IsByteCheck = 0
        Me.lblSagyoNmN2.IsCalendarCheck = False
        Me.lblSagyoNmN2.IsDakutenCheck = False
        Me.lblSagyoNmN2.IsEisuCheck = False
        Me.lblSagyoNmN2.IsForbiddenWordsCheck = False
        Me.lblSagyoNmN2.IsFullByteCheck = 0
        Me.lblSagyoNmN2.IsHankakuCheck = False
        Me.lblSagyoNmN2.IsHissuCheck = False
        Me.lblSagyoNmN2.IsKanaCheck = False
        Me.lblSagyoNmN2.IsMiddleSpace = False
        Me.lblSagyoNmN2.IsNumericCheck = False
        Me.lblSagyoNmN2.IsSujiCheck = False
        Me.lblSagyoNmN2.IsZenkakuCheck = False
        Me.lblSagyoNmN2.ItemName = ""
        Me.lblSagyoNmN2.LineSpace = 0
        Me.lblSagyoNmN2.Location = New System.Drawing.Point(88, 41)
        Me.lblSagyoNmN2.MaxLength = 0
        Me.lblSagyoNmN2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmN2.MaxLineCount = 0
        Me.lblSagyoNmN2.Multiline = False
        Me.lblSagyoNmN2.Name = "lblSagyoNmN2"
        Me.lblSagyoNmN2.ReadOnly = True
        Me.lblSagyoNmN2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmN2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmN2.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmN2.TabIndex = 197
        Me.lblSagyoNmN2.TabStop = False
        Me.lblSagyoNmN2.TabStopSetting = False
        Me.lblSagyoNmN2.TextValue = ""
        Me.lblSagyoNmN2.UseSystemPasswordChar = False
        Me.lblSagyoNmN2.WidthDef = 110
        Me.lblSagyoNmN2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoNmN3
        '
        Me.lblSagyoNmN3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoNmN3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoNmN3.CountWrappedLine = False
        Me.lblSagyoNmN3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoNmN3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNmN3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNmN3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoNmN3.HeightDef = 18
        Me.lblSagyoNmN3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNmN3.HissuLabelVisible = False
        Me.lblSagyoNmN3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoNmN3.IsByteCheck = 0
        Me.lblSagyoNmN3.IsCalendarCheck = False
        Me.lblSagyoNmN3.IsDakutenCheck = False
        Me.lblSagyoNmN3.IsEisuCheck = False
        Me.lblSagyoNmN3.IsForbiddenWordsCheck = False
        Me.lblSagyoNmN3.IsFullByteCheck = 0
        Me.lblSagyoNmN3.IsHankakuCheck = False
        Me.lblSagyoNmN3.IsHissuCheck = False
        Me.lblSagyoNmN3.IsKanaCheck = False
        Me.lblSagyoNmN3.IsMiddleSpace = False
        Me.lblSagyoNmN3.IsNumericCheck = False
        Me.lblSagyoNmN3.IsSujiCheck = False
        Me.lblSagyoNmN3.IsZenkakuCheck = False
        Me.lblSagyoNmN3.ItemName = ""
        Me.lblSagyoNmN3.LineSpace = 0
        Me.lblSagyoNmN3.Location = New System.Drawing.Point(88, 62)
        Me.lblSagyoNmN3.MaxLength = 0
        Me.lblSagyoNmN3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoNmN3.MaxLineCount = 0
        Me.lblSagyoNmN3.Multiline = False
        Me.lblSagyoNmN3.Name = "lblSagyoNmN3"
        Me.lblSagyoNmN3.ReadOnly = True
        Me.lblSagyoNmN3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoNmN3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoNmN3.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoNmN3.TabIndex = 200
        Me.lblSagyoNmN3.TabStop = False
        Me.lblSagyoNmN3.TabStopSetting = False
        Me.lblSagyoNmN3.TextValue = ""
        Me.lblSagyoNmN3.UseSystemPasswordChar = False
        Me.lblSagyoNmN3.WidthDef = 110
        Me.lblSagyoNmN3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo1New
        '
        Me.lblTitleSagyo1New.AutoSize = True
        Me.lblTitleSagyo1New.AutoSizeDef = True
        Me.lblTitleSagyo1New.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1New.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1New.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo1New.EnableStatus = False
        Me.lblTitleSagyo1New.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1New.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1New.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1New.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1New.HeightDef = 13
        Me.lblTitleSagyo1New.Location = New System.Drawing.Point(9, 21)
        Me.lblTitleSagyo1New.Name = "lblTitleSagyo1New"
        Me.lblTitleSagyo1New.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo1New.TabIndex = 215
        Me.lblTitleSagyo1New.Text = "①"
        Me.lblTitleSagyo1New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo1New.TextValue = "①"
        Me.lblTitleSagyo1New.WidthDef = 21
        '
        'txtSagyoCdN1
        '
        Me.txtSagyoCdN1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdN1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdN1.CountWrappedLine = False
        Me.txtSagyoCdN1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdN1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdN1.HeightDef = 18
        Me.txtSagyoCdN1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdN1.HissuLabelVisible = False
        Me.txtSagyoCdN1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdN1.IsByteCheck = 5
        Me.txtSagyoCdN1.IsCalendarCheck = False
        Me.txtSagyoCdN1.IsDakutenCheck = False
        Me.txtSagyoCdN1.IsEisuCheck = False
        Me.txtSagyoCdN1.IsForbiddenWordsCheck = False
        Me.txtSagyoCdN1.IsFullByteCheck = 0
        Me.txtSagyoCdN1.IsHankakuCheck = False
        Me.txtSagyoCdN1.IsHissuCheck = False
        Me.txtSagyoCdN1.IsKanaCheck = False
        Me.txtSagyoCdN1.IsMiddleSpace = False
        Me.txtSagyoCdN1.IsNumericCheck = False
        Me.txtSagyoCdN1.IsSujiCheck = False
        Me.txtSagyoCdN1.IsZenkakuCheck = False
        Me.txtSagyoCdN1.ItemName = ""
        Me.txtSagyoCdN1.LineSpace = 0
        Me.txtSagyoCdN1.Location = New System.Drawing.Point(36, 19)
        Me.txtSagyoCdN1.MaxLength = 5
        Me.txtSagyoCdN1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdN1.MaxLineCount = 0
        Me.txtSagyoCdN1.Multiline = False
        Me.txtSagyoCdN1.Name = "txtSagyoCdN1"
        Me.txtSagyoCdN1.ReadOnly = False
        Me.txtSagyoCdN1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdN1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdN1.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdN1.TabIndex = 193
        Me.txtSagyoCdN1.TabStopSetting = True
        Me.txtSagyoCdN1.TextValue = "XXXXX"
        Me.txtSagyoCdN1.UseSystemPasswordChar = False
        Me.txtSagyoCdN1.WidthDef = 68
        Me.txtSagyoCdN1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo3New
        '
        Me.lblTitleSagyo3New.AutoSize = True
        Me.lblTitleSagyo3New.AutoSizeDef = True
        Me.lblTitleSagyo3New.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3New.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3New.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo3New.EnableStatus = False
        Me.lblTitleSagyo3New.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3New.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3New.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3New.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3New.HeightDef = 13
        Me.lblTitleSagyo3New.Location = New System.Drawing.Point(9, 65)
        Me.lblTitleSagyo3New.Name = "lblTitleSagyo3New"
        Me.lblTitleSagyo3New.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo3New.TabIndex = 217
        Me.lblTitleSagyo3New.Text = "③"
        Me.lblTitleSagyo3New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo3New.TextValue = "③"
        Me.lblTitleSagyo3New.WidthDef = 21
        '
        'txtSagyoCdN2
        '
        Me.txtSagyoCdN2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdN2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdN2.CountWrappedLine = False
        Me.txtSagyoCdN2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdN2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdN2.HeightDef = 18
        Me.txtSagyoCdN2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdN2.HissuLabelVisible = False
        Me.txtSagyoCdN2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdN2.IsByteCheck = 5
        Me.txtSagyoCdN2.IsCalendarCheck = False
        Me.txtSagyoCdN2.IsDakutenCheck = False
        Me.txtSagyoCdN2.IsEisuCheck = False
        Me.txtSagyoCdN2.IsForbiddenWordsCheck = False
        Me.txtSagyoCdN2.IsFullByteCheck = 0
        Me.txtSagyoCdN2.IsHankakuCheck = False
        Me.txtSagyoCdN2.IsHissuCheck = False
        Me.txtSagyoCdN2.IsKanaCheck = False
        Me.txtSagyoCdN2.IsMiddleSpace = False
        Me.txtSagyoCdN2.IsNumericCheck = False
        Me.txtSagyoCdN2.IsSujiCheck = False
        Me.txtSagyoCdN2.IsZenkakuCheck = False
        Me.txtSagyoCdN2.ItemName = ""
        Me.txtSagyoCdN2.LineSpace = 0
        Me.txtSagyoCdN2.Location = New System.Drawing.Point(36, 41)
        Me.txtSagyoCdN2.MaxLength = 5
        Me.txtSagyoCdN2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdN2.MaxLineCount = 0
        Me.txtSagyoCdN2.Multiline = False
        Me.txtSagyoCdN2.Name = "txtSagyoCdN2"
        Me.txtSagyoCdN2.ReadOnly = False
        Me.txtSagyoCdN2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdN2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdN2.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdN2.TabIndex = 196
        Me.txtSagyoCdN2.TabStopSetting = True
        Me.txtSagyoCdN2.TextValue = ""
        Me.txtSagyoCdN2.UseSystemPasswordChar = False
        Me.txtSagyoCdN2.WidthDef = 68
        Me.txtSagyoCdN2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo2New
        '
        Me.lblTitleSagyo2New.AutoSize = True
        Me.lblTitleSagyo2New.AutoSizeDef = True
        Me.lblTitleSagyo2New.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2New.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2New.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo2New.EnableStatus = False
        Me.lblTitleSagyo2New.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2New.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2New.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2New.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2New.HeightDef = 13
        Me.lblTitleSagyo2New.Location = New System.Drawing.Point(9, 43)
        Me.lblTitleSagyo2New.Name = "lblTitleSagyo2New"
        Me.lblTitleSagyo2New.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSagyo2New.TabIndex = 216
        Me.lblTitleSagyo2New.Text = "②"
        Me.lblTitleSagyo2New.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo2New.TextValue = "②"
        Me.lblTitleSagyo2New.WidthDef = 21
        '
        'txtSagyoCdN3
        '
        Me.txtSagyoCdN3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoCdN3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCdN3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCdN3.CountWrappedLine = False
        Me.txtSagyoCdN3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCdN3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCdN3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCdN3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCdN3.HeightDef = 18
        Me.txtSagyoCdN3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCdN3.HissuLabelVisible = False
        Me.txtSagyoCdN3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyoCdN3.IsByteCheck = 5
        Me.txtSagyoCdN3.IsCalendarCheck = False
        Me.txtSagyoCdN3.IsDakutenCheck = False
        Me.txtSagyoCdN3.IsEisuCheck = False
        Me.txtSagyoCdN3.IsForbiddenWordsCheck = False
        Me.txtSagyoCdN3.IsFullByteCheck = 0
        Me.txtSagyoCdN3.IsHankakuCheck = False
        Me.txtSagyoCdN3.IsHissuCheck = False
        Me.txtSagyoCdN3.IsKanaCheck = False
        Me.txtSagyoCdN3.IsMiddleSpace = False
        Me.txtSagyoCdN3.IsNumericCheck = False
        Me.txtSagyoCdN3.IsSujiCheck = False
        Me.txtSagyoCdN3.IsZenkakuCheck = False
        Me.txtSagyoCdN3.ItemName = ""
        Me.txtSagyoCdN3.LineSpace = 0
        Me.txtSagyoCdN3.Location = New System.Drawing.Point(36, 62)
        Me.txtSagyoCdN3.MaxLength = 5
        Me.txtSagyoCdN3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCdN3.MaxLineCount = 0
        Me.txtSagyoCdN3.Multiline = False
        Me.txtSagyoCdN3.Name = "txtSagyoCdN3"
        Me.txtSagyoCdN3.ReadOnly = False
        Me.txtSagyoCdN3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCdN3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCdN3.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyoCdN3.TabIndex = 199
        Me.txtSagyoCdN3.TabStopSetting = True
        Me.txtSagyoCdN3.TextValue = ""
        Me.txtSagyoCdN3.UseSystemPasswordChar = False
        Me.txtSagyoCdN3.WidthDef = 68
        Me.txtSagyoCdN3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbTaxKbnNew
        '
        Me.cmbTaxKbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTaxKbnNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTaxKbnNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTaxKbnNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTaxKbnNew.DataCode = "Z001"
        Me.cmbTaxKbnNew.DataSource = Nothing
        Me.cmbTaxKbnNew.DisplayMember = Nothing
        Me.cmbTaxKbnNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKbnNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKbnNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKbnNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKbnNew.HeightDef = 18
        Me.cmbTaxKbnNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKbnNew.HissuLabelVisible = True
        Me.cmbTaxKbnNew.InsertWildCard = True
        Me.cmbTaxKbnNew.IsForbiddenWordsCheck = False
        Me.cmbTaxKbnNew.IsHissuCheck = True
        Me.cmbTaxKbnNew.ItemName = ""
        Me.cmbTaxKbnNew.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTaxKbnNew.Location = New System.Drawing.Point(268, 148)
        Me.cmbTaxKbnNew.Name = "cmbTaxKbnNew"
        Me.cmbTaxKbnNew.ReadOnly = False
        Me.cmbTaxKbnNew.SelectedIndex = -1
        Me.cmbTaxKbnNew.SelectedItem = Nothing
        Me.cmbTaxKbnNew.SelectedText = ""
        Me.cmbTaxKbnNew.SelectedValue = ""
        Me.cmbTaxKbnNew.Size = New System.Drawing.Size(83, 18)
        Me.cmbTaxKbnNew.TabIndex = 233
        Me.cmbTaxKbnNew.TabStopSetting = True
        Me.cmbTaxKbnNew.TextValue = ""
        Me.cmbTaxKbnNew.Value1 = Nothing
        Me.cmbTaxKbnNew.Value2 = Nothing
        Me.cmbTaxKbnNew.Value3 = Nothing
        Me.cmbTaxKbnNew.ValueMember = Nothing
        Me.cmbTaxKbnNew.WidthDef = 83
        '
        'lblTitleTaxKbnNew
        '
        Me.lblTitleTaxKbnNew.AutoSize = True
        Me.lblTitleTaxKbnNew.AutoSizeDef = True
        Me.lblTitleTaxKbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKbnNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKbnNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTaxKbnNew.EnableStatus = False
        Me.lblTitleTaxKbnNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKbnNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKbnNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKbnNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKbnNew.HeightDef = 13
        Me.lblTitleTaxKbnNew.Location = New System.Drawing.Point(186, 151)
        Me.lblTitleTaxKbnNew.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleTaxKbnNew.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleTaxKbnNew.Name = "lblTitleTaxKbnNew"
        Me.lblTitleTaxKbnNew.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleTaxKbnNew.TabIndex = 234
        Me.lblTitleTaxKbnNew.Text = "課税区分"
        Me.lblTitleTaxKbnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTaxKbnNew.TextValue = "課税区分"
        Me.lblTitleTaxKbnNew.WidthDef = 78
        '
        'txtDenpNo
        '
        Me.txtDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDenpNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDenpNo.CountWrappedLine = False
        Me.txtDenpNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDenpNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDenpNo.HeightDef = 18
        Me.txtDenpNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDenpNo.HissuLabelVisible = False
        Me.txtDenpNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtDenpNo.IsByteCheck = 20
        Me.txtDenpNo.IsCalendarCheck = False
        Me.txtDenpNo.IsDakutenCheck = False
        Me.txtDenpNo.IsEisuCheck = False
        Me.txtDenpNo.IsForbiddenWordsCheck = False
        Me.txtDenpNo.IsFullByteCheck = 0
        Me.txtDenpNo.IsHankakuCheck = False
        Me.txtDenpNo.IsHissuCheck = False
        Me.txtDenpNo.IsKanaCheck = False
        Me.txtDenpNo.IsMiddleSpace = False
        Me.txtDenpNo.IsNumericCheck = False
        Me.txtDenpNo.IsSujiCheck = False
        Me.txtDenpNo.IsZenkakuCheck = False
        Me.txtDenpNo.ItemName = ""
        Me.txtDenpNo.LineSpace = 0
        Me.txtDenpNo.Location = New System.Drawing.Point(89, 64)
        Me.txtDenpNo.MaxLength = 20
        Me.txtDenpNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDenpNo.MaxLineCount = 0
        Me.txtDenpNo.Multiline = False
        Me.txtDenpNo.Name = "txtDenpNo"
        Me.txtDenpNo.ReadOnly = False
        Me.txtDenpNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDenpNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDenpNo.Size = New System.Drawing.Size(199, 18)
        Me.txtDenpNo.TabIndex = 216
        Me.txtDenpNo.TabStopSetting = True
        Me.txtDenpNo.TextValue = ""
        Me.txtDenpNo.UseSystemPasswordChar = False
        Me.txtDenpNo.WidthDef = 199
        Me.txtDenpNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDenpNo
        '
        Me.lblTitleDenpNo.AutoSize = True
        Me.lblTitleDenpNo.AutoSizeDef = True
        Me.lblTitleDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDenpNo.EnableStatus = False
        Me.lblTitleDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDenpNo.HeightDef = 13
        Me.lblTitleDenpNo.Location = New System.Drawing.Point(4, 67)
        Me.lblTitleDenpNo.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleDenpNo.Name = "lblTitleDenpNo"
        Me.lblTitleDenpNo.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleDenpNo.TabIndex = 214
        Me.lblTitleDenpNo.Text = "送り状番号"
        Me.lblTitleDenpNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDenpNo.TextValue = "送り状番号"
        Me.lblTitleDenpNo.WidthDef = 78
        '
        'lblCustNmMNew
        '
        Me.lblCustNmMNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmMNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmMNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmMNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmMNew.CountWrappedLine = False
        Me.lblCustNmMNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmMNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmMNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmMNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmMNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmMNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmMNew.HeightDef = 18
        Me.lblCustNmMNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmMNew.HissuLabelVisible = False
        Me.lblCustNmMNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmMNew.IsByteCheck = 0
        Me.lblCustNmMNew.IsCalendarCheck = False
        Me.lblCustNmMNew.IsDakutenCheck = False
        Me.lblCustNmMNew.IsEisuCheck = False
        Me.lblCustNmMNew.IsForbiddenWordsCheck = False
        Me.lblCustNmMNew.IsFullByteCheck = 0
        Me.lblCustNmMNew.IsHankakuCheck = False
        Me.lblCustNmMNew.IsHissuCheck = False
        Me.lblCustNmMNew.IsKanaCheck = False
        Me.lblCustNmMNew.IsMiddleSpace = False
        Me.lblCustNmMNew.IsNumericCheck = False
        Me.lblCustNmMNew.IsSujiCheck = False
        Me.lblCustNmMNew.IsZenkakuCheck = False
        Me.lblCustNmMNew.ItemName = ""
        Me.lblCustNmMNew.LineSpace = 0
        Me.lblCustNmMNew.Location = New System.Drawing.Point(170, 43)
        Me.lblCustNmMNew.MaxLength = 0
        Me.lblCustNmMNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmMNew.MaxLineCount = 0
        Me.lblCustNmMNew.Multiline = False
        Me.lblCustNmMNew.Name = "lblCustNmMNew"
        Me.lblCustNmMNew.ReadOnly = True
        Me.lblCustNmMNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmMNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmMNew.Size = New System.Drawing.Size(440, 18)
        Me.lblCustNmMNew.TabIndex = 211
        Me.lblCustNmMNew.TabStop = False
        Me.lblCustNmMNew.TabStopSetting = False
        Me.lblCustNmMNew.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmMNew.UseSystemPasswordChar = False
        Me.lblCustNmMNew.WidthDef = 440
        Me.lblCustNmMNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCustNew
        '
        Me.lblTitleCustNew.AutoSize = True
        Me.lblTitleCustNew.AutoSizeDef = True
        Me.lblTitleCustNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustNew.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCustNew.EnableStatus = False
        Me.lblTitleCustNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustNew.HeightDef = 13
        Me.lblTitleCustNew.Location = New System.Drawing.Point(4, 24)
        Me.lblTitleCustNew.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblTitleCustNew.Name = "lblTitleCustNew"
        Me.lblTitleCustNew.Size = New System.Drawing.Size(78, 13)
        Me.lblTitleCustNew.TabIndex = 210
        Me.lblTitleCustNew.Text = "荷主"
        Me.lblTitleCustNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustNew.TextValue = "荷主"
        Me.lblTitleCustNew.WidthDef = 78
        '
        'lblCustNmLNew
        '
        Me.lblCustNmLNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmLNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmLNew.CountWrappedLine = False
        Me.lblCustNmLNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmLNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmLNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmLNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmLNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmLNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmLNew.HeightDef = 18
        Me.lblCustNmLNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLNew.HissuLabelVisible = False
        Me.lblCustNmLNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmLNew.IsByteCheck = 0
        Me.lblCustNmLNew.IsCalendarCheck = False
        Me.lblCustNmLNew.IsDakutenCheck = False
        Me.lblCustNmLNew.IsEisuCheck = False
        Me.lblCustNmLNew.IsForbiddenWordsCheck = False
        Me.lblCustNmLNew.IsFullByteCheck = 0
        Me.lblCustNmLNew.IsHankakuCheck = False
        Me.lblCustNmLNew.IsHissuCheck = False
        Me.lblCustNmLNew.IsKanaCheck = False
        Me.lblCustNmLNew.IsMiddleSpace = False
        Me.lblCustNmLNew.IsNumericCheck = False
        Me.lblCustNmLNew.IsSujiCheck = False
        Me.lblCustNmLNew.IsZenkakuCheck = False
        Me.lblCustNmLNew.ItemName = ""
        Me.lblCustNmLNew.LineSpace = 0
        Me.lblCustNmLNew.Location = New System.Drawing.Point(170, 22)
        Me.lblCustNmLNew.MaxLength = 0
        Me.lblCustNmLNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmLNew.MaxLineCount = 0
        Me.lblCustNmLNew.Multiline = False
        Me.lblCustNmLNew.Name = "lblCustNmLNew"
        Me.lblCustNmLNew.ReadOnly = True
        Me.lblCustNmLNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmLNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmLNew.Size = New System.Drawing.Size(440, 18)
        Me.lblCustNmLNew.TabIndex = 159
        Me.lblCustNmLNew.TabStop = False
        Me.lblCustNmLNew.TabStopSetting = False
        Me.lblCustNmLNew.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmLNew.UseSystemPasswordChar = False
        Me.lblCustNmLNew.WidthDef = 440
        Me.lblCustNmLNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdMNew
        '
        Me.txtCustCdMNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdMNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdMNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdMNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdMNew.CountWrappedLine = False
        Me.txtCustCdMNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdMNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdMNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdMNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdMNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdMNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdMNew.HeightDef = 18
        Me.txtCustCdMNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdMNew.HissuLabelVisible = False
        Me.txtCustCdMNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdMNew.IsByteCheck = 2
        Me.txtCustCdMNew.IsCalendarCheck = False
        Me.txtCustCdMNew.IsDakutenCheck = False
        Me.txtCustCdMNew.IsEisuCheck = False
        Me.txtCustCdMNew.IsForbiddenWordsCheck = False
        Me.txtCustCdMNew.IsFullByteCheck = 0
        Me.txtCustCdMNew.IsHankakuCheck = False
        Me.txtCustCdMNew.IsHissuCheck = False
        Me.txtCustCdMNew.IsKanaCheck = False
        Me.txtCustCdMNew.IsMiddleSpace = False
        Me.txtCustCdMNew.IsNumericCheck = False
        Me.txtCustCdMNew.IsSujiCheck = False
        Me.txtCustCdMNew.IsZenkakuCheck = False
        Me.txtCustCdMNew.ItemName = ""
        Me.txtCustCdMNew.LineSpace = 0
        Me.txtCustCdMNew.Location = New System.Drawing.Point(134, 43)
        Me.txtCustCdMNew.MaxLength = 2
        Me.txtCustCdMNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdMNew.MaxLineCount = 0
        Me.txtCustCdMNew.Multiline = False
        Me.txtCustCdMNew.Name = "txtCustCdMNew"
        Me.txtCustCdMNew.ReadOnly = False
        Me.txtCustCdMNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdMNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdMNew.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdMNew.TabIndex = 158
        Me.txtCustCdMNew.TabStopSetting = True
        Me.txtCustCdMNew.TextValue = ""
        Me.txtCustCdMNew.UseSystemPasswordChar = False
        Me.txtCustCdMNew.WidthDef = 52
        Me.txtCustCdMNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdLNew
        '
        Me.txtCustCdLNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdLNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdLNew.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdLNew.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdLNew.CountWrappedLine = False
        Me.txtCustCdLNew.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdLNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdLNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdLNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdLNew.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdLNew.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdLNew.HeightDef = 18
        Me.txtCustCdLNew.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdLNew.HissuLabelVisible = False
        Me.txtCustCdLNew.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdLNew.IsByteCheck = 5
        Me.txtCustCdLNew.IsCalendarCheck = False
        Me.txtCustCdLNew.IsDakutenCheck = False
        Me.txtCustCdLNew.IsEisuCheck = False
        Me.txtCustCdLNew.IsForbiddenWordsCheck = False
        Me.txtCustCdLNew.IsFullByteCheck = 0
        Me.txtCustCdLNew.IsHankakuCheck = False
        Me.txtCustCdLNew.IsHissuCheck = False
        Me.txtCustCdLNew.IsKanaCheck = False
        Me.txtCustCdLNew.IsMiddleSpace = False
        Me.txtCustCdLNew.IsNumericCheck = False
        Me.txtCustCdLNew.IsSujiCheck = False
        Me.txtCustCdLNew.IsZenkakuCheck = False
        Me.txtCustCdLNew.ItemName = ""
        Me.txtCustCdLNew.LineSpace = 0
        Me.txtCustCdLNew.Location = New System.Drawing.Point(89, 22)
        Me.txtCustCdLNew.MaxLength = 5
        Me.txtCustCdLNew.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdLNew.MaxLineCount = 0
        Me.txtCustCdLNew.Multiline = False
        Me.txtCustCdLNew.Name = "txtCustCdLNew"
        Me.txtCustCdLNew.ReadOnly = False
        Me.txtCustCdLNew.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdLNew.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdLNew.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdLNew.TabIndex = 157
        Me.txtCustCdLNew.TabStopSetting = True
        Me.txtCustCdLNew.TextValue = ""
        Me.txtCustCdLNew.UseSystemPasswordChar = False
        Me.txtCustCdLNew.WidthDef = 97
        Me.txtCustCdLNew.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnSakiAdd
        '
        Me.btnSakiAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSakiAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSakiAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSakiAdd.EnableStatus = True
        Me.btnSakiAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSakiAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSakiAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSakiAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSakiAdd.HeightDef = 22
        Me.btnSakiAdd.Location = New System.Drawing.Point(667, 414)
        Me.btnSakiAdd.Name = "btnSakiAdd"
        Me.btnSakiAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnSakiAdd.TabIndex = 279
        Me.btnSakiAdd.TabStopSetting = True
        Me.btnSakiAdd.Text = "行追加"
        Me.btnSakiAdd.TextValue = "行追加"
        Me.btnSakiAdd.UseVisualStyleBackColor = True
        Me.btnSakiAdd.WidthDef = 70
        '
        'btnSakiDel
        '
        Me.btnSakiDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSakiDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSakiDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSakiDel.EnableStatus = True
        Me.btnSakiDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSakiDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSakiDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSakiDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSakiDel.HeightDef = 22
        Me.btnSakiDel.Location = New System.Drawing.Point(743, 414)
        Me.btnSakiDel.Name = "btnSakiDel"
        Me.btnSakiDel.Size = New System.Drawing.Size(70, 22)
        Me.btnSakiDel.TabIndex = 277
        Me.btnSakiDel.TabStopSetting = True
        Me.btnSakiDel.Text = "行削除"
        Me.btnSakiDel.TextValue = "行削除"
        Me.btnSakiDel.UseVisualStyleBackColor = True
        Me.btnSakiDel.WidthDef = 70
        '
        'sprDtlNew
        '
        Me.sprDtlNew.AccessibleDescription = ""
        Me.sprDtlNew.AllowUserZoom = False
        Me.sprDtlNew.AutoImeMode = False
        Me.sprDtlNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDtlNew.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDtlNew.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDtlNew.CellClickEventArgs = Nothing
        Me.sprDtlNew.CheckToCheckBox = True
        Me.sprDtlNew.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDtlNew.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDtlNew.EditModeReplace = True
        Me.sprDtlNew.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprDtlNew.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDtlNew.HeightDef = 425
        Me.sprDtlNew.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDtlNew.HorizontalScrollBar.Name = ""
        Me.sprDtlNew.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprDtlNew.KeyboardCheckBoxOn = False
        Me.sprDtlNew.Location = New System.Drawing.Point(641, 439)
        Me.sprDtlNew.Name = "sprDtlNew"
        Me.sprDtlNew.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDtlNew.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDtlNew_Sheet1})
        Me.sprDtlNew.Size = New System.Drawing.Size(615, 425)
        Me.sprDtlNew.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDtlNew.SortColumn = True
        Me.sprDtlNew.SpanColumnLock = True
        Me.sprDtlNew.SpreadDoubleClicked = False
        Me.sprDtlNew.TabIndex = 278
        Me.sprDtlNew.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDtlNew.TextValue = Nothing
        Me.sprDtlNew.UseGrouping = False
        Me.sprDtlNew.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDtlNew.VerticalScrollBar.Name = ""
        Me.sprDtlNew.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprDtlNew.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDtlNew.WidthDef = 615
        sprDtlNew_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDtlNew_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDtlNew_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDtlNew.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDtlNew_InputMapWhenFocusedNormal)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDtlNew_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDtlNew.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDtlNew_InputMapWhenAncestorOfFocusedNormal)
        '
        'sprDtlNew_Sheet1
        '
        Me.sprDtlNew_Sheet1.Reset()
        Me.sprDtlNew_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprDtlNew_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprDtlNew_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprDtlNew_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprDtlNew_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprDtlNew_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprDtlNew_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDtlNew_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprDtlNew_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprDtlNew_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDtlNew_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.sprDtlNew_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.DefaultStyle.Locked = False
        Me.sprDtlNew_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprDtlNew_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprDtlNew_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprDtlNew_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprDtlNew_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprDtlNew_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDtlNew_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDtlNew_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprDtlNew_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprDtlNew_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprDtlNew_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprDtlNew_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprDtlNew_Sheet1.Rows.Default.Height = 18.0!
        Me.sprDtlNew_Sheet1.Rows.Default.Resizable = False
        Me.sprDtlNew_Sheet1.Rows.Default.Visible = True
        Me.sprDtlNew_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDtlNew_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprDtlNew_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprDtlNew_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDtlNew_Sheet1.SheetCornerStyle.Locked = True
        Me.sprDtlNew_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDtlNew_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprDtlNew_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDtlNew_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(393, 9)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(240, 18)
        Me.cmbNrsBrCd.TabIndex = 280
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 240
        '
        'cmbSoko
        '
        Me.cmbSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbSoko.Location = New System.Drawing.Point(888, 9)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = False
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(252, 18)
        Me.cmbSoko.TabIndex = 281
        Me.cmbSoko.TabStopSetting = True
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 252
        '
        'cmbToukiHokanKbn
        '
        Me.cmbToukiHokanKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbToukiHokanKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbToukiHokanKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbToukiHokanKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbToukiHokanKbn.DataCode = "H009"
        Me.cmbToukiHokanKbn.DataSource = Nothing
        Me.cmbToukiHokanKbn.DisplayMember = Nothing
        Me.cmbToukiHokanKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbToukiHokanKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbToukiHokanKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbToukiHokanKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbToukiHokanKbn.HeightDef = 18
        Me.cmbToukiHokanKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbToukiHokanKbn.HissuLabelVisible = True
        Me.cmbToukiHokanKbn.InsertWildCard = True
        Me.cmbToukiHokanKbn.IsForbiddenWordsCheck = False
        Me.cmbToukiHokanKbn.IsHissuCheck = True
        Me.cmbToukiHokanKbn.ItemName = ""
        Me.cmbToukiHokanKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbToukiHokanKbn.Location = New System.Drawing.Point(888, 30)
        Me.cmbToukiHokanKbn.Name = "cmbToukiHokanKbn"
        Me.cmbToukiHokanKbn.ReadOnly = False
        Me.cmbToukiHokanKbn.SelectedIndex = -1
        Me.cmbToukiHokanKbn.SelectedItem = Nothing
        Me.cmbToukiHokanKbn.SelectedText = ""
        Me.cmbToukiHokanKbn.SelectedValue = ""
        Me.cmbToukiHokanKbn.Size = New System.Drawing.Size(118, 18)
        Me.cmbToukiHokanKbn.TabIndex = 282
        Me.cmbToukiHokanKbn.TabStopSetting = True
        Me.cmbToukiHokanKbn.TextValue = ""
        Me.cmbToukiHokanKbn.Value1 = "= '1.000'"
        Me.cmbToukiHokanKbn.Value2 = Nothing
        Me.cmbToukiHokanKbn.Value3 = Nothing
        Me.cmbToukiHokanKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V1
        Me.cmbToukiHokanKbn.ValueMember = Nothing
        Me.cmbToukiHokanKbn.WidthDef = 118
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1131, 33)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 283
        Me.lblSituation.TabStop = False
        Me.lblSituation.Visible = False
        '
        'lblSagyoInNmO1
        '
        Me.lblSagyoInNmO1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmO1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmO1.CountWrappedLine = False
        Me.lblSagyoInNmO1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmO1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmO1.HeightDef = 18
        Me.lblSagyoInNmO1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO1.HissuLabelVisible = False
        Me.lblSagyoInNmO1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmO1.IsByteCheck = 0
        Me.lblSagyoInNmO1.IsCalendarCheck = False
        Me.lblSagyoInNmO1.IsDakutenCheck = False
        Me.lblSagyoInNmO1.IsEisuCheck = False
        Me.lblSagyoInNmO1.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmO1.IsFullByteCheck = 0
        Me.lblSagyoInNmO1.IsHankakuCheck = False
        Me.lblSagyoInNmO1.IsHissuCheck = False
        Me.lblSagyoInNmO1.IsKanaCheck = False
        Me.lblSagyoInNmO1.IsMiddleSpace = False
        Me.lblSagyoInNmO1.IsNumericCheck = False
        Me.lblSagyoInNmO1.IsSujiCheck = False
        Me.lblSagyoInNmO1.IsZenkakuCheck = False
        Me.lblSagyoInNmO1.ItemName = ""
        Me.lblSagyoInNmO1.LineSpace = 0
        Me.lblSagyoInNmO1.Location = New System.Drawing.Point(122, 414)
        Me.lblSagyoInNmO1.MaxLength = 0
        Me.lblSagyoInNmO1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmO1.MaxLineCount = 0
        Me.lblSagyoInNmO1.Multiline = False
        Me.lblSagyoInNmO1.Name = "lblSagyoInNmO1"
        Me.lblSagyoInNmO1.ReadOnly = True
        Me.lblSagyoInNmO1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmO1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmO1.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmO1.TabIndex = 218
        Me.lblSagyoInNmO1.TabStop = False
        Me.lblSagyoInNmO1.TabStopSetting = False
        Me.lblSagyoInNmO1.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmO1.UseSystemPasswordChar = False
        Me.lblSagyoInNmO1.Visible = False
        Me.lblSagyoInNmO1.WidthDef = 110
        Me.lblSagyoInNmO1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoInNmO2
        '
        Me.lblSagyoInNmO2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmO2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmO2.CountWrappedLine = False
        Me.lblSagyoInNmO2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmO2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmO2.HeightDef = 18
        Me.lblSagyoInNmO2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO2.HissuLabelVisible = False
        Me.lblSagyoInNmO2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmO2.IsByteCheck = 0
        Me.lblSagyoInNmO2.IsCalendarCheck = False
        Me.lblSagyoInNmO2.IsDakutenCheck = False
        Me.lblSagyoInNmO2.IsEisuCheck = False
        Me.lblSagyoInNmO2.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmO2.IsFullByteCheck = 0
        Me.lblSagyoInNmO2.IsHankakuCheck = False
        Me.lblSagyoInNmO2.IsHissuCheck = False
        Me.lblSagyoInNmO2.IsKanaCheck = False
        Me.lblSagyoInNmO2.IsMiddleSpace = False
        Me.lblSagyoInNmO2.IsNumericCheck = False
        Me.lblSagyoInNmO2.IsSujiCheck = False
        Me.lblSagyoInNmO2.IsZenkakuCheck = False
        Me.lblSagyoInNmO2.ItemName = ""
        Me.lblSagyoInNmO2.LineSpace = 0
        Me.lblSagyoInNmO2.Location = New System.Drawing.Point(216, 414)
        Me.lblSagyoInNmO2.MaxLength = 0
        Me.lblSagyoInNmO2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmO2.MaxLineCount = 0
        Me.lblSagyoInNmO2.Multiline = False
        Me.lblSagyoInNmO2.Name = "lblSagyoInNmO2"
        Me.lblSagyoInNmO2.ReadOnly = True
        Me.lblSagyoInNmO2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmO2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmO2.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmO2.TabIndex = 284
        Me.lblSagyoInNmO2.TabStop = False
        Me.lblSagyoInNmO2.TabStopSetting = False
        Me.lblSagyoInNmO2.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmO2.UseSystemPasswordChar = False
        Me.lblSagyoInNmO2.Visible = False
        Me.lblSagyoInNmO2.WidthDef = 110
        Me.lblSagyoInNmO2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoInNmO3
        '
        Me.lblSagyoInNmO3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmO3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmO3.CountWrappedLine = False
        Me.lblSagyoInNmO3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmO3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmO3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmO3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmO3.HeightDef = 18
        Me.lblSagyoInNmO3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmO3.HissuLabelVisible = False
        Me.lblSagyoInNmO3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmO3.IsByteCheck = 0
        Me.lblSagyoInNmO3.IsCalendarCheck = False
        Me.lblSagyoInNmO3.IsDakutenCheck = False
        Me.lblSagyoInNmO3.IsEisuCheck = False
        Me.lblSagyoInNmO3.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmO3.IsFullByteCheck = 0
        Me.lblSagyoInNmO3.IsHankakuCheck = False
        Me.lblSagyoInNmO3.IsHissuCheck = False
        Me.lblSagyoInNmO3.IsKanaCheck = False
        Me.lblSagyoInNmO3.IsMiddleSpace = False
        Me.lblSagyoInNmO3.IsNumericCheck = False
        Me.lblSagyoInNmO3.IsSujiCheck = False
        Me.lblSagyoInNmO3.IsZenkakuCheck = False
        Me.lblSagyoInNmO3.ItemName = ""
        Me.lblSagyoInNmO3.LineSpace = 0
        Me.lblSagyoInNmO3.Location = New System.Drawing.Point(310, 414)
        Me.lblSagyoInNmO3.MaxLength = 0
        Me.lblSagyoInNmO3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmO3.MaxLineCount = 0
        Me.lblSagyoInNmO3.Multiline = False
        Me.lblSagyoInNmO3.Name = "lblSagyoInNmO3"
        Me.lblSagyoInNmO3.ReadOnly = True
        Me.lblSagyoInNmO3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmO3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmO3.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmO3.TabIndex = 285
        Me.lblSagyoInNmO3.TabStop = False
        Me.lblSagyoInNmO3.TabStopSetting = False
        Me.lblSagyoInNmO3.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmO3.UseSystemPasswordChar = False
        Me.lblSagyoInNmO3.Visible = False
        Me.lblSagyoInNmO3.WidthDef = 110
        Me.lblSagyoInNmO3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoInNmN3
        '
        Me.lblSagyoInNmN3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmN3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmN3.CountWrappedLine = False
        Me.lblSagyoInNmN3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmN3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmN3.HeightDef = 18
        Me.lblSagyoInNmN3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN3.HissuLabelVisible = False
        Me.lblSagyoInNmN3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmN3.IsByteCheck = 0
        Me.lblSagyoInNmN3.IsCalendarCheck = False
        Me.lblSagyoInNmN3.IsDakutenCheck = False
        Me.lblSagyoInNmN3.IsEisuCheck = False
        Me.lblSagyoInNmN3.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmN3.IsFullByteCheck = 0
        Me.lblSagyoInNmN3.IsHankakuCheck = False
        Me.lblSagyoInNmN3.IsHissuCheck = False
        Me.lblSagyoInNmN3.IsKanaCheck = False
        Me.lblSagyoInNmN3.IsMiddleSpace = False
        Me.lblSagyoInNmN3.IsNumericCheck = False
        Me.lblSagyoInNmN3.IsSujiCheck = False
        Me.lblSagyoInNmN3.IsZenkakuCheck = False
        Me.lblSagyoInNmN3.ItemName = ""
        Me.lblSagyoInNmN3.LineSpace = 0
        Me.lblSagyoInNmN3.Location = New System.Drawing.Point(1003, 415)
        Me.lblSagyoInNmN3.MaxLength = 0
        Me.lblSagyoInNmN3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmN3.MaxLineCount = 0
        Me.lblSagyoInNmN3.Multiline = False
        Me.lblSagyoInNmN3.Name = "lblSagyoInNmN3"
        Me.lblSagyoInNmN3.ReadOnly = True
        Me.lblSagyoInNmN3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmN3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmN3.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmN3.TabIndex = 288
        Me.lblSagyoInNmN3.TabStop = False
        Me.lblSagyoInNmN3.TabStopSetting = False
        Me.lblSagyoInNmN3.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmN3.UseSystemPasswordChar = False
        Me.lblSagyoInNmN3.Visible = False
        Me.lblSagyoInNmN3.WidthDef = 110
        Me.lblSagyoInNmN3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoInNmN2
        '
        Me.lblSagyoInNmN2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmN2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmN2.CountWrappedLine = False
        Me.lblSagyoInNmN2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmN2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmN2.HeightDef = 18
        Me.lblSagyoInNmN2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN2.HissuLabelVisible = False
        Me.lblSagyoInNmN2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmN2.IsByteCheck = 0
        Me.lblSagyoInNmN2.IsCalendarCheck = False
        Me.lblSagyoInNmN2.IsDakutenCheck = False
        Me.lblSagyoInNmN2.IsEisuCheck = False
        Me.lblSagyoInNmN2.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmN2.IsFullByteCheck = 0
        Me.lblSagyoInNmN2.IsHankakuCheck = False
        Me.lblSagyoInNmN2.IsHissuCheck = False
        Me.lblSagyoInNmN2.IsKanaCheck = False
        Me.lblSagyoInNmN2.IsMiddleSpace = False
        Me.lblSagyoInNmN2.IsNumericCheck = False
        Me.lblSagyoInNmN2.IsSujiCheck = False
        Me.lblSagyoInNmN2.IsZenkakuCheck = False
        Me.lblSagyoInNmN2.ItemName = ""
        Me.lblSagyoInNmN2.LineSpace = 0
        Me.lblSagyoInNmN2.Location = New System.Drawing.Point(909, 415)
        Me.lblSagyoInNmN2.MaxLength = 0
        Me.lblSagyoInNmN2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmN2.MaxLineCount = 0
        Me.lblSagyoInNmN2.Multiline = False
        Me.lblSagyoInNmN2.Name = "lblSagyoInNmN2"
        Me.lblSagyoInNmN2.ReadOnly = True
        Me.lblSagyoInNmN2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmN2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmN2.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmN2.TabIndex = 287
        Me.lblSagyoInNmN2.TabStop = False
        Me.lblSagyoInNmN2.TabStopSetting = False
        Me.lblSagyoInNmN2.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmN2.UseSystemPasswordChar = False
        Me.lblSagyoInNmN2.Visible = False
        Me.lblSagyoInNmN2.WidthDef = 110
        Me.lblSagyoInNmN2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoInNmN1
        '
        Me.lblSagyoInNmN1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoInNmN1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoInNmN1.CountWrappedLine = False
        Me.lblSagyoInNmN1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoInNmN1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoInNmN1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoInNmN1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoInNmN1.HeightDef = 18
        Me.lblSagyoInNmN1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoInNmN1.HissuLabelVisible = False
        Me.lblSagyoInNmN1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoInNmN1.IsByteCheck = 0
        Me.lblSagyoInNmN1.IsCalendarCheck = False
        Me.lblSagyoInNmN1.IsDakutenCheck = False
        Me.lblSagyoInNmN1.IsEisuCheck = False
        Me.lblSagyoInNmN1.IsForbiddenWordsCheck = False
        Me.lblSagyoInNmN1.IsFullByteCheck = 0
        Me.lblSagyoInNmN1.IsHankakuCheck = False
        Me.lblSagyoInNmN1.IsHissuCheck = False
        Me.lblSagyoInNmN1.IsKanaCheck = False
        Me.lblSagyoInNmN1.IsMiddleSpace = False
        Me.lblSagyoInNmN1.IsNumericCheck = False
        Me.lblSagyoInNmN1.IsSujiCheck = False
        Me.lblSagyoInNmN1.IsZenkakuCheck = False
        Me.lblSagyoInNmN1.ItemName = ""
        Me.lblSagyoInNmN1.LineSpace = 0
        Me.lblSagyoInNmN1.Location = New System.Drawing.Point(815, 415)
        Me.lblSagyoInNmN1.MaxLength = 0
        Me.lblSagyoInNmN1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoInNmN1.MaxLineCount = 0
        Me.lblSagyoInNmN1.Multiline = False
        Me.lblSagyoInNmN1.Name = "lblSagyoInNmN1"
        Me.lblSagyoInNmN1.ReadOnly = True
        Me.lblSagyoInNmN1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoInNmN1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoInNmN1.Size = New System.Drawing.Size(110, 18)
        Me.lblSagyoInNmN1.TabIndex = 286
        Me.lblSagyoInNmN1.TabStop = False
        Me.lblSagyoInNmN1.TabStopSetting = False
        Me.lblSagyoInNmN1.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyoInNmN1.UseSystemPasswordChar = False
        Me.lblSagyoInNmN1.Visible = False
        Me.lblSagyoInNmN1.WidthDef = 110
        Me.lblSagyoInNmN1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMD010F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.FocusedControlName = "LMImTextBox"
        Me.Name = "LMD010F"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "【LMD010】  在庫振替入力"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlFurikae.ResumeLayout(False)
        Me.pnlFurikae.PerformLayout()
        Me.pnlSyukkaRemark.ResumeLayout(False)
        Me.pnlHutaiSagyo.ResumeLayout(False)
        Me.pnlHutaiSagyo.PerformLayout()
        Me.pnlKosu.ResumeLayout(False)
        Me.pnlKosu.PerformLayout()
        CType(Me.spdDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdDtl_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFurikaeNew.ResumeLayout(False)
        Me.pnlFurikaeNew.PerformLayout()
        Me.pnlNyukaRemark.ResumeLayout(False)
        Me.pnlHutaiSagyoNew.ResumeLayout(False)
        Me.pnlHutaiSagyoNew.PerformLayout()
        CType(Me.sprDtlNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDtlNew_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlFurikae As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleFurikaeKnariNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleFurikaeKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblFurikaeNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbFurikaeKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSoko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleFruikaeDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdFurikaeDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents chkYoukiChange As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleToukiHokanKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrderNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrderNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleLotNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSerialNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSerialNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtLotNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleIrime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleIrimeTanni As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numIrime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents pnlKosu As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleHikiZanCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHikiZanCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKosu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKosuCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHikiSumiCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHikiSumiCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleIrisu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrisuCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHasu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents pnlHutaiSagyo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblSagyoNmO1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoNmO2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoNmO3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdO1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdO2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdO3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlSyukkaRemark As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtSyukkaRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlFurikaeNew As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlHutaiSagyoNew As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblSagyoNmN1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoNmN2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoNmN3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo1New As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdN1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo3New As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdN2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo2New As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCdN3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbTaxKbnNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTaxKbnNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDenpNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDenpNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmMNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmLNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdMNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdLNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents chkInkoDateUmu As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents txtGoodsNmCust As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsCdCust As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNiyakuNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtGoodsNmCustNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNiyakuNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleGoodsNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsCdCustNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlNyukaRemark As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtNyukaRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleKonsu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numKonsu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents spdDtl As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents sprDtlNew As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnSakiAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnSakiDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnMotoDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbNiyaku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleNiyaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTaxKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTaxKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents cmbToukiHokanKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblGoodsCdNrs As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsCdNrsNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsCdNrsNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSagyoInNmO1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoInNmO2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoInNmN3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoInNmN2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoInNmN1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoInNmO3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numIrimeNew As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblIrimeTanni As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun
    Friend WithEvents lblKonsuTanni As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun
    Friend WithEvents lblCntTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun
    Friend WithEvents lblIrimeTanniNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun
    Friend WithEvents lblKosuTanniNew As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabelKubun
    Friend WithEvents sprDtlNew_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents spdDtl_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
