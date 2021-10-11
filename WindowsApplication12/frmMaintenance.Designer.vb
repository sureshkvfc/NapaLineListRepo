<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintenance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaintenance))
        Me.grdMaintenance = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.grdMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMaintenance
        '
        Me.grdMaintenance.AllowAddNew = True
        Me.grdMaintenance.AllowColMove = False
        Me.grdMaintenance.AllowDelete = True
        Me.grdMaintenance.FilterBar = True
        Me.grdMaintenance.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdMaintenance.Images.Add(CType(resources.GetObject("grdMaintenance.Images"), System.Drawing.Image))
        Me.grdMaintenance.Location = New System.Drawing.Point(12, 2)
        Me.grdMaintenance.Name = "grdMaintenance"
        Me.grdMaintenance.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdMaintenance.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdMaintenance.PreviewInfo.ZoomFactor = 75.0R
        Me.grdMaintenance.PrintInfo.PageSettings = CType(resources.GetObject("grdMaintenance.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdMaintenance.Size = New System.Drawing.Size(857, 511)
        Me.grdMaintenance.TabIndex = 20
        Me.grdMaintenance.Text = "C1TrueDBGrid3"
        Me.grdMaintenance.PropBag = resources.GetString("grdMaintenance.PropBag")
        '
        'frmMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 595)
        Me.Controls.Add(Me.grdMaintenance)
        Me.Name = "frmMaintenance"
        Me.Text = "Maintenance"
        CType(Me.grdMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdMaintenance As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
