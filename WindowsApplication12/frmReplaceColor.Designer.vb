<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplaceColor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReplaceColor))
        Me.cmbColorToReplace = New System.Windows.Forms.ComboBox
        Me.cmbColorToReplaceTo = New System.Windows.Forms.ComboBox
        Me.lblReplace = New System.Windows.Forms.Label
        Me.lblWith = New System.Windows.Forms.Label
        Me.grdColors = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.cmdReplace = New System.Windows.Forms.Button
        CType(Me.grdColors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbColorToReplace
        '
        Me.cmbColorToReplace.FormattingEnabled = True
        Me.cmbColorToReplace.Location = New System.Drawing.Point(95, 18)
        Me.cmbColorToReplace.Name = "cmbColorToReplace"
        Me.cmbColorToReplace.Size = New System.Drawing.Size(195, 21)
        Me.cmbColorToReplace.TabIndex = 0
        '
        'cmbColorToReplaceTo
        '
        Me.cmbColorToReplaceTo.FormattingEnabled = True
        Me.cmbColorToReplaceTo.Location = New System.Drawing.Point(411, 18)
        Me.cmbColorToReplaceTo.Name = "cmbColorToReplaceTo"
        Me.cmbColorToReplaceTo.Size = New System.Drawing.Size(195, 21)
        Me.cmbColorToReplaceTo.TabIndex = 1
        '
        'lblReplace
        '
        Me.lblReplace.AutoSize = True
        Me.lblReplace.Location = New System.Drawing.Point(12, 21)
        Me.lblReplace.Name = "lblReplace"
        Me.lblReplace.Size = New System.Drawing.Size(77, 13)
        Me.lblReplace.TabIndex = 2
        Me.lblReplace.Text = "Replace Color:"
        '
        'lblWith
        '
        Me.lblWith.AutoSize = True
        Me.lblWith.Location = New System.Drawing.Point(346, 21)
        Me.lblWith.Name = "lblWith"
        Me.lblWith.Size = New System.Drawing.Size(59, 13)
        Me.lblWith.TabIndex = 3
        Me.lblWith.Text = "With Color:"
        '
        'grdColors
        '
        Me.grdColors.AllowColMove = False
        Me.grdColors.AllowColSelect = False
        Me.grdColors.AllowFilter = False
        Me.grdColors.AllowUpdate = False
        Me.grdColors.AllowUpdateOnBlur = False
        Me.grdColors.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdColors.Images.Add(CType(resources.GetObject("grdColors.Images"), System.Drawing.Image))
        Me.grdColors.Location = New System.Drawing.Point(8, 55)
        Me.grdColors.Name = "grdColors"
        Me.grdColors.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdColors.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdColors.PreviewInfo.ZoomFactor = 75
        Me.grdColors.PrintInfo.PageSettings = CType(resources.GetObject("grdColors.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdColors.Size = New System.Drawing.Size(703, 231)
        Me.grdColors.TabIndex = 4
        Me.grdColors.Text = "Colors"
        Me.grdColors.PropBag = resources.GetString("grdColors.PropBag")
        '
        'cmdReplace
        '
        Me.cmdReplace.Location = New System.Drawing.Point(636, 16)
        Me.cmdReplace.Name = "cmdReplace"
        Me.cmdReplace.Size = New System.Drawing.Size(75, 23)
        Me.cmdReplace.TabIndex = 5
        Me.cmdReplace.Text = "Replace"
        Me.cmdReplace.UseVisualStyleBackColor = True
        '
        'frmReplaceColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(835, 491)
        Me.Controls.Add(Me.cmdReplace)
        Me.Controls.Add(Me.grdColors)
        Me.Controls.Add(Me.lblWith)
        Me.Controls.Add(Me.lblReplace)
        Me.Controls.Add(Me.cmbColorToReplaceTo)
        Me.Controls.Add(Me.cmbColorToReplace)
        Me.Name = "frmReplaceColor"
        Me.Text = "frmReplaceColor"
        CType(Me.grdColors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbColorToReplace As System.Windows.Forms.ComboBox
    Friend WithEvents cmbColorToReplaceTo As System.Windows.Forms.ComboBox
    Friend WithEvents lblReplace As System.Windows.Forms.Label
    Friend WithEvents lblWith As System.Windows.Forms.Label
    Friend WithEvents grdColors As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cmdReplace As System.Windows.Forms.Button
End Class
