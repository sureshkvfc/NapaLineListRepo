<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThumbnails
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
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.trackBar = New System.Windows.Forms.TrackBar
        CType(Me.trackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(24, 87)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(15, 66)
        Me.btnNext.TabIndex = 0
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(3, 87)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(15, 66)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Text = "<"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'trackBar
        '
        Me.trackBar.Cursor = System.Windows.Forms.Cursors.NoMoveVert
        Me.trackBar.Location = New System.Drawing.Point(3, 1)
        Me.trackBar.Name = "trackBar"
        Me.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.trackBar.Size = New System.Drawing.Size(45, 80)
        Me.trackBar.TabIndex = 2
        '
        'frmThumbnails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.trackBar)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Name = "frmThumbnails"
        Me.Text = "frmThumbnails"
        CType(Me.trackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents trackBar As System.Windows.Forms.TrackBar
End Class
