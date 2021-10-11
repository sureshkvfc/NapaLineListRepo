<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintenance2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaintenance2))
        Me.grdMeasurementTemplate = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.grdMeasurementTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMeasurementTemplate
        '
        Me.grdMeasurementTemplate.AllowAddNew = True
        Me.grdMeasurementTemplate.AllowColMove = False
        Me.grdMeasurementTemplate.AllowDelete = True
        Me.grdMeasurementTemplate.FilterBar = True
        Me.grdMeasurementTemplate.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdMeasurementTemplate.Images.Add(CType(resources.GetObject("grdMeasurementTemplate.Images"), System.Drawing.Image))
        Me.grdMeasurementTemplate.Location = New System.Drawing.Point(2, 1)
        Me.grdMeasurementTemplate.Name = "grdMeasurementTemplate"
        Me.grdMeasurementTemplate.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdMeasurementTemplate.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdMeasurementTemplate.PreviewInfo.ZoomFactor = 75.0R
        Me.grdMeasurementTemplate.PrintInfo.PageSettings = CType(resources.GetObject("grdMeasurementTemplate.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdMeasurementTemplate.Size = New System.Drawing.Size(857, 511)
        Me.grdMeasurementTemplate.TabIndex = 21
        Me.grdMeasurementTemplate.Text = "C1TrueDBGrid3"
        Me.grdMeasurementTemplate.PropBag = resources.GetString("grdMeasurementTemplate.PropBag")
        '
        'frmMaintenance2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 595)
        Me.Controls.Add(Me.grdMeasurementTemplate)
        Me.Name = "frmMaintenance2"
        Me.Text = "Maintenance"
        CType(Me.grdMeasurementTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdMeasurementTemplate As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
