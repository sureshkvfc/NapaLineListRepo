<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnFreeze
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnFreeze))
        Me.grdUnFreeze = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.grdUnFreeze, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUnFreeze
        '
        Me.grdUnFreeze.AllowColMove = False
        Me.grdUnFreeze.AllowFilter = False
        Me.grdUnFreeze.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdUnFreeze.Images.Add(CType(resources.GetObject("grdUnFreeze.Images"), System.Drawing.Image))
        Me.grdUnFreeze.Location = New System.Drawing.Point(12, 12)
        Me.grdUnFreeze.Name = "grdUnFreeze"
        Me.grdUnFreeze.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdUnFreeze.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdUnFreeze.PreviewInfo.ZoomFactor = 75
        Me.grdUnFreeze.PrintInfo.PageSettings = CType(resources.GetObject("grdMaintenance.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdUnFreeze.Size = New System.Drawing.Size(857, 511)
        Me.grdUnFreeze.TabIndex = 21
        Me.grdUnFreeze.Text = "C1TrueDBGrid3"
        Me.grdUnFreeze.PropBag = resources.GetString("grdUnFreeze.PropBag")
        '
        'frmUnFreeze
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 664)
        Me.Controls.Add(Me.grdUnFreeze)
        Me.Name = "frmUnFreeze"
        Me.Text = "UnFreeze Data"
        CType(Me.grdUnFreeze, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdUnFreeze As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
