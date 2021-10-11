<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadPotential
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUploadPotential))
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.lstLog = New System.Windows.Forms.ListBox()
        Me.lblUploading = New System.Windows.Forms.Label()
        Me.cmdUpload = New System.Windows.Forms.Button()
        Me.grdColumns = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.lblSheets = New System.Windows.Forms.Label()
        Me.lstSheets = New System.Windows.Forms.ListBox()
        Me.txtPotential = New System.Windows.Forms.TextBox()
        Me.lblPotential = New System.Windows.Forms.Label()
        Me.txtJBA = New System.Windows.Forms.TextBox()
        Me.lblJBA = New System.Windows.Forms.Label()
        Me.txtSeason = New System.Windows.Forms.TextBox()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.tmrCount = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(12, 32)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(628, 20)
        Me.txtFileName.TabIndex = 13
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(646, 29)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 23)
        Me.cmdBrowse.TabIndex = 12
        Me.cmdBrowse.Text = "Browse..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'lstLog
        '
        Me.lstLog.FormattingEnabled = True
        Me.lstLog.Location = New System.Drawing.Point(12, 392)
        Me.lstLog.Name = "lstLog"
        Me.lstLog.Size = New System.Drawing.Size(709, 108)
        Me.lstLog.TabIndex = 15
        '
        'lblUploading
        '
        Me.lblUploading.AutoSize = True
        Me.lblUploading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUploading.Location = New System.Drawing.Point(29, 194)
        Me.lblUploading.Name = "lblUploading"
        Me.lblUploading.Size = New System.Drawing.Size(64, 13)
        Me.lblUploading.TabIndex = 26
        Me.lblUploading.Text = "Uploading"
        Me.lblUploading.Visible = False
        '
        'cmdUpload
        '
        Me.cmdUpload.Enabled = False
        Me.cmdUpload.Location = New System.Drawing.Point(483, 181)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(75, 23)
        Me.cmdUpload.TabIndex = 25
        Me.cmdUpload.Text = "Upload..."
        Me.cmdUpload.UseVisualStyleBackColor = True
        '
        'grdColumns
        '
        Me.grdColumns.AllowColMove = False
        Me.grdColumns.AllowColSelect = False
        Me.grdColumns.AllowFilter = False
        Me.grdColumns.AllowUpdate = False
        Me.grdColumns.AllowUpdateOnBlur = False
        Me.grdColumns.ColumnHeaders = False
        Me.grdColumns.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdColumns.Images.Add(CType(resources.GetObject("grdColumns.Images"), System.Drawing.Image))
        Me.grdColumns.Location = New System.Drawing.Point(28, 210)
        Me.grdColumns.Name = "grdColumns"
        Me.grdColumns.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdColumns.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdColumns.PreviewInfo.ZoomFactor = 75.0R
        Me.grdColumns.PrintInfo.PageSettings = CType(resources.GetObject("grdColumns.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdColumns.Size = New System.Drawing.Size(674, 174)
        Me.grdColumns.TabIndex = 24
        Me.grdColumns.Text = "Colors"
        Me.grdColumns.PropBag = resources.GetString("grdColumns.PropBag")
        '
        'lblSheets
        '
        Me.lblSheets.AutoSize = True
        Me.lblSheets.Location = New System.Drawing.Point(29, 61)
        Me.lblSheets.Name = "lblSheets"
        Me.lblSheets.Size = New System.Drawing.Size(43, 13)
        Me.lblSheets.TabIndex = 23
        Me.lblSheets.Text = "Sheets:"
        '
        'lstSheets
        '
        Me.lstSheets.FormattingEnabled = True
        Me.lstSheets.Location = New System.Drawing.Point(87, 61)
        Me.lstSheets.Name = "lstSheets"
        Me.lstSheets.Size = New System.Drawing.Size(231, 108)
        Me.lstSheets.TabIndex = 22
        '
        'txtPotential
        '
        Me.txtPotential.Location = New System.Drawing.Point(530, 123)
        Me.txtPotential.Name = "txtPotential"
        Me.txtPotential.Size = New System.Drawing.Size(28, 20)
        Me.txtPotential.TabIndex = 21
        '
        'lblPotential
        '
        Me.lblPotential.AutoSize = True
        Me.lblPotential.Location = New System.Drawing.Point(404, 126)
        Me.lblPotential.Name = "lblPotential"
        Me.lblPotential.Size = New System.Drawing.Size(89, 13)
        Me.lblPotential.TabIndex = 20
        Me.lblPotential.Text = "Potential Column:"
        '
        'txtJBA
        '
        Me.txtJBA.Location = New System.Drawing.Point(530, 95)
        Me.txtJBA.Name = "txtJBA"
        Me.txtJBA.Size = New System.Drawing.Size(28, 20)
        Me.txtJBA.TabIndex = 19
        '
        'lblJBA
        '
        Me.lblJBA.AutoSize = True
        Me.lblJBA.Location = New System.Drawing.Point(404, 98)
        Me.lblJBA.Name = "lblJBA"
        Me.lblJBA.Size = New System.Drawing.Size(81, 13)
        Me.lblJBA.TabIndex = 18
        Me.lblJBA.Text = "JBANo Column:"
        '
        'txtSeason
        '
        Me.txtSeason.Location = New System.Drawing.Point(530, 64)
        Me.txtSeason.MaxLength = 3
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.Size = New System.Drawing.Size(28, 20)
        Me.txtSeason.TabIndex = 17
        '
        'lblSeason
        '
        Me.lblSeason.AutoSize = True
        Me.lblSeason.Location = New System.Drawing.Point(404, 67)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(84, 13)
        Me.lblSeason.TabIndex = 16
        Me.lblSeason.Text = "Season Column:"
        '
        'frmUploadPotential
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 502)
        Me.Controls.Add(Me.lblUploading)
        Me.Controls.Add(Me.cmdUpload)
        Me.Controls.Add(Me.grdColumns)
        Me.Controls.Add(Me.lblSheets)
        Me.Controls.Add(Me.lstSheets)
        Me.Controls.Add(Me.txtPotential)
        Me.Controls.Add(Me.lblPotential)
        Me.Controls.Add(Me.txtJBA)
        Me.Controls.Add(Me.lblJBA)
        Me.Controls.Add(Me.txtSeason)
        Me.Controls.Add(Me.lblSeason)
        Me.Controls.Add(Me.lstLog)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Name = "frmUploadPotential"
        Me.Text = "frmUploadPotential"
        CType(Me.grdColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents lstLog As System.Windows.Forms.ListBox
    Friend WithEvents lblUploading As System.Windows.Forms.Label
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents grdColumns As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents lblSheets As System.Windows.Forms.Label
    Friend WithEvents lstSheets As System.Windows.Forms.ListBox
    Friend WithEvents txtPotential As System.Windows.Forms.TextBox
    Friend WithEvents lblPotential As System.Windows.Forms.Label
    Friend WithEvents txtJBA As System.Windows.Forms.TextBox
    Friend WithEvents lblJBA As System.Windows.Forms.Label
    Friend WithEvents txtSeason As System.Windows.Forms.TextBox
    Friend WithEvents lblSeason As System.Windows.Forms.Label
    Friend WithEvents tmrCount As System.Windows.Forms.Timer
End Class
