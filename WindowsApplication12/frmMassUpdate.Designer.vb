<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMassUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMassUpdate))
        Me.tblView = New System.Windows.Forms.TableLayoutPanel
        Me.grdView = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.grpUpdate = New System.Windows.Forms.GroupBox
        Me.txtUpdate = New System.Windows.Forms.TextBox
        Me.tblButtons = New System.Windows.Forms.TableLayoutPanel
        Me.btnDeselectAll = New System.Windows.Forms.Button
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnExport = New System.Windows.Forms.Button
        Me.tblView.SuspendLayout()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUpdate.SuspendLayout()
        Me.tblButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblView
        '
        Me.tblView.ColumnCount = 1
        Me.tblView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblView.Controls.Add(Me.grdView, 0, 0)
        Me.tblView.Controls.Add(Me.grpUpdate, 0, 1)
        Me.tblView.Controls.Add(Me.tblButtons, 0, 2)
        Me.tblView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblView.Location = New System.Drawing.Point(0, 0)
        Me.tblView.Name = "tblView"
        Me.tblView.RowCount = 3
        Me.tblView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.03409!))
        Me.tblView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122.0!))
        Me.tblView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.tblView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblView.Size = New System.Drawing.Size(886, 417)
        Me.tblView.TabIndex = 0
        '
        'grdView
        '
        Me.grdView.AllowColMove = False
        Me.grdView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdView.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdView.Images.Add(CType(resources.GetObject("grdView.Images"), System.Drawing.Image))
        Me.grdView.Location = New System.Drawing.Point(3, 3)
        Me.grdView.Name = "grdView"
        Me.grdView.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdView.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdView.PreviewInfo.ZoomFactor = 75
        Me.grdView.PrintInfo.PageSettings = CType(resources.GetObject("grdView.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdView.Size = New System.Drawing.Size(880, 250)
        Me.grdView.TabIndex = 22
        Me.grdView.Text = "C1TrueDBGrid3"
        Me.grdView.PropBag = resources.GetString("grdView.PropBag")
        '
        'grpUpdate
        '
        Me.grpUpdate.Controls.Add(Me.txtUpdate)
        Me.grpUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpUpdate.Location = New System.Drawing.Point(3, 259)
        Me.grpUpdate.Name = "grpUpdate"
        Me.grpUpdate.Size = New System.Drawing.Size(880, 116)
        Me.grpUpdate.TabIndex = 23
        Me.grpUpdate.TabStop = False
        Me.grpUpdate.Text = "Update"
        '
        'txtUpdate
        '
        Me.txtUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUpdate.Location = New System.Drawing.Point(3, 16)
        Me.txtUpdate.Multiline = True
        Me.txtUpdate.Name = "txtUpdate"
        Me.txtUpdate.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtUpdate.Size = New System.Drawing.Size(874, 97)
        Me.txtUpdate.TabIndex = 1
        '
        'tblButtons
        '
        Me.tblButtons.ColumnCount = 6
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 569.0!))
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.tblButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblButtons.Controls.Add(Me.btnDeselectAll, 0, 0)
        Me.tblButtons.Controls.Add(Me.btnSelectAll, 0, 0)
        Me.tblButtons.Controls.Add(Me.btnCancel, 5, 0)
        Me.tblButtons.Controls.Add(Me.btnUpdate, 4, 0)
        Me.tblButtons.Controls.Add(Me.btnExport, 3, 0)
        Me.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblButtons.Location = New System.Drawing.Point(3, 381)
        Me.tblButtons.Name = "tblButtons"
        Me.tblButtons.RowCount = 1
        Me.tblButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.tblButtons.Size = New System.Drawing.Size(880, 33)
        Me.tblButtons.TabIndex = 24
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Location = New System.Drawing.Point(79, 3)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(98, 27)
        Me.btnDeselectAll.TabIndex = 30
        Me.btnDeselectAll.Tag = "0"
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 3)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(63, 27)
        Me.btnSelectAll.TabIndex = 29
        Me.btnSelectAll.Tag = "1"
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(819, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(58, 27)
        Me.btnCancel.TabIndex = 32
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(755, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(58, 27)
        Me.btnUpdate.TabIndex = 28
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(652, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(96, 27)
        Me.btnExport.TabIndex = 26
        Me.btnExport.Text = "Export To Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmMassUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(886, 417)
        Me.Controls.Add(Me.tblView)
        Me.Name = "frmMassUpdate"
        Me.Text = "Mass Update"
        Me.tblView.ResumeLayout(False)
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUpdate.ResumeLayout(False)
        Me.grpUpdate.PerformLayout()
        Me.tblButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tblView As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdView As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents grpUpdate As System.Windows.Forms.GroupBox
    Friend WithEvents txtUpdate As System.Windows.Forms.TextBox
    Friend WithEvents tblButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
