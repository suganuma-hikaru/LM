<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF020F
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
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlUnso = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnsoRemark = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtUnsocoBrCdOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblPayExtcTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitlePayExtcTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtUnsocoCdOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblPayTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtPayExtcTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtPayTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitlePayTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTareYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnsoBrNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnsoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSplitFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbTax = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTax = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCalcKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnKeisan = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.txtUnsoComment = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnsoComment = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKg1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbUnsoCntUt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbThermoKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleThermoKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUnsoCntUt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnsoWtL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleUnsoWtL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnsoPkgCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleUnsoPkgCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlDestOrigin = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtTel = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.beforeAutoDenpKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblAutoDenpNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbAutoDenpKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.titleAutoDenpNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTakSize = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDest = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleOrig = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblAreaNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtAreaCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleArea = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestAdd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblDestAdd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblZipNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleAdd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestJisCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDestJisCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestAdd3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleJiDestTime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleDestTime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdDestDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleDestDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblOrigJisCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleOrigJisCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblOrigNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtOrigCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleOrigTime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdOrigDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleOrigDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtOrigTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtJiDestTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtOrdNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtBuyerOrdNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleBuyerOrdNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOkuriNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitletxtOrdNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleOkuriNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblExtcTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleExtcTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtExtcTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblShipNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtShipCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleShip = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnsocoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnsocoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnsocoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnsoco = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTehaiKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleUnsoMotoKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbPcKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitlePcKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSharyoKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleSharyoKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTehaiKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbBinKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleBinKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbUnsoJiyuKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleUnsoJiyu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbMotoDataKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleMotoData = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblKanriNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleKanriNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnkoNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnkoNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnsoNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnsoNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleYosoEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbYosoEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.pnlCargo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.btnAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.pnlCharge = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.numInsurExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleInsurExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPassExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePassExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numRelyExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleRelyExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numWintExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleWintExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numCityExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleCityExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSeiqUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSeiqUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSeiqTariffDes = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel40 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnsoWt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleUnsoWt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.lblTitleBu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPrtCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.pnlPay = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.numPayInsurExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayInsurExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayPassExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayPassExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayRelyExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayRelyExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayWintExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayWintExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayCityExtc = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayCityExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayPayUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayPayUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitlePayKm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPaySeiqTariffDes = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePaySkyuKyori = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblPayTitleKg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayUnsoWt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayUnsoWt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPrtCnt_To = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numPrtCnt_From = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePrtCntFromTo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlUnso.SuspendLayout()
        Me.pnlDestOrigin.SuspendLayout()
        Me.pnlCargo.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCharge.SuspendLayout()
        Me.pnlPay.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.numPrtCnt_To)
        Me.pnlViewAria.Controls.Add(Me.pnlPay)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePrtCntFromTo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleBu)
        Me.pnlViewAria.Controls.Add(Me.numPrtCnt_From)
        Me.pnlViewAria.Controls.Add(Me.numPrtCnt)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.pnlCharge)
        Me.pnlViewAria.Controls.Add(Me.pnlCargo)
        Me.pnlViewAria.Controls.Add(Me.pnlUnso)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'lblTitleEigyo
        '
        Me.lblTitleEigyo.AutoSizeDef = False
        Me.lblTitleEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo.EnableStatus = False
        Me.lblTitleEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.HeightDef = 13
        Me.lblTitleEigyo.Location = New System.Drawing.Point(67, 22)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 1
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.lblCustNm.HissuLabelVisible = True
        Me.lblCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNm.IsByteCheck = 0
        Me.lblCustNm.IsCalendarCheck = False
        Me.lblCustNm.IsDakutenCheck = False
        Me.lblCustNm.IsEisuCheck = False
        Me.lblCustNm.IsForbiddenWordsCheck = False
        Me.lblCustNm.IsFullByteCheck = 0
        Me.lblCustNm.IsHankakuCheck = False
        Me.lblCustNm.IsHissuCheck = True
        Me.lblCustNm.IsKanaCheck = False
        Me.lblCustNm.IsMiddleSpace = False
        Me.lblCustNm.IsNumericCheck = False
        Me.lblCustNm.IsSujiCheck = False
        Me.lblCustNm.IsZenkakuCheck = False
        Me.lblCustNm.ItemName = ""
        Me.lblCustNm.LineSpace = 0
        Me.lblCustNm.Location = New System.Drawing.Point(211, 103)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(565, 18)
        Me.lblCustNm.TabIndex = 7
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 565
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdM.Location = New System.Drawing.Point(182, 103)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdM.TabIndex = 18
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 45
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(56, 106)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(60, 13)
        Me.lblTitleCust.TabIndex = 5
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 60
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
        Me.txtCustCdL.Location = New System.Drawing.Point(116, 103)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(82, 18)
        Me.txtCustCdL.TabIndex = 6
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 82
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlUnso
        '
        Me.pnlUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlUnso.Controls.Add(Me.txtRemark)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoRemark)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoBrCdOld)
        Me.pnlUnso.Controls.Add(Me.lblPayExtcTariffRem)
        Me.pnlUnso.Controls.Add(Me.lblTitlePayExtcTariff)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoCdOld)
        Me.pnlUnso.Controls.Add(Me.lblPayTariffRem)
        Me.pnlUnso.Controls.Add(Me.txtPayExtcTariffCd)
        Me.pnlUnso.Controls.Add(Me.txtPayTariffCd)
        Me.pnlUnso.Controls.Add(Me.lblTitlePayTariff)
        Me.pnlUnso.Controls.Add(Me.lblCustNmM)
        Me.pnlUnso.Controls.Add(Me.lblTareYn)
        Me.pnlUnso.Controls.Add(Me.lblUnsoBrNm)
        Me.pnlUnso.Controls.Add(Me.lblUnsoNm)
        Me.pnlUnso.Controls.Add(Me.lblSplitFlg)
        Me.pnlUnso.Controls.Add(Me.cmbTax)
        Me.pnlUnso.Controls.Add(Me.lblTitleTax)
        Me.pnlUnso.Controls.Add(Me.lblCalcKbn)
        Me.pnlUnso.Controls.Add(Me.btnKeisan)
        Me.pnlUnso.Controls.Add(Me.txtUnsoComment)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoComment)
        Me.pnlUnso.Controls.Add(Me.lblTitleKg1)
        Me.pnlUnso.Controls.Add(Me.cmbUnsoCntUt)
        Me.pnlUnso.Controls.Add(Me.cmbThermoKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleThermoKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoCntUt)
        Me.pnlUnso.Controls.Add(Me.numUnsoWtL)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoWtL)
        Me.pnlUnso.Controls.Add(Me.numUnsoPkgCnt)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoPkgCnt)
        Me.pnlUnso.Controls.Add(Me.pnlDestOrigin)
        Me.pnlUnso.Controls.Add(Me.txtOrdNo)
        Me.pnlUnso.Controls.Add(Me.txtBuyerOrdNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleBuyerOrdNo)
        Me.pnlUnso.Controls.Add(Me.txtOkuriNo)
        Me.pnlUnso.Controls.Add(Me.lblTitletxtOrdNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleOkuriNo)
        Me.pnlUnso.Controls.Add(Me.lblExtcTariffRem)
        Me.pnlUnso.Controls.Add(Me.lblTitleExtcTariff)
        Me.pnlUnso.Controls.Add(Me.lblTariffRem)
        Me.pnlUnso.Controls.Add(Me.txtExtcTariffCd)
        Me.pnlUnso.Controls.Add(Me.txtTariffCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleTariff)
        Me.pnlUnso.Controls.Add(Me.lblShipNm)
        Me.pnlUnso.Controls.Add(Me.txtShipCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleShip)
        Me.pnlUnso.Controls.Add(Me.lblUnsocoNm)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoBrCd)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoco)
        Me.pnlUnso.Controls.Add(Me.cmbTehaiKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoMotoKbn)
        Me.pnlUnso.Controls.Add(Me.cmbPcKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitlePcKbn)
        Me.pnlUnso.Controls.Add(Me.cmbSharyoKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleSharyoKbn)
        Me.pnlUnso.Controls.Add(Me.cmbTariffKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleTehaiKbn)
        Me.pnlUnso.Controls.Add(Me.cmbBinKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleBinKbn)
        Me.pnlUnso.Controls.Add(Me.cmbUnsoJiyuKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoJiyu)
        Me.pnlUnso.Controls.Add(Me.cmbMotoDataKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleMotoData)
        Me.pnlUnso.Controls.Add(Me.lblKanriNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleKanriNo)
        Me.pnlUnso.Controls.Add(Me.lblUnkoNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnkoNo)
        Me.pnlUnso.Controls.Add(Me.lblUnsoNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleYosoEigyo)
        Me.pnlUnso.Controls.Add(Me.lblCustNm)
        Me.pnlUnso.Controls.Add(Me.txtCustCdM)
        Me.pnlUnso.Controls.Add(Me.lblTitleEigyo)
        Me.pnlUnso.Controls.Add(Me.txtCustCdL)
        Me.pnlUnso.Controls.Add(Me.lblTitleCust)
        Me.pnlUnso.Controls.Add(Me.cmbEigyo)
        Me.pnlUnso.Controls.Add(Me.cmbYosoEigyo)
        Me.pnlUnso.EnableStatus = False
        Me.pnlUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.HeightDef = 435
        Me.pnlUnso.Location = New System.Drawing.Point(13, 33)
        Me.pnlUnso.Name = "pnlUnso"
        Me.pnlUnso.Size = New System.Drawing.Size(1253, 435)
        Me.pnlUnso.TabIndex = 33
        Me.pnlUnso.TabStop = False
        Me.pnlUnso.Text = "運送情報"
        Me.pnlUnso.TextValue = "運送情報"
        Me.pnlUnso.WidthDef = 1253
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
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
        Me.txtRemark.Location = New System.Drawing.Point(115, 415)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(842, 18)
        Me.txtRemark.TabIndex = 264
        Me.txtRemark.TabStopSetting = True
        Me.txtRemark.TextValue = ""
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 842
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoRemark
        '
        Me.lblTitleUnsoRemark.AutoSize = True
        Me.lblTitleUnsoRemark.AutoSizeDef = True
        Me.lblTitleUnsoRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoRemark.EnableStatus = False
        Me.lblTitleUnsoRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoRemark.HeightDef = 13
        Me.lblTitleUnsoRemark.Location = New System.Drawing.Point(25, 412)
        Me.lblTitleUnsoRemark.Name = "lblTitleUnsoRemark"
        Me.lblTitleUnsoRemark.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleUnsoRemark.TabIndex = 263
        Me.lblTitleUnsoRemark.Text = "備考"
        Me.lblTitleUnsoRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoRemark.TextValue = "備考"
        Me.lblTitleUnsoRemark.WidthDef = 35
        '
        'txtUnsocoBrCdOld
        '
        Me.txtUnsocoBrCdOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCdOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCdOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoBrCdOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoBrCdOld.CountWrappedLine = False
        Me.txtUnsocoBrCdOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoBrCdOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCdOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCdOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCdOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCdOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoBrCdOld.HeightDef = 18
        Me.txtUnsocoBrCdOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCdOld.HissuLabelVisible = False
        Me.txtUnsocoBrCdOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoBrCdOld.IsByteCheck = 3
        Me.txtUnsocoBrCdOld.IsCalendarCheck = False
        Me.txtUnsocoBrCdOld.IsDakutenCheck = False
        Me.txtUnsocoBrCdOld.IsEisuCheck = False
        Me.txtUnsocoBrCdOld.IsForbiddenWordsCheck = False
        Me.txtUnsocoBrCdOld.IsFullByteCheck = 0
        Me.txtUnsocoBrCdOld.IsHankakuCheck = False
        Me.txtUnsocoBrCdOld.IsHissuCheck = False
        Me.txtUnsocoBrCdOld.IsKanaCheck = False
        Me.txtUnsocoBrCdOld.IsMiddleSpace = False
        Me.txtUnsocoBrCdOld.IsNumericCheck = False
        Me.txtUnsocoBrCdOld.IsSujiCheck = False
        Me.txtUnsocoBrCdOld.IsZenkakuCheck = False
        Me.txtUnsocoBrCdOld.ItemName = ""
        Me.txtUnsocoBrCdOld.LineSpace = 0
        Me.txtUnsocoBrCdOld.Location = New System.Drawing.Point(855, 82)
        Me.txtUnsocoBrCdOld.MaxLength = 3
        Me.txtUnsocoBrCdOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoBrCdOld.MaxLineCount = 0
        Me.txtUnsocoBrCdOld.Multiline = False
        Me.txtUnsocoBrCdOld.Name = "txtUnsocoBrCdOld"
        Me.txtUnsocoBrCdOld.ReadOnly = True
        Me.txtUnsocoBrCdOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoBrCdOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoBrCdOld.Size = New System.Drawing.Size(33, 18)
        Me.txtUnsocoBrCdOld.TabIndex = 262
        Me.txtUnsocoBrCdOld.TabStop = False
        Me.txtUnsocoBrCdOld.TabStopSetting = False
        Me.txtUnsocoBrCdOld.TextValue = ""
        Me.txtUnsocoBrCdOld.UseSystemPasswordChar = False
        Me.txtUnsocoBrCdOld.Visible = False
        Me.txtUnsocoBrCdOld.WidthDef = 33
        Me.txtUnsocoBrCdOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblPayExtcTariffRem
        '
        Me.lblPayExtcTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayExtcTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayExtcTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPayExtcTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPayExtcTariffRem.CountWrappedLine = False
        Me.lblPayExtcTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblPayExtcTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayExtcTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayExtcTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayExtcTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayExtcTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblPayExtcTariffRem.HeightDef = 18
        Me.lblPayExtcTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayExtcTariffRem.HissuLabelVisible = False
        Me.lblPayExtcTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblPayExtcTariffRem.IsByteCheck = 0
        Me.lblPayExtcTariffRem.IsCalendarCheck = False
        Me.lblPayExtcTariffRem.IsDakutenCheck = False
        Me.lblPayExtcTariffRem.IsEisuCheck = False
        Me.lblPayExtcTariffRem.IsForbiddenWordsCheck = False
        Me.lblPayExtcTariffRem.IsFullByteCheck = 0
        Me.lblPayExtcTariffRem.IsHankakuCheck = False
        Me.lblPayExtcTariffRem.IsHissuCheck = False
        Me.lblPayExtcTariffRem.IsKanaCheck = False
        Me.lblPayExtcTariffRem.IsMiddleSpace = False
        Me.lblPayExtcTariffRem.IsNumericCheck = False
        Me.lblPayExtcTariffRem.IsSujiCheck = False
        Me.lblPayExtcTariffRem.IsZenkakuCheck = False
        Me.lblPayExtcTariffRem.ItemName = ""
        Me.lblPayExtcTariffRem.LineSpace = 0
        Me.lblPayExtcTariffRem.Location = New System.Drawing.Point(882, 166)
        Me.lblPayExtcTariffRem.MaxLength = 0
        Me.lblPayExtcTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblPayExtcTariffRem.MaxLineCount = 0
        Me.lblPayExtcTariffRem.Multiline = False
        Me.lblPayExtcTariffRem.Name = "lblPayExtcTariffRem"
        Me.lblPayExtcTariffRem.ReadOnly = True
        Me.lblPayExtcTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblPayExtcTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblPayExtcTariffRem.Size = New System.Drawing.Size(367, 18)
        Me.lblPayExtcTariffRem.TabIndex = 260
        Me.lblPayExtcTariffRem.TabStop = False
        Me.lblPayExtcTariffRem.TabStopSetting = False
        Me.lblPayExtcTariffRem.TextValue = ""
        Me.lblPayExtcTariffRem.UseSystemPasswordChar = False
        Me.lblPayExtcTariffRem.WidthDef = 367
        Me.lblPayExtcTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitlePayExtcTariff
        '
        Me.lblTitlePayExtcTariff.AutoSizeDef = False
        Me.lblTitlePayExtcTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayExtcTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayExtcTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayExtcTariff.EnableStatus = False
        Me.lblTitlePayExtcTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayExtcTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayExtcTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayExtcTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayExtcTariff.HeightDef = 13
        Me.lblTitlePayExtcTariff.Location = New System.Drawing.Point(651, 168)
        Me.lblTitlePayExtcTariff.Name = "lblTitlePayExtcTariff"
        Me.lblTitlePayExtcTariff.Size = New System.Drawing.Size(115, 13)
        Me.lblTitlePayExtcTariff.TabIndex = 259
        Me.lblTitlePayExtcTariff.Text = "支払割増タリフ"
        Me.lblTitlePayExtcTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayExtcTariff.TextValue = "支払割増タリフ"
        Me.lblTitlePayExtcTariff.WidthDef = 115
        '
        'txtUnsocoCdOld
        '
        Me.txtUnsocoCdOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCdOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCdOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoCdOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoCdOld.CountWrappedLine = False
        Me.txtUnsocoCdOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoCdOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCdOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCdOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCdOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCdOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoCdOld.HeightDef = 18
        Me.txtUnsocoCdOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCdOld.HissuLabelVisible = False
        Me.txtUnsocoCdOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoCdOld.IsByteCheck = 5
        Me.txtUnsocoCdOld.IsCalendarCheck = False
        Me.txtUnsocoCdOld.IsDakutenCheck = False
        Me.txtUnsocoCdOld.IsEisuCheck = False
        Me.txtUnsocoCdOld.IsForbiddenWordsCheck = False
        Me.txtUnsocoCdOld.IsFullByteCheck = 0
        Me.txtUnsocoCdOld.IsHankakuCheck = False
        Me.txtUnsocoCdOld.IsHissuCheck = False
        Me.txtUnsocoCdOld.IsKanaCheck = False
        Me.txtUnsocoCdOld.IsMiddleSpace = False
        Me.txtUnsocoCdOld.IsNumericCheck = False
        Me.txtUnsocoCdOld.IsSujiCheck = False
        Me.txtUnsocoCdOld.IsZenkakuCheck = False
        Me.txtUnsocoCdOld.ItemName = ""
        Me.txtUnsocoCdOld.LineSpace = 0
        Me.txtUnsocoCdOld.Location = New System.Drawing.Point(838, 82)
        Me.txtUnsocoCdOld.MaxLength = 5
        Me.txtUnsocoCdOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoCdOld.MaxLineCount = 0
        Me.txtUnsocoCdOld.Multiline = False
        Me.txtUnsocoCdOld.Name = "txtUnsocoCdOld"
        Me.txtUnsocoCdOld.ReadOnly = True
        Me.txtUnsocoCdOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoCdOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoCdOld.Size = New System.Drawing.Size(33, 18)
        Me.txtUnsocoCdOld.TabIndex = 261
        Me.txtUnsocoCdOld.TabStop = False
        Me.txtUnsocoCdOld.TabStopSetting = False
        Me.txtUnsocoCdOld.TextValue = ""
        Me.txtUnsocoCdOld.UseSystemPasswordChar = False
        Me.txtUnsocoCdOld.Visible = False
        Me.txtUnsocoCdOld.WidthDef = 33
        Me.txtUnsocoCdOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblPayTariffRem
        '
        Me.lblPayTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPayTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPayTariffRem.CountWrappedLine = False
        Me.lblPayTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblPayTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblPayTariffRem.HeightDef = 18
        Me.lblPayTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayTariffRem.HissuLabelVisible = False
        Me.lblPayTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblPayTariffRem.IsByteCheck = 0
        Me.lblPayTariffRem.IsCalendarCheck = False
        Me.lblPayTariffRem.IsDakutenCheck = False
        Me.lblPayTariffRem.IsEisuCheck = False
        Me.lblPayTariffRem.IsForbiddenWordsCheck = False
        Me.lblPayTariffRem.IsFullByteCheck = 0
        Me.lblPayTariffRem.IsHankakuCheck = False
        Me.lblPayTariffRem.IsHissuCheck = False
        Me.lblPayTariffRem.IsKanaCheck = False
        Me.lblPayTariffRem.IsMiddleSpace = False
        Me.lblPayTariffRem.IsNumericCheck = False
        Me.lblPayTariffRem.IsSujiCheck = False
        Me.lblPayTariffRem.IsZenkakuCheck = False
        Me.lblPayTariffRem.ItemName = ""
        Me.lblPayTariffRem.LineSpace = 0
        Me.lblPayTariffRem.Location = New System.Drawing.Point(231, 166)
        Me.lblPayTariffRem.MaxLength = 0
        Me.lblPayTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblPayTariffRem.MaxLineCount = 0
        Me.lblPayTariffRem.Multiline = False
        Me.lblPayTariffRem.Name = "lblPayTariffRem"
        Me.lblPayTariffRem.ReadOnly = True
        Me.lblPayTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblPayTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblPayTariffRem.Size = New System.Drawing.Size(433, 18)
        Me.lblPayTariffRem.TabIndex = 258
        Me.lblPayTariffRem.TabStop = False
        Me.lblPayTariffRem.TabStopSetting = False
        Me.lblPayTariffRem.TextValue = ""
        Me.lblPayTariffRem.UseSystemPasswordChar = False
        Me.lblPayTariffRem.WidthDef = 433
        Me.lblPayTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtPayExtcTariffCd
        '
        Me.txtPayExtcTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPayExtcTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPayExtcTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayExtcTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPayExtcTariffCd.CountWrappedLine = False
        Me.txtPayExtcTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtPayExtcTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPayExtcTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPayExtcTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPayExtcTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPayExtcTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtPayExtcTariffCd.HeightDef = 18
        Me.txtPayExtcTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtPayExtcTariffCd.HissuLabelVisible = False
        Me.txtPayExtcTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtPayExtcTariffCd.IsByteCheck = 10
        Me.txtPayExtcTariffCd.IsCalendarCheck = False
        Me.txtPayExtcTariffCd.IsDakutenCheck = False
        Me.txtPayExtcTariffCd.IsEisuCheck = False
        Me.txtPayExtcTariffCd.IsForbiddenWordsCheck = False
        Me.txtPayExtcTariffCd.IsFullByteCheck = 0
        Me.txtPayExtcTariffCd.IsHankakuCheck = False
        Me.txtPayExtcTariffCd.IsHissuCheck = False
        Me.txtPayExtcTariffCd.IsKanaCheck = False
        Me.txtPayExtcTariffCd.IsMiddleSpace = False
        Me.txtPayExtcTariffCd.IsNumericCheck = False
        Me.txtPayExtcTariffCd.IsSujiCheck = False
        Me.txtPayExtcTariffCd.IsZenkakuCheck = False
        Me.txtPayExtcTariffCd.ItemName = ""
        Me.txtPayExtcTariffCd.LineSpace = 0
        Me.txtPayExtcTariffCd.Location = New System.Drawing.Point(767, 166)
        Me.txtPayExtcTariffCd.MaxLength = 10
        Me.txtPayExtcTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtPayExtcTariffCd.MaxLineCount = 0
        Me.txtPayExtcTariffCd.Multiline = False
        Me.txtPayExtcTariffCd.Name = "txtPayExtcTariffCd"
        Me.txtPayExtcTariffCd.ReadOnly = False
        Me.txtPayExtcTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtPayExtcTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPayExtcTariffCd.Size = New System.Drawing.Size(131, 18)
        Me.txtPayExtcTariffCd.TabIndex = 257
        Me.txtPayExtcTariffCd.TabStopSetting = True
        Me.txtPayExtcTariffCd.TextValue = ""
        Me.txtPayExtcTariffCd.UseSystemPasswordChar = False
        Me.txtPayExtcTariffCd.WidthDef = 131
        Me.txtPayExtcTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtPayTariffCd
        '
        Me.txtPayTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPayTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPayTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPayTariffCd.CountWrappedLine = False
        Me.txtPayTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtPayTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPayTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPayTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPayTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPayTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtPayTariffCd.HeightDef = 18
        Me.txtPayTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtPayTariffCd.HissuLabelVisible = False
        Me.txtPayTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtPayTariffCd.IsByteCheck = 10
        Me.txtPayTariffCd.IsCalendarCheck = False
        Me.txtPayTariffCd.IsDakutenCheck = False
        Me.txtPayTariffCd.IsEisuCheck = False
        Me.txtPayTariffCd.IsForbiddenWordsCheck = False
        Me.txtPayTariffCd.IsFullByteCheck = 0
        Me.txtPayTariffCd.IsHankakuCheck = False
        Me.txtPayTariffCd.IsHissuCheck = False
        Me.txtPayTariffCd.IsKanaCheck = False
        Me.txtPayTariffCd.IsMiddleSpace = False
        Me.txtPayTariffCd.IsNumericCheck = False
        Me.txtPayTariffCd.IsSujiCheck = False
        Me.txtPayTariffCd.IsZenkakuCheck = False
        Me.txtPayTariffCd.ItemName = ""
        Me.txtPayTariffCd.LineSpace = 0
        Me.txtPayTariffCd.Location = New System.Drawing.Point(116, 166)
        Me.txtPayTariffCd.MaxLength = 10
        Me.txtPayTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtPayTariffCd.MaxLineCount = 0
        Me.txtPayTariffCd.Multiline = False
        Me.txtPayTariffCd.Name = "txtPayTariffCd"
        Me.txtPayTariffCd.ReadOnly = False
        Me.txtPayTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtPayTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPayTariffCd.Size = New System.Drawing.Size(131, 18)
        Me.txtPayTariffCd.TabIndex = 256
        Me.txtPayTariffCd.TabStopSetting = True
        Me.txtPayTariffCd.TextValue = ""
        Me.txtPayTariffCd.UseSystemPasswordChar = False
        Me.txtPayTariffCd.WidthDef = 131
        Me.txtPayTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitlePayTariff
        '
        Me.lblTitlePayTariff.AutoSizeDef = False
        Me.lblTitlePayTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayTariff.EnableStatus = False
        Me.lblTitlePayTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayTariff.HeightDef = 13
        Me.lblTitlePayTariff.Location = New System.Drawing.Point(22, 169)
        Me.lblTitlePayTariff.Name = "lblTitlePayTariff"
        Me.lblTitlePayTariff.Size = New System.Drawing.Size(94, 13)
        Me.lblTitlePayTariff.TabIndex = 255
        Me.lblTitlePayTariff.Text = "支払タリフ"
        Me.lblTitlePayTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayTariff.TextValue = "支払タリフ"
        Me.lblTitlePayTariff.WidthDef = 94
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
        Me.lblCustNmM.Location = New System.Drawing.Point(787, 103)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(33, 18)
        Me.lblCustNmM.TabIndex = 248
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = ""
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.Visible = False
        Me.lblCustNmM.WidthDef = 33
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTareYn
        '
        Me.lblTareYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTareYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTareYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTareYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTareYn.CountWrappedLine = False
        Me.lblTareYn.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTareYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTareYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTareYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTareYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTareYn.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTareYn.HeightDef = 18
        Me.lblTareYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTareYn.HissuLabelVisible = False
        Me.lblTareYn.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTareYn.IsByteCheck = 0
        Me.lblTareYn.IsCalendarCheck = False
        Me.lblTareYn.IsDakutenCheck = False
        Me.lblTareYn.IsEisuCheck = False
        Me.lblTareYn.IsForbiddenWordsCheck = False
        Me.lblTareYn.IsFullByteCheck = 0
        Me.lblTareYn.IsHankakuCheck = False
        Me.lblTareYn.IsHissuCheck = False
        Me.lblTareYn.IsKanaCheck = False
        Me.lblTareYn.IsMiddleSpace = False
        Me.lblTareYn.IsNumericCheck = False
        Me.lblTareYn.IsSujiCheck = False
        Me.lblTareYn.IsZenkakuCheck = False
        Me.lblTareYn.ItemName = ""
        Me.lblTareYn.LineSpace = 0
        Me.lblTareYn.Location = New System.Drawing.Point(821, 82)
        Me.lblTareYn.MaxLength = 0
        Me.lblTareYn.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTareYn.MaxLineCount = 0
        Me.lblTareYn.Multiline = False
        Me.lblTareYn.Name = "lblTareYn"
        Me.lblTareYn.ReadOnly = True
        Me.lblTareYn.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTareYn.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTareYn.Size = New System.Drawing.Size(33, 18)
        Me.lblTareYn.TabIndex = 245
        Me.lblTareYn.TabStop = False
        Me.lblTareYn.TabStopSetting = False
        Me.lblTareYn.TextValue = ""
        Me.lblTareYn.UseSystemPasswordChar = False
        Me.lblTareYn.Visible = False
        Me.lblTareYn.WidthDef = 33
        Me.lblTareYn.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnsoBrNm
        '
        Me.lblUnsoBrNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoBrNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoBrNm.CountWrappedLine = False
        Me.lblUnsoBrNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoBrNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoBrNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoBrNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoBrNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoBrNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoBrNm.HeightDef = 18
        Me.lblUnsoBrNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrNm.HissuLabelVisible = False
        Me.lblUnsoBrNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsoBrNm.IsByteCheck = 0
        Me.lblUnsoBrNm.IsCalendarCheck = False
        Me.lblUnsoBrNm.IsDakutenCheck = False
        Me.lblUnsoBrNm.IsEisuCheck = False
        Me.lblUnsoBrNm.IsForbiddenWordsCheck = False
        Me.lblUnsoBrNm.IsFullByteCheck = 0
        Me.lblUnsoBrNm.IsHankakuCheck = False
        Me.lblUnsoBrNm.IsHissuCheck = False
        Me.lblUnsoBrNm.IsKanaCheck = False
        Me.lblUnsoBrNm.IsMiddleSpace = False
        Me.lblUnsoBrNm.IsNumericCheck = False
        Me.lblUnsoBrNm.IsSujiCheck = False
        Me.lblUnsoBrNm.IsZenkakuCheck = False
        Me.lblUnsoBrNm.ItemName = ""
        Me.lblUnsoBrNm.LineSpace = 0
        Me.lblUnsoBrNm.Location = New System.Drawing.Point(804, 82)
        Me.lblUnsoBrNm.MaxLength = 0
        Me.lblUnsoBrNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoBrNm.MaxLineCount = 0
        Me.lblUnsoBrNm.Multiline = False
        Me.lblUnsoBrNm.Name = "lblUnsoBrNm"
        Me.lblUnsoBrNm.ReadOnly = True
        Me.lblUnsoBrNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoBrNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoBrNm.Size = New System.Drawing.Size(33, 18)
        Me.lblUnsoBrNm.TabIndex = 247
        Me.lblUnsoBrNm.TabStop = False
        Me.lblUnsoBrNm.TabStopSetting = False
        Me.lblUnsoBrNm.TextValue = ""
        Me.lblUnsoBrNm.UseSystemPasswordChar = False
        Me.lblUnsoBrNm.Visible = False
        Me.lblUnsoBrNm.WidthDef = 33
        Me.lblUnsoBrNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblUnsoNm.Location = New System.Drawing.Point(787, 82)
        Me.lblUnsoNm.MaxLength = 0
        Me.lblUnsoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoNm.MaxLineCount = 0
        Me.lblUnsoNm.Multiline = False
        Me.lblUnsoNm.Name = "lblUnsoNm"
        Me.lblUnsoNm.ReadOnly = True
        Me.lblUnsoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoNm.Size = New System.Drawing.Size(33, 18)
        Me.lblUnsoNm.TabIndex = 246
        Me.lblUnsoNm.TabStop = False
        Me.lblUnsoNm.TabStopSetting = False
        Me.lblUnsoNm.TextValue = ""
        Me.lblUnsoNm.UseSystemPasswordChar = False
        Me.lblUnsoNm.Visible = False
        Me.lblUnsoNm.WidthDef = 33
        Me.lblUnsoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSplitFlg
        '
        Me.lblSplitFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSplitFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSplitFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSplitFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSplitFlg.CountWrappedLine = False
        Me.lblSplitFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSplitFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSplitFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSplitFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSplitFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSplitFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSplitFlg.HeightDef = 18
        Me.lblSplitFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSplitFlg.HissuLabelVisible = False
        Me.lblSplitFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSplitFlg.IsByteCheck = 0
        Me.lblSplitFlg.IsCalendarCheck = False
        Me.lblSplitFlg.IsDakutenCheck = False
        Me.lblSplitFlg.IsEisuCheck = False
        Me.lblSplitFlg.IsForbiddenWordsCheck = False
        Me.lblSplitFlg.IsFullByteCheck = 0
        Me.lblSplitFlg.IsHankakuCheck = False
        Me.lblSplitFlg.IsHissuCheck = False
        Me.lblSplitFlg.IsKanaCheck = False
        Me.lblSplitFlg.IsMiddleSpace = False
        Me.lblSplitFlg.IsNumericCheck = False
        Me.lblSplitFlg.IsSujiCheck = False
        Me.lblSplitFlg.IsZenkakuCheck = False
        Me.lblSplitFlg.ItemName = ""
        Me.lblSplitFlg.LineSpace = 0
        Me.lblSplitFlg.Location = New System.Drawing.Point(1070, 389)
        Me.lblSplitFlg.MaxLength = 0
        Me.lblSplitFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSplitFlg.MaxLineCount = 0
        Me.lblSplitFlg.Multiline = False
        Me.lblSplitFlg.Name = "lblSplitFlg"
        Me.lblSplitFlg.ReadOnly = True
        Me.lblSplitFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSplitFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSplitFlg.Size = New System.Drawing.Size(50, 18)
        Me.lblSplitFlg.TabIndex = 244
        Me.lblSplitFlg.TabStop = False
        Me.lblSplitFlg.TabStopSetting = False
        Me.lblSplitFlg.TextValue = ""
        Me.lblSplitFlg.UseSystemPasswordChar = False
        Me.lblSplitFlg.Visible = False
        Me.lblSplitFlg.WidthDef = 50
        Me.lblSplitFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.cmbTax.HissuLabelVisible = True
        Me.cmbTax.InsertWildCard = True
        Me.cmbTax.IsForbiddenWordsCheck = False
        Me.cmbTax.IsHissuCheck = True
        Me.cmbTax.ItemName = ""
        Me.cmbTax.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTax.Location = New System.Drawing.Point(795, 40)
        Me.cmbTax.Name = "cmbTax"
        Me.cmbTax.ReadOnly = False
        Me.cmbTax.SelectedIndex = -1
        Me.cmbTax.SelectedItem = Nothing
        Me.cmbTax.SelectedText = ""
        Me.cmbTax.SelectedValue = ""
        Me.cmbTax.Size = New System.Drawing.Size(103, 18)
        Me.cmbTax.TabIndex = 243
        Me.cmbTax.TabStopSetting = True
        Me.cmbTax.TextValue = ""
        Me.cmbTax.Value1 = Nothing
        Me.cmbTax.Value2 = Nothing
        Me.cmbTax.Value3 = Nothing
        Me.cmbTax.ValueMember = Nothing
        Me.cmbTax.WidthDef = 103
        '
        'lblTitleTax
        '
        Me.lblTitleTax.AutoSizeDef = False
        Me.lblTitleTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTax.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTax.EnableStatus = False
        Me.lblTitleTax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTax.HeightDef = 13
        Me.lblTitleTax.Location = New System.Drawing.Point(732, 43)
        Me.lblTitleTax.Name = "lblTitleTax"
        Me.lblTitleTax.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTax.TabIndex = 242
        Me.lblTitleTax.Text = "課税区分"
        Me.lblTitleTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTax.TextValue = "課税区分"
        Me.lblTitleTax.WidthDef = 63
        '
        'lblCalcKbn
        '
        Me.lblCalcKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCalcKbn.CountWrappedLine = False
        Me.lblCalcKbn.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCalcKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCalcKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCalcKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCalcKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCalcKbn.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCalcKbn.HeightDef = 18
        Me.lblCalcKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.HissuLabelVisible = False
        Me.lblCalcKbn.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCalcKbn.IsByteCheck = 0
        Me.lblCalcKbn.IsCalendarCheck = False
        Me.lblCalcKbn.IsDakutenCheck = False
        Me.lblCalcKbn.IsEisuCheck = False
        Me.lblCalcKbn.IsForbiddenWordsCheck = False
        Me.lblCalcKbn.IsFullByteCheck = 0
        Me.lblCalcKbn.IsHankakuCheck = False
        Me.lblCalcKbn.IsHissuCheck = False
        Me.lblCalcKbn.IsKanaCheck = False
        Me.lblCalcKbn.IsMiddleSpace = False
        Me.lblCalcKbn.IsNumericCheck = False
        Me.lblCalcKbn.IsSujiCheck = False
        Me.lblCalcKbn.IsZenkakuCheck = False
        Me.lblCalcKbn.ItemName = ""
        Me.lblCalcKbn.LineSpace = 0
        Me.lblCalcKbn.Location = New System.Drawing.Point(1018, 389)
        Me.lblCalcKbn.MaxLength = 0
        Me.lblCalcKbn.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCalcKbn.MaxLineCount = 0
        Me.lblCalcKbn.Multiline = False
        Me.lblCalcKbn.Name = "lblCalcKbn"
        Me.lblCalcKbn.ReadOnly = True
        Me.lblCalcKbn.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCalcKbn.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCalcKbn.Size = New System.Drawing.Size(50, 18)
        Me.lblCalcKbn.TabIndex = 241
        Me.lblCalcKbn.TabStop = False
        Me.lblCalcKbn.TabStopSetting = False
        Me.lblCalcKbn.TextValue = ""
        Me.lblCalcKbn.UseSystemPasswordChar = False
        Me.lblCalcKbn.Visible = False
        Me.lblCalcKbn.WidthDef = 50
        Me.lblCalcKbn.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnKeisan
        '
        Me.btnKeisan.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKeisan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKeisan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnKeisan.EnableStatus = True
        Me.btnKeisan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKeisan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKeisan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKeisan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKeisan.HeightDef = 22
        Me.btnKeisan.Location = New System.Drawing.Point(949, 368)
        Me.btnKeisan.Name = "btnKeisan"
        Me.btnKeisan.Size = New System.Drawing.Size(70, 22)
        Me.btnKeisan.TabIndex = 240
        Me.btnKeisan.TabStopSetting = True
        Me.btnKeisan.Text = "計算"
        Me.btnKeisan.TextValue = "計算"
        Me.btnKeisan.UseVisualStyleBackColor = True
        Me.btnKeisan.WidthDef = 70
        '
        'txtUnsoComment
        '
        Me.txtUnsoComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsoComment.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsoComment.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsoComment.CountWrappedLine = False
        Me.txtUnsoComment.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsoComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoComment.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsoComment.HeightDef = 18
        Me.txtUnsoComment.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoComment.HissuLabelVisible = False
        Me.txtUnsoComment.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtUnsoComment.IsByteCheck = 100
        Me.txtUnsoComment.IsCalendarCheck = False
        Me.txtUnsoComment.IsDakutenCheck = False
        Me.txtUnsoComment.IsEisuCheck = False
        Me.txtUnsoComment.IsForbiddenWordsCheck = False
        Me.txtUnsoComment.IsFullByteCheck = 0
        Me.txtUnsoComment.IsHankakuCheck = False
        Me.txtUnsoComment.IsHissuCheck = False
        Me.txtUnsoComment.IsKanaCheck = False
        Me.txtUnsoComment.IsMiddleSpace = False
        Me.txtUnsoComment.IsNumericCheck = False
        Me.txtUnsoComment.IsSujiCheck = False
        Me.txtUnsoComment.IsZenkakuCheck = False
        Me.txtUnsoComment.ItemName = ""
        Me.txtUnsoComment.LineSpace = 0
        Me.txtUnsoComment.Location = New System.Drawing.Point(115, 391)
        Me.txtUnsoComment.MaxLength = 100
        Me.txtUnsoComment.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsoComment.MaxLineCount = 0
        Me.txtUnsoComment.Multiline = False
        Me.txtUnsoComment.Name = "txtUnsoComment"
        Me.txtUnsoComment.ReadOnly = False
        Me.txtUnsoComment.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsoComment.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsoComment.Size = New System.Drawing.Size(842, 18)
        Me.txtUnsoComment.TabIndex = 237
        Me.txtUnsoComment.TabStopSetting = True
        Me.txtUnsoComment.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtUnsoComment.UseSystemPasswordChar = False
        Me.txtUnsoComment.WidthDef = 842
        Me.txtUnsoComment.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoComment
        '
        Me.lblTitleUnsoComment.AutoSize = True
        Me.lblTitleUnsoComment.AutoSizeDef = True
        Me.lblTitleUnsoComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoComment.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoComment.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoComment.EnableStatus = False
        Me.lblTitleUnsoComment.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoComment.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoComment.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoComment.HeightDef = 13
        Me.lblTitleUnsoComment.Location = New System.Drawing.Point(24, 394)
        Me.lblTitleUnsoComment.Name = "lblTitleUnsoComment"
        Me.lblTitleUnsoComment.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleUnsoComment.TabIndex = 236
        Me.lblTitleUnsoComment.Text = "運送コメント"
        Me.lblTitleUnsoComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoComment.TextValue = "運送コメント"
        Me.lblTitleUnsoComment.WidthDef = 91
        '
        'lblTitleKg1
        '
        Me.lblTitleKg1.AutoSize = True
        Me.lblTitleKg1.AutoSizeDef = True
        Me.lblTitleKg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg1.EnableStatus = False
        Me.lblTitleKg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg1.HeightDef = 13
        Me.lblTitleKg1.Location = New System.Drawing.Point(431, 390)
        Me.lblTitleKg1.Name = "lblTitleKg1"
        Me.lblTitleKg1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg1.TabIndex = 234
        Me.lblTitleKg1.Text = "KG"
        Me.lblTitleKg1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg1.TextValue = "KG"
        Me.lblTitleKg1.WidthDef = 21
        '
        'cmbUnsoCntUt
        '
        Me.cmbUnsoCntUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnsoCntUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnsoCntUt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbUnsoCntUt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbUnsoCntUt.DataCode = "N001"
        Me.cmbUnsoCntUt.DataSource = Nothing
        Me.cmbUnsoCntUt.DisplayMember = Nothing
        Me.cmbUnsoCntUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnsoCntUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnsoCntUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnsoCntUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnsoCntUt.HeightDef = 18
        Me.cmbUnsoCntUt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbUnsoCntUt.HissuLabelVisible = False
        Me.cmbUnsoCntUt.InsertWildCard = True
        Me.cmbUnsoCntUt.IsForbiddenWordsCheck = False
        Me.cmbUnsoCntUt.IsHissuCheck = False
        Me.cmbUnsoCntUt.ItemName = ""
        Me.cmbUnsoCntUt.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbUnsoCntUt.Location = New System.Drawing.Point(527, 370)
        Me.cmbUnsoCntUt.Name = "cmbUnsoCntUt"
        Me.cmbUnsoCntUt.ReadOnly = False
        Me.cmbUnsoCntUt.SelectedIndex = -1
        Me.cmbUnsoCntUt.SelectedItem = Nothing
        Me.cmbUnsoCntUt.SelectedText = ""
        Me.cmbUnsoCntUt.SelectedValue = ""
        Me.cmbUnsoCntUt.Size = New System.Drawing.Size(129, 18)
        Me.cmbUnsoCntUt.TabIndex = 150
        Me.cmbUnsoCntUt.TabStopSetting = True
        Me.cmbUnsoCntUt.TextValue = ""
        Me.cmbUnsoCntUt.Value1 = Nothing
        Me.cmbUnsoCntUt.Value2 = Nothing
        Me.cmbUnsoCntUt.Value3 = Nothing
        Me.cmbUnsoCntUt.ValueMember = Nothing
        Me.cmbUnsoCntUt.WidthDef = 129
        '
        'cmbThermoKbn
        '
        Me.cmbThermoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbThermoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbThermoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbThermoKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbThermoKbn.DataCode = "U006"
        Me.cmbThermoKbn.DataSource = Nothing
        Me.cmbThermoKbn.DisplayMember = Nothing
        Me.cmbThermoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbThermoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbThermoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbThermoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbThermoKbn.HeightDef = 18
        Me.cmbThermoKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbThermoKbn.HissuLabelVisible = False
        Me.cmbThermoKbn.InsertWildCard = True
        Me.cmbThermoKbn.IsForbiddenWordsCheck = False
        Me.cmbThermoKbn.IsHissuCheck = False
        Me.cmbThermoKbn.ItemName = ""
        Me.cmbThermoKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbThermoKbn.Location = New System.Drawing.Point(794, 370)
        Me.cmbThermoKbn.Name = "cmbThermoKbn"
        Me.cmbThermoKbn.ReadOnly = False
        Me.cmbThermoKbn.SelectedIndex = -1
        Me.cmbThermoKbn.SelectedItem = Nothing
        Me.cmbThermoKbn.SelectedText = ""
        Me.cmbThermoKbn.SelectedValue = ""
        Me.cmbThermoKbn.Size = New System.Drawing.Size(163, 18)
        Me.cmbThermoKbn.TabIndex = 148
        Me.cmbThermoKbn.TabStopSetting = True
        Me.cmbThermoKbn.TextValue = ""
        Me.cmbThermoKbn.Value1 = Nothing
        Me.cmbThermoKbn.Value2 = Nothing
        Me.cmbThermoKbn.Value3 = Nothing
        Me.cmbThermoKbn.ValueMember = Nothing
        Me.cmbThermoKbn.WidthDef = 163
        '
        'lblTitleThermoKbn
        '
        Me.lblTitleThermoKbn.AutoSizeDef = False
        Me.lblTitleThermoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleThermoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleThermoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleThermoKbn.EnableStatus = False
        Me.lblTitleThermoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleThermoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleThermoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleThermoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleThermoKbn.HeightDef = 13
        Me.lblTitleThermoKbn.Location = New System.Drawing.Point(675, 373)
        Me.lblTitleThermoKbn.Name = "lblTitleThermoKbn"
        Me.lblTitleThermoKbn.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleThermoKbn.TabIndex = 147
        Me.lblTitleThermoKbn.Text = "運送温度管理区分"
        Me.lblTitleThermoKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleThermoKbn.TextValue = "運送温度管理区分"
        Me.lblTitleThermoKbn.WidthDef = 119
        '
        'lblTitleUnsoCntUt
        '
        Me.lblTitleUnsoCntUt.AutoSizeDef = False
        Me.lblTitleUnsoCntUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoCntUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoCntUt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoCntUt.EnableStatus = False
        Me.lblTitleUnsoCntUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoCntUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoCntUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoCntUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoCntUt.HeightDef = 13
        Me.lblTitleUnsoCntUt.Location = New System.Drawing.Point(464, 373)
        Me.lblTitleUnsoCntUt.Name = "lblTitleUnsoCntUt"
        Me.lblTitleUnsoCntUt.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnsoCntUt.TabIndex = 145
        Me.lblTitleUnsoCntUt.Text = "個数単位"
        Me.lblTitleUnsoCntUt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoCntUt.TextValue = "個数単位"
        Me.lblTitleUnsoCntUt.WidthDef = 63
        '
        'numUnsoWtL
        '
        Me.numUnsoWtL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoWtL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoWtL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoWtL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoWtL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWtL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWtL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWtL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWtL.HeightDef = 18
        Me.numUnsoWtL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoWtL.HissuLabelVisible = False
        Me.numUnsoWtL.IsHissuCheck = False
        Me.numUnsoWtL.IsRangeCheck = False
        Me.numUnsoWtL.ItemName = ""
        Me.numUnsoWtL.Location = New System.Drawing.Point(316, 370)
        Me.numUnsoWtL.Name = "numUnsoWtL"
        Me.numUnsoWtL.ReadOnly = False
        Me.numUnsoWtL.Size = New System.Drawing.Size(129, 18)
        Me.numUnsoWtL.TabIndex = 144
        Me.numUnsoWtL.TabStopSetting = True
        Me.numUnsoWtL.TextValue = "0"
        Me.numUnsoWtL.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnsoWtL.WidthDef = 129
        '
        'lblTitleUnsoWtL
        '
        Me.lblTitleUnsoWtL.AutoSizeDef = False
        Me.lblTitleUnsoWtL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoWtL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoWtL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoWtL.EnableStatus = False
        Me.lblTitleUnsoWtL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoWtL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoWtL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoWtL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoWtL.HeightDef = 13
        Me.lblTitleUnsoWtL.Location = New System.Drawing.Point(230, 373)
        Me.lblTitleUnsoWtL.Name = "lblTitleUnsoWtL"
        Me.lblTitleUnsoWtL.Size = New System.Drawing.Size(86, 13)
        Me.lblTitleUnsoWtL.TabIndex = 143
        Me.lblTitleUnsoWtL.Text = "運送重量"
        Me.lblTitleUnsoWtL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoWtL.TextValue = "運送重量"
        Me.lblTitleUnsoWtL.WidthDef = 86
        '
        'numUnsoPkgCnt
        '
        Me.numUnsoPkgCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoPkgCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoPkgCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoPkgCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoPkgCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoPkgCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoPkgCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoPkgCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoPkgCnt.HeightDef = 18
        Me.numUnsoPkgCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoPkgCnt.HissuLabelVisible = False
        Me.numUnsoPkgCnt.IsHissuCheck = False
        Me.numUnsoPkgCnt.IsRangeCheck = False
        Me.numUnsoPkgCnt.ItemName = ""
        Me.numUnsoPkgCnt.Location = New System.Drawing.Point(115, 370)
        Me.numUnsoPkgCnt.Name = "numUnsoPkgCnt"
        Me.numUnsoPkgCnt.ReadOnly = False
        Me.numUnsoPkgCnt.Size = New System.Drawing.Size(129, 18)
        Me.numUnsoPkgCnt.TabIndex = 142
        Me.numUnsoPkgCnt.TabStopSetting = True
        Me.numUnsoPkgCnt.TextValue = "0"
        Me.numUnsoPkgCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnsoPkgCnt.WidthDef = 129
        '
        'lblTitleUnsoPkgCnt
        '
        Me.lblTitleUnsoPkgCnt.AutoSizeDef = False
        Me.lblTitleUnsoPkgCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoPkgCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoPkgCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoPkgCnt.EnableStatus = False
        Me.lblTitleUnsoPkgCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoPkgCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoPkgCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoPkgCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoPkgCnt.HeightDef = 13
        Me.lblTitleUnsoPkgCnt.Location = New System.Drawing.Point(12, 373)
        Me.lblTitleUnsoPkgCnt.Name = "lblTitleUnsoPkgCnt"
        Me.lblTitleUnsoPkgCnt.Size = New System.Drawing.Size(103, 13)
        Me.lblTitleUnsoPkgCnt.TabIndex = 141
        Me.lblTitleUnsoPkgCnt.Text = "運送梱包数"
        Me.lblTitleUnsoPkgCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoPkgCnt.TextValue = "運送梱包数"
        Me.lblTitleUnsoPkgCnt.WidthDef = 103
        '
        'pnlDestOrigin
        '
        Me.pnlDestOrigin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlDestOrigin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlDestOrigin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlDestOrigin.Controls.Add(Me.txtTel)
        Me.pnlDestOrigin.Controls.Add(Me.lblTel)
        Me.pnlDestOrigin.Controls.Add(Me.beforeAutoDenpKbn)
        Me.pnlDestOrigin.Controls.Add(Me.lblAutoDenpNo)
        Me.pnlDestOrigin.Controls.Add(Me.cmbAutoDenpKbn)
        Me.pnlDestOrigin.Controls.Add(Me.titleAutoDenpNo)
        Me.pnlDestOrigin.Controls.Add(Me.lblTakSize)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleDest)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleOrig)
        Me.pnlDestOrigin.Controls.Add(Me.lblAreaNm)
        Me.pnlDestOrigin.Controls.Add(Me.txtAreaCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleArea)
        Me.pnlDestOrigin.Controls.Add(Me.lblDestAdd2)
        Me.pnlDestOrigin.Controls.Add(Me.lblDestAdd1)
        Me.pnlDestOrigin.Controls.Add(Me.lblZipNo)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleAdd)
        Me.pnlDestOrigin.Controls.Add(Me.lblDestJisCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleDestJisCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblDestNm)
        Me.pnlDestOrigin.Controls.Add(Me.txtDestAdd3)
        Me.pnlDestOrigin.Controls.Add(Me.txtDestCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleJiDestTime)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleDestTime)
        Me.pnlDestOrigin.Controls.Add(Me.imdDestDate)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleDestDate)
        Me.pnlDestOrigin.Controls.Add(Me.lblOrigJisCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleOrigJisCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblOrigNm)
        Me.pnlDestOrigin.Controls.Add(Me.txtOrigCd)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleOrigTime)
        Me.pnlDestOrigin.Controls.Add(Me.imdOrigDate)
        Me.pnlDestOrigin.Controls.Add(Me.lblTitleOrigDate)
        Me.pnlDestOrigin.Controls.Add(Me.txtDestTime)
        Me.pnlDestOrigin.Controls.Add(Me.txtOrigTime)
        Me.pnlDestOrigin.Controls.Add(Me.txtJiDestTime)
        Me.pnlDestOrigin.EnableStatus = False
        Me.pnlDestOrigin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlDestOrigin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlDestOrigin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlDestOrigin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlDestOrigin.HeightDef = 183
        Me.pnlDestOrigin.Location = New System.Drawing.Point(6, 185)
        Me.pnlDestOrigin.Name = "pnlDestOrigin"
        Me.pnlDestOrigin.Size = New System.Drawing.Size(1236, 183)
        Me.pnlDestOrigin.TabIndex = 101
        Me.pnlDestOrigin.TabStop = False
        Me.pnlDestOrigin.TextValue = ""
        Me.pnlDestOrigin.WidthDef = 1236
        '
        'txtTel
        '
        Me.txtTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTel.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTel.CountWrappedLine = False
        Me.txtTel.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTel.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTel.HeightDef = 18
        Me.txtTel.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTel.HissuLabelVisible = False
        Me.txtTel.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtTel.IsByteCheck = 20
        Me.txtTel.IsCalendarCheck = False
        Me.txtTel.IsDakutenCheck = False
        Me.txtTel.IsEisuCheck = False
        Me.txtTel.IsForbiddenWordsCheck = False
        Me.txtTel.IsFullByteCheck = 0
        Me.txtTel.IsHankakuCheck = False
        Me.txtTel.IsHissuCheck = False
        Me.txtTel.IsKanaCheck = False
        Me.txtTel.IsMiddleSpace = False
        Me.txtTel.IsNumericCheck = False
        Me.txtTel.IsSujiCheck = False
        Me.txtTel.IsZenkakuCheck = False
        Me.txtTel.ItemName = ""
        Me.txtTel.LineSpace = 0
        Me.txtTel.Location = New System.Drawing.Point(652, 110)
        Me.txtTel.MaxLength = 20
        Me.txtTel.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTel.MaxLineCount = 0
        Me.txtTel.Multiline = False
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = False
        Me.txtTel.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTel.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTel.Size = New System.Drawing.Size(118, 18)
        Me.txtTel.TabIndex = 446
        Me.txtTel.TabStopSetting = True
        Me.txtTel.TextValue = ""
        Me.txtTel.UseSystemPasswordChar = False
        Me.txtTel.WidthDef = 118
        Me.txtTel.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTel
        '
        Me.lblTel.AutoSize = True
        Me.lblTel.AutoSizeDef = True
        Me.lblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTel.EnableStatus = False
        Me.lblTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTel.HeightDef = 13
        Me.lblTel.Location = New System.Drawing.Point(587, 112)
        Me.lblTel.MinimumSize = New System.Drawing.Size(60, 0)
        Me.lblTel.Name = "lblTel"
        Me.lblTel.Size = New System.Drawing.Size(63, 13)
        Me.lblTel.TabIndex = 447
        Me.lblTel.Text = "電話番号"
        Me.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTel.TextValue = "電話番号"
        Me.lblTel.WidthDef = 63
        '
        'beforeAutoDenpKbn
        '
        Me.beforeAutoDenpKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.beforeAutoDenpKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.beforeAutoDenpKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.beforeAutoDenpKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.beforeAutoDenpKbn.DataCode = "O010"
        Me.beforeAutoDenpKbn.DataSource = Nothing
        Me.beforeAutoDenpKbn.DisplayMember = Nothing
        Me.beforeAutoDenpKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.beforeAutoDenpKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.beforeAutoDenpKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.beforeAutoDenpKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.beforeAutoDenpKbn.HeightDef = 18
        Me.beforeAutoDenpKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.beforeAutoDenpKbn.HissuLabelVisible = False
        Me.beforeAutoDenpKbn.InsertWildCard = True
        Me.beforeAutoDenpKbn.IsForbiddenWordsCheck = False
        Me.beforeAutoDenpKbn.IsHissuCheck = False
        Me.beforeAutoDenpKbn.ItemName = ""
        Me.beforeAutoDenpKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.beforeAutoDenpKbn.Location = New System.Drawing.Point(1181, 89)
        Me.beforeAutoDenpKbn.Name = "beforeAutoDenpKbn"
        Me.beforeAutoDenpKbn.ReadOnly = True
        Me.beforeAutoDenpKbn.SelectedIndex = -1
        Me.beforeAutoDenpKbn.SelectedItem = Nothing
        Me.beforeAutoDenpKbn.SelectedText = ""
        Me.beforeAutoDenpKbn.SelectedValue = ""
        Me.beforeAutoDenpKbn.Size = New System.Drawing.Size(49, 18)
        Me.beforeAutoDenpKbn.TabIndex = 445
        Me.beforeAutoDenpKbn.TabStop = False
        Me.beforeAutoDenpKbn.TabStopSetting = False
        Me.beforeAutoDenpKbn.TextValue = ""
        Me.beforeAutoDenpKbn.Value1 = Nothing
        Me.beforeAutoDenpKbn.Value2 = Nothing
        Me.beforeAutoDenpKbn.Value3 = Nothing
        Me.beforeAutoDenpKbn.ValueMember = Nothing
        Me.beforeAutoDenpKbn.Visible = False
        Me.beforeAutoDenpKbn.WidthDef = 49
        '
        'lblAutoDenpNo
        '
        Me.lblAutoDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAutoDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAutoDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAutoDenpNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAutoDenpNo.CountWrappedLine = False
        Me.lblAutoDenpNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblAutoDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAutoDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAutoDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAutoDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAutoDenpNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblAutoDenpNo.HeightDef = 18
        Me.lblAutoDenpNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAutoDenpNo.HissuLabelVisible = False
        Me.lblAutoDenpNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.lblAutoDenpNo.IsByteCheck = 20
        Me.lblAutoDenpNo.IsCalendarCheck = False
        Me.lblAutoDenpNo.IsDakutenCheck = False
        Me.lblAutoDenpNo.IsEisuCheck = False
        Me.lblAutoDenpNo.IsForbiddenWordsCheck = False
        Me.lblAutoDenpNo.IsFullByteCheck = 0
        Me.lblAutoDenpNo.IsHankakuCheck = False
        Me.lblAutoDenpNo.IsHissuCheck = False
        Me.lblAutoDenpNo.IsKanaCheck = False
        Me.lblAutoDenpNo.IsMiddleSpace = False
        Me.lblAutoDenpNo.IsNumericCheck = False
        Me.lblAutoDenpNo.IsSujiCheck = False
        Me.lblAutoDenpNo.IsZenkakuCheck = False
        Me.lblAutoDenpNo.ItemName = ""
        Me.lblAutoDenpNo.LineSpace = 0
        Me.lblAutoDenpNo.Location = New System.Drawing.Point(979, 68)
        Me.lblAutoDenpNo.MaxLength = 20
        Me.lblAutoDenpNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblAutoDenpNo.MaxLineCount = 0
        Me.lblAutoDenpNo.Multiline = False
        Me.lblAutoDenpNo.Name = "lblAutoDenpNo"
        Me.lblAutoDenpNo.ReadOnly = True
        Me.lblAutoDenpNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblAutoDenpNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblAutoDenpNo.Size = New System.Drawing.Size(251, 18)
        Me.lblAutoDenpNo.TabIndex = 443
        Me.lblAutoDenpNo.TabStop = False
        Me.lblAutoDenpNo.TabStopSetting = False
        Me.lblAutoDenpNo.TextValue = ""
        Me.lblAutoDenpNo.UseSystemPasswordChar = False
        Me.lblAutoDenpNo.WidthDef = 251
        Me.lblAutoDenpNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbAutoDenpKbn
        '
        Me.cmbAutoDenpKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbAutoDenpKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbAutoDenpKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbAutoDenpKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbAutoDenpKbn.DataCode = "O010"
        Me.cmbAutoDenpKbn.DataSource = Nothing
        Me.cmbAutoDenpKbn.DisplayMember = Nothing
        Me.cmbAutoDenpKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAutoDenpKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAutoDenpKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAutoDenpKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAutoDenpKbn.HeightDef = 18
        Me.cmbAutoDenpKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbAutoDenpKbn.HissuLabelVisible = False
        Me.cmbAutoDenpKbn.InsertWildCard = True
        Me.cmbAutoDenpKbn.IsForbiddenWordsCheck = False
        Me.cmbAutoDenpKbn.IsHissuCheck = False
        Me.cmbAutoDenpKbn.ItemName = ""
        Me.cmbAutoDenpKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbAutoDenpKbn.Location = New System.Drawing.Point(889, 68)
        Me.cmbAutoDenpKbn.Name = "cmbAutoDenpKbn"
        Me.cmbAutoDenpKbn.ReadOnly = True
        Me.cmbAutoDenpKbn.SelectedIndex = -1
        Me.cmbAutoDenpKbn.SelectedItem = Nothing
        Me.cmbAutoDenpKbn.SelectedText = ""
        Me.cmbAutoDenpKbn.SelectedValue = ""
        Me.cmbAutoDenpKbn.Size = New System.Drawing.Size(92, 18)
        Me.cmbAutoDenpKbn.TabIndex = 444
        Me.cmbAutoDenpKbn.TabStop = False
        Me.cmbAutoDenpKbn.TabStopSetting = False
        Me.cmbAutoDenpKbn.TextValue = ""
        Me.cmbAutoDenpKbn.Value1 = Nothing
        Me.cmbAutoDenpKbn.Value2 = Nothing
        Me.cmbAutoDenpKbn.Value3 = Nothing
        Me.cmbAutoDenpKbn.ValueMember = Nothing
        Me.cmbAutoDenpKbn.WidthDef = 92
        '
        'titleAutoDenpNo
        '
        Me.titleAutoDenpNo.AutoSizeDef = False
        Me.titleAutoDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.titleAutoDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.titleAutoDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.titleAutoDenpNo.EnableStatus = False
        Me.titleAutoDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.titleAutoDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.titleAutoDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.titleAutoDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.titleAutoDenpNo.HeightDef = 13
        Me.titleAutoDenpNo.Location = New System.Drawing.Point(747, 71)
        Me.titleAutoDenpNo.Name = "titleAutoDenpNo"
        Me.titleAutoDenpNo.Size = New System.Drawing.Size(140, 13)
        Me.titleAutoDenpNo.TabIndex = 442
        Me.titleAutoDenpNo.Text = "自動送り状番号"
        Me.titleAutoDenpNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.titleAutoDenpNo.TextValue = "自動送り状番号"
        Me.titleAutoDenpNo.WidthDef = 140
        '
        'lblTakSize
        '
        Me.lblTakSize.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTakSize.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTakSize.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTakSize.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTakSize.CountWrappedLine = False
        Me.lblTakSize.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTakSize.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTakSize.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTakSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTakSize.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTakSize.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTakSize.HeightDef = 18
        Me.lblTakSize.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTakSize.HissuLabelVisible = False
        Me.lblTakSize.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTakSize.IsByteCheck = 0
        Me.lblTakSize.IsCalendarCheck = False
        Me.lblTakSize.IsDakutenCheck = False
        Me.lblTakSize.IsEisuCheck = False
        Me.lblTakSize.IsForbiddenWordsCheck = False
        Me.lblTakSize.IsFullByteCheck = 0
        Me.lblTakSize.IsHankakuCheck = False
        Me.lblTakSize.IsHissuCheck = False
        Me.lblTakSize.IsKanaCheck = False
        Me.lblTakSize.IsMiddleSpace = False
        Me.lblTakSize.IsNumericCheck = False
        Me.lblTakSize.IsSujiCheck = False
        Me.lblTakSize.IsZenkakuCheck = False
        Me.lblTakSize.ItemName = ""
        Me.lblTakSize.LineSpace = 0
        Me.lblTakSize.Location = New System.Drawing.Point(616, 152)
        Me.lblTakSize.MaxLength = 0
        Me.lblTakSize.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTakSize.MaxLineCount = 0
        Me.lblTakSize.Multiline = False
        Me.lblTakSize.Name = "lblTakSize"
        Me.lblTakSize.ReadOnly = True
        Me.lblTakSize.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTakSize.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTakSize.Size = New System.Drawing.Size(433, 18)
        Me.lblTakSize.TabIndex = 152
        Me.lblTakSize.TabStop = False
        Me.lblTakSize.TabStopSetting = False
        Me.lblTakSize.TextValue = ""
        Me.lblTakSize.UseSystemPasswordChar = False
        Me.lblTakSize.Visible = False
        Me.lblTakSize.WidthDef = 433
        Me.lblTakSize.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDest
        '
        Me.lblTitleDest.AutoSizeDef = False
        Me.lblTitleDest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDest.EnableStatus = False
        Me.lblTitleDest.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.HeightDef = 13
        Me.lblTitleDest.Location = New System.Drawing.Point(16, 93)
        Me.lblTitleDest.Name = "lblTitleDest"
        Me.lblTitleDest.Size = New System.Drawing.Size(94, 13)
        Me.lblTitleDest.TabIndex = 148
        Me.lblTitleDest.Text = "荷降先"
        Me.lblTitleDest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDest.TextValue = "荷降先"
        Me.lblTitleDest.WidthDef = 94
        '
        'lblTitleOrig
        '
        Me.lblTitleOrig.AutoSizeDef = False
        Me.lblTitleOrig.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrig.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrig.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrig.EnableStatus = False
        Me.lblTitleOrig.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrig.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrig.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrig.HeightDef = 13
        Me.lblTitleOrig.Location = New System.Drawing.Point(9, 37)
        Me.lblTitleOrig.Name = "lblTitleOrig"
        Me.lblTitleOrig.Size = New System.Drawing.Size(101, 13)
        Me.lblTitleOrig.TabIndex = 147
        Me.lblTitleOrig.Text = "積込先"
        Me.lblTitleOrig.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrig.TextValue = "積込先"
        Me.lblTitleOrig.WidthDef = 101
        '
        'lblAreaNm
        '
        Me.lblAreaNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAreaNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAreaNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAreaNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAreaNm.CountWrappedLine = False
        Me.lblAreaNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblAreaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAreaNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAreaNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAreaNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAreaNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblAreaNm.HeightDef = 18
        Me.lblAreaNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAreaNm.HissuLabelVisible = False
        Me.lblAreaNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblAreaNm.IsByteCheck = 0
        Me.lblAreaNm.IsCalendarCheck = False
        Me.lblAreaNm.IsDakutenCheck = False
        Me.lblAreaNm.IsEisuCheck = False
        Me.lblAreaNm.IsForbiddenWordsCheck = False
        Me.lblAreaNm.IsFullByteCheck = 0
        Me.lblAreaNm.IsHankakuCheck = False
        Me.lblAreaNm.IsHissuCheck = False
        Me.lblAreaNm.IsKanaCheck = False
        Me.lblAreaNm.IsMiddleSpace = False
        Me.lblAreaNm.IsNumericCheck = False
        Me.lblAreaNm.IsSujiCheck = False
        Me.lblAreaNm.IsZenkakuCheck = False
        Me.lblAreaNm.ItemName = ""
        Me.lblAreaNm.LineSpace = 0
        Me.lblAreaNm.Location = New System.Drawing.Point(1047, 110)
        Me.lblAreaNm.MaxLength = 0
        Me.lblAreaNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblAreaNm.MaxLineCount = 0
        Me.lblAreaNm.Multiline = False
        Me.lblAreaNm.Name = "lblAreaNm"
        Me.lblAreaNm.ReadOnly = True
        Me.lblAreaNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblAreaNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblAreaNm.Size = New System.Drawing.Size(187, 18)
        Me.lblAreaNm.TabIndex = 146
        Me.lblAreaNm.TabStop = False
        Me.lblAreaNm.TabStopSetting = False
        Me.lblAreaNm.TextValue = ""
        Me.lblAreaNm.UseSystemPasswordChar = False
        Me.lblAreaNm.WidthDef = 187
        Me.lblAreaNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtAreaCd
        '
        Me.txtAreaCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAreaCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAreaCd.CountWrappedLine = False
        Me.txtAreaCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAreaCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAreaCd.HeightDef = 18
        Me.txtAreaCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAreaCd.HissuLabelVisible = False
        Me.txtAreaCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtAreaCd.IsByteCheck = 7
        Me.txtAreaCd.IsCalendarCheck = False
        Me.txtAreaCd.IsDakutenCheck = False
        Me.txtAreaCd.IsEisuCheck = False
        Me.txtAreaCd.IsForbiddenWordsCheck = False
        Me.txtAreaCd.IsFullByteCheck = 0
        Me.txtAreaCd.IsHankakuCheck = False
        Me.txtAreaCd.IsHissuCheck = False
        Me.txtAreaCd.IsKanaCheck = False
        Me.txtAreaCd.IsMiddleSpace = False
        Me.txtAreaCd.IsNumericCheck = False
        Me.txtAreaCd.IsSujiCheck = False
        Me.txtAreaCd.IsZenkakuCheck = False
        Me.txtAreaCd.ItemName = ""
        Me.txtAreaCd.LineSpace = 0
        Me.txtAreaCd.Location = New System.Drawing.Point(979, 110)
        Me.txtAreaCd.MaxLength = 7
        Me.txtAreaCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAreaCd.MaxLineCount = 0
        Me.txtAreaCd.Multiline = False
        Me.txtAreaCd.Name = "txtAreaCd"
        Me.txtAreaCd.ReadOnly = False
        Me.txtAreaCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAreaCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAreaCd.Size = New System.Drawing.Size(84, 18)
        Me.txtAreaCd.TabIndex = 145
        Me.txtAreaCd.TabStopSetting = True
        Me.txtAreaCd.TextValue = ""
        Me.txtAreaCd.UseSystemPasswordChar = False
        Me.txtAreaCd.WidthDef = 84
        Me.txtAreaCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleArea
        '
        Me.lblTitleArea.AutoSizeDef = False
        Me.lblTitleArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArea.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArea.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleArea.EnableStatus = False
        Me.lblTitleArea.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArea.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArea.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArea.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArea.HeightDef = 13
        Me.lblTitleArea.Location = New System.Drawing.Point(870, 113)
        Me.lblTitleArea.Name = "lblTitleArea"
        Me.lblTitleArea.Size = New System.Drawing.Size(109, 13)
        Me.lblTitleArea.TabIndex = 144
        Me.lblTitleArea.Text = "配送区域"
        Me.lblTitleArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleArea.TextValue = "配送区域"
        Me.lblTitleArea.WidthDef = 109
        '
        'lblDestAdd2
        '
        Me.lblDestAdd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestAdd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDestAdd2.CountWrappedLine = False
        Me.lblDestAdd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDestAdd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestAdd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestAdd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestAdd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestAdd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDestAdd2.HeightDef = 18
        Me.lblDestAdd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd2.HissuLabelVisible = False
        Me.lblDestAdd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestAdd2.IsByteCheck = 0
        Me.lblDestAdd2.IsCalendarCheck = False
        Me.lblDestAdd2.IsDakutenCheck = False
        Me.lblDestAdd2.IsEisuCheck = False
        Me.lblDestAdd2.IsForbiddenWordsCheck = False
        Me.lblDestAdd2.IsFullByteCheck = 0
        Me.lblDestAdd2.IsHankakuCheck = False
        Me.lblDestAdd2.IsHissuCheck = False
        Me.lblDestAdd2.IsKanaCheck = False
        Me.lblDestAdd2.IsMiddleSpace = False
        Me.lblDestAdd2.IsNumericCheck = False
        Me.lblDestAdd2.IsSujiCheck = False
        Me.lblDestAdd2.IsZenkakuCheck = False
        Me.lblDestAdd2.ItemName = ""
        Me.lblDestAdd2.LineSpace = 0
        Me.lblDestAdd2.Location = New System.Drawing.Point(225, 131)
        Me.lblDestAdd2.MaxLength = 0
        Me.lblDestAdd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestAdd2.MaxLineCount = 0
        Me.lblDestAdd2.Multiline = False
        Me.lblDestAdd2.Name = "lblDestAdd2"
        Me.lblDestAdd2.ReadOnly = True
        Me.lblDestAdd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestAdd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestAdd2.Size = New System.Drawing.Size(348, 18)
        Me.lblDestAdd2.TabIndex = 143
        Me.lblDestAdd2.TabStop = False
        Me.lblDestAdd2.TabStopSetting = False
        Me.lblDestAdd2.TextValue = ""
        Me.lblDestAdd2.UseSystemPasswordChar = False
        Me.lblDestAdd2.WidthDef = 348
        Me.lblDestAdd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblDestAdd1
        '
        Me.lblDestAdd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestAdd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDestAdd1.CountWrappedLine = False
        Me.lblDestAdd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDestAdd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestAdd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestAdd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestAdd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestAdd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDestAdd1.HeightDef = 18
        Me.lblDestAdd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestAdd1.HissuLabelVisible = False
        Me.lblDestAdd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestAdd1.IsByteCheck = 0
        Me.lblDestAdd1.IsCalendarCheck = False
        Me.lblDestAdd1.IsDakutenCheck = False
        Me.lblDestAdd1.IsEisuCheck = False
        Me.lblDestAdd1.IsForbiddenWordsCheck = False
        Me.lblDestAdd1.IsFullByteCheck = 0
        Me.lblDestAdd1.IsHankakuCheck = False
        Me.lblDestAdd1.IsHissuCheck = False
        Me.lblDestAdd1.IsKanaCheck = False
        Me.lblDestAdd1.IsMiddleSpace = False
        Me.lblDestAdd1.IsNumericCheck = False
        Me.lblDestAdd1.IsSujiCheck = False
        Me.lblDestAdd1.IsZenkakuCheck = False
        Me.lblDestAdd1.ItemName = ""
        Me.lblDestAdd1.LineSpace = 0
        Me.lblDestAdd1.Location = New System.Drawing.Point(225, 110)
        Me.lblDestAdd1.MaxLength = 0
        Me.lblDestAdd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestAdd1.MaxLineCount = 0
        Me.lblDestAdd1.Multiline = False
        Me.lblDestAdd1.Name = "lblDestAdd1"
        Me.lblDestAdd1.ReadOnly = True
        Me.lblDestAdd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestAdd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestAdd1.Size = New System.Drawing.Size(348, 18)
        Me.lblDestAdd1.TabIndex = 142
        Me.lblDestAdd1.TabStop = False
        Me.lblDestAdd1.TabStopSetting = False
        Me.lblDestAdd1.TextValue = ""
        Me.lblDestAdd1.UseSystemPasswordChar = False
        Me.lblDestAdd1.WidthDef = 348
        Me.lblDestAdd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblZipNo
        '
        Me.lblZipNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZipNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZipNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZipNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblZipNo.CountWrappedLine = False
        Me.lblZipNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblZipNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZipNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZipNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZipNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblZipNo.HeightDef = 18
        Me.lblZipNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZipNo.HissuLabelVisible = False
        Me.lblZipNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblZipNo.IsByteCheck = 0
        Me.lblZipNo.IsCalendarCheck = False
        Me.lblZipNo.IsDakutenCheck = False
        Me.lblZipNo.IsEisuCheck = False
        Me.lblZipNo.IsForbiddenWordsCheck = False
        Me.lblZipNo.IsFullByteCheck = 0
        Me.lblZipNo.IsHankakuCheck = False
        Me.lblZipNo.IsHissuCheck = False
        Me.lblZipNo.IsKanaCheck = False
        Me.lblZipNo.IsMiddleSpace = False
        Me.lblZipNo.IsNumericCheck = False
        Me.lblZipNo.IsSujiCheck = False
        Me.lblZipNo.IsZenkakuCheck = False
        Me.lblZipNo.ItemName = ""
        Me.lblZipNo.LineSpace = 0
        Me.lblZipNo.Location = New System.Drawing.Point(110, 110)
        Me.lblZipNo.MaxLength = 0
        Me.lblZipNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblZipNo.MaxLineCount = 0
        Me.lblZipNo.Multiline = False
        Me.lblZipNo.Name = "lblZipNo"
        Me.lblZipNo.ReadOnly = True
        Me.lblZipNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblZipNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblZipNo.Size = New System.Drawing.Size(118, 18)
        Me.lblZipNo.TabIndex = 141
        Me.lblZipNo.TabStop = False
        Me.lblZipNo.TabStopSetting = False
        Me.lblZipNo.TextValue = ""
        Me.lblZipNo.UseSystemPasswordChar = False
        Me.lblZipNo.WidthDef = 118
        Me.lblZipNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleAdd
        '
        Me.lblTitleAdd.AutoSizeDef = False
        Me.lblTitleAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleAdd.EnableStatus = False
        Me.lblTitleAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleAdd.HeightDef = 13
        Me.lblTitleAdd.Location = New System.Drawing.Point(12, 114)
        Me.lblTitleAdd.Name = "lblTitleAdd"
        Me.lblTitleAdd.Size = New System.Drawing.Size(98, 13)
        Me.lblTitleAdd.TabIndex = 140
        Me.lblTitleAdd.Text = "住所"
        Me.lblTitleAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleAdd.TextValue = "住所"
        Me.lblTitleAdd.WidthDef = 98
        '
        'lblDestJisCd
        '
        Me.lblDestJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestJisCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDestJisCd.CountWrappedLine = False
        Me.lblDestJisCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDestJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestJisCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDestJisCd.HeightDef = 18
        Me.lblDestJisCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestJisCd.HissuLabelVisible = False
        Me.lblDestJisCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestJisCd.IsByteCheck = 0
        Me.lblDestJisCd.IsCalendarCheck = False
        Me.lblDestJisCd.IsDakutenCheck = False
        Me.lblDestJisCd.IsEisuCheck = False
        Me.lblDestJisCd.IsForbiddenWordsCheck = False
        Me.lblDestJisCd.IsFullByteCheck = 0
        Me.lblDestJisCd.IsHankakuCheck = False
        Me.lblDestJisCd.IsHissuCheck = False
        Me.lblDestJisCd.IsKanaCheck = False
        Me.lblDestJisCd.IsMiddleSpace = False
        Me.lblDestJisCd.IsNumericCheck = False
        Me.lblDestJisCd.IsSujiCheck = False
        Me.lblDestJisCd.IsZenkakuCheck = False
        Me.lblDestJisCd.ItemName = ""
        Me.lblDestJisCd.LineSpace = 0
        Me.lblDestJisCd.Location = New System.Drawing.Point(979, 89)
        Me.lblDestJisCd.MaxLength = 0
        Me.lblDestJisCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestJisCd.MaxLineCount = 0
        Me.lblDestJisCd.Multiline = False
        Me.lblDestJisCd.Name = "lblDestJisCd"
        Me.lblDestJisCd.ReadOnly = True
        Me.lblDestJisCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestJisCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestJisCd.Size = New System.Drawing.Size(84, 18)
        Me.lblDestJisCd.TabIndex = 139
        Me.lblDestJisCd.TabStop = False
        Me.lblDestJisCd.TabStopSetting = False
        Me.lblDestJisCd.TextValue = "XXXXXXX"
        Me.lblDestJisCd.UseSystemPasswordChar = False
        Me.lblDestJisCd.WidthDef = 84
        Me.lblDestJisCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDestJisCd
        '
        Me.lblTitleDestJisCd.AutoSizeDef = False
        Me.lblTitleDestJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDestJisCd.EnableStatus = False
        Me.lblTitleDestJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestJisCd.HeightDef = 13
        Me.lblTitleDestJisCd.Location = New System.Drawing.Point(867, 92)
        Me.lblTitleDestJisCd.Name = "lblTitleDestJisCd"
        Me.lblTitleDestJisCd.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleDestJisCd.TabIndex = 138
        Me.lblTitleDestJisCd.Text = "荷降先JISコード"
        Me.lblTitleDestJisCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDestJisCd.TextValue = "荷降先JISコード"
        Me.lblTitleDestJisCd.WidthDef = 112
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
        Me.lblDestNm.HissuLabelVisible = False
        Me.lblDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestNm.IsByteCheck = 0
        Me.lblDestNm.IsCalendarCheck = False
        Me.lblDestNm.IsDakutenCheck = False
        Me.lblDestNm.IsEisuCheck = False
        Me.lblDestNm.IsForbiddenWordsCheck = False
        Me.lblDestNm.IsFullByteCheck = 0
        Me.lblDestNm.IsHankakuCheck = False
        Me.lblDestNm.IsHissuCheck = False
        Me.lblDestNm.IsKanaCheck = False
        Me.lblDestNm.IsMiddleSpace = False
        Me.lblDestNm.IsNumericCheck = False
        Me.lblDestNm.IsSujiCheck = False
        Me.lblDestNm.IsZenkakuCheck = False
        Me.lblDestNm.ItemName = ""
        Me.lblDestNm.LineSpace = 0
        Me.lblDestNm.Location = New System.Drawing.Point(225, 89)
        Me.lblDestNm.MaxLength = 0
        Me.lblDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestNm.MaxLineCount = 0
        Me.lblDestNm.Multiline = False
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.ReadOnly = True
        Me.lblDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestNm.Size = New System.Drawing.Size(545, 18)
        Me.lblDestNm.TabIndex = 137
        Me.lblDestNm.TabStop = False
        Me.lblDestNm.TabStopSetting = False
        Me.lblDestNm.TextValue = ""
        Me.lblDestNm.UseSystemPasswordChar = False
        Me.lblDestNm.WidthDef = 545
        Me.lblDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestAdd3
        '
        Me.txtDestAdd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestAdd3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestAdd3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestAdd3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestAdd3.CountWrappedLine = False
        Me.txtDestAdd3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestAdd3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdd3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdd3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdd3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdd3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestAdd3.HeightDef = 18
        Me.txtDestAdd3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdd3.HissuLabelVisible = False
        Me.txtDestAdd3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestAdd3.IsByteCheck = 80
        Me.txtDestAdd3.IsCalendarCheck = False
        Me.txtDestAdd3.IsDakutenCheck = False
        Me.txtDestAdd3.IsEisuCheck = False
        Me.txtDestAdd3.IsForbiddenWordsCheck = False
        Me.txtDestAdd3.IsFullByteCheck = 0
        Me.txtDestAdd3.IsHankakuCheck = False
        Me.txtDestAdd3.IsHissuCheck = False
        Me.txtDestAdd3.IsKanaCheck = False
        Me.txtDestAdd3.IsMiddleSpace = False
        Me.txtDestAdd3.IsNumericCheck = False
        Me.txtDestAdd3.IsSujiCheck = False
        Me.txtDestAdd3.IsZenkakuCheck = False
        Me.txtDestAdd3.ItemName = ""
        Me.txtDestAdd3.LineSpace = 0
        Me.txtDestAdd3.Location = New System.Drawing.Point(225, 152)
        Me.txtDestAdd3.MaxLength = 80
        Me.txtDestAdd3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestAdd3.MaxLineCount = 0
        Me.txtDestAdd3.Multiline = False
        Me.txtDestAdd3.Name = "txtDestAdd3"
        Me.txtDestAdd3.ReadOnly = False
        Me.txtDestAdd3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestAdd3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestAdd3.Size = New System.Drawing.Size(348, 18)
        Me.txtDestAdd3.TabIndex = 136
        Me.txtDestAdd3.TabStopSetting = True
        Me.txtDestAdd3.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtDestAdd3.UseSystemPasswordChar = False
        Me.txtDestAdd3.WidthDef = 348
        Me.txtDestAdd3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtDestCd.HissuLabelVisible = False
        Me.txtDestCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtDestCd.IsByteCheck = 15
        Me.txtDestCd.IsCalendarCheck = False
        Me.txtDestCd.IsDakutenCheck = False
        Me.txtDestCd.IsEisuCheck = False
        Me.txtDestCd.IsForbiddenWordsCheck = False
        Me.txtDestCd.IsFullByteCheck = 0
        Me.txtDestCd.IsHankakuCheck = False
        Me.txtDestCd.IsHissuCheck = False
        Me.txtDestCd.IsKanaCheck = False
        Me.txtDestCd.IsMiddleSpace = False
        Me.txtDestCd.IsNumericCheck = False
        Me.txtDestCd.IsSujiCheck = False
        Me.txtDestCd.IsZenkakuCheck = False
        Me.txtDestCd.ItemName = ""
        Me.txtDestCd.LineSpace = 0
        Me.txtDestCd.Location = New System.Drawing.Point(110, 89)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(131, 18)
        Me.txtDestCd.TabIndex = 136
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 131
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleJiDestTime
        '
        Me.lblTitleJiDestTime.AutoSizeDef = False
        Me.lblTitleJiDestTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJiDestTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJiDestTime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJiDestTime.EnableStatus = False
        Me.lblTitleJiDestTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJiDestTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJiDestTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJiDestTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJiDestTime.HeightDef = 13
        Me.lblTitleJiDestTime.Location = New System.Drawing.Point(567, 71)
        Me.lblTitleJiDestTime.Name = "lblTitleJiDestTime"
        Me.lblTitleJiDestTime.Size = New System.Drawing.Size(83, 13)
        Me.lblTitleJiDestTime.TabIndex = 133
        Me.lblTitleJiDestTime.Text = "実荷降時刻"
        Me.lblTitleJiDestTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJiDestTime.TextValue = "実荷降時刻"
        Me.lblTitleJiDestTime.WidthDef = 83
        '
        'lblTitleDestTime
        '
        Me.lblTitleDestTime.AutoSizeDef = False
        Me.lblTitleDestTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestTime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDestTime.EnableStatus = False
        Me.lblTitleDestTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestTime.HeightDef = 13
        Me.lblTitleDestTime.Location = New System.Drawing.Point(221, 71)
        Me.lblTitleDestTime.Name = "lblTitleDestTime"
        Me.lblTitleDestTime.Size = New System.Drawing.Size(109, 13)
        Me.lblTitleDestTime.TabIndex = 131
        Me.lblTitleDestTime.Text = "荷降予定時刻"
        Me.lblTitleDestTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDestTime.TextValue = "荷降予定時刻"
        Me.lblTitleDestTime.WidthDef = 109
        '
        'imdDestDate
        '
        Me.imdDestDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDestDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDestDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdDestDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdDestDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdDestDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDestDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDestDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDestDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDestDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdDestDate.HeightDef = 18
        Me.imdDestDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdDestDate.HissuLabelVisible = False
        Me.imdDestDate.Holiday = False
        Me.imdDestDate.IsAfterDateCheck = False
        Me.imdDestDate.IsBeforeDateCheck = False
        Me.imdDestDate.IsHissuCheck = False
        Me.imdDestDate.IsMinDateCheck = "1900/01/01"
        Me.imdDestDate.ItemName = ""
        Me.imdDestDate.Location = New System.Drawing.Point(110, 68)
        Me.imdDestDate.Name = "imdDestDate"
        Me.imdDestDate.Number = CType(10101000000, Long)
        Me.imdDestDate.ReadOnly = False
        Me.imdDestDate.Size = New System.Drawing.Size(118, 18)
        Me.imdDestDate.TabIndex = 130
        Me.imdDestDate.TabStopSetting = True
        Me.imdDestDate.TextValue = ""
        Me.imdDestDate.Value = New Date(CType(0, Long))
        Me.imdDestDate.WidthDef = 118
        '
        'lblTitleDestDate
        '
        Me.lblTitleDestDate.AutoSizeDef = False
        Me.lblTitleDestDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDestDate.EnableStatus = False
        Me.lblTitleDestDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestDate.HeightDef = 13
        Me.lblTitleDestDate.Location = New System.Drawing.Point(6, 72)
        Me.lblTitleDestDate.Name = "lblTitleDestDate"
        Me.lblTitleDestDate.Size = New System.Drawing.Size(104, 13)
        Me.lblTitleDestDate.TabIndex = 129
        Me.lblTitleDestDate.Text = "荷降予定日"
        Me.lblTitleDestDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDestDate.TextValue = "荷降予定日"
        Me.lblTitleDestDate.WidthDef = 104
        '
        'lblOrigJisCd
        '
        Me.lblOrigJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrigJisCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrigJisCd.CountWrappedLine = False
        Me.lblOrigJisCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblOrigJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigJisCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblOrigJisCd.HeightDef = 18
        Me.lblOrigJisCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisCd.HissuLabelVisible = False
        Me.lblOrigJisCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblOrigJisCd.IsByteCheck = 0
        Me.lblOrigJisCd.IsCalendarCheck = False
        Me.lblOrigJisCd.IsDakutenCheck = False
        Me.lblOrigJisCd.IsEisuCheck = False
        Me.lblOrigJisCd.IsForbiddenWordsCheck = False
        Me.lblOrigJisCd.IsFullByteCheck = 0
        Me.lblOrigJisCd.IsHankakuCheck = False
        Me.lblOrigJisCd.IsHissuCheck = False
        Me.lblOrigJisCd.IsKanaCheck = False
        Me.lblOrigJisCd.IsMiddleSpace = False
        Me.lblOrigJisCd.IsNumericCheck = False
        Me.lblOrigJisCd.IsSujiCheck = False
        Me.lblOrigJisCd.IsZenkakuCheck = False
        Me.lblOrigJisCd.ItemName = ""
        Me.lblOrigJisCd.LineSpace = 0
        Me.lblOrigJisCd.Location = New System.Drawing.Point(979, 34)
        Me.lblOrigJisCd.MaxLength = 0
        Me.lblOrigJisCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblOrigJisCd.MaxLineCount = 0
        Me.lblOrigJisCd.Multiline = False
        Me.lblOrigJisCd.Name = "lblOrigJisCd"
        Me.lblOrigJisCd.ReadOnly = True
        Me.lblOrigJisCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblOrigJisCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblOrigJisCd.Size = New System.Drawing.Size(84, 18)
        Me.lblOrigJisCd.TabIndex = 128
        Me.lblOrigJisCd.TabStop = False
        Me.lblOrigJisCd.TabStopSetting = False
        Me.lblOrigJisCd.TextValue = "XXXXXXX"
        Me.lblOrigJisCd.UseSystemPasswordChar = False
        Me.lblOrigJisCd.WidthDef = 84
        Me.lblOrigJisCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrigJisCd
        '
        Me.lblTitleOrigJisCd.AutoSizeDef = False
        Me.lblTitleOrigJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrigJisCd.EnableStatus = False
        Me.lblTitleOrigJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigJisCd.HeightDef = 13
        Me.lblTitleOrigJisCd.Location = New System.Drawing.Point(867, 37)
        Me.lblTitleOrigJisCd.Name = "lblTitleOrigJisCd"
        Me.lblTitleOrigJisCd.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleOrigJisCd.TabIndex = 127
        Me.lblTitleOrigJisCd.Text = "積込先JISコード"
        Me.lblTitleOrigJisCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrigJisCd.TextValue = "積込先JISコード"
        Me.lblTitleOrigJisCd.WidthDef = 112
        '
        'lblOrigNm
        '
        Me.lblOrigNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrigNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrigNm.CountWrappedLine = False
        Me.lblOrigNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblOrigNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblOrigNm.HeightDef = 18
        Me.lblOrigNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigNm.HissuLabelVisible = False
        Me.lblOrigNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblOrigNm.IsByteCheck = 0
        Me.lblOrigNm.IsCalendarCheck = False
        Me.lblOrigNm.IsDakutenCheck = False
        Me.lblOrigNm.IsEisuCheck = False
        Me.lblOrigNm.IsForbiddenWordsCheck = False
        Me.lblOrigNm.IsFullByteCheck = 0
        Me.lblOrigNm.IsHankakuCheck = False
        Me.lblOrigNm.IsHissuCheck = False
        Me.lblOrigNm.IsKanaCheck = False
        Me.lblOrigNm.IsMiddleSpace = False
        Me.lblOrigNm.IsNumericCheck = False
        Me.lblOrigNm.IsSujiCheck = False
        Me.lblOrigNm.IsZenkakuCheck = False
        Me.lblOrigNm.ItemName = ""
        Me.lblOrigNm.LineSpace = 0
        Me.lblOrigNm.Location = New System.Drawing.Point(225, 34)
        Me.lblOrigNm.MaxLength = 0
        Me.lblOrigNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblOrigNm.MaxLineCount = 0
        Me.lblOrigNm.Multiline = False
        Me.lblOrigNm.Name = "lblOrigNm"
        Me.lblOrigNm.ReadOnly = True
        Me.lblOrigNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblOrigNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblOrigNm.Size = New System.Drawing.Size(545, 18)
        Me.lblOrigNm.TabIndex = 126
        Me.lblOrigNm.TabStop = False
        Me.lblOrigNm.TabStopSetting = False
        Me.lblOrigNm.TextValue = ""
        Me.lblOrigNm.UseSystemPasswordChar = False
        Me.lblOrigNm.WidthDef = 545
        Me.lblOrigNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtOrigCd
        '
        Me.txtOrigCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrigCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrigCd.CountWrappedLine = False
        Me.txtOrigCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrigCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrigCd.HeightDef = 18
        Me.txtOrigCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrigCd.HissuLabelVisible = False
        Me.txtOrigCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtOrigCd.IsByteCheck = 15
        Me.txtOrigCd.IsCalendarCheck = False
        Me.txtOrigCd.IsDakutenCheck = False
        Me.txtOrigCd.IsEisuCheck = False
        Me.txtOrigCd.IsForbiddenWordsCheck = False
        Me.txtOrigCd.IsFullByteCheck = 0
        Me.txtOrigCd.IsHankakuCheck = False
        Me.txtOrigCd.IsHissuCheck = False
        Me.txtOrigCd.IsKanaCheck = False
        Me.txtOrigCd.IsMiddleSpace = False
        Me.txtOrigCd.IsNumericCheck = False
        Me.txtOrigCd.IsSujiCheck = False
        Me.txtOrigCd.IsZenkakuCheck = False
        Me.txtOrigCd.ItemName = ""
        Me.txtOrigCd.LineSpace = 0
        Me.txtOrigCd.Location = New System.Drawing.Point(110, 34)
        Me.txtOrigCd.MaxLength = 15
        Me.txtOrigCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrigCd.MaxLineCount = 0
        Me.txtOrigCd.Multiline = False
        Me.txtOrigCd.Name = "txtOrigCd"
        Me.txtOrigCd.ReadOnly = False
        Me.txtOrigCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrigCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrigCd.Size = New System.Drawing.Size(131, 18)
        Me.txtOrigCd.TabIndex = 125
        Me.txtOrigCd.TabStopSetting = True
        Me.txtOrigCd.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtOrigCd.UseSystemPasswordChar = False
        Me.txtOrigCd.WidthDef = 131
        Me.txtOrigCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrigTime
        '
        Me.lblTitleOrigTime.AutoSizeDef = False
        Me.lblTitleOrigTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigTime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrigTime.EnableStatus = False
        Me.lblTitleOrigTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigTime.HeightDef = 13
        Me.lblTitleOrigTime.Location = New System.Drawing.Point(218, 16)
        Me.lblTitleOrigTime.Name = "lblTitleOrigTime"
        Me.lblTitleOrigTime.Size = New System.Drawing.Size(112, 13)
        Me.lblTitleOrigTime.TabIndex = 92
        Me.lblTitleOrigTime.Text = "積込予定時刻"
        Me.lblTitleOrigTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrigTime.TextValue = "積込予定時刻"
        Me.lblTitleOrigTime.WidthDef = 112
        '
        'imdOrigDate
        '
        Me.imdOrigDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOrigDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOrigDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdOrigDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdOrigDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdOrigDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOrigDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOrigDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOrigDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOrigDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdOrigDate.HeightDef = 18
        Me.imdOrigDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdOrigDate.HissuLabelVisible = False
        Me.imdOrigDate.Holiday = False
        Me.imdOrigDate.IsAfterDateCheck = False
        Me.imdOrigDate.IsBeforeDateCheck = False
        Me.imdOrigDate.IsHissuCheck = False
        Me.imdOrigDate.IsMinDateCheck = "1900/01/01"
        Me.imdOrigDate.ItemName = ""
        Me.imdOrigDate.Location = New System.Drawing.Point(110, 13)
        Me.imdOrigDate.Name = "imdOrigDate"
        Me.imdOrigDate.Number = CType(10101000000, Long)
        Me.imdOrigDate.ReadOnly = False
        Me.imdOrigDate.Size = New System.Drawing.Size(118, 18)
        Me.imdOrigDate.TabIndex = 91
        Me.imdOrigDate.TabStopSetting = True
        Me.imdOrigDate.TextValue = ""
        Me.imdOrigDate.Value = New Date(CType(0, Long))
        Me.imdOrigDate.WidthDef = 118
        '
        'lblTitleOrigDate
        '
        Me.lblTitleOrigDate.AutoSizeDef = False
        Me.lblTitleOrigDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrigDate.EnableStatus = False
        Me.lblTitleOrigDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigDate.HeightDef = 13
        Me.lblTitleOrigDate.Location = New System.Drawing.Point(6, 16)
        Me.lblTitleOrigDate.Name = "lblTitleOrigDate"
        Me.lblTitleOrigDate.Size = New System.Drawing.Size(104, 13)
        Me.lblTitleOrigDate.TabIndex = 90
        Me.lblTitleOrigDate.Text = "積込予定日"
        Me.lblTitleOrigDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrigDate.TextValue = "積込予定日"
        Me.lblTitleOrigDate.WidthDef = 104
        '
        'txtDestTime
        '
        Me.txtDestTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestTime.CountWrappedLine = False
        Me.txtDestTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestTime.HeightDef = 18
        Me.txtDestTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTime.HissuLabelVisible = False
        Me.txtDestTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtDestTime.IsByteCheck = 10
        Me.txtDestTime.IsCalendarCheck = False
        Me.txtDestTime.IsDakutenCheck = False
        Me.txtDestTime.IsEisuCheck = False
        Me.txtDestTime.IsForbiddenWordsCheck = False
        Me.txtDestTime.IsFullByteCheck = 0
        Me.txtDestTime.IsHankakuCheck = False
        Me.txtDestTime.IsHissuCheck = False
        Me.txtDestTime.IsKanaCheck = False
        Me.txtDestTime.IsMiddleSpace = False
        Me.txtDestTime.IsNumericCheck = False
        Me.txtDestTime.IsSujiCheck = False
        Me.txtDestTime.IsZenkakuCheck = False
        Me.txtDestTime.ItemName = ""
        Me.txtDestTime.LineSpace = 0
        Me.txtDestTime.Location = New System.Drawing.Point(330, 68)
        Me.txtDestTime.MaxLength = 10
        Me.txtDestTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestTime.MaxLineCount = 0
        Me.txtDestTime.Multiline = False
        Me.txtDestTime.Name = "txtDestTime"
        Me.txtDestTime.ReadOnly = False
        Me.txtDestTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestTime.Size = New System.Drawing.Size(161, 18)
        Me.txtDestTime.TabIndex = 149
        Me.txtDestTime.TabStopSetting = True
        Me.txtDestTime.TextValue = ""
        Me.txtDestTime.UseSystemPasswordChar = False
        Me.txtDestTime.WidthDef = 161
        Me.txtDestTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtOrigTime
        '
        Me.txtOrigTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrigTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrigTime.CountWrappedLine = False
        Me.txtOrigTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrigTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrigTime.HeightDef = 18
        Me.txtOrigTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrigTime.HissuLabelVisible = False
        Me.txtOrigTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtOrigTime.IsByteCheck = 6
        Me.txtOrigTime.IsCalendarCheck = False
        Me.txtOrigTime.IsDakutenCheck = False
        Me.txtOrigTime.IsEisuCheck = False
        Me.txtOrigTime.IsForbiddenWordsCheck = False
        Me.txtOrigTime.IsFullByteCheck = 0
        Me.txtOrigTime.IsHankakuCheck = False
        Me.txtOrigTime.IsHissuCheck = False
        Me.txtOrigTime.IsKanaCheck = False
        Me.txtOrigTime.IsMiddleSpace = False
        Me.txtOrigTime.IsNumericCheck = False
        Me.txtOrigTime.IsSujiCheck = False
        Me.txtOrigTime.IsZenkakuCheck = False
        Me.txtOrigTime.ItemName = ""
        Me.txtOrigTime.LineSpace = 0
        Me.txtOrigTime.Location = New System.Drawing.Point(330, 13)
        Me.txtOrigTime.MaxLength = 6
        Me.txtOrigTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrigTime.MaxLineCount = 0
        Me.txtOrigTime.Multiline = False
        Me.txtOrigTime.Name = "txtOrigTime"
        Me.txtOrigTime.ReadOnly = False
        Me.txtOrigTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrigTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrigTime.Size = New System.Drawing.Size(73, 18)
        Me.txtOrigTime.TabIndex = 150
        Me.txtOrigTime.TabStopSetting = True
        Me.txtOrigTime.TextValue = ""
        Me.txtOrigTime.UseSystemPasswordChar = False
        Me.txtOrigTime.WidthDef = 73
        Me.txtOrigTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtJiDestTime
        '
        Me.txtJiDestTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJiDestTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJiDestTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJiDestTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtJiDestTime.CountWrappedLine = False
        Me.txtJiDestTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtJiDestTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJiDestTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJiDestTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJiDestTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJiDestTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtJiDestTime.HeightDef = 18
        Me.txtJiDestTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtJiDestTime.HissuLabelVisible = False
        Me.txtJiDestTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtJiDestTime.IsByteCheck = 6
        Me.txtJiDestTime.IsCalendarCheck = False
        Me.txtJiDestTime.IsDakutenCheck = False
        Me.txtJiDestTime.IsEisuCheck = False
        Me.txtJiDestTime.IsForbiddenWordsCheck = False
        Me.txtJiDestTime.IsFullByteCheck = 0
        Me.txtJiDestTime.IsHankakuCheck = False
        Me.txtJiDestTime.IsHissuCheck = False
        Me.txtJiDestTime.IsKanaCheck = False
        Me.txtJiDestTime.IsMiddleSpace = False
        Me.txtJiDestTime.IsNumericCheck = False
        Me.txtJiDestTime.IsSujiCheck = False
        Me.txtJiDestTime.IsZenkakuCheck = False
        Me.txtJiDestTime.ItemName = ""
        Me.txtJiDestTime.LineSpace = 0
        Me.txtJiDestTime.Location = New System.Drawing.Point(652, 68)
        Me.txtJiDestTime.MaxLength = 6
        Me.txtJiDestTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtJiDestTime.MaxLineCount = 0
        Me.txtJiDestTime.Multiline = False
        Me.txtJiDestTime.Name = "txtJiDestTime"
        Me.txtJiDestTime.ReadOnly = False
        Me.txtJiDestTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtJiDestTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtJiDestTime.Size = New System.Drawing.Size(73, 18)
        Me.txtJiDestTime.TabIndex = 151
        Me.txtJiDestTime.TabStopSetting = True
        Me.txtJiDestTime.TextValue = ""
        Me.txtJiDestTime.UseSystemPasswordChar = False
        Me.txtJiDestTime.WidthDef = 73
        Me.txtJiDestTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtOrdNo
        '
        Me.txtOrdNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrdNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrdNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrdNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrdNo.CountWrappedLine = False
        Me.txtOrdNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrdNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrdNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrdNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrdNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrdNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrdNo.HeightDef = 18
        Me.txtOrdNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrdNo.HissuLabelVisible = False
        Me.txtOrdNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtOrdNo.IsByteCheck = 30
        Me.txtOrdNo.IsCalendarCheck = False
        Me.txtOrdNo.IsDakutenCheck = False
        Me.txtOrdNo.IsEisuCheck = False
        Me.txtOrdNo.IsForbiddenWordsCheck = False
        Me.txtOrdNo.IsFullByteCheck = 0
        Me.txtOrdNo.IsHankakuCheck = False
        Me.txtOrdNo.IsHissuCheck = False
        Me.txtOrdNo.IsKanaCheck = False
        Me.txtOrdNo.IsMiddleSpace = False
        Me.txtOrdNo.IsNumericCheck = False
        Me.txtOrdNo.IsSujiCheck = False
        Me.txtOrdNo.IsZenkakuCheck = False
        Me.txtOrdNo.ItemName = ""
        Me.txtOrdNo.LineSpace = 0
        Me.txtOrdNo.Location = New System.Drawing.Point(997, 103)
        Me.txtOrdNo.MaxLength = 30
        Me.txtOrdNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrdNo.MaxLineCount = 0
        Me.txtOrdNo.Multiline = False
        Me.txtOrdNo.Name = "txtOrdNo"
        Me.txtOrdNo.ReadOnly = False
        Me.txtOrdNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrdNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrdNo.Size = New System.Drawing.Size(252, 18)
        Me.txtOrdNo.TabIndex = 100
        Me.txtOrdNo.TabStopSetting = True
        Me.txtOrdNo.TextValue = ""
        Me.txtOrdNo.UseSystemPasswordChar = False
        Me.txtOrdNo.WidthDef = 252
        Me.txtOrdNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtBuyerOrdNo.Location = New System.Drawing.Point(997, 124)
        Me.txtBuyerOrdNo.MaxLength = 30
        Me.txtBuyerOrdNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBuyerOrdNo.MaxLineCount = 0
        Me.txtBuyerOrdNo.Multiline = False
        Me.txtBuyerOrdNo.Name = "txtBuyerOrdNo"
        Me.txtBuyerOrdNo.ReadOnly = False
        Me.txtBuyerOrdNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBuyerOrdNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBuyerOrdNo.Size = New System.Drawing.Size(252, 18)
        Me.txtBuyerOrdNo.TabIndex = 99
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
        Me.lblTitleBuyerOrdNo.Location = New System.Drawing.Point(934, 128)
        Me.lblTitleBuyerOrdNo.Name = "lblTitleBuyerOrdNo"
        Me.lblTitleBuyerOrdNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleBuyerOrdNo.TabIndex = 98
        Me.lblTitleBuyerOrdNo.Text = "注文番号"
        Me.lblTitleBuyerOrdNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBuyerOrdNo.TextValue = "注文番号"
        Me.lblTitleBuyerOrdNo.WidthDef = 63
        '
        'txtOkuriNo
        '
        Me.txtOkuriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOkuriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOkuriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOkuriNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOkuriNo.CountWrappedLine = False
        Me.txtOkuriNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOkuriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOkuriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOkuriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOkuriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOkuriNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOkuriNo.HeightDef = 18
        Me.txtOkuriNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOkuriNo.HissuLabelVisible = False
        Me.txtOkuriNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtOkuriNo.IsByteCheck = 20
        Me.txtOkuriNo.IsCalendarCheck = False
        Me.txtOkuriNo.IsDakutenCheck = False
        Me.txtOkuriNo.IsEisuCheck = False
        Me.txtOkuriNo.IsForbiddenWordsCheck = False
        Me.txtOkuriNo.IsFullByteCheck = 0
        Me.txtOkuriNo.IsHankakuCheck = False
        Me.txtOkuriNo.IsHissuCheck = False
        Me.txtOkuriNo.IsKanaCheck = False
        Me.txtOkuriNo.IsMiddleSpace = False
        Me.txtOkuriNo.IsNumericCheck = False
        Me.txtOkuriNo.IsSujiCheck = False
        Me.txtOkuriNo.IsZenkakuCheck = False
        Me.txtOkuriNo.ItemName = ""
        Me.txtOkuriNo.LineSpace = 0
        Me.txtOkuriNo.Location = New System.Drawing.Point(997, 82)
        Me.txtOkuriNo.MaxLength = 20
        Me.txtOkuriNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOkuriNo.MaxLineCount = 0
        Me.txtOkuriNo.Multiline = False
        Me.txtOkuriNo.Name = "txtOkuriNo"
        Me.txtOkuriNo.ReadOnly = False
        Me.txtOkuriNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOkuriNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOkuriNo.Size = New System.Drawing.Size(252, 18)
        Me.txtOkuriNo.TabIndex = 97
        Me.txtOkuriNo.TabStopSetting = True
        Me.txtOkuriNo.TextValue = ""
        Me.txtOkuriNo.UseSystemPasswordChar = False
        Me.txtOkuriNo.WidthDef = 252
        Me.txtOkuriNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitletxtOrdNo
        '
        Me.lblTitletxtOrdNo.AutoSizeDef = False
        Me.lblTitletxtOrdNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitletxtOrdNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitletxtOrdNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitletxtOrdNo.EnableStatus = False
        Me.lblTitletxtOrdNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitletxtOrdNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitletxtOrdNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitletxtOrdNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitletxtOrdNo.HeightDef = 13
        Me.lblTitletxtOrdNo.Location = New System.Drawing.Point(876, 106)
        Me.lblTitletxtOrdNo.Name = "lblTitletxtOrdNo"
        Me.lblTitletxtOrdNo.Size = New System.Drawing.Size(121, 13)
        Me.lblTitletxtOrdNo.TabIndex = 96
        Me.lblTitletxtOrdNo.Text = "オーダー番号"
        Me.lblTitletxtOrdNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitletxtOrdNo.TextValue = "オーダー番号"
        Me.lblTitletxtOrdNo.WidthDef = 121
        '
        'lblTitleOkuriNo
        '
        Me.lblTitleOkuriNo.AutoSizeDef = False
        Me.lblTitleOkuriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOkuriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOkuriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOkuriNo.EnableStatus = False
        Me.lblTitleOkuriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOkuriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOkuriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOkuriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOkuriNo.HeightDef = 13
        Me.lblTitleOkuriNo.Location = New System.Drawing.Point(920, 85)
        Me.lblTitleOkuriNo.Name = "lblTitleOkuriNo"
        Me.lblTitleOkuriNo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleOkuriNo.TabIndex = 95
        Me.lblTitleOkuriNo.Text = "送り状番号"
        Me.lblTitleOkuriNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOkuriNo.TextValue = "送り状番号"
        Me.lblTitleOkuriNo.WidthDef = 77
        '
        'lblExtcTariffRem
        '
        Me.lblExtcTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExtcTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExtcTariffRem.CountWrappedLine = False
        Me.lblExtcTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblExtcTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblExtcTariffRem.HeightDef = 18
        Me.lblExtcTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcTariffRem.HissuLabelVisible = False
        Me.lblExtcTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblExtcTariffRem.IsByteCheck = 0
        Me.lblExtcTariffRem.IsCalendarCheck = False
        Me.lblExtcTariffRem.IsDakutenCheck = False
        Me.lblExtcTariffRem.IsEisuCheck = False
        Me.lblExtcTariffRem.IsForbiddenWordsCheck = False
        Me.lblExtcTariffRem.IsFullByteCheck = 0
        Me.lblExtcTariffRem.IsHankakuCheck = False
        Me.lblExtcTariffRem.IsHissuCheck = False
        Me.lblExtcTariffRem.IsKanaCheck = False
        Me.lblExtcTariffRem.IsMiddleSpace = False
        Me.lblExtcTariffRem.IsNumericCheck = False
        Me.lblExtcTariffRem.IsSujiCheck = False
        Me.lblExtcTariffRem.IsZenkakuCheck = False
        Me.lblExtcTariffRem.ItemName = ""
        Me.lblExtcTariffRem.LineSpace = 0
        Me.lblExtcTariffRem.Location = New System.Drawing.Point(882, 145)
        Me.lblExtcTariffRem.MaxLength = 0
        Me.lblExtcTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblExtcTariffRem.MaxLineCount = 0
        Me.lblExtcTariffRem.Multiline = False
        Me.lblExtcTariffRem.Name = "lblExtcTariffRem"
        Me.lblExtcTariffRem.ReadOnly = True
        Me.lblExtcTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblExtcTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblExtcTariffRem.Size = New System.Drawing.Size(367, 18)
        Me.lblExtcTariffRem.TabIndex = 94
        Me.lblExtcTariffRem.TabStop = False
        Me.lblExtcTariffRem.TabStopSetting = False
        Me.lblExtcTariffRem.TextValue = ""
        Me.lblExtcTariffRem.UseSystemPasswordChar = False
        Me.lblExtcTariffRem.WidthDef = 367
        Me.lblExtcTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleExtcTariff
        '
        Me.lblTitleExtcTariff.AutoSizeDef = False
        Me.lblTitleExtcTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExtcTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExtcTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleExtcTariff.EnableStatus = False
        Me.lblTitleExtcTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExtcTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExtcTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExtcTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExtcTariff.HeightDef = 13
        Me.lblTitleExtcTariff.Location = New System.Drawing.Point(652, 148)
        Me.lblTitleExtcTariff.Name = "lblTitleExtcTariff"
        Me.lblTitleExtcTariff.Size = New System.Drawing.Size(115, 13)
        Me.lblTitleExtcTariff.TabIndex = 93
        Me.lblTitleExtcTariff.Text = "請求割増タリフ"
        Me.lblTitleExtcTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleExtcTariff.TextValue = "請求割増タリフ"
        Me.lblTitleExtcTariff.WidthDef = 115
        '
        'lblTariffRem
        '
        Me.lblTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTariffRem.CountWrappedLine = False
        Me.lblTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTariffRem.HeightDef = 18
        Me.lblTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffRem.HissuLabelVisible = False
        Me.lblTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTariffRem.IsByteCheck = 0
        Me.lblTariffRem.IsCalendarCheck = False
        Me.lblTariffRem.IsDakutenCheck = False
        Me.lblTariffRem.IsEisuCheck = False
        Me.lblTariffRem.IsForbiddenWordsCheck = False
        Me.lblTariffRem.IsFullByteCheck = 0
        Me.lblTariffRem.IsHankakuCheck = False
        Me.lblTariffRem.IsHissuCheck = False
        Me.lblTariffRem.IsKanaCheck = False
        Me.lblTariffRem.IsMiddleSpace = False
        Me.lblTariffRem.IsNumericCheck = False
        Me.lblTariffRem.IsSujiCheck = False
        Me.lblTariffRem.IsZenkakuCheck = False
        Me.lblTariffRem.ItemName = ""
        Me.lblTariffRem.LineSpace = 0
        Me.lblTariffRem.Location = New System.Drawing.Point(231, 145)
        Me.lblTariffRem.MaxLength = 0
        Me.lblTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffRem.MaxLineCount = 0
        Me.lblTariffRem.Multiline = False
        Me.lblTariffRem.Name = "lblTariffRem"
        Me.lblTariffRem.ReadOnly = True
        Me.lblTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffRem.Size = New System.Drawing.Size(433, 18)
        Me.lblTariffRem.TabIndex = 92
        Me.lblTariffRem.TabStop = False
        Me.lblTariffRem.TabStopSetting = False
        Me.lblTariffRem.TextValue = ""
        Me.lblTariffRem.UseSystemPasswordChar = False
        Me.lblTariffRem.WidthDef = 433
        Me.lblTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtExtcTariffCd
        '
        Me.txtExtcTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtExtcTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtExtcTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExtcTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtExtcTariffCd.CountWrappedLine = False
        Me.txtExtcTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtExtcTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtExtcTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtExtcTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtExtcTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtExtcTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtExtcTariffCd.HeightDef = 18
        Me.txtExtcTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtExtcTariffCd.HissuLabelVisible = False
        Me.txtExtcTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtExtcTariffCd.IsByteCheck = 10
        Me.txtExtcTariffCd.IsCalendarCheck = False
        Me.txtExtcTariffCd.IsDakutenCheck = False
        Me.txtExtcTariffCd.IsEisuCheck = False
        Me.txtExtcTariffCd.IsForbiddenWordsCheck = False
        Me.txtExtcTariffCd.IsFullByteCheck = 0
        Me.txtExtcTariffCd.IsHankakuCheck = False
        Me.txtExtcTariffCd.IsHissuCheck = False
        Me.txtExtcTariffCd.IsKanaCheck = False
        Me.txtExtcTariffCd.IsMiddleSpace = False
        Me.txtExtcTariffCd.IsNumericCheck = False
        Me.txtExtcTariffCd.IsSujiCheck = False
        Me.txtExtcTariffCd.IsZenkakuCheck = False
        Me.txtExtcTariffCd.ItemName = ""
        Me.txtExtcTariffCd.LineSpace = 0
        Me.txtExtcTariffCd.Location = New System.Drawing.Point(767, 145)
        Me.txtExtcTariffCd.MaxLength = 10
        Me.txtExtcTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtExtcTariffCd.MaxLineCount = 0
        Me.txtExtcTariffCd.Multiline = False
        Me.txtExtcTariffCd.Name = "txtExtcTariffCd"
        Me.txtExtcTariffCd.ReadOnly = False
        Me.txtExtcTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtExtcTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtExtcTariffCd.Size = New System.Drawing.Size(131, 18)
        Me.txtExtcTariffCd.TabIndex = 91
        Me.txtExtcTariffCd.TabStopSetting = True
        Me.txtExtcTariffCd.TextValue = ""
        Me.txtExtcTariffCd.UseSystemPasswordChar = False
        Me.txtExtcTariffCd.WidthDef = 131
        Me.txtExtcTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtTariffCd.Location = New System.Drawing.Point(116, 145)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(131, 18)
        Me.txtTariffCd.TabIndex = 90
        Me.txtTariffCd.TabStopSetting = True
        Me.txtTariffCd.TextValue = ""
        Me.txtTariffCd.UseSystemPasswordChar = False
        Me.txtTariffCd.WidthDef = 131
        Me.txtTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTariff
        '
        Me.lblTitleTariff.AutoSizeDef = False
        Me.lblTitleTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariff.EnableStatus = False
        Me.lblTitleTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.HeightDef = 13
        Me.lblTitleTariff.Location = New System.Drawing.Point(39, 148)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariff.TabIndex = 89
        Me.lblTitleTariff.Text = "請求タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "請求タリフ"
        Me.lblTitleTariff.WidthDef = 77
        '
        'lblShipNm
        '
        Me.lblShipNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShipNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShipNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShipNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipNm.CountWrappedLine = False
        Me.lblShipNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblShipNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShipNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShipNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShipNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShipNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblShipNm.HeightDef = 18
        Me.lblShipNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShipNm.HissuLabelVisible = False
        Me.lblShipNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblShipNm.IsByteCheck = 0
        Me.lblShipNm.IsCalendarCheck = False
        Me.lblShipNm.IsDakutenCheck = False
        Me.lblShipNm.IsEisuCheck = False
        Me.lblShipNm.IsForbiddenWordsCheck = False
        Me.lblShipNm.IsFullByteCheck = 0
        Me.lblShipNm.IsHankakuCheck = False
        Me.lblShipNm.IsHissuCheck = False
        Me.lblShipNm.IsKanaCheck = False
        Me.lblShipNm.IsMiddleSpace = False
        Me.lblShipNm.IsNumericCheck = False
        Me.lblShipNm.IsSujiCheck = False
        Me.lblShipNm.IsZenkakuCheck = False
        Me.lblShipNm.ItemName = ""
        Me.lblShipNm.LineSpace = 0
        Me.lblShipNm.Location = New System.Drawing.Point(231, 124)
        Me.lblShipNm.MaxLength = 0
        Me.lblShipNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblShipNm.MaxLineCount = 0
        Me.lblShipNm.Multiline = False
        Me.lblShipNm.Name = "lblShipNm"
        Me.lblShipNm.ReadOnly = True
        Me.lblShipNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblShipNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblShipNm.Size = New System.Drawing.Size(545, 18)
        Me.lblShipNm.TabIndex = 87
        Me.lblShipNm.TabStop = False
        Me.lblShipNm.TabStopSetting = False
        Me.lblShipNm.TextValue = ""
        Me.lblShipNm.UseSystemPasswordChar = False
        Me.lblShipNm.WidthDef = 545
        Me.lblShipNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtShipCd
        '
        Me.txtShipCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShipCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShipCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShipCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShipCd.CountWrappedLine = False
        Me.txtShipCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShipCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShipCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShipCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShipCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShipCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShipCd.HeightDef = 18
        Me.txtShipCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShipCd.HissuLabelVisible = False
        Me.txtShipCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtShipCd.IsByteCheck = 15
        Me.txtShipCd.IsCalendarCheck = False
        Me.txtShipCd.IsDakutenCheck = False
        Me.txtShipCd.IsEisuCheck = False
        Me.txtShipCd.IsForbiddenWordsCheck = False
        Me.txtShipCd.IsFullByteCheck = 0
        Me.txtShipCd.IsHankakuCheck = False
        Me.txtShipCd.IsHissuCheck = False
        Me.txtShipCd.IsKanaCheck = False
        Me.txtShipCd.IsMiddleSpace = False
        Me.txtShipCd.IsNumericCheck = False
        Me.txtShipCd.IsSujiCheck = False
        Me.txtShipCd.IsZenkakuCheck = False
        Me.txtShipCd.ItemName = ""
        Me.txtShipCd.LineSpace = 0
        Me.txtShipCd.Location = New System.Drawing.Point(116, 124)
        Me.txtShipCd.MaxLength = 15
        Me.txtShipCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShipCd.MaxLineCount = 0
        Me.txtShipCd.Multiline = False
        Me.txtShipCd.Name = "txtShipCd"
        Me.txtShipCd.ReadOnly = False
        Me.txtShipCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShipCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShipCd.Size = New System.Drawing.Size(131, 18)
        Me.txtShipCd.TabIndex = 86
        Me.txtShipCd.TabStopSetting = True
        Me.txtShipCd.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtShipCd.UseSystemPasswordChar = False
        Me.txtShipCd.WidthDef = 131
        Me.txtShipCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleShip
        '
        Me.lblTitleShip.AutoSizeDef = False
        Me.lblTitleShip.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShip.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShip.EnableStatus = False
        Me.lblTitleShip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShip.HeightDef = 13
        Me.lblTitleShip.Location = New System.Drawing.Point(56, 127)
        Me.lblTitleShip.Name = "lblTitleShip"
        Me.lblTitleShip.Size = New System.Drawing.Size(60, 13)
        Me.lblTitleShip.TabIndex = 85
        Me.lblTitleShip.Text = "荷送人"
        Me.lblTitleShip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShip.TextValue = "荷送人"
        Me.lblTitleShip.WidthDef = 60
        '
        'lblUnsocoNm
        '
        Me.lblUnsocoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsocoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsocoNm.CountWrappedLine = False
        Me.lblUnsocoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsocoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsocoNm.HeightDef = 18
        Me.lblUnsocoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.HissuLabelVisible = True
        Me.lblUnsocoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsocoNm.IsByteCheck = 0
        Me.lblUnsocoNm.IsCalendarCheck = False
        Me.lblUnsocoNm.IsDakutenCheck = False
        Me.lblUnsocoNm.IsEisuCheck = False
        Me.lblUnsocoNm.IsForbiddenWordsCheck = False
        Me.lblUnsocoNm.IsFullByteCheck = 0
        Me.lblUnsocoNm.IsHankakuCheck = False
        Me.lblUnsocoNm.IsHissuCheck = True
        Me.lblUnsocoNm.IsKanaCheck = False
        Me.lblUnsocoNm.IsMiddleSpace = False
        Me.lblUnsocoNm.IsNumericCheck = False
        Me.lblUnsocoNm.IsSujiCheck = False
        Me.lblUnsocoNm.IsZenkakuCheck = False
        Me.lblUnsocoNm.ItemName = ""
        Me.lblUnsocoNm.LineSpace = 0
        Me.lblUnsocoNm.Location = New System.Drawing.Point(211, 82)
        Me.lblUnsocoNm.MaxLength = 0
        Me.lblUnsocoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsocoNm.MaxLineCount = 0
        Me.lblUnsocoNm.Multiline = False
        Me.lblUnsocoNm.Name = "lblUnsocoNm"
        Me.lblUnsocoNm.ReadOnly = True
        Me.lblUnsocoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsocoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsocoNm.Size = New System.Drawing.Size(565, 18)
        Me.lblUnsocoNm.TabIndex = 84
        Me.lblUnsocoNm.TabStop = False
        Me.lblUnsocoNm.TabStopSetting = False
        Me.lblUnsocoNm.TextValue = ""
        Me.lblUnsocoNm.UseSystemPasswordChar = False
        Me.lblUnsocoNm.WidthDef = 565
        Me.lblUnsocoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoBrCd
        '
        Me.txtUnsocoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoBrCd.CountWrappedLine = False
        Me.txtUnsocoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoBrCd.HeightDef = 18
        Me.txtUnsocoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCd.HissuLabelVisible = False
        Me.txtUnsocoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoBrCd.IsByteCheck = 3
        Me.txtUnsocoBrCd.IsCalendarCheck = False
        Me.txtUnsocoBrCd.IsDakutenCheck = False
        Me.txtUnsocoBrCd.IsEisuCheck = False
        Me.txtUnsocoBrCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoBrCd.IsFullByteCheck = 0
        Me.txtUnsocoBrCd.IsHankakuCheck = False
        Me.txtUnsocoBrCd.IsHissuCheck = False
        Me.txtUnsocoBrCd.IsKanaCheck = False
        Me.txtUnsocoBrCd.IsMiddleSpace = False
        Me.txtUnsocoBrCd.IsNumericCheck = False
        Me.txtUnsocoBrCd.IsSujiCheck = False
        Me.txtUnsocoBrCd.IsZenkakuCheck = False
        Me.txtUnsocoBrCd.ItemName = ""
        Me.txtUnsocoBrCd.LineSpace = 0
        Me.txtUnsocoBrCd.Location = New System.Drawing.Point(182, 82)
        Me.txtUnsocoBrCd.MaxLength = 3
        Me.txtUnsocoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoBrCd.MaxLineCount = 0
        Me.txtUnsocoBrCd.Multiline = False
        Me.txtUnsocoBrCd.Name = "txtUnsocoBrCd"
        Me.txtUnsocoBrCd.ReadOnly = False
        Me.txtUnsocoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoBrCd.Size = New System.Drawing.Size(45, 18)
        Me.txtUnsocoBrCd.TabIndex = 83
        Me.txtUnsocoBrCd.TabStopSetting = True
        Me.txtUnsocoBrCd.TextValue = ""
        Me.txtUnsocoBrCd.UseSystemPasswordChar = False
        Me.txtUnsocoBrCd.WidthDef = 45
        Me.txtUnsocoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoCd
        '
        Me.txtUnsocoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoCd.CountWrappedLine = False
        Me.txtUnsocoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoCd.HeightDef = 18
        Me.txtUnsocoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCd.HissuLabelVisible = False
        Me.txtUnsocoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoCd.IsByteCheck = 5
        Me.txtUnsocoCd.IsCalendarCheck = False
        Me.txtUnsocoCd.IsDakutenCheck = False
        Me.txtUnsocoCd.IsEisuCheck = False
        Me.txtUnsocoCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoCd.IsFullByteCheck = 0
        Me.txtUnsocoCd.IsHankakuCheck = False
        Me.txtUnsocoCd.IsHissuCheck = False
        Me.txtUnsocoCd.IsKanaCheck = False
        Me.txtUnsocoCd.IsMiddleSpace = False
        Me.txtUnsocoCd.IsNumericCheck = False
        Me.txtUnsocoCd.IsSujiCheck = False
        Me.txtUnsocoCd.IsZenkakuCheck = False
        Me.txtUnsocoCd.ItemName = ""
        Me.txtUnsocoCd.LineSpace = 0
        Me.txtUnsocoCd.Location = New System.Drawing.Point(116, 82)
        Me.txtUnsocoCd.MaxLength = 5
        Me.txtUnsocoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoCd.MaxLineCount = 0
        Me.txtUnsocoCd.Multiline = False
        Me.txtUnsocoCd.Name = "txtUnsocoCd"
        Me.txtUnsocoCd.ReadOnly = False
        Me.txtUnsocoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoCd.Size = New System.Drawing.Size(82, 18)
        Me.txtUnsocoCd.TabIndex = 82
        Me.txtUnsocoCd.TabStopSetting = True
        Me.txtUnsocoCd.TextValue = ""
        Me.txtUnsocoCd.UseSystemPasswordChar = False
        Me.txtUnsocoCd.WidthDef = 82
        Me.txtUnsocoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoco
        '
        Me.lblTitleUnsoco.AutoSizeDef = False
        Me.lblTitleUnsoco.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoco.EnableStatus = False
        Me.lblTitleUnsoco.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.HeightDef = 13
        Me.lblTitleUnsoco.Location = New System.Drawing.Point(9, 85)
        Me.lblTitleUnsoco.Name = "lblTitleUnsoco"
        Me.lblTitleUnsoco.Size = New System.Drawing.Size(107, 13)
        Me.lblTitleUnsoco.TabIndex = 81
        Me.lblTitleUnsoco.Text = "運送会社"
        Me.lblTitleUnsoco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoco.TextValue = "運送会社"
        Me.lblTitleUnsoco.WidthDef = 107
        '
        'cmbTehaiKbn
        '
        Me.cmbTehaiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTehaiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTehaiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTehaiKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTehaiKbn.DataCode = "U005"
        Me.cmbTehaiKbn.DataSource = Nothing
        Me.cmbTehaiKbn.DisplayMember = Nothing
        Me.cmbTehaiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTehaiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTehaiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTehaiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTehaiKbn.HeightDef = 18
        Me.cmbTehaiKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTehaiKbn.HissuLabelVisible = False
        Me.cmbTehaiKbn.InsertWildCard = True
        Me.cmbTehaiKbn.IsForbiddenWordsCheck = False
        Me.cmbTehaiKbn.IsHissuCheck = False
        Me.cmbTehaiKbn.ItemName = ""
        Me.cmbTehaiKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTehaiKbn.Location = New System.Drawing.Point(116, 61)
        Me.cmbTehaiKbn.Name = "cmbTehaiKbn"
        Me.cmbTehaiKbn.ReadOnly = True
        Me.cmbTehaiKbn.SelectedIndex = -1
        Me.cmbTehaiKbn.SelectedItem = Nothing
        Me.cmbTehaiKbn.SelectedText = ""
        Me.cmbTehaiKbn.SelectedValue = ""
        Me.cmbTehaiKbn.Size = New System.Drawing.Size(111, 18)
        Me.cmbTehaiKbn.TabIndex = 80
        Me.cmbTehaiKbn.TabStop = False
        Me.cmbTehaiKbn.TabStopSetting = False
        Me.cmbTehaiKbn.TextValue = ""
        Me.cmbTehaiKbn.Value1 = Nothing
        Me.cmbTehaiKbn.Value2 = Nothing
        Me.cmbTehaiKbn.Value3 = Nothing
        Me.cmbTehaiKbn.ValueMember = Nothing
        Me.cmbTehaiKbn.WidthDef = 111
        '
        'lblTitleUnsoMotoKbn
        '
        Me.lblTitleUnsoMotoKbn.AutoSizeDef = False
        Me.lblTitleUnsoMotoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoMotoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoMotoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoMotoKbn.EnableStatus = False
        Me.lblTitleUnsoMotoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoMotoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoMotoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoMotoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoMotoKbn.HeightDef = 13
        Me.lblTitleUnsoMotoKbn.Location = New System.Drawing.Point(6, 64)
        Me.lblTitleUnsoMotoKbn.Name = "lblTitleUnsoMotoKbn"
        Me.lblTitleUnsoMotoKbn.Size = New System.Drawing.Size(110, 13)
        Me.lblTitleUnsoMotoKbn.TabIndex = 79
        Me.lblTitleUnsoMotoKbn.Text = "運送手配区分"
        Me.lblTitleUnsoMotoKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoMotoKbn.TextValue = "運送手配区分"
        Me.lblTitleUnsoMotoKbn.WidthDef = 110
        '
        'cmbPcKbn
        '
        Me.cmbPcKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPcKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPcKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPcKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPcKbn.DataCode = "M001"
        Me.cmbPcKbn.DataSource = Nothing
        Me.cmbPcKbn.DisplayMember = Nothing
        Me.cmbPcKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPcKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPcKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPcKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPcKbn.HeightDef = 18
        Me.cmbPcKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPcKbn.HissuLabelVisible = True
        Me.cmbPcKbn.InsertWildCard = True
        Me.cmbPcKbn.IsForbiddenWordsCheck = False
        Me.cmbPcKbn.IsHissuCheck = True
        Me.cmbPcKbn.ItemName = ""
        Me.cmbPcKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPcKbn.Location = New System.Drawing.Point(574, 40)
        Me.cmbPcKbn.Name = "cmbPcKbn"
        Me.cmbPcKbn.ReadOnly = False
        Me.cmbPcKbn.SelectedIndex = -1
        Me.cmbPcKbn.SelectedItem = Nothing
        Me.cmbPcKbn.SelectedText = ""
        Me.cmbPcKbn.SelectedValue = ""
        Me.cmbPcKbn.Size = New System.Drawing.Size(111, 18)
        Me.cmbPcKbn.TabIndex = 76
        Me.cmbPcKbn.TabStopSetting = True
        Me.cmbPcKbn.TextValue = ""
        Me.cmbPcKbn.Value1 = Nothing
        Me.cmbPcKbn.Value2 = Nothing
        Me.cmbPcKbn.Value3 = Nothing
        Me.cmbPcKbn.ValueMember = Nothing
        Me.cmbPcKbn.WidthDef = 111
        '
        'lblTitlePcKbn
        '
        Me.lblTitlePcKbn.AutoSizeDef = False
        Me.lblTitlePcKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePcKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePcKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePcKbn.EnableStatus = False
        Me.lblTitlePcKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePcKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePcKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePcKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePcKbn.HeightDef = 13
        Me.lblTitlePcKbn.Location = New System.Drawing.Point(455, 43)
        Me.lblTitlePcKbn.Name = "lblTitlePcKbn"
        Me.lblTitlePcKbn.Size = New System.Drawing.Size(119, 13)
        Me.lblTitlePcKbn.TabIndex = 75
        Me.lblTitlePcKbn.Text = "元着払区分"
        Me.lblTitlePcKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePcKbn.TextValue = "元着払区分"
        Me.lblTitlePcKbn.WidthDef = 119
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
        Me.cmbSharyoKbn.Location = New System.Drawing.Point(795, 61)
        Me.cmbSharyoKbn.Name = "cmbSharyoKbn"
        Me.cmbSharyoKbn.ReadOnly = False
        Me.cmbSharyoKbn.SelectedIndex = -1
        Me.cmbSharyoKbn.SelectedItem = Nothing
        Me.cmbSharyoKbn.SelectedText = ""
        Me.cmbSharyoKbn.SelectedValue = ""
        Me.cmbSharyoKbn.Size = New System.Drawing.Size(103, 18)
        Me.cmbSharyoKbn.TabIndex = 74
        Me.cmbSharyoKbn.TabStopSetting = True
        Me.cmbSharyoKbn.TextValue = ""
        Me.cmbSharyoKbn.Value1 = Nothing
        Me.cmbSharyoKbn.Value2 = Nothing
        Me.cmbSharyoKbn.Value3 = Nothing
        Me.cmbSharyoKbn.ValueMember = Nothing
        Me.cmbSharyoKbn.WidthDef = 103
        '
        'lblTitleSharyoKbn
        '
        Me.lblTitleSharyoKbn.AutoSizeDef = False
        Me.lblTitleSharyoKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSharyoKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSharyoKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSharyoKbn.EnableStatus = False
        Me.lblTitleSharyoKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSharyoKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSharyoKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSharyoKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSharyoKbn.HeightDef = 13
        Me.lblTitleSharyoKbn.Location = New System.Drawing.Point(678, 64)
        Me.lblTitleSharyoKbn.Name = "lblTitleSharyoKbn"
        Me.lblTitleSharyoKbn.Size = New System.Drawing.Size(117, 13)
        Me.lblTitleSharyoKbn.TabIndex = 73
        Me.lblTitleSharyoKbn.Text = "車輌区分"
        Me.lblTitleSharyoKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSharyoKbn.TextValue = "車輌区分"
        Me.lblTitleSharyoKbn.WidthDef = 117
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
        Me.cmbTariffKbn.HissuLabelVisible = True
        Me.cmbTariffKbn.InsertWildCard = True
        Me.cmbTariffKbn.IsForbiddenWordsCheck = False
        Me.cmbTariffKbn.IsHissuCheck = True
        Me.cmbTariffKbn.ItemName = ""
        Me.cmbTariffKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTariffKbn.Location = New System.Drawing.Point(574, 61)
        Me.cmbTariffKbn.Name = "cmbTariffKbn"
        Me.cmbTariffKbn.ReadOnly = False
        Me.cmbTariffKbn.SelectedIndex = -1
        Me.cmbTariffKbn.SelectedItem = Nothing
        Me.cmbTariffKbn.SelectedText = ""
        Me.cmbTariffKbn.SelectedValue = ""
        Me.cmbTariffKbn.Size = New System.Drawing.Size(111, 18)
        Me.cmbTariffKbn.TabIndex = 72
        Me.cmbTariffKbn.TabStopSetting = True
        Me.cmbTariffKbn.TextValue = ""
        Me.cmbTariffKbn.Value1 = Nothing
        Me.cmbTariffKbn.Value2 = Nothing
        Me.cmbTariffKbn.Value3 = Nothing
        Me.cmbTariffKbn.ValueMember = Nothing
        Me.cmbTariffKbn.WidthDef = 111
        '
        'lblTitleTehaiKbn
        '
        Me.lblTitleTehaiKbn.AutoSizeDef = False
        Me.lblTitleTehaiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehaiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehaiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTehaiKbn.EnableStatus = False
        Me.lblTitleTehaiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehaiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehaiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehaiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehaiKbn.HeightDef = 13
        Me.lblTitleTehaiKbn.Location = New System.Drawing.Point(470, 64)
        Me.lblTitleTehaiKbn.Name = "lblTitleTehaiKbn"
        Me.lblTitleTehaiKbn.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleTehaiKbn.TabIndex = 71
        Me.lblTitleTehaiKbn.Text = "タリフ分類区分"
        Me.lblTitleTehaiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTehaiKbn.TextValue = "タリフ分類区分"
        Me.lblTitleTehaiKbn.WidthDef = 105
        '
        'cmbBinKbn
        '
        Me.cmbBinKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBinKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBinKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBinKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBinKbn.DataCode = "U001"
        Me.cmbBinKbn.DataSource = Nothing
        Me.cmbBinKbn.DisplayMember = Nothing
        Me.cmbBinKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBinKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBinKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBinKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBinKbn.HeightDef = 18
        Me.cmbBinKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBinKbn.HissuLabelVisible = False
        Me.cmbBinKbn.InsertWildCard = True
        Me.cmbBinKbn.IsForbiddenWordsCheck = False
        Me.cmbBinKbn.IsHissuCheck = False
        Me.cmbBinKbn.ItemName = ""
        Me.cmbBinKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBinKbn.Location = New System.Drawing.Point(343, 61)
        Me.cmbBinKbn.Name = "cmbBinKbn"
        Me.cmbBinKbn.ReadOnly = False
        Me.cmbBinKbn.SelectedIndex = -1
        Me.cmbBinKbn.SelectedItem = Nothing
        Me.cmbBinKbn.SelectedText = ""
        Me.cmbBinKbn.SelectedValue = ""
        Me.cmbBinKbn.Size = New System.Drawing.Size(113, 18)
        Me.cmbBinKbn.TabIndex = 70
        Me.cmbBinKbn.TabStopSetting = True
        Me.cmbBinKbn.TextValue = ""
        Me.cmbBinKbn.Value1 = Nothing
        Me.cmbBinKbn.Value2 = Nothing
        Me.cmbBinKbn.Value3 = Nothing
        Me.cmbBinKbn.ValueMember = Nothing
        Me.cmbBinKbn.WidthDef = 113
        '
        'lblTitleBinKbn
        '
        Me.lblTitleBinKbn.AutoSizeDef = False
        Me.lblTitleBinKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBinKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBinKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBinKbn.EnableStatus = False
        Me.lblTitleBinKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBinKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBinKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBinKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBinKbn.HeightDef = 13
        Me.lblTitleBinKbn.Location = New System.Drawing.Point(254, 64)
        Me.lblTitleBinKbn.Name = "lblTitleBinKbn"
        Me.lblTitleBinKbn.Size = New System.Drawing.Size(89, 13)
        Me.lblTitleBinKbn.TabIndex = 69
        Me.lblTitleBinKbn.Text = "便区分"
        Me.lblTitleBinKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBinKbn.TextValue = "便区分"
        Me.lblTitleBinKbn.WidthDef = 89
        '
        'cmbUnsoJiyuKbn
        '
        Me.cmbUnsoJiyuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnsoJiyuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnsoJiyuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbUnsoJiyuKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbUnsoJiyuKbn.DataCode = "U004"
        Me.cmbUnsoJiyuKbn.DataSource = Nothing
        Me.cmbUnsoJiyuKbn.DisplayMember = Nothing
        Me.cmbUnsoJiyuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnsoJiyuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnsoJiyuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnsoJiyuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnsoJiyuKbn.HeightDef = 18
        Me.cmbUnsoJiyuKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbUnsoJiyuKbn.HissuLabelVisible = False
        Me.cmbUnsoJiyuKbn.InsertWildCard = True
        Me.cmbUnsoJiyuKbn.IsForbiddenWordsCheck = False
        Me.cmbUnsoJiyuKbn.IsHissuCheck = False
        Me.cmbUnsoJiyuKbn.ItemName = ""
        Me.cmbUnsoJiyuKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbUnsoJiyuKbn.Location = New System.Drawing.Point(342, 40)
        Me.cmbUnsoJiyuKbn.Name = "cmbUnsoJiyuKbn"
        Me.cmbUnsoJiyuKbn.ReadOnly = False
        Me.cmbUnsoJiyuKbn.SelectedIndex = -1
        Me.cmbUnsoJiyuKbn.SelectedItem = Nothing
        Me.cmbUnsoJiyuKbn.SelectedText = ""
        Me.cmbUnsoJiyuKbn.SelectedValue = ""
        Me.cmbUnsoJiyuKbn.Size = New System.Drawing.Size(114, 18)
        Me.cmbUnsoJiyuKbn.TabIndex = 66
        Me.cmbUnsoJiyuKbn.TabStopSetting = True
        Me.cmbUnsoJiyuKbn.TextValue = ""
        Me.cmbUnsoJiyuKbn.Value1 = Nothing
        Me.cmbUnsoJiyuKbn.Value2 = Nothing
        Me.cmbUnsoJiyuKbn.Value3 = Nothing
        Me.cmbUnsoJiyuKbn.ValueMember = Nothing
        Me.cmbUnsoJiyuKbn.WidthDef = 114
        '
        'lblTitleUnsoJiyu
        '
        Me.lblTitleUnsoJiyu.AutoSize = True
        Me.lblTitleUnsoJiyu.AutoSizeDef = True
        Me.lblTitleUnsoJiyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoJiyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoJiyu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoJiyu.EnableStatus = False
        Me.lblTitleUnsoJiyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoJiyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoJiyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoJiyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoJiyu.HeightDef = 13
        Me.lblTitleUnsoJiyu.Location = New System.Drawing.Point(251, 43)
        Me.lblTitleUnsoJiyu.Name = "lblTitleUnsoJiyu"
        Me.lblTitleUnsoJiyu.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleUnsoJiyu.TabIndex = 65
        Me.lblTitleUnsoJiyu.Text = "運送事由区分"
        Me.lblTitleUnsoJiyu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoJiyu.TextValue = "運送事由区分"
        Me.lblTitleUnsoJiyu.WidthDef = 91
        '
        'cmbMotoDataKbn
        '
        Me.cmbMotoDataKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMotoDataKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMotoDataKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbMotoDataKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbMotoDataKbn.DataCode = "M004"
        Me.cmbMotoDataKbn.DataSource = Nothing
        Me.cmbMotoDataKbn.DisplayMember = Nothing
        Me.cmbMotoDataKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMotoDataKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMotoDataKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMotoDataKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMotoDataKbn.HeightDef = 18
        Me.cmbMotoDataKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbMotoDataKbn.HissuLabelVisible = False
        Me.cmbMotoDataKbn.InsertWildCard = True
        Me.cmbMotoDataKbn.IsForbiddenWordsCheck = False
        Me.cmbMotoDataKbn.IsHissuCheck = False
        Me.cmbMotoDataKbn.ItemName = ""
        Me.cmbMotoDataKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbMotoDataKbn.Location = New System.Drawing.Point(115, 40)
        Me.cmbMotoDataKbn.Name = "cmbMotoDataKbn"
        Me.cmbMotoDataKbn.ReadOnly = False
        Me.cmbMotoDataKbn.SelectedIndex = -1
        Me.cmbMotoDataKbn.SelectedItem = Nothing
        Me.cmbMotoDataKbn.SelectedText = ""
        Me.cmbMotoDataKbn.SelectedValue = ""
        Me.cmbMotoDataKbn.Size = New System.Drawing.Size(112, 18)
        Me.cmbMotoDataKbn.TabIndex = 64
        Me.cmbMotoDataKbn.TabStopSetting = True
        Me.cmbMotoDataKbn.TextValue = ""
        Me.cmbMotoDataKbn.Value1 = "='1.000'"
        Me.cmbMotoDataKbn.Value2 = Nothing
        Me.cmbMotoDataKbn.Value3 = Nothing
        Me.cmbMotoDataKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V1
        Me.cmbMotoDataKbn.ValueMember = Nothing
        Me.cmbMotoDataKbn.WidthDef = 112
        '
        'lblTitleMotoData
        '
        Me.lblTitleMotoData.AutoSizeDef = False
        Me.lblTitleMotoData.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMotoData.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMotoData.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMotoData.EnableStatus = False
        Me.lblTitleMotoData.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMotoData.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMotoData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMotoData.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMotoData.HeightDef = 13
        Me.lblTitleMotoData.Location = New System.Drawing.Point(24, 43)
        Me.lblTitleMotoData.Name = "lblTitleMotoData"
        Me.lblTitleMotoData.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleMotoData.TabIndex = 44
        Me.lblTitleMotoData.Text = "元データ区分"
        Me.lblTitleMotoData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMotoData.TextValue = "元データ区分"
        Me.lblTitleMotoData.WidthDef = 91
        '
        'lblKanriNo
        '
        Me.lblKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKanriNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKanriNo.CountWrappedLine = False
        Me.lblKanriNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanriNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblKanriNo.HeightDef = 18
        Me.lblKanriNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanriNo.HissuLabelVisible = False
        Me.lblKanriNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblKanriNo.IsByteCheck = 0
        Me.lblKanriNo.IsCalendarCheck = False
        Me.lblKanriNo.IsDakutenCheck = False
        Me.lblKanriNo.IsEisuCheck = False
        Me.lblKanriNo.IsForbiddenWordsCheck = False
        Me.lblKanriNo.IsFullByteCheck = 0
        Me.lblKanriNo.IsHankakuCheck = False
        Me.lblKanriNo.IsHissuCheck = False
        Me.lblKanriNo.IsKanaCheck = False
        Me.lblKanriNo.IsMiddleSpace = False
        Me.lblKanriNo.IsNumericCheck = False
        Me.lblKanriNo.IsSujiCheck = False
        Me.lblKanriNo.IsZenkakuCheck = False
        Me.lblKanriNo.ItemName = ""
        Me.lblKanriNo.LineSpace = 0
        Me.lblKanriNo.Location = New System.Drawing.Point(997, 61)
        Me.lblKanriNo.MaxLength = 0
        Me.lblKanriNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblKanriNo.MaxLineCount = 0
        Me.lblKanriNo.Multiline = False
        Me.lblKanriNo.Name = "lblKanriNo"
        Me.lblKanriNo.ReadOnly = True
        Me.lblKanriNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblKanriNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblKanriNo.Size = New System.Drawing.Size(124, 18)
        Me.lblKanriNo.TabIndex = 43
        Me.lblKanriNo.TabStop = False
        Me.lblKanriNo.TabStopSetting = False
        Me.lblKanriNo.TextValue = ""
        Me.lblKanriNo.UseSystemPasswordChar = False
        Me.lblKanriNo.WidthDef = 124
        Me.lblKanriNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleKanriNo
        '
        Me.lblTitleKanriNo.AutoSizeDef = False
        Me.lblTitleKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKanriNo.EnableStatus = False
        Me.lblTitleKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNo.HeightDef = 13
        Me.lblTitleKanriNo.Location = New System.Drawing.Point(934, 64)
        Me.lblTitleKanriNo.Name = "lblTitleKanriNo"
        Me.lblTitleKanriNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleKanriNo.TabIndex = 42
        Me.lblTitleKanriNo.Text = "管理番号"
        Me.lblTitleKanriNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKanriNo.TextValue = "管理番号"
        Me.lblTitleKanriNo.WidthDef = 63
        '
        'lblUnkoNo
        '
        Me.lblUnkoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnkoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnkoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnkoNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnkoNo.CountWrappedLine = False
        Me.lblUnkoNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnkoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnkoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnkoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnkoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnkoNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnkoNo.HeightDef = 18
        Me.lblUnkoNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnkoNo.HissuLabelVisible = False
        Me.lblUnkoNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnkoNo.IsByteCheck = 0
        Me.lblUnkoNo.IsCalendarCheck = False
        Me.lblUnkoNo.IsDakutenCheck = False
        Me.lblUnkoNo.IsEisuCheck = False
        Me.lblUnkoNo.IsForbiddenWordsCheck = False
        Me.lblUnkoNo.IsFullByteCheck = 0
        Me.lblUnkoNo.IsHankakuCheck = False
        Me.lblUnkoNo.IsHissuCheck = False
        Me.lblUnkoNo.IsKanaCheck = False
        Me.lblUnkoNo.IsMiddleSpace = False
        Me.lblUnkoNo.IsNumericCheck = False
        Me.lblUnkoNo.IsSujiCheck = False
        Me.lblUnkoNo.IsZenkakuCheck = False
        Me.lblUnkoNo.ItemName = ""
        Me.lblUnkoNo.LineSpace = 0
        Me.lblUnkoNo.Location = New System.Drawing.Point(997, 40)
        Me.lblUnkoNo.MaxLength = 0
        Me.lblUnkoNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnkoNo.MaxLineCount = 0
        Me.lblUnkoNo.Multiline = False
        Me.lblUnkoNo.Name = "lblUnkoNo"
        Me.lblUnkoNo.ReadOnly = True
        Me.lblUnkoNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnkoNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnkoNo.Size = New System.Drawing.Size(124, 18)
        Me.lblUnkoNo.TabIndex = 41
        Me.lblUnkoNo.TabStop = False
        Me.lblUnkoNo.TabStopSetting = False
        Me.lblUnkoNo.TextValue = ""
        Me.lblUnkoNo.UseSystemPasswordChar = False
        Me.lblUnkoNo.WidthDef = 124
        Me.lblUnkoNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnkoNo
        '
        Me.lblTitleUnkoNo.AutoSizeDef = False
        Me.lblTitleUnkoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnkoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnkoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnkoNo.EnableStatus = False
        Me.lblTitleUnkoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnkoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnkoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnkoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnkoNo.HeightDef = 13
        Me.lblTitleUnkoNo.Location = New System.Drawing.Point(934, 43)
        Me.lblTitleUnkoNo.Name = "lblTitleUnkoNo"
        Me.lblTitleUnkoNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnkoNo.TabIndex = 40
        Me.lblTitleUnkoNo.Text = "運行番号"
        Me.lblTitleUnkoNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnkoNo.TextValue = "運行番号"
        Me.lblTitleUnkoNo.WidthDef = 63
        '
        'lblUnsoNo
        '
        Me.lblUnsoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoNo.CountWrappedLine = False
        Me.lblUnsoNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoNo.HeightDef = 18
        Me.lblUnsoNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNo.HissuLabelVisible = False
        Me.lblUnsoNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsoNo.IsByteCheck = 0
        Me.lblUnsoNo.IsCalendarCheck = False
        Me.lblUnsoNo.IsDakutenCheck = False
        Me.lblUnsoNo.IsEisuCheck = False
        Me.lblUnsoNo.IsForbiddenWordsCheck = False
        Me.lblUnsoNo.IsFullByteCheck = 0
        Me.lblUnsoNo.IsHankakuCheck = False
        Me.lblUnsoNo.IsHissuCheck = False
        Me.lblUnsoNo.IsKanaCheck = False
        Me.lblUnsoNo.IsMiddleSpace = False
        Me.lblUnsoNo.IsNumericCheck = False
        Me.lblUnsoNo.IsSujiCheck = False
        Me.lblUnsoNo.IsZenkakuCheck = False
        Me.lblUnsoNo.ItemName = ""
        Me.lblUnsoNo.LineSpace = 0
        Me.lblUnsoNo.Location = New System.Drawing.Point(997, 19)
        Me.lblUnsoNo.MaxLength = 0
        Me.lblUnsoNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoNo.MaxLineCount = 0
        Me.lblUnsoNo.Multiline = False
        Me.lblUnsoNo.Name = "lblUnsoNo"
        Me.lblUnsoNo.ReadOnly = True
        Me.lblUnsoNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoNo.Size = New System.Drawing.Size(124, 18)
        Me.lblUnsoNo.TabIndex = 39
        Me.lblUnsoNo.TabStop = False
        Me.lblUnsoNo.TabStopSetting = False
        Me.lblUnsoNo.TextValue = ""
        Me.lblUnsoNo.UseSystemPasswordChar = False
        Me.lblUnsoNo.WidthDef = 124
        Me.lblUnsoNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoNo
        '
        Me.lblTitleUnsoNo.AutoSizeDef = False
        Me.lblTitleUnsoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoNo.EnableStatus = False
        Me.lblTitleUnsoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNo.HeightDef = 13
        Me.lblTitleUnsoNo.Location = New System.Drawing.Point(909, 22)
        Me.lblTitleUnsoNo.Name = "lblTitleUnsoNo"
        Me.lblTitleUnsoNo.Size = New System.Drawing.Size(88, 13)
        Me.lblTitleUnsoNo.TabIndex = 38
        Me.lblTitleUnsoNo.Text = "運送番号"
        Me.lblTitleUnsoNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoNo.TextValue = "運送番号"
        Me.lblTitleUnsoNo.WidthDef = 88
        '
        'lblTitleYosoEigyo
        '
        Me.lblTitleYosoEigyo.AutoSizeDef = False
        Me.lblTitleYosoEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYosoEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYosoEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYosoEigyo.EnableStatus = False
        Me.lblTitleYosoEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYosoEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYosoEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYosoEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYosoEigyo.HeightDef = 13
        Me.lblTitleYosoEigyo.Location = New System.Drawing.Point(459, 22)
        Me.lblTitleYosoEigyo.Name = "lblTitleYosoEigyo"
        Me.lblTitleYosoEigyo.Size = New System.Drawing.Size(116, 13)
        Me.lblTitleYosoEigyo.TabIndex = 36
        Me.lblTitleYosoEigyo.Text = "輸送部営業所"
        Me.lblTitleYosoEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYosoEigyo.TextValue = "輸送部営業所"
        Me.lblTitleYosoEigyo.WidthDef = 116
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
        Me.cmbEigyo.Location = New System.Drawing.Point(116, 19)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 238
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'cmbYosoEigyo
        '
        Me.cmbYosoEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbYosoEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbYosoEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbYosoEigyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbYosoEigyo.DataSource = Nothing
        Me.cmbYosoEigyo.DisplayMember = Nothing
        Me.cmbYosoEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbYosoEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbYosoEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbYosoEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbYosoEigyo.HeightDef = 18
        Me.cmbYosoEigyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbYosoEigyo.HissuLabelVisible = False
        Me.cmbYosoEigyo.InsertWildCard = True
        Me.cmbYosoEigyo.IsForbiddenWordsCheck = False
        Me.cmbYosoEigyo.IsHissuCheck = False
        Me.cmbYosoEigyo.ItemName = ""
        Me.cmbYosoEigyo.Location = New System.Drawing.Point(575, 19)
        Me.cmbYosoEigyo.Name = "cmbYosoEigyo"
        Me.cmbYosoEigyo.ReadOnly = True
        Me.cmbYosoEigyo.SelectedIndex = -1
        Me.cmbYosoEigyo.SelectedItem = Nothing
        Me.cmbYosoEigyo.SelectedText = ""
        Me.cmbYosoEigyo.SelectedValue = ""
        Me.cmbYosoEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbYosoEigyo.TabIndex = 239
        Me.cmbYosoEigyo.TabStop = False
        Me.cmbYosoEigyo.TabStopSetting = False
        Me.cmbYosoEigyo.TextValue = ""
        Me.cmbYosoEigyo.ValueMember = Nothing
        Me.cmbYosoEigyo.WidthDef = 300
        '
        'pnlCargo
        '
        Me.pnlCargo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCargo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCargo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCargo.Controls.Add(Me.btnAdd)
        Me.pnlCargo.Controls.Add(Me.btnDel)
        Me.pnlCargo.Controls.Add(Me.sprDetail)
        Me.pnlCargo.EnableStatus = False
        Me.pnlCargo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCargo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCargo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCargo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCargo.HeightDef = 246
        Me.pnlCargo.Location = New System.Drawing.Point(13, 474)
        Me.pnlCargo.Name = "pnlCargo"
        Me.pnlCargo.Size = New System.Drawing.Size(1253, 246)
        Me.pnlCargo.TabIndex = 34
        Me.pnlCargo.TabStop = False
        Me.pnlCargo.Text = "貨物詳細情報"
        Me.pnlCargo.TextValue = "貨物詳細情報"
        Me.pnlCargo.WidthDef = 1253
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnAdd.EnableStatus = True
        Me.btnAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.HeightDef = 22
        Me.btnAdd.Location = New System.Drawing.Point(22, 17)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.TabStopSetting = True
        Me.btnAdd.Text = "行追加"
        Me.btnAdd.TextValue = "行追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.WidthDef = 70
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
        Me.btnDel.Location = New System.Drawing.Point(103, 17)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 22)
        Me.btnDel.TabIndex = 16
        Me.btnDel.TabStopSetting = True
        Me.btnDel.Text = "行削除"
        Me.btnDel.TextValue = "行削除"
        Me.btnDel.UseVisualStyleBackColor = True
        Me.btnDel.WidthDef = 70
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
        Me.sprDetail.HeightDef = 193
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(6, 45)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1241, 193)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 116
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.WidthDef = 1241
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
        '
        'pnlCharge
        '
        Me.pnlCharge.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCharge.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCharge.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCharge.Controls.Add(Me.numInsurExtc)
        Me.pnlCharge.Controls.Add(Me.lblTitleInsurExtc)
        Me.pnlCharge.Controls.Add(Me.numPassExtc)
        Me.pnlCharge.Controls.Add(Me.lblTitlePassExtc)
        Me.pnlCharge.Controls.Add(Me.numRelyExtc)
        Me.pnlCharge.Controls.Add(Me.lblTitleRelyExtc)
        Me.pnlCharge.Controls.Add(Me.numWintExtc)
        Me.pnlCharge.Controls.Add(Me.lblTitleWintExtc)
        Me.pnlCharge.Controls.Add(Me.numCityExtc)
        Me.pnlCharge.Controls.Add(Me.lblTitleCityExtc)
        Me.pnlCharge.Controls.Add(Me.numPayUnchin)
        Me.pnlCharge.Controls.Add(Me.lblTitlePayUnchin)
        Me.pnlCharge.Controls.Add(Me.numSeiqUnchin)
        Me.pnlCharge.Controls.Add(Me.lblTitleSeiqUnchin)
        Me.pnlCharge.Controls.Add(Me.lblTitleKm)
        Me.pnlCharge.Controls.Add(Me.numSeiqTariffDes)
        Me.pnlCharge.Controls.Add(Me.LmTitleLabel40)
        Me.pnlCharge.Controls.Add(Me.lblTitleKg)
        Me.pnlCharge.Controls.Add(Me.numUnsoWt)
        Me.pnlCharge.Controls.Add(Me.lblTitleUnsoWt)
        Me.pnlCharge.EnableStatus = False
        Me.pnlCharge.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCharge.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCharge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCharge.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCharge.HeightDef = 71
        Me.pnlCharge.Location = New System.Drawing.Point(13, 726)
        Me.pnlCharge.Name = "pnlCharge"
        Me.pnlCharge.Size = New System.Drawing.Size(1253, 71)
        Me.pnlCharge.TabIndex = 35
        Me.pnlCharge.TabStop = False
        Me.pnlCharge.Text = "請求料金情報"
        Me.pnlCharge.TextValue = "請求料金情報"
        Me.pnlCharge.WidthDef = 1253
        '
        'numInsurExtc
        '
        Me.numInsurExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numInsurExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numInsurExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numInsurExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numInsurExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numInsurExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numInsurExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numInsurExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numInsurExtc.HeightDef = 18
        Me.numInsurExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numInsurExtc.HissuLabelVisible = False
        Me.numInsurExtc.IsHissuCheck = False
        Me.numInsurExtc.IsRangeCheck = False
        Me.numInsurExtc.ItemName = ""
        Me.numInsurExtc.Location = New System.Drawing.Point(1110, 40)
        Me.numInsurExtc.Name = "numInsurExtc"
        Me.numInsurExtc.ReadOnly = True
        Me.numInsurExtc.Size = New System.Drawing.Size(150, 18)
        Me.numInsurExtc.TabIndex = 254
        Me.numInsurExtc.TabStop = False
        Me.numInsurExtc.TabStopSetting = False
        Me.numInsurExtc.TextValue = "0"
        Me.numInsurExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numInsurExtc.WidthDef = 150
        '
        'lblTitleInsurExtc
        '
        Me.lblTitleInsurExtc.AutoSizeDef = False
        Me.lblTitleInsurExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInsurExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInsurExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInsurExtc.EnableStatus = False
        Me.lblTitleInsurExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInsurExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInsurExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInsurExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInsurExtc.HeightDef = 13
        Me.lblTitleInsurExtc.Location = New System.Drawing.Point(1022, 43)
        Me.lblTitleInsurExtc.Name = "lblTitleInsurExtc"
        Me.lblTitleInsurExtc.Size = New System.Drawing.Size(87, 13)
        Me.lblTitleInsurExtc.TabIndex = 253
        Me.lblTitleInsurExtc.Text = "保険料他"
        Me.lblTitleInsurExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInsurExtc.TextValue = "保険料他"
        Me.lblTitleInsurExtc.WidthDef = 87
        '
        'numPassExtc
        '
        Me.numPassExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPassExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPassExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPassExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPassExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPassExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPassExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPassExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPassExtc.HeightDef = 18
        Me.numPassExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPassExtc.HissuLabelVisible = False
        Me.numPassExtc.IsHissuCheck = False
        Me.numPassExtc.IsRangeCheck = False
        Me.numPassExtc.ItemName = ""
        Me.numPassExtc.Location = New System.Drawing.Point(881, 40)
        Me.numPassExtc.Name = "numPassExtc"
        Me.numPassExtc.ReadOnly = True
        Me.numPassExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPassExtc.TabIndex = 252
        Me.numPassExtc.TabStop = False
        Me.numPassExtc.TabStopSetting = False
        Me.numPassExtc.TextValue = "0"
        Me.numPassExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPassExtc.WidthDef = 150
        '
        'lblTitlePassExtc
        '
        Me.lblTitlePassExtc.AutoSizeDef = False
        Me.lblTitlePassExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePassExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePassExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePassExtc.EnableStatus = False
        Me.lblTitlePassExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePassExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePassExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePassExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePassExtc.HeightDef = 13
        Me.lblTitlePassExtc.Location = New System.Drawing.Point(781, 43)
        Me.lblTitlePassExtc.Name = "lblTitlePassExtc"
        Me.lblTitlePassExtc.Size = New System.Drawing.Size(100, 13)
        Me.lblTitlePassExtc.TabIndex = 251
        Me.lblTitlePassExtc.Text = "通行・航送料"
        Me.lblTitlePassExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePassExtc.TextValue = "通行・航送料"
        Me.lblTitlePassExtc.WidthDef = 100
        '
        'numRelyExtc
        '
        Me.numRelyExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRelyExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRelyExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numRelyExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numRelyExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRelyExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRelyExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRelyExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRelyExtc.HeightDef = 18
        Me.numRelyExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRelyExtc.HissuLabelVisible = False
        Me.numRelyExtc.IsHissuCheck = False
        Me.numRelyExtc.IsRangeCheck = False
        Me.numRelyExtc.ItemName = ""
        Me.numRelyExtc.Location = New System.Drawing.Point(637, 40)
        Me.numRelyExtc.Name = "numRelyExtc"
        Me.numRelyExtc.ReadOnly = True
        Me.numRelyExtc.Size = New System.Drawing.Size(150, 18)
        Me.numRelyExtc.TabIndex = 250
        Me.numRelyExtc.TabStop = False
        Me.numRelyExtc.TabStopSetting = False
        Me.numRelyExtc.TextValue = "0"
        Me.numRelyExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numRelyExtc.WidthDef = 150
        '
        'lblTitleRelyExtc
        '
        Me.lblTitleRelyExtc.AutoSizeDef = False
        Me.lblTitleRelyExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRelyExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRelyExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRelyExtc.EnableStatus = False
        Me.lblTitleRelyExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRelyExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRelyExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRelyExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRelyExtc.HeightDef = 13
        Me.lblTitleRelyExtc.Location = New System.Drawing.Point(541, 43)
        Me.lblTitleRelyExtc.Name = "lblTitleRelyExtc"
        Me.lblTitleRelyExtc.Size = New System.Drawing.Size(96, 13)
        Me.lblTitleRelyExtc.TabIndex = 249
        Me.lblTitleRelyExtc.Text = "中継料"
        Me.lblTitleRelyExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRelyExtc.TextValue = "中継料"
        Me.lblTitleRelyExtc.WidthDef = 96
        '
        'numWintExtc
        '
        Me.numWintExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWintExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWintExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numWintExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numWintExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numWintExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numWintExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numWintExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numWintExtc.HeightDef = 18
        Me.numWintExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWintExtc.HissuLabelVisible = False
        Me.numWintExtc.IsHissuCheck = False
        Me.numWintExtc.IsRangeCheck = False
        Me.numWintExtc.ItemName = ""
        Me.numWintExtc.Location = New System.Drawing.Point(399, 40)
        Me.numWintExtc.Name = "numWintExtc"
        Me.numWintExtc.ReadOnly = True
        Me.numWintExtc.Size = New System.Drawing.Size(150, 18)
        Me.numWintExtc.TabIndex = 246
        Me.numWintExtc.TabStop = False
        Me.numWintExtc.TabStopSetting = False
        Me.numWintExtc.TextValue = "0"
        Me.numWintExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numWintExtc.WidthDef = 150
        '
        'lblTitleWintExtc
        '
        Me.lblTitleWintExtc.AutoSizeDef = False
        Me.lblTitleWintExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWintExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWintExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWintExtc.EnableStatus = False
        Me.lblTitleWintExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWintExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWintExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWintExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWintExtc.HeightDef = 13
        Me.lblTitleWintExtc.Location = New System.Drawing.Point(272, 43)
        Me.lblTitleWintExtc.Name = "lblTitleWintExtc"
        Me.lblTitleWintExtc.Size = New System.Drawing.Size(127, 13)
        Me.lblTitleWintExtc.TabIndex = 245
        Me.lblTitleWintExtc.Text = "冬期割増"
        Me.lblTitleWintExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWintExtc.TextValue = "冬期割増"
        Me.lblTitleWintExtc.WidthDef = 127
        '
        'numCityExtc
        '
        Me.numCityExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCityExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCityExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCityExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCityExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCityExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCityExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCityExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCityExtc.HeightDef = 18
        Me.numCityExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCityExtc.HissuLabelVisible = False
        Me.numCityExtc.IsHissuCheck = False
        Me.numCityExtc.IsRangeCheck = False
        Me.numCityExtc.ItemName = ""
        Me.numCityExtc.Location = New System.Drawing.Point(116, 40)
        Me.numCityExtc.Name = "numCityExtc"
        Me.numCityExtc.ReadOnly = True
        Me.numCityExtc.Size = New System.Drawing.Size(150, 18)
        Me.numCityExtc.TabIndex = 244
        Me.numCityExtc.TabStop = False
        Me.numCityExtc.TabStopSetting = False
        Me.numCityExtc.TextValue = "0"
        Me.numCityExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCityExtc.WidthDef = 150
        '
        'lblTitleCityExtc
        '
        Me.lblTitleCityExtc.AutoSizeDef = False
        Me.lblTitleCityExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCityExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCityExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCityExtc.EnableStatus = False
        Me.lblTitleCityExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCityExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCityExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCityExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCityExtc.HeightDef = 13
        Me.lblTitleCityExtc.Location = New System.Drawing.Point(12, 43)
        Me.lblTitleCityExtc.Name = "lblTitleCityExtc"
        Me.lblTitleCityExtc.Size = New System.Drawing.Size(104, 13)
        Me.lblTitleCityExtc.TabIndex = 243
        Me.lblTitleCityExtc.Text = "都市割増"
        Me.lblTitleCityExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCityExtc.TextValue = "都市割増"
        Me.lblTitleCityExtc.WidthDef = 104
        '
        'numPayUnchin
        '
        Me.numPayUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayUnchin.HeightDef = 18
        Me.numPayUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnchin.HissuLabelVisible = False
        Me.numPayUnchin.IsHissuCheck = False
        Me.numPayUnchin.IsRangeCheck = False
        Me.numPayUnchin.ItemName = ""
        Me.numPayUnchin.Location = New System.Drawing.Point(881, 19)
        Me.numPayUnchin.Name = "numPayUnchin"
        Me.numPayUnchin.ReadOnly = True
        Me.numPayUnchin.Size = New System.Drawing.Size(150, 18)
        Me.numPayUnchin.TabIndex = 242
        Me.numPayUnchin.TabStop = False
        Me.numPayUnchin.TabStopSetting = False
        Me.numPayUnchin.TextValue = "0"
        Me.numPayUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayUnchin.WidthDef = 150
        '
        'lblTitlePayUnchin
        '
        Me.lblTitlePayUnchin.AutoSizeDef = False
        Me.lblTitlePayUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayUnchin.EnableStatus = False
        Me.lblTitlePayUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayUnchin.HeightDef = 13
        Me.lblTitlePayUnchin.Location = New System.Drawing.Point(778, 21)
        Me.lblTitlePayUnchin.Name = "lblTitlePayUnchin"
        Me.lblTitlePayUnchin.Size = New System.Drawing.Size(103, 13)
        Me.lblTitlePayUnchin.TabIndex = 241
        Me.lblTitlePayUnchin.Text = "支払運賃"
        Me.lblTitlePayUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayUnchin.TextValue = "支払運賃"
        Me.lblTitlePayUnchin.WidthDef = 103
        '
        'numSeiqUnchin
        '
        Me.numSeiqUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeiqUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeiqUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeiqUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeiqUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeiqUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeiqUnchin.HeightDef = 18
        Me.numSeiqUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqUnchin.HissuLabelVisible = False
        Me.numSeiqUnchin.IsHissuCheck = False
        Me.numSeiqUnchin.IsRangeCheck = False
        Me.numSeiqUnchin.ItemName = ""
        Me.numSeiqUnchin.Location = New System.Drawing.Point(638, 19)
        Me.numSeiqUnchin.Name = "numSeiqUnchin"
        Me.numSeiqUnchin.ReadOnly = True
        Me.numSeiqUnchin.Size = New System.Drawing.Size(150, 18)
        Me.numSeiqUnchin.TabIndex = 240
        Me.numSeiqUnchin.TabStop = False
        Me.numSeiqUnchin.TabStopSetting = False
        Me.numSeiqUnchin.TextValue = "0"
        Me.numSeiqUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeiqUnchin.WidthDef = 150
        '
        'lblTitleSeiqUnchin
        '
        Me.lblTitleSeiqUnchin.AutoSizeDef = False
        Me.lblTitleSeiqUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqUnchin.EnableStatus = False
        Me.lblTitleSeiqUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqUnchin.HeightDef = 13
        Me.lblTitleSeiqUnchin.Location = New System.Drawing.Point(555, 21)
        Me.lblTitleSeiqUnchin.Name = "lblTitleSeiqUnchin"
        Me.lblTitleSeiqUnchin.Size = New System.Drawing.Size(83, 13)
        Me.lblTitleSeiqUnchin.TabIndex = 239
        Me.lblTitleSeiqUnchin.Text = "請求運賃"
        Me.lblTitleSeiqUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqUnchin.TextValue = "請求運賃"
        Me.lblTitleSeiqUnchin.WidthDef = 83
        '
        'lblTitleKm
        '
        Me.lblTitleKm.AutoSize = True
        Me.lblTitleKm.AutoSizeDef = True
        Me.lblTitleKm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKm.EnableStatus = False
        Me.lblTitleKm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKm.HeightDef = 13
        Me.lblTitleKm.Location = New System.Drawing.Point(534, 21)
        Me.lblTitleKm.Name = "lblTitleKm"
        Me.lblTitleKm.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKm.TabIndex = 238
        Me.lblTitleKm.Text = "KM"
        Me.lblTitleKm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKm.TextValue = "KM"
        Me.lblTitleKm.WidthDef = 21
        '
        'numSeiqTariffDes
        '
        Me.numSeiqTariffDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqTariffDes.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqTariffDes.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeiqTariffDes.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeiqTariffDes.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeiqTariffDes.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeiqTariffDes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeiqTariffDes.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeiqTariffDes.HeightDef = 18
        Me.numSeiqTariffDes.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeiqTariffDes.HissuLabelVisible = False
        Me.numSeiqTariffDes.IsHissuCheck = False
        Me.numSeiqTariffDes.IsRangeCheck = False
        Me.numSeiqTariffDes.ItemName = ""
        Me.numSeiqTariffDes.Location = New System.Drawing.Point(399, 19)
        Me.numSeiqTariffDes.Name = "numSeiqTariffDes"
        Me.numSeiqTariffDes.ReadOnly = True
        Me.numSeiqTariffDes.Size = New System.Drawing.Size(150, 18)
        Me.numSeiqTariffDes.TabIndex = 237
        Me.numSeiqTariffDes.TabStop = False
        Me.numSeiqTariffDes.TabStopSetting = False
        Me.numSeiqTariffDes.TextValue = "0"
        Me.numSeiqTariffDes.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeiqTariffDes.WidthDef = 150
        '
        'LmTitleLabel40
        '
        Me.LmTitleLabel40.AutoSizeDef = False
        Me.LmTitleLabel40.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel40.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel40.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel40.EnableStatus = False
        Me.LmTitleLabel40.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel40.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel40.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel40.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel40.HeightDef = 13
        Me.LmTitleLabel40.Location = New System.Drawing.Point(294, 21)
        Me.LmTitleLabel40.Name = "LmTitleLabel40"
        Me.LmTitleLabel40.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel40.TabIndex = 236
        Me.LmTitleLabel40.Text = "請求タリフ距離"
        Me.LmTitleLabel40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel40.TextValue = "請求タリフ距離"
        Me.LmTitleLabel40.WidthDef = 105
        '
        'lblTitleKg
        '
        Me.lblTitleKg.AutoSize = True
        Me.lblTitleKg.AutoSizeDef = True
        Me.lblTitleKg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg.EnableStatus = False
        Me.lblTitleKg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.HeightDef = 13
        Me.lblTitleKg.Location = New System.Drawing.Point(251, 21)
        Me.lblTitleKg.Name = "lblTitleKg"
        Me.lblTitleKg.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg.TabIndex = 235
        Me.lblTitleKg.Text = "KG"
        Me.lblTitleKg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg.TextValue = "KG"
        Me.lblTitleKg.WidthDef = 21
        '
        'numUnsoWt
        '
        Me.numUnsoWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoWt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoWt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWt.HeightDef = 18
        Me.numUnsoWt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoWt.HissuLabelVisible = False
        Me.numUnsoWt.IsHissuCheck = False
        Me.numUnsoWt.IsRangeCheck = False
        Me.numUnsoWt.ItemName = ""
        Me.numUnsoWt.Location = New System.Drawing.Point(116, 19)
        Me.numUnsoWt.Name = "numUnsoWt"
        Me.numUnsoWt.ReadOnly = True
        Me.numUnsoWt.Size = New System.Drawing.Size(150, 18)
        Me.numUnsoWt.TabIndex = 145
        Me.numUnsoWt.TabStop = False
        Me.numUnsoWt.TabStopSetting = False
        Me.numUnsoWt.TextValue = "0"
        Me.numUnsoWt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnsoWt.WidthDef = 150
        '
        'lblTitleUnsoWt
        '
        Me.lblTitleUnsoWt.AutoSizeDef = False
        Me.lblTitleUnsoWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoWt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoWt.EnableStatus = False
        Me.lblTitleUnsoWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoWt.HeightDef = 13
        Me.lblTitleUnsoWt.Location = New System.Drawing.Point(15, 21)
        Me.lblTitleUnsoWt.Name = "lblTitleUnsoWt"
        Me.lblTitleUnsoWt.Size = New System.Drawing.Size(101, 13)
        Me.lblTitleUnsoWt.TabIndex = 142
        Me.lblTitleUnsoWt.Text = "運送重量"
        Me.lblTitleUnsoWt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoWt.TextValue = "運送重量"
        Me.lblTitleUnsoWt.WidthDef = 101
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
        Me.btnPrint.Location = New System.Drawing.Point(1042, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 111
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
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
        Me.lblSituation.TabIndex = 113
        Me.lblSituation.TabStop = False
        '
        'lblTitleBu
        '
        Me.lblTitleBu.AutoSize = True
        Me.lblTitleBu.AutoSizeDef = True
        Me.lblTitleBu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBu.EnableStatus = False
        Me.lblTitleBu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBu.HeightDef = 13
        Me.lblTitleBu.Location = New System.Drawing.Point(957, 12)
        Me.lblTitleBu.Name = "lblTitleBu"
        Me.lblTitleBu.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleBu.TabIndex = 262
        Me.lblTitleBu.Text = "部"
        Me.lblTitleBu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBu.TextValue = "部"
        Me.lblTitleBu.WidthDef = 21
        '
        'numPrtCnt
        '
        Me.numPrtCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrtCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPrtCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt.HeightDef = 18
        Me.numPrtCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPrtCnt.HissuLabelVisible = False
        Me.numPrtCnt.IsHissuCheck = False
        Me.numPrtCnt.IsRangeCheck = False
        Me.numPrtCnt.ItemName = ""
        Me.numPrtCnt.Location = New System.Drawing.Point(907, 10)
        Me.numPrtCnt.Name = "numPrtCnt"
        Me.numPrtCnt.ReadOnly = False
        Me.numPrtCnt.Size = New System.Drawing.Size(61, 18)
        Me.numPrtCnt.TabIndex = 263
        Me.numPrtCnt.TabStopSetting = True
        Me.numPrtCnt.TextValue = "0"
        Me.numPrtCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPrtCnt.WidthDef = 61
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "P003"
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
        Me.cmbPrint.Location = New System.Drawing.Point(752, 10)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(157, 18)
        Me.cmbPrint.TabIndex = 264
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 157
        '
        'pnlPay
        '
        Me.pnlPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlPay.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlPay.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlPay.Controls.Add(Me.numPayInsurExtc)
        Me.pnlPay.Controls.Add(Me.lblTitlePayInsurExtc)
        Me.pnlPay.Controls.Add(Me.numPayPassExtc)
        Me.pnlPay.Controls.Add(Me.lblTitlePayPassExtc)
        Me.pnlPay.Controls.Add(Me.numPayRelyExtc)
        Me.pnlPay.Controls.Add(Me.lblTitlePayRelyExtc)
        Me.pnlPay.Controls.Add(Me.numPayWintExtc)
        Me.pnlPay.Controls.Add(Me.lblTitlePayWintExtc)
        Me.pnlPay.Controls.Add(Me.numPayCityExtc)
        Me.pnlPay.Controls.Add(Me.lblTitlePayCityExtc)
        Me.pnlPay.Controls.Add(Me.numPayPayUnchin)
        Me.pnlPay.Controls.Add(Me.lblTitlePayPayUnchin)
        Me.pnlPay.Controls.Add(Me.lblTitlePayKm)
        Me.pnlPay.Controls.Add(Me.numPaySeiqTariffDes)
        Me.pnlPay.Controls.Add(Me.lblTitlePaySkyuKyori)
        Me.pnlPay.Controls.Add(Me.lblPayTitleKg)
        Me.pnlPay.Controls.Add(Me.numPayUnsoWt)
        Me.pnlPay.Controls.Add(Me.lblTitlePayUnsoWt)
        Me.pnlPay.EnableStatus = False
        Me.pnlPay.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlPay.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlPay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlPay.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlPay.HeightDef = 71
        Me.pnlPay.Location = New System.Drawing.Point(13, 803)
        Me.pnlPay.Name = "pnlPay"
        Me.pnlPay.Size = New System.Drawing.Size(1253, 71)
        Me.pnlPay.TabIndex = 255
        Me.pnlPay.TabStop = False
        Me.pnlPay.Text = "支払料金情報"
        Me.pnlPay.TextValue = "支払料金情報"
        Me.pnlPay.WidthDef = 1253
        '
        'numPayInsurExtc
        '
        Me.numPayInsurExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayInsurExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayInsurExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayInsurExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayInsurExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayInsurExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayInsurExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayInsurExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayInsurExtc.HeightDef = 18
        Me.numPayInsurExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayInsurExtc.HissuLabelVisible = False
        Me.numPayInsurExtc.IsHissuCheck = False
        Me.numPayInsurExtc.IsRangeCheck = False
        Me.numPayInsurExtc.ItemName = ""
        Me.numPayInsurExtc.Location = New System.Drawing.Point(1108, 38)
        Me.numPayInsurExtc.Name = "numPayInsurExtc"
        Me.numPayInsurExtc.ReadOnly = True
        Me.numPayInsurExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPayInsurExtc.TabIndex = 254
        Me.numPayInsurExtc.TabStop = False
        Me.numPayInsurExtc.TabStopSetting = False
        Me.numPayInsurExtc.TextValue = "0"
        Me.numPayInsurExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayInsurExtc.WidthDef = 150
        '
        'lblTitlePayInsurExtc
        '
        Me.lblTitlePayInsurExtc.AutoSizeDef = False
        Me.lblTitlePayInsurExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayInsurExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayInsurExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayInsurExtc.EnableStatus = False
        Me.lblTitlePayInsurExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayInsurExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayInsurExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayInsurExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayInsurExtc.HeightDef = 13
        Me.lblTitlePayInsurExtc.Location = New System.Drawing.Point(1025, 40)
        Me.lblTitlePayInsurExtc.Name = "lblTitlePayInsurExtc"
        Me.lblTitlePayInsurExtc.Size = New System.Drawing.Size(84, 13)
        Me.lblTitlePayInsurExtc.TabIndex = 253
        Me.lblTitlePayInsurExtc.Text = "保険料他"
        Me.lblTitlePayInsurExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayInsurExtc.TextValue = "保険料他"
        Me.lblTitlePayInsurExtc.WidthDef = 84
        '
        'numPayPassExtc
        '
        Me.numPayPassExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPassExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPassExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayPassExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayPassExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayPassExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayPassExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayPassExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayPassExtc.HeightDef = 18
        Me.numPayPassExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPassExtc.HissuLabelVisible = False
        Me.numPayPassExtc.IsHissuCheck = False
        Me.numPayPassExtc.IsRangeCheck = False
        Me.numPayPassExtc.ItemName = ""
        Me.numPayPassExtc.Location = New System.Drawing.Point(881, 40)
        Me.numPayPassExtc.Name = "numPayPassExtc"
        Me.numPayPassExtc.ReadOnly = True
        Me.numPayPassExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPayPassExtc.TabIndex = 252
        Me.numPayPassExtc.TabStop = False
        Me.numPayPassExtc.TabStopSetting = False
        Me.numPayPassExtc.TextValue = "0"
        Me.numPayPassExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayPassExtc.WidthDef = 150
        '
        'lblTitlePayPassExtc
        '
        Me.lblTitlePayPassExtc.AutoSizeDef = False
        Me.lblTitlePayPassExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayPassExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayPassExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayPassExtc.EnableStatus = False
        Me.lblTitlePayPassExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayPassExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayPassExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayPassExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayPassExtc.HeightDef = 13
        Me.lblTitlePayPassExtc.Location = New System.Drawing.Point(781, 43)
        Me.lblTitlePayPassExtc.Name = "lblTitlePayPassExtc"
        Me.lblTitlePayPassExtc.Size = New System.Drawing.Size(100, 13)
        Me.lblTitlePayPassExtc.TabIndex = 251
        Me.lblTitlePayPassExtc.Text = "通行・航送料"
        Me.lblTitlePayPassExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayPassExtc.TextValue = "通行・航送料"
        Me.lblTitlePayPassExtc.WidthDef = 100
        '
        'numPayRelyExtc
        '
        Me.numPayRelyExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayRelyExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayRelyExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayRelyExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayRelyExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayRelyExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayRelyExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayRelyExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayRelyExtc.HeightDef = 18
        Me.numPayRelyExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayRelyExtc.HissuLabelVisible = False
        Me.numPayRelyExtc.IsHissuCheck = False
        Me.numPayRelyExtc.IsRangeCheck = False
        Me.numPayRelyExtc.ItemName = ""
        Me.numPayRelyExtc.Location = New System.Drawing.Point(637, 40)
        Me.numPayRelyExtc.Name = "numPayRelyExtc"
        Me.numPayRelyExtc.ReadOnly = True
        Me.numPayRelyExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPayRelyExtc.TabIndex = 250
        Me.numPayRelyExtc.TabStop = False
        Me.numPayRelyExtc.TabStopSetting = False
        Me.numPayRelyExtc.TextValue = "0"
        Me.numPayRelyExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayRelyExtc.WidthDef = 150
        '
        'lblTitlePayRelyExtc
        '
        Me.lblTitlePayRelyExtc.AutoSizeDef = False
        Me.lblTitlePayRelyExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayRelyExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayRelyExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayRelyExtc.EnableStatus = False
        Me.lblTitlePayRelyExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayRelyExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayRelyExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayRelyExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayRelyExtc.HeightDef = 13
        Me.lblTitlePayRelyExtc.Location = New System.Drawing.Point(555, 43)
        Me.lblTitlePayRelyExtc.Name = "lblTitlePayRelyExtc"
        Me.lblTitlePayRelyExtc.Size = New System.Drawing.Size(82, 13)
        Me.lblTitlePayRelyExtc.TabIndex = 249
        Me.lblTitlePayRelyExtc.Text = "中継料"
        Me.lblTitlePayRelyExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayRelyExtc.TextValue = "中継料"
        Me.lblTitlePayRelyExtc.WidthDef = 82
        '
        'numPayWintExtc
        '
        Me.numPayWintExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayWintExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayWintExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayWintExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayWintExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayWintExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayWintExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayWintExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayWintExtc.HeightDef = 18
        Me.numPayWintExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayWintExtc.HissuLabelVisible = False
        Me.numPayWintExtc.IsHissuCheck = False
        Me.numPayWintExtc.IsRangeCheck = False
        Me.numPayWintExtc.ItemName = ""
        Me.numPayWintExtc.Location = New System.Drawing.Point(399, 40)
        Me.numPayWintExtc.Name = "numPayWintExtc"
        Me.numPayWintExtc.ReadOnly = True
        Me.numPayWintExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPayWintExtc.TabIndex = 246
        Me.numPayWintExtc.TabStop = False
        Me.numPayWintExtc.TabStopSetting = False
        Me.numPayWintExtc.TextValue = "0"
        Me.numPayWintExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayWintExtc.WidthDef = 150
        '
        'lblTitlePayWintExtc
        '
        Me.lblTitlePayWintExtc.AutoSizeDef = False
        Me.lblTitlePayWintExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayWintExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayWintExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayWintExtc.EnableStatus = False
        Me.lblTitlePayWintExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayWintExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayWintExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayWintExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayWintExtc.HeightDef = 13
        Me.lblTitlePayWintExtc.Location = New System.Drawing.Point(290, 43)
        Me.lblTitlePayWintExtc.Name = "lblTitlePayWintExtc"
        Me.lblTitlePayWintExtc.Size = New System.Drawing.Size(109, 13)
        Me.lblTitlePayWintExtc.TabIndex = 245
        Me.lblTitlePayWintExtc.Text = "冬期割増"
        Me.lblTitlePayWintExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayWintExtc.TextValue = "冬期割増"
        Me.lblTitlePayWintExtc.WidthDef = 109
        '
        'numPayCityExtc
        '
        Me.numPayCityExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayCityExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayCityExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayCityExtc.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayCityExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayCityExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayCityExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayCityExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayCityExtc.HeightDef = 18
        Me.numPayCityExtc.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayCityExtc.HissuLabelVisible = False
        Me.numPayCityExtc.IsHissuCheck = False
        Me.numPayCityExtc.IsRangeCheck = False
        Me.numPayCityExtc.ItemName = ""
        Me.numPayCityExtc.Location = New System.Drawing.Point(116, 40)
        Me.numPayCityExtc.Name = "numPayCityExtc"
        Me.numPayCityExtc.ReadOnly = True
        Me.numPayCityExtc.Size = New System.Drawing.Size(150, 18)
        Me.numPayCityExtc.TabIndex = 244
        Me.numPayCityExtc.TabStop = False
        Me.numPayCityExtc.TabStopSetting = False
        Me.numPayCityExtc.TextValue = "0"
        Me.numPayCityExtc.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayCityExtc.WidthDef = 150
        '
        'lblTitlePayCityExtc
        '
        Me.lblTitlePayCityExtc.AutoSizeDef = False
        Me.lblTitlePayCityExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayCityExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayCityExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayCityExtc.EnableStatus = False
        Me.lblTitlePayCityExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayCityExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayCityExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayCityExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayCityExtc.HeightDef = 13
        Me.lblTitlePayCityExtc.Location = New System.Drawing.Point(15, 43)
        Me.lblTitlePayCityExtc.Name = "lblTitlePayCityExtc"
        Me.lblTitlePayCityExtc.Size = New System.Drawing.Size(101, 13)
        Me.lblTitlePayCityExtc.TabIndex = 243
        Me.lblTitlePayCityExtc.Text = "都市割増"
        Me.lblTitlePayCityExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayCityExtc.TextValue = "都市割増"
        Me.lblTitlePayCityExtc.WidthDef = 101
        '
        'numPayPayUnchin
        '
        Me.numPayPayUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPayUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPayUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayPayUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayPayUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayPayUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayPayUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayPayUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayPayUnchin.HeightDef = 18
        Me.numPayPayUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayPayUnchin.HissuLabelVisible = False
        Me.numPayPayUnchin.IsHissuCheck = False
        Me.numPayPayUnchin.IsRangeCheck = False
        Me.numPayPayUnchin.ItemName = ""
        Me.numPayPayUnchin.Location = New System.Drawing.Point(638, 19)
        Me.numPayPayUnchin.Name = "numPayPayUnchin"
        Me.numPayPayUnchin.ReadOnly = True
        Me.numPayPayUnchin.Size = New System.Drawing.Size(150, 18)
        Me.numPayPayUnchin.TabIndex = 242
        Me.numPayPayUnchin.TabStop = False
        Me.numPayPayUnchin.TabStopSetting = False
        Me.numPayPayUnchin.TextValue = "0"
        Me.numPayPayUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayPayUnchin.WidthDef = 150
        '
        'lblTitlePayPayUnchin
        '
        Me.lblTitlePayPayUnchin.AutoSizeDef = False
        Me.lblTitlePayPayUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayPayUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayPayUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayPayUnchin.EnableStatus = False
        Me.lblTitlePayPayUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayPayUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayPayUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayPayUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayPayUnchin.HeightDef = 13
        Me.lblTitlePayPayUnchin.Location = New System.Drawing.Point(555, 21)
        Me.lblTitlePayPayUnchin.Name = "lblTitlePayPayUnchin"
        Me.lblTitlePayPayUnchin.Size = New System.Drawing.Size(83, 13)
        Me.lblTitlePayPayUnchin.TabIndex = 241
        Me.lblTitlePayPayUnchin.Text = "支払運賃"
        Me.lblTitlePayPayUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayPayUnchin.TextValue = "支払運賃"
        Me.lblTitlePayPayUnchin.WidthDef = 83
        '
        'lblTitlePayKm
        '
        Me.lblTitlePayKm.AutoSize = True
        Me.lblTitlePayKm.AutoSizeDef = True
        Me.lblTitlePayKm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayKm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayKm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayKm.EnableStatus = False
        Me.lblTitlePayKm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayKm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayKm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayKm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayKm.HeightDef = 13
        Me.lblTitlePayKm.Location = New System.Drawing.Point(534, 21)
        Me.lblTitlePayKm.Name = "lblTitlePayKm"
        Me.lblTitlePayKm.Size = New System.Drawing.Size(21, 13)
        Me.lblTitlePayKm.TabIndex = 238
        Me.lblTitlePayKm.Text = "KM"
        Me.lblTitlePayKm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayKm.TextValue = "KM"
        Me.lblTitlePayKm.WidthDef = 21
        '
        'numPaySeiqTariffDes
        '
        Me.numPaySeiqTariffDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPaySeiqTariffDes.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPaySeiqTariffDes.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPaySeiqTariffDes.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPaySeiqTariffDes.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPaySeiqTariffDes.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPaySeiqTariffDes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPaySeiqTariffDes.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPaySeiqTariffDes.HeightDef = 18
        Me.numPaySeiqTariffDes.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPaySeiqTariffDes.HissuLabelVisible = False
        Me.numPaySeiqTariffDes.IsHissuCheck = False
        Me.numPaySeiqTariffDes.IsRangeCheck = False
        Me.numPaySeiqTariffDes.ItemName = ""
        Me.numPaySeiqTariffDes.Location = New System.Drawing.Point(399, 19)
        Me.numPaySeiqTariffDes.Name = "numPaySeiqTariffDes"
        Me.numPaySeiqTariffDes.ReadOnly = True
        Me.numPaySeiqTariffDes.Size = New System.Drawing.Size(150, 18)
        Me.numPaySeiqTariffDes.TabIndex = 237
        Me.numPaySeiqTariffDes.TabStop = False
        Me.numPaySeiqTariffDes.TabStopSetting = False
        Me.numPaySeiqTariffDes.TextValue = "0"
        Me.numPaySeiqTariffDes.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPaySeiqTariffDes.WidthDef = 150
        '
        'lblTitlePaySkyuKyori
        '
        Me.lblTitlePaySkyuKyori.AutoSizeDef = False
        Me.lblTitlePaySkyuKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePaySkyuKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePaySkyuKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePaySkyuKyori.EnableStatus = False
        Me.lblTitlePaySkyuKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePaySkyuKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePaySkyuKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePaySkyuKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePaySkyuKyori.HeightDef = 13
        Me.lblTitlePaySkyuKyori.Location = New System.Drawing.Point(275, 21)
        Me.lblTitlePaySkyuKyori.Name = "lblTitlePaySkyuKyori"
        Me.lblTitlePaySkyuKyori.Size = New System.Drawing.Size(124, 13)
        Me.lblTitlePaySkyuKyori.TabIndex = 236
        Me.lblTitlePaySkyuKyori.Text = "支払タリフ距離"
        Me.lblTitlePaySkyuKyori.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePaySkyuKyori.TextValue = "支払タリフ距離"
        Me.lblTitlePaySkyuKyori.WidthDef = 124
        '
        'lblPayTitleKg
        '
        Me.lblPayTitleKg.AutoSize = True
        Me.lblPayTitleKg.AutoSizeDef = True
        Me.lblPayTitleKg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayTitleKg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPayTitleKg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblPayTitleKg.EnableStatus = False
        Me.lblPayTitleKg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayTitleKg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPayTitleKg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayTitleKg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPayTitleKg.HeightDef = 13
        Me.lblPayTitleKg.Location = New System.Drawing.Point(251, 21)
        Me.lblPayTitleKg.Name = "lblPayTitleKg"
        Me.lblPayTitleKg.Size = New System.Drawing.Size(21, 13)
        Me.lblPayTitleKg.TabIndex = 235
        Me.lblPayTitleKg.Text = "KG"
        Me.lblPayTitleKg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPayTitleKg.TextValue = "KG"
        Me.lblPayTitleKg.WidthDef = 21
        '
        'numPayUnsoWt
        '
        Me.numPayUnsoWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnsoWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnsoWt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayUnsoWt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayUnsoWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayUnsoWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayUnsoWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayUnsoWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayUnsoWt.HeightDef = 18
        Me.numPayUnsoWt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayUnsoWt.HissuLabelVisible = False
        Me.numPayUnsoWt.IsHissuCheck = False
        Me.numPayUnsoWt.IsRangeCheck = False
        Me.numPayUnsoWt.ItemName = ""
        Me.numPayUnsoWt.Location = New System.Drawing.Point(116, 19)
        Me.numPayUnsoWt.Name = "numPayUnsoWt"
        Me.numPayUnsoWt.ReadOnly = True
        Me.numPayUnsoWt.Size = New System.Drawing.Size(150, 18)
        Me.numPayUnsoWt.TabIndex = 145
        Me.numPayUnsoWt.TabStop = False
        Me.numPayUnsoWt.TabStopSetting = False
        Me.numPayUnsoWt.TextValue = "0"
        Me.numPayUnsoWt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayUnsoWt.WidthDef = 150
        '
        'lblTitlePayUnsoWt
        '
        Me.lblTitlePayUnsoWt.AutoSizeDef = False
        Me.lblTitlePayUnsoWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayUnsoWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayUnsoWt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayUnsoWt.EnableStatus = False
        Me.lblTitlePayUnsoWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayUnsoWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayUnsoWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayUnsoWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayUnsoWt.HeightDef = 13
        Me.lblTitlePayUnsoWt.Location = New System.Drawing.Point(9, 21)
        Me.lblTitlePayUnsoWt.Name = "lblTitlePayUnsoWt"
        Me.lblTitlePayUnsoWt.Size = New System.Drawing.Size(107, 13)
        Me.lblTitlePayUnsoWt.TabIndex = 142
        Me.lblTitlePayUnsoWt.Text = "運送重量"
        Me.lblTitlePayUnsoWt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayUnsoWt.TextValue = "運送重量"
        Me.lblTitlePayUnsoWt.WidthDef = 107
        '
        'numPrtCnt_To
        '
        Me.numPrtCnt_To.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt_To.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt_To.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrtCnt_To.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPrtCnt_To.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt_To.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt_To.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt_To.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt_To.HeightDef = 18
        Me.numPrtCnt_To.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPrtCnt_To.HissuLabelVisible = False
        Me.numPrtCnt_To.IsHissuCheck = False
        Me.numPrtCnt_To.IsRangeCheck = False
        Me.numPrtCnt_To.ItemName = ""
        Me.numPrtCnt_To.Location = New System.Drawing.Point(982, 10)
        Me.numPrtCnt_To.Name = "numPrtCnt_To"
        Me.numPrtCnt_To.ReadOnly = False
        Me.numPrtCnt_To.Size = New System.Drawing.Size(61, 18)
        Me.numPrtCnt_To.TabIndex = 265
        Me.numPrtCnt_To.TabStopSetting = True
        Me.numPrtCnt_To.TextValue = "0"
        Me.numPrtCnt_To.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPrtCnt_To.WidthDef = 61
        '
        'numPrtCnt_From
        '
        Me.numPrtCnt_From.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt_From.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrtCnt_From.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrtCnt_From.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPrtCnt_From.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt_From.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrtCnt_From.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt_From.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrtCnt_From.HeightDef = 18
        Me.numPrtCnt_From.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPrtCnt_From.HissuLabelVisible = False
        Me.numPrtCnt_From.IsHissuCheck = False
        Me.numPrtCnt_From.IsRangeCheck = False
        Me.numPrtCnt_From.ItemName = ""
        Me.numPrtCnt_From.Location = New System.Drawing.Point(907, 10)
        Me.numPrtCnt_From.Name = "numPrtCnt_From"
        Me.numPrtCnt_From.ReadOnly = False
        Me.numPrtCnt_From.Size = New System.Drawing.Size(61, 18)
        Me.numPrtCnt_From.TabIndex = 263
        Me.numPrtCnt_From.TabStopSetting = True
        Me.numPrtCnt_From.TextValue = "0"
        Me.numPrtCnt_From.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPrtCnt_From.WidthDef = 61
        '
        'lblTitlePrtCntFromTo
        '
        Me.lblTitlePrtCntFromTo.AutoSize = True
        Me.lblTitlePrtCntFromTo.AutoSizeDef = True
        Me.lblTitlePrtCntFromTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrtCntFromTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrtCntFromTo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePrtCntFromTo.EnableStatus = False
        Me.lblTitlePrtCntFromTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrtCntFromTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrtCntFromTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrtCntFromTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrtCntFromTo.HeightDef = 13
        Me.lblTitlePrtCntFromTo.Location = New System.Drawing.Point(957, 12)
        Me.lblTitlePrtCntFromTo.Name = "lblTitlePrtCntFromTo"
        Me.lblTitlePrtCntFromTo.Size = New System.Drawing.Size(21, 13)
        Me.lblTitlePrtCntFromTo.TabIndex = 262
        Me.lblTitlePrtCntFromTo.Text = "～"
        Me.lblTitlePrtCntFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrtCntFromTo.TextValue = "～"
        Me.lblTitlePrtCntFromTo.WidthDef = 21
        '
        'LMF020F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMF020F"
        Me.RightToLeftLayout = True
        Me.Text = "【LMF020】 運送情報編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlUnso.ResumeLayout(False)
        Me.pnlUnso.PerformLayout()
        Me.pnlDestOrigin.ResumeLayout(False)
        Me.pnlDestOrigin.PerformLayout()
        Me.pnlCargo.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCharge.ResumeLayout(False)
        Me.pnlCharge.PerformLayout()
        Me.pnlPay.ResumeLayout(False)
        Me.pnlPay.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlUnso As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleYosoEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKanriNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnkoNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnkoNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnsoNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnsoNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKanriNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleMotoData As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbMotoDataKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbUnsoJiyuKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleUnsoJiyu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSharyoKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSharyoKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTariffKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTehaiKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbBinKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleBinKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPcKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitlePcKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTehaiKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleUnsoMotoKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsocoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnsoco As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsocoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsocoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleShip As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtExtcTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblShipNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtShipCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleExtcTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleBuyerOrdNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOkuriNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitletxtOrdNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleOkuriNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblExtcTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtBuyerOrdNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtOrdNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlDestOrigin As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleOrigDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdOrigDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleOrigTime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrigCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblOrigNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblOrigJisCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrigJisCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdDestDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleDestDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleJiDestTime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleDestTime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestJisCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDestJisCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZipNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleAdd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAreaCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestAdd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestAdd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleArea As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblAreaNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnsoPkgCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnsoCntUt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoWtL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleUnsoWtL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoPkgCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents cmbThermoKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleThermoKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbUnsoCntUt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleUnsoComment As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsoComment As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlCargo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlCharge As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents numSeiqTariffDes As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel40 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoWt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleUnsoWt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitlePayUnchin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSeiqUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSeiqUnchin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numCityExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleCityExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleRelyExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numWintExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleWintExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPassExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePassExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numRelyExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleInsurExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numInsurExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents btnDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents btnAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbYosoEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleOrig As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleDest As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents txtDestAdd3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnKeisan As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleBu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPrtCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtDestTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtOrigTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtJiDestTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCalcKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTax As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTax As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTakSize As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSplitFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTareYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsoBrNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlPay As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents numPayInsurExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayInsurExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayPassExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayPassExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayRelyExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayRelyExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayWintExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayWintExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayCityExtc As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayCityExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayPayUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayPayUnchin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitlePayKm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPaySeiqTariffDes As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePaySkyuKyori As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblPayTitleKg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayUnsoWt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayUnsoWt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblPayExtcTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitlePayExtcTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblPayTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtPayExtcTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtPayTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitlePayTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsocoBrCdOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsocoCdOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblAutoDenpNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbAutoDenpKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents titleAutoDenpNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents beforeAutoDenpKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtTel As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTel As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnsoRemark As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numPrtCnt_To As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePrtCntFromTo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPrtCnt_From As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber

End Class
