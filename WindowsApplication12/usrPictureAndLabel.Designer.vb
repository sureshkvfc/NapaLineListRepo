<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrPictureAndLabel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pictBox = New System.Windows.Forms.PictureBox
        Me.lblLabel = New System.Windows.Forms.Label
        CType(Me.pictBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictBox
        '
        Me.pictBox.Location = New System.Drawing.Point(0, 0)
        Me.pictBox.Name = "pictBox"
        Me.pictBox.Size = New System.Drawing.Size(283, 199)
        Me.pictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictBox.TabIndex = 0
        Me.pictBox.TabStop = False
        '
        'lblLabel
        '
        Me.lblLabel.BackColor = System.Drawing.SystemColors.Control
        Me.lblLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabel.Location = New System.Drawing.Point(0, 202)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(283, 30)
        Me.lblLabel.TabIndex = 1
        Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'usrPictureAndLabel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblLabel)
        Me.Controls.Add(Me.pictBox)
        Me.Name = "usrPictureAndLabel"
        Me.Size = New System.Drawing.Size(283, 232)
        CType(Me.pictBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pictBox As System.Windows.Forms.PictureBox
    Friend WithEvents lblLabel As System.Windows.Forms.Label

End Class
