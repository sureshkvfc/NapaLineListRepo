<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmColors
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmColors))
        Me.grdColors = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.grdColors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdColors
        '
        Me.grdColors.FilterBar = True
        Me.grdColors.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdColors.Images.Add(CType(resources.GetObject("grdColors.Images"), System.Drawing.Image))
        Me.grdColors.Location = New System.Drawing.Point(12, 12)
        Me.grdColors.Name = "grdColors"
        Me.grdColors.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdColors.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdColors.PreviewInfo.ZoomFactor = 75.0R
        Me.grdColors.PrintInfo.PageSettings = CType(resources.GetObject("grdColors.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdColors.Size = New System.Drawing.Size(703, 231)
        Me.grdColors.TabIndex = 1
        Me.grdColors.Text = "Colors"
        Me.grdColors.PropBag = resources.GetString("grdColors.PropBag")
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 535)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 54)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Delete Selected Records"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmColors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 601)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.grdColors)
        Me.Name = "frmColors"
        Me.Text = "Maintenance Colors"
        CType(Me.grdColors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdColors As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
