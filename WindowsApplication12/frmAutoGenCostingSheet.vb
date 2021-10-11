Public Class frmAutoGenCostingSheet

    
    Private Sub btnAutomateExport_Click(sender As Object, e As EventArgs) Handles btnAutomateExport.Click
        ExcelGenerate()
    End Sub
    Public Sub ExcelGenerate()

        Dim sTemplateFileName As String = sAS_NAPARootFolder & "NAPAFiles\CostingBDTemplate.xlsx"
        Dim sExportDir As String = sAS_NAPARootFolder & "NAPAFiles\"
        Dim sExportFileName As String

        Dim str_fields As String = ""
        Dim str_fabricfields As String = ""
        Dim str_Accfields As String = ""
        Dim str_Trimsfields As String = ""
        Dim chkFabricsChecked As Boolean = False
        Dim chkAccChecked As Boolean = False
        Dim chkTrimsChecked As Boolean = False
        Dim sSeason As String = ""
        Dim dtSeason As DataTable
        dtSeason = getSelectDataSet("SELECT Season FROM napa.dbo.Param_Season WHERE (CostBreakDown <> 0)").Tables(0)
        If dtSeason.Rows.Count > 0 Then
            sSeason = dtSeason.Rows(0)(0)
        End If


        Dim dtTemplateFields As DataTable
        dtTemplateFields = getSelectDataSet("SELECT * FROM dbo.tbl_ExcelReports WHERE templateName='template_CBSExport'").Tables(0)
        If dtTemplateFields.Rows.Count > 0 Then
            str_fields = dtTemplateFields.Rows(0)("fields").ToString()
            If dtTemplateFields.Rows(0)("chkfabrics").ToString = "True" Then
                chkFabricsChecked = True
                str_fabricfields = dtTemplateFields.Rows(0)("fabricfields").ToString()
            End If
            If dtTemplateFields.Rows(0)("chkAcc").ToString = "True" Then
                chkAccChecked = True
                str_Accfields = dtTemplateFields.Rows(0)("accfields").ToString()
            End If
            If dtTemplateFields.Rows(0)("chkTrims").ToString = "True" Then
                chkTrimsChecked = True
                str_Trimsfields = dtTemplateFields.Rows(0)("trimsfields").ToString()
            End If

            sTemplateFileName = dtTemplateFields.Rows(0)("templatepath").ToString()
        Else
            Exit Sub
        End If

        If System.IO.File.Exists(sTemplateFileName) And System.IO.Directory.Exists(sExportDir) Then 'And sExportFileName.Length > 0
            Dim oXL As Object = CreateObject("Excel.Application")
            Dim oWB As Object
            Dim oSheet As Object
            oXL.DisplayAlerts = False
            oXL.Visible = False

            'start using the view vw_Report_CostingBreakdown_Main
            'SELECT top 1 * FROM vw_Report_CostingBreakdown_Shanker WHERE MOC091=1
            Dim dstColumns As DataRow() = getSelectDataSet("SELECT top 1 * FROM vw_Report_CostingBreakdown_Main WHERE MOC091=1").Tables(0).Select("") '"Split = 0")
            'Dim dstColumns As DataRow() = getSelectDataSet("SELECT ColumnName, Split FROM dbo.GridLayout WHERE(GridName = N'grdMain')").Tables(0).Select("") '"Split = 0")
            Dim str_columns As String = ""
            For i As Integer = 0 To dstColumns.Length - 1
                str_columns &= "dbo.NewGrid." & dstColumns(i).Item(0) & IIf(i < dstColumns.Length - 1, ", ", "")
            Next
            Dim dtColumns As DataTable = getSelectDataSet("SELECT top 10 * FROM vw_Report_CostingBreakdown_Main WHERE jbano is not null and  Season='" & sSeason & "'  and ( picture is not null and picture <> '')").Tables(0) '"Split = 0")
            'Dim dtColumns As DataTable = getSelectDataSet("SELECT " & str_columns & " FROM dbo.NewGrid INNER JOIN dbo.tmp_NewGridAfterFilter ON dbo.NewGrid.DevNo = dbo.tmp_NewGridAfterFilter.DevNo WHERE (dbo.tmp_NewGridAfterFilter.userNaam = N'" & UserId & "')").Tables(0)
            'Dim dtColumns As DataTable = getSelectDataSet("SELECT " & str_columns & " FROM dbo.NewGrid  WHERE (dbo.NewGrid.Season = '" & sSeason & "'  and isdeleted=0)").Tables(0)

            For i_grdRow As Integer = 0 To dtColumns.Rows.Count - 1
                'If grdStyles.Item(i_grdRow, 0).ToString = "True" Then
                Me.Cursor = Cursors.WaitCursor

                sExportFileName = "CostSheet-[Name]-(JBANo).xlsx"
                sExportFileName = sSeason & "-" & sExportFileName

                oWB = oXL.Workbooks.Open(sTemplateFileName)
                oSheet = oWB.ActiveSheet
                Dim tblRow As String() = Split(str_fields, " ~ ")
                For i As Integer = 0 To tblRow.Length - 1
                    Dim str_range As String = ""
                    Dim str_value As String = ""

                    Dim aryRangeValue() As String
                    aryRangeValue = tblRow(i).Split("|")
                    str_range = aryRangeValue(1)
                    str_value = aryRangeValue(0)
                    'For j As Integer = 0 To tblRow.ColumnCount - 1
                    '    Dim cntrl As Control = tblRow.GetControlFromPosition(j, i)
                    '    If Not IsNothing(cntrl) Then
                    '        Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                    '            Case "System.Windows.Forms.TextBox"
                    '                str_range = cntrl.Text.Trim
                    '            Case "System.Windows.Forms.ComboBox"
                    '                Dim cbo As ComboBox = cntrl
                    '                str_value = cbo.SelectedValue
                    '        End Select
                    '    End If
                    'Next
                    If str_range.Length > 0 Then
                        If LCase(str_value) = "picture" Or str_value = "sketch / picture" Then
                            Dim dataSet As DataSet = getSelectDataSet("SELECT picBlob_square FROM dbo.vf_Sketches WHERE lotnumber='" & dtColumns.Rows(i_grdRow)("DevNo") & "'")
                            'Dim dataSet As DataSet = getSelectDataSet("SELECT picBlob_square FROM dbo.vf_Sketches WHERE lotnumber='" & grdStyles.Item(i_grdRow, "DevNo") & "'")
                            Dim img As Image
                            Dim str_imagePath As String = sExportDir & "\tmp_image_" & UserId & ".jpg"
                            If dataSet.Tables(0).Rows.Count > 0 Then
                                Dim imageAsBytes As Byte() = dataSet.Tables(0).Rows(0).Item("picBlob_square")
                                img = Image.FromStream(New System.IO.MemoryStream(imageAsBytes))
                            Else
                                img = Image.FromFile(sAS_StartupPath & "\Resources\napa logo_square.gif")
                            End If
                            img.Save(str_imagePath)

                            Dim destWidth As Integer = 270
                            Dim destHeight As Integer = 180
                            Dim destLeft As Integer = oSheet.Range(str_range).Left + 2
                            Dim destTop As Integer = oSheet.Range(str_range).Top + 2
                            With oSheet.Range(str_range)

                                'If .Width > .Height Then
                                '    destHeight = .Height - 4
                                '    destWidth = .Height - 4
                                '    destLeft += (.Width - .Height) / 2
                                'Else
                                '    destHeight = .Width - 4
                                '    destWidth = .Width - 4
                                '    destTop += (.Height - .Width) / 2
                                'End If
                            End With

                            With oSheet.Shapes.AddPicture(str_imagePath, 0, True, destLeft, destTop, destWidth, destHeight)
                                .LockAspectRatio = -1
                                .Placement = 1
                            End With
                            img.Dispose()
                            If System.IO.File.Exists(str_imagePath) = True Then System.IO.File.Delete(str_imagePath)
                        Else
                            oSheet.Range(Trim(str_range)).Value = getValue(dtColumns.Rows(i_grdRow)("DevNo").ToString, str_value, dtColumns.Rows(i_grdRow)(str_value).ToString)
                            'oSheet.Range(str_range).Value = getValue(grdStyles.Item(i_grdRow, "DevNo").ToString, str_value, grdStyles.Item(i_grdRow, str_value).ToString)
                        End If
                    End If
                Next

                'try to get mainkey from drcolumns
                Dim dsData As DataSet = getSelectDataSet("SELECT * FROM vw_Report_CostingBreakdown WHERE MOC091='" & dtColumns.Rows(i_grdRow)("DevNo").ToString & "'")
                'Dim dsData As DataSet = getSelectDataSet("SELECT * FROM vw_Report_CostingBreakdown WHERE MOC091='" & dtColumns.Rows(i_grdRow)("DevNo").ToString & "'")
                'Dim dsData As DataSet = getSelectDataSet("SELECT * FROM vw_Report_CostingBreakdown WHERE MOC091='" & grdStyles.Item(i_grdRow, "DevNo").ToString & "'")
                If chkFabricsChecked = True Then fillExcelFields(str_fabricfields.Split("~"), dsData, "ARTNO='N1'", oSheet, False)
                If chkAccChecked = True Then fillExcelFields(str_Accfields.Split("~"), dsData, "ARTNO='N2'", oSheet, False)
                If chkTrimsChecked = True Then fillExcelFields(str_Trimsfields.Split("~"), dsData, "ARTNO='N3'", oSheet, False)

                sExportFileName = sExportFileName.Replace("(JBANo)", dtColumns.Rows(i_grdRow)("JBANo").ToString)
                sExportFileName = sExportFileName.Replace("[Name]", dtColumns.Rows(i_grdRow)("stylename").ToString)
                oWB.SaveAs(sExportDir & IIf(sExportDir.EndsWith("\"), "", "\") & sExportFileName)
                'oWB.SaveAs(sExportDir & IIf(sExportDir.EndsWith("\"), "", "\") & resolveFileName(sExportFileName, i_grdRow, dtColumns))
                If oWB IsNot Nothing Then
                    oWB.Close(False)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB)
                    oSheet = Nothing
                    oWB = Nothing
                End If
                Me.Cursor = Cursors.Default
                'End If
            Next
            If oXL IsNot Nothing Then
                oXL.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL)
                oXL = Nothing
            End If
            MsgBox("Export done!", MsgBoxStyle.Information, "Done")
        ElseIf Not System.IO.File.Exists(sTemplateFileName) Then
            MsgBox("Please select a template", MsgBoxStyle.Information, "No Template")
        ElseIf Not System.IO.Directory.Exists(sExportDir) Then
            MsgBox("Please select an export directory", MsgBoxStyle.Information, "No Export Directory")
        ElseIf sExportFileName.Length = 0 Then
            MsgBox("Please select a file name", MsgBoxStyle.Information, "No File Name")
        End If
    End Sub
    Private Sub fillExcelFields(ByVal tblRow() As String, ByVal dsData As DataSet, ByVal str_where As String, ByVal osheet As Object, radioSMSPricesChecked As Boolean)
        For i As Integer = 0 To tblRow.Length - 1
            Dim str_range As String = ""
            Dim str_value As String = ""

            Dim aryRangeValue() As String
            aryRangeValue = tblRow(i).Split("|")
            If aryRangeValue.Length > 1 Then
                str_range = aryRangeValue(1)
                str_value = aryRangeValue(0).Trim()
            End If

            If str_range.Length > 1 Then
                Dim i_row As Integer = 0
                For Each dsRow As DataRow In dsData.Tables(0).Select(str_where)
                    Dim str_content As String = ""
                    For Each str_column As String In str_value.Split("|")
                        If str_column = "Unit Price" Then
                            str_column += IIf(radioSMSPricesChecked, " SMS", " Bulk")
                        End If
                        str_content &= IIf(str_content.Length > 0 And dsRow(str_column).ToString.Length > 0, " - ", "") & dsRow(str_column).ToString
                    Next
                    osheet.Range(str_range).Offset(i_row, 0).Value = str_content
                    i_row += 1
                Next
            End If
        Next i
        'For i As Integer = 0 To tblTable.RowCount - 1
        '    Dim str_range As String = ""
        '    Dim str_value As String = ""
        '    For j As Integer = 0 To tblTable.ColumnCount - 1
        '        Dim cntrl As Control = tblTable.GetControlFromPosition(j, i)
        '        If Not IsNothing(cntrl) Then
        '            Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
        '                Case "System.Windows.Forms.TextBox"
        '                    str_range = cntrl.Text.Trim
        '                Case "System.Windows.Forms.Label"
        '                    If cntrl.Text.IndexOf("Column: ") >= 0 Then str_value = cntrl.Tag
        '            End Select
        '        End If
        '    Next
        '    If str_range.Length > 0 Then
        '        Dim i_row As Integer = 0
        '        For Each dsRow As DataRow In dsData.Tables(0).Select("ARTNO='" & str_where & "'")
        '            Dim str_content As String = ""
        '            For Each str_column As String In str_value.Split("|")
        '                If str_column = "Unit Price" Then
        '                    str_column += IIf(radioSMSPrices.Checked, " SMS", " Bulk")
        '                End If
        '                str_content &= IIf(str_content.Length > 0 And dsRow(str_column).ToString.Length > 0, " - ", "") & dsRow(str_column).ToString
        '            Next
        '            osheet.Range(str_range).Offset(i_row, 0).Value = str_content
        '            i_row += 1
        '        Next
        '    End If
        'Next
    End Sub

    Private Function resolveFileName(ByVal str_filename As String, ByVal i_row As Integer, dtcolumns As DataTable) As String
        If str_filename.IndexOf("[") > 0 And str_filename.IndexOf("]") > 0 Then
            For i As Integer = 0 To dtcolumns.Columns.Count - 1
                str_filename = str_filename.ToUpper.Replace("[" & dtcolumns.Columns(i).Caption.ToUpper & "]", dtcolumns.Rows(i_row)(i).ToString)
                'Debug.WriteLine(grdStyles.Columns(i).Caption)
            Next
        End If
        resolveFileName = str_filename.Replace("[", "").Replace("]", "") & IIf(str_filename.ToLower.EndsWith(".xls") Or str_filename.ToLower.EndsWith(".xlsx"), "", ".xls")
    End Function


    Private Sub fillFields(ByVal tblTable As TableLayoutPanel, ByVal str_fields As String)
        If str_fields.Length > 0 Then
            Dim str_Rows As String() = Split(str_fields, " ~ ")
            For i As Integer = 0 To str_Rows.Length - 1
                'If tblTable.Name = "tblRow" Then btnAddField_Click(Nothing, Nothing)
                For j As Integer = 0 To tblTable.ColumnCount - 1
                    Dim cntrl As Control = tblTable.GetControlFromPosition(j, i)
                    If Not IsNothing(cntrl) Then
                        Select Case cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(","))
                            Case "System.Windows.Forms.TextBox"
                                cntrl.Text = Split(str_Rows(i), "|")(1)
                            Case "System.Windows.Forms.ComboBox"
                                Dim cbo As ComboBox '= tblRow.GetControlFromPosition(j, i)
                                cbo.SelectedValue = Split(str_Rows(i), "|")(0)
                        End Select
                    End If
                Next
            Next
        End If
    End Sub


End Class