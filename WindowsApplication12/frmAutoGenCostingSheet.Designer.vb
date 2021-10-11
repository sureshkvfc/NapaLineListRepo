<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoGenCostingSheet
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAutomateExport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAutomateExport
        '
        Me.btnAutomateExport.Location = New System.Drawing.Point(40, 40)
        Me.btnAutomateExport.Name = "btnAutomateExport"
        Me.btnAutomateExport.Size = New System.Drawing.Size(75, 23)
        Me.btnAutomateExport.TabIndex = 0
        Me.btnAutomateExport.Text = "Export"
        Me.btnAutomateExport.UseVisualStyleBackColor = True
        '
        'frmAutoGenCostingSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.btnAutomateExport)
        Me.Name = "frmAutoGenCostingSheet"
        Me.Text = "Costing Sheet Export"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAutomateExport As System.Windows.Forms.Button
End Class
