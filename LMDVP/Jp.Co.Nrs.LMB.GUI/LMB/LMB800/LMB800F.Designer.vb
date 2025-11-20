<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMB800F
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
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'LMB800F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMB800F"
        Me.Text = "【LMB800】 GHSラベルCSV作成"
        Me.ResumeLayout(False)

    End Sub

End Class
