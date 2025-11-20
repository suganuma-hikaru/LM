<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI530F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL8B

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
        Me.lblTitleSelectKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSelectKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.pnlViewAria.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbSelectKb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSelectKb)
        Me.pnlViewAria.Size = New System.Drawing.Size(794, 188)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonEnabled = False
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = "保存"
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F5ButtonEnabled = False
        Me.FunctionKey.F6ButtonEnabled = False
        Me.FunctionKey.F7ButtonEnabled = False
        Me.FunctionKey.F8ButtonEnabled = False
        Me.FunctionKey.F9ButtonEnabled = False
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(113, 1)
        Me.FunctionKey.Size = New System.Drawing.Size(677, 40)
        Me.FunctionKey.WidthDef = 677
        '
        'lblTitleSelectKb
        '
        Me.lblTitleSelectKb.AutoSize = True
        Me.lblTitleSelectKb.AutoSizeDef = True
        Me.lblTitleSelectKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSelectKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSelectKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSelectKb.EnableStatus = False
        Me.lblTitleSelectKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSelectKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSelectKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSelectKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSelectKb.HeightDef = 13
        Me.lblTitleSelectKb.Location = New System.Drawing.Point(19, 32)
        Me.lblTitleSelectKb.Name = "lblTitleSelectKb"
        Me.lblTitleSelectKb.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSelectKb.TabIndex = 17
        Me.lblTitleSelectKb.Text = "現在の取込対象"
        Me.lblTitleSelectKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSelectKb.TextValue = "現在の取込対象"
        Me.lblTitleSelectKb.WidthDef = 105
        '
        'cmbSelectKb
        '
        Me.cmbSelectKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSelectKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSelectKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSelectKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSelectKb.DataSource = Nothing
        Me.cmbSelectKb.DisplayMember = Nothing
        Me.cmbSelectKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSelectKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSelectKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSelectKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSelectKb.HeightDef = 18
        Me.cmbSelectKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSelectKb.HissuLabelVisible = True
        Me.cmbSelectKb.InsertWildCard = True
        Me.cmbSelectKb.IsForbiddenWordsCheck = False
        Me.cmbSelectKb.IsHissuCheck = True
        Me.cmbSelectKb.ItemName = ""
        Me.cmbSelectKb.Location = New System.Drawing.Point(130, 29)
        Me.cmbSelectKb.Name = "cmbSelectKb"
        Me.cmbSelectKb.ReadOnly = False
        Me.cmbSelectKb.SelectedIndex = -1
        Me.cmbSelectKb.SelectedItem = Nothing
        Me.cmbSelectKb.SelectedText = ""
        Me.cmbSelectKb.SelectedValue = ""
        Me.cmbSelectKb.Size = New System.Drawing.Size(115, 18)
        Me.cmbSelectKb.TabIndex = 637
        Me.cmbSelectKb.TabStopSetting = True
        Me.cmbSelectKb.TextValue = ""
        Me.cmbSelectKb.ValueMember = Nothing
        Me.cmbSelectKb.WidthDef = 115
        '
        'LMI530F
        '
        Me.ClientSize = New System.Drawing.Size(794, 268)
        Me.Name = "LMI530F"
        Me.Text = "【LMI530F】  セミEDI環境切り替え"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleSelectKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSelectKb As Win.InputMan.LMImCombo
End Class
