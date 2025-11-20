<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI450F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopM

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grpAction = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.btnFileSelect = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.txtFileName = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblFileName = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtFilePath = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtLocalPath = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlViewAria.SuspendLayout()
        Me.grpAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtFilePath)
        Me.pnlViewAria.Controls.Add(Me.txtLocalPath)
        Me.pnlViewAria.Controls.Add(Me.grpAction)
        Me.pnlViewAria.Size = New System.Drawing.Size(594, 345)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F9ButtonName = "実 行"
        '
        'grpAction
        '
        Me.grpAction.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpAction.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpAction.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpAction.Controls.Add(Me.btnFileSelect)
        Me.grpAction.Controls.Add(Me.txtFileName)
        Me.grpAction.Controls.Add(Me.lblFileName)
        Me.grpAction.EnableStatus = False
        Me.grpAction.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpAction.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpAction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpAction.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpAction.HeightDef = 120
        Me.grpAction.Location = New System.Drawing.Point(20, 28)
        Me.grpAction.Name = "grpAction"
        Me.grpAction.Size = New System.Drawing.Size(554, 120)
        Me.grpAction.TabIndex = 413
        Me.grpAction.TabStop = False
        Me.grpAction.Text = "処理条件"
        Me.grpAction.TextValue = "処理条件"
        Me.grpAction.WidthDef = 554
        '
        'btnFileSelect
        '
        Me.btnFileSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnFileSelect.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnFileSelect.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnFileSelect.EnableStatus = True
        Me.btnFileSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileSelect.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFileSelect.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFileSelect.HeightDef = 22
        Me.btnFileSelect.Location = New System.Drawing.Point(441, 54)
        Me.btnFileSelect.Name = "btnFileSelect"
        Me.btnFileSelect.Size = New System.Drawing.Size(80, 22)
        Me.btnFileSelect.TabIndex = 705
        Me.btnFileSelect.TabStopSetting = True
        Me.btnFileSelect.Text = "選択"
        Me.btnFileSelect.TextValue = "選択"
        Me.btnFileSelect.UseVisualStyleBackColor = True
        Me.btnFileSelect.WidthDef = 80
        '
        'txtFileName
        '
        Me.txtFileName.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileName.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileName.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileName.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFileName.CountWrappedLine = False
        Me.txtFileName.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFileName.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileName.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileName.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileName.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFileName.HeightDef = 18
        Me.txtFileName.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileName.HissuLabelVisible = False
        Me.txtFileName.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtFileName.IsByteCheck = 60
        Me.txtFileName.IsCalendarCheck = False
        Me.txtFileName.IsDakutenCheck = False
        Me.txtFileName.IsEisuCheck = False
        Me.txtFileName.IsForbiddenWordsCheck = False
        Me.txtFileName.IsFullByteCheck = 0
        Me.txtFileName.IsHankakuCheck = False
        Me.txtFileName.IsHissuCheck = False
        Me.txtFileName.IsKanaCheck = False
        Me.txtFileName.IsMiddleSpace = False
        Me.txtFileName.IsNumericCheck = False
        Me.txtFileName.IsSujiCheck = False
        Me.txtFileName.IsZenkakuCheck = False
        Me.txtFileName.ItemName = ""
        Me.txtFileName.LineSpace = 0
        Me.txtFileName.Location = New System.Drawing.Point(82, 30)
        Me.txtFileName.MaxLength = 60
        Me.txtFileName.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFileName.MaxLineCount = 0
        Me.txtFileName.Multiline = False
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFileName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFileName.Size = New System.Drawing.Size(452, 18)
        Me.txtFileName.TabIndex = 708
        Me.txtFileName.TabStop = False
        Me.txtFileName.TabStopSetting = False
        Me.txtFileName.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtFileName.UseSystemPasswordChar = False
        Me.txtFileName.WidthDef = 452
        Me.txtFileName.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.AutoSizeDef = True
        Me.lblFileName.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFileName.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFileName.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblFileName.EnableStatus = False
        Me.lblFileName.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFileName.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFileName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFileName.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFileName.HeightDef = 13
        Me.lblFileName.Location = New System.Drawing.Point(4, 33)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(77, 13)
        Me.lblFileName.TabIndex = 413
        Me.lblFileName.Text = "ファイル："
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFileName.TextValue = "ファイル："
        Me.lblFileName.WidthDef = 77
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFilePath.CountWrappedLine = False
        Me.txtFilePath.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFilePath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFilePath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFilePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFilePath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFilePath.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFilePath.HeightDef = 18
        Me.txtFilePath.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.HissuLabelVisible = False
        Me.txtFilePath.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtFilePath.IsByteCheck = 60
        Me.txtFilePath.IsCalendarCheck = False
        Me.txtFilePath.IsDakutenCheck = False
        Me.txtFilePath.IsEisuCheck = False
        Me.txtFilePath.IsForbiddenWordsCheck = False
        Me.txtFilePath.IsFullByteCheck = 0
        Me.txtFilePath.IsHankakuCheck = False
        Me.txtFilePath.IsHissuCheck = False
        Me.txtFilePath.IsKanaCheck = False
        Me.txtFilePath.IsMiddleSpace = False
        Me.txtFilePath.IsNumericCheck = False
        Me.txtFilePath.IsSujiCheck = False
        Me.txtFilePath.IsZenkakuCheck = False
        Me.txtFilePath.ItemName = ""
        Me.txtFilePath.LineSpace = 0
        Me.txtFilePath.Location = New System.Drawing.Point(102, 154)
        Me.txtFilePath.MaxLength = 60
        Me.txtFilePath.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFilePath.MaxLineCount = 0
        Me.txtFilePath.Multiline = False
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFilePath.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFilePath.Size = New System.Drawing.Size(452, 18)
        Me.txtFilePath.TabIndex = 709
        Me.txtFilePath.TabStop = False
        Me.txtFilePath.TabStopSetting = False
        Me.txtFilePath.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtFilePath.UseSystemPasswordChar = False
        Me.txtFilePath.Visible = False
        Me.txtFilePath.WidthDef = 452
        Me.txtFilePath.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtLocalPath
        '
        Me.txtLocalPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLocalPath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLocalPath.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocalPath.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLocalPath.CountWrappedLine = False
        Me.txtLocalPath.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLocalPath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLocalPath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLocalPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLocalPath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLocalPath.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLocalPath.HeightDef = 18
        Me.txtLocalPath.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLocalPath.HissuLabelVisible = False
        Me.txtLocalPath.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLocalPath.IsByteCheck = 60
        Me.txtLocalPath.IsCalendarCheck = False
        Me.txtLocalPath.IsDakutenCheck = False
        Me.txtLocalPath.IsEisuCheck = False
        Me.txtLocalPath.IsForbiddenWordsCheck = False
        Me.txtLocalPath.IsFullByteCheck = 0
        Me.txtLocalPath.IsHankakuCheck = False
        Me.txtLocalPath.IsHissuCheck = False
        Me.txtLocalPath.IsKanaCheck = False
        Me.txtLocalPath.IsMiddleSpace = False
        Me.txtLocalPath.IsNumericCheck = False
        Me.txtLocalPath.IsSujiCheck = False
        Me.txtLocalPath.IsZenkakuCheck = False
        Me.txtLocalPath.ItemName = ""
        Me.txtLocalPath.LineSpace = 0
        Me.txtLocalPath.Location = New System.Drawing.Point(102, 178)
        Me.txtLocalPath.MaxLength = 60
        Me.txtLocalPath.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLocalPath.MaxLineCount = 0
        Me.txtLocalPath.Multiline = False
        Me.txtLocalPath.Name = "txtLocalPath"
        Me.txtLocalPath.ReadOnly = True
        Me.txtLocalPath.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLocalPath.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLocalPath.Size = New System.Drawing.Size(452, 18)
        Me.txtLocalPath.TabIndex = 710
        Me.txtLocalPath.TabStop = False
        Me.txtLocalPath.TabStopSetting = False
        Me.txtLocalPath.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtLocalPath.UseSystemPasswordChar = False
        Me.txtLocalPath.Visible = False
        Me.txtLocalPath.WidthDef = 452
        Me.txtLocalPath.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMI450F
        '
        Me.ClientSize = New System.Drawing.Size(594, 425)
        Me.Name = "LMI450F"
        Me.Text = "【LMI450】 東レ・ダウExcelファイル取込"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpAction.ResumeLayout(False)
        Me.grpAction.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpAction As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblFileName As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnFileSelect As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents txtFileName As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtFilePath As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtLocalPath As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
