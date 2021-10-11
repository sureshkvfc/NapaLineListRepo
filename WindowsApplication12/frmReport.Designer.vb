<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReport))
        Me.btnSearchByLine = New System.Windows.Forms.Button()
        Me.lblLineByLine = New System.Windows.Forms.Label()
        Me.cmbLineByLine = New System.Windows.Forms.ComboBox()
        Me.lblSeasonByLine = New System.Windows.Forms.Label()
        Me.cmbSeasonByLine = New System.Windows.Forms.ComboBox()
        Me.lblPM = New System.Windows.Forms.Label()
        Me.cmbPM = New System.Windows.Forms.ComboBox()
        Me.tblLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.tblRight = New System.Windows.Forms.TableLayoutPanel()
        Me.chkAcc = New System.Windows.Forms.CheckBox()
        Me.tblTemplate = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTemplate = New System.Windows.Forms.Label()
        Me.txtTemplate = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.tblExportDir = New System.Windows.Forms.TableLayoutPanel()
        Me.lblExportDir = New System.Windows.Forms.Label()
        Me.txtExportDir = New System.Windows.Forms.TextBox()
        Me.btnBrowseExportDir = New System.Windows.Forms.Button()
        Me.tblFileName = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.tblRow = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddField = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.chkFabrics = New System.Windows.Forms.CheckBox()
        Me.chkTrims = New System.Windows.Forms.CheckBox()
        Me.tabExtra = New System.Windows.Forms.TabControl()
        Me.tabFabrics = New System.Windows.Forms.TabPage()
        Me.tblFabrics = New System.Windows.Forms.TableLayoutPanel()
        Me.tabAcc = New System.Windows.Forms.TabPage()
        Me.tblAcc = New System.Windows.Forms.TableLayoutPanel()
        Me.tabTrims = New System.Windows.Forms.TabPage()
        Me.tblTrims = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.radioBulkPrices = New System.Windows.Forms.RadioButton()
        Me.radioSMSPrices = New System.Windows.Forms.RadioButton()
        Me.tblSelect = New System.Windows.Forms.TableLayoutPanel()
        Me.grdStyles = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.tblLeft = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlArticles = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.cmbDefault = New System.Windows.Forms.ComboBox()
        Me.C1XLBook1 = New C1.C1Excel.C1XLBook()
        Me.tblLayout.SuspendLayout()
        Me.tblRight.SuspendLayout()
        Me.tblTemplate.SuspendLayout()
        Me.tblExportDir.SuspendLayout()
        Me.tblFileName.SuspendLayout()
        Me.tabExtra.SuspendLayout()
        Me.tabFabrics.SuspendLayout()
        Me.tabAcc.SuspendLayout()
        Me.tabTrims.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tblSelect.SuspendLayout()
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblLeft.SuspendLayout()
        Me.pnlArticles.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearchByLine
        '
        Me.btnSearchByLine.Location = New System.Drawing.Point(515, 24)
        Me.btnSearchByLine.Name = "btnSearchByLine"
        Me.btnSearchByLine.Size = New System.Drawing.Size(159, 21)
        Me.btnSearchByLine.TabIndex = 76
        Me.btnSearchByLine.Text = "Search"
        Me.btnSearchByLine.UseVisualStyleBackColor = True
        '
        'lblLineByLine
        '
        Me.lblLineByLine.AutoSize = True
        Me.lblLineByLine.Location = New System.Drawing.Point(177, 9)
        Me.lblLineByLine.Name = "lblLineByLine"
        Me.lblLineByLine.Size = New System.Drawing.Size(27, 13)
        Me.lblLineByLine.TabIndex = 75
        Me.lblLineByLine.Text = "Line"
        '
        'cmbLineByLine
        '
        Me.cmbLineByLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLineByLine.FormattingEnabled = True
        Me.cmbLineByLine.Location = New System.Drawing.Point(180, 25)
        Me.cmbLineByLine.Name = "cmbLineByLine"
        Me.cmbLineByLine.Size = New System.Drawing.Size(159, 21)
        Me.cmbLineByLine.TabIndex = 73
        '
        'lblSeasonByLine
        '
        Me.lblSeasonByLine.AutoSize = True
        Me.lblSeasonByLine.Location = New System.Drawing.Point(12, 9)
        Me.lblSeasonByLine.Name = "lblSeasonByLine"
        Me.lblSeasonByLine.Size = New System.Drawing.Size(43, 13)
        Me.lblSeasonByLine.TabIndex = 74
        Me.lblSeasonByLine.Text = "Season"
        '
        'cmbSeasonByLine
        '
        Me.cmbSeasonByLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSeasonByLine.FormattingEnabled = True
        Me.cmbSeasonByLine.Location = New System.Drawing.Point(15, 25)
        Me.cmbSeasonByLine.Name = "cmbSeasonByLine"
        Me.cmbSeasonByLine.Size = New System.Drawing.Size(159, 21)
        Me.cmbSeasonByLine.TabIndex = 72
        '
        'lblPM
        '
        Me.lblPM.AutoSize = True
        Me.lblPM.Location = New System.Drawing.Point(342, 9)
        Me.lblPM.Name = "lblPM"
        Me.lblPM.Size = New System.Drawing.Size(89, 13)
        Me.lblPM.TabIndex = 78
        Me.lblPM.Text = "Product Manager"
        '
        'cmbPM
        '
        Me.cmbPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPM.FormattingEnabled = True
        Me.cmbPM.Location = New System.Drawing.Point(345, 25)
        Me.cmbPM.Name = "cmbPM"
        Me.cmbPM.Size = New System.Drawing.Size(159, 21)
        Me.cmbPM.TabIndex = 77
        '
        'tblLayout
        '
        Me.tblLayout.ColumnCount = 2
        Me.tblLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 377.0!))
        Me.tblLayout.Controls.Add(Me.tblRight, 1, 0)
        Me.tblLayout.Controls.Add(Me.tblSelect, 0, 0)
        Me.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLayout.Location = New System.Drawing.Point(0, 0)
        Me.tblLayout.Name = "tblLayout"
        Me.tblLayout.RowCount = 1
        Me.tblLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.91741!))
        Me.tblLayout.Size = New System.Drawing.Size(1052, 610)
        Me.tblLayout.TabIndex = 79
        Me.tblLayout.Visible = False
        '
        'tblRight
        '
        Me.tblRight.AutoScroll = True
        Me.tblRight.ColumnCount = 1
        Me.tblRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblRight.Controls.Add(Me.chkAcc, 0, 8)
        Me.tblRight.Controls.Add(Me.tblTemplate, 0, 0)
        Me.tblRight.Controls.Add(Me.btnExport, 0, 3)
        Me.tblRight.Controls.Add(Me.tblExportDir, 0, 1)
        Me.tblRight.Controls.Add(Me.tblFileName, 0, 2)
        Me.tblRight.Controls.Add(Me.tblRow, 0, 6)
        Me.tblRight.Controls.Add(Me.btnAddField, 0, 5)
        Me.tblRight.Controls.Add(Me.btnSave, 0, 4)
        Me.tblRight.Controls.Add(Me.chkFabrics, 0, 9)
        Me.tblRight.Controls.Add(Me.chkTrims, 0, 10)
        Me.tblRight.Controls.Add(Me.tabExtra, 0, 11)
        Me.tblRight.Controls.Add(Me.Panel1, 0, 7)
        Me.tblRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRight.Location = New System.Drawing.Point(678, 3)
        Me.tblRight.Name = "tblRight"
        Me.tblRight.RowCount = 12
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0.0!))
        Me.tblRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRight.Size = New System.Drawing.Size(371, 604)
        Me.tblRight.TabIndex = 22
        '
        'chkAcc
        '
        Me.chkAcc.AutoSize = True
        Me.chkAcc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkAcc.Location = New System.Drawing.Point(3, 538)
        Me.chkAcc.Name = "chkAcc"
        Me.chkAcc.Size = New System.Drawing.Size(365, 17)
        Me.chkAcc.TabIndex = 47
        Me.chkAcc.Text = "Include Personalised Accessories"
        Me.chkAcc.UseVisualStyleBackColor = True
        '
        'tblTemplate
        '
        Me.tblTemplate.ColumnCount = 3
        Me.tblTemplate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.72727!))
        Me.tblTemplate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.27273!))
        Me.tblTemplate.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95.0!))
        Me.tblTemplate.Controls.Add(Me.lblTemplate, 0, 0)
        Me.tblTemplate.Controls.Add(Me.txtTemplate, 1, 0)
        Me.tblTemplate.Controls.Add(Me.btnBrowse, 2, 0)
        Me.tblTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTemplate.Location = New System.Drawing.Point(3, 3)
        Me.tblTemplate.Name = "tblTemplate"
        Me.tblTemplate.RowCount = 1
        Me.tblTemplate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblTemplate.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tblTemplate.Size = New System.Drawing.Size(365, 27)
        Me.tblTemplate.TabIndex = 26
        '
        'lblTemplate
        '
        Me.lblTemplate.AutoSize = True
        Me.lblTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTemplate.Location = New System.Drawing.Point(3, 0)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(55, 27)
        Me.lblTemplate.TabIndex = 1
        Me.lblTemplate.Text = "Template:"
        Me.lblTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTemplate
        '
        Me.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTemplate.Enabled = False
        Me.txtTemplate.Location = New System.Drawing.Point(64, 3)
        Me.txtTemplate.Name = "txtTemplate"
        Me.txtTemplate.Size = New System.Drawing.Size(202, 20)
        Me.txtTemplate.TabIndex = 2
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(272, 3)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(60, 21)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExport.Location = New System.Drawing.Point(3, 102)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(365, 27)
        Me.btnExport.TabIndex = 36
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'tblExportDir
        '
        Me.tblExportDir.ColumnCount = 3
        Me.tblExportDir.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.56643!))
        Me.tblExportDir.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.43356!))
        Me.tblExportDir.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94.0!))
        Me.tblExportDir.Controls.Add(Me.lblExportDir, 0, 0)
        Me.tblExportDir.Controls.Add(Me.txtExportDir, 1, 0)
        Me.tblExportDir.Controls.Add(Me.btnBrowseExportDir, 2, 0)
        Me.tblExportDir.Location = New System.Drawing.Point(3, 36)
        Me.tblExportDir.Name = "tblExportDir"
        Me.tblExportDir.RowCount = 1
        Me.tblExportDir.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblExportDir.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.tblExportDir.Size = New System.Drawing.Size(365, 27)
        Me.tblExportDir.TabIndex = 39
        '
        'lblExportDir
        '
        Me.lblExportDir.AutoSize = True
        Me.lblExportDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExportDir.Location = New System.Drawing.Point(3, 0)
        Me.lblExportDir.Name = "lblExportDir"
        Me.lblExportDir.Size = New System.Drawing.Size(84, 27)
        Me.lblExportDir.TabIndex = 1
        Me.lblExportDir.Text = "Export Directory:"
        Me.lblExportDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExportDir
        '
        Me.txtExportDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExportDir.Enabled = False
        Me.txtExportDir.Location = New System.Drawing.Point(93, 3)
        Me.txtExportDir.Name = "txtExportDir"
        Me.txtExportDir.Size = New System.Drawing.Size(174, 20)
        Me.txtExportDir.TabIndex = 2
        Me.txtExportDir.Text = "\\vebb2a24\BEBORQUESTPDM\Linelist\Napa\NAPAFiles\"
        '
        'btnBrowseExportDir
        '
        Me.btnBrowseExportDir.Location = New System.Drawing.Point(273, 3)
        Me.btnBrowseExportDir.Name = "btnBrowseExportDir"
        Me.btnBrowseExportDir.Size = New System.Drawing.Size(61, 21)
        Me.btnBrowseExportDir.TabIndex = 3
        Me.btnBrowseExportDir.Text = "Browse..."
        Me.btnBrowseExportDir.UseVisualStyleBackColor = True
        '
        'tblFileName
        '
        Me.tblFileName.ColumnCount = 2
        Me.tblFileName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.60274!))
        Me.tblFileName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.39726!))
        Me.tblFileName.Controls.Add(Me.lblFileName, 0, 0)
        Me.tblFileName.Controls.Add(Me.txtFileName, 1, 0)
        Me.tblFileName.Location = New System.Drawing.Point(3, 69)
        Me.tblFileName.Name = "tblFileName"
        Me.tblFileName.RowCount = 1
        Me.tblFileName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFileName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tblFileName.Size = New System.Drawing.Size(365, 25)
        Me.tblFileName.TabIndex = 43
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFileName.Location = New System.Drawing.Point(3, 0)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(76, 25)
        Me.lblFileName.TabIndex = 1
        Me.lblFileName.Text = "File Name:"
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFileName
        '
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFileName.Location = New System.Drawing.Point(85, 3)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(277, 20)
        Me.txtFileName.TabIndex = 2
        '
        'tblRow
        '
        Me.tblRow.AutoScroll = True
        Me.tblRow.AutoSize = True
        Me.tblRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblRow.ColumnCount = 5
        Me.tblRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.tblRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.tblRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tblRow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRow.Location = New System.Drawing.Point(3, 195)
        Me.tblRow.Name = "tblRow"
        Me.tblRow.RowCount = 1
        Me.tblRow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRow.Size = New System.Drawing.Size(365, 314)
        Me.tblRow.TabIndex = 38
        '
        'btnAddField
        '
        Me.btnAddField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddField.Location = New System.Drawing.Point(3, 168)
        Me.btnAddField.Name = "btnAddField"
        Me.btnAddField.Size = New System.Drawing.Size(365, 21)
        Me.btnAddField.TabIndex = 36
        Me.btnAddField.Text = "Add Field..."
        Me.btnAddField.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSave.Location = New System.Drawing.Point(3, 135)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(365, 27)
        Me.btnSave.TabIndex = 44
        Me.btnSave.Text = "Save Template"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'chkFabrics
        '
        Me.chkFabrics.AutoSize = True
        Me.chkFabrics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkFabrics.Location = New System.Drawing.Point(3, 561)
        Me.chkFabrics.Name = "chkFabrics"
        Me.chkFabrics.Size = New System.Drawing.Size(365, 17)
        Me.chkFabrics.TabIndex = 45
        Me.chkFabrics.Text = "Include Fabrics"
        Me.chkFabrics.UseVisualStyleBackColor = True
        '
        'chkTrims
        '
        Me.chkTrims.AutoSize = True
        Me.chkTrims.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkTrims.Location = New System.Drawing.Point(3, 584)
        Me.chkTrims.Name = "chkTrims"
        Me.chkTrims.Size = New System.Drawing.Size(365, 17)
        Me.chkTrims.TabIndex = 49
        Me.chkTrims.Text = "Include Print and Embroidery"
        Me.chkTrims.UseVisualStyleBackColor = True
        '
        'tabExtra
        '
        Me.tabExtra.Controls.Add(Me.tabFabrics)
        Me.tabExtra.Controls.Add(Me.tabAcc)
        Me.tabExtra.Controls.Add(Me.tabTrims)
        Me.tabExtra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabExtra.Location = New System.Drawing.Point(3, 607)
        Me.tabExtra.Name = "tabExtra"
        Me.tabExtra.SelectedIndex = 0
        Me.tabExtra.Size = New System.Drawing.Size(365, 1)
        Me.tabExtra.TabIndex = 52
        '
        'tabFabrics
        '
        Me.tabFabrics.Controls.Add(Me.tblFabrics)
        Me.tabFabrics.Location = New System.Drawing.Point(4, 22)
        Me.tabFabrics.Name = "tabFabrics"
        Me.tabFabrics.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFabrics.Size = New System.Drawing.Size(357, 0)
        Me.tabFabrics.TabIndex = 0
        Me.tabFabrics.Text = "Fabrics"
        Me.tabFabrics.UseVisualStyleBackColor = True
        '
        'tblFabrics
        '
        Me.tblFabrics.ColumnCount = 3
        Me.tblFabrics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFabrics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.tblFabrics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tblFabrics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFabrics.Location = New System.Drawing.Point(3, 3)
        Me.tblFabrics.Name = "tblFabrics"
        Me.tblFabrics.RowCount = 1
        Me.tblFabrics.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFabrics.Size = New System.Drawing.Size(351, 0)
        Me.tblFabrics.TabIndex = 52
        '
        'tabAcc
        '
        Me.tabAcc.Controls.Add(Me.tblAcc)
        Me.tabAcc.Location = New System.Drawing.Point(4, 22)
        Me.tabAcc.Name = "tabAcc"
        Me.tabAcc.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAcc.Size = New System.Drawing.Size(357, 0)
        Me.tabAcc.TabIndex = 1
        Me.tabAcc.Text = "Accessories"
        Me.tabAcc.UseVisualStyleBackColor = True
        '
        'tblAcc
        '
        Me.tblAcc.ColumnCount = 3
        Me.tblAcc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblAcc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.tblAcc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81.0!))
        Me.tblAcc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblAcc.Location = New System.Drawing.Point(3, 3)
        Me.tblAcc.Name = "tblAcc"
        Me.tblAcc.RowCount = 1
        Me.tblAcc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAcc.Size = New System.Drawing.Size(351, 0)
        Me.tblAcc.TabIndex = 49
        '
        'tabTrims
        '
        Me.tabTrims.Controls.Add(Me.tblTrims)
        Me.tabTrims.Location = New System.Drawing.Point(4, 22)
        Me.tabTrims.Name = "tabTrims"
        Me.tabTrims.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTrims.Size = New System.Drawing.Size(357, 0)
        Me.tabTrims.TabIndex = 2
        Me.tabTrims.Text = "Print and Embroidery "
        Me.tabTrims.UseVisualStyleBackColor = True
        '
        'tblTrims
        '
        Me.tblTrims.ColumnCount = 3
        Me.tblTrims.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTrims.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.tblTrims.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81.0!))
        Me.tblTrims.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTrims.Location = New System.Drawing.Point(3, 3)
        Me.tblTrims.Name = "tblTrims"
        Me.tblTrims.RowCount = 1
        Me.tblTrims.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrims.Size = New System.Drawing.Size(351, 0)
        Me.tblTrims.TabIndex = 51
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.radioBulkPrices)
        Me.Panel1.Controls.Add(Me.radioSMSPrices)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 515)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(365, 17)
        Me.Panel1.TabIndex = 53
        '
        'radioBulkPrices
        '
        Me.radioBulkPrices.AutoSize = True
        Me.radioBulkPrices.Location = New System.Drawing.Point(287, 0)
        Me.radioBulkPrices.Name = "radioBulkPrices"
        Me.radioBulkPrices.Size = New System.Drawing.Size(78, 17)
        Me.radioBulkPrices.TabIndex = 58
        Me.radioBulkPrices.TabStop = True
        Me.radioBulkPrices.Text = "Bulk Prices"
        Me.radioBulkPrices.UseVisualStyleBackColor = True
        '
        'radioSMSPrices
        '
        Me.radioSMSPrices.AutoSize = True
        Me.radioSMSPrices.Location = New System.Drawing.Point(0, 0)
        Me.radioSMSPrices.Name = "radioSMSPrices"
        Me.radioSMSPrices.Size = New System.Drawing.Size(80, 17)
        Me.radioSMSPrices.TabIndex = 57
        Me.radioSMSPrices.TabStop = True
        Me.radioSMSPrices.Text = "SMS Prices"
        Me.radioSMSPrices.UseVisualStyleBackColor = True
        '
        'tblSelect
        '
        Me.tblSelect.ColumnCount = 1
        Me.tblSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSelect.Controls.Add(Me.grdStyles, 0, 0)
        Me.tblSelect.Controls.Add(Me.tblLeft, 0, 1)
        Me.tblSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSelect.Location = New System.Drawing.Point(3, 3)
        Me.tblSelect.Name = "tblSelect"
        Me.tblSelect.RowCount = 2
        Me.tblSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.16239!))
        Me.tblSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.837607!))
        Me.tblSelect.Size = New System.Drawing.Size(669, 604)
        Me.tblSelect.TabIndex = 23
        '
        'grdStyles
        '
        Me.grdStyles.AllowAddNew = True
        Me.grdStyles.AllowColMove = False
        Me.grdStyles.AllowDelete = True
        Me.grdStyles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStyles.FilterBar = True
        Me.grdStyles.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdStyles.Images.Add(CType(resources.GetObject("grdStyles.Images"), System.Drawing.Image))
        Me.grdStyles.Location = New System.Drawing.Point(3, 3)
        Me.grdStyles.Name = "grdStyles"
        Me.grdStyles.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdStyles.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdStyles.PreviewInfo.ZoomFactor = 75.0R
        Me.grdStyles.PrintInfo.PageSettings = CType(resources.GetObject("grdStyles.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdStyles.Size = New System.Drawing.Size(663, 556)
        Me.grdStyles.TabIndex = 22
        Me.grdStyles.Text = "C1TrueDBGrid3"
        Me.grdStyles.PropBag = resources.GetString("grdStyles.PropBag")
        '
        'tblLeft
        '
        Me.tblLeft.ColumnCount = 2
        Me.tblLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLeft.Controls.Add(Me.btnDeselectAll, 0, 0)
        Me.tblLeft.Controls.Add(Me.btnSelectAll, 0, 0)
        Me.tblLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLeft.Location = New System.Drawing.Point(3, 565)
        Me.tblLeft.Name = "tblLeft"
        Me.tblLeft.RowCount = 1
        Me.tblLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLeft.Size = New System.Drawing.Size(663, 36)
        Me.tblLeft.TabIndex = 23
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeselectAll.Location = New System.Drawing.Point(562, 3)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(98, 30)
        Me.btnDeselectAll.TabIndex = 32
        Me.btnDeselectAll.Tag = "0"
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 3)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(98, 30)
        Me.btnSelectAll.TabIndex = 31
        Me.btnSelectAll.Tag = "1"
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'pnlArticles
        '
        Me.pnlArticles.ColumnCount = 3
        Me.pnlArticles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.pnlArticles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.pnlArticles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.pnlArticles.Controls.Add(Me.btnGenerate, 2, 0)
        Me.pnlArticles.Controls.Add(Me.cmbDefault, 1, 0)
        Me.pnlArticles.Location = New System.Drawing.Point(77, 0)
        Me.pnlArticles.Name = "pnlArticles"
        Me.pnlArticles.RowCount = 2
        Me.pnlArticles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.pnlArticles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.pnlArticles.Size = New System.Drawing.Size(514, 114)
        Me.pnlArticles.TabIndex = 80
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(103, 3)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 0
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'cmbDefault
        '
        Me.cmbDefault.FormattingEnabled = True
        Me.cmbDefault.Items.AddRange(New Object() {"", "<", ">", "=", "<=", ">=", "LIKE", "BETWEEN", "IN", "NOT IN"})
        Me.cmbDefault.Location = New System.Drawing.Point(3, 3)
        Me.cmbDefault.Name = "cmbDefault"
        Me.cmbDefault.Size = New System.Drawing.Size(83, 21)
        Me.cmbDefault.TabIndex = 1
        Me.cmbDefault.Visible = False
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 610)
        Me.Controls.Add(Me.pnlArticles)
        Me.Controls.Add(Me.tblLayout)
        Me.Controls.Add(Me.lblPM)
        Me.Controls.Add(Me.cmbPM)
        Me.Controls.Add(Me.btnSearchByLine)
        Me.Controls.Add(Me.lblLineByLine)
        Me.Controls.Add(Me.cmbLineByLine)
        Me.Controls.Add(Me.lblSeasonByLine)
        Me.Controls.Add(Me.cmbSeasonByLine)
        Me.Name = "frmReport"
        Me.Text = "Report by Line"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tblLayout.ResumeLayout(False)
        Me.tblRight.ResumeLayout(False)
        Me.tblRight.PerformLayout()
        Me.tblTemplate.ResumeLayout(False)
        Me.tblTemplate.PerformLayout()
        Me.tblExportDir.ResumeLayout(False)
        Me.tblExportDir.PerformLayout()
        Me.tblFileName.ResumeLayout(False)
        Me.tblFileName.PerformLayout()
        Me.tabExtra.ResumeLayout(False)
        Me.tabFabrics.ResumeLayout(False)
        Me.tabAcc.ResumeLayout(False)
        Me.tabTrims.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblSelect.ResumeLayout(False)
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblLeft.ResumeLayout(False)
        Me.pnlArticles.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSearchByLine As System.Windows.Forms.Button
    Friend WithEvents lblLineByLine As System.Windows.Forms.Label
    Friend WithEvents cmbLineByLine As System.Windows.Forms.ComboBox
    Friend WithEvents lblSeasonByLine As System.Windows.Forms.Label
    Friend WithEvents cmbSeasonByLine As System.Windows.Forms.ComboBox
    Friend WithEvents lblPM As System.Windows.Forms.Label
    Friend WithEvents cmbPM As System.Windows.Forms.ComboBox
    Friend WithEvents tblLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSelect As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdStyles As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents tblLeft As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblRight As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkAcc As System.Windows.Forms.CheckBox
    Friend WithEvents tblTemplate As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents txtTemplate As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents tblExportDir As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblExportDir As System.Windows.Forms.Label
    Friend WithEvents txtExportDir As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseExportDir As System.Windows.Forms.Button
    Friend WithEvents tblFileName As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents tblRow As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAddField As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents chkFabrics As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrims As System.Windows.Forms.CheckBox
    Friend WithEvents tabExtra As System.Windows.Forms.TabControl
    Friend WithEvents tabFabrics As System.Windows.Forms.TabPage
    Friend WithEvents tblFabrics As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tabAcc As System.Windows.Forms.TabPage
    Friend WithEvents tblAcc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tabTrims As System.Windows.Forms.TabPage
    Friend WithEvents tblTrims As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents radioBulkPrices As System.Windows.Forms.RadioButton
    Friend WithEvents radioSMSPrices As System.Windows.Forms.RadioButton
    Friend WithEvents pnlArticles As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents cmbDefault As System.Windows.Forms.ComboBox
    Friend WithEvents C1XLBook1 As C1.C1Excel.C1XLBook
End Class
