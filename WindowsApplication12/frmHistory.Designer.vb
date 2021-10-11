<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHistory))
        Me.grdHistory = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdHistory
        '
        Me.grdHistory.AllowColMove = False
        Me.grdHistory.AllowUpdate = False
        Me.grdHistory.AllowUpdateOnBlur = False
        Me.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHistory.FilterBar = True
        Me.grdHistory.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdHistory.Images.Add(CType(resources.GetObject("grdHistory.Images"), System.Drawing.Image))
        Me.grdHistory.Location = New System.Drawing.Point(0, 0)
        Me.grdHistory.Name = "grdHistory"
        Me.grdHistory.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdHistory.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdHistory.PreviewInfo.ZoomFactor = 75
        Me.grdHistory.PrintInfo.PageSettings = CType(resources.GetObject("grdHistory.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdHistory.Size = New System.Drawing.Size(821, 635)
        Me.grdHistory.TabIndex = 21
        Me.grdHistory.Text = "C1TrueDBGrid3"
        Me.grdHistory.PropBag = resources.GetString("grdHistory.PropBag")
        '
        'frmHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 635)
        Me.Controls.Add(Me.grdHistory)
        Me.Name = "frmHistory"
        Me.Text = "History"
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdHistory As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
