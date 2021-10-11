<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMailPDF
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMailPDF))
        Me.txtStartdate = New System.Windows.Forms.TextBox
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.List1 = New System.Windows.Forms.ListBox
        Me.btnPrintserver = New System.Windows.Forms.Button
        Me.btnRetrieveOrders = New System.Windows.Forms.Button
        Me.btnFTP = New System.Windows.Forms.Button
        Me.lblPrintBuffer = New System.Windows.Forms.Label
        Me.chkPDF = New System.Windows.Forms.CheckBox
        Me.chkUSA = New System.Windows.Forms.CheckBox
        Me.tmrPrintserver = New System.Windows.Forms.Timer(Me.components)
        Me.tmrFTP = New System.Windows.Forms.Timer(Me.components)
        Me.grdContractors = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.grdStyles = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.grdRefnames = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.txtUsers = New System.Windows.Forms.TextBox
        Me.txtLastFTP = New System.Windows.Forms.TextBox
        Me.FileListBox1 = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.Dir1 = New Microsoft.VisualBasic.Compatibility.VB6.DirListBox
        Me.File1 = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.FilSuppliers = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.filpdfs = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        CType(Me.grdContractors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRefnames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtStartdate
        '
        Me.txtStartdate.Location = New System.Drawing.Point(521, 15)
        Me.txtStartdate.Name = "txtStartdate"
        Me.txtStartdate.Size = New System.Drawing.Size(100, 20)
        Me.txtStartdate.TabIndex = 0
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(12, 12)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(149, 20)
        Me.txtPath.TabIndex = 1
        '
        'List1
        '
        Me.List1.FormattingEnabled = True
        Me.List1.Location = New System.Drawing.Point(12, 178)
        Me.List1.Name = "List1"
        Me.List1.Size = New System.Drawing.Size(149, 277)
        Me.List1.TabIndex = 2
        '
        'btnPrintserver
        '
        Me.btnPrintserver.Location = New System.Drawing.Point(187, 12)
        Me.btnPrintserver.Name = "btnPrintserver"
        Me.btnPrintserver.Size = New System.Drawing.Size(75, 23)
        Me.btnPrintserver.TabIndex = 5
        Me.btnPrintserver.Text = "Printerserver"
        Me.btnPrintserver.UseVisualStyleBackColor = True
        '
        'btnRetrieveOrders
        '
        Me.btnRetrieveOrders.Location = New System.Drawing.Point(268, 12)
        Me.btnRetrieveOrders.Name = "btnRetrieveOrders"
        Me.btnRetrieveOrders.Size = New System.Drawing.Size(75, 23)
        Me.btnRetrieveOrders.TabIndex = 6
        Me.btnRetrieveOrders.Text = "Get Customer Orders"
        Me.btnRetrieveOrders.UseVisualStyleBackColor = True
        '
        'btnFTP
        '
        Me.btnFTP.Location = New System.Drawing.Point(349, 10)
        Me.btnFTP.Name = "btnFTP"
        Me.btnFTP.Size = New System.Drawing.Size(75, 23)
        Me.btnFTP.TabIndex = 7
        Me.btnFTP.Text = "FTP files"
        Me.btnFTP.UseVisualStyleBackColor = True
        '
        'lblPrintBuffer
        '
        Me.lblPrintBuffer.AutoSize = True
        Me.lblPrintBuffer.Location = New System.Drawing.Point(653, 15)
        Me.lblPrintBuffer.Name = "lblPrintBuffer"
        Me.lblPrintBuffer.Size = New System.Drawing.Size(112, 13)
        Me.lblPrintBuffer.TabIndex = 9
        Me.lblPrintBuffer.Text = "0 records in printbuffer"
        '
        'chkPDF
        '
        Me.chkPDF.AutoSize = True
        Me.chkPDF.Location = New System.Drawing.Point(656, 38)
        Me.chkPDF.Name = "chkPDF"
        Me.chkPDF.Size = New System.Drawing.Size(110, 17)
        Me.chkPDF.TabIndex = 10
        Me.chkPDF.Text = "FTP Without PDF"
        Me.chkPDF.UseVisualStyleBackColor = True
        '
        'chkUSA
        '
        Me.chkUSA.AutoSize = True
        Me.chkUSA.Location = New System.Drawing.Point(656, 62)
        Me.chkUSA.Name = "chkUSA"
        Me.chkUSA.Size = New System.Drawing.Size(105, 17)
        Me.chkUSA.TabIndex = 11
        Me.chkUSA.Text = "Ignore US Filelist"
        Me.chkUSA.UseVisualStyleBackColor = True
        '
        'tmrPrintserver
        '
        Me.tmrPrintserver.Interval = 10000
        '
        'grdContractors
        '
        Me.grdContractors.AllowAddNew = True
        Me.grdContractors.AllowColMove = False
        Me.grdContractors.AllowDelete = True
        Me.grdContractors.FilterBar = True
        Me.grdContractors.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdContractors.Images.Add(CType(resources.GetObject("grdContractors.Images"), System.Drawing.Image))
        Me.grdContractors.Location = New System.Drawing.Point(187, 89)
        Me.grdContractors.Name = "grdContractors"
        Me.grdContractors.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdContractors.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdContractors.PreviewInfo.ZoomFactor = 75
        Me.grdContractors.PrintInfo.PageSettings = CType(resources.GetObject("grdContractors.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdContractors.Size = New System.Drawing.Size(794, 200)
        Me.grdContractors.TabIndex = 21
        Me.grdContractors.Text = "C1TrueDBGrid3"
        Me.grdContractors.PropBag = resources.GetString("grdContractors.PropBag")
        '
        'grdStyles
        '
        Me.grdStyles.AllowAddNew = True
        Me.grdStyles.AllowColMove = False
        Me.grdStyles.AllowDelete = True
        Me.grdStyles.FilterBar = True
        Me.grdStyles.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdStyles.Images.Add(CType(resources.GetObject("grdStyles.Images"), System.Drawing.Image))
        Me.grdStyles.Location = New System.Drawing.Point(187, 310)
        Me.grdStyles.Name = "grdStyles"
        Me.grdStyles.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdStyles.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdStyles.PreviewInfo.ZoomFactor = 75
        Me.grdStyles.PrintInfo.PageSettings = CType(resources.GetObject("grdStyles.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdStyles.Size = New System.Drawing.Size(394, 200)
        Me.grdStyles.TabIndex = 22
        Me.grdStyles.Text = "C1TrueDBGrid3"
        Me.grdStyles.PropBag = resources.GetString("grdStyles.PropBag")
        '
        'grdRefnames
        '
        Me.grdRefnames.AllowAddNew = True
        Me.grdRefnames.AllowColMove = False
        Me.grdRefnames.AllowDelete = True
        Me.grdRefnames.FilterBar = True
        Me.grdRefnames.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdRefnames.Images.Add(CType(resources.GetObject("grdRefnames.Images"), System.Drawing.Image))
        Me.grdRefnames.Location = New System.Drawing.Point(587, 310)
        Me.grdRefnames.Name = "grdRefnames"
        Me.grdRefnames.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdRefnames.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdRefnames.PreviewInfo.ZoomFactor = 75
        Me.grdRefnames.PrintInfo.PageSettings = CType(resources.GetObject("grdRefnames.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdRefnames.Size = New System.Drawing.Size(394, 200)
        Me.grdRefnames.TabIndex = 23
        Me.grdRefnames.Text = "C1TrueDBGrid3"
        Me.grdRefnames.PropBag = resources.GetString("grdRefnames.PropBag")
        '
        'txtUsers
        '
        Me.txtUsers.Location = New System.Drawing.Point(187, 526)
        Me.txtUsers.Name = "txtUsers"
        Me.txtUsers.Size = New System.Drawing.Size(100, 20)
        Me.txtUsers.TabIndex = 24
        Me.txtUsers.Text = "<?xml version=""1.0""?><authentication></authentication>"
        '
        'txtLastFTP
        '
        Me.txtLastFTP.Location = New System.Drawing.Point(455, 526)
        Me.txtLastFTP.Name = "txtLastFTP"
        Me.txtLastFTP.Size = New System.Drawing.Size(100, 20)
        Me.txtLastFTP.TabIndex = 26
        '
        'FileListBox1
        '
        Me.FileListBox1.FormattingEnabled = True
        Me.FileListBox1.Location = New System.Drawing.Point(0, 0)
        Me.FileListBox1.Name = "FileListBox1"
        Me.FileListBox1.Pattern = "*.*"
        Me.FileListBox1.Size = New System.Drawing.Size(120, 95)
        Me.FileListBox1.TabIndex = 0
        '
        'Dir1
        '
        Me.Dir1.FormattingEnabled = True
        Me.Dir1.IntegralHeight = False
        Me.Dir1.Location = New System.Drawing.Point(13, 39)
        Me.Dir1.Name = "Dir1"
        Me.Dir1.Size = New System.Drawing.Size(148, 133)
        Me.Dir1.TabIndex = 27
        '
        'File1
        '
        Me.File1.FormattingEnabled = True
        Me.File1.Location = New System.Drawing.Point(12, 462)
        Me.File1.Name = "File1"
        Me.File1.Pattern = "*.*"
        Me.File1.Size = New System.Drawing.Size(149, 134)
        Me.File1.TabIndex = 28
        '
        'FilSuppliers
        '
        Me.FilSuppliers.FormattingEnabled = True
        Me.FilSuppliers.Location = New System.Drawing.Point(304, 526)
        Me.FilSuppliers.Name = "FilSuppliers"
        Me.FilSuppliers.Pattern = "*.*"
        Me.FilSuppliers.Size = New System.Drawing.Size(120, 95)
        Me.FilSuppliers.TabIndex = 29
        '
        'filpdfs
        '
        Me.filpdfs.FormattingEnabled = True
        Me.filpdfs.Location = New System.Drawing.Point(430, 10)
        Me.filpdfs.Name = "filpdfs"
        Me.filpdfs.Pattern = "*.*"
        Me.filpdfs.Size = New System.Drawing.Size(85, 56)
        Me.filpdfs.TabIndex = 30
        '
        'frmMailPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 605)
        Me.Controls.Add(Me.filpdfs)
        Me.Controls.Add(Me.FilSuppliers)
        Me.Controls.Add(Me.File1)
        Me.Controls.Add(Me.Dir1)
        Me.Controls.Add(Me.txtLastFTP)
        Me.Controls.Add(Me.txtUsers)
        Me.Controls.Add(Me.grdRefnames)
        Me.Controls.Add(Me.grdStyles)
        Me.Controls.Add(Me.grdContractors)
        Me.Controls.Add(Me.chkUSA)
        Me.Controls.Add(Me.chkPDF)
        Me.Controls.Add(Me.lblPrintBuffer)
        Me.Controls.Add(Me.btnFTP)
        Me.Controls.Add(Me.btnRetrieveOrders)
        Me.Controls.Add(Me.btnPrintserver)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.txtStartdate)
        Me.Name = "frmMailPDF"
        Me.Text = "frmMailPDF"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdContractors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRefnames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtStartdate As System.Windows.Forms.TextBox
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents List1 As System.Windows.Forms.ListBox
    Friend WithEvents btnPrintserver As System.Windows.Forms.Button
    Friend WithEvents btnRetrieveOrders As System.Windows.Forms.Button
    Friend WithEvents btnFTP As System.Windows.Forms.Button
    Friend WithEvents lblPrintBuffer As System.Windows.Forms.Label
    Friend WithEvents chkPDF As System.Windows.Forms.CheckBox
    Friend WithEvents chkUSA As System.Windows.Forms.CheckBox
    Friend WithEvents tmrPrintserver As System.Windows.Forms.Timer
    Friend WithEvents tmrFTP As System.Windows.Forms.Timer
    Friend WithEvents grdContractors As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents grdStyles As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents grdRefnames As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents txtUsers As System.Windows.Forms.TextBox
    Friend WithEvents txtLastFTP As System.Windows.Forms.TextBox
    Friend WithEvents FileListBox1 As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
    Friend WithEvents Dir1 As Microsoft.VisualBasic.Compatibility.VB6.DirListBox
    Friend WithEvents File1 As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
    Friend WithEvents FilSuppliers As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
    Friend WithEvents filpdfs As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
End Class
