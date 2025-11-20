<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LMB080F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.pnlDetailIn = New System.Windows.Forms.Panel()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.pnlDetailIn.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.pnlDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(878, 626)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(517, 0)
        Me.FunctionKey.Size = New System.Drawing.Size(361, 40)
        Me.FunctionKey.WidthDef = 361
        '
        'pnlDetail
        '
        Me.pnlDetail.AutoScroll = True
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.pnlDetailIn)
        Me.pnlDetail.Location = New System.Drawing.Point(12, 12)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(854, 608)
        Me.pnlDetail.TabIndex = 609
        '
        'pnlDetailIn
        '
        Me.pnlDetailIn.Location = New System.Drawing.Point(0, 0)
        Me.pnlDetailIn.Name = "pnlDetailIn"
        Me.pnlDetailIn.Size = New System.Drawing.Size(828, 900)
        Me.pnlDetailIn.TabIndex = 0
        '
        'LMB080F
        '
        Me.ClientSize = New System.Drawing.Size(878, 706)
        Me.Name = "LMB080F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlDetail As Panel
    Friend WithEvents pnlDetailIn As Panel
End Class
