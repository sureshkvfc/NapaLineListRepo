<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomExcel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomExcel))
        Me.grdCustomExcel = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.chkYouth = New System.Windows.Forms.CheckBox
        Me.chkLUserGroups = New System.Windows.Forms.CheckedListBox
        Me.lblUserGroups = New System.Windows.Forms.Label
        Me.chkIncludeNP = New System.Windows.Forms.CheckBox
        CType(Me.grdCustomExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdCustomExcel
        '
        Me.grdCustomExcel.AllowColMove = False
        Me.grdCustomExcel.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdCustomExcel.Images.Add(CType(resources.GetObject("grdCustomExcel.Images"), System.Drawing.Image))
        Me.grdCustomExcel.Location = New System.Drawing.Point(12, 112)
        Me.grdCustomExcel.Name = "grdCustomExcel"
        Me.grdCustomExcel.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdCustomExcel.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdCustomExcel.PreviewInfo.ZoomFactor = 75
        Me.grdCustomExcel.PrintInfo.PageSettings = CType(resources.GetObject("grdCustomExcel.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdCustomExcel.Size = New System.Drawing.Size(857, 400)
        Me.grdCustomExcel.TabIndex = 21
        Me.grdCustomExcel.Text = "C1TrueDBGrid3"
        Me.grdCustomExcel.PropBag = resources.GetString("grdCustomExcel.PropBag")
        '
        'chkYouth
        '
        Me.chkYouth.AutoSize = True
        Me.chkYouth.Location = New System.Drawing.Point(12, 12)
        Me.chkYouth.Name = "chkYouth"
        Me.chkYouth.Size = New System.Drawing.Size(92, 17)
        Me.chkYouth.TabIndex = 22
        Me.chkYouth.Text = "Include Youth"
        Me.chkYouth.UseVisualStyleBackColor = True
        '
        'chkLUserGroups
        '
        Me.chkLUserGroups.FormattingEnabled = True
        Me.chkLUserGroups.Location = New System.Drawing.Point(280, 12)
        Me.chkLUserGroups.Name = "chkLUserGroups"
        Me.chkLUserGroups.Size = New System.Drawing.Size(120, 94)
        Me.chkLUserGroups.TabIndex = 23
        '
        'lblUserGroups
        '
        Me.lblUserGroups.AutoSize = True
        Me.lblUserGroups.Location = New System.Drawing.Point(173, 12)
        Me.lblUserGroups.Name = "lblUserGroups"
        Me.lblUserGroups.Size = New System.Drawing.Size(101, 13)
        Me.lblUserGroups.TabIndex = 24
        Me.lblUserGroups.Text = "Usergroups access:"
        '
        'chkIncludeNP
        '
        Me.chkIncludeNP.AutoSize = True
        Me.chkIncludeNP.Location = New System.Drawing.Point(12, 35)
        Me.chkIncludeNP.Name = "chkIncludeNP"
        Me.chkIncludeNP.Size = New System.Drawing.Size(144, 17)
        Me.chkIncludeNP.TabIndex = 25
        Me.chkIncludeNP.Text = "Include StyleStatus = NP"
        Me.chkIncludeNP.UseVisualStyleBackColor = True
        '
        'frmCustomExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1166, 716)
        Me.Controls.Add(Me.chkIncludeNP)
        Me.Controls.Add(Me.lblUserGroups)
        Me.Controls.Add(Me.chkLUserGroups)
        Me.Controls.Add(Me.chkYouth)
        Me.Controls.Add(Me.grdCustomExcel)
        Me.Name = "frmCustomExcel"
        Me.Text = "Customize Excel"
        CType(Me.grdCustomExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdCustomExcel As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents chkYouth As System.Windows.Forms.CheckBox
    Friend WithEvents chkLUserGroups As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblUserGroups As System.Windows.Forms.Label
    Friend WithEvents chkIncludeNP As System.Windows.Forms.CheckBox
End Class
