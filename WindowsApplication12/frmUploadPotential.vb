Public Class frmUploadPotential

    Private xlsApp As Object
    Private columnData As New DataSet
    Private str_lblLoading As String = "Loading"
    Private i_lblLoading As Integer

    Private Sub cmdBrowse_Click(sender As Object, e As EventArgs) Handles cmdBrowse.Click
        xlsApp = CreateObject("Excel.Application")
        Dim dialogBrowse = New System.Windows.Forms.OpenFileDialog

        If CInt(xlsApp.Version) >= 12 Then
            dialogBrowse.Filter = "Excel files (*.xls,*.xlsx) |*.xls;*.xlsx"
        Else
            dialogBrowse.Filter = "Excel files (*.xls) |*.xls"
        End If
        dialogBrowse.Title = "Potential upload file"
        If dialogBrowse.ShowDialog() = DialogResult.OK Then
            txtFileName.Text = dialogBrowse.FileName
            cmdUpload.Enabled = True

            Dim xlsWorkBook As Object
            Dim xlsWorkSheet As Object

            executeSQL("DELETE FROM dbo.tmp_PotentialExcelUpload WHERE userNaam='" & UserId & "'")
            xlsWorkBook = xlsApp.Workbooks.Open(txtFileName.Text)
            lstSheets.Items.Clear()
            For Each xlsWorkSheet In xlsApp.activeworkbook.worksheets
                lstSheets.Items.Add(xlsWorkSheet.Name)
                Dim i_col As Integer = 1
                Do While Not xlsWorkSheet.cells(1, i_col).value = Nothing
                    Dim str_columnHeader As String = ""
                    If Not xlsWorkSheet.cells(1, i_col).value Is Nothing Then
                        str_columnHeader = xlsWorkSheet.cells(1, i_col).value.ToString
                    End If
                    Dim str_columnValue As String = ""
                    If Not xlsWorkSheet.cells(2, i_col).value Is Nothing Then
                        str_columnValue = xlsWorkSheet.cells(2, i_col).value.ToString
                    End If
                    executeSQL("INSERT INTO dbo.tmp_PotentialExcelUpload VALUES ('" & UserId & "','" & xlsWorkSheet.Name & "','" & i_col & "','" & str_columnHeader & "','" & str_columnValue & "')")
                    i_col += 1
                Loop
            Next
            lstSheets.SelectedItems.Add(lstSheets.Items(0))
            lstSheets_Click(Nothing, Nothing)
        End If
        'If dialogBrowse.ShowDialog() = DialogResult.OK Then
        '    txtFileName.Text = dialogBrowse.FileName

        '    Dim xlsWorkBook As Object
        '    Dim xlsWorkSheet As Object
        '    Dim sLog As String = "Potentail values uploaded  by user:" & UserId

        '    xlsWorkBook = xlsApp.Workbooks.Open(txtFileName.Text)
        '    For Each xlsWorkSheet In xlsApp.activeworkbook.worksheets
        '        Dim i_row As Integer = 1
        '        If xlsWorkSheet.cells(i_row, 1).value <> "season" And Not xlsWorkSheet.cells(i_row, 2).value <> "jbano" And Not xlsWorkSheet.cells(i_row, 3).value <> "potential" Then
        '            MsgBox("Invalid excel template", MsgBoxStyle.Critical, "Invalid template")
        '            lstSheets.Items.Add("Upload Aborted!")
        '            Exit Sub
        '        End If
        '        i_row = 2
        '        lstSheets.Items.Clear()
        '        Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing And Not xlsWorkSheet.cells(i_row, 2).value = Nothing And Not xlsWorkSheet.cells(i_row, 3).value = Nothing
        '            Dim sQuery As String = ""

        '            sQuery = "update newgrid set potential=" & xlsWorkSheet.cells(i_row, 3).value & " where season='" & xlsWorkSheet.cells(i_row, 1).value & "' and jbano='" & xlsWorkSheet.cells(i_row, 2).value & "'"
        '            sLog &= vbCrLf & "updating potential=" & xlsWorkSheet.cells(i_row, 3).value & " for season='" & xlsWorkSheet.cells(i_row, 1).value & "' and jbano='" & xlsWorkSheet.cells(i_row, 2).value & "'"
        '            lstSheets.Items.Add("updating potential=" & xlsWorkSheet.cells(i_row, 3).value & " for season='" & xlsWorkSheet.cells(i_row, 1).value & "' and jbano='" & xlsWorkSheet.cells(i_row, 2).value & "'")

        '            executeSQL(sQuery)
        '            i_row += 1
        '        Loop
        '    Next
        '    lstSheets.Items.Add("Upload Completed.")
        '    MsgBox("Upload Completed")
        '    SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", "Napalinelist - Potential file Uploaded", sLog)
        'End If

    End Sub

    Private Sub lstSheets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSheets.Click
        grdColumns.DataSource = Nothing
        grdColumns.ClearFields()
        columnData.Clear()
        columnData = getSelectDataSet("SELECT columnID, columnHeader, columnFirstRow FROM dbo.tmp_PotentialExcelUpload WHERE userNaam='" & UserId & "' AND worksheet='" & lstSheets.SelectedItem & "'")
        grdColumns.DataSource = columnData
        grdColumns.DataMember = columnData.Tables(0).ToString
        grdColumns.Rebind(True)
    End Sub

    Private Sub frmUploadPotential_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            xlsApp.quit()
        Catch
        End Try
        xlsApp = Nothing

    End Sub

    Private Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Dim i_row As Integer = 2
        Dim xlsWorkSheet As Object
        Dim sLog As String = "Potentail values uploaded  by user:" & UserId & vbCrLf

        xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
        UploadingLabel(True)
        Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing
            If Not xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value Is Nothing Then
                'executeSQL("INSERT INTO tbl_Linelist_forecast (Season, Lotnumber, Forecast, ForecastDate) VALUES " & _
                '          "('" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value & "','" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value & "','" & xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value & "','" & Today & "')")
                Dim sQuery As String = ""

                sQuery = "update newgrid set potential=" & xlsWorkSheet.cells(i_row, CInt(txtPotential.Text)).value & " where season='" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value & "' and jbano='" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value & "'"
                sLog &= vbCrLf & "updating potential=" & xlsWorkSheet.cells(i_row, CInt(txtPotential.Text)).value & " for season='" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value & "' and jbano='" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value & "'"
                lstLog.Items.Add("updating potential=" & xlsWorkSheet.cells(i_row, CInt(txtPotential.Text)).value & " for season='" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value & "' and jbano='" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value & "'")

                executeSQL(sQuery)

                applyFormulas("potential", xlsWorkSheet.cells(i_row, CInt(txtPotential.Text)).value, xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value, xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value)

            End If
            i_row += 1
        Loop
        lstLog.Items.Add("Upload Completed.")
        MsgBox("Upload Completed")
        SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", "Napalinelist - Potential file Uploaded", sLog)
        UploadingLabel(False)
    End Sub

    Private Sub UploadingLabel(ByVal run As Boolean)
        cmdUpload.Enabled = Not run
        tmrCount.Enabled = run
        i_lblLoading = str_lblLoading.Length
        lblUploading.Visible = run
    End Sub

    Private Sub txtColumnNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPotential.KeyPress, txtSeason.KeyPress, txtJBA.KeyPress
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub frmUploadPotential_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSeason.Text = "1"
        txtJBA.Text = "2"
        txtPotential.Text = "3"
    End Sub


    Private Sub applyFormulas(ByVal ColumnName As String, ByVal value As String, ByVal JBANO As String, ByVal season As String)
        Dim sql As String
        Dim odt As DataTable
        Try
            sql = "select columnname,formulastring from gridlayout where formulastring like '%[" & ColumnName & "]%'"
            odt = getSelectDataSet(sql).Tables(0)
            If odt.Rows.Count > 0 Then
                For Each dr As DataRow In odt.Rows ' multiple rows 
                    Dim sFormula, supdateColumn, sformulaFields, sformulaEval, aryFormulaFields() As String
                    Dim dtNewgrid As DataTable
                    sFormula = dr("formulastring")
                    supdateColumn = dr("columnname")
                    sformulaFields = getFormulaFields(sFormula)
                    sformulaEval = sFormula
                    'sformulaFields = "'2' RetailHigh,'5' Potential,'2.6' Markup " 'remove
                    dtNewgrid = getSelectDataSet("select " & sformulaFields & " from newgrid where season='" & season & "' and JBANO='" & JBANO & "'").Tables(0)

                    'sformulaFields = getFormulaFields(sFormula) 'remove
                    aryFormulaFields = sformulaFields.Split(",")

                    If dtNewgrid.Rows.Count > 0 Then
                        Dim sVal, sqlExec, sUpdateSql As String
                        Dim odtExec As DataTable
                        For i As Integer = 0 To aryFormulaFields.Length - 1
                            If (LCase(aryFormulaFields(i)).Trim = LCase(ColumnName).Trim) Then
                                sVal = value
                            Else
                                sVal = IIf(IsDBNull(dtNewgrid.Rows(0)(aryFormulaFields(i))), "", dtNewgrid.Rows(0)(aryFormulaFields(i)))
                            End If
                            If sVal = "" Then
                                sformulaEval = String.Empty
                                sUpdateSql = "update newgrid set " & supdateColumn & " = NULL where season='" & season & "' and JBANO='" & JBANO & "'"
                                executeSQL(sUpdateSql)
                                Exit For
                            End If
                            sformulaEval = sformulaEval.Replace("[" & aryFormulaFields(i) & "]", sVal)
                        Next
                        'MsgBox(sformulaEval)
                        If sformulaEval <> String.Empty Then
                            sformulaEval = sformulaEval.Replace("=", "")
                            sqlExec = "Exec ('select " & sformulaEval & "')"
                            odtExec = getSelectDataSet(sqlExec).Tables(0)
                            If odtExec.Rows.Count > 0 Then
                                sVal = Math.Round(odtExec.Rows(0)(0), 0)
                                'MsgBox(odtExec.Rows(0)(0) & vbCrLf & "sval:" & sVal)
                                sUpdateSql = "update newgrid set " & supdateColumn & " = " & sVal & " where season='" & season & "' and JBANO='" & JBANO & "'"
                                executeSQL(sUpdateSql)
                            End If

                        End If

                    End If
                Next

            End If

        Catch ex As Exception
            'SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", "Napalinelist Error Apply formula", "Error Applying Formula JBANO:" & JBANO & " column:" & ColumnName)
        End Try

    End Sub


End Class