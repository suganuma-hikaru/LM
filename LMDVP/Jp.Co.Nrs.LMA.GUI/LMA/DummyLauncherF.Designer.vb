<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DummyLauncherF
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        Dim Label7 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Me.LoadButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ClassListBox = New System.Windows.Forms.ListBox()
        Me.AssemblyListBox = New System.Windows.Forms.ListBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.grpPram = New System.Windows.Forms.GroupBox()
        Me.grpMsgType = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.optEn = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optJp = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.cmbMultiFlg = New System.Windows.Forms.ComboBox()
        Me.sprUserPram = New FarPoint.Win.Spread.FpSpread()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.pramView = New FarPoint.Win.Spread.FpSpread()
        Me.cmbSkpFlg = New System.Windows.Forms.ComboBox()
        Me.cmbRtnFlg = New System.Windows.Forms.ComboBox()
        Me.cmbRecStatus = New System.Windows.Forms.ComboBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.btnGetMaster = New System.Windows.Forms.Button()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.LmErrorProvider = New Jp.Co.Nrs.LM.GUI.Win.LMErrorProvider()
        Me.optKo = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optCN = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Label7 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.grpPram.SuspendLayout()
        Me.grpMsgType.SuspendLayout()
        CType(Me.sprUserPram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pramView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LmErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label7
        '
        Label7.Location = New System.Drawing.Point(289, 139)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(99, 15)
        Label7.TabIndex = 37
        Label7.Text = "スキップフラグ"
        Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Label6.Location = New System.Drawing.Point(11, 164)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(89, 15)
        Label6.TabIndex = 36
        Label6.Text = "リターンフラグ"
        Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Label5.Location = New System.Drawing.Point(10, 140)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(89, 17)
        Label5.TabIndex = 35
        Label5.Text = "レコードステータス"
        Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.Location = New System.Drawing.Point(15, 21)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(89, 15)
        Label4.TabIndex = 31
        Label4.Text = "ユーザーID"
        Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Label8.Location = New System.Drawing.Point(11, 210)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(99, 15)
        Label8.TabIndex = 39
        Label8.Text = "その他のパラメータ"
        Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Label9.Location = New System.Drawing.Point(289, 21)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(62, 15)
        Label9.TabIndex = 44
        Label9.Text = "パスワード"
        Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.Location = New System.Drawing.Point(15, 44)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(89, 15)
        Label3.TabIndex = 46
        Label3.Text = "ユーザー情報"
        Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Label10.Location = New System.Drawing.Point(289, 166)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(99, 15)
        Label10.TabIndex = 48
        Label10.Text = "複数選択フラグ"
        Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LoadButton
        '
        Me.LoadButton.Location = New System.Drawing.Point(343, 30)
        Me.LoadButton.Name = "LoadButton"
        Me.LoadButton.Size = New System.Drawing.Size(100, 25)
        Me.LoadButton.TabIndex = 11
        Me.LoadButton.TabStop = False
        Me.LoadButton.Text = "読み込み"
        Me.LoadButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(453, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 20)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "ロードするクラス"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "ロードするアセンブリ"
        '
        'ClassListBox
        '
        Me.ClassListBox.FormattingEnabled = True
        Me.ClassListBox.ItemHeight = 12
        Me.ClassListBox.Location = New System.Drawing.Point(456, 30)
        Me.ClassListBox.Name = "ClassListBox"
        Me.ClassListBox.Size = New System.Drawing.Size(312, 208)
        Me.ClassListBox.TabIndex = 2
        '
        'AssemblyListBox
        '
        Me.AssemblyListBox.FormattingEnabled = True
        Me.AssemblyListBox.ItemHeight = 12
        Me.AssemblyListBox.Location = New System.Drawing.Point(25, 30)
        Me.AssemblyListBox.Name = "AssemblyListBox"
        Me.AssemblyListBox.Size = New System.Drawing.Size(312, 76)
        Me.AssemblyListBox.TabIndex = 7
        Me.AssemblyListBox.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 644)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(892, 22)
        Me.StatusStrip1.TabIndex = 20
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'grpPram
        '
        Me.grpPram.Controls.Add(Me.grpMsgType)
        Me.grpPram.Controls.Add(Label10)
        Me.grpPram.Controls.Add(Me.cmbMultiFlg)
        Me.grpPram.Controls.Add(Label3)
        Me.grpPram.Controls.Add(Me.sprUserPram)
        Me.grpPram.Controls.Add(Label9)
        Me.grpPram.Controls.Add(Me.txtPassword)
        Me.grpPram.Controls.Add(Me.btnLogin)
        Me.grpPram.Controls.Add(Label8)
        Me.grpPram.Controls.Add(Me.pramView)
        Me.grpPram.Controls.Add(Label7)
        Me.grpPram.Controls.Add(Label6)
        Me.grpPram.Controls.Add(Label5)
        Me.grpPram.Controls.Add(Me.cmbSkpFlg)
        Me.grpPram.Controls.Add(Me.cmbRtnFlg)
        Me.grpPram.Controls.Add(Me.cmbRecStatus)
        Me.grpPram.Controls.Add(Label4)
        Me.grpPram.Controls.Add(Me.txtUserID)
        Me.grpPram.Location = New System.Drawing.Point(12, 256)
        Me.grpPram.Name = "grpPram"
        Me.grpPram.Size = New System.Drawing.Size(868, 352)
        Me.grpPram.TabIndex = 28
        Me.grpPram.TabStop = False
        Me.grpPram.Text = "画面遷移パラメータ"
        '
        'grpMsgType
        '
        Me.grpMsgType.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.grpMsgType.BackColorDef = System.Drawing.SystemColors.ButtonFace
        Me.grpMsgType.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMsgType.Controls.Add(Me.optCN)
        Me.grpMsgType.Controls.Add(Me.optKo)
        Me.grpMsgType.Controls.Add(Me.optEn)
        Me.grpMsgType.Controls.Add(Me.optJp)
        Me.grpMsgType.EnableStatus = False
        Me.grpMsgType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMsgType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMsgType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMsgType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMsgType.HeightDef = 86
        Me.grpMsgType.Location = New System.Drawing.Point(589, 139)
        Me.grpMsgType.Name = "grpMsgType"
        Me.grpMsgType.Size = New System.Drawing.Size(264, 86)
        Me.grpMsgType.TabIndex = 50
        Me.grpMsgType.TabStop = False
        Me.grpMsgType.Text = "メッセージ言語"
        Me.grpMsgType.TextValue = "メッセージ言語"
        Me.grpMsgType.WidthDef = 264
        '
        'optEn
        '
        Me.optEn.AutoSize = True
        Me.optEn.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.optEn.BackColorDef = System.Drawing.SystemColors.ButtonFace
        Me.optEn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optEn.EnableStatus = True
        Me.optEn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optEn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optEn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optEn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optEn.HeightDef = 21
        Me.optEn.Location = New System.Drawing.Point(128, 18)
        Me.optEn.Name = "optEn"
        Me.optEn.Size = New System.Drawing.Size(65, 21)
        Me.optEn.TabIndex = 1
        Me.optEn.TabStop = True
        Me.optEn.TabStopSetting = True
        Me.optEn.Text = "英語"
        Me.optEn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optEn.TextValue = "英語"
        Me.optEn.UseVisualStyleBackColor = False
        Me.optEn.WidthDef = 65
        '
        'optJp
        '
        Me.optJp.AutoSize = True
        Me.optJp.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.optJp.BackColorDef = System.Drawing.SystemColors.ButtonFace
        Me.optJp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optJp.EnableStatus = True
        Me.optJp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optJp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optJp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optJp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optJp.HeightDef = 21
        Me.optJp.Location = New System.Drawing.Point(28, 18)
        Me.optJp.Name = "optJp"
        Me.optJp.Size = New System.Drawing.Size(83, 21)
        Me.optJp.TabIndex = 0
        Me.optJp.TabStop = True
        Me.optJp.TabStopSetting = True
        Me.optJp.Text = "日本語"
        Me.optJp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optJp.TextValue = "日本語"
        Me.optJp.UseVisualStyleBackColor = False
        Me.optJp.WidthDef = 83
        '
        'cmbMultiFlg
        '
        Me.cmbMultiFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMultiFlg.FormattingEnabled = True
        Me.cmbMultiFlg.Items.AddRange(New Object() {"True", "False"})
        Me.cmbMultiFlg.Location = New System.Drawing.Point(388, 164)
        Me.cmbMultiFlg.Name = "cmbMultiFlg"
        Me.cmbMultiFlg.Size = New System.Drawing.Size(165, 20)
        Me.cmbMultiFlg.TabIndex = 47
        '
        'sprUserPram
        '
        Me.sprUserPram.AccessibleDescription = ""
        Me.sprUserPram.Location = New System.Drawing.Point(13, 62)
        Me.sprUserPram.Name = "sprUserPram"
        Me.sprUserPram.Size = New System.Drawing.Size(840, 71)
        Me.sprUserPram.TabIndex = 30
        Me.sprUserPram.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(352, 19)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(165, 19)
        Me.txtPassword.TabIndex = 1
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(523, 16)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(100, 25)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "認証"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'pramView
        '
        Me.pramView.AccessibleDescription = ""
        Me.pramView.Location = New System.Drawing.Point(13, 230)
        Me.pramView.Name = "pramView"
        Me.pramView.Size = New System.Drawing.Size(840, 99)
        Me.pramView.TabIndex = 38
        Me.pramView.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always
        '
        'cmbSkpFlg
        '
        Me.cmbSkpFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSkpFlg.FormattingEnabled = True
        Me.cmbSkpFlg.Items.AddRange(New Object() {"True", "False"})
        Me.cmbSkpFlg.Location = New System.Drawing.Point(388, 137)
        Me.cmbSkpFlg.Name = "cmbSkpFlg"
        Me.cmbSkpFlg.Size = New System.Drawing.Size(165, 20)
        Me.cmbSkpFlg.TabIndex = 5
        '
        'cmbRtnFlg
        '
        Me.cmbRtnFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRtnFlg.FormattingEnabled = True
        Me.cmbRtnFlg.Items.AddRange(New Object() {"True", "False"})
        Me.cmbRtnFlg.Location = New System.Drawing.Point(104, 162)
        Me.cmbRtnFlg.Name = "cmbRtnFlg"
        Me.cmbRtnFlg.Size = New System.Drawing.Size(165, 20)
        Me.cmbRtnFlg.TabIndex = 4
        '
        'cmbRecStatus
        '
        Me.cmbRecStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRecStatus.FormattingEnabled = True
        Me.cmbRecStatus.Location = New System.Drawing.Point(104, 139)
        Me.cmbRecStatus.Name = "cmbRecStatus"
        Me.cmbRecStatus.Size = New System.Drawing.Size(165, 20)
        Me.cmbRecStatus.TabIndex = 3
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(104, 19)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(165, 19)
        Me.txtUserID.TabIndex = 0
        '
        'btnGetMaster
        '
        Me.btnGetMaster.Location = New System.Drawing.Point(21, 614)
        Me.btnGetMaster.Name = "btnGetMaster"
        Me.btnGetMaster.Size = New System.Drawing.Size(100, 25)
        Me.btnGetMaster.TabIndex = 31
        Me.btnGetMaster.TabStop = False
        Me.btnGetMaster.Text = "マスタデータ取得"
        Me.btnGetMaster.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(674, 614)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(100, 25)
        Me.btnExecute.TabIndex = 8
        Me.btnExecute.Text = "実行"
        Me.btnExecute.UseVisualStyleBackColor = True
        Me.btnExecute.Visible = False
        '
        'btnEnd
        '
        Me.btnEnd.Location = New System.Drawing.Point(780, 614)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(100, 25)
        Me.btnEnd.TabIndex = 30
        Me.btnEnd.Text = "終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'LmErrorProvider
        '
        Me.LmErrorProvider.ContainerControl = Me
        '
        'optKo
        '
        Me.optKo.AutoSize = True
        Me.optKo.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.optKo.BackColorDef = System.Drawing.SystemColors.ButtonFace
        Me.optKo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKo.EnableStatus = True
        Me.optKo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKo.HeightDef = 21
        Me.optKo.Location = New System.Drawing.Point(28, 45)
        Me.optKo.Name = "optKo"
        Me.optKo.Size = New System.Drawing.Size(83, 21)
        Me.optKo.TabIndex = 2
        Me.optKo.TabStop = True
        Me.optKo.TabStopSetting = True
        Me.optKo.Text = "韓国語"
        Me.optKo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKo.TextValue = "韓国語"
        Me.optKo.UseVisualStyleBackColor = False
        Me.optKo.WidthDef = 83
        '
        'optCN
        '
        Me.optCN.AutoSize = True
        Me.optCN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.optCN.BackColorDef = System.Drawing.SystemColors.ButtonFace
        Me.optCN.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optCN.EnableStatus = True
        Me.optCN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optCN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optCN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCN.HeightDef = 21
        Me.optCN.Location = New System.Drawing.Point(128, 45)
        Me.optCN.Name = "optCN"
        Me.optCN.Size = New System.Drawing.Size(83, 21)
        Me.optCN.TabIndex = 3
        Me.optCN.TabStop = True
        Me.optCN.TabStopSetting = True
        Me.optCN.Text = "中国語"
        Me.optCN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optCN.TextValue = "中国語"
        Me.optCN.UseVisualStyleBackColor = False
        Me.optCN.WidthDef = 83
        '
        'DummyLauncherF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 666)
        Me.Controls.Add(Me.btnGetMaster)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.grpPram)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.LoadButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ClassListBox)
        Me.Controls.Add(Me.AssemblyListBox)
        Me.KeyPreview = True
        Me.Name = "DummyLauncherF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Luncher"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.grpPram.ResumeLayout(False)
        Me.grpPram.PerformLayout()
        Me.grpMsgType.ResumeLayout(False)
        Me.grpMsgType.PerformLayout()
        CType(Me.sprUserPram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pramView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LmErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LoadButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ClassListBox As System.Windows.Forms.ListBox
    Friend WithEvents AssemblyListBox As System.Windows.Forms.ListBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents grpPram As System.Windows.Forms.GroupBox
    Friend WithEvents pramView As FarPoint.Win.Spread.FpSpread
    Friend WithEvents cmbSkpFlg As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRtnFlg As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRecStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents btnGetMaster As System.Windows.Forms.Button
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents sprUserPram As FarPoint.Win.Spread.FpSpread
    Friend WithEvents cmbMultiFlg As System.Windows.Forms.ComboBox
    Friend WithEvents grpMsgType As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents optEn As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optJp As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents LmErrorProvider As Jp.Co.Nrs.LM.GUI.Win.LMErrorProvider
    Friend WithEvents optCN As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optKo As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
End Class
