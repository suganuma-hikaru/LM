<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LMB090F
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
        Me.picPhoto = New System.Windows.Forms.PictureBox()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.pnlDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(913, 0)
        Me.FunctionKey.Size = New System.Drawing.Size(361, 40)
        Me.FunctionKey.WidthDef = 361
        '
        'pnlDetail
        '
        Me.pnlDetail.AutoScroll = True
        Me.pnlDetail.Controls.Add(Me.picPhoto)
        Me.pnlDetail.Location = New System.Drawing.Point(3, 3)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(1268, 873)
        Me.pnlDetail.TabIndex = 609
        '
        'picPhoto
        '
        Me.picPhoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.picPhoto.Location = New System.Drawing.Point(0, 0)
        Me.picPhoto.Name = "picPhoto"
        Me.picPhoto.Size = New System.Drawing.Size(50, 50)
        Me.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPhoto.TabIndex = 609
        Me.picPhoto.TabStop = False
        '
        'LMB090F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMB090F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.picPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlDetail As Panel
    Friend WithEvents picPhoto As PictureBox
End Class
