<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUndelete
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUndelete))
        Me.grdUnDeleteRecords = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.Button2 = New System.Windows.Forms.Button
        CType(Me.grdUnDeleteRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUnDeleteRecords
        '
        Me.grdUnDeleteRecords.AllowUpdate = False
        Me.grdUnDeleteRecords.AllowUpdateOnBlur = False
        Me.grdUnDeleteRecords.FilterBar = True
        Me.grdUnDeleteRecords.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdUnDeleteRecords.Images.Add(CType(resources.GetObject("grdUnDeleteRecords.Images"), System.Drawing.Image))
        Me.grdUnDeleteRecords.Location = New System.Drawing.Point(12, 12)
        Me.grdUnDeleteRecords.Name = "grdUnDeleteRecords"
        Me.grdUnDeleteRecords.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdUnDeleteRecords.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdUnDeleteRecords.PreviewInfo.ZoomFactor = 75
        Me.grdUnDeleteRecords.PrintInfo.PageSettings = CType(resources.GetObject("grdUnDeleteRecords.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdUnDeleteRecords.Size = New System.Drawing.Size(574, 267)
        Me.grdUnDeleteRecords.TabIndex = 20
        Me.grdUnDeleteRecords.Text = "C1TrueDBGrid4"
        Me.grdUnDeleteRecords.PropBag = resources.GetString("grdUnDeleteRecords.PropBag")
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 505)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 54)
        Me.Button2.TabIndex = 21
        Me.Button2.Text = "UnDelete Selected Records"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmUndelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 571)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.grdUnDeleteRecords)
        Me.Name = "frmUndelete"
        Me.Text = "Undelete Records"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdUnDeleteRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdUnDeleteRecords As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
