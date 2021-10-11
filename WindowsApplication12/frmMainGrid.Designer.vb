<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainGrid
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainGrid))
        Me.grdMain = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.C1XLBook1 = New C1.C1Excel.C1XLBook()
        Me.mnuCopyDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.pnlComposition = New System.Windows.Forms.Panel()
        Me.btnCompClear = New System.Windows.Forms.Button()
        Me.btnCompCancel = New System.Windows.Forms.Button()
        Me.btnCompOK = New System.Windows.Forms.Button()
        Me.cmbComp6 = New System.Windows.Forms.ComboBox()
        Me.cmbComp5 = New System.Windows.Forms.ComboBox()
        Me.cmbComp4 = New System.Windows.Forms.ComboBox()
        Me.cmbComp3 = New System.Windows.Forms.ComboBox()
        Me.cmbComp2 = New System.Windows.Forms.ComboBox()
        Me.cmbComp1 = New System.Windows.Forms.ComboBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.txtComp6 = New System.Windows.Forms.TextBox()
        Me.txtComp5 = New System.Windows.Forms.TextBox()
        Me.txtComp4 = New System.Windows.Forms.TextBox()
        Me.txtComp3 = New System.Windows.Forms.TextBox()
        Me.txtComp2 = New System.Windows.Forms.TextBox()
        Me.txtComp1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdYouth = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClearFilters = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlMargins = New System.Windows.Forms.Panel()
        Me.lblGrossMarginAll = New System.Windows.Forms.Label()
        Me.lblNrOfColors = New System.Windows.Forms.Label()
        Me.lblActualsSales = New System.Windows.Forms.Label()
        Me.lblGrossMarginCount = New System.Windows.Forms.Label()
        Me.lblGrossMarginOne = New System.Windows.Forms.Label()
        Me.lblGrossMarginFF = New System.Windows.Forms.Label()
        Me.lblGrossMarginAllText = New System.Windows.Forms.Label()
        Me.lblNrOfColorsText = New System.Windows.Forms.Label()
        Me.lblActualsSalesText = New System.Windows.Forms.Label()
        Me.lblGrossMargin = New System.Windows.Forms.Label()
        Me.lblGrossMarginText2 = New System.Windows.Forms.Label()
        Me.picStyle = New System.Windows.Forms.PictureBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.pnlNotes = New System.Windows.Forms.Panel()
        Me.pnlNotesBorder = New System.Windows.Forms.Panel()
        Me.btnNotesOk = New System.Windows.Forms.Button()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.lblBreadCrum = New System.Windows.Forms.Label()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuCopyDown.SuspendLayout()
        Me.pnlComposition.SuspendLayout()
        CType(Me.grdYouth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlMargins.SuspendLayout()
        CType(Me.picStyle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNotes.SuspendLayout()
        Me.pnlNotesBorder.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdMain
        '
        Me.grdMain.FilterBar = True
        Me.grdMain.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdMain.Images.Add(CType(resources.GetObject("grdMain.Images"), System.Drawing.Image))
        Me.grdMain.Location = New System.Drawing.Point(12, 23)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdMain.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdMain.PreviewInfo.ZoomFactor = 75.0R
        Me.grdMain.PrintInfo.PageSettings = CType(resources.GetObject("grdMain.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdMain.Size = New System.Drawing.Size(703, 265)
        Me.grdMain.TabIndex = 0
        Me.grdMain.Text = "grdMain"
        Me.grdMain.PropBag = resources.GetString("grdMain.PropBag")
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportToExcel.Image = CType(resources.GetObject("btnExportToExcel.Image"), System.Drawing.Image)
        Me.btnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExportToExcel.Location = New System.Drawing.Point(12, 478)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(102, 54)
        Me.btnExportToExcel.TabIndex = 11
        Me.btnExportToExcel.Text = "Export to Excel"
        Me.btnExportToExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'mnuCopyDown
        '
        Me.mnuCopyDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyDownToolStripMenuItem, Me.DeleteDateToolStripMenuItem, Me.EditTemplateToolStripMenuItem, Me.DeleteTemplateToolStripMenuItem})
        Me.mnuCopyDown.Name = "mnuCopyDown"
        Me.mnuCopyDown.Size = New System.Drawing.Size(158, 92)
        '
        'CopyDownToolStripMenuItem
        '
        Me.CopyDownToolStripMenuItem.Name = "CopyDownToolStripMenuItem"
        Me.CopyDownToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.CopyDownToolStripMenuItem.Text = "Copy Down"
        Me.CopyDownToolStripMenuItem.Visible = False
        '
        'DeleteDateToolStripMenuItem
        '
        Me.DeleteDateToolStripMenuItem.Name = "DeleteDateToolStripMenuItem"
        Me.DeleteDateToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.DeleteDateToolStripMenuItem.Text = "Delete Date"
        Me.DeleteDateToolStripMenuItem.Visible = False
        '
        'EditTemplateToolStripMenuItem
        '
        Me.EditTemplateToolStripMenuItem.Name = "EditTemplateToolStripMenuItem"
        Me.EditTemplateToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.EditTemplateToolStripMenuItem.Text = "Edit Template"
        Me.EditTemplateToolStripMenuItem.Visible = False
        '
        'DeleteTemplateToolStripMenuItem
        '
        Me.DeleteTemplateToolStripMenuItem.Name = "DeleteTemplateToolStripMenuItem"
        Me.DeleteTemplateToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.DeleteTemplateToolStripMenuItem.Text = "DeleteTemplate"
        Me.DeleteTemplateToolStripMenuItem.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(323, 478)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(102, 54)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh The Records"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(36, 40)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(78, 147)
        Me.ListBox1.TabIndex = 15
        Me.ListBox1.Visible = False
        '
        'pnlComposition
        '
        Me.pnlComposition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlComposition.Controls.Add(Me.btnCompClear)
        Me.pnlComposition.Controls.Add(Me.btnCompCancel)
        Me.pnlComposition.Controls.Add(Me.btnCompOK)
        Me.pnlComposition.Controls.Add(Me.cmbComp6)
        Me.pnlComposition.Controls.Add(Me.cmbComp5)
        Me.pnlComposition.Controls.Add(Me.cmbComp4)
        Me.pnlComposition.Controls.Add(Me.cmbComp3)
        Me.pnlComposition.Controls.Add(Me.cmbComp2)
        Me.pnlComposition.Controls.Add(Me.cmbComp1)
        Me.pnlComposition.Controls.Add(Me.lblTotal)
        Me.pnlComposition.Controls.Add(Me.txtComp6)
        Me.pnlComposition.Controls.Add(Me.txtComp5)
        Me.pnlComposition.Controls.Add(Me.txtComp4)
        Me.pnlComposition.Controls.Add(Me.txtComp3)
        Me.pnlComposition.Controls.Add(Me.txtComp2)
        Me.pnlComposition.Controls.Add(Me.txtComp1)
        Me.pnlComposition.Controls.Add(Me.Label2)
        Me.pnlComposition.Location = New System.Drawing.Point(752, 185)
        Me.pnlComposition.Name = "pnlComposition"
        Me.pnlComposition.Size = New System.Drawing.Size(418, 229)
        Me.pnlComposition.TabIndex = 16
        Me.pnlComposition.Visible = False
        '
        'btnCompClear
        '
        Me.btnCompClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompClear.Location = New System.Drawing.Point(331, 71)
        Me.btnCompClear.Name = "btnCompClear"
        Me.btnCompClear.Size = New System.Drawing.Size(75, 23)
        Me.btnCompClear.TabIndex = 16
        Me.btnCompClear.Text = "Clear"
        Me.btnCompClear.UseVisualStyleBackColor = True
        '
        'btnCompCancel
        '
        Me.btnCompCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompCancel.Location = New System.Drawing.Point(331, 120)
        Me.btnCompCancel.Name = "btnCompCancel"
        Me.btnCompCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCompCancel.TabIndex = 15
        Me.btnCompCancel.Text = "Cancel"
        Me.btnCompCancel.UseVisualStyleBackColor = True
        '
        'btnCompOK
        '
        Me.btnCompOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompOK.Location = New System.Drawing.Point(331, 42)
        Me.btnCompOK.Name = "btnCompOK"
        Me.btnCompOK.Size = New System.Drawing.Size(75, 23)
        Me.btnCompOK.TabIndex = 14
        Me.btnCompOK.Text = "OK"
        Me.btnCompOK.UseVisualStyleBackColor = True
        '
        'cmbComp6
        '
        Me.cmbComp6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp6.FormattingEnabled = True
        Me.cmbComp6.Location = New System.Drawing.Point(85, 171)
        Me.cmbComp6.Name = "cmbComp6"
        Me.cmbComp6.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp6.TabIndex = 13
        '
        'cmbComp5
        '
        Me.cmbComp5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp5.FormattingEnabled = True
        Me.cmbComp5.Location = New System.Drawing.Point(85, 145)
        Me.cmbComp5.Name = "cmbComp5"
        Me.cmbComp5.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp5.TabIndex = 12
        '
        'cmbComp4
        '
        Me.cmbComp4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp4.FormattingEnabled = True
        Me.cmbComp4.Location = New System.Drawing.Point(85, 119)
        Me.cmbComp4.Name = "cmbComp4"
        Me.cmbComp4.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp4.TabIndex = 11
        '
        'cmbComp3
        '
        Me.cmbComp3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp3.FormattingEnabled = True
        Me.cmbComp3.Location = New System.Drawing.Point(85, 93)
        Me.cmbComp3.Name = "cmbComp3"
        Me.cmbComp3.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp3.TabIndex = 10
        '
        'cmbComp2
        '
        Me.cmbComp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp2.FormattingEnabled = True
        Me.cmbComp2.Location = New System.Drawing.Point(85, 67)
        Me.cmbComp2.Name = "cmbComp2"
        Me.cmbComp2.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp2.TabIndex = 9
        '
        'cmbComp1
        '
        Me.cmbComp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComp1.FormattingEnabled = True
        Me.cmbComp1.Location = New System.Drawing.Point(85, 41)
        Me.cmbComp1.Name = "cmbComp1"
        Me.cmbComp1.Size = New System.Drawing.Size(213, 21)
        Me.cmbComp1.TabIndex = 8
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(19, 204)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(0, 13)
        Me.lblTotal.TabIndex = 7
        '
        'txtComp6
        '
        Me.txtComp6.Location = New System.Drawing.Point(14, 172)
        Me.txtComp6.Name = "txtComp6"
        Me.txtComp6.Size = New System.Drawing.Size(64, 20)
        Me.txtComp6.TabIndex = 6
        '
        'txtComp5
        '
        Me.txtComp5.Location = New System.Drawing.Point(14, 146)
        Me.txtComp5.Name = "txtComp5"
        Me.txtComp5.Size = New System.Drawing.Size(64, 20)
        Me.txtComp5.TabIndex = 5
        '
        'txtComp4
        '
        Me.txtComp4.Location = New System.Drawing.Point(14, 120)
        Me.txtComp4.Name = "txtComp4"
        Me.txtComp4.Size = New System.Drawing.Size(64, 20)
        Me.txtComp4.TabIndex = 4
        '
        'txtComp3
        '
        Me.txtComp3.Location = New System.Drawing.Point(14, 94)
        Me.txtComp3.Name = "txtComp3"
        Me.txtComp3.Size = New System.Drawing.Size(64, 20)
        Me.txtComp3.TabIndex = 3
        '
        'txtComp2
        '
        Me.txtComp2.Location = New System.Drawing.Point(14, 68)
        Me.txtComp2.Name = "txtComp2"
        Me.txtComp2.Size = New System.Drawing.Size(64, 20)
        Me.txtComp2.TabIndex = 2
        '
        'txtComp1
        '
        Me.txtComp1.Location = New System.Drawing.Point(14, 42)
        Me.txtComp1.Name = "txtComp1"
        Me.txtComp1.Size = New System.Drawing.Size(64, 20)
        Me.txtComp1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Composition"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(128, 478)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Select Template"
        '
        'grdYouth
        '
        Me.grdYouth.FilterBar = True
        Me.grdYouth.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdYouth.Images.Add(CType(resources.GetObject("grdYouth.Images"), System.Drawing.Image))
        Me.grdYouth.Location = New System.Drawing.Point(12, 332)
        Me.grdYouth.Name = "grdYouth"
        Me.grdYouth.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdYouth.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdYouth.PreviewInfo.ZoomFactor = 75.0R
        Me.grdYouth.PrintInfo.PageSettings = CType(resources.GetObject("grdYouth.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdYouth.Size = New System.Drawing.Size(703, 140)
        Me.grdYouth.TabIndex = 19
        Me.grdYouth.Text = "C1TrueDBGrid1"
        Me.grdYouth.Visible = False
        Me.grdYouth.PropBag = resources.GetString("grdYouth.PropBag")
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(431, 511)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(244, 21)
        Me.ComboBox2.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(431, 478)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Current Filters"
        '
        'btnClearFilters
        '
        Me.btnClearFilters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearFilters.Location = New System.Drawing.Point(681, 495)
        Me.btnClearFilters.Name = "btnClearFilters"
        Me.btnClearFilters.Size = New System.Drawing.Size(102, 37)
        Me.btnClearFilters.TabIndex = 22
        Me.btnClearFilters.Text = "Clear All Filters"
        Me.btnClearFilters.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHistoryToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 26)
        '
        'ShowHistoryToolStripMenuItem
        '
        Me.ShowHistoryToolStripMenuItem.Name = "ShowHistoryToolStripMenuItem"
        Me.ShowHistoryToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.ShowHistoryToolStripMenuItem.Text = "Show History"
        '
        'pnlMargins
        '
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginAll)
        Me.pnlMargins.Controls.Add(Me.lblNrOfColors)
        Me.pnlMargins.Controls.Add(Me.lblActualsSales)
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginCount)
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginOne)
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginFF)
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginAllText)
        Me.pnlMargins.Controls.Add(Me.lblNrOfColorsText)
        Me.pnlMargins.Controls.Add(Me.lblActualsSalesText)
        Me.pnlMargins.Controls.Add(Me.lblGrossMargin)
        Me.pnlMargins.Controls.Add(Me.lblGrossMarginText2)
        Me.pnlMargins.Location = New System.Drawing.Point(789, 437)
        Me.pnlMargins.Name = "pnlMargins"
        Me.pnlMargins.Size = New System.Drawing.Size(291, 95)
        Me.pnlMargins.TabIndex = 31
        Me.pnlMargins.Visible = False
        '
        'lblGrossMarginAll
        '
        Me.lblGrossMarginAll.AutoSize = True
        Me.lblGrossMarginAll.Location = New System.Drawing.Point(122, 0)
        Me.lblGrossMarginAll.Name = "lblGrossMarginAll"
        Me.lblGrossMarginAll.Size = New System.Drawing.Size(0, 13)
        Me.lblGrossMarginAll.TabIndex = 40
        '
        'lblNrOfColors
        '
        Me.lblNrOfColors.AutoSize = True
        Me.lblNrOfColors.Location = New System.Drawing.Point(122, 78)
        Me.lblNrOfColors.Name = "lblNrOfColors"
        Me.lblNrOfColors.Size = New System.Drawing.Size(0, 13)
        Me.lblNrOfColors.TabIndex = 38
        '
        'lblActualsSales
        '
        Me.lblActualsSales.AutoSize = True
        Me.lblActualsSales.Location = New System.Drawing.Point(122, 58)
        Me.lblActualsSales.Name = "lblActualsSales"
        Me.lblActualsSales.Size = New System.Drawing.Size(0, 13)
        Me.lblActualsSales.TabIndex = 36
        '
        'lblGrossMarginCount
        '
        Me.lblGrossMarginCount.AutoSize = True
        Me.lblGrossMarginCount.Location = New System.Drawing.Point(122, 78)
        Me.lblGrossMarginCount.Name = "lblGrossMarginCount"
        Me.lblGrossMarginCount.Size = New System.Drawing.Size(0, 13)
        Me.lblGrossMarginCount.TabIndex = 34
        '
        'lblGrossMarginOne
        '
        Me.lblGrossMarginOne.AutoSize = True
        Me.lblGrossMarginOne.Location = New System.Drawing.Point(122, 38)
        Me.lblGrossMarginOne.Name = "lblGrossMarginOne"
        Me.lblGrossMarginOne.Size = New System.Drawing.Size(0, 13)
        Me.lblGrossMarginOne.TabIndex = 33
        '
        'lblGrossMarginFF
        '
        Me.lblGrossMarginFF.AutoSize = True
        Me.lblGrossMarginFF.Location = New System.Drawing.Point(122, 18)
        Me.lblGrossMarginFF.Name = "lblGrossMarginFF"
        Me.lblGrossMarginFF.Size = New System.Drawing.Size(0, 13)
        Me.lblGrossMarginFF.TabIndex = 32
        '
        'lblGrossMarginAllText
        '
        Me.lblGrossMarginAllText.AutoSize = True
        Me.lblGrossMarginAllText.Location = New System.Drawing.Point(0, 0)
        Me.lblGrossMarginAllText.Name = "lblGrossMarginAllText"
        Me.lblGrossMarginAllText.Size = New System.Drawing.Size(78, 13)
        Me.lblGrossMarginAllText.TabIndex = 39
        Me.lblGrossMarginAllText.Text = "Total Forecast:"
        '
        'lblNrOfColorsText
        '
        Me.lblNrOfColorsText.AutoSize = True
        Me.lblNrOfColorsText.Location = New System.Drawing.Point(0, 78)
        Me.lblNrOfColorsText.Name = "lblNrOfColorsText"
        Me.lblNrOfColorsText.Size = New System.Drawing.Size(108, 13)
        Me.lblNrOfColorsText.TabIndex = 37
        Me.lblNrOfColorsText.Text = "Average Nr of Colors:"
        '
        'lblActualsSalesText
        '
        Me.lblActualsSalesText.AutoSize = True
        Me.lblActualsSalesText.Location = New System.Drawing.Point(0, 58)
        Me.lblActualsSalesText.Name = "lblActualsSalesText"
        Me.lblActualsSalesText.Size = New System.Drawing.Size(107, 13)
        Me.lblActualsSalesText.TabIndex = 35
        Me.lblActualsSalesText.Text = "Gross Margin (Sales):"
        '
        'lblGrossMargin
        '
        Me.lblGrossMargin.AutoSize = True
        Me.lblGrossMargin.Location = New System.Drawing.Point(0, 18)
        Me.lblGrossMargin.Name = "lblGrossMargin"
        Me.lblGrossMargin.Size = New System.Drawing.Size(119, 13)
        Me.lblGrossMargin.TabIndex = 30
        Me.lblGrossMargin.Text = "Gross Margin (forecast):"
        '
        'lblGrossMarginText2
        '
        Me.lblGrossMarginText2.AutoSize = True
        Me.lblGrossMarginText2.Location = New System.Drawing.Point(0, 38)
        Me.lblGrossMarginText2.Name = "lblGrossMarginText2"
        Me.lblGrossMarginText2.Size = New System.Drawing.Size(99, 13)
        Me.lblGrossMarginText2.TabIndex = 31
        Me.lblGrossMarginText2.Text = "Gross Margin (1 pc)"
        '
        'picStyle
        '
        Me.picStyle.Location = New System.Drawing.Point(1086, 454)
        Me.picStyle.Name = "picStyle"
        Me.picStyle.Size = New System.Drawing.Size(70, 80)
        Me.picStyle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picStyle.TabIndex = 32
        Me.picStyle.TabStop = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(131, 511)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(186, 21)
        Me.ComboBox1.TabIndex = 33
        '
        'pnlNotes
        '
        Me.pnlNotes.BackColor = System.Drawing.Color.Transparent
        Me.pnlNotes.Controls.Add(Me.pnlNotesBorder)
        Me.pnlNotes.ForeColor = System.Drawing.Color.Transparent
        Me.pnlNotes.Location = New System.Drawing.Point(12, 2)
        Me.pnlNotes.Name = "pnlNotes"
        Me.pnlNotes.Size = New System.Drawing.Size(1177, 466)
        Me.pnlNotes.TabIndex = 35
        Me.pnlNotes.Visible = False
        '
        'pnlNotesBorder
        '
        Me.pnlNotesBorder.BackColor = System.Drawing.SystemColors.ControlDark
        Me.pnlNotesBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlNotesBorder.Controls.Add(Me.lblBreadCrum)
        Me.pnlNotesBorder.Controls.Add(Me.btnNotesOk)
        Me.pnlNotesBorder.Controls.Add(Me.txtNotes)
        Me.pnlNotesBorder.Location = New System.Drawing.Point(246, 108)
        Me.pnlNotesBorder.Name = "pnlNotesBorder"
        Me.pnlNotesBorder.Size = New System.Drawing.Size(708, 340)
        Me.pnlNotesBorder.TabIndex = 35
        '
        'btnNotesOk
        '
        Me.btnNotesOk.Location = New System.Drawing.Point(612, 293)
        Me.btnNotesOk.Name = "btnNotesOk"
        Me.btnNotesOk.Size = New System.Drawing.Size(81, 40)
        Me.btnNotesOk.TabIndex = 1
        Me.btnNotesOk.Text = "Ok"
        Me.btnNotesOk.UseVisualStyleBackColor = True
        '
        'txtNotes
        '
        Me.txtNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.Location = New System.Drawing.Point(3, 4)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(690, 283)
        Me.txtNotes.TabIndex = 0
        '
        'lblBreadCrum
        '
        Me.lblBreadCrum.AutoSize = True
        Me.lblBreadCrum.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBreadCrum.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.lblBreadCrum.Location = New System.Drawing.Point(16, 294)
        Me.lblBreadCrum.Name = "lblBreadCrum"
        Me.lblBreadCrum.Size = New System.Drawing.Size(0, 18)
        Me.lblBreadCrum.TabIndex = 2
        '
        'frmMainGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1201, 544)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlNotes)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.picStyle)
        Me.Controls.Add(Me.btnClearFilters)
        Me.Controls.Add(Me.grdYouth)
        Me.Controls.Add(Me.pnlMargins)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.pnlComposition)
        Me.Controls.Add(Me.btnExportToExcel)
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.btnRefresh)
        Me.KeyPreview = True
        Me.Name = "frmMainGrid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuCopyDown.ResumeLayout(False)
        Me.pnlComposition.ResumeLayout(False)
        Me.pnlComposition.PerformLayout()
        CType(Me.grdYouth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlMargins.ResumeLayout(False)
        Me.pnlMargins.PerformLayout()
        CType(Me.picStyle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNotes.ResumeLayout(False)
        Me.pnlNotesBorder.ResumeLayout(False)
        Me.pnlNotesBorder.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdMain As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents C1XLBook1 As C1.C1Excel.C1XLBook
    Friend WithEvents mnuCopyDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents pnlComposition As System.Windows.Forms.Panel
    Friend WithEvents txtComp2 As System.Windows.Forms.TextBox
    Friend WithEvents txtComp1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents txtComp6 As System.Windows.Forms.TextBox
    Friend WithEvents txtComp5 As System.Windows.Forms.TextBox
    Friend WithEvents txtComp4 As System.Windows.Forms.TextBox
    Friend WithEvents txtComp3 As System.Windows.Forms.TextBox
    Friend WithEvents btnCompOK As System.Windows.Forms.Button
    Friend WithEvents cmbComp6 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComp5 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComp4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComp3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComp2 As System.Windows.Forms.ComboBox
    Friend WithEvents btnCompCancel As System.Windows.Forms.Button
    Friend WithEvents cmbComp1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnCompClear As System.Windows.Forms.Button
    Friend WithEvents DeleteDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents EditTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grdYouth As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClearFilters As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlMargins As System.Windows.Forms.Panel
    Friend WithEvents lblActualsSalesText As System.Windows.Forms.Label
    Friend WithEvents lblActualsSales As System.Windows.Forms.Label
    Friend WithEvents lblGrossMargin As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginCount As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginText2 As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginOne As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginFF As System.Windows.Forms.Label
    Friend WithEvents picStyle As System.Windows.Forms.PictureBox
    Friend WithEvents lblNrOfColorsText As System.Windows.Forms.Label
    Friend WithEvents lblNrOfColors As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginAllText As System.Windows.Forms.Label
    Friend WithEvents lblGrossMarginAll As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlNotes As System.Windows.Forms.Panel
    Friend WithEvents pnlNotesBorder As System.Windows.Forms.Panel
    Friend WithEvents btnNotesOk As System.Windows.Forms.Button
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblBreadCrum As System.Windows.Forms.Label
End Class
