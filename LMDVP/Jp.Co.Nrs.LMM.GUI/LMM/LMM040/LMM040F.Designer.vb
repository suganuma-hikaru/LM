<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM040F
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
        Dim sprDetail2_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail2_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.lblAd3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblFax = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.lblEDICd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtEDICd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel33 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numKyori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblUnchinSeiqtoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnchinSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnchinSeiqto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtJIS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblKyori = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblJIS = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblYokoTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtYokoTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblYokoTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblExtcTariffRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtExtcTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblExtcTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnchinTariffRem2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnchinTariffCd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnchinTariff2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnchinTariffRem1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnchinTariffCd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnchinTariff1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUriageNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUriageCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUriage = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSales = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSalesNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSalesCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCargoTimeLimit = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCargoTimeLimit = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbMotoChakuKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblMotoChakuKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbBin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblBin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblPick = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbPick = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.grpUnso = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblSpUnsoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSpUnsoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSpUnso = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSpUnsoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCoaYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtFax = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTel = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtAd3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtAd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblAd2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtAd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblAd1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDeliAtt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtZip = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblDeliAtt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDicDestCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSpNhsKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZip = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblEigyosyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTariffBunruiKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbLargeCarYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblLargeCarYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.sprDetail2 = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbCoaYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbSpNhsKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblMaxEda = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdDateT = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdTimeT = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSetMstCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSetKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblModeFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbTariffBunruiKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblRemark = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKanaNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblKanaNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtShiharaiAd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblShiharaiAd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail2_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUnso.SuspendLayout()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.lblShiharaiAd)
        Me.pnlViewAria.Controls.Add(Me.txtCustDestCd)
        Me.pnlViewAria.Controls.Add(Me.txtDestNm)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdDateT)
        Me.pnlViewAria.Controls.Add(Me.txtShiharaiAd)
        Me.pnlViewAria.Controls.Add(Me.txtKanaNm)
        Me.pnlViewAria.Controls.Add(Me.lblKanaNm)
        Me.pnlViewAria.Controls.Add(Me.txtRemark)
        Me.pnlViewAria.Controls.Add(Me.lblRemark)
        Me.pnlViewAria.Controls.Add(Me.cmbTariffBunruiKbn)
        Me.pnlViewAria.Controls.Add(Me.lblModeFlg)
        Me.pnlViewAria.Controls.Add(Me.lblSetKbn)
        Me.pnlViewAria.Controls.Add(Me.lblSetMstCd)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdM)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdTimeT)
        Me.pnlViewAria.Controls.Add(Me.lblMaxEda)
        Me.pnlViewAria.Controls.Add(Me.cmbSpNhsKb)
        Me.pnlViewAria.Controls.Add(Me.cmbCoaYn)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.sprDetail2)
        Me.pnlViewAria.Controls.Add(Me.cmbLargeCarYn)
        Me.pnlViewAria.Controls.Add(Me.lblLargeCarYn)
        Me.pnlViewAria.Controls.Add(Me.lblTariffBunruiKbn)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.lblEigyosyo)
        Me.pnlViewAria.Controls.Add(Me.lblEDICd)
        Me.pnlViewAria.Controls.Add(Me.txtEDICd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel33)
        Me.pnlViewAria.Controls.Add(Me.numKyori)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinSeiqtoNm)
        Me.pnlViewAria.Controls.Add(Me.txtUnchinSeiqtoCd)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinSeiqto)
        Me.pnlViewAria.Controls.Add(Me.txtJIS)
        Me.pnlViewAria.Controls.Add(Me.lblKyori)
        Me.pnlViewAria.Controls.Add(Me.lblJIS)
        Me.pnlViewAria.Controls.Add(Me.lblYokoTariffRem)
        Me.pnlViewAria.Controls.Add(Me.txtYokoTariffCd)
        Me.pnlViewAria.Controls.Add(Me.lblYokoTariff)
        Me.pnlViewAria.Controls.Add(Me.lblExtcTariffRem)
        Me.pnlViewAria.Controls.Add(Me.txtExtcTariffCd)
        Me.pnlViewAria.Controls.Add(Me.lblExtcTariff)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinTariffRem2)
        Me.pnlViewAria.Controls.Add(Me.txtUnchinTariffCd2)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinTariff2)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinTariffRem1)
        Me.pnlViewAria.Controls.Add(Me.txtUnchinTariffCd1)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinTariff1)
        Me.pnlViewAria.Controls.Add(Me.lblUriageNm)
        Me.pnlViewAria.Controls.Add(Me.txtUriageCd)
        Me.pnlViewAria.Controls.Add(Me.lblUriage)
        Me.pnlViewAria.Controls.Add(Me.lblSales)
        Me.pnlViewAria.Controls.Add(Me.lblSalesNm)
        Me.pnlViewAria.Controls.Add(Me.txtSalesCd)
        Me.pnlViewAria.Controls.Add(Me.txtCargoTimeLimit)
        Me.pnlViewAria.Controls.Add(Me.lblCargoTimeLimit)
        Me.pnlViewAria.Controls.Add(Me.cmbMotoChakuKbn)
        Me.pnlViewAria.Controls.Add(Me.lblMotoChakuKbn)
        Me.pnlViewAria.Controls.Add(Me.cmbBin)
        Me.pnlViewAria.Controls.Add(Me.lblBin)
        Me.pnlViewAria.Controls.Add(Me.lblPick)
        Me.pnlViewAria.Controls.Add(Me.cmbPick)
        Me.pnlViewAria.Controls.Add(Me.grpUnso)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.lblCoaYn)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.txtFax)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.lblFax)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.txtTel)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.txtAd3)
        Me.pnlViewAria.Controls.Add(Me.lblAd3)
        Me.pnlViewAria.Controls.Add(Me.txtAd2)
        Me.pnlViewAria.Controls.Add(Me.lblAd2)
        Me.pnlViewAria.Controls.Add(Me.txtAd1)
        Me.pnlViewAria.Controls.Add(Me.lblAd1)
        Me.pnlViewAria.Controls.Add(Me.lblDestCd)
        Me.pnlViewAria.Controls.Add(Me.txtDestCd)
        Me.pnlViewAria.Controls.Add(Me.txtDeliAtt)
        Me.pnlViewAria.Controls.Add(Me.txtZip)
        Me.pnlViewAria.Controls.Add(Me.lblDeliAtt)
        Me.pnlViewAria.Controls.Add(Me.lblDicDestCd)
        Me.pnlViewAria.Controls.Add(Me.lblCustL)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmL)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
        Me.pnlViewAria.Controls.Add(Me.lblSpNhsKb)
        Me.pnlViewAria.Controls.Add(Me.lblTel)
        Me.pnlViewAria.Controls.Add(Me.lblZip)
        Me.pnlViewAria.Controls.Add(Me.lblDestNm)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'lblAd3
        '
        Me.lblAd3.AutoSize = True
        Me.lblAd3.AutoSizeDef = True
        Me.lblAd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblAd3.EnableStatus = False
        Me.lblAd3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd3.HeightDef = 13
        Me.lblAd3.Location = New System.Drawing.Point(336, 405)
        Me.lblAd3.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblAd3.Name = "lblAd3"
        Me.lblAd3.Size = New System.Drawing.Size(78, 13)
        Me.lblAd3.TabIndex = 410
        Me.lblAd3.Text = "住所3"
        Me.lblAd3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAd3.TextValue = "住所3"
        Me.lblAd3.WidthDef = 78
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.AutoSizeDef = True
        Me.lblFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFax.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblFax.EnableStatus = False
        Me.lblFax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFax.HeightDef = 13
        Me.lblFax.Location = New System.Drawing.Point(10, 467)
        Me.lblFax.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(167, 13)
        Me.lblFax.TabIndex = 416
        Me.lblFax.Text = "FAX番号"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFax.TextValue = "FAX番号"
        Me.lblFax.WidthDef = 167
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
        Me.sprDetail.HeightDef = 248
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 14)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1245, 248)
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
        'lblEDICd
        '
        Me.lblEDICd.AutoSize = True
        Me.lblEDICd.AutoSizeDef = True
        Me.lblEDICd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEDICd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEDICd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblEDICd.EnableStatus = False
        Me.lblEDICd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEDICd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEDICd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEDICd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEDICd.HeightDef = 13
        Me.lblEDICd.Location = New System.Drawing.Point(317, 325)
        Me.lblEDICd.MinimumSize = New System.Drawing.Size(148, 0)
        Me.lblEDICd.Name = "lblEDICd"
        Me.lblEDICd.Size = New System.Drawing.Size(148, 13)
        Me.lblEDICd.TabIndex = 457
        Me.lblEDICd.Text = "EDI届先コード"
        Me.lblEDICd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEDICd.TextValue = "EDI届先コード"
        Me.lblEDICd.WidthDef = 148
        '
        'txtEDICd
        '
        Me.txtEDICd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEDICd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEDICd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEDICd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEDICd.CountWrappedLine = False
        Me.txtEDICd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEDICd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEDICd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEDICd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEDICd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEDICd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEDICd.HeightDef = 18
        Me.txtEDICd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEDICd.HissuLabelVisible = False
        Me.txtEDICd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtEDICd.IsByteCheck = 20
        Me.txtEDICd.IsCalendarCheck = False
        Me.txtEDICd.IsDakutenCheck = False
        Me.txtEDICd.IsEisuCheck = False
        Me.txtEDICd.IsForbiddenWordsCheck = False
        Me.txtEDICd.IsFullByteCheck = 0
        Me.txtEDICd.IsHankakuCheck = False
        Me.txtEDICd.IsHissuCheck = False
        Me.txtEDICd.IsKanaCheck = False
        Me.txtEDICd.IsMiddleSpace = False
        Me.txtEDICd.IsNumericCheck = False
        Me.txtEDICd.IsSujiCheck = False
        Me.txtEDICd.IsZenkakuCheck = False
        Me.txtEDICd.ItemName = ""
        Me.txtEDICd.LineSpace = 0
        Me.txtEDICd.Location = New System.Drawing.Point(469, 323)
        Me.txtEDICd.MaxLength = 20
        Me.txtEDICd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEDICd.MaxLineCount = 0
        Me.txtEDICd.Multiline = False
        Me.txtEDICd.Name = "txtEDICd"
        Me.txtEDICd.ReadOnly = False
        Me.txtEDICd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEDICd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEDICd.Size = New System.Drawing.Size(267, 18)
        Me.txtEDICd.TabIndex = 458
        Me.txtEDICd.TabStopSetting = True
        Me.txtEDICd.TextValue = ""
        Me.txtEDICd.UseSystemPasswordChar = False
        Me.txtEDICd.WidthDef = 267
        Me.txtEDICd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel33
        '
        Me.LmTitleLabel33.AutoSizeDef = False
        Me.LmTitleLabel33.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel33.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel33.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel33.EnableStatus = False
        Me.LmTitleLabel33.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel33.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel33.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel33.HeightDef = 18
        Me.LmTitleLabel33.Location = New System.Drawing.Point(545, 463)
        Me.LmTitleLabel33.Name = "LmTitleLabel33"
        Me.LmTitleLabel33.Size = New System.Drawing.Size(22, 18)
        Me.LmTitleLabel33.TabIndex = 456
        Me.LmTitleLabel33.Text = "km"
        Me.LmTitleLabel33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel33.TextValue = "km"
        Me.LmTitleLabel33.WidthDef = 22
        '
        'numKyori
        '
        Me.numKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKyori.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKyori.HeightDef = 18
        Me.numKyori.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKyori.HissuLabelVisible = False
        Me.numKyori.IsHissuCheck = False
        Me.numKyori.IsRangeCheck = False
        Me.numKyori.ItemName = ""
        Me.numKyori.Location = New System.Drawing.Point(417, 463)
        Me.numKyori.Name = "numKyori"
        Me.numKyori.ReadOnly = False
        Me.numKyori.Size = New System.Drawing.Size(139, 18)
        Me.numKyori.TabIndex = 455
        Me.numKyori.TabStopSetting = True
        Me.numKyori.TextValue = "0"
        Me.numKyori.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKyori.WidthDef = 139
        '
        'lblUnchinSeiqtoNm
        '
        Me.lblUnchinSeiqtoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinSeiqtoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinSeiqtoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnchinSeiqtoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnchinSeiqtoNm.CountWrappedLine = False
        Me.lblUnchinSeiqtoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnchinSeiqtoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinSeiqtoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinSeiqtoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinSeiqtoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinSeiqtoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnchinSeiqtoNm.HeightDef = 18
        Me.lblUnchinSeiqtoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinSeiqtoNm.HissuLabelVisible = False
        Me.lblUnchinSeiqtoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnchinSeiqtoNm.IsByteCheck = 0
        Me.lblUnchinSeiqtoNm.IsCalendarCheck = False
        Me.lblUnchinSeiqtoNm.IsDakutenCheck = False
        Me.lblUnchinSeiqtoNm.IsEisuCheck = False
        Me.lblUnchinSeiqtoNm.IsForbiddenWordsCheck = False
        Me.lblUnchinSeiqtoNm.IsFullByteCheck = 0
        Me.lblUnchinSeiqtoNm.IsHankakuCheck = False
        Me.lblUnchinSeiqtoNm.IsHissuCheck = False
        Me.lblUnchinSeiqtoNm.IsKanaCheck = False
        Me.lblUnchinSeiqtoNm.IsMiddleSpace = False
        Me.lblUnchinSeiqtoNm.IsNumericCheck = False
        Me.lblUnchinSeiqtoNm.IsSujiCheck = False
        Me.lblUnchinSeiqtoNm.IsZenkakuCheck = False
        Me.lblUnchinSeiqtoNm.ItemName = ""
        Me.lblUnchinSeiqtoNm.LineSpace = 0
        Me.lblUnchinSeiqtoNm.Location = New System.Drawing.Point(299, 637)
        Me.lblUnchinSeiqtoNm.MaxLength = 0
        Me.lblUnchinSeiqtoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnchinSeiqtoNm.MaxLineCount = 0
        Me.lblUnchinSeiqtoNm.Multiline = False
        Me.lblUnchinSeiqtoNm.Name = "lblUnchinSeiqtoNm"
        Me.lblUnchinSeiqtoNm.ReadOnly = True
        Me.lblUnchinSeiqtoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnchinSeiqtoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnchinSeiqtoNm.Size = New System.Drawing.Size(507, 18)
        Me.lblUnchinSeiqtoNm.TabIndex = 454
        Me.lblUnchinSeiqtoNm.TabStop = False
        Me.lblUnchinSeiqtoNm.TabStopSetting = False
        Me.lblUnchinSeiqtoNm.TextValue = ""
        Me.lblUnchinSeiqtoNm.UseSystemPasswordChar = False
        Me.lblUnchinSeiqtoNm.WidthDef = 507
        Me.lblUnchinSeiqtoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnchinSeiqtoCd
        '
        Me.txtUnchinSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnchinSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnchinSeiqtoCd.CountWrappedLine = False
        Me.txtUnchinSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnchinSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnchinSeiqtoCd.HeightDef = 18
        Me.txtUnchinSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnchinSeiqtoCd.HissuLabelVisible = False
        Me.txtUnchinSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnchinSeiqtoCd.IsByteCheck = 7
        Me.txtUnchinSeiqtoCd.IsCalendarCheck = False
        Me.txtUnchinSeiqtoCd.IsDakutenCheck = False
        Me.txtUnchinSeiqtoCd.IsEisuCheck = False
        Me.txtUnchinSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtUnchinSeiqtoCd.IsFullByteCheck = 0
        Me.txtUnchinSeiqtoCd.IsHankakuCheck = False
        Me.txtUnchinSeiqtoCd.IsHissuCheck = False
        Me.txtUnchinSeiqtoCd.IsKanaCheck = False
        Me.txtUnchinSeiqtoCd.IsMiddleSpace = False
        Me.txtUnchinSeiqtoCd.IsNumericCheck = False
        Me.txtUnchinSeiqtoCd.IsSujiCheck = False
        Me.txtUnchinSeiqtoCd.IsZenkakuCheck = False
        Me.txtUnchinSeiqtoCd.ItemName = ""
        Me.txtUnchinSeiqtoCd.LineSpace = 0
        Me.txtUnchinSeiqtoCd.Location = New System.Drawing.Point(182, 637)
        Me.txtUnchinSeiqtoCd.MaxLength = 7
        Me.txtUnchinSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnchinSeiqtoCd.MaxLineCount = 0
        Me.txtUnchinSeiqtoCd.Multiline = False
        Me.txtUnchinSeiqtoCd.Name = "txtUnchinSeiqtoCd"
        Me.txtUnchinSeiqtoCd.ReadOnly = False
        Me.txtUnchinSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnchinSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnchinSeiqtoCd.Size = New System.Drawing.Size(133, 18)
        Me.txtUnchinSeiqtoCd.TabIndex = 453
        Me.txtUnchinSeiqtoCd.TabStopSetting = True
        Me.txtUnchinSeiqtoCd.TextValue = ""
        Me.txtUnchinSeiqtoCd.UseSystemPasswordChar = False
        Me.txtUnchinSeiqtoCd.WidthDef = 133
        Me.txtUnchinSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnchinSeiqto
        '
        Me.lblUnchinSeiqto.AutoSize = True
        Me.lblUnchinSeiqto.AutoSizeDef = True
        Me.lblUnchinSeiqto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinSeiqto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinSeiqto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnchinSeiqto.EnableStatus = False
        Me.lblUnchinSeiqto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinSeiqto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinSeiqto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinSeiqto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinSeiqto.HeightDef = 13
        Me.lblUnchinSeiqto.Location = New System.Drawing.Point(10, 639)
        Me.lblUnchinSeiqto.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblUnchinSeiqto.Name = "lblUnchinSeiqto"
        Me.lblUnchinSeiqto.Size = New System.Drawing.Size(167, 13)
        Me.lblUnchinSeiqto.TabIndex = 452
        Me.lblUnchinSeiqto.Text = "運賃請求先"
        Me.lblUnchinSeiqto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinSeiqto.TextValue = "運賃請求先"
        Me.lblUnchinSeiqto.WidthDef = 167
        '
        'txtJIS
        '
        Me.txtJIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJIS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJIS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJIS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtJIS.CountWrappedLine = False
        Me.txtJIS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtJIS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJIS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJIS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJIS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJIS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtJIS.HeightDef = 18
        Me.txtJIS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtJIS.HissuLabelVisible = False
        Me.txtJIS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtJIS.IsByteCheck = 7
        Me.txtJIS.IsCalendarCheck = False
        Me.txtJIS.IsDakutenCheck = False
        Me.txtJIS.IsEisuCheck = False
        Me.txtJIS.IsForbiddenWordsCheck = False
        Me.txtJIS.IsFullByteCheck = 0
        Me.txtJIS.IsHankakuCheck = False
        Me.txtJIS.IsHissuCheck = False
        Me.txtJIS.IsKanaCheck = False
        Me.txtJIS.IsMiddleSpace = False
        Me.txtJIS.IsNumericCheck = False
        Me.txtJIS.IsSujiCheck = False
        Me.txtJIS.IsZenkakuCheck = False
        Me.txtJIS.ItemName = ""
        Me.txtJIS.LineSpace = 0
        Me.txtJIS.Location = New System.Drawing.Point(417, 443)
        Me.txtJIS.MaxLength = 7
        Me.txtJIS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtJIS.MaxLineCount = 0
        Me.txtJIS.Multiline = False
        Me.txtJIS.Name = "txtJIS"
        Me.txtJIS.ReadOnly = False
        Me.txtJIS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtJIS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtJIS.Size = New System.Drawing.Size(139, 18)
        Me.txtJIS.TabIndex = 451
        Me.txtJIS.TabStopSetting = True
        Me.txtJIS.TextValue = ""
        Me.txtJIS.UseSystemPasswordChar = False
        Me.txtJIS.WidthDef = 139
        Me.txtJIS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKyori
        '
        Me.lblKyori.AutoSize = True
        Me.lblKyori.AutoSizeDef = True
        Me.lblKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKyori.EnableStatus = False
        Me.lblKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKyori.HeightDef = 13
        Me.lblKyori.Location = New System.Drawing.Point(336, 465)
        Me.lblKyori.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblKyori.Name = "lblKyori"
        Me.lblKyori.Size = New System.Drawing.Size(78, 13)
        Me.lblKyori.TabIndex = 450
        Me.lblKyori.Text = "指定距離"
        Me.lblKyori.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKyori.TextValue = "指定距離"
        Me.lblKyori.WidthDef = 78
        '
        'lblJIS
        '
        Me.lblJIS.AutoSize = True
        Me.lblJIS.AutoSizeDef = True
        Me.lblJIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJIS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJIS.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblJIS.EnableStatus = False
        Me.lblJIS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJIS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJIS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJIS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJIS.HeightDef = 13
        Me.lblJIS.Location = New System.Drawing.Point(336, 444)
        Me.lblJIS.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblJIS.Name = "lblJIS"
        Me.lblJIS.Size = New System.Drawing.Size(78, 13)
        Me.lblJIS.TabIndex = 449
        Me.lblJIS.Text = "JISコード"
        Me.lblJIS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblJIS.TextValue = "JISコード"
        Me.lblJIS.WidthDef = 78
        '
        'lblYokoTariffRem
        '
        Me.lblYokoTariffRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokoTariffRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokoTariffRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYokoTariffRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblYokoTariffRem.CountWrappedLine = False
        Me.lblYokoTariffRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblYokoTariffRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokoTariffRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokoTariffRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokoTariffRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokoTariffRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblYokoTariffRem.HeightDef = 18
        Me.lblYokoTariffRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokoTariffRem.HissuLabelVisible = False
        Me.lblYokoTariffRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblYokoTariffRem.IsByteCheck = 0
        Me.lblYokoTariffRem.IsCalendarCheck = False
        Me.lblYokoTariffRem.IsDakutenCheck = False
        Me.lblYokoTariffRem.IsEisuCheck = False
        Me.lblYokoTariffRem.IsForbiddenWordsCheck = False
        Me.lblYokoTariffRem.IsFullByteCheck = 0
        Me.lblYokoTariffRem.IsHankakuCheck = False
        Me.lblYokoTariffRem.IsHissuCheck = False
        Me.lblYokoTariffRem.IsKanaCheck = False
        Me.lblYokoTariffRem.IsMiddleSpace = False
        Me.lblYokoTariffRem.IsNumericCheck = False
        Me.lblYokoTariffRem.IsSujiCheck = False
        Me.lblYokoTariffRem.IsZenkakuCheck = False
        Me.lblYokoTariffRem.ItemName = ""
        Me.lblYokoTariffRem.LineSpace = 0
        Me.lblYokoTariffRem.Location = New System.Drawing.Point(299, 717)
        Me.lblYokoTariffRem.MaxLength = 0
        Me.lblYokoTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblYokoTariffRem.MaxLineCount = 0
        Me.lblYokoTariffRem.Multiline = False
        Me.lblYokoTariffRem.Name = "lblYokoTariffRem"
        Me.lblYokoTariffRem.ReadOnly = True
        Me.lblYokoTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblYokoTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblYokoTariffRem.Size = New System.Drawing.Size(507, 18)
        Me.lblYokoTariffRem.TabIndex = 448
        Me.lblYokoTariffRem.TabStop = False
        Me.lblYokoTariffRem.TabStopSetting = False
        Me.lblYokoTariffRem.TextValue = ""
        Me.lblYokoTariffRem.UseSystemPasswordChar = False
        Me.lblYokoTariffRem.WidthDef = 507
        Me.lblYokoTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtYokoTariffCd
        '
        Me.txtYokoTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYokoTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYokoTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYokoTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtYokoTariffCd.CountWrappedLine = False
        Me.txtYokoTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtYokoTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYokoTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYokoTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYokoTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYokoTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtYokoTariffCd.HeightDef = 18
        Me.txtYokoTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtYokoTariffCd.HissuLabelVisible = False
        Me.txtYokoTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtYokoTariffCd.IsByteCheck = 10
        Me.txtYokoTariffCd.IsCalendarCheck = False
        Me.txtYokoTariffCd.IsDakutenCheck = False
        Me.txtYokoTariffCd.IsEisuCheck = False
        Me.txtYokoTariffCd.IsForbiddenWordsCheck = False
        Me.txtYokoTariffCd.IsFullByteCheck = 0
        Me.txtYokoTariffCd.IsHankakuCheck = False
        Me.txtYokoTariffCd.IsHissuCheck = False
        Me.txtYokoTariffCd.IsKanaCheck = False
        Me.txtYokoTariffCd.IsMiddleSpace = False
        Me.txtYokoTariffCd.IsNumericCheck = False
        Me.txtYokoTariffCd.IsSujiCheck = False
        Me.txtYokoTariffCd.IsZenkakuCheck = False
        Me.txtYokoTariffCd.ItemName = ""
        Me.txtYokoTariffCd.LineSpace = 0
        Me.txtYokoTariffCd.Location = New System.Drawing.Point(182, 717)
        Me.txtYokoTariffCd.MaxLength = 10
        Me.txtYokoTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtYokoTariffCd.MaxLineCount = 0
        Me.txtYokoTariffCd.Multiline = False
        Me.txtYokoTariffCd.Name = "txtYokoTariffCd"
        Me.txtYokoTariffCd.ReadOnly = False
        Me.txtYokoTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtYokoTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtYokoTariffCd.Size = New System.Drawing.Size(133, 18)
        Me.txtYokoTariffCd.TabIndex = 447
        Me.txtYokoTariffCd.TabStopSetting = True
        Me.txtYokoTariffCd.TextValue = ""
        Me.txtYokoTariffCd.UseSystemPasswordChar = False
        Me.txtYokoTariffCd.WidthDef = 133
        Me.txtYokoTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblYokoTariff
        '
        Me.lblYokoTariff.AutoSize = True
        Me.lblYokoTariff.AutoSizeDef = True
        Me.lblYokoTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokoTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokoTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblYokoTariff.EnableStatus = False
        Me.lblYokoTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokoTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokoTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokoTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokoTariff.HeightDef = 13
        Me.lblYokoTariff.Location = New System.Drawing.Point(10, 719)
        Me.lblYokoTariff.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblYokoTariff.Name = "lblYokoTariff"
        Me.lblYokoTariff.Size = New System.Drawing.Size(167, 13)
        Me.lblYokoTariff.TabIndex = 446
        Me.lblYokoTariff.Text = "横持ち運賃タリフ"
        Me.lblYokoTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblYokoTariff.TextValue = "横持ち運賃タリフ"
        Me.lblYokoTariff.WidthDef = 167
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
        Me.lblExtcTariffRem.Location = New System.Drawing.Point(299, 697)
        Me.lblExtcTariffRem.MaxLength = 0
        Me.lblExtcTariffRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblExtcTariffRem.MaxLineCount = 0
        Me.lblExtcTariffRem.Multiline = False
        Me.lblExtcTariffRem.Name = "lblExtcTariffRem"
        Me.lblExtcTariffRem.ReadOnly = True
        Me.lblExtcTariffRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblExtcTariffRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblExtcTariffRem.Size = New System.Drawing.Size(507, 18)
        Me.lblExtcTariffRem.TabIndex = 445
        Me.lblExtcTariffRem.TabStop = False
        Me.lblExtcTariffRem.TabStopSetting = False
        Me.lblExtcTariffRem.TextValue = ""
        Me.lblExtcTariffRem.UseSystemPasswordChar = False
        Me.lblExtcTariffRem.WidthDef = 507
        Me.lblExtcTariffRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtExtcTariffCd.Location = New System.Drawing.Point(182, 697)
        Me.txtExtcTariffCd.MaxLength = 10
        Me.txtExtcTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtExtcTariffCd.MaxLineCount = 0
        Me.txtExtcTariffCd.Multiline = False
        Me.txtExtcTariffCd.Name = "txtExtcTariffCd"
        Me.txtExtcTariffCd.ReadOnly = False
        Me.txtExtcTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtExtcTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtExtcTariffCd.Size = New System.Drawing.Size(133, 18)
        Me.txtExtcTariffCd.TabIndex = 444
        Me.txtExtcTariffCd.TabStopSetting = True
        Me.txtExtcTariffCd.TextValue = ""
        Me.txtExtcTariffCd.UseSystemPasswordChar = False
        Me.txtExtcTariffCd.WidthDef = 133
        Me.txtExtcTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblExtcTariff
        '
        Me.lblExtcTariff.AutoSize = True
        Me.lblExtcTariff.AutoSizeDef = True
        Me.lblExtcTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblExtcTariff.EnableStatus = False
        Me.lblExtcTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcTariff.HeightDef = 13
        Me.lblExtcTariff.Location = New System.Drawing.Point(10, 700)
        Me.lblExtcTariff.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblExtcTariff.Name = "lblExtcTariff"
        Me.lblExtcTariff.Size = New System.Drawing.Size(167, 13)
        Me.lblExtcTariff.TabIndex = 443
        Me.lblExtcTariff.Text = "割増運賃タリフ"
        Me.lblExtcTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblExtcTariff.TextValue = "割増運賃タリフ"
        Me.lblExtcTariff.WidthDef = 167
        '
        'lblUnchinTariffRem2
        '
        Me.lblUnchinTariffRem2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnchinTariffRem2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnchinTariffRem2.CountWrappedLine = False
        Me.lblUnchinTariffRem2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnchinTariffRem2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariffRem2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariffRem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariffRem2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariffRem2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnchinTariffRem2.HeightDef = 18
        Me.lblUnchinTariffRem2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem2.HissuLabelVisible = False
        Me.lblUnchinTariffRem2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnchinTariffRem2.IsByteCheck = 0
        Me.lblUnchinTariffRem2.IsCalendarCheck = False
        Me.lblUnchinTariffRem2.IsDakutenCheck = False
        Me.lblUnchinTariffRem2.IsEisuCheck = False
        Me.lblUnchinTariffRem2.IsForbiddenWordsCheck = False
        Me.lblUnchinTariffRem2.IsFullByteCheck = 0
        Me.lblUnchinTariffRem2.IsHankakuCheck = False
        Me.lblUnchinTariffRem2.IsHissuCheck = False
        Me.lblUnchinTariffRem2.IsKanaCheck = False
        Me.lblUnchinTariffRem2.IsMiddleSpace = False
        Me.lblUnchinTariffRem2.IsNumericCheck = False
        Me.lblUnchinTariffRem2.IsSujiCheck = False
        Me.lblUnchinTariffRem2.IsZenkakuCheck = False
        Me.lblUnchinTariffRem2.ItemName = ""
        Me.lblUnchinTariffRem2.LineSpace = 0
        Me.lblUnchinTariffRem2.Location = New System.Drawing.Point(299, 677)
        Me.lblUnchinTariffRem2.MaxLength = 0
        Me.lblUnchinTariffRem2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnchinTariffRem2.MaxLineCount = 0
        Me.lblUnchinTariffRem2.Multiline = False
        Me.lblUnchinTariffRem2.Name = "lblUnchinTariffRem2"
        Me.lblUnchinTariffRem2.ReadOnly = True
        Me.lblUnchinTariffRem2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnchinTariffRem2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnchinTariffRem2.Size = New System.Drawing.Size(507, 18)
        Me.lblUnchinTariffRem2.TabIndex = 442
        Me.lblUnchinTariffRem2.TabStop = False
        Me.lblUnchinTariffRem2.TabStopSetting = False
        Me.lblUnchinTariffRem2.TextValue = ""
        Me.lblUnchinTariffRem2.UseSystemPasswordChar = False
        Me.lblUnchinTariffRem2.WidthDef = 507
        Me.lblUnchinTariffRem2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnchinTariffCd2
        '
        Me.txtUnchinTariffCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinTariffCd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinTariffCd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnchinTariffCd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnchinTariffCd2.CountWrappedLine = False
        Me.txtUnchinTariffCd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnchinTariffCd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinTariffCd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinTariffCd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinTariffCd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinTariffCd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnchinTariffCd2.HeightDef = 18
        Me.txtUnchinTariffCd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnchinTariffCd2.HissuLabelVisible = False
        Me.txtUnchinTariffCd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtUnchinTariffCd2.IsByteCheck = 10
        Me.txtUnchinTariffCd2.IsCalendarCheck = False
        Me.txtUnchinTariffCd2.IsDakutenCheck = False
        Me.txtUnchinTariffCd2.IsEisuCheck = False
        Me.txtUnchinTariffCd2.IsForbiddenWordsCheck = False
        Me.txtUnchinTariffCd2.IsFullByteCheck = 0
        Me.txtUnchinTariffCd2.IsHankakuCheck = False
        Me.txtUnchinTariffCd2.IsHissuCheck = False
        Me.txtUnchinTariffCd2.IsKanaCheck = False
        Me.txtUnchinTariffCd2.IsMiddleSpace = False
        Me.txtUnchinTariffCd2.IsNumericCheck = False
        Me.txtUnchinTariffCd2.IsSujiCheck = False
        Me.txtUnchinTariffCd2.IsZenkakuCheck = False
        Me.txtUnchinTariffCd2.ItemName = ""
        Me.txtUnchinTariffCd2.LineSpace = 0
        Me.txtUnchinTariffCd2.Location = New System.Drawing.Point(182, 677)
        Me.txtUnchinTariffCd2.MaxLength = 10
        Me.txtUnchinTariffCd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnchinTariffCd2.MaxLineCount = 0
        Me.txtUnchinTariffCd2.Multiline = False
        Me.txtUnchinTariffCd2.Name = "txtUnchinTariffCd2"
        Me.txtUnchinTariffCd2.ReadOnly = False
        Me.txtUnchinTariffCd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnchinTariffCd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnchinTariffCd2.Size = New System.Drawing.Size(133, 18)
        Me.txtUnchinTariffCd2.TabIndex = 441
        Me.txtUnchinTariffCd2.TabStopSetting = True
        Me.txtUnchinTariffCd2.TextValue = ""
        Me.txtUnchinTariffCd2.UseSystemPasswordChar = False
        Me.txtUnchinTariffCd2.WidthDef = 133
        Me.txtUnchinTariffCd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnchinTariff2
        '
        Me.lblUnchinTariff2.AutoSize = True
        Me.lblUnchinTariff2.AutoSizeDef = True
        Me.lblUnchinTariff2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariff2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariff2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnchinTariff2.EnableStatus = False
        Me.lblUnchinTariff2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariff2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariff2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariff2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariff2.HeightDef = 13
        Me.lblUnchinTariff2.Location = New System.Drawing.Point(10, 679)
        Me.lblUnchinTariff2.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblUnchinTariff2.Name = "lblUnchinTariff2"
        Me.lblUnchinTariff2.Size = New System.Drawing.Size(167, 13)
        Me.lblUnchinTariff2.TabIndex = 440
        Me.lblUnchinTariff2.Text = "運賃タリフ(車建)"
        Me.lblUnchinTariff2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinTariff2.TextValue = "運賃タリフ(車建)"
        Me.lblUnchinTariff2.WidthDef = 167
        '
        'lblUnchinTariffRem1
        '
        Me.lblUnchinTariffRem1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnchinTariffRem1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnchinTariffRem1.CountWrappedLine = False
        Me.lblUnchinTariffRem1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnchinTariffRem1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariffRem1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariffRem1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariffRem1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariffRem1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnchinTariffRem1.HeightDef = 18
        Me.lblUnchinTariffRem1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariffRem1.HissuLabelVisible = False
        Me.lblUnchinTariffRem1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnchinTariffRem1.IsByteCheck = 0
        Me.lblUnchinTariffRem1.IsCalendarCheck = False
        Me.lblUnchinTariffRem1.IsDakutenCheck = False
        Me.lblUnchinTariffRem1.IsEisuCheck = False
        Me.lblUnchinTariffRem1.IsForbiddenWordsCheck = False
        Me.lblUnchinTariffRem1.IsFullByteCheck = 0
        Me.lblUnchinTariffRem1.IsHankakuCheck = False
        Me.lblUnchinTariffRem1.IsHissuCheck = False
        Me.lblUnchinTariffRem1.IsKanaCheck = False
        Me.lblUnchinTariffRem1.IsMiddleSpace = False
        Me.lblUnchinTariffRem1.IsNumericCheck = False
        Me.lblUnchinTariffRem1.IsSujiCheck = False
        Me.lblUnchinTariffRem1.IsZenkakuCheck = False
        Me.lblUnchinTariffRem1.ItemName = ""
        Me.lblUnchinTariffRem1.LineSpace = 0
        Me.lblUnchinTariffRem1.Location = New System.Drawing.Point(299, 657)
        Me.lblUnchinTariffRem1.MaxLength = 0
        Me.lblUnchinTariffRem1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnchinTariffRem1.MaxLineCount = 0
        Me.lblUnchinTariffRem1.Multiline = False
        Me.lblUnchinTariffRem1.Name = "lblUnchinTariffRem1"
        Me.lblUnchinTariffRem1.ReadOnly = True
        Me.lblUnchinTariffRem1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnchinTariffRem1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnchinTariffRem1.Size = New System.Drawing.Size(507, 18)
        Me.lblUnchinTariffRem1.TabIndex = 439
        Me.lblUnchinTariffRem1.TabStop = False
        Me.lblUnchinTariffRem1.TabStopSetting = False
        Me.lblUnchinTariffRem1.TextValue = ""
        Me.lblUnchinTariffRem1.UseSystemPasswordChar = False
        Me.lblUnchinTariffRem1.WidthDef = 507
        Me.lblUnchinTariffRem1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnchinTariffCd1
        '
        Me.txtUnchinTariffCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinTariffCd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnchinTariffCd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnchinTariffCd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnchinTariffCd1.CountWrappedLine = False
        Me.txtUnchinTariffCd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnchinTariffCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinTariffCd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnchinTariffCd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinTariffCd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnchinTariffCd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnchinTariffCd1.HeightDef = 18
        Me.txtUnchinTariffCd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnchinTariffCd1.HissuLabelVisible = False
        Me.txtUnchinTariffCd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtUnchinTariffCd1.IsByteCheck = 10
        Me.txtUnchinTariffCd1.IsCalendarCheck = False
        Me.txtUnchinTariffCd1.IsDakutenCheck = False
        Me.txtUnchinTariffCd1.IsEisuCheck = False
        Me.txtUnchinTariffCd1.IsForbiddenWordsCheck = False
        Me.txtUnchinTariffCd1.IsFullByteCheck = 0
        Me.txtUnchinTariffCd1.IsHankakuCheck = False
        Me.txtUnchinTariffCd1.IsHissuCheck = False
        Me.txtUnchinTariffCd1.IsKanaCheck = False
        Me.txtUnchinTariffCd1.IsMiddleSpace = False
        Me.txtUnchinTariffCd1.IsNumericCheck = False
        Me.txtUnchinTariffCd1.IsSujiCheck = False
        Me.txtUnchinTariffCd1.IsZenkakuCheck = False
        Me.txtUnchinTariffCd1.ItemName = ""
        Me.txtUnchinTariffCd1.LineSpace = 0
        Me.txtUnchinTariffCd1.Location = New System.Drawing.Point(182, 657)
        Me.txtUnchinTariffCd1.MaxLength = 10
        Me.txtUnchinTariffCd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnchinTariffCd1.MaxLineCount = 0
        Me.txtUnchinTariffCd1.Multiline = False
        Me.txtUnchinTariffCd1.Name = "txtUnchinTariffCd1"
        Me.txtUnchinTariffCd1.ReadOnly = False
        Me.txtUnchinTariffCd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnchinTariffCd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnchinTariffCd1.Size = New System.Drawing.Size(133, 18)
        Me.txtUnchinTariffCd1.TabIndex = 438
        Me.txtUnchinTariffCd1.TabStopSetting = True
        Me.txtUnchinTariffCd1.TextValue = ""
        Me.txtUnchinTariffCd1.UseSystemPasswordChar = False
        Me.txtUnchinTariffCd1.WidthDef = 133
        Me.txtUnchinTariffCd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnchinTariff1
        '
        Me.lblUnchinTariff1.AutoSize = True
        Me.lblUnchinTariff1.AutoSizeDef = True
        Me.lblUnchinTariff1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariff1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinTariff1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnchinTariff1.EnableStatus = False
        Me.lblUnchinTariff1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariff1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinTariff1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariff1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinTariff1.HeightDef = 13
        Me.lblUnchinTariff1.Location = New System.Drawing.Point(10, 659)
        Me.lblUnchinTariff1.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblUnchinTariff1.Name = "lblUnchinTariff1"
        Me.lblUnchinTariff1.Size = New System.Drawing.Size(167, 13)
        Me.lblUnchinTariff1.TabIndex = 437
        Me.lblUnchinTariff1.Text = "運賃タリフ(トンキロ建)"
        Me.lblUnchinTariff1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinTariff1.TextValue = "運賃タリフ(トンキロ建)"
        Me.lblUnchinTariff1.WidthDef = 167
        '
        'lblUriageNm
        '
        Me.lblUriageNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUriageNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUriageNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUriageNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUriageNm.CountWrappedLine = False
        Me.lblUriageNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUriageNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUriageNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUriageNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUriageNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUriageNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUriageNm.HeightDef = 18
        Me.lblUriageNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUriageNm.HissuLabelVisible = False
        Me.lblUriageNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUriageNm.IsByteCheck = 0
        Me.lblUriageNm.IsCalendarCheck = False
        Me.lblUriageNm.IsDakutenCheck = False
        Me.lblUriageNm.IsEisuCheck = False
        Me.lblUriageNm.IsForbiddenWordsCheck = False
        Me.lblUriageNm.IsFullByteCheck = 0
        Me.lblUriageNm.IsHankakuCheck = False
        Me.lblUriageNm.IsHissuCheck = False
        Me.lblUriageNm.IsKanaCheck = False
        Me.lblUriageNm.IsMiddleSpace = False
        Me.lblUriageNm.IsNumericCheck = False
        Me.lblUriageNm.IsSujiCheck = False
        Me.lblUriageNm.IsZenkakuCheck = False
        Me.lblUriageNm.ItemName = ""
        Me.lblUriageNm.LineSpace = 0
        Me.lblUriageNm.Location = New System.Drawing.Point(299, 617)
        Me.lblUriageNm.MaxLength = 0
        Me.lblUriageNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUriageNm.MaxLineCount = 0
        Me.lblUriageNm.Multiline = False
        Me.lblUriageNm.Name = "lblUriageNm"
        Me.lblUriageNm.ReadOnly = True
        Me.lblUriageNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUriageNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUriageNm.Size = New System.Drawing.Size(507, 18)
        Me.lblUriageNm.TabIndex = 436
        Me.lblUriageNm.TabStop = False
        Me.lblUriageNm.TabStopSetting = False
        Me.lblUriageNm.TextValue = ""
        Me.lblUriageNm.UseSystemPasswordChar = False
        Me.lblUriageNm.WidthDef = 507
        Me.lblUriageNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUriageCd
        '
        Me.txtUriageCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUriageCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUriageCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUriageCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUriageCd.CountWrappedLine = False
        Me.txtUriageCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUriageCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUriageCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUriageCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUriageCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUriageCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUriageCd.HeightDef = 18
        Me.txtUriageCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUriageCd.HissuLabelVisible = False
        Me.txtUriageCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtUriageCd.IsByteCheck = 15
        Me.txtUriageCd.IsCalendarCheck = False
        Me.txtUriageCd.IsDakutenCheck = False
        Me.txtUriageCd.IsEisuCheck = False
        Me.txtUriageCd.IsForbiddenWordsCheck = False
        Me.txtUriageCd.IsFullByteCheck = 0
        Me.txtUriageCd.IsHankakuCheck = False
        Me.txtUriageCd.IsHissuCheck = False
        Me.txtUriageCd.IsKanaCheck = False
        Me.txtUriageCd.IsMiddleSpace = False
        Me.txtUriageCd.IsNumericCheck = False
        Me.txtUriageCd.IsSujiCheck = False
        Me.txtUriageCd.IsZenkakuCheck = False
        Me.txtUriageCd.ItemName = ""
        Me.txtUriageCd.LineSpace = 0
        Me.txtUriageCd.Location = New System.Drawing.Point(182, 617)
        Me.txtUriageCd.MaxLength = 15
        Me.txtUriageCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUriageCd.MaxLineCount = 0
        Me.txtUriageCd.Multiline = False
        Me.txtUriageCd.Name = "txtUriageCd"
        Me.txtUriageCd.ReadOnly = False
        Me.txtUriageCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUriageCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUriageCd.Size = New System.Drawing.Size(133, 18)
        Me.txtUriageCd.TabIndex = 435
        Me.txtUriageCd.TabStopSetting = True
        Me.txtUriageCd.TextValue = ""
        Me.txtUriageCd.UseSystemPasswordChar = False
        Me.txtUriageCd.WidthDef = 133
        Me.txtUriageCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUriage
        '
        Me.lblUriage.AutoSize = True
        Me.lblUriage.AutoSizeDef = True
        Me.lblUriage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUriage.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUriage.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUriage.EnableStatus = False
        Me.lblUriage.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUriage.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUriage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUriage.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUriage.HeightDef = 13
        Me.lblUriage.Location = New System.Drawing.Point(10, 619)
        Me.lblUriage.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblUriage.Name = "lblUriage"
        Me.lblUriage.Size = New System.Drawing.Size(167, 13)
        Me.lblUriage.TabIndex = 434
        Me.lblUriage.Text = "売上先"
        Me.lblUriage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUriage.TextValue = "売上先"
        Me.lblUriage.WidthDef = 167
        '
        'lblSales
        '
        Me.lblSales.AutoSize = True
        Me.lblSales.AutoSizeDef = True
        Me.lblSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSales.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSales.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSales.EnableStatus = False
        Me.lblSales.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSales.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSales.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSales.HeightDef = 13
        Me.lblSales.Location = New System.Drawing.Point(10, 599)
        Me.lblSales.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblSales.Name = "lblSales"
        Me.lblSales.Size = New System.Drawing.Size(167, 13)
        Me.lblSales.TabIndex = 433
        Me.lblSales.Text = "納品書荷主名義"
        Me.lblSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSales.TextValue = "納品書荷主名義"
        Me.lblSales.WidthDef = 167
        '
        'lblSalesNm
        '
        Me.lblSalesNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSalesNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSalesNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalesNm.CountWrappedLine = False
        Me.lblSalesNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSalesNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSalesNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSalesNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSalesNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSalesNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSalesNm.HeightDef = 18
        Me.lblSalesNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSalesNm.HissuLabelVisible = False
        Me.lblSalesNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSalesNm.IsByteCheck = 0
        Me.lblSalesNm.IsCalendarCheck = False
        Me.lblSalesNm.IsDakutenCheck = False
        Me.lblSalesNm.IsEisuCheck = False
        Me.lblSalesNm.IsForbiddenWordsCheck = False
        Me.lblSalesNm.IsFullByteCheck = 0
        Me.lblSalesNm.IsHankakuCheck = False
        Me.lblSalesNm.IsHissuCheck = False
        Me.lblSalesNm.IsKanaCheck = False
        Me.lblSalesNm.IsMiddleSpace = False
        Me.lblSalesNm.IsNumericCheck = False
        Me.lblSalesNm.IsSujiCheck = False
        Me.lblSalesNm.IsZenkakuCheck = False
        Me.lblSalesNm.ItemName = ""
        Me.lblSalesNm.LineSpace = 0
        Me.lblSalesNm.Location = New System.Drawing.Point(263, 597)
        Me.lblSalesNm.MaxLength = 0
        Me.lblSalesNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSalesNm.MaxLineCount = 0
        Me.lblSalesNm.Multiline = False
        Me.lblSalesNm.Name = "lblSalesNm"
        Me.lblSalesNm.ReadOnly = True
        Me.lblSalesNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSalesNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSalesNm.Size = New System.Drawing.Size(543, 18)
        Me.lblSalesNm.TabIndex = 432
        Me.lblSalesNm.TabStop = False
        Me.lblSalesNm.TabStopSetting = False
        Me.lblSalesNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSalesNm.UseSystemPasswordChar = False
        Me.lblSalesNm.WidthDef = 543
        Me.lblSalesNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSalesCd
        '
        Me.txtSalesCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSalesCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSalesCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSalesCd.CountWrappedLine = False
        Me.txtSalesCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSalesCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSalesCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSalesCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSalesCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSalesCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSalesCd.HeightDef = 18
        Me.txtSalesCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSalesCd.HissuLabelVisible = False
        Me.txtSalesCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSalesCd.IsByteCheck = 5
        Me.txtSalesCd.IsCalendarCheck = False
        Me.txtSalesCd.IsDakutenCheck = False
        Me.txtSalesCd.IsEisuCheck = False
        Me.txtSalesCd.IsForbiddenWordsCheck = False
        Me.txtSalesCd.IsFullByteCheck = 0
        Me.txtSalesCd.IsHankakuCheck = False
        Me.txtSalesCd.IsHissuCheck = False
        Me.txtSalesCd.IsKanaCheck = False
        Me.txtSalesCd.IsMiddleSpace = False
        Me.txtSalesCd.IsNumericCheck = False
        Me.txtSalesCd.IsSujiCheck = False
        Me.txtSalesCd.IsZenkakuCheck = False
        Me.txtSalesCd.ItemName = ""
        Me.txtSalesCd.LineSpace = 0
        Me.txtSalesCd.Location = New System.Drawing.Point(182, 597)
        Me.txtSalesCd.MaxLength = 5
        Me.txtSalesCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSalesCd.MaxLineCount = 0
        Me.txtSalesCd.Multiline = False
        Me.txtSalesCd.Name = "txtSalesCd"
        Me.txtSalesCd.ReadOnly = False
        Me.txtSalesCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSalesCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSalesCd.Size = New System.Drawing.Size(97, 18)
        Me.txtSalesCd.TabIndex = 431
        Me.txtSalesCd.TabStopSetting = True
        Me.txtSalesCd.TextValue = "X1"
        Me.txtSalesCd.UseSystemPasswordChar = False
        Me.txtSalesCd.WidthDef = 97
        Me.txtSalesCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCargoTimeLimit
        '
        Me.txtCargoTimeLimit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCargoTimeLimit.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCargoTimeLimit.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCargoTimeLimit.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCargoTimeLimit.CountWrappedLine = False
        Me.txtCargoTimeLimit.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCargoTimeLimit.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCargoTimeLimit.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCargoTimeLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCargoTimeLimit.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCargoTimeLimit.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCargoTimeLimit.HeightDef = 18
        Me.txtCargoTimeLimit.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCargoTimeLimit.HissuLabelVisible = False
        Me.txtCargoTimeLimit.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCargoTimeLimit.IsByteCheck = 40
        Me.txtCargoTimeLimit.IsCalendarCheck = False
        Me.txtCargoTimeLimit.IsDakutenCheck = False
        Me.txtCargoTimeLimit.IsEisuCheck = False
        Me.txtCargoTimeLimit.IsForbiddenWordsCheck = False
        Me.txtCargoTimeLimit.IsFullByteCheck = 0
        Me.txtCargoTimeLimit.IsHankakuCheck = False
        Me.txtCargoTimeLimit.IsHissuCheck = False
        Me.txtCargoTimeLimit.IsKanaCheck = False
        Me.txtCargoTimeLimit.IsMiddleSpace = False
        Me.txtCargoTimeLimit.IsNumericCheck = False
        Me.txtCargoTimeLimit.IsSujiCheck = False
        Me.txtCargoTimeLimit.IsZenkakuCheck = False
        Me.txtCargoTimeLimit.ItemName = ""
        Me.txtCargoTimeLimit.LineSpace = 0
        Me.txtCargoTimeLimit.Location = New System.Drawing.Point(790, 537)
        Me.txtCargoTimeLimit.MaxLength = 40
        Me.txtCargoTimeLimit.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCargoTimeLimit.MaxLineCount = 0
        Me.txtCargoTimeLimit.Multiline = False
        Me.txtCargoTimeLimit.Name = "txtCargoTimeLimit"
        Me.txtCargoTimeLimit.ReadOnly = False
        Me.txtCargoTimeLimit.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCargoTimeLimit.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCargoTimeLimit.Size = New System.Drawing.Size(303, 18)
        Me.txtCargoTimeLimit.TabIndex = 428
        Me.txtCargoTimeLimit.TabStopSetting = True
        Me.txtCargoTimeLimit.TextValue = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
        Me.txtCargoTimeLimit.UseSystemPasswordChar = False
        Me.txtCargoTimeLimit.WidthDef = 303
        Me.txtCargoTimeLimit.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCargoTimeLimit
        '
        Me.lblCargoTimeLimit.AutoSize = True
        Me.lblCargoTimeLimit.AutoSizeDef = True
        Me.lblCargoTimeLimit.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCargoTimeLimit.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCargoTimeLimit.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCargoTimeLimit.EnableStatus = False
        Me.lblCargoTimeLimit.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCargoTimeLimit.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCargoTimeLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCargoTimeLimit.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCargoTimeLimit.HeightDef = 13
        Me.lblCargoTimeLimit.Location = New System.Drawing.Point(679, 539)
        Me.lblCargoTimeLimit.MinimumSize = New System.Drawing.Size(108, 0)
        Me.lblCargoTimeLimit.Name = "lblCargoTimeLimit"
        Me.lblCargoTimeLimit.Size = New System.Drawing.Size(108, 13)
        Me.lblCargoTimeLimit.TabIndex = 427
        Me.lblCargoTimeLimit.Text = "荷卸時間制限"
        Me.lblCargoTimeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCargoTimeLimit.TextValue = "荷卸時間制限"
        Me.lblCargoTimeLimit.WidthDef = 108
        '
        'cmbMotoChakuKbn
        '
        Me.cmbMotoChakuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMotoChakuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMotoChakuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbMotoChakuKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbMotoChakuKbn.DataCode = "M001"
        Me.cmbMotoChakuKbn.DataSource = Nothing
        Me.cmbMotoChakuKbn.DisplayMember = Nothing
        Me.cmbMotoChakuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMotoChakuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMotoChakuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMotoChakuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMotoChakuKbn.HeightDef = 18
        Me.cmbMotoChakuKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbMotoChakuKbn.HissuLabelVisible = False
        Me.cmbMotoChakuKbn.InsertWildCard = True
        Me.cmbMotoChakuKbn.IsForbiddenWordsCheck = False
        Me.cmbMotoChakuKbn.IsHissuCheck = False
        Me.cmbMotoChakuKbn.ItemName = ""
        Me.cmbMotoChakuKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbMotoChakuKbn.Location = New System.Drawing.Point(604, 537)
        Me.cmbMotoChakuKbn.Name = "cmbMotoChakuKbn"
        Me.cmbMotoChakuKbn.ReadOnly = False
        Me.cmbMotoChakuKbn.SelectedIndex = -1
        Me.cmbMotoChakuKbn.SelectedItem = Nothing
        Me.cmbMotoChakuKbn.SelectedText = ""
        Me.cmbMotoChakuKbn.SelectedValue = ""
        Me.cmbMotoChakuKbn.Size = New System.Drawing.Size(85, 18)
        Me.cmbMotoChakuKbn.TabIndex = 426
        Me.cmbMotoChakuKbn.TabStopSetting = True
        Me.cmbMotoChakuKbn.TextValue = ""
        Me.cmbMotoChakuKbn.Value1 = Nothing
        Me.cmbMotoChakuKbn.Value2 = Nothing
        Me.cmbMotoChakuKbn.Value3 = Nothing
        Me.cmbMotoChakuKbn.ValueMember = Nothing
        Me.cmbMotoChakuKbn.WidthDef = 85
        '
        'lblMotoChakuKbn
        '
        Me.lblMotoChakuKbn.AutoSize = True
        Me.lblMotoChakuKbn.AutoSizeDef = True
        Me.lblMotoChakuKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoChakuKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoChakuKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblMotoChakuKbn.EnableStatus = False
        Me.lblMotoChakuKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoChakuKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoChakuKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoChakuKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoChakuKbn.HeightDef = 13
        Me.lblMotoChakuKbn.Location = New System.Drawing.Point(518, 540)
        Me.lblMotoChakuKbn.MinimumSize = New System.Drawing.Size(83, 0)
        Me.lblMotoChakuKbn.Name = "lblMotoChakuKbn"
        Me.lblMotoChakuKbn.Size = New System.Drawing.Size(83, 13)
        Me.lblMotoChakuKbn.TabIndex = 425
        Me.lblMotoChakuKbn.Text = "元着払区分"
        Me.lblMotoChakuKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMotoChakuKbn.TextValue = "元着払区分"
        Me.lblMotoChakuKbn.WidthDef = 83
        '
        'cmbBin
        '
        Me.cmbBin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBin.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBin.DataCode = "U001"
        Me.cmbBin.DataSource = Nothing
        Me.cmbBin.DisplayMember = Nothing
        Me.cmbBin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBin.HeightDef = 18
        Me.cmbBin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBin.HissuLabelVisible = False
        Me.cmbBin.InsertWildCard = True
        Me.cmbBin.IsForbiddenWordsCheck = False
        Me.cmbBin.IsHissuCheck = False
        Me.cmbBin.ItemName = ""
        Me.cmbBin.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBin.Location = New System.Drawing.Point(405, 537)
        Me.cmbBin.Name = "cmbBin"
        Me.cmbBin.ReadOnly = False
        Me.cmbBin.SelectedIndex = -1
        Me.cmbBin.SelectedItem = Nothing
        Me.cmbBin.SelectedText = ""
        Me.cmbBin.SelectedValue = ""
        Me.cmbBin.Size = New System.Drawing.Size(124, 18)
        Me.cmbBin.TabIndex = 424
        Me.cmbBin.TabStopSetting = True
        Me.cmbBin.TextValue = ""
        Me.cmbBin.Value1 = Nothing
        Me.cmbBin.Value2 = Nothing
        Me.cmbBin.Value3 = Nothing
        Me.cmbBin.ValueMember = Nothing
        Me.cmbBin.WidthDef = 124
        '
        'lblBin
        '
        Me.lblBin.AutoSize = True
        Me.lblBin.AutoSizeDef = True
        Me.lblBin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblBin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblBin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblBin.EnableStatus = False
        Me.lblBin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBin.HeightDef = 13
        Me.lblBin.Location = New System.Drawing.Point(336, 539)
        Me.lblBin.MinimumSize = New System.Drawing.Size(65, 0)
        Me.lblBin.Name = "lblBin"
        Me.lblBin.Size = New System.Drawing.Size(65, 13)
        Me.lblBin.TabIndex = 423
        Me.lblBin.Text = "便区分"
        Me.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBin.TextValue = "便区分"
        Me.lblBin.WidthDef = 65
        '
        'lblPick
        '
        Me.lblPick.AutoSize = True
        Me.lblPick.AutoSizeDef = True
        Me.lblPick.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPick.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPick.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblPick.EnableStatus = False
        Me.lblPick.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPick.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPick.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPick.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPick.HeightDef = 13
        Me.lblPick.Location = New System.Drawing.Point(10, 540)
        Me.lblPick.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblPick.Name = "lblPick"
        Me.lblPick.Size = New System.Drawing.Size(167, 13)
        Me.lblPick.TabIndex = 421
        Me.lblPick.Text = "ピッキングリスト区分"
        Me.lblPick.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPick.TextValue = "ピッキングリスト区分"
        Me.lblPick.WidthDef = 167
        '
        'cmbPick
        '
        Me.cmbPick.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPick.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPick.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPick.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPick.DataCode = "P001"
        Me.cmbPick.DataSource = Nothing
        Me.cmbPick.DisplayMember = Nothing
        Me.cmbPick.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPick.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPick.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPick.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPick.HeightDef = 18
        Me.cmbPick.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPick.HissuLabelVisible = False
        Me.cmbPick.InsertWildCard = True
        Me.cmbPick.IsForbiddenWordsCheck = False
        Me.cmbPick.IsHissuCheck = False
        Me.cmbPick.ItemName = ""
        Me.cmbPick.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPick.Location = New System.Drawing.Point(182, 537)
        Me.cmbPick.Name = "cmbPick"
        Me.cmbPick.ReadOnly = False
        Me.cmbPick.SelectedIndex = -1
        Me.cmbPick.SelectedItem = Nothing
        Me.cmbPick.SelectedText = ""
        Me.cmbPick.SelectedValue = ""
        Me.cmbPick.Size = New System.Drawing.Size(166, 18)
        Me.cmbPick.TabIndex = 422
        Me.cmbPick.TabStopSetting = True
        Me.cmbPick.TextValue = ""
        Me.cmbPick.Value1 = Nothing
        Me.cmbPick.Value2 = Nothing
        Me.cmbPick.Value3 = Nothing
        Me.cmbPick.ValueMember = Nothing
        Me.cmbPick.WidthDef = 166
        '
        'grpUnso
        '
        Me.grpUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpUnso.Controls.Add(Me.lblSpUnsoNm)
        Me.grpUnso.Controls.Add(Me.txtSpUnsoBrCd)
        Me.grpUnso.Controls.Add(Me.lblSpUnso)
        Me.grpUnso.Controls.Add(Me.txtSpUnsoCd)
        Me.grpUnso.EnableStatus = False
        Me.grpUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpUnso.HeightDef = 46
        Me.grpUnso.Location = New System.Drawing.Point(95, 486)
        Me.grpUnso.Name = "grpUnso"
        Me.grpUnso.Size = New System.Drawing.Size(975, 46)
        Me.grpUnso.TabIndex = 420
        Me.grpUnso.TabStop = False
        Me.grpUnso.Text = "指定運送会社"
        Me.grpUnso.TextValue = "指定運送会社"
        Me.grpUnso.WidthDef = 975
        '
        'lblSpUnsoNm
        '
        Me.lblSpUnsoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpUnsoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpUnsoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpUnsoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSpUnsoNm.CountWrappedLine = False
        Me.lblSpUnsoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSpUnsoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpUnsoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpUnsoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpUnsoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpUnsoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSpUnsoNm.HeightDef = 18
        Me.lblSpUnsoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpUnsoNm.HissuLabelVisible = False
        Me.lblSpUnsoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSpUnsoNm.IsByteCheck = 0
        Me.lblSpUnsoNm.IsCalendarCheck = False
        Me.lblSpUnsoNm.IsDakutenCheck = False
        Me.lblSpUnsoNm.IsEisuCheck = False
        Me.lblSpUnsoNm.IsForbiddenWordsCheck = False
        Me.lblSpUnsoNm.IsFullByteCheck = 0
        Me.lblSpUnsoNm.IsHankakuCheck = False
        Me.lblSpUnsoNm.IsHissuCheck = False
        Me.lblSpUnsoNm.IsKanaCheck = False
        Me.lblSpUnsoNm.IsMiddleSpace = False
        Me.lblSpUnsoNm.IsNumericCheck = False
        Me.lblSpUnsoNm.IsSujiCheck = False
        Me.lblSpUnsoNm.IsZenkakuCheck = False
        Me.lblSpUnsoNm.ItemName = ""
        Me.lblSpUnsoNm.LineSpace = 0
        Me.lblSpUnsoNm.Location = New System.Drawing.Point(182, 19)
        Me.lblSpUnsoNm.MaxLength = 0
        Me.lblSpUnsoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSpUnsoNm.MaxLineCount = 0
        Me.lblSpUnsoNm.Multiline = False
        Me.lblSpUnsoNm.Name = "lblSpUnsoNm"
        Me.lblSpUnsoNm.ReadOnly = True
        Me.lblSpUnsoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSpUnsoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSpUnsoNm.Size = New System.Drawing.Size(782, 18)
        Me.lblSpUnsoNm.TabIndex = 345
        Me.lblSpUnsoNm.TabStop = False
        Me.lblSpUnsoNm.TabStopSetting = False
        Me.lblSpUnsoNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSpUnsoNm.UseSystemPasswordChar = False
        Me.lblSpUnsoNm.WidthDef = 782
        Me.lblSpUnsoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSpUnsoBrCd
        '
        Me.txtSpUnsoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSpUnsoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSpUnsoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpUnsoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSpUnsoBrCd.CountWrappedLine = False
        Me.txtSpUnsoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSpUnsoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSpUnsoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSpUnsoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSpUnsoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSpUnsoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSpUnsoBrCd.HeightDef = 18
        Me.txtSpUnsoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSpUnsoBrCd.HissuLabelVisible = False
        Me.txtSpUnsoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSpUnsoBrCd.IsByteCheck = 3
        Me.txtSpUnsoBrCd.IsCalendarCheck = False
        Me.txtSpUnsoBrCd.IsDakutenCheck = False
        Me.txtSpUnsoBrCd.IsEisuCheck = False
        Me.txtSpUnsoBrCd.IsForbiddenWordsCheck = False
        Me.txtSpUnsoBrCd.IsFullByteCheck = 0
        Me.txtSpUnsoBrCd.IsHankakuCheck = False
        Me.txtSpUnsoBrCd.IsHissuCheck = False
        Me.txtSpUnsoBrCd.IsKanaCheck = False
        Me.txtSpUnsoBrCd.IsMiddleSpace = False
        Me.txtSpUnsoBrCd.IsNumericCheck = False
        Me.txtSpUnsoBrCd.IsSujiCheck = False
        Me.txtSpUnsoBrCd.IsZenkakuCheck = False
        Me.txtSpUnsoBrCd.ItemName = ""
        Me.txtSpUnsoBrCd.LineSpace = 0
        Me.txtSpUnsoBrCd.Location = New System.Drawing.Point(153, 19)
        Me.txtSpUnsoBrCd.MaxLength = 3
        Me.txtSpUnsoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSpUnsoBrCd.MaxLineCount = 0
        Me.txtSpUnsoBrCd.Multiline = False
        Me.txtSpUnsoBrCd.Name = "txtSpUnsoBrCd"
        Me.txtSpUnsoBrCd.ReadOnly = False
        Me.txtSpUnsoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSpUnsoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSpUnsoBrCd.Size = New System.Drawing.Size(45, 18)
        Me.txtSpUnsoBrCd.TabIndex = 342
        Me.txtSpUnsoBrCd.TabStopSetting = True
        Me.txtSpUnsoBrCd.TextValue = "XXX"
        Me.txtSpUnsoBrCd.UseSystemPasswordChar = False
        Me.txtSpUnsoBrCd.WidthDef = 45
        Me.txtSpUnsoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSpUnso
        '
        Me.lblSpUnso.AutoSize = True
        Me.lblSpUnso.AutoSizeDef = True
        Me.lblSpUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSpUnso.EnableStatus = False
        Me.lblSpUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpUnso.HeightDef = 13
        Me.lblSpUnso.Location = New System.Drawing.Point(7, 21)
        Me.lblSpUnso.MinimumSize = New System.Drawing.Size(77, 0)
        Me.lblSpUnso.Name = "lblSpUnso"
        Me.lblSpUnso.Size = New System.Drawing.Size(77, 13)
        Me.lblSpUnso.TabIndex = 342
        Me.lblSpUnso.Text = "会社"
        Me.lblSpUnso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSpUnso.TextValue = "会社"
        Me.lblSpUnso.WidthDef = 77
        '
        'txtSpUnsoCd
        '
        Me.txtSpUnsoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSpUnsoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSpUnsoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpUnsoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSpUnsoCd.CountWrappedLine = False
        Me.txtSpUnsoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSpUnsoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSpUnsoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSpUnsoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSpUnsoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSpUnsoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSpUnsoCd.HeightDef = 18
        Me.txtSpUnsoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSpUnsoCd.HissuLabelVisible = False
        Me.txtSpUnsoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSpUnsoCd.IsByteCheck = 5
        Me.txtSpUnsoCd.IsCalendarCheck = False
        Me.txtSpUnsoCd.IsDakutenCheck = False
        Me.txtSpUnsoCd.IsEisuCheck = False
        Me.txtSpUnsoCd.IsForbiddenWordsCheck = False
        Me.txtSpUnsoCd.IsFullByteCheck = 0
        Me.txtSpUnsoCd.IsHankakuCheck = False
        Me.txtSpUnsoCd.IsHissuCheck = False
        Me.txtSpUnsoCd.IsKanaCheck = False
        Me.txtSpUnsoCd.IsMiddleSpace = False
        Me.txtSpUnsoCd.IsNumericCheck = False
        Me.txtSpUnsoCd.IsSujiCheck = False
        Me.txtSpUnsoCd.IsZenkakuCheck = False
        Me.txtSpUnsoCd.ItemName = ""
        Me.txtSpUnsoCd.LineSpace = 0
        Me.txtSpUnsoCd.Location = New System.Drawing.Point(87, 19)
        Me.txtSpUnsoCd.MaxLength = 5
        Me.txtSpUnsoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSpUnsoCd.MaxLineCount = 0
        Me.txtSpUnsoCd.Multiline = False
        Me.txtSpUnsoCd.Name = "txtSpUnsoCd"
        Me.txtSpUnsoCd.ReadOnly = False
        Me.txtSpUnsoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSpUnsoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSpUnsoCd.Size = New System.Drawing.Size(82, 18)
        Me.txtSpUnsoCd.TabIndex = 344
        Me.txtSpUnsoCd.TabStopSetting = True
        Me.txtSpUnsoCd.TextValue = "XXXXX"
        Me.txtSpUnsoCd.UseSystemPasswordChar = False
        Me.txtSpUnsoCd.WidthDef = 82
        Me.txtSpUnsoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1127, 283)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 382
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
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1047, 315)
        Me.LmTitleLabel7.MinimumSize = New System.Drawing.Size(70, 0)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(70, 18)
        Me.LmTitleLabel7.TabIndex = 381
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 70
        '
        'lblCoaYn
        '
        Me.lblCoaYn.AutoSize = True
        Me.lblCoaYn.AutoSizeDef = True
        Me.lblCoaYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCoaYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCoaYn.EnableStatus = False
        Me.lblCoaYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoaYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoaYn.HeightDef = 13
        Me.lblCoaYn.Location = New System.Drawing.Point(607, 466)
        Me.lblCoaYn.MinimumSize = New System.Drawing.Size(180, 0)
        Me.lblCoaYn.Name = "lblCoaYn"
        Me.lblCoaYn.Size = New System.Drawing.Size(180, 13)
        Me.lblCoaYn.TabIndex = 418
        Me.lblCoaYn.Text = "分析票添付区分"
        Me.lblCoaYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCoaYn.TextValue = "分析票添付区分"
        Me.lblCoaYn.WidthDef = 180
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1120, 316)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 383
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFax.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFax.CountWrappedLine = False
        Me.txtFax.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFax.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFax.HeightDef = 18
        Me.txtFax.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFax.HissuLabelVisible = False
        Me.txtFax.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtFax.IsByteCheck = 20
        Me.txtFax.IsCalendarCheck = False
        Me.txtFax.IsDakutenCheck = False
        Me.txtFax.IsEisuCheck = False
        Me.txtFax.IsForbiddenWordsCheck = False
        Me.txtFax.IsFullByteCheck = 0
        Me.txtFax.IsHankakuCheck = False
        Me.txtFax.IsHissuCheck = False
        Me.txtFax.IsKanaCheck = False
        Me.txtFax.IsMiddleSpace = False
        Me.txtFax.IsNumericCheck = False
        Me.txtFax.IsSujiCheck = False
        Me.txtFax.IsZenkakuCheck = False
        Me.txtFax.ItemName = ""
        Me.txtFax.LineSpace = 0
        Me.txtFax.Location = New System.Drawing.Point(182, 464)
        Me.txtFax.MaxLength = 20
        Me.txtFax.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFax.MaxLineCount = 0
        Me.txtFax.Multiline = False
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = False
        Me.txtFax.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFax.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFax.Size = New System.Drawing.Size(166, 18)
        Me.txtFax.TabIndex = 417
        Me.txtFax.TabStopSetting = True
        Me.txtFax.TextValue = ""
        Me.txtFax.UseSystemPasswordChar = False
        Me.txtFax.WidthDef = 166
        Me.txtFax.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1047, 338)
        Me.LmTitleLabel8.MinimumSize = New System.Drawing.Size(70, 0)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(70, 18)
        Me.LmTitleLabel8.TabIndex = 384
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 70
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1120, 338)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 385
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtTel.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
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
        Me.txtTel.Location = New System.Drawing.Point(182, 444)
        Me.txtTel.MaxLength = 20
        Me.txtTel.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTel.MaxLineCount = 0
        Me.txtTel.Multiline = False
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = False
        Me.txtTel.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTel.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTel.Size = New System.Drawing.Size(166, 18)
        Me.txtTel.TabIndex = 416
        Me.txtTel.TabStopSetting = True
        Me.txtTel.TextValue = "12345678901234567891"
        Me.txtTel.UseSystemPasswordChar = False
        Me.txtTel.WidthDef = 166
        Me.txtTel.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1047, 360)
        Me.LmTitleLabel9.MinimumSize = New System.Drawing.Size(70, 0)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(70, 18)
        Me.LmTitleLabel9.TabIndex = 386
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 70
        '
        'txtCustDestCd
        '
        Me.txtCustDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustDestCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustDestCd.CountWrappedLine = False
        Me.txtCustDestCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustDestCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustDestCd.HeightDef = 18
        Me.txtCustDestCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustDestCd.HissuLabelVisible = False
        Me.txtCustDestCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtCustDestCd.IsByteCheck = 15
        Me.txtCustDestCd.IsCalendarCheck = False
        Me.txtCustDestCd.IsDakutenCheck = False
        Me.txtCustDestCd.IsEisuCheck = False
        Me.txtCustDestCd.IsForbiddenWordsCheck = False
        Me.txtCustDestCd.IsFullByteCheck = 0
        Me.txtCustDestCd.IsHankakuCheck = False
        Me.txtCustDestCd.IsHissuCheck = False
        Me.txtCustDestCd.IsKanaCheck = False
        Me.txtCustDestCd.IsMiddleSpace = False
        Me.txtCustDestCd.IsNumericCheck = False
        Me.txtCustDestCd.IsSujiCheck = False
        Me.txtCustDestCd.IsZenkakuCheck = False
        Me.txtCustDestCd.ItemName = ""
        Me.txtCustDestCd.LineSpace = 0
        Me.txtCustDestCd.Location = New System.Drawing.Point(182, 424)
        Me.txtCustDestCd.MaxLength = 15
        Me.txtCustDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustDestCd.MaxLineCount = 0
        Me.txtCustDestCd.Multiline = False
        Me.txtCustDestCd.Name = "txtCustDestCd"
        Me.txtCustDestCd.ReadOnly = False
        Me.txtCustDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustDestCd.Size = New System.Drawing.Size(166, 18)
        Me.txtCustDestCd.TabIndex = 414
        Me.txtCustDestCd.TabStopSetting = True
        Me.txtCustDestCd.TextValue = ""
        Me.txtCustDestCd.UseSystemPasswordChar = False
        Me.txtCustDestCd.WidthDef = 166
        Me.txtCustDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1120, 360)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 387
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
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1047, 382)
        Me.LmTitleLabel10.MinimumSize = New System.Drawing.Size(70, 0)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(70, 18)
        Me.LmTitleLabel10.TabIndex = 388
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 70
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1120, 382)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 389
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtAd3
        '
        Me.txtAd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd3.CountWrappedLine = False
        Me.txtAd3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd3.HeightDef = 18
        Me.txtAd3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd3.HissuLabelVisible = False
        Me.txtAd3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd3.IsByteCheck = 80
        Me.txtAd3.IsCalendarCheck = False
        Me.txtAd3.IsDakutenCheck = False
        Me.txtAd3.IsEisuCheck = False
        Me.txtAd3.IsForbiddenWordsCheck = False
        Me.txtAd3.IsFullByteCheck = 0
        Me.txtAd3.IsHankakuCheck = False
        Me.txtAd3.IsHissuCheck = False
        Me.txtAd3.IsKanaCheck = False
        Me.txtAd3.IsMiddleSpace = False
        Me.txtAd3.IsNumericCheck = False
        Me.txtAd3.IsSujiCheck = False
        Me.txtAd3.IsZenkakuCheck = False
        Me.txtAd3.ItemName = ""
        Me.txtAd3.LineSpace = 0
        Me.txtAd3.Location = New System.Drawing.Point(417, 403)
        Me.txtAd3.MaxLength = 80
        Me.txtAd3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd3.MaxLineCount = 0
        Me.txtAd3.Multiline = False
        Me.txtAd3.Name = "txtAd3"
        Me.txtAd3.ReadOnly = False
        Me.txtAd3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd3.Size = New System.Drawing.Size(319, 18)
        Me.txtAd3.TabIndex = 411
        Me.txtAd3.TabStopSetting = True
        Me.txtAd3.TextValue = ""
        Me.txtAd3.UseSystemPasswordChar = False
        Me.txtAd3.WidthDef = 319
        Me.txtAd3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtAd2
        '
        Me.txtAd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd2.CountWrappedLine = False
        Me.txtAd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd2.HeightDef = 18
        Me.txtAd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd2.HissuLabelVisible = False
        Me.txtAd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd2.IsByteCheck = 40
        Me.txtAd2.IsCalendarCheck = False
        Me.txtAd2.IsDakutenCheck = False
        Me.txtAd2.IsEisuCheck = False
        Me.txtAd2.IsForbiddenWordsCheck = False
        Me.txtAd2.IsFullByteCheck = 0
        Me.txtAd2.IsHankakuCheck = False
        Me.txtAd2.IsHissuCheck = False
        Me.txtAd2.IsKanaCheck = False
        Me.txtAd2.IsMiddleSpace = False
        Me.txtAd2.IsNumericCheck = False
        Me.txtAd2.IsSujiCheck = False
        Me.txtAd2.IsZenkakuCheck = False
        Me.txtAd2.ItemName = ""
        Me.txtAd2.LineSpace = 0
        Me.txtAd2.Location = New System.Drawing.Point(417, 383)
        Me.txtAd2.MaxLength = 40
        Me.txtAd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd2.MaxLineCount = 0
        Me.txtAd2.Multiline = False
        Me.txtAd2.Name = "txtAd2"
        Me.txtAd2.ReadOnly = False
        Me.txtAd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd2.Size = New System.Drawing.Size(319, 18)
        Me.txtAd2.TabIndex = 409
        Me.txtAd2.TabStopSetting = True
        Me.txtAd2.TextValue = ""
        Me.txtAd2.UseSystemPasswordChar = False
        Me.txtAd2.WidthDef = 319
        Me.txtAd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblAd2
        '
        Me.lblAd2.AutoSize = True
        Me.lblAd2.AutoSizeDef = True
        Me.lblAd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblAd2.EnableStatus = False
        Me.lblAd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd2.HeightDef = 13
        Me.lblAd2.Location = New System.Drawing.Point(336, 386)
        Me.lblAd2.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblAd2.Name = "lblAd2"
        Me.lblAd2.Size = New System.Drawing.Size(78, 13)
        Me.lblAd2.TabIndex = 408
        Me.lblAd2.Text = "住所2"
        Me.lblAd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAd2.TextValue = "住所2"
        Me.lblAd2.WidthDef = 78
        '
        'txtAd1
        '
        Me.txtAd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd1.CountWrappedLine = False
        Me.txtAd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd1.HeightDef = 18
        Me.txtAd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd1.HissuLabelVisible = False
        Me.txtAd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd1.IsByteCheck = 40
        Me.txtAd1.IsCalendarCheck = False
        Me.txtAd1.IsDakutenCheck = False
        Me.txtAd1.IsEisuCheck = False
        Me.txtAd1.IsForbiddenWordsCheck = False
        Me.txtAd1.IsFullByteCheck = 0
        Me.txtAd1.IsHankakuCheck = False
        Me.txtAd1.IsHissuCheck = False
        Me.txtAd1.IsKanaCheck = False
        Me.txtAd1.IsMiddleSpace = False
        Me.txtAd1.IsNumericCheck = False
        Me.txtAd1.IsSujiCheck = False
        Me.txtAd1.IsZenkakuCheck = False
        Me.txtAd1.ItemName = ""
        Me.txtAd1.LineSpace = 0
        Me.txtAd1.Location = New System.Drawing.Point(417, 363)
        Me.txtAd1.MaxLength = 40
        Me.txtAd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd1.MaxLineCount = 0
        Me.txtAd1.Multiline = False
        Me.txtAd1.Name = "txtAd1"
        Me.txtAd1.ReadOnly = False
        Me.txtAd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd1.Size = New System.Drawing.Size(319, 18)
        Me.txtAd1.TabIndex = 407
        Me.txtAd1.TabStopSetting = True
        Me.txtAd1.TextValue = ""
        Me.txtAd1.UseSystemPasswordChar = False
        Me.txtAd1.WidthDef = 319
        Me.txtAd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblAd1
        '
        Me.lblAd1.AutoSize = True
        Me.lblAd1.AutoSizeDef = True
        Me.lblAd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAd1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblAd1.EnableStatus = False
        Me.lblAd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAd1.HeightDef = 13
        Me.lblAd1.Location = New System.Drawing.Point(336, 366)
        Me.lblAd1.MinimumSize = New System.Drawing.Size(78, 0)
        Me.lblAd1.Name = "lblAd1"
        Me.lblAd1.Size = New System.Drawing.Size(78, 13)
        Me.lblAd1.TabIndex = 406
        Me.lblAd1.Text = "住所1"
        Me.lblAd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAd1.TextValue = "住所1"
        Me.lblAd1.WidthDef = 78
        '
        'lblDestCd
        '
        Me.lblDestCd.AutoSize = True
        Me.lblDestCd.AutoSizeDef = True
        Me.lblDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblDestCd.EnableStatus = False
        Me.lblDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestCd.HeightDef = 13
        Me.lblDestCd.Location = New System.Drawing.Point(10, 325)
        Me.lblDestCd.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblDestCd.Name = "lblDestCd"
        Me.lblDestCd.Size = New System.Drawing.Size(167, 13)
        Me.lblDestCd.TabIndex = 404
        Me.lblDestCd.Text = "届先コード"
        Me.lblDestCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDestCd.TextValue = "届先コード"
        Me.lblDestCd.WidthDef = 167
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
        Me.txtDestCd.Location = New System.Drawing.Point(182, 323)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(133, 18)
        Me.txtDestCd.TabIndex = 405
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = ""
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 133
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDeliAtt
        '
        Me.txtDeliAtt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDeliAtt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDeliAtt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliAtt.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtDeliAtt.CountWrappedLine = False
        Me.txtDeliAtt.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDeliAtt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDeliAtt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDeliAtt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDeliAtt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDeliAtt.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDeliAtt.HeightDef = 18
        Me.txtDeliAtt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDeliAtt.HissuLabelVisible = False
        Me.txtDeliAtt.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDeliAtt.IsByteCheck = 100
        Me.txtDeliAtt.IsCalendarCheck = False
        Me.txtDeliAtt.IsDakutenCheck = False
        Me.txtDeliAtt.IsEisuCheck = False
        Me.txtDeliAtt.IsForbiddenWordsCheck = False
        Me.txtDeliAtt.IsFullByteCheck = 0
        Me.txtDeliAtt.IsHankakuCheck = False
        Me.txtDeliAtt.IsHissuCheck = False
        Me.txtDeliAtt.IsKanaCheck = False
        Me.txtDeliAtt.IsMiddleSpace = False
        Me.txtDeliAtt.IsNumericCheck = False
        Me.txtDeliAtt.IsSujiCheck = False
        Me.txtDeliAtt.IsZenkakuCheck = False
        Me.txtDeliAtt.ItemName = ""
        Me.txtDeliAtt.LineSpace = 0
        Me.txtDeliAtt.Location = New System.Drawing.Point(182, 557)
        Me.txtDeliAtt.MaxLength = 100
        Me.txtDeliAtt.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDeliAtt.MaxLineCount = 0
        Me.txtDeliAtt.Multiline = False
        Me.txtDeliAtt.Name = "txtDeliAtt"
        Me.txtDeliAtt.ReadOnly = False
        Me.txtDeliAtt.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDeliAtt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDeliAtt.Size = New System.Drawing.Size(911, 18)
        Me.txtDeliAtt.TabIndex = 403
        Me.txtDeliAtt.TabStopSetting = True
        Me.txtDeliAtt.TextValue = ""
        Me.txtDeliAtt.UseSystemPasswordChar = False
        Me.txtDeliAtt.WidthDef = 911
        Me.txtDeliAtt.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtZip
        '
        Me.txtZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZip.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZip.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtZip.CountWrappedLine = False
        Me.txtZip.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZip.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtZip.HeightDef = 18
        Me.txtZip.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtZip.HissuLabelVisible = False
        Me.txtZip.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtZip.IsByteCheck = 7
        Me.txtZip.IsCalendarCheck = False
        Me.txtZip.IsDakutenCheck = False
        Me.txtZip.IsEisuCheck = False
        Me.txtZip.IsForbiddenWordsCheck = False
        Me.txtZip.IsFullByteCheck = 0
        Me.txtZip.IsHankakuCheck = False
        Me.txtZip.IsHissuCheck = False
        Me.txtZip.IsKanaCheck = False
        Me.txtZip.IsMiddleSpace = False
        Me.txtZip.IsNumericCheck = False
        Me.txtZip.IsSujiCheck = False
        Me.txtZip.IsZenkakuCheck = False
        Me.txtZip.ItemName = ""
        Me.txtZip.LineSpace = 0
        Me.txtZip.Location = New System.Drawing.Point(182, 363)
        Me.txtZip.MaxLength = 7
        Me.txtZip.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtZip.MaxLineCount = 0
        Me.txtZip.Multiline = False
        Me.txtZip.Name = "txtZip"
        Me.txtZip.ReadOnly = False
        Me.txtZip.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtZip.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtZip.Size = New System.Drawing.Size(97, 18)
        Me.txtZip.TabIndex = 401
        Me.txtZip.TabStopSetting = True
        Me.txtZip.TextValue = "1"
        Me.txtZip.UseSystemPasswordChar = False
        Me.txtZip.WidthDef = 97
        Me.txtZip.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestNm
        '
        Me.txtDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestNm.CountWrappedLine = False
        Me.txtDestNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestNm.HeightDef = 18
        Me.txtDestNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNm.HissuLabelVisible = True
        Me.txtDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestNm.IsByteCheck = 80
        Me.txtDestNm.IsCalendarCheck = False
        Me.txtDestNm.IsDakutenCheck = False
        Me.txtDestNm.IsEisuCheck = False
        Me.txtDestNm.IsForbiddenWordsCheck = False
        Me.txtDestNm.IsFullByteCheck = 0
        Me.txtDestNm.IsHankakuCheck = False
        Me.txtDestNm.IsHissuCheck = True
        Me.txtDestNm.IsKanaCheck = False
        Me.txtDestNm.IsMiddleSpace = False
        Me.txtDestNm.IsNumericCheck = False
        Me.txtDestNm.IsSujiCheck = False
        Me.txtDestNm.IsZenkakuCheck = False
        Me.txtDestNm.ItemName = ""
        Me.txtDestNm.LineSpace = 0
        Me.txtDestNm.Location = New System.Drawing.Point(182, 343)
        Me.txtDestNm.MaxLength = 80
        Me.txtDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestNm.MaxLineCount = 0
        Me.txtDestNm.Multiline = False
        Me.txtDestNm.Name = "txtDestNm"
        Me.txtDestNm.ReadOnly = False
        Me.txtDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestNm.Size = New System.Drawing.Size(554, 18)
        Me.txtDestNm.TabIndex = 400
        Me.txtDestNm.TabStopSetting = True
        Me.txtDestNm.TextValue = ""
        Me.txtDestNm.UseSystemPasswordChar = False
        Me.txtDestNm.WidthDef = 554
        Me.txtDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblDeliAtt
        '
        Me.lblDeliAtt.AutoSize = True
        Me.lblDeliAtt.AutoSizeDef = True
        Me.lblDeliAtt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDeliAtt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDeliAtt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblDeliAtt.EnableStatus = False
        Me.lblDeliAtt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeliAtt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeliAtt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDeliAtt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDeliAtt.HeightDef = 13
        Me.lblDeliAtt.Location = New System.Drawing.Point(10, 560)
        Me.lblDeliAtt.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblDeliAtt.Name = "lblDeliAtt"
        Me.lblDeliAtt.Size = New System.Drawing.Size(167, 13)
        Me.lblDeliAtt.TabIndex = 399
        Me.lblDeliAtt.Text = "配送時注意事項"
        Me.lblDeliAtt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDeliAtt.TextValue = "配送時注意事項"
        Me.lblDeliAtt.WidthDef = 167
        '
        'lblDicDestCd
        '
        Me.lblDicDestCd.AutoSize = True
        Me.lblDicDestCd.AutoSizeDef = True
        Me.lblDicDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDicDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDicDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblDicDestCd.EnableStatus = False
        Me.lblDicDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDicDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDicDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDicDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDicDestCd.HeightDef = 13
        Me.lblDicDestCd.Location = New System.Drawing.Point(10, 426)
        Me.lblDicDestCd.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblDicDestCd.Name = "lblDicDestCd"
        Me.lblDicDestCd.Size = New System.Drawing.Size(167, 13)
        Me.lblDicDestCd.TabIndex = 398
        Me.lblDicDestCd.Text = "顧客運賃纏めコード"
        Me.lblDicDestCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDicDestCd.TextValue = "顧客運賃纏めコード"
        Me.lblDicDestCd.WidthDef = 167
        '
        'lblCustL
        '
        Me.lblCustL.AutoSize = True
        Me.lblCustL.AutoSizeDef = True
        Me.lblCustL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCustL.EnableStatus = False
        Me.lblCustL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustL.HeightDef = 13
        Me.lblCustL.Location = New System.Drawing.Point(10, 305)
        Me.lblCustL.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblCustL.Name = "lblCustL"
        Me.lblCustL.Size = New System.Drawing.Size(167, 13)
        Me.lblCustL.TabIndex = 397
        Me.lblCustL.Text = "荷主"
        Me.lblCustL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCustL.TextValue = "荷主"
        Me.lblCustL.WidthDef = 167
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
        Me.lblCustNmL.Location = New System.Drawing.Point(263, 303)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 396
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
        Me.txtCustCdL.Location = New System.Drawing.Point(182, 303)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 395
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "X1"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSpNhsKb
        '
        Me.lblSpNhsKb.AutoSize = True
        Me.lblSpNhsKb.AutoSizeDef = True
        Me.lblSpNhsKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpNhsKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSpNhsKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSpNhsKb.EnableStatus = False
        Me.lblSpNhsKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpNhsKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSpNhsKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpNhsKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpNhsKb.HeightDef = 13
        Me.lblSpNhsKb.Location = New System.Drawing.Point(607, 447)
        Me.lblSpNhsKb.MinimumSize = New System.Drawing.Size(180, 0)
        Me.lblSpNhsKb.Name = "lblSpNhsKb"
        Me.lblSpNhsKb.Size = New System.Drawing.Size(180, 13)
        Me.lblSpNhsKb.TabIndex = 394
        Me.lblSpNhsKb.Text = "指定納品書添付区分"
        Me.lblSpNhsKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSpNhsKb.TextValue = "指定納品書添付区分"
        Me.lblSpNhsKb.WidthDef = 180
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
        Me.lblTel.Location = New System.Drawing.Point(10, 446)
        Me.lblTel.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblTel.Name = "lblTel"
        Me.lblTel.Size = New System.Drawing.Size(167, 13)
        Me.lblTel.TabIndex = 390
        Me.lblTel.Text = "電話番号"
        Me.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTel.TextValue = "電話番号"
        Me.lblTel.WidthDef = 167
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.AutoSizeDef = True
        Me.lblZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZip.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblZip.EnableStatus = False
        Me.lblZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZip.HeightDef = 13
        Me.lblZip.Location = New System.Drawing.Point(10, 365)
        Me.lblZip.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(167, 13)
        Me.lblZip.TabIndex = 392
        Me.lblZip.Text = "郵便番号"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblZip.TextValue = "郵便番号"
        Me.lblZip.WidthDef = 167
        '
        'lblDestNm
        '
        Me.lblDestNm.AutoSize = True
        Me.lblDestNm.AutoSizeDef = True
        Me.lblDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblDestNm.EnableStatus = False
        Me.lblDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.HeightDef = 13
        Me.lblDestNm.Location = New System.Drawing.Point(10, 345)
        Me.lblDestNm.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.Size = New System.Drawing.Size(167, 13)
        Me.lblDestNm.TabIndex = 391
        Me.lblDestNm.Text = "届先名"
        Me.lblDestNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDestNm.TextValue = "届先名"
        Me.lblDestNm.WidthDef = 167
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(182, 283)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 460
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'lblEigyosyo
        '
        Me.lblEigyosyo.AutoSize = True
        Me.lblEigyosyo.AutoSizeDef = True
        Me.lblEigyosyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyosyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyosyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblEigyosyo.EnableStatus = False
        Me.lblEigyosyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyosyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyosyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyosyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyosyo.HeightDef = 13
        Me.lblEigyosyo.Location = New System.Drawing.Point(10, 285)
        Me.lblEigyosyo.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblEigyosyo.Name = "lblEigyosyo"
        Me.lblEigyosyo.Size = New System.Drawing.Size(167, 13)
        Me.lblEigyosyo.TabIndex = 459
        Me.lblEigyosyo.Text = "営業所"
        Me.lblEigyosyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEigyosyo.TextValue = "営業所"
        Me.lblEigyosyo.WidthDef = 167
        '
        'lblTariffBunruiKbn
        '
        Me.lblTariffBunruiKbn.AutoSize = True
        Me.lblTariffBunruiKbn.AutoSizeDef = True
        Me.lblTariffBunruiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffBunruiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffBunruiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTariffBunruiKbn.EnableStatus = False
        Me.lblTariffBunruiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffBunruiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffBunruiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffBunruiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffBunruiKbn.HeightDef = 13
        Me.lblTariffBunruiKbn.Location = New System.Drawing.Point(798, 660)
        Me.lblTariffBunruiKbn.MinimumSize = New System.Drawing.Size(135, 0)
        Me.lblTariffBunruiKbn.Name = "lblTariffBunruiKbn"
        Me.lblTariffBunruiKbn.Size = New System.Drawing.Size(135, 13)
        Me.lblTariffBunruiKbn.TabIndex = 465
        Me.lblTariffBunruiKbn.Text = "タリフ分類区分"
        Me.lblTariffBunruiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTariffBunruiKbn.TextValue = "タリフ分類区分"
        Me.lblTariffBunruiKbn.WidthDef = 135
        '
        'cmbLargeCarYn
        '
        Me.cmbLargeCarYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLargeCarYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLargeCarYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbLargeCarYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbLargeCarYn.DataCode = "K017"
        Me.cmbLargeCarYn.DataSource = Nothing
        Me.cmbLargeCarYn.DisplayMember = Nothing
        Me.cmbLargeCarYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLargeCarYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLargeCarYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLargeCarYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLargeCarYn.HeightDef = 18
        Me.cmbLargeCarYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLargeCarYn.HissuLabelVisible = True
        Me.cmbLargeCarYn.InsertWildCard = True
        Me.cmbLargeCarYn.IsForbiddenWordsCheck = False
        Me.cmbLargeCarYn.IsHissuCheck = True
        Me.cmbLargeCarYn.ItemName = ""
        Me.cmbLargeCarYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbLargeCarYn.Location = New System.Drawing.Point(1164, 537)
        Me.cmbLargeCarYn.Name = "cmbLargeCarYn"
        Me.cmbLargeCarYn.ReadOnly = False
        Me.cmbLargeCarYn.SelectedIndex = -1
        Me.cmbLargeCarYn.SelectedItem = Nothing
        Me.cmbLargeCarYn.SelectedText = ""
        Me.cmbLargeCarYn.SelectedValue = ""
        Me.cmbLargeCarYn.Size = New System.Drawing.Size(80, 18)
        Me.cmbLargeCarYn.TabIndex = 468
        Me.cmbLargeCarYn.TabStopSetting = True
        Me.cmbLargeCarYn.TextValue = ""
        Me.cmbLargeCarYn.Value1 = Nothing
        Me.cmbLargeCarYn.Value2 = Nothing
        Me.cmbLargeCarYn.Value3 = Nothing
        Me.cmbLargeCarYn.ValueMember = Nothing
        Me.cmbLargeCarYn.WidthDef = 80
        '
        'lblLargeCarYn
        '
        Me.lblLargeCarYn.AutoSize = True
        Me.lblLargeCarYn.AutoSizeDef = True
        Me.lblLargeCarYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLargeCarYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLargeCarYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblLargeCarYn.EnableStatus = False
        Me.lblLargeCarYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLargeCarYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLargeCarYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLargeCarYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLargeCarYn.HeightDef = 13
        Me.lblLargeCarYn.Location = New System.Drawing.Point(1080, 540)
        Me.lblLargeCarYn.MinimumSize = New System.Drawing.Size(81, 0)
        Me.lblLargeCarYn.Name = "lblLargeCarYn"
        Me.lblLargeCarYn.Size = New System.Drawing.Size(81, 13)
        Me.lblLargeCarYn.TabIndex = 467
        Me.lblLargeCarYn.Text = "大型車輌"
        Me.lblLargeCarYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLargeCarYn.TextValue = "大型車輌"
        Me.lblLargeCarYn.WidthDef = 81
        '
        'btnRowAdd
        '
        Me.btnRowAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowAdd.EnableStatus = True
        Me.btnRowAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.HeightDef = 22
        Me.btnRowAdd.Location = New System.Drawing.Point(106, 741)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 232
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 70
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
        Me.sprDetail2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail2.HeightDef = 98
        Me.sprDetail2.KeyboardCheckBoxOn = False
        Me.sprDetail2.Location = New System.Drawing.Point(64, 767)
        Me.sprDetail2.Name = "sprDetail2"
        Me.sprDetail2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail2.Size = New System.Drawing.Size(1198, 98)
        Me.sprDetail2.SortColumn = True
        Me.sprDetail2.SpanColumnLock = True
        Me.sprDetail2.SpreadDoubleClicked = False
        Me.sprDetail2.TabIndex = 230
        Me.sprDetail2.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail2.TextValue = Nothing
        Me.sprDetail2.UseGrouping = False
        Me.sprDetail2.WidthDef = 1198
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail2_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
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
        'btnRowDel
        '
        Me.btnRowDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowDel.EnableStatus = True
        Me.btnRowDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.HeightDef = 22
        Me.btnRowDel.Location = New System.Drawing.Point(183, 741)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 231
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1120, 425)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 606
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1120, 404)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 605
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbCoaYn
        '
        Me.cmbCoaYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCoaYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCoaYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCoaYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCoaYn.DataCode = "B005"
        Me.cmbCoaYn.DataSource = Nothing
        Me.cmbCoaYn.DisplayMember = Nothing
        Me.cmbCoaYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCoaYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCoaYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCoaYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCoaYn.HeightDef = 18
        Me.cmbCoaYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCoaYn.HissuLabelVisible = True
        Me.cmbCoaYn.InsertWildCard = True
        Me.cmbCoaYn.IsForbiddenWordsCheck = False
        Me.cmbCoaYn.IsHissuCheck = True
        Me.cmbCoaYn.ItemName = ""
        Me.cmbCoaYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCoaYn.Location = New System.Drawing.Point(790, 464)
        Me.cmbCoaYn.Name = "cmbCoaYn"
        Me.cmbCoaYn.ReadOnly = False
        Me.cmbCoaYn.SelectedIndex = -1
        Me.cmbCoaYn.SelectedItem = Nothing
        Me.cmbCoaYn.SelectedText = ""
        Me.cmbCoaYn.SelectedValue = ""
        Me.cmbCoaYn.Size = New System.Drawing.Size(139, 18)
        Me.cmbCoaYn.TabIndex = 607
        Me.cmbCoaYn.TabStopSetting = True
        Me.cmbCoaYn.TextValue = ""
        Me.cmbCoaYn.Value1 = Nothing
        Me.cmbCoaYn.Value2 = Nothing
        Me.cmbCoaYn.Value3 = Nothing
        Me.cmbCoaYn.ValueMember = Nothing
        Me.cmbCoaYn.WidthDef = 139
        '
        'cmbSpNhsKb
        '
        Me.cmbSpNhsKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSpNhsKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSpNhsKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSpNhsKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSpNhsKb.DataCode = "S013"
        Me.cmbSpNhsKb.DataSource = Nothing
        Me.cmbSpNhsKb.DisplayMember = Nothing
        Me.cmbSpNhsKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSpNhsKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSpNhsKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSpNhsKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSpNhsKb.HeightDef = 18
        Me.cmbSpNhsKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSpNhsKb.HissuLabelVisible = False
        Me.cmbSpNhsKb.InsertWildCard = True
        Me.cmbSpNhsKb.IsForbiddenWordsCheck = False
        Me.cmbSpNhsKb.IsHissuCheck = False
        Me.cmbSpNhsKb.ItemName = ""
        Me.cmbSpNhsKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSpNhsKb.Location = New System.Drawing.Point(790, 444)
        Me.cmbSpNhsKb.Name = "cmbSpNhsKb"
        Me.cmbSpNhsKb.ReadOnly = False
        Me.cmbSpNhsKb.SelectedIndex = -1
        Me.cmbSpNhsKb.SelectedItem = Nothing
        Me.cmbSpNhsKb.SelectedText = ""
        Me.cmbSpNhsKb.SelectedValue = ""
        Me.cmbSpNhsKb.Size = New System.Drawing.Size(139, 18)
        Me.cmbSpNhsKb.TabIndex = 608
        Me.cmbSpNhsKb.TabStopSetting = True
        Me.cmbSpNhsKb.TextValue = ""
        Me.cmbSpNhsKb.Value1 = Nothing
        Me.cmbSpNhsKb.Value2 = Nothing
        Me.cmbSpNhsKb.Value3 = Nothing
        Me.cmbSpNhsKb.ValueMember = Nothing
        Me.cmbSpNhsKb.WidthDef = 139
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
        Me.lblMaxEda.Location = New System.Drawing.Point(1232, 746)
        Me.lblMaxEda.MaxLength = 0
        Me.lblMaxEda.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMaxEda.MaxLineCount = 0
        Me.lblMaxEda.Multiline = False
        Me.lblMaxEda.Name = "lblMaxEda"
        Me.lblMaxEda.ReadOnly = True
        Me.lblMaxEda.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMaxEda.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMaxEda.Size = New System.Drawing.Size(30, 15)
        Me.lblMaxEda.TabIndex = 611
        Me.lblMaxEda.TabStop = False
        Me.lblMaxEda.TabStopSetting = False
        Me.lblMaxEda.TextValue = ""
        Me.lblMaxEda.UseSystemPasswordChar = False
        Me.lblMaxEda.Visible = False
        Me.lblMaxEda.WidthDef = 30
        Me.lblMaxEda.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdDateT
        '
        Me.lblSysUpdDateT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDateT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDateT.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdDateT.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdDateT.CountWrappedLine = False
        Me.lblSysUpdDateT.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdDateT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDateT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDateT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDateT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDateT.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdDateT.HeightDef = 15
        Me.lblSysUpdDateT.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDateT.HissuLabelVisible = False
        Me.lblSysUpdDateT.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdDateT.IsByteCheck = 0
        Me.lblSysUpdDateT.IsCalendarCheck = False
        Me.lblSysUpdDateT.IsDakutenCheck = False
        Me.lblSysUpdDateT.IsEisuCheck = False
        Me.lblSysUpdDateT.IsForbiddenWordsCheck = False
        Me.lblSysUpdDateT.IsFullByteCheck = 0
        Me.lblSysUpdDateT.IsHankakuCheck = False
        Me.lblSysUpdDateT.IsHissuCheck = False
        Me.lblSysUpdDateT.IsKanaCheck = False
        Me.lblSysUpdDateT.IsMiddleSpace = False
        Me.lblSysUpdDateT.IsNumericCheck = False
        Me.lblSysUpdDateT.IsSujiCheck = False
        Me.lblSysUpdDateT.IsZenkakuCheck = False
        Me.lblSysUpdDateT.ItemName = ""
        Me.lblSysUpdDateT.LineSpace = 0
        Me.lblSysUpdDateT.Location = New System.Drawing.Point(1087, 593)
        Me.lblSysUpdDateT.MaxLength = 0
        Me.lblSysUpdDateT.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdDateT.MaxLineCount = 0
        Me.lblSysUpdDateT.Multiline = False
        Me.lblSysUpdDateT.Name = "lblSysUpdDateT"
        Me.lblSysUpdDateT.ReadOnly = True
        Me.lblSysUpdDateT.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdDateT.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdDateT.Size = New System.Drawing.Size(94, 15)
        Me.lblSysUpdDateT.TabIndex = 612
        Me.lblSysUpdDateT.TabStop = False
        Me.lblSysUpdDateT.TabStopSetting = False
        Me.lblSysUpdDateT.TextValue = ""
        Me.lblSysUpdDateT.UseSystemPasswordChar = False
        Me.lblSysUpdDateT.Visible = False
        Me.lblSysUpdDateT.WidthDef = 94
        Me.lblSysUpdDateT.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdTimeT
        '
        Me.lblSysUpdTimeT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTimeT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTimeT.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdTimeT.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdTimeT.CountWrappedLine = False
        Me.lblSysUpdTimeT.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdTimeT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTimeT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTimeT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTimeT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTimeT.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdTimeT.HeightDef = 15
        Me.lblSysUpdTimeT.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTimeT.HissuLabelVisible = False
        Me.lblSysUpdTimeT.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdTimeT.IsByteCheck = 0
        Me.lblSysUpdTimeT.IsCalendarCheck = False
        Me.lblSysUpdTimeT.IsDakutenCheck = False
        Me.lblSysUpdTimeT.IsEisuCheck = False
        Me.lblSysUpdTimeT.IsForbiddenWordsCheck = False
        Me.lblSysUpdTimeT.IsFullByteCheck = 0
        Me.lblSysUpdTimeT.IsHankakuCheck = False
        Me.lblSysUpdTimeT.IsHissuCheck = False
        Me.lblSysUpdTimeT.IsKanaCheck = False
        Me.lblSysUpdTimeT.IsMiddleSpace = False
        Me.lblSysUpdTimeT.IsNumericCheck = False
        Me.lblSysUpdTimeT.IsSujiCheck = False
        Me.lblSysUpdTimeT.IsZenkakuCheck = False
        Me.lblSysUpdTimeT.ItemName = ""
        Me.lblSysUpdTimeT.LineSpace = 0
        Me.lblSysUpdTimeT.Location = New System.Drawing.Point(1087, 613)
        Me.lblSysUpdTimeT.MaxLength = 0
        Me.lblSysUpdTimeT.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdTimeT.MaxLineCount = 0
        Me.lblSysUpdTimeT.Multiline = False
        Me.lblSysUpdTimeT.Name = "lblSysUpdTimeT"
        Me.lblSysUpdTimeT.ReadOnly = True
        Me.lblSysUpdTimeT.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdTimeT.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdTimeT.Size = New System.Drawing.Size(94, 15)
        Me.lblSysUpdTimeT.TabIndex = 613
        Me.lblSysUpdTimeT.TabStop = False
        Me.lblSysUpdTimeT.TabStopSetting = False
        Me.lblSysUpdTimeT.TextValue = ""
        Me.lblSysUpdTimeT.UseSystemPasswordChar = False
        Me.lblSysUpdTimeT.Visible = False
        Me.lblSysUpdTimeT.WidthDef = 94
        Me.lblSysUpdTimeT.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCdM
        '
        Me.lblCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdM.CountWrappedLine = False
        Me.lblCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdM.HeightDef = 15
        Me.lblCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.HissuLabelVisible = False
        Me.lblCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustCdM.IsByteCheck = 0
        Me.lblCustCdM.IsCalendarCheck = False
        Me.lblCustCdM.IsDakutenCheck = False
        Me.lblCustCdM.IsEisuCheck = False
        Me.lblCustCdM.IsForbiddenWordsCheck = False
        Me.lblCustCdM.IsFullByteCheck = 0
        Me.lblCustCdM.IsHankakuCheck = False
        Me.lblCustCdM.IsHissuCheck = False
        Me.lblCustCdM.IsKanaCheck = False
        Me.lblCustCdM.IsMiddleSpace = False
        Me.lblCustCdM.IsNumericCheck = False
        Me.lblCustCdM.IsSujiCheck = False
        Me.lblCustCdM.IsZenkakuCheck = False
        Me.lblCustCdM.ItemName = ""
        Me.lblCustCdM.LineSpace = 0
        Me.lblCustCdM.Location = New System.Drawing.Point(1087, 632)
        Me.lblCustCdM.MaxLength = 0
        Me.lblCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdM.MaxLineCount = 0
        Me.lblCustCdM.Multiline = False
        Me.lblCustCdM.Name = "lblCustCdM"
        Me.lblCustCdM.ReadOnly = True
        Me.lblCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdM.Size = New System.Drawing.Size(94, 15)
        Me.lblCustCdM.TabIndex = 614
        Me.lblCustCdM.TabStop = False
        Me.lblCustCdM.TabStopSetting = False
        Me.lblCustCdM.TextValue = ""
        Me.lblCustCdM.UseSystemPasswordChar = False
        Me.lblCustCdM.Visible = False
        Me.lblCustCdM.WidthDef = 94
        Me.lblCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSetMstCd
        '
        Me.lblSetMstCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetMstCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetMstCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSetMstCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSetMstCd.CountWrappedLine = False
        Me.lblSetMstCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSetMstCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSetMstCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSetMstCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSetMstCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSetMstCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSetMstCd.HeightDef = 15
        Me.lblSetMstCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetMstCd.HissuLabelVisible = False
        Me.lblSetMstCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSetMstCd.IsByteCheck = 0
        Me.lblSetMstCd.IsCalendarCheck = False
        Me.lblSetMstCd.IsDakutenCheck = False
        Me.lblSetMstCd.IsEisuCheck = False
        Me.lblSetMstCd.IsForbiddenWordsCheck = False
        Me.lblSetMstCd.IsFullByteCheck = 0
        Me.lblSetMstCd.IsHankakuCheck = False
        Me.lblSetMstCd.IsHissuCheck = False
        Me.lblSetMstCd.IsKanaCheck = False
        Me.lblSetMstCd.IsMiddleSpace = False
        Me.lblSetMstCd.IsNumericCheck = False
        Me.lblSetMstCd.IsSujiCheck = False
        Me.lblSetMstCd.IsZenkakuCheck = False
        Me.lblSetMstCd.ItemName = ""
        Me.lblSetMstCd.LineSpace = 0
        Me.lblSetMstCd.Location = New System.Drawing.Point(1087, 653)
        Me.lblSetMstCd.MaxLength = 0
        Me.lblSetMstCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSetMstCd.MaxLineCount = 0
        Me.lblSetMstCd.Multiline = False
        Me.lblSetMstCd.Name = "lblSetMstCd"
        Me.lblSetMstCd.ReadOnly = True
        Me.lblSetMstCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSetMstCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSetMstCd.Size = New System.Drawing.Size(94, 15)
        Me.lblSetMstCd.TabIndex = 615
        Me.lblSetMstCd.TabStop = False
        Me.lblSetMstCd.TabStopSetting = False
        Me.lblSetMstCd.TextValue = ""
        Me.lblSetMstCd.UseSystemPasswordChar = False
        Me.lblSetMstCd.Visible = False
        Me.lblSetMstCd.WidthDef = 94
        Me.lblSetMstCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSetKbn
        '
        Me.lblSetKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSetKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSetKbn.CountWrappedLine = False
        Me.lblSetKbn.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSetKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSetKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSetKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSetKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSetKbn.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSetKbn.HeightDef = 15
        Me.lblSetKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSetKbn.HissuLabelVisible = False
        Me.lblSetKbn.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSetKbn.IsByteCheck = 0
        Me.lblSetKbn.IsCalendarCheck = False
        Me.lblSetKbn.IsDakutenCheck = False
        Me.lblSetKbn.IsEisuCheck = False
        Me.lblSetKbn.IsForbiddenWordsCheck = False
        Me.lblSetKbn.IsFullByteCheck = 0
        Me.lblSetKbn.IsHankakuCheck = False
        Me.lblSetKbn.IsHissuCheck = False
        Me.lblSetKbn.IsKanaCheck = False
        Me.lblSetKbn.IsMiddleSpace = False
        Me.lblSetKbn.IsNumericCheck = False
        Me.lblSetKbn.IsSujiCheck = False
        Me.lblSetKbn.IsZenkakuCheck = False
        Me.lblSetKbn.ItemName = ""
        Me.lblSetKbn.LineSpace = 0
        Me.lblSetKbn.Location = New System.Drawing.Point(1087, 674)
        Me.lblSetKbn.MaxLength = 0
        Me.lblSetKbn.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSetKbn.MaxLineCount = 0
        Me.lblSetKbn.Multiline = False
        Me.lblSetKbn.Name = "lblSetKbn"
        Me.lblSetKbn.ReadOnly = True
        Me.lblSetKbn.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSetKbn.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSetKbn.Size = New System.Drawing.Size(94, 15)
        Me.lblSetKbn.TabIndex = 616
        Me.lblSetKbn.TabStop = False
        Me.lblSetKbn.TabStopSetting = False
        Me.lblSetKbn.TextValue = ""
        Me.lblSetKbn.UseSystemPasswordChar = False
        Me.lblSetKbn.Visible = False
        Me.lblSetKbn.WidthDef = 94
        Me.lblSetKbn.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblModeFlg
        '
        Me.lblModeFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblModeFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblModeFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblModeFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblModeFlg.CountWrappedLine = False
        Me.lblModeFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblModeFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblModeFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblModeFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblModeFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblModeFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblModeFlg.HeightDef = 15
        Me.lblModeFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblModeFlg.HissuLabelVisible = False
        Me.lblModeFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblModeFlg.IsByteCheck = 0
        Me.lblModeFlg.IsCalendarCheck = False
        Me.lblModeFlg.IsDakutenCheck = False
        Me.lblModeFlg.IsEisuCheck = False
        Me.lblModeFlg.IsForbiddenWordsCheck = False
        Me.lblModeFlg.IsFullByteCheck = 0
        Me.lblModeFlg.IsHankakuCheck = False
        Me.lblModeFlg.IsHissuCheck = False
        Me.lblModeFlg.IsKanaCheck = False
        Me.lblModeFlg.IsMiddleSpace = False
        Me.lblModeFlg.IsNumericCheck = False
        Me.lblModeFlg.IsSujiCheck = False
        Me.lblModeFlg.IsZenkakuCheck = False
        Me.lblModeFlg.ItemName = ""
        Me.lblModeFlg.LineSpace = 0
        Me.lblModeFlg.Location = New System.Drawing.Point(1241, 449)
        Me.lblModeFlg.MaxLength = 0
        Me.lblModeFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblModeFlg.MaxLineCount = 0
        Me.lblModeFlg.Multiline = False
        Me.lblModeFlg.Name = "lblModeFlg"
        Me.lblModeFlg.ReadOnly = True
        Me.lblModeFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblModeFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblModeFlg.Size = New System.Drawing.Size(30, 15)
        Me.lblModeFlg.TabIndex = 617
        Me.lblModeFlg.TabStop = False
        Me.lblModeFlg.TabStopSetting = False
        Me.lblModeFlg.TextValue = ""
        Me.lblModeFlg.UseSystemPasswordChar = False
        Me.lblModeFlg.Visible = False
        Me.lblModeFlg.WidthDef = 30
        Me.lblModeFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbTariffBunruiKbn
        '
        Me.cmbTariffBunruiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffBunruiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffBunruiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTariffBunruiKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTariffBunruiKbn.DataSource = Nothing
        Me.cmbTariffBunruiKbn.DisplayMember = Nothing
        Me.cmbTariffBunruiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffBunruiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffBunruiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffBunruiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffBunruiKbn.HeightDef = 18
        Me.cmbTariffBunruiKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTariffBunruiKbn.HissuLabelVisible = False
        Me.cmbTariffBunruiKbn.InsertWildCard = False
        Me.cmbTariffBunruiKbn.IsForbiddenWordsCheck = False
        Me.cmbTariffBunruiKbn.IsHissuCheck = False
        Me.cmbTariffBunruiKbn.ItemName = ""
        Me.cmbTariffBunruiKbn.Location = New System.Drawing.Point(936, 657)
        Me.cmbTariffBunruiKbn.Name = "cmbTariffBunruiKbn"
        Me.cmbTariffBunruiKbn.ReadOnly = False
        Me.cmbTariffBunruiKbn.SelectedIndex = -1
        Me.cmbTariffBunruiKbn.SelectedItem = Nothing
        Me.cmbTariffBunruiKbn.SelectedText = ""
        Me.cmbTariffBunruiKbn.SelectedValue = ""
        Me.cmbTariffBunruiKbn.Size = New System.Drawing.Size(123, 18)
        Me.cmbTariffBunruiKbn.TabIndex = 618
        Me.cmbTariffBunruiKbn.TabStopSetting = True
        Me.cmbTariffBunruiKbn.TextValue = ""
        Me.cmbTariffBunruiKbn.ValueMember = Nothing
        Me.cmbTariffBunruiKbn.WidthDef = 123
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
        Me.txtRemark.Location = New System.Drawing.Point(182, 577)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(911, 18)
        Me.txtRemark.TabIndex = 620
        Me.txtRemark.TabStopSetting = True
        Me.txtRemark.TextValue = ""
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 911
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.AutoSizeDef = True
        Me.lblRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblRemark.EnableStatus = False
        Me.lblRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRemark.HeightDef = 13
        Me.lblRemark.Location = New System.Drawing.Point(10, 580)
        Me.lblRemark.MinimumSize = New System.Drawing.Size(167, 0)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(167, 13)
        Me.lblRemark.TabIndex = 619
        Me.lblRemark.Text = "備考"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRemark.TextValue = "備考"
        Me.lblRemark.WidthDef = 167
        '
        'txtKanaNm
        '
        Me.txtKanaNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKanaNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKanaNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKanaNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKanaNm.CountWrappedLine = False
        Me.txtKanaNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKanaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanaNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanaNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanaNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanaNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKanaNm.HeightDef = 18
        Me.txtKanaNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanaNm.HissuLabelVisible = False
        Me.txtKanaNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKanaNm.IsByteCheck = 40
        Me.txtKanaNm.IsCalendarCheck = False
        Me.txtKanaNm.IsDakutenCheck = False
        Me.txtKanaNm.IsEisuCheck = False
        Me.txtKanaNm.IsForbiddenWordsCheck = False
        Me.txtKanaNm.IsFullByteCheck = 0
        Me.txtKanaNm.IsHankakuCheck = False
        Me.txtKanaNm.IsHissuCheck = False
        Me.txtKanaNm.IsKanaCheck = False
        Me.txtKanaNm.IsMiddleSpace = False
        Me.txtKanaNm.IsNumericCheck = False
        Me.txtKanaNm.IsSujiCheck = False
        Me.txtKanaNm.IsZenkakuCheck = False
        Me.txtKanaNm.ItemName = ""
        Me.txtKanaNm.LineSpace = 0
        Me.txtKanaNm.Location = New System.Drawing.Point(790, 343)
        Me.txtKanaNm.MaxLength = 40
        Me.txtKanaNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKanaNm.MaxLineCount = 0
        Me.txtKanaNm.Multiline = False
        Me.txtKanaNm.Name = "txtKanaNm"
        Me.txtKanaNm.ReadOnly = False
        Me.txtKanaNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKanaNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKanaNm.Size = New System.Drawing.Size(269, 18)
        Me.txtKanaNm.TabIndex = 415
        Me.txtKanaNm.TabStopSetting = True
        Me.txtKanaNm.TextValue = ""
        Me.txtKanaNm.UseSystemPasswordChar = False
        Me.txtKanaNm.WidthDef = 269
        Me.txtKanaNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKanaNm
        '
        Me.lblKanaNm.AutoSize = True
        Me.lblKanaNm.AutoSizeDef = True
        Me.lblKanaNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanaNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanaNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKanaNm.EnableStatus = False
        Me.lblKanaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanaNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanaNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanaNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanaNm.HeightDef = 13
        Me.lblKanaNm.Location = New System.Drawing.Point(730, 345)
        Me.lblKanaNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKanaNm.MinimumSize = New System.Drawing.Size(57, 0)
        Me.lblKanaNm.Name = "lblKanaNm"
        Me.lblKanaNm.Size = New System.Drawing.Size(57, 13)
        Me.lblKanaNm.TabIndex = 621
        Me.lblKanaNm.Text = "カナ名"
        Me.lblKanaNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKanaNm.TextValue = "カナ名"
        Me.lblKanaNm.WidthDef = 57
        '
        'txtShiharaiAd
        '
        Me.txtShiharaiAd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiAd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShiharaiAd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShiharaiAd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShiharaiAd.CountWrappedLine = False
        Me.txtShiharaiAd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShiharaiAd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiAd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiharaiAd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiAd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShiharaiAd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShiharaiAd.HeightDef = 18
        Me.txtShiharaiAd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShiharaiAd.HissuLabelVisible = False
        Me.txtShiharaiAd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtShiharaiAd.IsByteCheck = 120
        Me.txtShiharaiAd.IsCalendarCheck = False
        Me.txtShiharaiAd.IsDakutenCheck = False
        Me.txtShiharaiAd.IsEisuCheck = False
        Me.txtShiharaiAd.IsForbiddenWordsCheck = False
        Me.txtShiharaiAd.IsFullByteCheck = 0
        Me.txtShiharaiAd.IsHankakuCheck = False
        Me.txtShiharaiAd.IsHissuCheck = False
        Me.txtShiharaiAd.IsKanaCheck = False
        Me.txtShiharaiAd.IsMiddleSpace = False
        Me.txtShiharaiAd.IsNumericCheck = False
        Me.txtShiharaiAd.IsSujiCheck = False
        Me.txtShiharaiAd.IsZenkakuCheck = False
        Me.txtShiharaiAd.ItemName = ""
        Me.txtShiharaiAd.LineSpace = 0
        Me.txtShiharaiAd.Location = New System.Drawing.Point(417, 423)
        Me.txtShiharaiAd.MaxLength = 120
        Me.txtShiharaiAd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShiharaiAd.MaxLineCount = 0
        Me.txtShiharaiAd.Multiline = False
        Me.txtShiharaiAd.Name = "txtShiharaiAd"
        Me.txtShiharaiAd.ReadOnly = False
        Me.txtShiharaiAd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShiharaiAd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShiharaiAd.Size = New System.Drawing.Size(319, 18)
        Me.txtShiharaiAd.TabIndex = 623
        Me.txtShiharaiAd.TabStopSetting = True
        Me.txtShiharaiAd.TextValue = ""
        Me.txtShiharaiAd.UseSystemPasswordChar = False
        Me.txtShiharaiAd.WidthDef = 319
        Me.txtShiharaiAd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblShiharaiAd
        '
        Me.lblShiharaiAd.AutoSize = True
        Me.lblShiharaiAd.AutoSizeDef = True
        Me.lblShiharaiAd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiAd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShiharaiAd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblShiharaiAd.EnableStatus = False
        Me.lblShiharaiAd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiAd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiharaiAd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiAd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShiharaiAd.HeightDef = 13
        Me.lblShiharaiAd.Location = New System.Drawing.Point(333, 425)
        Me.lblShiharaiAd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShiharaiAd.MinimumSize = New System.Drawing.Size(81, 0)
        Me.lblShiharaiAd.Name = "lblShiharaiAd"
        Me.lblShiharaiAd.Size = New System.Drawing.Size(81, 13)
        Me.lblShiharaiAd.TabIndex = 622
        Me.lblShiharaiAd.Text = "支払用住所"
        Me.lblShiharaiAd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblShiharaiAd.TextValue = "支払用住所"
        Me.lblShiharaiAd.WidthDef = 81
        '
        'LMM040F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM040F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUnso.ResumeLayout(False)
        Me.grpUnso.PerformLayout()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblEDICd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtEDICd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel33 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numKyori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblUnchinSeiqtoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnchinSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnchinSeiqto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtJIS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblKyori As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblJIS As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblYokoTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtYokoTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblYokoTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblExtcTariffRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtExtcTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblExtcTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnchinTariffRem2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnchinTariffCd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnchinTariff2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnchinTariffRem1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnchinTariffCd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnchinTariff1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUriageNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUriageCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUriage As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSales As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSalesNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSalesCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCargoTimeLimit As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCargoTimeLimit As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbMotoChakuKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblMotoChakuKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbBin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblBin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblPick As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPick As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents grpUnso As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblSpUnso As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSpUnsoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSpUnsoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSpUnsoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCoaYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtFax As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTel As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtAd3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtAd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblAd2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblAd1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDestCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDeliAtt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtZip As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDeliAtt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDicDestCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSpNhsKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTel As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZip As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblEigyosyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTariffBunruiKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbLargeCarYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblLargeCarYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents sprDetail2 As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSpNhsKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbCoaYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblAd3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblFax As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblMaxEda As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdDateT As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSetKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSetMstCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdTimeT As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblModeFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbTariffBunruiKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents txtRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblRemark As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKanaNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblKanaNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtShiharaiAd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblShiharaiAd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
