<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCollection))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbUse = New System.Windows.Forms.ComboBox()
        Me.btnAddColumn = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbLine = New System.Windows.Forms.ComboBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSavePrest = New System.Windows.Forms.Button()
        Me.cmbMajor = New System.Windows.Forms.ComboBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSeason = New System.Windows.Forms.ComboBox()
        Me.btnCreateRecords = New System.Windows.Forms.Button()
        Me.grdStyles = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.pnlAddColumn = New System.Windows.Forms.GroupBox()
        Me.btnCancelColumn = New System.Windows.Forms.Button()
        Me.btnSaveColumn = New System.Windows.Forms.Button()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlAddDeleteRecords = New System.Windows.Forms.Panel()
        Me.grdAddDeleteRecords = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteThisColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnDeleteCollection = New System.Windows.Forms.Button()
        Me.cmdExcel = New System.Windows.Forms.Button()
        Me.C1XLBook1 = New C1.C1Excel.C1XLBook()
        Me.cmdUpdateFreeze = New System.Windows.Forms.Button()
        Me.pnlUpdateFreeze = New System.Windows.Forms.GroupBox()
        Me.cmbFreeze = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdCancelFreeze = New System.Windows.Forms.Button()
        Me.cmdSaveFreeze = New System.Windows.Forms.Button()
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAddColumn.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAddDeleteRecords.SuspendLayout()
        CType(Me.grdAddDeleteRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlUpdateFreeze.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(338, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Use"
        '
        'cmbUse
        '
        Me.cmbUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUse.FormattingEnabled = True
        Me.cmbUse.Location = New System.Drawing.Point(340, 26)
        Me.cmbUse.Name = "cmbUse"
        Me.cmbUse.Size = New System.Drawing.Size(159, 21)
        Me.cmbUse.TabIndex = 26
        '
        'btnAddColumn
        '
        Me.btnAddColumn.Location = New System.Drawing.Point(670, 78)
        Me.btnAddColumn.Name = "btnAddColumn"
        Me.btnAddColumn.Size = New System.Drawing.Size(159, 21)
        Me.btnAddColumn.TabIndex = 34
        Me.btnAddColumn.Text = "Add a New Column"
        Me.btnAddColumn.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(172, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Line"
        '
        'cmbLine
        '
        Me.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLine.FormattingEnabled = True
        Me.cmbLine.Location = New System.Drawing.Point(175, 26)
        Me.cmbLine.Name = "cmbLine"
        Me.cmbLine.Size = New System.Drawing.Size(159, 21)
        Me.cmbLine.TabIndex = 24
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(10, 77)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(489, 21)
        Me.ComboBox4.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Your Presets:"
        '
        'btnSavePrest
        '
        Me.btnSavePrest.Location = New System.Drawing.Point(846, 22)
        Me.btnSavePrest.Name = "btnSavePrest"
        Me.btnSavePrest.Size = New System.Drawing.Size(159, 21)
        Me.btnSavePrest.TabIndex = 32
        Me.btnSavePrest.Text = "Save This Preset"
        Me.btnSavePrest.UseVisualStyleBackColor = True
        '
        'cmbMajor
        '
        Me.cmbMajor.DisplayMember = "MOC039"
        Me.cmbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMajor.FormattingEnabled = True
        Me.cmbMajor.Location = New System.Drawing.Point(670, 26)
        Me.cmbMajor.Name = "cmbMajor"
        Me.cmbMajor.Size = New System.Drawing.Size(159, 21)
        Me.cmbMajor.TabIndex = 29
        Me.cmbMajor.ValueMember = "MOC039"
        '
        'cmbGender
        '
        Me.cmbGender.DisplayMember = "GenderCode"
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Location = New System.Drawing.Point(505, 26)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(159, 21)
        Me.cmbGender.TabIndex = 28
        Me.cmbGender.ValueMember = "GenderCode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(667, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Major"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(502, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Gender"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Season"
        '
        'cmbSeason
        '
        Me.cmbSeason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSeason.FormattingEnabled = True
        Me.cmbSeason.Location = New System.Drawing.Point(10, 26)
        Me.cmbSeason.Name = "cmbSeason"
        Me.cmbSeason.Size = New System.Drawing.Size(159, 21)
        Me.cmbSeason.TabIndex = 23
        '
        'btnCreateRecords
        '
        Me.btnCreateRecords.Enabled = False
        Me.btnCreateRecords.Location = New System.Drawing.Point(846, 53)
        Me.btnCreateRecords.Name = "btnCreateRecords"
        Me.btnCreateRecords.Size = New System.Drawing.Size(159, 21)
        Me.btnCreateRecords.TabIndex = 33
        Me.btnCreateRecords.Text = "Create The Records"
        Me.btnCreateRecords.UseVisualStyleBackColor = True
        '
        'grdStyles
        '
        Me.grdStyles.AllowRowSelect = False
        Me.grdStyles.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdStyles.Images.Add(CType(resources.GetObject("grdStyles.Images"), System.Drawing.Image))
        Me.grdStyles.Location = New System.Drawing.Point(12, 114)
        Me.grdStyles.Name = "grdStyles"
        Me.grdStyles.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdStyles.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdStyles.PreviewInfo.ZoomFactor = 75.0R
        Me.grdStyles.PrintInfo.PageSettings = CType(resources.GetObject("grdStyles.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdStyles.Size = New System.Drawing.Size(532, 246)
        Me.grdStyles.TabIndex = 38
        Me.grdStyles.Text = "C1TrueDBGrid3"
        Me.grdStyles.WrapCellPointer = True
        Me.grdStyles.PropBag = resources.GetString("grdStyles.PropBag")
        '
        'pnlAddColumn
        '
        Me.pnlAddColumn.Controls.Add(Me.btnCancelColumn)
        Me.pnlAddColumn.Controls.Add(Me.btnSaveColumn)
        Me.pnlAddColumn.Controls.Add(Me.NumericUpDown1)
        Me.pnlAddColumn.Controls.Add(Me.Label10)
        Me.pnlAddColumn.Controls.Add(Me.RadioButton3)
        Me.pnlAddColumn.Controls.Add(Me.RadioButton2)
        Me.pnlAddColumn.Controls.Add(Me.RadioButton1)
        Me.pnlAddColumn.Controls.Add(Me.Label9)
        Me.pnlAddColumn.Controls.Add(Me.TextBox1)
        Me.pnlAddColumn.Controls.Add(Me.Label8)
        Me.pnlAddColumn.Controls.Add(Me.Label7)
        Me.pnlAddColumn.Location = New System.Drawing.Point(550, 101)
        Me.pnlAddColumn.Name = "pnlAddColumn"
        Me.pnlAddColumn.Size = New System.Drawing.Size(509, 299)
        Me.pnlAddColumn.TabIndex = 39
        Me.pnlAddColumn.TabStop = False
        Me.pnlAddColumn.Visible = False
        '
        'btnCancelColumn
        '
        Me.btnCancelColumn.Location = New System.Drawing.Point(347, 270)
        Me.btnCancelColumn.Name = "btnCancelColumn"
        Me.btnCancelColumn.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelColumn.TabIndex = 11
        Me.btnCancelColumn.Text = "Cancel"
        Me.btnCancelColumn.UseVisualStyleBackColor = True
        '
        'btnSaveColumn
        '
        Me.btnSaveColumn.Location = New System.Drawing.Point(428, 270)
        Me.btnSaveColumn.Name = "btnSaveColumn"
        Me.btnSaveColumn.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveColumn.TabIndex = 10
        Me.btnSaveColumn.Text = "Save"
        Me.btnSaveColumn.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(150, 158)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(70, 20)
        Me.NumericUpDown1.TabIndex = 9
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(104, 164)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Length"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(20, 244)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(48, 17)
        Me.RadioButton3.TabIndex = 6
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Date"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(20, 202)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(64, 17)
        Me.RadioButton2.TabIndex = 5
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Numeric"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(20, 164)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(46, 17)
        Me.RadioButton1.TabIndex = 4
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Text"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 117)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(161, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Select The Type for this Column:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(20, 68)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(469, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "(Please don't use Spaces)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(153, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Give the Name for this Column."
        '
        'pnlAddDeleteRecords
        '
        Me.pnlAddDeleteRecords.Controls.Add(Me.grdAddDeleteRecords)
        Me.pnlAddDeleteRecords.Controls.Add(Me.ToolStrip3)
        Me.pnlAddDeleteRecords.Location = New System.Drawing.Point(175, 371)
        Me.pnlAddDeleteRecords.Name = "pnlAddDeleteRecords"
        Me.pnlAddDeleteRecords.Size = New System.Drawing.Size(574, 292)
        Me.pnlAddDeleteRecords.TabIndex = 40
        Me.pnlAddDeleteRecords.Visible = False
        '
        'grdAddDeleteRecords
        '
        Me.grdAddDeleteRecords.AllowUpdate = False
        Me.grdAddDeleteRecords.AllowUpdateOnBlur = False
        Me.grdAddDeleteRecords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAddDeleteRecords.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdAddDeleteRecords.Images.Add(CType(resources.GetObject("grdAddDeleteRecords.Images"), System.Drawing.Image))
        Me.grdAddDeleteRecords.Location = New System.Drawing.Point(0, 25)
        Me.grdAddDeleteRecords.Name = "grdAddDeleteRecords"
        Me.grdAddDeleteRecords.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdAddDeleteRecords.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdAddDeleteRecords.PreviewInfo.ZoomFactor = 75.0R
        Me.grdAddDeleteRecords.PrintInfo.PageSettings = CType(resources.GetObject("grdAddDeleteRecords.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdAddDeleteRecords.Size = New System.Drawing.Size(574, 267)
        Me.grdAddDeleteRecords.TabIndex = 19
        Me.grdAddDeleteRecords.Text = "C1TrueDBGrid4"
        Me.grdAddDeleteRecords.PropBag = resources.GetString("grdAddDeleteRecords.PropBag")
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripDropDownButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(574, 25)
        Me.ToolStrip3.TabIndex = 18
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "X"
        '
        'ToolStripDropDownButton2
        '
        Me.ToolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.ToolStripDropDownButton2.Image = CType(resources.GetObject("ToolStripDropDownButton2.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(29, 22)
        Me.ToolStripDropDownButton2.Text = "ToolStripDropDownButton3"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(103, 22)
        Me.ToolStripMenuItem1.Text = "Close"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteThisColumnToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(179, 26)
        '
        'DeleteThisColumnToolStripMenuItem
        '
        Me.DeleteThisColumnToolStripMenuItem.Name = "DeleteThisColumnToolStripMenuItem"
        Me.DeleteThisColumnToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DeleteThisColumnToolStripMenuItem.Text = "Delete This Column"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(851, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 13)
        Me.Label11.TabIndex = 41
        Me.Label11.Visible = False
        '
        'btnDeleteCollection
        '
        Me.btnDeleteCollection.Location = New System.Drawing.Point(505, 78)
        Me.btnDeleteCollection.Name = "btnDeleteCollection"
        Me.btnDeleteCollection.Size = New System.Drawing.Size(159, 21)
        Me.btnDeleteCollection.TabIndex = 42
        Me.btnDeleteCollection.Text = "Delete This Collection"
        Me.btnDeleteCollection.UseVisualStyleBackColor = True
        '
        'cmdExcel
        '
        Me.cmdExcel.Location = New System.Drawing.Point(670, 53)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(159, 21)
        Me.cmdExcel.TabIndex = 43
        Me.cmdExcel.Text = "Export To Excel"
        Me.cmdExcel.UseVisualStyleBackColor = True
        '
        'cmdUpdateFreeze
        '
        Me.cmdUpdateFreeze.Location = New System.Drawing.Point(846, 78)
        Me.cmdUpdateFreeze.Name = "cmdUpdateFreeze"
        Me.cmdUpdateFreeze.Size = New System.Drawing.Size(159, 21)
        Me.cmdUpdateFreeze.TabIndex = 44
        Me.cmdUpdateFreeze.Text = "Update Freeze"
        Me.cmdUpdateFreeze.UseVisualStyleBackColor = True
        '
        'pnlUpdateFreeze
        '
        Me.pnlUpdateFreeze.Controls.Add(Me.cmbFreeze)
        Me.pnlUpdateFreeze.Controls.Add(Me.Label12)
        Me.pnlUpdateFreeze.Controls.Add(Me.cmdCancelFreeze)
        Me.pnlUpdateFreeze.Controls.Add(Me.cmdSaveFreeze)
        Me.pnlUpdateFreeze.Location = New System.Drawing.Point(656, 3)
        Me.pnlUpdateFreeze.Name = "pnlUpdateFreeze"
        Me.pnlUpdateFreeze.Size = New System.Drawing.Size(349, 96)
        Me.pnlUpdateFreeze.TabIndex = 40
        Me.pnlUpdateFreeze.TabStop = False
        Me.pnlUpdateFreeze.Visible = False
        '
        'cmbFreeze
        '
        Me.cmbFreeze.DisplayMember = "GenderCode"
        Me.cmbFreeze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreeze.FormattingEnabled = True
        Me.cmbFreeze.Location = New System.Drawing.Point(26, 32)
        Me.cmbFreeze.Name = "cmbFreeze"
        Me.cmbFreeze.Size = New System.Drawing.Size(307, 21)
        Me.cmbFreeze.TabIndex = 29
        Me.cmbFreeze.ValueMember = "GenderCode"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(23, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(113, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Select Freeze Column:"
        '
        'cmdCancelFreeze
        '
        Me.cmdCancelFreeze.Location = New System.Drawing.Point(144, 59)
        Me.cmdCancelFreeze.Name = "cmdCancelFreeze"
        Me.cmdCancelFreeze.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancelFreeze.TabIndex = 11
        Me.cmdCancelFreeze.Text = "Cancel"
        Me.cmdCancelFreeze.UseVisualStyleBackColor = True
        '
        'cmdSaveFreeze
        '
        Me.cmdSaveFreeze.Location = New System.Drawing.Point(258, 58)
        Me.cmdSaveFreeze.Name = "cmdSaveFreeze"
        Me.cmdSaveFreeze.Size = New System.Drawing.Size(75, 23)
        Me.cmdSaveFreeze.TabIndex = 10
        Me.cmdSaveFreeze.Text = "Save"
        Me.cmdSaveFreeze.UseVisualStyleBackColor = True
        '
        'frmCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 694)
        Me.Controls.Add(Me.pnlUpdateFreeze)
        Me.Controls.Add(Me.cmdUpdateFreeze)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.pnlAddDeleteRecords)
        Me.Controls.Add(Me.pnlAddColumn)
        Me.Controls.Add(Me.grdStyles)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbUse)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbLine)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbMajor)
        Me.Controls.Add(Me.cmbGender)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbSeason)
        Me.Controls.Add(Me.btnDeleteCollection)
        Me.Controls.Add(Me.btnAddColumn)
        Me.Controls.Add(Me.cmdExcel)
        Me.Controls.Add(Me.btnCreateRecords)
        Me.Controls.Add(Me.btnSavePrest)
        Me.KeyPreview = True
        Me.Name = "frmCollection"
        Me.Text = "Collection Manager"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdStyles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAddColumn.ResumeLayout(False)
        Me.pnlAddColumn.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAddDeleteRecords.ResumeLayout(False)
        Me.pnlAddDeleteRecords.PerformLayout()
        CType(Me.grdAddDeleteRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlUpdateFreeze.ResumeLayout(False)
        Me.pnlUpdateFreeze.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbUse As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddColumn As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSavePrest As System.Windows.Forms.Button
    Friend WithEvents cmbMajor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSeason As System.Windows.Forms.ComboBox
    Friend WithEvents btnCreateRecords As System.Windows.Forms.Button
    Friend WithEvents grdStyles As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents pnlAddColumn As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelColumn As System.Windows.Forms.Button
    Friend WithEvents btnSaveColumn As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlAddDeleteRecords As System.Windows.Forms.Panel
    Friend WithEvents grdAddDeleteRecords As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteThisColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteCollection As System.Windows.Forms.Button
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents C1XLBook1 As C1.C1Excel.C1XLBook
    Friend WithEvents cmdUpdateFreeze As System.Windows.Forms.Button
    Friend WithEvents pnlUpdateFreeze As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFreeze As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmdCancelFreeze As System.Windows.Forms.Button
    Friend WithEvents cmdSaveFreeze As System.Windows.Forms.Button
End Class
