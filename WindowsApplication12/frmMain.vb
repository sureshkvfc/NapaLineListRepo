Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization

Public Class frmMain
    Private UserData As New DataSet
    Public ShowData As New DataSet

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Set the inputlanguage and globalization to English - US
        '-------------------------------------------------------
        'setInputLanguage()
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)
            'MsgBox("System.Windows.Forms.Application.StartupPath:" & System.Windows.Forms.Application.StartupPath & vbCrLf & "sAS_StartupPath:" & sAS_StartupPath)
        Catch
            MessageBox.Show("Could not set application culture to en-US")
        End Try
        'MessageBox.Show(Thread.CurrentThread.CurrentCulture.ToString)
        Try
            setAppsettings() 'setting for Unity Go live
        Catch ex As Exception
            MessageBox.Show("Problem in gettting appsetting from DB")
        End Try
        Dim i As Integer
        Dim b_commandLine As Boolean = False

        'If My.Application.CommandLineArgs.Count > 0 Then
        frmSplash.TopMost = False
        frmSplash.Show()
        Me.Visible = False
        Try
            MapDrive("o:", My.Settings.O_Drive, False)
        Catch ex As Exception
            SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", "Could not Map O drive", "frmmain line / NapaStylesetup Napalinelist job")
            'MessageBox.Show("Could not map the O: drive to " & My.Settings.O_Drive, "Map O drive", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Fill the season table
        SeasonTable = getSelectDataSet("SELECT * FROM param_season").Tables(0)

        If My.Application.CommandLineArgs.Count > 0 Then
            b_commandLine = True
            Select Case My.Application.CommandLineArgs(0).ToUpper
                Case "UPDATECOLORS"
                    If My.Application.CommandLineArgs.Count > 1 Then
                        updateColors(My.Application.CommandLineArgs(1))
                    Else
                        updateColors()
                    End If
                Case "UPDATETHUMBNAILS"
                    If My.Application.CommandLineArgs.Count > 1 Then
                        If My.Application.CommandLineArgs.Count > 2 Then
                            checkThumbnails(My.Application.CommandLineArgs(1).ToUpper(), IIf(My.Application.CommandLineArgs(2).ToUpper() = "NOLOG", True, False))
                        Else
                            checkThumbnails(My.Application.CommandLineArgs(1).ToUpper())
                        End If
                    Else
                        checkThumbnails()
                    End If
                Case "UPLOADTOBRIO"
                    uploadLinelistToBrio()
                Case "CREATERGB"
                    createRGBPicturesQuest()
                Case "PRINTSERVER"
                    'Dim dtaJobs As DataSet = getSelectDataSet("select sum(ctr) from " & _
                    '                                            "(select count(*) as ctr from QUESTPDMNAPA.dbo.printjob where processed <> 1) as tblDerived")
                    'If Not dtaJobs.Tables(0).Rows(0).Item(0).ToString = "0" Then
                    frmMailPDF.Show()
                    'frmMailPDF.Cmdprintserver_Click()
                    'frmMailPDF.Close()
                    'End If

                    'mnuSubFile_Click(7)
                Case "COSTINGSHEETEXPORT"
                    frmAutoGenCostingSheet.ExcelGenerate()
                Case Else
                    Dim str_message = "Use the command line like this:" & vbCrLf & _
                                        "   To update the colors:" & vbCrLf & _
                                        "      NapaLineList.exe UPDATECOLORS" & vbCrLf & _
                                        "     OR" & vbCrLf & _
                                        "      NapaLineList.exe UPDATECOLORS ""WHERE CLAUSE""" & vbCrLf & _
                                        "      ex.: NapaLineList.exe UPDATECOLORS ""Season='FW11'""" & vbCrLf & _
                                        "   To update the thumnails:" & vbCrLf & _
                                        "      NapaLineList.exe UPDATETHUMBNAILS" & vbCrLf & _
                                        "     OR" & vbCrLf & _
                                        "      NapaLineList.exe UPDATETHUMBNAILS ""WHERE CLAUSE""" & vbCrLf & _
                                        "      ex.: NapaLineList.exe UPDATETHUMBNAILS ""DevNo=1000""" & vbCrLf & _
                                        "     OR" & vbCrLf & _
                                        "      NapaLineList.exe UPDATETHUMBNAILS ""WHERE CLAUSE"" NOLOG" & vbCrLf & _
                                        "      ex.: NapaLineList.exe UPDATETHUMBNAILS ""DevNo=1000"" NOLOG" & vbCrLf & _
                                        "   To Upload Linelist data to JBA for use in Brio:" & vbCrLf & _
                                        "      NapaLineList.exe UPLOADTOBRIO" & vbCrLf & _
                                        "   To Create The RGB Pictures for Colors from Quest:" & vbCrLf & _
                                        "      NapaLineList.exe CREATERGB" & vbCrLf & _
                                        "   To Launch Printserver:" & vbCrLf & _
                                        "       NapaLineList.exe PRINTSERVER"
                    MsgBox(str_message)
            End Select
            Application.Exit()
            Exit Sub
        End If

        'fill the combobox for the maintenance
        Dim MaintenanceData As DataSet = getSelectDataSet("SELECT Description, myTable From MaintenanceMenu order by Description")
        For i = 0 To MaintenanceData.Tables(0).Rows.Count - 1
            cmbMaintenance.Items.Add(MaintenanceData.Tables(0).Rows(i).Item("Description"))
        Next
        MaintenanceData = Nothing

        'set the correct captions for the 'Show' menu
        ShowData = getSelectDataSet("SELECT Description, Split, DBField From SplitHeaders WHERE visible = 1 ORDER BY Split")
        Dim mnuShowItem As ToolStripItem
        For i = 0 To ShowData.Tables(0).Rows.Count - 1
            mnuShowItem = mnuShow.DropDownItems.Add(ShowData.Tables(0).Rows(i).Item("Description"), Nothing, New EventHandler(AddressOf mnushow_Click))
            mnuShowItem.Tag = ShowData.Tables(0).Rows(i).Item("Split") & "|" & ShowData.Tables(0).Rows(i).Item("DBField")
        Next

        'get and set the user rights
        GetUser()
        UserData = getSelectDataSet("SELECT u.*, a.* FROM Param_Users AS u LEFT JOIN Param_Admin AS a ON u.UserNaam = a.UserNaam WHERE u.UserNaam = '" & UserId & "'")
        If UserData.Tables(0).Rows.Count = 0 Then
            MsgBox("You don't have access to this program")
            End
        End If

        NoSubs = True
        userGroup = UserData.Tables(0).Rows(0).Item("userGroup").ToString
        IsAdmin = IIf(UserData.Tables(0).Rows(0).Item("IsAdmin").ToString = "True", True, False)
        str_A3Printer = UserData.Tables(0).Rows(0).Item("A3Printer").ToString
        AllowUpdatequestfreeze = IIf(UserData.Tables(0).Rows(0).Item("AllowUpdatequestfreeze").ToString = "True", True, False)

        If UserData.Tables(0).Rows(0).Item("AllowUpdateMain").ToString = "True" Then
            frmMainGrid.grdMain.Splits(0).Locked = False
            cmbMaintenance.Visible = True
        Else
            frmMainGrid.grdMain.Splits(0).Locked = True
            'cmbMaintenance.Visible = False
        End If

        If UserData.Tables(0).Rows(0).Item("AllowUpdateCosting").ToString = "True" Then
            frmMainGrid.grdYouth.Splits(0).Locked = False
            AllowYouthCosting = True
        Else
            frmMainGrid.grdYouth.Splits(0).Locked = True
            AllowYouthCosting = False
        End If

        btnMassUpdate.Visible = IIf(UserData.Tables(0).Rows(0).Item("massUpdate").ToString = "True", True, False)
        SystemToolStripMenuItem.Visible = IsAdmin
        If IsAdmin Then
            cmbMaintenance.Visible = IsAdmin
        End If
        btnUndelete.Visible = IsAdmin
        btnAutomationExport.Visible = IsAdmin

        bl_styleAdmin = IIf(UserData.Tables(0).Rows(0).Item("StyleAdmin").ToString = "True", True, False)
        StyleToolStripMenuItem.Visible = bl_styleAdmin
        btnCollection.Visible = IIf(UserData.Tables(0).Rows(0).Item("Merchandiser").ToString = "True", True, False)
        b_showCosting = IIf(UserData.Tables(0).Rows(0).Item("ShowCosting").ToString = "True", True, False)
        str_picDirectory = IIf(Directory.Exists(UserData.Tables(0).Rows(0).Item("picDirectory").ToString), UserData.Tables(0).Rows(0).Item("picDirectory"), str_picDirectory)

        CostingToolStripMenuItem.Visible = IIf(UserData.Tables(0).Rows(0).Item("CostingAdmin").ToString = "True", True, False)
        If UserData.Tables(0).Rows(0).Item("AllowUploadForecastJBA").ToString = "True" Then
            CostingToolStripMenuItem.Visible = True
            For Each mnuItem As ToolStripMenuItem In CostingToolStripMenuItem.DropDownItems
                If Not mnuItem Is UploadForecastToolStripMenuItem Then mnuItem.Visible = False
            Next
        End If

        PurchaseToolStripMenuItem.Visible = IIf(UserData.Tables(0).Rows(0).Item("PurchasingAdmin").ToString = "True", True, False)
        PlanningToolStripMenuItem.Visible = IIf(UserData.Tables(0).Rows(0).Item("PlanningAdmin").ToString = "True", True, False)
        UploadForecastMTGToolStripMenuItem.Visible = IIf(UserData.Tables(0).Rows(0).Item("PlanningAdmin").ToString = "True", True, False)

        btnAdmin.Visible = (IsAdmin Or bl_styleAdmin Or IIf(UserData.Tables(0).Rows(0).Item("CostingAdmin").ToString = "True", True, False) Or IIf(UserData.Tables(0).Rows(0).Item("PurchasingAdmin").ToString = "True", True, False) Or IIf(UserData.Tables(0).Rows(0).Item("PlanningAdmin").ToString = "True", True, False))

        Dim Col As New C1.Win.C1TrueDBGrid.C1DataColumn
        ' fill the main grid
        frmMainGrid.btnRefresh_Click(Nothing, Nothing)

        NoSubs = False
        LayoutData.Clear()
        LayoutData = getSelectDataSet("SELECT * From GridLayout WHERE gridName = 'grdMain' ORDER BY Split")
        setDisplayColumns(frmMainGrid.grdMain)
        frmMainGrid.grdMain.Splits(0).Name = "Split0"

        If Not IsDBNull(UserData.Tables(0).Rows(0).Item("IsAdmin")) Then
            frmMainGrid.grdMain.Splits(0).DisplayColumns("Freeze").Locked = Not (UserData.Tables(0).Rows(0).Item("IsAdmin"))
            mnuRecords.Visible = True
        Else
            frmMainGrid.grdMain.Splits(0).DisplayColumns("Freeze").Locked = True
            mnuRecords.Visible = False
        End If

        If UserData.Tables(0).Rows(0).Item("UpdateColors").ToString = "1" Then UpdateColorsToolStripMenuItem.Visible = True

        For Each mnuShowItem In mnuShow.DropDownItems
            mnuShowItem.Visible = IIf(UserData.Tables(0).Rows(0).Item("Show" & Split(mnuShowItem.Tag, "|")(1)).ToString = "True", True, False)

            If UserData.Tables(0).Rows(0).Item(Split(mnuShowItem.Tag, "|")(1)).ToString = "True" Then
                mnushow_Click(mnuShowItem, Nothing)
            End If
        Next

        frmMainGrid.fillTemplateCombo()

        If Not b_commandLine Then
            Me.Visible = True
            frmMainGrid.MdiParent = Me
            frmMainGrid.Show()
        End If

        fillExcelReports()

        'FindDuplicateStylesToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Public Sub fillExcelReports()
        mnuExcelReport.DropDown.Items.Clear()
        Dim dsExcelReports As DataSet = getSelectDataSet("SELECT templateName FROM dbo.tbl_ExcelReports")
        For i_report As Integer = 0 To dsExcelReports.Tables(0).Rows.Count - 1
            Dim mnuExcelReportSub As ToolStripMenuItem = mnuExcelReport.DropDown.Items.Add(dsExcelReports.Tables(0).Rows(i_report).Item(0))
            mnuExcelReportSub.Tag = dsExcelReports.Tables(0).Rows(i_report).Item(0)
            If IsAdmin Then
                mnuExcelReportSub.DropDownItems.Add("Open", Nothing, AddressOf mnuExcelReportSub_Click).Tag = mnuExcelReportSub.Tag
                mnuExcelReportSub.DropDownItems.Add("Delete", Nothing, AddressOf mnuExcelReportSub_DeleteTemplate)
            Else
                AddHandler mnuExcelReportSub.Click, AddressOf mnuExcelReportSub_Click
            End If
        Next
        If IsAdmin Then
            Dim mnuExcelReportSubNew As ToolStripItem = mnuExcelReport.DropDown.Items.Add("New...")
            mnuExcelReportSubNew.Tag = "[new]"
            AddHandler mnuExcelReportSubNew.Click, AddressOf mnuExcelReportSub_Click
        End If
    End Sub

    Public Sub mnushow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        menuClick(sender)
    End Sub

    Private Sub btnCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCollection.Click
        showMDIForm(frmCollection)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        showMDIForm(frmCustomExcel)
    End Sub

    Private Sub UpdateColorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateColorsToolStripMenuItem.Click
        updateColors()
    End Sub

    Private Sub OpenReportForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        mnuReports1.Click, mnuReports2.Click, mnuReports4.Click, mnuReports3Sub.Click, ReportMerchandiser1ToolStripMenuItem.Click, _
        ReportByFabricToolStripMenuItem.Click, menuReportHandOff.Click, mnuReportFabrics.Click, mnuReportSuppliers.Click, _
        ReportMarchandiser3WithoutMarginsToolStripMenuItem.Click, ReportWallStyleToolStripMenuItem.Click, mnuReportArticles.Click, _
        ReportMechandiser3WithoutMarginsWithoutDWGroupingToolStripMenuItem.Click, ReportWallStyleBigToolStripMenuItem.Click, _
        ReportWallStyleByFabricToolStripMenuItem.Click, ReportByMinorToolStripMenuItem.Click, ReportWallStyleBigNoMarginToolStripMenuItem.Click, _
        ReportWallStyleByPriceToolStripMenuItem.Click, _
        ReportMerchandising6PPAllToolStripMenuItem.Click, ReportMerchandising6PPMenToolStripMenuItem.Click, _
        ReportMerchandising6PPLadiesToolStripMenuItem.Click, _
        ReportMerchaindinsing6AllToolStripMenuItem.Click, ReportMerchaindinsing6MenToolStripMenuItem.Click, _
        ReportMerchaindinsing6LadiesToolStripMenuItem.Click, ReportMerchandising6PPSeqToolStripMenuItem.Click, _
        Merchandising6PPSeqNomarginToolStripMenuItem.Click, _
        RepMerch6ppSequencedNotesToolStripMenuItem.Click, RepMerch6ppGroupedNotesToolStripMenuItem.Click, _
        RepMerch6ppNoMarginGroupedWithNotesToolStripMenuItem1.Click, RepMerch6ppNoMarginSequencedNotesToolStripMenuItem1.Click, RepMerchByFabricGrouped.Click, _
        RepMerchByFabricGrouped.Click, RepMerchByFabricGroupedNoMargin.Click, RepMerchByFabricSequenced.Click, RepMerchByFabricSequencedNotes.Click, RepMerchByFabricGroupedNotes.Click, RepMerchByFabricNoMarginSequencedNotes.Click, RepMerchByFabricSequencedNoMargin.Click, RepMerchByFabricNoMarginNotes.Click, SequencedToolStripMenuItem1.Click, SequencedToolStripMenuItem.Click, GroupedToolStripMenuItem1.Click, GroupedToolStripMenuItem.Click, WithMarginToolStripMenuItem.Click, NOMarginToolStripMenuItem.Click, _
        ReportMerchandising6PPNoMarginNoPotentialToolStripMenuItem.Click, SequencedGenderToolStripMenuItem.Click

        ', ReportMerchaindinsing6ToolStripMenuItem.Click
        ', ReportMerchandising6PPToolStripMenuItem.Click, _
        frmReport.str_report = sender.tag
        showMDIForm(frmReport)
    End Sub

    Private Sub ManageColorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageColorsToolStripMenuItem.Click
        showMDIForm(frmPickColors)
    End Sub

    Private Sub btnMaintenanceColors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaintenanceColors.Click
        showMDIForm(frmColors)
    End Sub

    Private Sub UsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersToolStripMenuItem.Click
        showMDIForm(frmAdmin)
    End Sub

    Private Sub UnFreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnFreeToolStripMenuItem.Click
        If AllowUpdatequestfreeze Then
            showMDIForm(frmUnFreeze)
        Else
            MsgBox("You don't have rights to change this.")
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim CurrDat As Date = Now
        Dim CurrHour As Integer
        CurrHour = DatePart(DateInterval.Hour, CurrDat)

        If CurrHour >= 22 Then
            Me.Close()
        End If
    End Sub

    Private Sub cmbMaintenance_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMaintenance.SelectedIndexChanged
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        If Not IsNothing(cmbMaintenance.SelectedItem) Then
            SearchAdapter.SelectCommand = dcNAPA.CreateCommand
            SearchAdapter.SelectCommand.CommandText = "Select myTable From MaintenanceMenu Where Description = '" & cmbMaintenance.SelectedItem.ToString & "'"
            SearchAdapter.Fill(SearchData)
            If (cmbMaintenance.SelectedItem.ToString = "MeasurementTemplate") Then
                'frmMaintenance2.Show()
                openFrmMaintenance(UCase(SearchData.Tables(0).Rows(0).Item("myTable").ToString), "SELECT * FROM " & SearchData.Tables(0).Rows(0).Item("myTable").ToString, False, False)
            Else
                openFrmMaintenance(UCase(SearchData.Tables(0).Rows(0).Item("myTable").ToString), "SELECT * FROM " & SearchData.Tables(0).Rows(0).Item("myTable").ToString, False, False)
            End If

        End If
    End Sub

    Private Sub SourcingRatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourcingRatesToolStripMenuItem.Click
        openFrmMaintenance("SOURCINGRATES", "SELECT * FROM NAPA.dbo.tbl_sourcing_Rates WHERE Coalition = 'DP'", True, False)
    End Sub

    Private Sub CostBucketsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostBucketsToolStripMenuItem.Click
        openFrmMaintenance("COSTBUCKETS", "SELECT * FROM NAPA.dbo.Param_CostBuckets", True, False)
    End Sub

    Private Sub openFrmMaintenance(ByVal str_tableName As String, ByVal str_sql As String, ByVal b_doCalculate As Boolean, ByVal b_lockColumnName As Boolean)
        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        MaintainData = Nothing
        MaintainData = New DataSet
        MaintainAdapter.SelectCommand = dcNAPA.CreateCommand
        MaintainAdapter.SelectCommand.CommandText = str_sql
        MaintainBuilder = New SqlCommandBuilder(MaintainAdapter)
        MaintainAdapter.Fill(MaintainData)
        MaintainData.CreateDataReader()
        frmMaintenance.grdMaintenance.DataSource = MaintainData
        frmMaintenance.grdMaintenance.DataMember = MaintainData.Tables(0).ToString
        frmMaintenance.grdMaintenance.Rebind(False)
        frmMaintenance.tableName = str_tableName
        DoCalculate = b_doCalculate
        If b_lockColumnName Then frmMaintenance.grdMaintenance.Splits(0).DisplayColumns("ColumnName").Locked = True
        Select Case UCase(str_tableName)
            Case "GRIDLAYOUT"
                Dim c As Integer = frmMaintenance.grdMaintenance.Splits.Count
                Dim i, j As Integer
                frmMaintenance.grdMaintenance.InsertHorizontalSplit(c)
                For i = 0 To frmMaintenance.grdMaintenance.Splits.Count - 1
                    For j = 0 To frmMaintenance.grdMaintenance.Splits(i).DisplayColumns.Count - 1
                        frmMaintenance.grdMaintenance.Splits(i).DisplayColumns(j).Visible = IIf(i = 0, IIf(j < 2, True, False), IIf(j < 2, False, True))
                    Next
                Next
                frmMaintenance.grdMaintenance.Splits(0).SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.NumberOfColumns
                frmMaintenance.grdMaintenance.Splits(0).SplitSize = 2
                Dim comboData As DataSet = getSelectDataSet("SELECT Split, Description FROM dbo.SplitHeaders ORDER BY Split")
                For j = 0 To comboData.Tables(0).Rows.Count - 1
                    frmMaintenance.grdMaintenance.Columns("Split").ValueItems.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem(comboData.Tables(0).Rows(j).Item("Split"), comboData.Tables(0).Rows(j).Item("Description")))
                Next
                frmMaintenance.grdMaintenance.Columns("Split").ValueItems.Translate = True
                frmMaintenance.grdMaintenance.Columns("Split").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                frmMaintenance.grdMaintenance.Columns("Split").FilterDropdown = True
                frmMaintenance.grdMaintenance.Splits(1).DisplayColumns("Split").DropDownList = True
            Case "SOURCINGRATES"
                frmMaintenance.grdMaintenance.Splits(0).DisplayColumns("Coalition").Visible = False
            Case "TBL_CODES"
                Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection = frmMaintenance.grdMaintenance.Columns("selected").ValueItems.Values
                v.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", "Unused"))
                v.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", "Used"))
                frmMaintenance.grdMaintenance.Columns("selected").ValueItems.Translate = True
                frmMaintenance.grdMaintenance.Columns("selected").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                frmMaintenance.grdMaintenance.Splits(0).DisplayColumns("Code").Locked = True
                frmMaintenance.grdMaintenance.Splits(0).DisplayColumns("Season").Visible = False
            Case "TBL_APPROVEDSUPPLIERS"
                frmMaintenance.grdMaintenance.Splits(0).DisplayColumns("SupplierDescription").Locked = True
            Case "TBL_MEASUREMENTTEMPLATE"
                LayoutData.Clear()
                LayoutData = getSelectDataSet("SELECT * From GridLayout WHERE gridName = 'grdMeasurementTemplate' ORDER BY Split")
                frmMaintenance.grdMaintenance.Name = "grdMeasurementTemplate"
                setDisplayColumns(frmMaintenance.grdMaintenance)
                'setDisplayColumns(grdMeasurementTemplate)
        End Select
        frmMaintenance.MdiParent = Me
        frmMaintenance.Show()
    End Sub

    Private Sub showMDIForm(ByVal frmObj As Form)
        'If frmObj.FindForm.IsHandleCreated Then
        'frmObj.Close()
        'End If
        frmObj.MdiParent = Me
        frmObj.Show()
    End Sub

    Private Sub menuClick(ByVal menuShow As Object)
        Dim i As Integer
        Dim c As Integer
        Dim foundRows() As Data.DataRow
        Dim str_split() As String = Split(menuShow.tag, "|")
        Dim str As String = ""
        Dim h As String = ""
        Dim s As String = ""
        c = frmMainGrid.grdMain.Splits.Count
        isLoadingSplit = False
        If menuShow.Checked = True Then
            menuShow.Checked = False
            SaveSplitLayout(frmMainGrid, frmMainGrid.grdMain, frmMainGrid.grdMain.Splits("Split" & str_split(0)))
            frmMainGrid.grdMain.RemoveHorizontalSplit(frmMainGrid.grdMain.Splits.IndexOf(frmMainGrid.grdMain.Splits("Split" & str_split(0))))
            executeSQL("update Param_Users set " & str_split(1) & "= 0 Where UserNaam = '" & UserId & "'")
        Else
            menuShow.Checked = True
            frmMainGrid.grdMain.InsertHorizontalSplit(c)
            With frmMainGrid.grdMain.Splits(c)
                .Name = "Split" & str_split(0)
                .Caption = menuShow.Text
                .Locked = IIf(UserData.Tables(0).Rows(0).Item("AllowUpdate" & str_split(1)).ToString = "True", False, True)
                For i = .DisplayColumns.Count - 1 To 0 Step -1
                    foundRows = LayoutData.Tables(0).Select("ColumnName = '" & frmMainGrid.grdMain.Columns(i).DataField & "' And Split = " & str_split(0) & " And isnull(InVisible,0) <> 1")

                    .DisplayColumns(frmMainGrid.grdMain.Columns(i).DataField).Visible = IIf(foundRows.Length > 0, True, False) ''commented to test

                    'If (frmMainGrid.grdMain.Columns(i).DataField.ToString = "ProdColor31" Or frmMainGrid.grdMain.Columns(i).DataField.ToString = "ProdColor32") Then
                    'If (.DisplayColumns(i).DataColumn.DataField.ToString = "ProdColor31" Or .DisplayColumns(i).DataColumn.DataField.ToString = "ProdColor32") Then
                    '    .DisplayColumns(i).Visible = True
                    '    s = s & vbCrLf & frmMainGrid.grdMain.Columns(i).DataField & "(" & .DisplayColumns(i).DataColumn.DataField.ToString & "),"
                    '    'Else

                    '    '    h = h & vbCrLf & frmMainGrid.grdMain.Columns(i).DataField & ","
                    'End If

                    'If (frmMainGrid.grdMain.Columns(i).DataField.ToString.StartsWith("ProdColor")) Then

                    '    str = str & "," & frmMainGrid.grdMain.Columns(i).DataField & ",  length" & foundRows.Length & ",  Split" & str_split(0)
                    '    str = str & vbTab & vbTab & "ColumnName = '" & frmMainGrid.grdMain.Columns(i).DataField & "' And Split = " & str_split(0) & " And isnull(InVisible,0) <> 1"
                    '    str = str & ", Visible status: " & .DisplayColumns(frmMainGrid.grdMain.Columns(i).DataField).Visible & vbCrLf

                    'End If
                    'If str_split(0) = "2" And LayoutData.Tables(0).se Then

                    'End If
                Next
            End With
            'MsgBox("show:hide:" & vbCrLf & s)
            'MsgBox("" & vbCrLf & h)
            'MsgBox(str)
            executeSQL("update Param_Users set " & str_split(1) & " = -1 Where UserNaam = '" & UserId & "'")
            LoadSplitLayout(frmMainGrid, frmMainGrid.grdMain, "NewGrid", True, c)
            frmMainGrid.grdMain.ResumeBinding()
        End If
    End Sub

    Private Sub btnUndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndelete.Click
        showMDIForm(frmUndelete)
    End Sub

    Private Sub DeleteSelectedRecordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSelectedRecordsToolStripMenuItem.Click
        Dim lstSorted As New ArrayList
        Dim myid As Long

        If frmMainGrid.grdMain.SelectedRows.Count > 0 Then
            If MsgBox("Are you sure to delete this records?", MsgBoxStyle.OkCancel, "Delete records?") = MsgBoxResult.Ok Then
                For Each row As Object In frmMainGrid.grdMain.SelectedRows
                    lstSorted.Add(CType(row, Integer))
                Next
                lstSorted.Sort()
                For intIndex As Integer = lstSorted.Count - 1 To 0 Step -1
                    frmMainGrid.grdMain.Row = lstSorted.Item(intIndex)
                    myid = frmMainGrid.grdMain.Item(frmMainGrid.grdMain.Row, 0)
                    executeSQL("Update NewGrid set IsDeleted = -1 , myUser = '" & UserId & " ' where DevNo = " & myid)
                    frmMainGrid.grdMain.Delete()
                Next
            End If
        End If
    End Sub

    Private Sub UpdateThumbnailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateThumbnailsToolStripMenuItem.Click
        'checkThumbnails(" devno=20177")
        For irow As Integer = 0 To frmMainGrid.grdMain.RowCount - 1
            checkThumbnails(" devno=" & frmMainGrid.grdMain.Item(irow, "devno"))
        Next
        MsgBox("Thumbnail update completed!")
    End Sub

    Private Sub ReplaceColorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceColorsToolStripMenuItem.Click
        showMDIForm(frmReplaceColor)
    End Sub

    Private Sub mnuShowThumbnails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowThumbnails.Click
        showMDIForm(frmThumbnails)
    End Sub

    Private Sub UploadForecastToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadForecastToolStripMenuItem.Click, UploadForecastOnBuyToolStripMenuItem.Click
        frmUploadForecast.str_tableName = sender.tag
        showMDIForm(frmUploadForecast)
    End Sub

    Private Sub AvailableStyleCodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AvailableStyleCodesToolStripMenuItem.Click
        openFrmMaintenance("tbl_codes", "SELECT * FROM NAPA.dbo.tbl_codes WHERE selected = 1", False, False)
    End Sub

    Private Sub ApprovedSuppliersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovedSuppliersToolStripMenuItem.Click
        openFrmMaintenance("tbl_ApprovedSuppliers", "SELECT * FROM NAPA.dbo.tbl_ApprovedSuppliers", False, False)
        'openFrmMaintenance("tbl_ApprovedSuppliers", "SELECT SupplierCode, (SELECT MAX(SNAM05) FROM VFMobileXpense.dbo.PLP05 WHERE (SUPN05 = dbo.tbl_ApprovedSuppliers.SupplierCode)) AS SupplierDescription FROM dbo.tbl_ApprovedSuppliers", False, False)
    End Sub

    Private Sub GrdMainToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdMainToolStripMenuItem.Click, GrdYouthToolStripMenuItem.Click
        Dim str_table As String = ""
        Select Case sender.ToString
            Case "grdMain"
                str_table = "NewGrid"
            Case "grdYouth"
                str_table = "SalesYouth"
        End Select
        checkColumnsGridLayout(sender.ToString, str_table)
        openFrmMaintenance("GRIDLAYOUT", "SELECT * FROM GridLayout WHERE gridName='" & sender.ToString & "'", False, True)
    End Sub

    Private Sub UploadToJBABrioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadToJBABrioToolStripMenuItem.Click
        uploadLinelistToBrio()
    End Sub

    Private Sub btnMassUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMassUpdate.Click
        showMDIForm(frmMassUpdate)
    End Sub

    Private Sub mnuExcelReportSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmReport.str_report = sender.tag
        frmReport.b_IsExcel = True
        showMDIForm(frmReport)
    End Sub

    Private Sub mnuExcelReportSub_DeleteTemplate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you shure you want to delete the template '" & sender.OwnerItem.tag & "'?", MsgBoxStyle.OkCancel, "Delete Template") = MsgBoxResult.Ok Then
            executeSQL("DELETE FROM dbo.tbl_ExcelReports WHERE templateName = '" & sender.OwnerItem.tag & "'")
            fillExcelReports()
        End If
    End Sub

    Private Sub PrinterServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrinterServerToolStripMenuItem.Click
        showMDIForm(frmMailPDF)
    End Sub

    Private Sub UploadForecastOnLotnumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadForecastOnLotnumberToolStripMenuItem.Click
        frmUploadForecast.str_tableName = sender.tag
        showMDIForm(frmUploadForecast)
    End Sub

    Private Sub FindDuplicateStylesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindDuplicateStylesToolStripMenuItem.Click
        showMDIForm(frmFindDuplicateStyles)
    End Sub

    Private Sub updateFinalFOBs(ByVal tableName As String)
        Dim recTable As DataTable = getSelectDataSet("SELECT * FROM dbo." & tableName).Tables(0)
        For Each recRow As DataRow In recTable.Rows
            Dim maxFOB As Integer = 15
            If SeasonTable.Select("Season = '" & recRow("Season") & "'").Length > 0 Then
                maxFOB = CInt(SeasonTable.Select("Season = '" & recRow("Season") & "'")(0).Item("MaxFOB"))
            End If
            Dim finalFob As String = ""
            For i_counter As Integer = maxFOB To 1 Step -1
                If Not IsDBNull(recRow("ProtoFob" & i_counter)) Then
                    If recRow("ProtoFob" & i_counter) > 0 Then
                        finalFob = recRow("ProtoFob" & i_counter)
                        Exit For
                    End If
                End If
            Next
            executeSQL("Update dbo." & tableName & " SET FinalFob = " & IIf(finalFob.Length = 0, "NULL", "'" & finalFob & "'") & " where DevNo = " & recRow("DevNo"))
        Next
    End Sub

    Private Sub UpdateAllFinalFOBsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateAllFinalFOBsToolStripMenuItem.Click
        updateFinalFOBs("Salesyouth")
        updateFinalFOBs("NewGrid")

        frmMaintenance.calculateTable("SalesYouth")
        frmMaintenance.calculateTable("NewGrid")

        frmMainGrid.btnRefresh_Click(Nothing, Nothing)
    End Sub

    'Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllToolStripMenuItem.Click
    '    AllToolStripMenuItem.CheckOnClick 'ReportMerchaindinsing6ToolStripMenuItem.Click,
    'End Sub

    Private Sub btnAutomationExport_Click(sender As Object, e As EventArgs) Handles btnAutomationExport.Click
        frmAutoGenCostingSheet.Show()
    End Sub

    Private Sub UploadPotentialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadPotentialToolStripMenuItem.Click
        showMDIForm(frmUploadPotential)
    End Sub

    Private Sub UploadForecastMTGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadForecastMTGToolStripMenuItem.Click
        frmUploadForecast.str_tableName = sender.tag
        showMDIForm(frmUploadForecast)
    End Sub


    Private Sub tBtnVersion_Click(sender As System.Object, e As System.EventArgs) Handles tBtnVersion.Click
        MsgBox("Version 2.9.2, Released on 03 July 2018")
    End Sub
End Class
