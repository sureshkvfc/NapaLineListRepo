<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadForecast
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUploadForecast))
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.txtSeason = New System.Windows.Forms.TextBox()
        Me.txtJBA = New System.Windows.Forms.TextBox()
        Me.lblJBA = New System.Windows.Forms.Label()
        Me.txtFCast = New System.Windows.Forms.TextBox()
        Me.lblFcast = New System.Windows.Forms.Label()
        Me.lstSheets = New System.Windows.Forms.ListBox()
        Me.lblSheets = New System.Windows.Forms.Label()
        Me.grdColumns = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.cmdUpload = New System.Windows.Forms.Button()
        Me.lblUploading = New System.Windows.Forms.Label()
        Me.tmrCount = New System.Windows.Forms.Timer(Me.components)
        Me.txtFCastBuy = New System.Windows.Forms.TextBox()
        Me.lblFCastBuy = New System.Windows.Forms.Label()
        Me.txtPO_3 = New System.Windows.Forms.TextBox()
        Me.lblP0_3 = New System.Windows.Forms.Label()
        Me.txtPO_2 = New System.Windows.Forms.TextBox()
        Me.lblP0_2 = New System.Windows.Forms.Label()
        Me.txtPO_1 = New System.Windows.Forms.TextBox()
        Me.lblP0_1 = New System.Windows.Forms.Label()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.lblPO = New System.Windows.Forms.Label()
        Me.txtForecastdate = New System.Windows.Forms.TextBox()
        Me.lblForecastdate = New System.Windows.Forms.Label()
        Me.txtPO_6 = New System.Windows.Forms.TextBox()
        Me.lblP0_6 = New System.Windows.Forms.Label()
        Me.txtPO_5 = New System.Windows.Forms.TextBox()
        Me.lblP0_5 = New System.Windows.Forms.Label()
        Me.txtPO_4 = New System.Windows.Forms.TextBox()
        Me.lblP0_4 = New System.Windows.Forms.Label()
        Me.txtPO_11 = New System.Windows.Forms.TextBox()
        Me.lblP0_11 = New System.Windows.Forms.Label()
        Me.txtPO_10 = New System.Windows.Forms.TextBox()
        Me.lblP0_10 = New System.Windows.Forms.Label()
        Me.txtPO_9 = New System.Windows.Forms.TextBox()
        Me.lblP0_9 = New System.Windows.Forms.Label()
        Me.txtPO_8 = New System.Windows.Forms.TextBox()
        Me.lblP0_8 = New System.Windows.Forms.Label()
        Me.txtPO_7 = New System.Windows.Forms.TextBox()
        Me.lblP0_7 = New System.Windows.Forms.Label()
        CType(Me.grdColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(478, 12)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "Browse..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(23, 14)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(449, 20)
        Me.txtFileName.TabIndex = 1
        '
        'lblSeason
        '
        Me.lblSeason.AutoSize = True
        Me.lblSeason.Location = New System.Drawing.Point(399, 46)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(84, 13)
        Me.lblSeason.TabIndex = 2
        Me.lblSeason.Text = "Season Column:"
        '
        'txtSeason
        '
        Me.txtSeason.Location = New System.Drawing.Point(525, 43)
        Me.txtSeason.MaxLength = 3
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.Size = New System.Drawing.Size(28, 20)
        Me.txtSeason.TabIndex = 3
        '
        'txtJBA
        '
        Me.txtJBA.Location = New System.Drawing.Point(525, 74)
        Me.txtJBA.Name = "txtJBA"
        Me.txtJBA.Size = New System.Drawing.Size(28, 20)
        Me.txtJBA.TabIndex = 5
        '
        'lblJBA
        '
        Me.lblJBA.AutoSize = True
        Me.lblJBA.Location = New System.Drawing.Point(399, 77)
        Me.lblJBA.Name = "lblJBA"
        Me.lblJBA.Size = New System.Drawing.Size(81, 13)
        Me.lblJBA.TabIndex = 4
        Me.lblJBA.Text = "JBANo Column:"
        '
        'txtFCast
        '
        Me.txtFCast.Location = New System.Drawing.Point(525, 102)
        Me.txtFCast.Name = "txtFCast"
        Me.txtFCast.Size = New System.Drawing.Size(28, 20)
        Me.txtFCast.TabIndex = 7
        '
        'lblFcast
        '
        Me.lblFcast.AutoSize = True
        Me.lblFcast.Location = New System.Drawing.Point(399, 105)
        Me.lblFcast.Name = "lblFcast"
        Me.lblFcast.Size = New System.Drawing.Size(89, 13)
        Me.lblFcast.TabIndex = 6
        Me.lblFcast.Text = "Forecast Column:"
        '
        'lstSheets
        '
        Me.lstSheets.FormattingEnabled = True
        Me.lstSheets.Location = New System.Drawing.Point(82, 40)
        Me.lstSheets.Name = "lstSheets"
        Me.lstSheets.Size = New System.Drawing.Size(231, 108)
        Me.lstSheets.TabIndex = 8
        '
        'lblSheets
        '
        Me.lblSheets.AutoSize = True
        Me.lblSheets.Location = New System.Drawing.Point(24, 40)
        Me.lblSheets.Name = "lblSheets"
        Me.lblSheets.Size = New System.Drawing.Size(43, 13)
        Me.lblSheets.TabIndex = 9
        Me.lblSheets.Text = "Sheets:"
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
        Me.grdColumns.Location = New System.Drawing.Point(23, 189)
        Me.grdColumns.Name = "grdColumns"
        Me.grdColumns.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdColumns.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdColumns.PreviewInfo.ZoomFactor = 75.0R
        Me.grdColumns.PrintInfo.PageSettings = CType(resources.GetObject("grdColumns.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdColumns.Size = New System.Drawing.Size(674, 213)
        Me.grdColumns.TabIndex = 10
        Me.grdColumns.Text = "Colors"
        Me.grdColumns.PropBag = resources.GetString("grdColumns.PropBag")
        '
        'cmdUpload
        '
        Me.cmdUpload.Enabled = False
        Me.cmdUpload.Location = New System.Drawing.Point(478, 160)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(75, 23)
        Me.cmdUpload.TabIndex = 11
        Me.cmdUpload.Text = "Upload..."
        Me.cmdUpload.UseVisualStyleBackColor = True
        '
        'lblUploading
        '
        Me.lblUploading.AutoSize = True
        Me.lblUploading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUploading.Location = New System.Drawing.Point(24, 173)
        Me.lblUploading.Name = "lblUploading"
        Me.lblUploading.Size = New System.Drawing.Size(64, 13)
        Me.lblUploading.TabIndex = 12
        Me.lblUploading.Text = "Uploading"
        Me.lblUploading.Visible = False
        '
        'tmrCount
        '
        '
        'txtFCastBuy
        '
        Me.txtFCastBuy.Location = New System.Drawing.Point(525, 134)
        Me.txtFCastBuy.Name = "txtFCastBuy"
        Me.txtFCastBuy.Size = New System.Drawing.Size(28, 20)
        Me.txtFCastBuy.TabIndex = 14
        '
        'lblFCastBuy
        '
        Me.lblFCastBuy.AutoSize = True
        Me.lblFCastBuy.Location = New System.Drawing.Point(399, 137)
        Me.lblFCastBuy.Name = "lblFCastBuy"
        Me.lblFCastBuy.Size = New System.Drawing.Size(89, 13)
        Me.lblFCastBuy.TabIndex = 13
        Me.lblFCastBuy.Text = "Forecast Column:"
        '
        'txtPO_3
        '
        Me.txtPO_3.Location = New System.Drawing.Point(697, 134)
        Me.txtPO_3.Name = "txtPO_3"
        Me.txtPO_3.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_3.TabIndex = 22
        '
        'lblP0_3
        '
        Me.lblP0_3.AutoSize = True
        Me.lblP0_3.Location = New System.Drawing.Point(571, 137)
        Me.lblP0_3.Name = "lblP0_3"
        Me.lblP0_3.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_3.TabIndex = 21
        Me.lblP0_3.Text = "Forecast Column:"
        '
        'txtPO_2
        '
        Me.txtPO_2.Location = New System.Drawing.Point(697, 102)
        Me.txtPO_2.Name = "txtPO_2"
        Me.txtPO_2.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_2.TabIndex = 20
        '
        'lblP0_2
        '
        Me.lblP0_2.AutoSize = True
        Me.lblP0_2.Location = New System.Drawing.Point(571, 105)
        Me.lblP0_2.Name = "lblP0_2"
        Me.lblP0_2.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_2.TabIndex = 19
        Me.lblP0_2.Text = "Forecast Column:"
        '
        'txtPO_1
        '
        Me.txtPO_1.Location = New System.Drawing.Point(697, 74)
        Me.txtPO_1.Name = "txtPO_1"
        Me.txtPO_1.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_1.TabIndex = 18
        '
        'lblP0_1
        '
        Me.lblP0_1.AutoSize = True
        Me.lblP0_1.Location = New System.Drawing.Point(571, 77)
        Me.lblP0_1.Name = "lblP0_1"
        Me.lblP0_1.Size = New System.Drawing.Size(81, 13)
        Me.lblP0_1.TabIndex = 17
        Me.lblP0_1.Text = "JBANo Column:"
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(697, 43)
        Me.txtPO.MaxLength = 3
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(28, 20)
        Me.txtPO.TabIndex = 16
        '
        'lblPO
        '
        Me.lblPO.AutoSize = True
        Me.lblPO.Location = New System.Drawing.Point(571, 46)
        Me.lblPO.Name = "lblPO"
        Me.lblPO.Size = New System.Drawing.Size(84, 13)
        Me.lblPO.TabIndex = 15
        Me.lblPO.Text = "Season Column:"
        '
        'txtForecastdate
        '
        Me.txtForecastdate.Location = New System.Drawing.Point(1230, 47)
        Me.txtForecastdate.Name = "txtForecastdate"
        Me.txtForecastdate.Size = New System.Drawing.Size(28, 20)
        Me.txtForecastdate.TabIndex = 30
        '
        'lblForecastdate
        '
        Me.lblForecastdate.AutoSize = True
        Me.lblForecastdate.Location = New System.Drawing.Point(1104, 50)
        Me.lblForecastdate.Name = "lblForecastdate"
        Me.lblForecastdate.Size = New System.Drawing.Size(89, 13)
        Me.lblForecastdate.TabIndex = 29
        Me.lblForecastdate.Text = "Forecast Column:"
        '
        'txtPO_6
        '
        Me.txtPO_6.Location = New System.Drawing.Point(873, 102)
        Me.txtPO_6.Name = "txtPO_6"
        Me.txtPO_6.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_6.TabIndex = 28
        '
        'lblP0_6
        '
        Me.lblP0_6.AutoSize = True
        Me.lblP0_6.Location = New System.Drawing.Point(747, 105)
        Me.lblP0_6.Name = "lblP0_6"
        Me.lblP0_6.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_6.TabIndex = 27
        Me.lblP0_6.Text = "Forecast Column:"
        '
        'txtPO_5
        '
        Me.txtPO_5.Location = New System.Drawing.Point(873, 74)
        Me.txtPO_5.Name = "txtPO_5"
        Me.txtPO_5.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_5.TabIndex = 26
        '
        'lblP0_5
        '
        Me.lblP0_5.AutoSize = True
        Me.lblP0_5.Location = New System.Drawing.Point(747, 77)
        Me.lblP0_5.Name = "lblP0_5"
        Me.lblP0_5.Size = New System.Drawing.Size(81, 13)
        Me.lblP0_5.TabIndex = 25
        Me.lblP0_5.Text = "JBANo Column:"
        '
        'txtPO_4
        '
        Me.txtPO_4.Location = New System.Drawing.Point(873, 43)
        Me.txtPO_4.MaxLength = 3
        Me.txtPO_4.Name = "txtPO_4"
        Me.txtPO_4.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_4.TabIndex = 24
        '
        'lblP0_4
        '
        Me.lblP0_4.AutoSize = True
        Me.lblP0_4.Location = New System.Drawing.Point(747, 46)
        Me.lblP0_4.Name = "lblP0_4"
        Me.lblP0_4.Size = New System.Drawing.Size(84, 13)
        Me.lblP0_4.TabIndex = 23
        Me.lblP0_4.Text = "Season Column:"
        '
        'txtPO_11
        '
        Me.txtPO_11.Location = New System.Drawing.Point(1051, 134)
        Me.txtPO_11.Name = "txtPO_11"
        Me.txtPO_11.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_11.TabIndex = 38
        '
        'lblP0_11
        '
        Me.lblP0_11.AutoSize = True
        Me.lblP0_11.Location = New System.Drawing.Point(925, 137)
        Me.lblP0_11.Name = "lblP0_11"
        Me.lblP0_11.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_11.TabIndex = 37
        Me.lblP0_11.Text = "Forecast Column:"
        '
        'txtPO_10
        '
        Me.txtPO_10.Location = New System.Drawing.Point(1051, 102)
        Me.txtPO_10.Name = "txtPO_10"
        Me.txtPO_10.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_10.TabIndex = 36
        '
        'lblP0_10
        '
        Me.lblP0_10.AutoSize = True
        Me.lblP0_10.Location = New System.Drawing.Point(925, 105)
        Me.lblP0_10.Name = "lblP0_10"
        Me.lblP0_10.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_10.TabIndex = 35
        Me.lblP0_10.Text = "Forecast Column:"
        '
        'txtPO_9
        '
        Me.txtPO_9.Location = New System.Drawing.Point(1051, 74)
        Me.txtPO_9.Name = "txtPO_9"
        Me.txtPO_9.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_9.TabIndex = 34
        '
        'lblP0_9
        '
        Me.lblP0_9.AutoSize = True
        Me.lblP0_9.Location = New System.Drawing.Point(925, 77)
        Me.lblP0_9.Name = "lblP0_9"
        Me.lblP0_9.Size = New System.Drawing.Size(81, 13)
        Me.lblP0_9.TabIndex = 33
        Me.lblP0_9.Text = "JBANo Column:"
        '
        'txtPO_8
        '
        Me.txtPO_8.Location = New System.Drawing.Point(1051, 43)
        Me.txtPO_8.MaxLength = 3
        Me.txtPO_8.Name = "txtPO_8"
        Me.txtPO_8.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_8.TabIndex = 32
        '
        'lblP0_8
        '
        Me.lblP0_8.AutoSize = True
        Me.lblP0_8.Location = New System.Drawing.Point(925, 46)
        Me.lblP0_8.Name = "lblP0_8"
        Me.lblP0_8.Size = New System.Drawing.Size(84, 13)
        Me.lblP0_8.TabIndex = 31
        Me.lblP0_8.Text = "Season Column:"
        '
        'txtPO_7
        '
        Me.txtPO_7.Location = New System.Drawing.Point(873, 134)
        Me.txtPO_7.Name = "txtPO_7"
        Me.txtPO_7.Size = New System.Drawing.Size(28, 20)
        Me.txtPO_7.TabIndex = 46
        '
        'lblP0_7
        '
        Me.lblP0_7.AutoSize = True
        Me.lblP0_7.Location = New System.Drawing.Point(747, 137)
        Me.lblP0_7.Name = "lblP0_7"
        Me.lblP0_7.Size = New System.Drawing.Size(89, 13)
        Me.lblP0_7.TabIndex = 45
        Me.lblP0_7.Text = "Forecast Column:"
        '
        'frmUploadForecast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1368, 511)
        Me.Controls.Add(Me.txtPO_7)
        Me.Controls.Add(Me.lblP0_7)
        Me.Controls.Add(Me.txtPO_11)
        Me.Controls.Add(Me.lblP0_11)
        Me.Controls.Add(Me.txtPO_10)
        Me.Controls.Add(Me.lblP0_10)
        Me.Controls.Add(Me.txtPO_9)
        Me.Controls.Add(Me.lblP0_9)
        Me.Controls.Add(Me.txtPO_8)
        Me.Controls.Add(Me.lblP0_8)
        Me.Controls.Add(Me.txtForecastdate)
        Me.Controls.Add(Me.lblForecastdate)
        Me.Controls.Add(Me.txtPO_6)
        Me.Controls.Add(Me.lblP0_6)
        Me.Controls.Add(Me.txtPO_5)
        Me.Controls.Add(Me.lblP0_5)
        Me.Controls.Add(Me.txtPO_4)
        Me.Controls.Add(Me.lblP0_4)
        Me.Controls.Add(Me.txtPO_3)
        Me.Controls.Add(Me.lblP0_3)
        Me.Controls.Add(Me.txtPO_2)
        Me.Controls.Add(Me.lblP0_2)
        Me.Controls.Add(Me.txtPO_1)
        Me.Controls.Add(Me.lblP0_1)
        Me.Controls.Add(Me.txtPO)
        Me.Controls.Add(Me.lblPO)
        Me.Controls.Add(Me.txtFCastBuy)
        Me.Controls.Add(Me.lblFCastBuy)
        Me.Controls.Add(Me.lblUploading)
        Me.Controls.Add(Me.cmdUpload)
        Me.Controls.Add(Me.grdColumns)
        Me.Controls.Add(Me.lblSheets)
        Me.Controls.Add(Me.lstSheets)
        Me.Controls.Add(Me.txtFCast)
        Me.Controls.Add(Me.lblFcast)
        Me.Controls.Add(Me.txtJBA)
        Me.Controls.Add(Me.lblJBA)
        Me.Controls.Add(Me.txtSeason)
        Me.Controls.Add(Me.lblSeason)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Name = "frmUploadForecast"
        Me.Text = "frmUploadForecast"
        CType(Me.grdColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblSeason As System.Windows.Forms.Label
    Friend WithEvents txtSeason As System.Windows.Forms.TextBox
    Friend WithEvents txtJBA As System.Windows.Forms.TextBox
    Friend WithEvents lblJBA As System.Windows.Forms.Label
    Friend WithEvents txtFCast As System.Windows.Forms.TextBox
    Friend WithEvents lblFcast As System.Windows.Forms.Label
    Friend WithEvents lstSheets As System.Windows.Forms.ListBox
    Friend WithEvents lblSheets As System.Windows.Forms.Label
    Friend WithEvents grdColumns As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents lblUploading As System.Windows.Forms.Label
    Friend WithEvents tmrCount As System.Windows.Forms.Timer
    Friend WithEvents txtFCastBuy As System.Windows.Forms.TextBox
    Friend WithEvents lblFCastBuy As System.Windows.Forms.Label
    Friend WithEvents txtPO_3 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_3 As System.Windows.Forms.Label
    Friend WithEvents txtPO_2 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_2 As System.Windows.Forms.Label
    Friend WithEvents txtPO_1 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_1 As System.Windows.Forms.Label
    Friend WithEvents txtPO As System.Windows.Forms.TextBox
    Friend WithEvents lblPO As System.Windows.Forms.Label
    Friend WithEvents txtForecastdate As System.Windows.Forms.TextBox
    Friend WithEvents lblForecastdate As System.Windows.Forms.Label
    Friend WithEvents txtPO_6 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_6 As System.Windows.Forms.Label
    Friend WithEvents txtPO_5 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_5 As System.Windows.Forms.Label
    Friend WithEvents txtPO_4 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_4 As System.Windows.Forms.Label
    Friend WithEvents txtPO_11 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_11 As System.Windows.Forms.Label
    Friend WithEvents txtPO_10 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_10 As System.Windows.Forms.Label
    Friend WithEvents txtPO_9 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_9 As System.Windows.Forms.Label
    Friend WithEvents txtPO_8 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_8 As System.Windows.Forms.Label
    Friend WithEvents txtPO_7 As System.Windows.Forms.TextBox
    Friend WithEvents lblP0_7 As System.Windows.Forms.Label
End Class
