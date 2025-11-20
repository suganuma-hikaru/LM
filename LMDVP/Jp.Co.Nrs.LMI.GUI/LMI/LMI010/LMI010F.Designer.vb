<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI010F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopS

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
        Me.LblTitleNinushi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LblTitleShori = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.WebServiceInterFace1 = New Jp.Co.Nrs.Com.WebServiceProxy.WebServiceInterFace()
        Me.CmbShori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.CmbCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.pnlViewAria.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.CmbCustNm)
        Me.pnlViewAria.Controls.Add(Me.CmbShori)
        Me.pnlViewAria.Controls.Add(Me.LblTitleShori)
        Me.pnlViewAria.Controls.Add(Me.LblTitleNinushi)
        Me.pnlViewAria.Size = New System.Drawing.Size(394, 188)
        '
        'FunctionKey
        '
        Me.FunctionKey.F12ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(56, 1)
        '
        'LblTitleNinushi
        '
        Me.LblTitleNinushi.AutoSizeDef = False
        Me.LblTitleNinushi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LblTitleNinushi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LblTitleNinushi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LblTitleNinushi.EnableStatus = False
        Me.LblTitleNinushi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitleNinushi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitleNinushi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTitleNinushi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTitleNinushi.HeightDef = 18
        Me.LblTitleNinushi.Location = New System.Drawing.Point(3, 32)
        Me.LblTitleNinushi.Name = "LblTitleNinushi"
        Me.LblTitleNinushi.Size = New System.Drawing.Size(65, 18)
        Me.LblTitleNinushi.TabIndex = 108
        Me.LblTitleNinushi.Text = "荷主名"
        Me.LblTitleNinushi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblTitleNinushi.TextValue = "荷主名"
        Me.LblTitleNinushi.WidthDef = 65
        '
        'LblTitleShori
        '
        Me.LblTitleShori.AutoSizeDef = False
        Me.LblTitleShori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LblTitleShori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LblTitleShori.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LblTitleShori.EnableStatus = False
        Me.LblTitleShori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitleShori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitleShori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTitleShori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblTitleShori.HeightDef = 18
        Me.LblTitleShori.Location = New System.Drawing.Point(3, 53)
        Me.LblTitleShori.Name = "LblTitleShori"
        Me.LblTitleShori.Size = New System.Drawing.Size(65, 18)
        Me.LblTitleShori.TabIndex = 109
        Me.LblTitleShori.Text = "処理内容"
        Me.LblTitleShori.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblTitleShori.TextValue = "処理内容"
        Me.LblTitleShori.WidthDef = 65
        '
        'WebServiceInterFace1
        '
        Me.WebServiceInterFace1.Url = "http://localhost:1371/WebServiceInterFace.asmx"
        Me.WebServiceInterFace1.UseDefaultCredentials = True
        '
        'CmbShori
        '
        Me.CmbShori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbShori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbShori.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CmbShori.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmbShori.DataSource = Nothing
        Me.CmbShori.DisplayMember = Nothing
        Me.CmbShori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbShori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbShori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbShori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbShori.HeightDef = 18
        Me.CmbShori.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.CmbShori.HissuLabelVisible = True
        Me.CmbShori.InsertWildCard = True
        Me.CmbShori.IsForbiddenWordsCheck = False
        Me.CmbShori.IsHissuCheck = True
        Me.CmbShori.ItemName = ""
        Me.CmbShori.Location = New System.Drawing.Point(71, 53)
        Me.CmbShori.Name = "CmbShori"
        Me.CmbShori.ReadOnly = False
        Me.CmbShori.SelectedIndex = -1
        Me.CmbShori.SelectedItem = Nothing
        Me.CmbShori.SelectedText = ""
        Me.CmbShori.SelectedValue = ""
        Me.CmbShori.Size = New System.Drawing.Size(319, 18)
        Me.CmbShori.TabIndex = 112
        Me.CmbShori.TabStopSetting = True
        Me.CmbShori.TextValue = ""
        Me.CmbShori.ValueMember = Nothing
        Me.CmbShori.WidthDef = 319
        '
        'CmbCustNm
        '
        Me.CmbCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CmbCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmbCustNm.DataCode = "N018"
        Me.CmbCustNm.DataSource = Nothing
        Me.CmbCustNm.DisplayMember = Nothing
        Me.CmbCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbCustNm.HeightDef = 18
        Me.CmbCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.CmbCustNm.HissuLabelVisible = True
        Me.CmbCustNm.InsertWildCard = True
        Me.CmbCustNm.IsForbiddenWordsCheck = False
        Me.CmbCustNm.IsHissuCheck = True
        Me.CmbCustNm.ItemName = ""
        Me.CmbCustNm.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.CmbCustNm.Location = New System.Drawing.Point(71, 32)
        Me.CmbCustNm.Name = "CmbCustNm"
        Me.CmbCustNm.ReadOnly = False
        Me.CmbCustNm.SelectedIndex = -1
        Me.CmbCustNm.SelectedItem = Nothing
        Me.CmbCustNm.SelectedText = ""
        Me.CmbCustNm.SelectedValue = ""
        Me.CmbCustNm.Size = New System.Drawing.Size(319, 18)
        Me.CmbCustNm.TabIndex = 113
        Me.CmbCustNm.TabStopSetting = True
        Me.CmbCustNm.TextValue = ""
        Me.CmbCustNm.Value1 = Nothing
        Me.CmbCustNm.Value2 = Nothing
        Me.CmbCustNm.Value3 = Nothing
        Me.CmbCustNm.ValueMember = Nothing
        Me.CmbCustNm.WidthDef = 319
        '
        'LMI010F
        '
        Me.ClientSize = New System.Drawing.Size(394, 268)
        Me.Name = "LMI010F"
        Me.Text = "【LMI010】 荷主選択"
        Me.pnlViewAria.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblTitleShori As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LblTitleNinushi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents WebServiceInterFace1 As Jp.Co.Nrs.Com.WebServiceProxy.WebServiceInterFace
    Friend WithEvents CmbShori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents CmbCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun

End Class
