Public Class frmUploadForecast
    Private xlsApp As Object
    Private columnData As New DataSet
    Private str_lblLoading As String = "Loading"
    Private i_lblLoading As Integer

    Public str_tableName As String = ""

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        xlsApp = CreateObject("Excel.Application")

        Dim dialogBrowse As New System.Windows.Forms.OpenFileDialog
        If CInt(xlsApp.Version) >= 12 Then
            dialogBrowse.Filter = "Excel files (*.xls,*.xlsx) |*.xls;*.xlsx"
        Else
            dialogBrowse.Filter = "Excel files (*.xls) |*.xls"
        End If
        dialogBrowse.Title = "Forecast file"

        If dialogBrowse.ShowDialog() = DialogResult.OK Then
            txtFileName.Text = dialogBrowse.FileName
            cmdUpload.Enabled = True

            Dim xlsWorkBook As Object
            Dim xlsWorkSheet As Object

            executeSQL("DELETE FROM dbo.tmp_importForeCastExcelColumns WHERE userNaam='" & UserId & "'")
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
                    executeSQL("INSERT INTO dbo.tmp_importForeCastExcelColumns VALUES ('" & UserId & "','" & xlsWorkSheet.Name & "','" & i_col & "','" & str_columnHeader & "','" & str_columnValue & "')")
                    i_col += 1
                Loop
            Next
            lstSheets.SelectedItems.Add(lstSheets.Items(0))
            lstSheets_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmUploadForecast_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            xlsApp.quit()
        Catch
        End Try
        xlsApp = Nothing
        frmMainGrid.btnRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub frmUploadForecast_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case str_tableName
            Case "NewGrid", "ForecastMTG", "tbl_Linelist_forecast"
                lblFCastBuy.Visible = False
                lblPO.Visible = False
                lblP0_1.Visible = False
                lblP0_2.Visible = False
                lblP0_3.Visible = False
                lblP0_4.Visible = False
                lblP0_5.Visible = False
                lblP0_6.Visible = False
                lblP0_7.Visible = False
                lblP0_8.Visible = False
                lblP0_9.Visible = False
                lblP0_10.Visible = False
                lblP0_11.Visible = False
                lblForecastdate.Visible = False
        End Select
        Select Case str_tableName
            Case "NewGrid"
                lblSeason.Text = "Season Column:"
                lblJBA.Text = "JBANo Column:"
                lblFcast.Text = "Forecast Column:"
            Case "ForecastMTG"
                lblSeason.Text = "Season Column:"
                lblJBA.Text = "JBANo Column:"
                lblFcast.Text = "ForecastMTG Column:"
            Case "tbl_Linelist_forecast"
                lblSeason.Text = "Season Column:"
                lblJBA.Text = "Lotnumber Column:"
                lblFcast.Text = "Forecast Column:"
            Case "tbl_Linelist_forecast_buy"
                lblSeason.Text = "Season Column:"
                txtSeason.Text = "1"
                lblJBA.Text = "Lotnumber Column:"
                txtJBA.Text = "2"
                lblFcast.Text = "Buy Column:"
                txtFCast.Text = "3"
                lblFCastBuy.Text = "Forecast Column:"
                txtFCastBuy.Text = "4"
                lblPO.Text = "PO Precollection:"
                txtPO.Text = "8"
                lblP0_1.Text = "PO Buy 0:"
                txtPO_1.Text = "9"
                lblP0_2.Text = "PO Buy 1:"
                txtPO_2.Text = "10"
                lblP0_3.Text = "PO Buy 1bis:"
                txtPO_3.Text = "11"
                lblP0_4.Text = "PO Buy 2:"
                txtPO_4.Text = "12"
                lblP0_5.Text = "PO Buy 2bis:"
                txtPO_5.Text = "13"
                lblP0_6.Text = "PO Buy 3:"
                txtPO_6.Text = "14"
                lblP0_7.Text = "PO Buy 3bis:"
                txtPO_7.Text = "15"
                lblP0_8.Text = "PO Buy 4:"
                txtPO_8.Text = "16"
                lblP0_9.Text = "Total Trims PO:"
                txtPO_9.Text = "17"
                lblP0_10.Text = "Total PO Japan NO EU:"
                txtPO_10.Text = "18"
                lblP0_11.Text = "Total PO Canada:"
                txtPO_11.Text = "19"
                lblForecastdate.Text = "Forecast Date:"
                txtForecastdate.Text = "7"
        End Select
        txtFCastBuy.Visible = lblFCastBuy.Visible
        txtPO.Visible = lblPO.Visible
        txtPO_1.Visible = lblP0_1.Visible
        txtPO_2.Visible = lblP0_2.Visible
        txtPO_3.Visible = lblP0_3.Visible
        txtPO_4.Visible = lblP0_4.Visible
        txtPO_5.Visible = lblP0_5.Visible
        txtPO_6.Visible = lblP0_6.Visible
        txtPO_7.Visible = lblP0_7.Visible
        txtPO_8.Visible = lblP0_8.Visible
        txtPO_9.Visible = lblP0_9.Visible
        txtPO_10.Visible = lblP0_10.Visible
        txtPO_11.Visible = lblP0_11.Visible
        txtForecastdate.Visible = lblForecastdate.Visible
    End Sub

    Private Sub frmUploadForecast_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdColumns.Width = Me.Width - 50
        grdColumns.Height = Me.Height - grdColumns.Top - 40
    End Sub

    Private Sub lstSheets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSheets.Click
        grdColumns.DataSource = Nothing
        grdColumns.ClearFields()
        columnData.Clear()
        columnData = getSelectDataSet("SELECT columnID, columnHeader, columnFirstRow FROM dbo.tmp_importForeCastExcelColumns WHERE userNaam='" & UserId & "' AND worksheet='" & lstSheets.SelectedItem & "'")
        grdColumns.DataSource = columnData
        grdColumns.DataMember = columnData.Tables(0).ToString
        grdColumns.Rebind(True)
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim i_row As Integer = 2
        Dim xlsWorkSheet As Object
        Dim a_Seasons(2)() As String
        ReDim a_Seasons(0)(0)
        ReDim a_Seasons(1)(0)

        Select str_tableName
            Case "NewGrid"
                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                Do While Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value = Nothing
                    Dim b_alreadyIn As Boolean = False
                    For i As Integer = 0 To a_Seasons(0).Length - 1
                        If a_Seasons(0)(i) = xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.ToUpper Or xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.Length = 0 Then
                            b_alreadyIn = True
                        End If
                    Next
                    If b_alreadyIn = False Then
                        ReDim Preserve a_Seasons(0)(a_Seasons(0).Length - 1)
                        ReDim Preserve a_Seasons(1)(a_Seasons(0).Length - 1)
                        a_Seasons(0)(a_Seasons(0).Length - 1) = xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.ToUpper
                        a_Seasons(1)(a_Seasons(0).Length - 1) = CInt(getLastForecast(xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString).Replace("RevisedForecast", ""))
                    End If
                    i_row += 1
                Loop
                Dim str_seasons_nr As String = ""
                For i As Integer = 0 To a_Seasons(0).Length - 1
                    If a_Seasons(1)(i) > 8 Then
                        Dim i_answer As Integer = MessageBox.Show("The forecast fields for season " + a_Seasons(0)(i) + " are full." + vbCrLf + _
                                                                    IIf(i < a_Seasons(0).Length - 1, "Do you want to continue loading the other seasons?", ""), "Forcast fields full", _
                                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If i_answer = vbNo Then Exit Sub
                    End If
                    str_seasons_nr &= a_Seasons(0)(i) & " -> " & a_Seasons(1)(i) & " | "
                Next
                i_row = 2

                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                UploadingLabel(True)
                Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing
                    If Not xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value Is Nothing Then
                        Dim i_forecast As Integer = 9
                        For i As Integer = 0 To a_Seasons(0).Length - 1
                            If a_Seasons(0)(i).ToUpper = xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.ToUpper And a_Seasons(1)(i) < 9 Then
                                i_forecast = a_Seasons(1)(i)
                            End If
                        Next
                        If i_forecast < 9 Then
                            Dim str_fCast As String = "NULL"
                            If Not xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value Is Nothing Then
                                str_fCast = xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value.ToString
                            End If
                            executeSQL("UPDATE NewGrid SET RevisedForecast" & i_forecast & "= " & str_fCast & " , RevisedForecastFinal= " & str_fCast & " WHERE Right(JBANo,3)='" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value.ToString.Substring(xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value.ToString.Length - 3, 3) & "' AND Season='" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString & "'")
                        End If
                    End If
                    i_row += 1
                Loop
                UploadingLabel(False)
            Case "ForecastMTG"
                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                'Do While Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value = Nothing
                '    Dim b_alreadyIn As Boolean = False
                '    For i As Integer = 0 To a_Seasons(0).Length - 1
                '        If a_Seasons(0)(i) = xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.ToUpper Or xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.Length = 0 Then
                '            b_alreadyIn = True
                '        End If
                '    Next
                '    If b_alreadyIn = False Then
                '        ReDim Preserve a_Seasons(0)(a_Seasons(0).Length - 1)
                '        ReDim Preserve a_Seasons(1)(a_Seasons(0).Length - 1)
                '        a_Seasons(0)(a_Seasons(0).Length - 1) = xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString.ToUpper
                '        a_Seasons(1)(a_Seasons(0).Length - 1) = CInt(getLastForecast(xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString).Replace("RevisedForecast", ""))
                '    End If
                '    i_row += 1
                'Loop
                'Dim str_seasons_nr As String = ""
                'For i As Integer = 0 To a_Seasons(0).Length - 1
                '    If a_Seasons(1)(i) > 8 Then
                '        Dim i_answer As Integer = MessageBox.Show("The forecast fields for season " + a_Seasons(0)(i) + " are full." + vbCrLf + _
                '                                                    IIf(i < a_Seasons(0).Length - 1, "Do you want to continue loading the other seasons?", ""), "Forcast fields full", _
                '                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                '        If i_answer = vbNo Then Exit Sub
                '    End If
                '    str_seasons_nr &= a_Seasons(0)(i) & " -> " & a_Seasons(1)(i) & " | "
                'Next
                i_row = 2

                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                UploadingLabel(True)
                Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing
                    If Not xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value Is Nothing Then
                        Dim str_fCast As String = "NULL"
                        If Not xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value Is Nothing Then
                            str_fCast = xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value.ToString
                        End If
                        executeSQL("UPDATE NewGrid SET ForecastMTG = " & str_fCast & " WHERE JBANo='" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value.ToString & "' AND Season='" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value.ToString & "'")
                    End If
                    i_row += 1
                Loop
                UploadingLabel(False)

            Case "tbl_Linelist_forecast"
                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                UploadingLabel(True)
                Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing
                    If Not xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value Is Nothing Then
                        executeSQL("INSERT INTO tbl_Linelist_forecast (Season, Lotnumber, Forecast, ForecastDate) VALUES " & _
                                   "('" & xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value & "','" & xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value & "','" & xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value & "','" & Today & "')")
                    End If
                    i_row += 1
                Loop
                UploadingLabel(False)
            Case "tbl_Linelist_forecast_buy"
                xlsWorkSheet = xlsApp.activeworkbook.worksheets(lstSheets.SelectedItem)
                UploadingLabel(True)
                executeSQL("TRUNCATE TABLE tbl_Linelist_forecast_buy")
                Do While Not xlsWorkSheet.cells(i_row, 1).value = Nothing
                    If Not xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value Is Nothing And Not xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value Is Nothing Then
                        Dim str_sql As String = "INSERT INTO tbl_Linelist_forecast_buy ( " & _
                                        "Season, " & _
                                        "Lotnumber, " & _
                                        "Buy, " & _
                                        "Forecast, " & _
                                        "PO, " & _
                                        "PO_1, " & _
                                        "PO_2, " & _
                                        "PO_3, " & _
                                        "PO_4, " & _
                                        "PO_5, " & _
                                        "PO_6, " & _
                                        "PO_7, " & _
                                        "PO_8, " & _
                                        "PO_9, " & _
                                        "PO_10, " & _
                                        "PO_11, " & _
                                        "Forecastdate  " & _
                                    ")VALUES (" & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtSeason.Text)).value) & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtJBA.Text)).value) & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtFCast.Text)).value) & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtFCastBuy.Text)).value) & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_1.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_2.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_3.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_4.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_5.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_6.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_7.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_8.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_9.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_10.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtPO_11.Text)).value, "Int") & "', " & _
                                        "'" & getCellValue(xlsWorkSheet.cells(i_row, CInt(txtForecastdate.Text)).value) & "' " & _
                                    ")"
                        executeSQL(str_sql)
                    End If
                    i_row += 1
                Loop
                executeSQL("DELETE FROM tbl_Linelist_forecast WHERE SEASON IN (SELECT DISTINCT SEASON FROM tbl_Linelist_forecast_buy)")
                executeSQL("INSERT INTO tbl_Linelist_forecast SELECT [Season],[Lotnumber],SUM([Forecast]),MAX([Forecastdate]) FROM tbl_Linelist_forecast_buy GROUP BY [Season],[Lotnumber]")
                UploadingLabel(False)
        End Select
    End Sub

    Private Function getCellValue(ByVal cellValue As String, Optional ByVal outputType As String = "String") As String
        Dim returnValue As String = ""
        If Not IsNothing(cellValue) Then
            Select Case outputType
                Case "Int"
                    returnValue = cellValue.Replace("'", "")
                    If returnValue.Length > 0 Then
                        returnValue = CInt(returnValue)
                    End If
                Case Else
                    returnValue = cellValue.Replace("'", "")
            End Select
        End If
        Return returnValue
    End Function

    Private Function getLastForecast(ByVal str_season As String) As String
        Dim str_sql As String = ""
        Dim i As Integer

        For i = 1 To 8
            str_sql &= "SELECT 'RevisedForecast" & i & "' as fcast, ISNULL(SUM(RevisedForecast" & i & "),0) AS sumFcast FROM dbo.NewGrid WHERE Season='" & str_season & "'"
            If i < 8 Then str_sql &= "UNION "
        Next
        Dim forecastData As DataSet = getSelectDataSet(str_sql & " ORDER BY fcast DESC")
        getLastForecast = ""
        For i = forecastData.Tables(0).Rows.Count - 1 To 0 Step -1
            getLastForecast = forecastData.Tables(0).Rows(i).Item("fcast").ToString
            If forecastData.Tables(0).Rows(i).Item("sumFcast").ToString = "0" Then Exit For
        Next
    End Function

    Private Sub txtFCast_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFCast.KeyPress, txtSeason.KeyPress, txtJBA.KeyPress
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub UploadingLabel(ByVal run As Boolean)
        cmdUpload.Enabled = Not run
        tmrCount.Enabled = run
        i_lblLoading = str_lblLoading.Length
        lblUploading.Visible = run
    End Sub

    Private Sub tmrCount_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrCount.Tick
        lblUploading.Text = IIf(lblUploading.Text.Length - i_lblLoading > 5, str_lblLoading, lblUploading.Text & ".")
    End Sub
End Class