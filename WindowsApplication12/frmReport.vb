Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO

Public Class frmReport
    Public str_report As String = ""
    Public b_IsExcel As Boolean = False

    Private WithEvents cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Private WithEvents rptByLine As New CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private str_parameters(3) As String
    Private defPrinter As String = ""
    Private b_LoadingForm As Boolean = False
    Private str_queryTable As String = ""
    Private str_startSQL As String = ""
    Private b_Dirty As Boolean = False
    Private str_errorPrinters As String = ""
    Private oXL As Object = CreateObject("Excel.Application")

    Private Sub frmReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If defPrinter.Length > 0 Then SetDefaultPrinter(defPrinter)
        If b_IsExcel And b_Dirty Then
            If MsgBox("Do you want to save the changes?", MsgBoxStyle.YesNo, "Save") = MsgBoxResult.Yes Then
                saveTemplate()
            End If
        End If

        oXL.Workbooks.Close()
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL.Workbooks)
        oXL.Quit()
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL)
        oXL = Nothing
        GC.Collect()
    End Sub

    Private Sub saveTemplate()
        If getSelectDataSet("SELECT * FROM dbo.tbl_ExcelReports WHERE templateName = '" & str_report & "'").Tables(0).Rows.Count > 0 Then
            executeSQL("UPDATE dbo.tbl_ExcelReports SET " & _
                            "templatePath ='" & txtTemplate.Text & "', " & _
                            "exportDir='" & txtExportDir.Text & "', " & _
                            "fileName='" & txtFileName.Text & "', " & _
                            "fields='" & getFields(tblRow) & "', " & _
                            "chkFabrics=" & IIf(chkFabrics.Checked, 1, 0) & ", " & _
                            "fabricFields='" & getFields(tblFabrics) & "', " & _
                            "chkAcc=" & IIf(chkAcc.Checked, 1, 0) & ", " & _
                            "accFields='" & getFields(tblAcc) & "', " & _
                            "chkTrims=" & IIf(chkTrims.Checked, 1, 0) & ", " & _
                            "trimsFields='" & getFields(tblTrims) & "', " & _
                            "smsPrices=" & IIf(radioSMSPrices.Checked, 1, 0) & _
                        "WHERE templateName = '" & str_report & "'")
        Else
            Dim str_templateName As String = ""
            Dim b_unique As Boolean = False
            Do While Not b_unique = True
                str_templateName = InputBox("Please enter a new template name", "New Template", "template_" & UserId)
                b_unique = IIf(getSelectDataSet("SELECT * FROM dbo.tbl_ExcelReports WHERE templateName = '" & str_templateName & "'").Tables(0).Rows.Count > 0, False, True)
            Loop
            executeSQL("INSERT INTO dbo.tbl_ExcelReports (" & _
                            "templateName, " & _
                            "templatePath, " & _
                            "exportDir, " & _
                            " fileName, " & _
                            " fields, " & _
                            "chkFabrics, " & _
                            "fabricFields, " & _
                            "chkAcc, " & _
                            "accFields, " & _
                            "chkTrims, " & _
                            "trimsFields, " & _
                            "smsPrices" & _
                     ") VALUES( " & _
                            "'" & str_templateName & "', " & _
                            "'" & txtTemplate.Text & "', " & _
                            "'" & txtExportDir.Text & "', " & _
                            "'" & txtFileName.Text & "', " & _
                            "'" & getFields(tblRow) & "', " & _
                            IIf(chkFabrics.Checked, 1, 0) & ", " & _
                            "'" & getFields(tblFabrics) & "', " & _
                            IIf(chkAcc.Checked, 1, 0) & ", " & _
                            "'" & getFields(tblAcc) & "', " & _
                            IIf(chkTrims.Checked, 1, 0) & ", " & _
                            "'" & getFields(tblTrims) & "', " & _
                            IIf(radioSMSPrices.Checked, 1, 0) & _
                     ")")
            frmMain.fillExcelReports()
            str_report = str_templateName
        End If
        b_Dirty = False
    End Sub

    Private Function getFields(ByVal tblTable As TableLayoutPanel) As String
        getFields = ""
        For i As Integer = 0 To tblTable.RowCount - 1
            Dim str_range As String = ""
            Dim str_value As String = ""
            For j As Integer = 0 To tblTable.ColumnCount - 1
                Dim cntrl As Control = tblTable.GetControlFromPosition(j, i)
                If Not IsNothing(cntrl) Then
                    Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                        Case "System.Windows.Forms.TextBox"
                            str_range = cntrl.Text.Trim
                        Case "System.Windows.Forms.ComboBox"
                            Dim cbo As ComboBox = cntrl
                            str_value = cbo.SelectedValue
                    End Select
                End If
            Next
            If tblTable.Name = "tblRow" Then
                getFields &= IIf(str_value.Length > 0 And str_range.Length > 0, str_value & "|" & str_range & " ~ ", "")
            Else
                getFields &= IIf(str_range.Length > 0, "|" & str_range & " ~ ", "")
            End If
        Next
        If getFields.Length > 3 Then
            getFields = IIf(getFields.EndsWith(" ~ "), getFields.Substring(0, getFields.Length - 3), getFields)
        Else
            getFields = ""
        End If
    End Function

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnSearchByLine.Visible = False
        cmbSeasonByLine.Visible = False
        cmbLineByLine.Visible = False
        cmbPM.Visible = False
        cmbSeasonByLine.Tag = ""
        cmbLineByLine.Tag = ""
        lblSeasonByLine.Text = ""
        pnlArticles.Visible = False
        'MsgBox("frmreport load called")
        Me.Text = str_report
        b_LoadingForm = True

        If b_IsExcel Then
            tblLayout.Visible = True
            Dim dstColumns As DataRow() = getSelectDataSet("SELECT ColumnName, Split FROM dbo.GridLayout WHERE(GridName = N'grdMain')").Tables(0).Select("") '"Split = 0")
            Dim str_columns As String = ""
            For i As Integer = 0 To dstColumns.Length - 1
                str_columns &= "dbo.NewGrid." & dstColumns(i).Item(0) & IIf(i < dstColumns.Length - 1, ", ", "")
            Next
            Dim dsColumns As DataSet = getSelectDataSet("SELECT CAST('0' as bit) as Export," & str_columns & " FROM dbo.NewGrid INNER JOIN dbo.tmp_NewGridAfterFilter ON dbo.NewGrid.DevNo = dbo.tmp_NewGridAfterFilter.DevNo WHERE (dbo.tmp_NewGridAfterFilter.userNaam = N'" & UserId & "')")
            grdStyles.DataSource = dsColumns
            grdStyles.DataMember = dsColumns.Tables(0).ToString
            'Load layout of grdMain on frmMaingrid, to have column order the same
            setDisplayColumns(grdStyles, True, False)
            LoadSplitLayout(Me, grdStyles, "Newgrid", False, 0, frmMainGrid, frmMainGrid.grdMain)
            'After applying the grdMain column order, to checkbox for export needs to be moved back on front and lock all other columns
            Dim dc As C1.Win.C1TrueDBGrid.C1DisplayColumn = grdStyles.Splits(0).DisplayColumns("Export")
            With dc
                .Visible = True
                If grdStyles.Splits(0).DisplayColumns.IndexOf(dc) <> 0 And 0 < grdStyles.Splits(0).DisplayColumns.Count Then
                    grdStyles.Splits(0).DisplayColumns.RemoveAt(grdStyles.Splits(0).DisplayColumns.IndexOf(dc))
                    grdStyles.Splits(0).DisplayColumns.Insert(0, dc)
                End If
                .Width = 30
            End With

            If Not IsAdmin Then
                btnAddField.Visible = False
                btnBrowse.Visible = False
            End If

            tblRight.RowStyles(tblRight.RowCount - 1).Height = 0
            tabExtra.TabPages.Remove(tabExtra.TabPages.Item(2))
            tabExtra.TabPages.Remove(tabExtra.TabPages.Item(1))
            tabExtra.TabPages.Remove(tabExtra.TabPages.Item(0))

            loadExcelSettings()
            b_LoadingForm = False
        ElseIf str_report = "Articles" Then
            Dim fields(,) As String = {{"SEASON", "Season"}, {"SUPPLIER", "Supplier"}, {"COO", "COO"}, {"GROUPNAME", "Group Name"}, _
                                            {"SUBGROUP", "Sub Group"}, {"ARTITYPE", "Article Type"}, {"RMN", "RMN"}}
            Dim i_top As Integer = 30
            pnlArticles.Size = Me.Size
            pnlArticles.Visible = True

            For i As Integer = 0 To (fields.Length / 2) - 1
                Dim lblField As New System.Windows.Forms.Label
                Dim cmbField As New System.Windows.Forms.ComboBox
                Dim txtField As New System.Windows.Forms.TextBox

                lblField.Text = fields(i, 1).ToString() & ":"
                lblField.Tag = fields(i, 0).ToString()
                lblField.Name = "lblField" & i
                lblField.Dock = DockStyle.Fill
                For j As Integer = 0 To cmbDefault.Items.Count - 1
                    cmbField.Items.Add(cmbDefault.Items(j))
                Next
                cmbField.Visible = True
                cmbField.Dock = DockStyle.Fill
                cmbField.Name = "cmbField" & i
                'AddHandler cmbField.SelectedValueChanged, AddressOf cmbField_ValueChanged
                txtField.Dock = DockStyle.Fill
                txtField.Name = "txtField" & i

                pnlArticles.Controls.Add(lblField, 0, pnlArticles.RowCount - 1)
                pnlArticles.Controls.Add(cmbField, 1, pnlArticles.RowCount - 1)
                pnlArticles.Controls.Add(txtField, 2, pnlArticles.RowCount - 1)
                pnlArticles.RowCount += 1

                lblField.TextAlign = ContentAlignment.MiddleLeft
            Next
        Else
            btnSearchByLine.Visible = True
            Me.Controls.Add(rptByLine)
            rptByLine.Visible = False
            'MsgBox("sAS_StartupPath" & sAS_StartupPath & vbCrLf & str_report)
            Select Case str_report
                Case "ByLine"
                    setUpReport("SeasonName", "Season", "LineName", "Line", "", "", "TotalByLine", sAS_StartupPath & "\Reports\ReportByLine.rpt", "", False, False)
                Case "Merchandising"
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PP"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP_Grouped] @usernaam = N'" & UserId & "', @PP = 6")
                    'executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PP.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPMen"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPMen.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "' and {vw_Quest_Rep_StyleOverview.Gender} = 'Men'", False, True)
                Case "Merchandising6PPLadies"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPLadies.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "' and {vw_Quest_Rep_StyleOverview.Gender} = 'Ladies'", False, True)
                Case "Merchandising6PPSequenced"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPPSequenced] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPSequencedGender"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPPSequenced_Gender] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPSequencegender.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPSequencedNomargin"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPPSequenced] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPSequenceNomargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMargin"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP_Grouped] @usernaam = N'" & UserId & "', @PP = 6")
                    'executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMarginNoPotential"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMarginNoPotential.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMarginMen"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMarginMen.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "' and {vw_Quest_Rep_StyleOverview.Gender} = 'Men'", False, True)
                Case "Merchandising6PPNoMarginLadies"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMarginLadies.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "' and {vw_Quest_Rep_StyleOverview.Gender} = 'Ladies'", False, True)
                Case "Merchandising6PPByPrice"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPByPrice.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMarginByPrice"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMarginByPrice.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPSequencedNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNotesSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMarginNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPNoMarginNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "Merchandising6PPNoMarginNotesSequenced"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchandising6PPSequenceNomarginNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)

                Case "RepMerchByFabric"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabric.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricSequenced"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricSequencedNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchandiserPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricNotesSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)



                Case "RepMerchByFabricNoMargin"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricSequencedNoMargin"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricSequenceNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricNoMarginNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricNoMarginNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByFabricNoMarginSequencedNotes"
                    executeSQL("EXEC [dbo].[sp_GetMerchByFabricPP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerchByFabricSequenceNomarginNotes.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)


                Case "RepMerchByCapsule"
                    executeSQL("EXEC [dbo].[sp_GetMerchByCapsulePP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerch6PPByCapsule.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByCapsuleSequenced"
                    executeSQL("EXEC [dbo].[sp_GetMerchByCapsulePP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerch6PPByCapsuleSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByCapsuleNoMargin"
                    executeSQL("EXEC [dbo].[sp_GetMerchByCapsulePP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerch6PPByCapsuleNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "RepMerchByCapsuleNoMarginSequenced"
                    executeSQL("EXEC [dbo].[sp_GetMerchByCapsulePP] @usernaam = N'" & UserId & "', @PP = 6")
                    loadReport(sAS_StartupPath & "\Reports\ReportMerch6PPByCapsuleNomarginSequence.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)


                Case "Merchand1"
                    loadReport(sAS_StartupPath & "\Reports\Merchand1.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Merchand3"
                    loadReport(sAS_StartupPath & "\Reports\Merchand3.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Merchand3Minor"
                    loadReport(sAS_StartupPath & "\Reports\Merchand3Minor.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Merchand3Fabric"
                    loadReport(sAS_StartupPath & "\Reports\Merchand3Fabric.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Merchand3WOMargins"
                    loadReport(sAS_StartupPath & "\Reports\Merchand3WOMargins.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Merchand3WOMarginsWODW"
                    loadReport(sAS_StartupPath & "\Reports\Merchand3WOMarginsWODW.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "ProductManager"
                    setUpReport("Season", "Season", "Vendor", "Vendor", "Product_Manager", "Product Manager", "vw_Report_ProductManager", sAS_StartupPath & "\Reports\" & "ProductManagerReport.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "HandOff"
                    setUpReport("Season", "Season", "PM", "Product Manager", "", "", "vw_Report_handOff", sAS_StartupPath & "\Reports\" & "ReportHandOff.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Fabrics"
                    setUpReport("Season", "Season", "Major", "Major", "Minor", "Minor", "vw_Report_Fabric", sAS_StartupPath & "\Reports\" & "ReportFabric.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "Suppliers"
                    setUpReport("Season", "Season", "Minor", "Minor", "", "", "vw_Report_Suppliers", sAS_StartupPath & "\Reports\" & "ReportSupplier.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "WallStyle"
                    loadReport(sAS_StartupPath & "\Reports\ReportWallStyle.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", True, True)
                Case "WallStyleBig"
                    loadReport(sAS_StartupPath & "\Reports\ReportWallStyleBig.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "WallStyleBigNoMargin"
                    loadReport(sAS_StartupPath & "\Reports\ReportWallStyleBigNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "WallStyleByFabric"
                    loadReport(sAS_StartupPath & "\Reports\ReportWallStyleByFabric.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "WallStyleByPrice"
                    loadReport(sAS_StartupPath & "\Reports\ReportWallStyleByPrice.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "StickerForMeeting"
                    loadReport(sAS_StartupPath & "\Reports\ReportStickerForMeeting.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
                Case "StickerForMeetingNoMargin"
                    loadReport(sAS_StartupPath & "\Reports\ReportStickerForMeetingNoMargin.rpt", "{tmp_NewGridAfterFilter.userNaam} = '" & UserId & "'", False, True)
            End Select

            lblSeasonByLine.Visible = cmbSeasonByLine.Visible
            lblLineByLine.Visible = cmbLineByLine.Visible
            lblPM.Visible = cmbPM.Visible
            btnSearchByLine.Visible = cmbSeasonByLine.Visible
            rptByLine.Visible = True
            Me.rptByLine.RefreshReport()
        End If
    End Sub

    Private Sub loadExcelSettings()
        Dim dsSettings As DataSet = getSelectDataSet("SELECT * FROM dbo.tbl_ExcelReports WHERE templateName = '" & str_report & "'")
        If dsSettings.Tables(0).Rows.Count > 0 Then
            Dim dsRow As DataRow = dsSettings.Tables(0).Rows(0)
            txtTemplate.Text = dsRow("templatePath")
            txtExportDir.Text = dsRow("exportDir")
            txtFileName.Text = dsRow("fileName")

            fillFields(tblRow, dsRow("fields"))

            If dsRow("smsPrices").ToString = "True" Then
                radioSMSPrices.Checked = True
            Else
                radioBulkPrices.Checked = True
            End If

            If dsRow("chkFabrics").ToString = "True" Then
                chkFabrics.Checked = True
                fillFields(tblFabrics, dsRow("fabricFields"))
            End If

            If dsRow("chkAcc").ToString = "True" Then
                chkAcc.Checked = True
                fillFields(tblAcc, dsRow("accFields"))
            End If

            If dsRow("chkTrims").ToString = "True" Then
                chkTrims.Checked = True
                fillFields(tblTrims, dsRow("trimsFields"))
            End If
        End If
    End Sub

    Private Sub fillFields(ByVal tblTable As TableLayoutPanel, ByVal str_fields As String)
        If str_fields.Length > 0 Then
            Dim str_Rows As String() = Split(str_fields, " ~ ")
            For i As Integer = 0 To str_Rows.Length - 1
                If tblTable.Name = "tblRow" Then btnAddField_Click(Nothing, Nothing)
                For j As Integer = 0 To tblTable.ColumnCount - 1
                    Dim cntrl As Control = tblTable.GetControlFromPosition(j, i)
                    If Not IsNothing(cntrl) Then
                        Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                            Case "System.Windows.Forms.TextBox"
                                cntrl.Text = Split(str_Rows(i), "|")(1)
                            Case "System.Windows.Forms.ComboBox"
                                Dim cbo As ComboBox = tblRow.GetControlFromPosition(j, i)
                                cbo.SelectedValue = Split(str_Rows(i), "|")(0)
                        End Select
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub setUpReport(ByVal str_combo1Value As String, ByVal str_combo1Text As String, ByVal str_combo2Value As String, ByVal str_combo2Text As String, ByVal str_combo3Value As String, ByVal str_combo3Text As String, ByVal str_table As String, ByVal str_reportPath As String, ByVal str_formula As String, ByVal b_loadA3 As Boolean, ByVal b_loadSettings As Boolean)
        cmbSeasonByLine.Visible = IIf(str_combo1Value.Length > 0, True, False)
        cmbLineByLine.Visible = IIf(str_combo2Value.Length > 0, True, False)
        cmbPM.Visible = IIf(str_combo3Value.Length > 0, True, False)
        cmbSeasonByLine.Tag = str_combo1Value
        cmbLineByLine.Tag = str_combo2Value
        cmbPM.Tag = str_combo3Value
        lblSeasonByLine.Text = str_combo1Text
        lblLineByLine.Text = str_combo2Text
        lblPM.Text = str_combo3Text
        str_queryTable = str_table

        fillCombo(cmbSeasonByLine)
        b_LoadingForm = False
        cmbSeasonByLine_TextChanged(Nothing, Nothing)
        loadReport(str_reportPath, str_formula, b_loadA3, b_loadSettings)
    End Sub

    Private Sub btnSearchByLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchByLine.Click
        cryRpt.RecordSelectionFormula = IIf(str_startSQL.Length > 0, str_startSQL, "")
        If cmbSeasonByLine.Visible And Not cmbSeasonByLine.Text.ToString() = "(All)" And cryRpt.RecordSelectionFormula.Length > 0 Then cryRpt.RecordSelectionFormula &= " AND "
        If cmbSeasonByLine.Visible And Not cmbSeasonByLine.Text.ToString() = "(All)" Then cryRpt.RecordSelectionFormula &= "{" & str_queryTable & "." & cmbSeasonByLine.Tag & "} = '" & cmbSeasonByLine.SelectedValue.ToString() & "'"
        If cmbLineByLine.Visible And Not cmbLineByLine.Text.ToString() = "(All)" And cryRpt.RecordSelectionFormula.Length > 0 Then cryRpt.RecordSelectionFormula &= " AND "
        If cmbLineByLine.Visible And Not cmbLineByLine.Text.ToString() = "(All)" Then cryRpt.RecordSelectionFormula &= "{" & str_queryTable & "." & cmbLineByLine.Tag & "} = '" & cmbLineByLine.SelectedValue.ToString() & "'"
        If cmbPM.Visible And Not cmbPM.Text.ToString() = "(All)" And cryRpt.RecordSelectionFormula.Length > 0 Then cryRpt.RecordSelectionFormula &= " AND "
        If cmbPM.Visible And Not cmbPM.Text.ToString() = "(All)" Then cryRpt.RecordSelectionFormula &= "{" & str_queryTable & "." & cmbPM.Tag & "} = '" & cmbPM.SelectedValue.ToString() & "'"

        setReportWithSettings()
    End Sub

    Private Sub frmReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        rptByLine.Left = 15
        rptByLine.Top = IIf(cmbLineByLine.Visible, cmbLineByLine.Top + cmbLineByLine.Height + 20, 5)
        rptByLine.Width = Me.Width - 30
        rptByLine.Height = Me.Height - rptByLine.Top - 40
    End Sub

    Private Sub cmbSeasonByLine_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSeasonByLine.TextChanged
        If b_LoadingForm = False Then fillCombo(cmbLineByLine, True)
    End Sub

    Private Sub cmbLineByLine_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLineByLine.TextChanged
        If b_LoadingForm = False Then fillCombo(cmbPM, True, True)
    End Sub

    Private Sub fillCombo(ByVal cmbObject As ComboBox, Optional ByVal b_checkSeason As Boolean = False, Optional ByVal b_checkLineByLine As Boolean = False)
        If Not cmbObject.Tag = Nothing Then
            cmbObject.Visible = True
            Dim str_where As String = ""
            If b_checkSeason Then
                If Not cmbSeasonByLine.Text.ToString() = "(All)" Then str_where &= cmbSeasonByLine.Tag & " = '" & cmbSeasonByLine.SelectedValue.ToString() & "'"
                If b_checkLineByLine Then
                    If Not cmbLineByLine.Text.ToString() = "(All)" And Len(str_where) > 0 Then str_where &= " AND "
                    If Not cmbLineByLine.Text.ToString() = "(All)" Then str_where &= cmbLineByLine.Tag & " = '" & cmbLineByLine.SelectedValue.ToString() & "'"
                End If
                If Len(str_where) > 0 Then str_where = " WHERE " & str_where
            End If

            cmbObject.DataSource = getSelectDataSet("SELECT DISTINCT " & cmbObject.Tag & " From " & str_queryTable & " " & str_where & " UNION SELECT '(All)' Order By " & cmbObject.Tag & " ASC").Tables(0)
            cmbObject.DisplayMember = cmbObject.Tag
            cmbObject.ValueMember = cmbObject.Tag
            cmbObject.Update()
        End If
    End Sub

    Private Sub setA3Printer()
        str_A3Printer = "\\vebb2a12\vebb4403"
        str_A3Printer = IIf(str_A3Printer.Length > 0, str_A3Printer, getA3Printer(str_errorPrinters))
        If str_A3Printer.Length > 0 Then
            Try
                SetDefaultPrinter(str_A3Printer)
            Catch
                MessageBox.Show("Going to look for other A3 printer, this one gave an error", "A3 Printer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                str_errorPrinters &= "|" & str_A3Printer & "|"
                str_A3Printer = ""
                setA3Printer()
                Exit Sub
            End Try
            Try
                cryRpt.PrintOptions.PrinterName = str_A3Printer
            Catch
                str_A3Printer = ""
                setA3Printer()
            End Try
            cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
        End If
        cryRpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
        cryRpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto
        str_A3Printer = str_A3Printer
    End Sub

    Private Function getA3Printer(ByVal str_errorPrinters As String)
        Dim printerformat As System.Drawing.Printing.PaperSize
        Dim sA3Printer As String = ""

        For Each printer As String In PrinterSettings.InstalledPrinters
            If str_errorPrinters.IndexOf("|" & printer & "|") < 0 Then
                If sA3Printer.Length > 0 And defPrinter.Length > 0 Then Exit For
                Dim PrinterObj As New System.Drawing.Printing.PrinterSettings
                PrinterObj.PrinterName = printer
                If sA3Printer.Length = 0 Then
                    For Each printerformat In PrinterObj.PaperSizes()
                        Try
                            If printerformat.PaperName.ToString = "A3" Then
                                sA3Printer = printer
                                Exit For
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Can't scan printer for page sizes", "A3 Printer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try
                    Next
                End If
                If PrinterObj.IsDefaultPrinter Then defPrinter = PrinterObj.PrinterName
            End If
        Next
        getA3Printer = sA3Printer
    End Function

    Private Sub loadReport(ByVal str_path As String, ByVal str_formula As String, ByVal b_loadA3 As Boolean, ByVal b_setSettings As Boolean)
        cryRpt.Load(str_path)
        cryRpt.SetDatabaseLogon("Appl_sa", "Appl2011", "vebb2asql03\live", "NAPA")
        cryRpt.RecordSelectionFormula = str_formula
        If str_formula.Length > 0 Then str_startSQL = str_formula
        If b_loadA3 Then setA3Printer()
        If b_setSettings Then setReportWithSettings()
    End Sub

    Private Sub setReportWithSettings()
        rptByLine.ReportSource = cryRpt
        rptByLine.Refresh()
        rptByLine.Zoom(80%)
        rptByLine.DisplayStatusBar = True
        rptByLine.DisplayToolbar = True
        rptByLine.ShowRefreshButton = True
        rptByLine.ShowExportButton = True
        rptByLine.ShowTextSearchButton = True
        rptByLine.ShowZoomButton = True
        'rptByLine.DisplayGroupTree = True
        rptByLine.DisplayBackgroundEdge = False
        rptByLine.Visible = True
    End Sub

    Private Sub btnAddField_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddField.Click
        Dim lblField As New System.Windows.Forms.Label
        Dim cmbField As New System.Windows.Forms.ComboBox
        Dim lblCell As New System.Windows.Forms.Label
        Dim txtCell As New System.Windows.Forms.TextBox
        Dim btnDel As New System.Windows.Forms.Button

        lblField.Text = "Column:"
        lblField.Dock = DockStyle.Fill
        cmbField.DataSource = getSelectDataSet("SELECT ColumnName, ISNULL(ColumnDescription, ColumnName) as Description, Split FROM dbo.GridLayout WHERE(GridName = N'grdMain') ORDER BY ISNULL(ColumnDescription, ColumnName) ASC").Tables(0)
        cmbField.ValueMember = "ColumnName"
        cmbField.DisplayMember = "Description"
        cmbField.DropDownStyle = ComboBoxStyle.Simple
        cmbField.Dock = DockStyle.Fill
        lblCell.Text = "Cell:"
        lblCell.Dock = DockStyle.Fill
        btnDel.Text = "X"
        btnDel.Tag = tblRow.RowCount - 1
        btnDel.Dock = DockStyle.Fill
        AddHandler btnDel.Click, AddressOf btnDel_Click

        If Not IsAdmin Then
            cmbField.Enabled = False
            txtCell.Enabled = False
            btnDel.Visible = False
            btnSave.Visible = False
        End If

        tblRow.Controls.Add(lblField, 0, tblRow.RowCount - 1)
        tblRow.Controls.Add(cmbField, 1, tblRow.RowCount - 1)
        tblRow.Controls.Add(lblCell, 2, tblRow.RowCount - 1)
        tblRow.Controls.Add(txtCell, 3, tblRow.RowCount - 1)
        tblRow.Controls.Add(btnDel, 4, tblRow.RowCount - 1)
        tblRow.RowCount += 1

        lblField.TextAlign = ContentAlignment.MiddleLeft
        lblCell.TextAlign = ContentAlignment.MiddleLeft

        If b_LoadingForm = False Then b_Dirty = True
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        removeRow(tblRow, CInt(sender.tag))
    End Sub

    Private Sub removeRow(ByVal tblTable As TableLayoutPanel, ByVal i_row As Integer)
        For i As Integer = 0 To tblRow.ColumnCount - 1
            tblTable.Controls.Remove(tblTable.GetControlFromPosition(i, i_row))
        Next
        b_Dirty = True
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If System.IO.File.Exists(txtTemplate.Text) And System.IO.Directory.Exists(txtExportDir.Text) And txtFileName.Text.Length > 0 Then
            Dim oXL As Object = CreateObject("Excel.Application")
            Dim oWB As Object
            Dim oSheet As Object
            oXL.DisplayAlerts = False
            oXL.Visible = False
            For i_grdRow As Integer = 0 To grdStyles.RowCount - 1
                If grdStyles.Item(i_grdRow, 0).ToString = "True" Then
                    Me.Cursor = Cursors.WaitCursor

                    oWB = oXL.Workbooks.Open(txtTemplate.Text)
                    oSheet = oWB.ActiveSheet

                    For i As Integer = 0 To tblRow.RowCount - 1
                        Dim str_range As String = ""
                        Dim str_value As String = ""
                        For j As Integer = 0 To tblRow.ColumnCount - 1
                            Dim cntrl As Control = tblRow.GetControlFromPosition(j, i)
                            If Not IsNothing(cntrl) Then
                                Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                                    Case "System.Windows.Forms.TextBox"
                                        str_range = cntrl.Text.Trim
                                    Case "System.Windows.Forms.ComboBox"
                                        Dim cbo As ComboBox = cntrl
                                        str_value = cbo.SelectedValue
                                End Select
                            End If
                        Next
                        If str_range.Length > 0 Then
                            If str_value = "Picture" Then
                                Dim dataSet As DataSet = getSelectDataSet("SELECT picBlob_square FROM dbo.vf_Sketches WHERE lotnumber='" & grdStyles.Item(i_grdRow, "DevNo") & "'")
                                Dim img As Image
                                Dim str_imagePath As String = txtExportDir.Text & "\tmp_image_" & UserId & ".jpg"
                                If dataSet.Tables(0).Rows.Count > 0 Then
                                    Dim imageAsBytes As Byte() = dataSet.Tables(0).Rows(0).Item("picBlob_square")
                                    img = Image.FromStream(New System.IO.MemoryStream(imageAsBytes))
                                Else
                                    img = Image.FromFile(sAS_StartupPath & "\Resources\napa logo_square.gif")
                                End If
                                img.Save(str_imagePath)

                                Dim destWidth As Integer = 0
                                Dim destHeight As Integer = 0
                                Dim destLeft As Integer = oSheet.Range(str_range).Left + 2
                                Dim destTop As Integer = oSheet.Range(str_range).Top + 2
                                With oSheet.Range(str_range)
                                    If .Width > .Height Then
                                        destHeight = .Height - 4
                                        destWidth = .Height - 4
                                        destLeft += (.Width - .Height) / 2
                                    Else
                                        destHeight = .Width - 4
                                        destWidth = .Width - 4
                                        destTop += (.Height - .Width) / 2
                                    End If
                                End With

                                With oSheet.Shapes.AddPicture(str_imagePath, 0, True, destLeft, destTop, destWidth, destHeight)
                                    .LockAspectRatio = -1
                                    .Placement = 1
                                End With
                                img.Dispose()
                                If System.IO.File.Exists(str_imagePath) = True Then System.IO.File.Delete(str_imagePath)
                            Else
                                oSheet.Range(str_range).Value = getValue(grdStyles.Item(i_grdRow, "DevNo").ToString, str_value, grdStyles.Item(i_grdRow, str_value).ToString)
                            End If
                        End If
                    Next

                    Dim dsData As DataSet = getSelectDataSet("SELECT * FROM vw_Report_CostingBreakdown WHERE MOC091='" & grdStyles.Item(i_grdRow, "DevNo").ToString & "'")
                    If chkFabrics.Checked = True Then fillExcelFields(tblFabrics, dsData, "N1", oSheet)
                    If chkAcc.Checked = True Then fillExcelFields(tblAcc, dsData, "N2", oSheet)
                    If chkTrims.Checked = True Then fillExcelFields(tblTrims, dsData, "N3", oSheet)

                    oWB.SaveAs(txtExportDir.Text & IIf(txtExportDir.Text.EndsWith("\"), "", "\") & resolveFileName(txtFileName.Text, i_grdRow))
                    If oWB IsNot Nothing Then
                        oWB.Close(False)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB)
                        oSheet = Nothing
                        oWB = Nothing
                    End If
                    Me.Cursor = Cursors.Default
                End If
            Next
            If oXL IsNot Nothing Then
                oXL.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL)
                oXL = Nothing
            End If
            MsgBox("Export done!", MsgBoxStyle.Information, "Done")
        ElseIf Not System.IO.File.Exists(txtTemplate.Text) Then
            MsgBox("Please select a template", MsgBoxStyle.Information, "No Template")
        ElseIf Not System.IO.Directory.Exists(txtExportDir.Text) Then
            MsgBox("Please select an export directory", MsgBoxStyle.Information, "No Export Directory")
        ElseIf txtFileName.Text.Length = 0 Then
            MsgBox("Please select a file name", MsgBoxStyle.Information, "No File Name")
        End If
    End Sub

    Private Sub fillExcelFields(ByVal tblTable As TableLayoutPanel, ByVal dsData As DataSet, ByVal str_where As String, ByVal osheet As Object)
        For i As Integer = 0 To tblTable.RowCount - 1
            Dim str_range As String = ""
            Dim str_value As String = ""
            For j As Integer = 0 To tblTable.ColumnCount - 1
                Dim cntrl As Control = tblTable.GetControlFromPosition(j, i)
                If Not IsNothing(cntrl) Then
                    Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                        Case "System.Windows.Forms.TextBox"
                            str_range = cntrl.Text.Trim
                        Case "System.Windows.Forms.Label"
                            If cntrl.Text.IndexOf("Column: ") >= 0 Then str_value = cntrl.Tag
                    End Select
                End If
            Next
            If str_range.Length > 0 Then
                Dim i_row As Integer = 0
                For Each dsRow As DataRow In dsData.Tables(0).Select("ARTNO='" & str_where & "'")
                    Dim str_content As String = ""
                    For Each str_column As String In str_value.Split("|")
                        If str_column = "Unit Price" Then
                            str_column += IIf(radioSMSPrices.Checked, " SMS", " Bulk")
                        End If
                        str_content &= IIf(str_content.Length > 0 And dsRow(str_column).ToString.Length > 0, " - ", "") & dsRow(str_column).ToString
                    Next
                    osheet.Range(str_range).Offset(i_row, 0).Value = str_content
                    i_row += 1
                Next
            End If
        Next
    End Sub

    Private Function resolveFileName(ByVal str_filename As String, ByVal i_row As Integer) As String
        If str_filename.IndexOf("[") > 0 And str_filename.IndexOf("]") > 0 Then
            For i As Integer = 0 To grdStyles.Columns.Count - 1
                str_filename = str_filename.ToUpper.Replace("[" & grdStyles.Columns(i).Caption.ToUpper & "]", grdStyles.Item(i_row, i).ToString)
                'Debug.WriteLine(grdStyles.Columns(i).Caption)
            Next
        End If
        resolveFileName = str_filename.Replace("[", "").Replace("]", "") & IIf(str_filename.ToLower.EndsWith(".xls") Or str_filename.ToLower.EndsWith(".xlsx"), "", ".xls")
    End Function

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim FileDialog As New OpenFileDialog()
        With FileDialog
            .InitialDirectory = sAS_NAPARootFolder & "NAPAFiles\"
            .Filter = "Excel files (*.xls, *.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            .FilterIndex = 1
            .RestoreDirectory = True
            .Multiselect = False
            .Title = "Select a template file"
            If .ShowDialog() = DialogResult.OK Then
                txtTemplate.Text = .FileName
                txtFileName.Text = .FileName.Substring(.FileName.LastIndexOf("\") + 1, .FileName.LastIndexOf(".") - .FileName.LastIndexOf("\") - 1) & "_[Name]" & .FileName.Substring(.FileName.LastIndexOf("."))
                b_Dirty = True
            End If
        End With
    End Sub

    Private Sub btnBrowseExportDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseExportDir.Click
        Dim fldDialog As New FolderBrowserDialog
        If fldDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtExportDir.Text = fldDialog.SelectedPath
            b_Dirty = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        saveTemplate()
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click, btnDeselectAll.Click
        For i As Integer = 0 To grdStyles.RowCount - 1
            grdStyles.Item(i, 0) = sender.tag
        Next
    End Sub

    Private Sub chkBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFabrics.CheckedChanged, chkAcc.CheckedChanged, chkTrims.CheckedChanged
        Dim tblTable As TableLayoutPanel
        Dim tabTable As TabPage
        Select Case sender.name
            Case "chkFabrics"
                tblTable = tblFabrics
                tabTable = tabFabrics
            Case "chkAcc"
                tblTable = tblAcc
                tabTable = tabAcc
            Case Else
                tblTable = tblTrims
                tabTable = tabTrims
        End Select
        If sender.Checked Then
            addTableRow(tblTable, "Description")
            Select Case sender.name
                Case "chkFabrics"
                    addTableRow(tblTable, "Descrip5", "Fabric Name")
                Case "chkAcc"
                    addTableRow(tblTable, "Code|Size_mask", "Code - Size Mask")
                Case Else
                    addTableRow(tblTable, "Code")
            End Select
            addTableRow(tblTable, "Unit Price")
            addTableRow(tblTable, "Quantity")
            tblRight.RowStyles(tblRight.RowCount - 1).Height = 140
            tabExtra.TabPages.Insert(0, tabTable)
        Else
            For i As Integer = 0 To tblTable.RowCount - 1
                removeRow(tblTable, i)
            Next
            tblTable.RowCount = 1
            tabExtra.TabPages.Remove(tabTable)
            If tblFabrics.RowCount = 1 And tblAcc.RowCount = 1 And tblTrims.RowCount = 1 Then tblRight.RowStyles(tblRight.RowCount - 1).Height = 0
        End If
        b_Dirty = True
    End Sub

    Private Sub addTableRow(ByVal tblTable As TableLayoutPanel, ByVal str_column As String, Optional ByVal str_columnDescription As String = "")
        Dim lblField As New System.Windows.Forms.Label
        Dim lblCell As New System.Windows.Forms.Label
        Dim txtCell As New System.Windows.Forms.TextBox

        lblField.Text = "Column: " & IIf(str_columnDescription.Length = 0, str_column, str_columnDescription)
        lblField.Dock = DockStyle.Fill
        lblField.Tag = str_column
        lblCell.Text = "Cell:"
        lblCell.Dock = DockStyle.Fill
        If Not IsAdmin Then txtCell.Enabled = False

        tblTable.Controls.Add(lblField, 0, tblTable.RowCount - 1)
        tblTable.Controls.Add(lblCell, 1, tblTable.RowCount - 1)
        tblTable.Controls.Add(txtCell, 2, tblTable.RowCount - 1)
        tblTable.RowCount += 1

        lblField.TextAlign = ContentAlignment.MiddleLeft
        lblCell.TextAlign = ContentAlignment.MiddleLeft

        If b_LoadingForm = False Then b_Dirty = True
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim str_clause As String = ""
        For Each cntrl As Control In pnlArticles.Controls
            If TypeOf cntrl Is TextBox Then
                If cntrl.Text.Length > 0 Then
                    Dim lblField As Label = getControlByName(pnlArticles, "lblField" & cntrl.Name.ToString().Replace("txtField", ""))
                    Dim cmbField As ComboBox = getControlByName(pnlArticles, "cmbField" & cntrl.Name.ToString().Replace("txtField", ""))
                    If str_clause.Length > 0 Then str_clause &= " AND "
                    str_clause &= lblField.Tag & " " & cmbField.Text & " '" & cntrl.Text & "'"
                End If
            End If
        Next

        ' = New Excel.Application
        Dim oWB As Object
        Dim oSheet As Object

        oXL.DisplayAlerts = True
        oXL.Visible = True

        Me.Cursor = Cursors.WaitCursor

        oWB = oXL.Workbooks.Add
        oSheet = oWB.Worksheets(1)

        If str_clause.Length > 0 Then str_clause = " WHERE " & str_clause
        Dim dtaData As DataSet = getSelectDataSet("SELECT * FROM QUESTPDMNAPA.dbo.vw_report_Articles " & str_clause)
        Dim i_row As Integer = 2

        oSheet.Range("A1").FormulaR1C1 = "Season"
        oSheet.Range("B1").FormulaR1C1 = "Personalised Trim Code"
        oSheet.Range("C1").FormulaR1C1 = "Supplier Details"
        oSheet.Range("D1").FormulaR1C1 = "Description / Note"
        oSheet.Range("E1").FormulaR1C1 = "QTY"
        oSheet.Range("F1").FormulaR1C1 = "Euro Price"
        oSheet.Range("G1").FormulaR1C1 = "USD Price"
        oSheet.Range("H1").FormulaR1C1 = "Min for Delivery Order"
        oSheet.Range("I1").FormulaR1C1 = "Zone"

        setBorders(oSheet, "A1:I1", True, True, True, True, True, True)
        With oSheet.Range("A1:I1").Font
            .Name = "Arial Black"
            .Size = 12
        End With
        oSheet.Range("A1:I1").EntireColumn.AutoFit()

        For Each row As DataRow In dtaData.Tables(0).Rows
            setLayout(oSheet, i_row)
            oSheet.Range("A" & i_row).Value = row("Season").ToString()

            Dim destWidth As Integer = 0
            Dim destHeight As Integer = 0
            Dim destLeft As Integer = oSheet.Range("B" & i_row).Left + 2
            Dim destTop As Integer = oSheet.Range("B" & i_row).Top + 2
            With oSheet.Range("B" & i_row & ":B" & i_row + 1)
                If .Width > .Height Then
                    destHeight = .Height - 4
                    destWidth = .Height - 4
                    destLeft += (.Width - .Height) / 2
                Else
                    destHeight = .Width - 4
                    destWidth = .Width - 4
                    destTop += (.Height - .Width) / 2
                End If
            End With

            'Try
            ' If File.Exists(row("PICTARTI01").ToString()) Then
            ' With oSheet.Shapes.AddPicture(row("PICTARTI01").ToString(), 0, True, destLeft, destTop, destWidth, destHeight)
            ' .LockAspectRatio = -1
            ' .Placement = 1
            ' End With
            ' End If
            ' Catch
            'End Try

            oSheet.Range("C" & i_row).Value = row("Supplier").ToString() & Chr(10) & _
                                                row("Address1").ToString() & Chr(10) & _
                                                row("Address2").ToString() & Chr(10) & _
                                                row("Address3").ToString() & Chr(10) & _
                                                row("Address4").ToString()

            oSheet.Range("D" & i_row).Value = row("Descrip1").ToString()

            Dim RTFConvert As RichTextBox = New RichTextBox
            RTFConvert.Rtf = row("AComment").ToString()
            oSheet.Range("D" & i_row + 1 & ":H" & i_row + 1).Value = RTFConvert.Text

            oSheet.Range("F" & i_row).Value = row("currency").ToString() & " " & row("atprice").ToString()

            i_row += 2
        Next

        Try
            Dim MyAttr As FileAttribute = GetAttr(sAS_NAPARootFolder & "NAPAEXCEL")
        Catch
            MkDir(sAS_NAPARootFolder & "NAPAEXCEL")
        End Try

        Dim fileName As String = sAS_NAPARootFolder & "NAPAEXCEL\NAPALineList_" & UserId & "_" & Format(Now, "yyyyMMdd_hhmmss") & ".xls"
        oWB.SaveAs(fileName:=fileName)
        oWB.Close(False)

        System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet)
        oSheet = Nothing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB)
        oWB = Nothing

        Me.Cursor = Cursors.Default

        'System.Diagnostics.Process.Start(fileName)
    End Sub

    Private Sub setLayout(ByRef oSheet As Object, ByVal i_row As Integer)
        Dim xlBottom As Integer = -4107
        Dim xlCenter As Integer = -4108
        Dim xlContext As Integer = -5002

        oSheet.Rows(i_row & ":" & i_row + 1).RowHeight = 50

        setBorders(oSheet, "A" & i_row & ":I" & i_row + 1, True, True, True, True, False, False)
        setBorders(oSheet, "A" & i_row & ":D" & i_row + 1, False, False, False, False, True, False)
        setBorders(oSheet, "D" & i_row & ":H" & i_row, False, False, False, True, True, False)
        setBorders(oSheet, "I" & i_row & ":I" & i_row + 1, True, False, False, False, False, False)

        oSheet.Range("D" & i_row + 1 & ":H" & i_row + 1).MergeCells = True
        oSheet.Range("A" & i_row & ":A" & i_row + 1).MergeCells = True
        oSheet.Range("B" & i_row & ":B" & i_row + 1).MergeCells = True
        oSheet.Range("C" & i_row & ":C" & i_row + 1).MergeCells = True
        oSheet.Range("I" & i_row & ":I" & i_row + 1).MergeCells = True

        With oSheet.Range("A" & i_row & ":I" & i_row + 1)
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .WrapText = True
            .Font.Name = "Arial"
            .Font.Size = 6
        End With
    End Sub

    Private Sub setBorders(ByRef oSheet As Object, ByVal str_range As String, ByVal b_Left As Boolean, ByVal b_Right As Boolean, ByVal b_Top As Boolean, ByVal b_Bottom As Boolean, ByVal b_vertInside As Boolean, ByVal b_horInside As Boolean)
        If b_Left Then oSheet.Range(str_range).Borders(7).Weight = 2
        If b_Right Then oSheet.Range(str_range).Borders(10).Weight = 2
        If b_Top Then oSheet.Range(str_range).Borders(8).Weight = 2
        If b_Bottom Then oSheet.Range(str_range).Borders(9).Weight = 2
        If b_vertInside Then oSheet.Range(str_range).Borders(11).Weight = 2
        If b_horInside Then oSheet.Range(str_range).Borders(12).Weight = 2
    End Sub

    Private Function getControlByName(ByVal cntrlParent As Control, ByVal str_name As String)
        getControlByName = Nothing
        For Each cntrl As Control In cntrlParent.Controls
            If cntrl.Name = str_name Then
                getControlByName = cntrl
                Exit Function
            End If
        Next
    End Function

End Class