<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPickColors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPickColors))
        Me.clrDialog = New System.Windows.Forms.ColorDialog
        Me.grdMinors = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.grdMinors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMinors
        '
        Me.grdMinors.AllowColMove = False
        Me.grdMinors.ColumnHeaders = False
        Me.grdMinors.FetchRowStyles = True
        Me.grdMinors.FilterBar = True
        Me.grdMinors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMinors.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdMinors.Images.Add(CType(resources.GetObject("grdMinors.Images"), System.Drawing.Image))
        Me.grdMinors.Location = New System.Drawing.Point(12, 12)
        Me.grdMinors.Name = "grdMinors"
        Me.grdMinors.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdMinors.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdMinors.PreviewInfo.ZoomFactor = 75
        Me.grdMinors.PrintInfo.PageSettings = CType(resources.GetObject("grdMinors.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdMinors.Size = New System.Drawing.Size(526, 465)
        Me.grdMinors.TabIndex = 6
        Me.grdMinors.Text = "Minors"
        Me.grdMinors.PropBag = resources.GetString("grdMinors.PropBag")
        '
        'frmPickColors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(918, 567)
        Me.Controls.Add(Me.grdMinors)
        Me.Name = "frmPickColors"
        Me.Text = "Pick Colors"
        CType(Me.grdMinors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents clrDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents grdMinors As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
