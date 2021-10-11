Imports System.Data.SqlClient
Imports C1.C1Excel
Imports System.IO
Imports System.Threading
Imports System.Globalization

Public Class frmMainGrid
    Private PreviousPart As String
    Private str_currentDevNo As String = ""
    Private b_formLoading As Boolean = True
    Private picH, picW, picT, picL As Integer
    Private oldValue As String
    Private YouthAdapter As New SqlDataAdapter
    Private YouthData As New DataSet
    Private CurrNotesColumn As C1.Win.C1TrueDBGrid.C1DataColumn

    Private Sub frmMainGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim SelectAdapter1 As New SqlDataAdapter
        Dim SelectData1 As New DataSet

        InputLanguage.FromCulture(lngInputLanguage.Culture)
        grdMain.FetchRowStyles = True

        grdMain.Splits(0).Name = "Split0"
        LoadGridLayout(Me, grdMain, "NewGrid", True)
        SelectData1 = getSelectDataSet("select Description from tbl_Materials order by Description")
        For i = 0 To SelectData1.Tables(0).Rows.Count - 1
            cmbComp1.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
            cmbComp2.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
            cmbComp3.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
            cmbComp4.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
            cmbComp5.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
            cmbComp6.Items.Add(SelectData1.Tables(0).Rows(i).Item("Description"))
        Next
        grdMain.Splits(0).DisplayColumns("JBAno").Locked = (Not bl_styleAdmin)
        grdMain.MaintainRowCurrency = True



        b_formLoading = False
        grdMain_AfterFilter(Nothing, Nothing)

        frmSplash.Close()
    End Sub

    Private Sub frmMainGrid_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveGridLayout(Me, grdMain)
    End Sub

    Private Sub frmMainGrid_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        btnExportToExcel.Top = Me.Height - btnExportToExcel.Height - 50
        btnExportToExcel.Left = Me.Width / 150

        Label3.Top = btnExportToExcel.Top
        Label3.Left = btnExportToExcel.Left + Label3.Width + btnExportToExcel.Left + 15
        ComboBox1.Top = btnExportToExcel.Top + btnExportToExcel.Height - ComboBox1.Height
        ComboBox1.Left = Label3.Left

        btnRefresh.Top = btnExportToExcel.Top
        btnRefresh.Left = ComboBox1.Left + ComboBox1.Width + btnExportToExcel.Left

        Label1.Top = btnExportToExcel.Top
        Label1.Left = btnRefresh.Left + btnRefresh.Width + btnExportToExcel.Left
        ComboBox2.Top = btnExportToExcel.Top + btnExportToExcel.Height - ComboBox2.Height
        ComboBox2.Left = Label1.Left

        btnClearFilters.Top = btnExportToExcel.Top + btnExportToExcel.Height - btnClearFilters.Height
        btnClearFilters.Left = ComboBox2.Left + ComboBox2.Width + btnExportToExcel.Left

        pnlMargins.Visible = b_showCosting
        pnlMargins.Top = Me.Height - pnlMargins.Height - 40
        pnlMargins.Left = btnClearFilters.Left + btnClearFilters.Width + btnExportToExcel.Left

        picStyle.Top = pnlMargins.Top
        picStyle.Left = pnlMargins.Left + pnlMargins.Width + btnExportToExcel.Left

        grdMain.Top = 5
        grdMain.Width = Me.Width - 25
        grdMain.Height = Me.Height - pnlMargins.Height - 80 - IIf(grdYouth.Visible, grdYouth.Height, 0)
        grdYouth.Width = Me.Width - 25
        grdYouth.Top = pnlMargins.Top - grdYouth.Height - 25

        picT = picStyle.Top
        picL = picStyle.Left
    End Sub

    Private Sub grdMain_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles grdMain.BeforeColUpdate
        oldValue = e.OldValue.ToString
    End Sub

    Private Sub grdMain_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdMain.AfterColUpdate
        ListBox1.Visible = False
        Dim Sql As String
        Dim MyId As String = grdMain.Item(grdMain.Row, "DevNo").ToString
        Dim myTotal As Integer = 0
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = e.Column.DataColumn 'Me.grdMain.FocusedSplit.DisplayColumns(Me.grdMain.Col).DataColumn

        If grdMain.Columns(col.DataField).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox Then Exit Sub

        Dim NewValue As String = grdMain.Item(grdMain.Row, col.DataField).ToString
        If NewValue <> oldValue Then
            executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & MyId & "', '" & col.DataField & "', '" & oldValue & "', '" & NewValue & "', '" & UserId & "')")
        End If

        applyFormulas(col.DataField, NewValue, MyId)



        Sql = "Update Newgrid set " & col.DataField & " = "
        Select Case col.DataType.ToString
            Case "System.Boolean"
                Sql = Sql & IIf(NewValue = "True", "-1", "0")
            Case "System.Int32", "System.Decimal"
                Sql = Sql & IIf(NewValue.Length = 0, "NULL", NewValue)
            Case "System.String"
                Sql = Sql & IIf(NewValue.Length = 0 Or NewValue = "TBD", "NULL", "'" & NewValue & "'")
            Case "System.DateTime"
                Sql = Sql & IIf(NewValue.Length = 0, "NULL", "CONVERT(DATETIME, '" & Format(CDate(NewValue), "yyyy-MM-dd") & "', 102)")
            Case Else
                MsgBox("Something is Wrong. Your data can be lost. Please Contact Patrick Waeytens", MsgBoxStyle.Critical)
                Exit Sub
        End Select
        executeSQL(Sql & " WHERE DevNo = " & MyId)

        Select Case True
            Case col.DataField = "Name", col.DataField = "JbaNo"
                If Not NewValue.Length = 0 Then
                    Sql = "SELECT devNo FROM NewGrid WHERE " & col.DataField & " = '" & NewValue & "' And DevNo <> " & MyId & " AND (Season = '" & grdMain.Item(grdMain.Row, "Season") & "' OR (NOT Season = '" & grdMain.Item(grdMain.Row, "Season") & "' AND ISNULL((SELECT TOP 1 GetNewCode FROM param_life WHERE Code = '" & grdMain.Item(grdMain.Row, "life") & "'),1) = 1))"
                    If getSelectDataSet(Sql).Tables(0).Rows.Count > 0 Then
                        MsgBox("This " & col.DataField & " already exists. Please choose another one", MsgBoxStyle.Exclamation, "Attention")
                        grdMain.Item(grdMain.Row, col.DataField) = oldValue
                    End If
                End If
            Case col.DataField = "ProposedRetail"
                If Not NewValue.Length = 0 Then
                    grdMain.Item(grdMain.Row, "Retail") = grdMain.Item(grdMain.Row, col.DataField)
                    executeSQL("Update NewGrid Set " & col.DataField & " = " & NewValue & ", Retail = " & NewValue & " where DevNo = " & MyId)
                Else
                    grdMain.Item(grdMain.Row, "Retail") = ""
                    executeSQL("Update NewGrid Set " & col.DataField & " = NULL, Retail = NULL where DevNo = " & MyId)
                End If
            Case col.DataField = "Standard_Quantity", col.DataField = "UsSelection", col.DataField = "UkSelection", col.DataField = "JapanSelection"
                myTotal = IIf(Not IsDBNull(grdMain.Item(grdMain.Row, "Standard_Quantity")), grdMain.Item(grdMain.Row, "Standard_Quantity"), 0)
                myTotal += IIf(Not IsDBNull(grdMain.Item(grdMain.Row, "UsSelection")), grdMain.Item(grdMain.Row, "UsSelection"), 0)
                myTotal += IIf(Not IsDBNull(grdMain.Item(grdMain.Row, "UkSelection")), grdMain.Item(grdMain.Row, "UkSelection"), 0)
                myTotal += IIf(Not IsDBNull(grdMain.Item(grdMain.Row, "JapanSelection")), grdMain.Item(grdMain.Row, "JapanSelection"), 0)
                grdMain.Item(grdMain.Row, "SMSQuantities") = IIf(myTotal > 0, myTotal, "")
                executeSQL("UPDATE NewGrid SET  SMSQuantities = " & IIf(myTotal > 0, myTotal, "NULL") & " WHERE DevNo = " & MyId)
            Case col.DataField.ToLower.StartsWith("protofob")
                Dim i_counter As Integer
                Dim maxFOB As Integer = 15
                If SeasonTable.Select("Season = '" & grdMain.Item(grdMain.Row, "Season") & "'").Length > 0 Then
                    maxFOB = CInt(SeasonTable.Select("Season = '" & grdMain.Item(grdMain.Row, "Season") & "'")(0).Item("MaxFOB"))
                End If
                For i_counter = maxFOB To 1 Step -1
                    If Not IsDBNull(grdMain.Item(grdMain.Row, "ProtoFob" & i_counter)) Then
                        If grdMain.Item(grdMain.Row, "ProtoFob" & i_counter) > 0 Then
                            grdMain.Item(grdMain.Row, "FinalFob") = grdMain.Item(grdMain.Row, "ProtoFob" & i_counter)
                            Exit For
                        Else
                            grdMain.Item(grdMain.Row, "FinalFob") = ""
                        End If
                    Else
                        grdMain.Item(grdMain.Row, "FinalFob") = ""
                    End If
                Next
                executeSQL("Update NewGrid set " & col.DataField & " = " & IIf(NewValue.Length = 0, "NULL", "'" & NewValue & "'") & _
                        ", FinalFob = " & IIf(IsDBNull(grdMain.Item(grdMain.Row, "FinalFob")), "NULL", "'" & grdMain.Item(grdMain.Row, "FinalFob") & "'") & " where DevNo = " & MyId)
            Case col.DataField.ToLower.StartsWith("revisedforecast")
                Dim i_counter As Integer
                For i_counter = 8 To 1 Step -1
                    If Not IsDBNull(grdMain.Item(grdMain.Row, "RevisedForecast" & i_counter)) Then
                        If grdMain.Item(grdMain.Row, "RevisedForecast" & i_counter) >= 0 Then
                            grdMain.Item(grdMain.Row, "RevisedForecastFinal") = grdMain.Item(grdMain.Row, "RevisedForecast" & i_counter)
                            Exit For
                        Else
                            grdMain.Item(grdMain.Row, "RevisedForecastFinal") = 0
                        End If
                    Else
                        grdMain.Item(grdMain.Row, "RevisedForecastFinal") = 0
                    End If
                Next
                executeSQL("Update NewGrid set " & col.DataField & " = " & IIf(Len(grdMain.Item(grdMain.Row, col.DataField).ToString) = 0, "NULL", "'" & grdMain.Item(grdMain.Row, col.DataField) & "'") & _
                        ", RevisedForecastFinal = " & IIf(IsDBNull(grdMain.Item(grdMain.Row, "RevisedForecastFinal")), "NULL", "'" & grdMain.Item(grdMain.Row, "RevisedForecastFinal") & "'") & " where DevNo = " & MyId)
            Case col.DataField = "Season", col.DataField = "Notes", col.DataField = "Major", col.DataField = "SourcingUS", _
                    col.DataField.ToLower.StartsWith("notused"), col.DataField = "SourcingAsia", col.DataField = "ActualSales", _
                    col.DataField = "WholeSale", col.DataField = "LavMat"
            Case col.DataField = "MarkUp", col.DataField = "Duty"
                executeSQL("UPDATE dbo.SalesYouth SET " & col.DataField & "=" & IIf(NewValue.Length = 0, "NULL", "'" & NewValue & "'") & " WHERE Mother=" & grdMain.Item(grdMain.Row, "DevNo"))
                If grdYouth.Visible = True Then
                    For i As Integer = grdYouth.FirstRow To grdYouth.RowCount - 1
                        grdYouth.Bookmark = i
                        gridCalculations(grdYouth, "SalesYouth")
                    Next
                End If
            Case col.DataField.EndsWith("_dropped")
                updateColorRow(grdMain.Item(grdMain.Row, "DevNo"), True, grdMain.Row)
                Try

                    Dim sColumnName As String = col.DataField.ToString()
                    Dim sDevno As String = grdMain.Item(grdMain.Row, "DevNo").ToString()
                    Dim bChecked As Boolean = grdMain.Item(grdMain.Row, col.DataField)

                    sColumnName = sColumnName & "Date"
                    If bChecked Then
                        executeSQL("update NewGrid set " & sColumnName & "=GETDATE() where devno= " & sDevno)
                        grdMain.Item(grdMain.Row, sColumnName) = Now.Date.ToString()
                    Else
                        executeSQL("update NewGrid set " & sColumnName & "=NULL where devno= " & sDevno)
                        grdMain.Item(grdMain.Row, sColumnName) = ""
                    End If

                Catch ex As Exception

                End Try
                Exit Sub
            Case Else
                Exit Sub
        End Select

        gridCalculations(grdMain, "NewGrid")

        'Debug.WriteLine("End grdMain_AfterColUpdate")
    End Sub

    Private Sub grdMain_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdMain.ButtonClick
        Dim foundrows() As Data.DataRow
        Dim ComboAdapter As New SqlDataAdapter
        Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection
        Dim mySql As String
        Dim j As Integer

        'Debug.WriteLine("Begin grdMain_ButtonClick")

        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = e.Column.DataColumn

        OldValue = IIf(IsDBNull(grdMain.Item(grdMain.Row, col.DataField)), "", grdMain.Item(grdMain.Row, col.DataField))

        foundrows = LayoutData.Tables(0).Select("ColumnName = '" & col.DataField & "' And SeasonLinked = 'True'")
        If foundrows.Length > 0 Then
            ComboAdapter.SelectCommand = dcNAPA.CreateCommand
            Dim ComboData As New DataSet
            mySql = foundrows(0).Item("DropDownSQL")
            If mySql.ToString.ToUpper.IndexOf("ORDER BY") > 0 Then mySql = mySql.ToString.Substring(0, mySql.ToString.ToUpper.IndexOf("ORDER BY"))
            mySql &= " INNER JOIN dbo.Param_Seasons_Napa " & _
                        "ON " & foundrows(0).Item("SeasonName") & " LIKE '%' + dbo.Param_Seasons_Napa.Season + '%'" & _
                        "WHERE (dbo.Param_Seasons_Napa.Season = '" & grdMain.Columns("Season").CellValue(grdMain.Row) & "') "
            If Not IsDBNull(foundrows(0).Item("ExtraSeasonField")) Then mySql &= " AND " & foundrows(0).Item("ExtraSeasonField")
            mySql &= IIf(col.DataField.ToLower.StartsWith("prodcolor") Or col.DataField.ToLower.StartsWith("smscolor"), " order by colourart", "")
            ComboData = getSelectDataSet(mySql)
            v = Me.grdMain.Columns(foundrows(0).Item("columnName")).ValueItems.values
            v.Clear()
            v.Add(New C1.Win.C1TrueDBGrid.ValueItem("TBD", "To Be Defined"))
            If ComboData.Tables(0).Rows.Count > 0 Then
                For j = 0 To ComboData.Tables(0).Rows.Count - 1
                    If col.DataField.ToLower.StartsWith("prodcolor") Or col.DataField.ToLower.StartsWith("smscolor") Then
                        v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundrows(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundrows(0).Item("DropDownValueMember")) & " " & ComboData.Tables(0).Rows(j).Item(foundrows(0).Item("DropDownDisplayMember"))))
                    Else
                        v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundrows(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundrows(0).Item("DropDownDisplayMember"))))
                    End If
                Next
                Me.grdMain.Columns(foundrows(0).Item("columnName")).ValueItems.Translate = True
                Me.grdMain.Columns(foundrows(0).Item("columnName")).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                Me.grdMain.Columns(foundrows(0).Item("columnName")).FilterDropdown = True
                Me.grdMain.FocusedSplit.DisplayColumns(foundrows(0).Item("columnName")).DropDownList = True
            End If
        End If

        'Debug.WriteLine("End grdMain_ButtonClick")
    End Sub

    Private Sub grdMain_ComboSelect(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdMain.ComboSelect
        'Debug.WriteLine("Begin grdMain_ComboSelect")

        If grdMain.FilterActive Then Exit Sub
        If grdMain.RowCount = 0 Then Exit Sub
        If grdMain.SelectedText = "(All)" Then Exit Sub

        Dim myid As Long = grdMain.Item(grdMain.Row, "DevNo").ToString
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = e.Column.DataColumn 'Me.grdMain.FocusedSplit.DisplayColumns(Me.grdMain.Col).DataColumn
        Dim NewValue As String = e.Column.DataColumn.Value ' grdMain.Item(grdMain.Row, col.DataField).ToString

        If NewValue <> oldValue Then
            executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & myid & "', '" & col.DataField & "', '" & oldValue & "', '" & NewValue & "', '" & UserId & "')")
        End If

        Select Case True
            Case col.DataField = "SmsProducer", col.DataField = "VendorBulk"
                grdMain.Item(grdMain.Row, col.DataField) = NewValue
                SplitAndUpdate_Producers(col.DataField, col.CellText(grdMain.Row), NewValue, myid, grdMain.Row)
            Case col.DataField = "Fabric"
                If grdMain.Columns("Fabric").Text.ToString.Length = 0 Then
                    grdMain.Item(grdMain.Row, "Composition") = ""
                    grdMain.Item(grdMain.Row, "FabricInfo") = ""
                    grdMain.Item(grdMain.Row, "FabricsCode") = ""
                    grdMain.Item(grdMain.Row, "Fabric") = ""
                    executeSQL("Update NewGrid set Fabric = NULL, Composition = NULL, FabricInfo = NULL, FabricsCode = NULL where DevNo = " & myid)
                Else
                    Dim SelectData As DataSet = getSelectDataSet("Select * From tbl_FabricInfo where ID = " & NewValue)
                    If SelectData.Tables(0).Rows.Count > 0 Then
                        grdMain.Item(grdMain.Row, "Composition") = SelectData.Tables(0).Rows(0).Item("Composition")
                        grdMain.Item(grdMain.Row, "FabricInfo") = SelectData.Tables(0).Rows(0).Item("FabricInfo")
                        grdMain.Item(grdMain.Row, "FabricsCode") = SelectData.Tables(0).Rows(0).Item("FabricCode")
                        grdMain.Item(grdMain.Row, "Fabric") = NewValue
                        executeSQL("Update NewGrid set Fabric = " & NewValue & ", Composition = '" & SelectData.Tables(0).Rows(0).Item("Composition") _
                                & "', FabricInfo = '" & SelectData.Tables(0).Rows(0).Item("FabricInfo") _
                                & "', FabricsCode = '" & SelectData.Tables(0).Rows(0).Item("FabricCode") & "' where DevNo = " & myid)
                    End If
                End If
            Case col.DataField.ToLower.StartsWith("prodcolor"), col.DataField.ToLower.StartsWith("smscolor")
                grdMain.Item(grdMain.Row, col.DataField) = NewValue
                Dim b_smsColor As Boolean = col.DataField.ToLower.StartsWith("smscolor")
                Dim str_ProdColor As String = "ProdColor" & IIf(CInt(Replace(col.DataField, IIf(b_smsColor, "SmsColor", "ProdColor"), "")) < 10, "0", "") & Replace(col.DataField, IIf(b_smsColor, "SmsColor", "ProdColor"), "")
                'condition needed here
                'If  col.DataField.ToLower.StartsWith("smscolor13") Then
                '    str_ProdColor = ""
                'End If
                PreviousPart = col.CellText(grdMain.Row)
                Dim ValueText As String = grdMain.SelectedText
                Dim ValuePart As String = "NULL"
                If NewValue = "TBD" Or Len(NewValue) = 0 Then ' Or NewValue = "000" removed to accomodate color NEUTRO
                    If Not col.DataField.ToLower.StartsWith("smscolor13") Then If b_smsColor Then grdMain.Item(grdMain.Row, str_ProdColor) = ""
                Else
                    ValuePart = "'" & grdMain.Item(grdMain.Row, col.DataField) & "'"
                    If Not col.DataField.ToLower.StartsWith("smscolor13") Then If b_smsColor Then grdMain.Item(grdMain.Row, str_ProdColor) = NewValue
                End If
                'If Not col.DataField.ToLower.StartsWith("smscolor13") Then
                Dim squery1 As String = "Update NewGrid set " & col.DataField & " = " & ValuePart & IIf(Not col.DataField.ToLower.StartsWith("smscolor13"), IIf(b_smsColor, ", " & str_ProdColor & " = " & ValuePart, ""), "") & " where DevNo = " & myid
                executeSQL("Update NewGrid set " & col.DataField & " = " & ValuePart & IIf(Not col.DataField.ToLower.StartsWith("smscolor13"), IIf(b_smsColor, ", " & str_ProdColor & " = " & ValuePart, ""), "") & " where DevNo = " & myid)
                NewValue = IIf(ValuePart = "NULL", "", Replace(ValuePart, "'", ""))
                updateColorRow(grdMain.Item(grdMain.Row, "DevNo"), True, grdMain.Row)
            Case col.DataField = "Currency", col.DataField = "SourceLocation"
                If NewValue = "TBD" Or Len(NewValue) = 0 Then
                    executeSQL("Update NewGrid set " & col.DataField & " = NULL where DevNo = " & myid)
                    If grdMain.Row > 0 Then grdMain.Item(grdMain.Row, col.DataField) = ""
                Else
                    executeSQL("Update NewGrid set " & col.DataField & " = '" & NewValue & "' where DevNo = " & myid)
                End If
                SendKeys.Send("{escape}")
                grdMain.Item(grdMain.Row, col.DataField) = NewValue
                gridCalculations(grdMain, "NewGrid")
                executeSQL("UPDATE dbo.SalesYouth SET " & col.DataField & "=" & IIf(NewValue.Length = 0, "NULL", "'" & NewValue & "'") & " WHERE Mother=" & grdMain.Item(grdMain.Row, "DevNo"))
                If grdYouth.Visible = True Then
                    For i As Integer = grdYouth.FirstRow To grdYouth.RowCount - 1
                        grdYouth.Bookmark = i
                        gridCalculations(grdYouth, "SalesYouth")
                    Next
                End If
            Case Else
                If NewValue = "TBD" Or Len(NewValue) = 0 Then
                    executeSQL("Update NewGrid set " & col.DataField & " = NULL where DevNo = " & myid)
                    If grdMain.Row > 0 Then grdMain.Item(grdMain.Row, col.DataField) = ""
                Else
                    executeSQL("Update NewGrid set " & col.DataField & " = '" & NewValue & "' where DevNo = " & myid)
                End If
                SendKeys.Send("{escape}")
                grdMain.Item(grdMain.Row, col.DataField) = NewValue
        End Select

        calculateMargin()
        writeDevNoToTable()
        SaveGridLayout(Me, grdMain)

        'Debug.WriteLine(col.Value)
        'Debug.WriteLine("End grdMain_ComboSelect")

    End Sub

    Private Sub grdMain_FetchCellStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs) Handles grdMain.FetchCellStyle
        Dim HaveCombination As Boolean
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet

        'Debug.WriteLine("Begin grdMain_FetchCellStyle")

        SearchData = getSelectDataSet("Select Gender, Major From SplitRecords")
        HaveCombination = False
        For i As Integer = 0 To SearchData.Tables(0).Rows.Count - 1
            If SearchData.Tables(0).Rows(i).Item("Gender").ToString = grdMain.Item(e.Row, "Gender").ToString And SearchData.Tables(0).Rows(i).Item("Major").ToString = grdMain.Item(e.Row, "Major").ToString Then
                HaveCombination = True
                Exit For
            End If
        Next
        Select Case True
            Case e.Column.DataColumn.DataField = "DevNo"
                e.CellStyle.BackColor = IIf(grdMain.Item(e.Row, "SMSQuantityRequired") = -1, Color.DeepPink, Color.White)
            Case e.Column.DataColumn.DataField = "JbaNo"
                e.CellStyle.BackColor = IIf(grdMain.Item(e.Row, "StyleExist") = 1, Color.GreenYellow, Color.White)
            Case e.Column.DataColumn.DataField = "ProposedRetail", e.Column.DataColumn.DataField = "Retail"
                e.CellStyle.Locked = IIf(HaveCombination = True, True, False)
                e.CellStyle.BackColor = IIf(grdMain.Item(e.Row, "RetailOK") = -1, Color.Yellow, Color.White)
            Case e.Column.DataColumn.DataField.ToLower.StartsWith("protofob") ', "Duty"
                e.CellStyle.Locked = IIf(HaveCombination = True, True, False)
            Case e.Column.DataColumn.DataField = "Freeze", e.Column.DataColumn.DataField = "StyleStatus"
                e.CellStyle.Locked = False
            Case e.Column.DataColumn.DataField.ToLower.StartsWith("color")
                Try
                    Dim int As Integer = CInt(e.Column.DataColumn.DataField.ToLower.Replace("color", ""))
                    If int <= i_smsColorCount Then
                        If grdMain.Item(e.Row, "smsColor" & int & "_dropped").ToString = "True" Then e.CellStyle.BackColor = Color.LightSalmon
                    Else
                        If grdMain.Item(e.Row, "prodColor" & IIf(int < 10, "0" & int, int) & "_dropped").ToString = "True" Then e.CellStyle.BackColor = Color.LightSalmon
                    End If
                Catch ex As Exception

                End Try
            Case Else
                'Debug.WriteLine("   FetchCellStyle not handled: " & e.Col)
        End Select

        For Each colSplit As C1.Win.C1TrueDBGrid.C1DisplayColumn In grdMain.Splits(e.Split).DisplayColumns
            If colSplit.DataColumn.DataField.ToLower().StartsWith("freezesplit") Then
                If colSplit.DataColumn.CellValue(e.Row).ToString() = "True" And colSplit.Visible = True And Not e.Column.DataColumn.DataField.ToLower().StartsWith("freezesplit") Then
                    e.CellStyle.Locked = True
                End If
            End If
        Next

        With e.CellStyle
            If grdMain.Columns("Freeze").CellText(e.Row).ToString = "True" Then
                If e.Split = 0 Then ' added - for only the main split
                    .Locked = True
                Else
                    .Locked = False
                End If
            End If
        End With
        'Debug.WriteLine("End grdMain_FetchCellStyle")
    End Sub

    Public Sub grdMain_FetchRowStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs) Handles grdMain.FetchRowStyle
        'Debug.WriteLine("Begin grdMain_FetchRowStyle")

        If grdMain.RowCount = 0 Then Exit Sub

        With e.CellStyle
            If grdMain.Columns("Freeze").CellText(e.Row).ToString = "True" Then

                If e.Split = 0 Then ' added - for only the main split
                    .Locked = True
                End If
                .BackColor = Me.BackColor
            Else
                .Locked = False
            End If

            If grdMain.Columns("StyleStatus").CellValue(e.Row).ToString = "1" Then
                .Locked = True
                .BackColor = Color.LightSalmon
            Else
                .Locked = False
            End If
        End With

        'Debug.WriteLine("End grdMain_FetchRowStyle")
    End Sub

    Private Sub grdMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMain.MouseDown
        Dim SearchData As New DataSet
        Dim foundRows() As Data.DataRow

        'Debug.WriteLine("Begin grdMain_MouseDown")

        foundRows = Nothing
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdMain.FocusedSplit.DisplayColumns(Me.grdMain.Col).DataColumn
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Select Case col.DataField
                'Case "JbaNo"
                '    Dim ComboData As New DataSet
                '    ComboAdapter.SelectCommand = dcNAPA.CreateCommand
                '    mySql = "SELECT * FROM tbl_UsedFabricCodes WHERE (InUse = 0)"
                '    ComboAdapter.SelectCommand.CommandText = mySql
                '    ComboAdapter.Fill(ComboData)
                '    ListBox1.DataSource = ComboData.Tables(0)
                '    ListBox1.DisplayMember = "FabricCode"
                '    ListBox1.Update()
                '    ListBox1.Left = e.X
                '    ListBox1.Top = e.Y
                '    ListBox1.Visible = True
                Case "DevNo"
                    grdMain.ContextMenuStrip = ContextMenuStrip1
                    ContextMenuStrip1.Visible = True
                Case "Picture"
                    grdMain.ContextMenuStrip = New ContextMenuStrip
                    If userGroup = "SUPER" Then
                        grdMain.ContextMenuStrip.Items.Add("Update Picture", Nothing, New EventHandler(AddressOf mnuPicAndLabelA_mnuClick)).Tag = grdMain.Row
                        grdMain.ContextMenuStrip.Items.Add("Remove Picture", Nothing, New EventHandler(AddressOf mnuPicAndLabelA_mnuClick)).Tag = grdMain.Row
                    End If
                Case Else
                    foundRows = LayoutData.Tables(0).Select("ColumnName = '" & grdMain.FocusedSplit.DisplayColumns(col).DataColumn.DataField & "' And NoCopyDown = True")
                    If foundRows.Length > 0 Then Exit Sub
                    If Not IsDBNull(grdMain.Columns(grdMain.FocusedSplit.DisplayColumns(col).DataColumn.DataField).Value) Then
                        If grdMain.FocusedSplit.DisplayColumns(col).Locked = False Then
                            grdMain.ContextMenuStrip = mnuCopyDown
                            If grdMain.FocusedSplit.DisplayColumns(col).DataColumn.DataType.ToString = "System.DateTime" Then
                                DeleteDateToolStripMenuItem.Text = "Delete Date"
                                DeleteDateToolStripMenuItem.Visible = True
                                CopyDownToolStripMenuItem.Visible = True
                                EditTemplateToolStripMenuItem.Visible = False
                                DeleteTemplateToolStripMenuItem.Visible = False
                            Else
                                DeleteDateToolStripMenuItem.Visible = False
                                CopyDownToolStripMenuItem.Visible = True
                                EditTemplateToolStripMenuItem.Visible = False
                                DeleteTemplateToolStripMenuItem.Visible = False
                            End If
                        Else
                            grdMain.ContextMenuStrip = Nothing
                        End If
                    End If
            End Select
        End If

        'Debug.WriteLine("End grdMain_MouseDown")
    End Sub

    Public Sub grdMain_AfterFilter(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles grdMain.AfterFilter
        If Not isLoadingSplit Then
            calculateMargin()
            writeDevNoToTable()
            SaveGridLayout(Me, grdMain)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub grdMain_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles grdMain.RowColChange
        'grdMain.Height = Me.Height - 65 - Button1.Height - 25
        If grdMain.RowCount = 1 Or grdMain.Item(grdMain.Row, "DevNo").ToString <> str_currentDevNo Then
            'grdYouth.Visible = False
            str_currentDevNo = grdMain.Item(grdMain.Row, "DevNo").ToString

            'Dim c As Integer
            Dim i As Integer
            Dim index As Integer
            Dim ComboAdapter As New SqlDataAdapter
            'Dim ComboData As New DataSet
            Dim SearchAdapter As New SqlDataAdapter
            Dim SearchData As New DataSet
            Dim mySql As String
            Dim foundRows() As Data.DataRow
            Dim sql As String
            Dim j As Integer
            'Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection
            Dim adapter As New SqlDataAdapter
            Dim HaveCombination As Boolean

            'Debug.WriteLine("Begin grdMain_DoubleClick")
            foundRows = Nothing
            'c = Int(Val(Mid(grdMain.FocusedSplit.Name, 9, 1)))
            Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdMain.FocusedSplit.DisplayColumns(Me.grdMain.Col).DataColumn

            picStyle.ImageLocation = ""
            picStyle.Enabled = False
            picStyle.Visible = False
            If grdMain.Item(grdMain.Row, "Picture").ToString.Length > 0 Then
                Dim dataSet As DataSet = getSelectDataSet("SELECT picBlob, picWidth, picHeight FROM dbo.vf_Sketches WHERE lotnumber='" & grdMain.Item(grdMain.Row, "DevNo").ToString & "'")
                If dataSet.Tables(0).Rows.Count > 0 Then
                    Dim imageAsBytes As Byte() = dataSet.Tables(0).Rows(0).Item("picBlob")
                    Dim picWidth As Integer = dataSet.Tables(0).Rows(0).Item("picWidth")
                    Dim picHeight As Integer = dataSet.Tables(0).Rows(0).Item("picHeight")
                    picStyle.Image = Image.FromStream(New System.IO.MemoryStream(imageAsBytes))
                    If picHeight > picWidth Then
                        picStyle.Height = 80
                        picStyle.Width = picWidth * picStyle.Height / picHeight
                    Else
                        picStyle.Width = 80
                        picStyle.Height = picHeight * picStyle.Width / picWidth
                    End If
                    picStyle.Enabled = IIf(grdMain.Item(grdMain.Row, "Picture").ToString.Length > 0, True, False)
                    picStyle.Visible = True
                    'End if
                End If
            End If

            If grdMain.FocusedSplit.DisplayColumns(col).DataColumn.DataField = "GarmentComposition" Then
                If grdMain.Item(grdMain.Row, grdMain.Col).ToString <> "" Then
                    Dim str_split() As String
                    str_split = Split(grdMain.Item(grdMain.Row, grdMain.Col).ToString, ",")
                    Dim str_per() As String
                    For i = 0 To UBound(str_split)
                        str_per = Split(str_split(i), "%")
                        Select Case i
                            Case 0
                                txtComp1.Text = str_per(0)
                                index = cmbComp1.FindString(str_per(1))
                                cmbComp1.SelectedIndex = index
                            Case 1
                                txtComp2.Text = str_per(0)
                                index = cmbComp2.FindString(str_per(1))
                                cmbComp2.SelectedIndex = index
                            Case 2
                                txtComp3.Text = str_per(0)
                                index = cmbComp3.FindString(str_per(1))
                                cmbComp3.SelectedIndex = index
                            Case 4
                                txtComp4.Text = str_per(0)
                                index = cmbComp4.FindString(str_per(1))
                                cmbComp4.SelectedIndex = index
                            Case 5
                                txtComp5.Text = str_per(0)
                                index = cmbComp5.FindString(str_per(1))
                                cmbComp5.SelectedIndex = index
                            Case 6
                                txtComp6.Text = str_per(0)
                                index = cmbComp6.FindString(str_per(1))
                                cmbComp6.SelectedIndex = index
                        End Select
                    Next i
                Else
                    txtComp1.Text = vbNullString
                    txtComp2.Text = vbNullString
                    txtComp3.Text = vbNullString
                    txtComp4.Text = vbNullString
                    txtComp5.Text = vbNullString
                    txtComp6.Text = vbNullString
                    cmbComp1.SelectedIndex = 0
                    cmbComp2.SelectedIndex = 0
                    cmbComp3.SelectedIndex = 0
                    cmbComp4.SelectedIndex = 0
                    cmbComp5.SelectedIndex = 0
                    cmbComp6.SelectedIndex = 0
                End If
                pnlComposition.Location = New Point((Me.Width - pnlComposition.Width) / 2, (Me.Height - pnlComposition.Height) / 2)
                pnlComposition.Visible = True
            End If

            SearchData = getSelectDataSet("Select Gender, Major From SplitRecords WHERE Gender='" & grdMain.Item(grdMain.Row, "Gender") & "' AND Major='" & grdMain.Item(grdMain.Row, "Major") & "'")
            HaveCombination = IIf(SearchData.Tables(0).Rows.Count > 0, True, False)

            If grdMain.FocusedSplit.Caption = "Costing" And HaveCombination = True Then
                YouthData.Clear()
                Try
                    YouthData.Tables(0).Columns.Clear()
                Catch ex As Exception
                    'MsgBox(ex.ToString)
                End Try

                grdYouth.DataSource = Nothing
                ComboAdapter.SelectCommand = dcNAPA.CreateCommand
                YouthAdapter.SelectCommand = dcNAPA.CreateCommand
                mySql = "SELECT * FROM SalesYouth WHERE Mother = " & grdMain.Item(grdMain.Row, "DevNo")
                YouthAdapter.SelectCommand.CommandText = mySql
                YouthAdapter.Fill(YouthData)

                Dim fieldArray(,) As String = {{"Season", "s"}, {"Duty", ""}, {"Currency", "s"}, {"SourceLocation", "s"}, _
                    {"Lavmat", ""}, {"Major", "s"}, {"SourcingUS", ""}, {"Note", "s"}, {"Markup", ""}, _
                    {"ActualSales", ""}, {"VendorBulk", "s"}, {"MadeInBulk", "s"}, {"NotUsed", ""}, {"NotUsed2", ""}, _
                    {"NotUsed3", ""}, {"NotUsed4", ""}, {"SourcingEurope", ""}, {"Retail", ""}, {"ProposedRetail", ""}, _
                    {"ProtoFob1", ""}, {"ProtoFob2", ""}, {"ProtoFob3", ""}, {"ProtoFob4", ""}, {"ProtoFob5", ""}, _
                    {"ProtoFob6", ""}, {"ProtoFob7", ""}, {"ProtoFob8", ""}, {"ProtoFob9", ""}, {"ProtoFob10", ""}, _
                    {"FinalFob", ""}, {"FobInEuro", ""}, {"SourcingAsia", ""}, {"Freight", ""}, {"Airfreight", ""}, _
                    {"Duty2", ""}, {"Operations", ""}, {"Planning", ""}, {"QualityControl", ""}, {"Reserve", ""}, _
                    {"TurinBuilding", ""}, {"TurinWarehouse", ""}, {"Turin_RMSourcing", ""}, {"StandardCost", ""}, _
                    {"Wholesale", ""}, {"GrossMargin", ""}, {"ActualsSales", ""}}

                If YouthData.Tables(0).Rows.Count = 0 Then
                    Dim ds_SizeMasks As DataSet = getSelectDataSet("SELECT SizeMask, Checked FROM param_Sizemasks WHERE Brand='N' AND Major='" & grdMain.Item(grdMain.Row, "Major") & "'")
                    For Each dr_SizeMask As DataRow In ds_SizeMasks.Tables(0).Rows
                        sql = "Insert INTO SalesYouth (SizeMask, Mother, ToJBA, Season, Duty, Currency, SourceLocation, Lavmat, Major, SourcingUS, " _
                        & "Note, Markup, ActualSales, VendorBulk, MadeInBulk, NotUsed, NotUsed2, NotUsed3, NotUsed4, SourcingEurope, " _
                        & "Retail, ProposedRetail, ProtoFob1, ProtoFob2, ProtoFob3, ProtoFob4, ProtoFob5, ProtoFob6, ProtoFob7, ProtoFob8, ProtoFob9, ProtoFob10, " _
                        & "FinalFob, FobInEuro,SourcingAsia, Freight, Airfreight, Duty2, Operations, Planning, QualityControl, Reserve, TurinBuilding, TurinWarehouse, " _
                        & "Turin_RMSourcing, StandardCost, Wholesale, GrossMargin, ActualsSales) " _
                        & "Values('" & dr_SizeMask("SizeMask") & "', " & grdMain.Item(grdMain.Row, "DevNo") & "," & IIf(dr_SizeMask("Checked").ToString = "True", 1, 0)
                        For i = 0 To (fieldArray.Length / 2) - 1
                            sql = sql & ", " & IIf(Not IsDBNull(grdMain.Item(grdMain.Row, fieldArray(i, 0))), IIf(fieldArray(i, 1) = "s", "'", "") & grdMain.Item(grdMain.Row, fieldArray(i, 0)) & IIf(fieldArray(i, 1) = "s", "'", ""), " NULL")
                        Next
                        executeSQL(sql & ")")
                    Next
                    YouthData.Clear()
                    Try
                        YouthData.Tables(0).Columns.Clear()
                    Catch ex As Exception
                        ' MsgBox(ex.ToString)
                    End Try
                    grdYouth.DataSource = Nothing
                    YouthAdapter.SelectCommand = dcNAPA.CreateCommand
                    mySql = "SELECT * FROM SalesYouth WHERE Mother = " & grdMain.Item(grdMain.Row, "DevNo")
                    YouthAdapter.SelectCommand.CommandText = mySql
                    YouthAdapter.Fill(YouthData)
                Else
                    sql = "Update SalesYouth set "
                    For i = 0 To 15
                        sql = sql & IIf(Not IsDBNull(grdMain.Item(grdMain.Row, fieldArray(i, 0))), fieldArray(i, 0) & "=" & IIf(fieldArray(i, 1) = "s", "'", "") & grdMain.Item(grdMain.Row, fieldArray(i, 0)) & IIf(fieldArray(i, 1) = "s", "'", "") & ", ", "")
                    Next
                    sql = Mid(sql, 1, Len(sql) - 2) & " WHERE Mother = " & grdMain.Item(grdMain.Row, "DevNo")
                    YouthData.Clear()
                    Try
                        YouthData.Tables(0).Columns.Clear()
                    Catch ex As Exception
                        ' MsgBox(ex.ToString)
                    End Try
                    grdYouth.DataSource = Nothing
                    YouthAdapter.SelectCommand = dcNAPA.CreateCommand
                    mySql = "SELECT * FROM SalesYouth WHERE Mother = " & grdMain.Item(grdMain.Row, "DevNo")
                    YouthAdapter.SelectCommand.CommandText = mySql
                    YouthAdapter.Fill(YouthData)
                End If
                grdYouth.DataSource = YouthData
                grdYouth.DataMember = YouthData.Tables(0).ToString
                grdYouth.Rebind(True)

                Dim YouthLayoutData = getSelectDataSet("SELECT * From GridLayout WHERE gridName = 'grdYouth' ORDER BY Split ")

                Dim foundRowsYouth() As Data.DataRow
                For i = 0 To grdYouth.Columns.Count - 1
                    foundRowsYouth = YouthLayoutData.Tables(0).Select("ColumnName = '" & grdYouth.Columns(i).DataField & "'")
                    If foundRowsYouth.Length > 0 Then
                        grdYouth.Columns(i).Caption = IIf(foundRowsYouth(0).Item("columnDescription").ToString.Length = 0, grdYouth.Columns(i).DataField, foundRowsYouth(0).Item("columnDescription").ToString)
                        If foundRowsYouth(0).Item("HasDropDown").ToString = "True" Then
                            Dim ComboData As New DataSet
                            ComboData = getSelectDataSet(foundRowsYouth(0).Item("DropDownSQL"))
                            Dim vYouth As C1.Win.C1TrueDBGrid.ValueItemCollection = grdYouth.Columns(foundRowsYouth(0).Item("columnName")).ValueItems.Values
                            If Not grdYouth.Columns(i).DataField = "StyleStatus" Then
                                'v.Add(New C1.Win.C1TrueDBGrid.ValueItem("", ""))
                                vYouth.Add(New C1.Win.C1TrueDBGrid.ValueItem("TBD", "To Be Defined"))
                            End If
                            For j = 0 To ComboData.Tables(0).Rows.Count - 1
                                If grdYouth.Columns(i).DataField.ToLower.StartsWith("prodcolor") Or grdYouth.Columns(i).DataField.ToLower.StartsWith("smscolor") Then
                                    vYouth.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRowsYouth(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundRowsYouth(0).Item("DropDownValueMember")) & " " & ComboData.Tables(0).Rows(j).Item(foundRowsYouth(0).Item("DropDownDisplayMember"))))
                                Else
                                    vYouth.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRowsYouth(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundRowsYouth(0).Item("DropDownDisplayMember"))))
                                End If
                            Next
                            grdYouth.Columns(foundRowsYouth(0).Item("columnName")).ValueItems.Translate = True
                            grdYouth.Columns(i).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                            grdYouth.Columns(i).FilterDropdown = True
                            grdYouth.Splits(0).DisplayColumns(i).DropDownList = True
                            ComboData = Nothing
                        End If
                        If foundRowsYouth(0).Item("Locked").ToString = "True" Or grdYouth.Columns(i).DataField = "Picture" Then
                            grdYouth.Splits(0).DisplayColumns(i).Locked = True
                        End If
                        If foundRowsYouth(0).Item("Split").ToString = "0" And Not foundRowsYouth(0).Item("InVisible").ToString = "True" Then
                            grdYouth.Splits(0).DisplayColumns(i).Visible = True
                        Else
                            grdYouth.Splits(0).DisplayColumns(i).Visible = False
                        End If
                        If foundRowsYouth(0).Item("formatNumber").ToString.Length > 0 Then
                            grdYouth.Columns(i).NumberFormat = foundRowsYouth(0).Item("formatNumber").ToString
                        End If
                    End If
                    grdYouth.Columns(i).EnableDateTimeEditor = True
                    grdYouth.Splits(0).DisplayColumns(i).FetchStyle = True
                Next
                If AllowYouthCosting = False Then grdYouth.Splits(0).Locked = True
                grdMain.Height = grdYouth.Top - 25
                grdYouth.Visible = True
            Else
                grdMain.Height = Me.Height - 65 - btnExportToExcel.Height - 25
                grdYouth.Visible = False
            End If
        End If

        'Debug.WriteLine("End grdMain_RowColChange")
    End Sub

    Private Sub grdMain_Filter(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles grdMain.Filter
        grdMain.Height = Me.Height - btnExportToExcel.Height - 80
        grdYouth.Visible = False
    End Sub

    Public Sub grdMain_FilterChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.FilterChange
        Me.Cursor = Cursors.WaitCursor
        If Not isLoadingSplit Then
            ComboBox2.Items.Clear()
            For Each dc As C1.Win.C1TrueDBGrid.C1DataColumn In grdMain.Columns
                'Debug.WriteLine(dc.Caption & " --------------------> " & dc.FilterText)
                If dc.FilterText.Length > 0 And dc.FilterText <> "(All)" Then
                    Dim Sql As String = "SELECT SplitHeaders.Description FROM GridLayout INNER JOIN " _
                            & "SplitHeaders ON GridLayout.Split = SplitHeaders.Split WHERE (GridLayout.ColumnName = '" & dc.DataField & "')"
                    Dim SelectData As DataSet = getSelectDataSet(Sql)
                    If SelectData.Tables(0).Rows.Count > 0 Then
                        ComboBox2.Items.Add(SelectData.Tables(0).Rows(0).Item("Description") & ": " & dc.DataField & ": " & dc.FilterText)
                    Else
                        ComboBox2.Items.Add("Main: " & dc.DataField & ": " & dc.FilterText)
                    End If
                End If
            Next
        End If
    End Sub


    Private Sub grdYouth_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles grdYouth.BeforeColUpdate
        oldValue = e.OldValue.ToString
    End Sub

    Private Sub grdYouth_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdYouth.AfterColUpdate
        Dim Sql As String
        Dim MyId As String
        Dim i, i_counter As Integer
        Dim NewValue As String

        MyId = grdYouth.Item(grdYouth.Row, "DevNo").ToString
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = e.Column.DataColumn 'Me.grdYouth.Columns(Me.grdYouth.Col)

        NewValue = grdYouth.Item(grdYouth.Row, col.DataField).ToString
        If NewValue <> oldValue Then
            executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & grdYouth.Item(grdYouth.Row, "Mother").ToString & "', 'Youth: " & MyId & " - " & col.DataField & "', '" & oldValue.ToString & "', '" & NewValue & "', '" & UserId & "')")
        End If

        Select Case True
            Case col.DataField = "ToJBA"
                For i = 0 To grdYouth.RowCount - 1
                    grdYouth.Item(i, "ToJBA") = IIf(i = grdYouth.Row, -1, 0)
                    executeSQL("Update SalesYouth Set ToJBA = '" & grdYouth.Item(i, "ToJBA") & "' where DevNo = " & grdYouth.Item(i, "DevNo"))
                Next
            Case col.DataField.ToLower.StartsWith("protofob")
                Dim maxFOB As Integer = 15
                If SeasonTable.Select("Season = '" & grdYouth.Item(grdYouth.Row, "Season") & "'").Length > 0 Then
                    maxFOB = CInt(SeasonTable.Select("Season = '" & grdYouth.Item(grdYouth.Row, "Season") & "'")(0).Item("MaxFOB"))
                End If
                For i_counter = maxFOB To 1 Step -1
                    If Not IsDBNull(grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter)) Then
                        If grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter) > 0 Then
                            grdYouth.Item(grdYouth.Row, "FinalFob") = grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter)
                            Exit For
                        Else
                            grdYouth.Item(grdYouth.Row, "FinalFob") = ""
                        End If
                    Else
                        grdYouth.Item(grdYouth.Row, "FinalFob") = ""
                    End If
                Next
                Sql = "Update SalesYouth set " & col.DataField & " = " & IIf(NewValue.Length = 0, "NULL", "'" & NewValue & "'") & _
                        ", FinalFob = " & IIf(grdYouth.Item(grdYouth.Row, "FinalFob").ToString.Length = 0, "NULL", "'" & grdYouth.Item(grdYouth.Row, "FinalFob") & "'") & " where DevNo = " & MyId
                executeSQL(Sql)
            Case Else
                Sql = "Update SalesYouth set " & col.DataField & " = "
                Select Case col.DataType.ToString
                    Case "System.Boolean"
                        Sql = Sql & IIf(NewValue = "True", "-1", "0")
                    Case "System.Int32", "System.Decimal"
                        Sql = Sql & IIf(NewValue.Length = 0, "NULL", NewValue)
                    Case "System.String"
                        Sql = Sql & IIf(NewValue.Length = 0, "NULL", "'" & NewValue & "'")
                    Case "System.DateTime"
                        Sql = Sql & IIf(NewValue.Length = 0, "NULL", "CONVERT(DATETIME, '" & Format(CDate(NewValue), "yyyy-MM-dd") & "', 102)")
                    Case Else
                        MsgBox("Something is Wrong. Your data can be lost. Please Contact Patrick Waeytens", MsgBoxStyle.Critical)
                        Exit Sub
                End Select
                executeSQL(Sql & " Where DevNo = " & MyId)
        End Select

        gridCalculations(grdYouth, "SalesYouth")

        If grdYouth.Item(grdYouth.Row, "ToJBA").ToString = "True" Then
            Sql = "UPDATE NewGrid SET "
            grdMain.Item(grdMain.Row, "LavMat") = grdYouth.Item(grdYouth.Row, "LavMat").ToString
            Sql &= "LavMat=" & IIf(grdYouth.Item(grdYouth.Row, "LavMat").ToString.Length = 0, "NULL", grdYouth.Item(grdYouth.Row, "LavMat")) & ", "
            For i_counter = 10 To 1 Step -1
                If Not grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter).ToString.Length = 0 Then
                    Sql &= "ProtoFob" & i_counter & "=" & grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter) & ", "
                    grdMain.Item(grdMain.Row, "ProtoFob" & i_counter) = grdYouth.Item(grdYouth.Row, "ProtoFob" & i_counter)
                Else
                    Sql &= "ProtoFob" & i_counter & "=NULL, "
                    grdMain.Item(grdMain.Row, "ProtoFob" & i_counter) = ""
                End If
            Next
            grdMain.Item(grdMain.Row, "Retail") = IIf(grdYouth.Item(grdYouth.Row, "ProposedRetail").ToString.Length = 0, "", grdYouth.Item(grdYouth.Row, "ProposedRetail"))
            Sql &= "Retail=" & IIf(grdYouth.Item(grdYouth.Row, "ProposedRetail").ToString.Length = 0, "NULL", grdYouth.Item(grdYouth.Row, "ProposedRetail")) & ", "
            grdMain.Item(grdMain.Row, "ProposedRetail") = IIf(grdYouth.Item(grdYouth.Row, "ProposedRetail").ToString.Length = 0, "", grdYouth.Item(grdYouth.Row, "ProposedRetail"))
            Sql &= "ProposedRetail = " & IIf(grdYouth.Item(grdYouth.Row, "ProposedRetail").ToString.Length = 0, "NULL", grdYouth.Item(grdYouth.Row, "ProposedRetail")) & ", "
            grdMain.Item(grdMain.Row, "FinalFob") = IIf(grdYouth.Item(grdYouth.Row, "FinalFob").ToString.Length = 0, "", grdYouth.Item(grdYouth.Row, "FinalFob"))
            Sql &= " FinalFob = " & IIf(grdYouth.Item(grdYouth.Row, "FinalFob").ToString.Length = 0, "NULL", grdYouth.Item(grdYouth.Row, "FinalFob"))
            executeSQL(Sql & " WHERE devNo = " & grdYouth.Item(grdYouth.Row, "Mother"))

            gridCalculations(grdMain, "NewGrid")
        End If
    End Sub

    Private Sub pnlMargins_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlMargins.DoubleClick
        Dim str_text As String = ""
        For i As Integer = 0 To grdMain.Columns.Count - 1
            If grdMain.Columns(i).FilterText <> "" And grdMain.Columns(i).FilterText <> "(All)" Then
                Dim Sql As String = "SELECT SplitHeaders.Description FROM GridLayout INNER JOIN " _
                        & "SplitHeaders ON GridLayout.Split = SplitHeaders.Split " _
                        & "WHERE (GridLayout.ColumnName = '" & grdMain.Columns(i).DataField & "')"
                Dim SelectData As DataSet = getSelectDataSet(Sql)
                If SelectData.Tables(0).Rows.Count > 0 Then
                    str_text &= SelectData.Tables(0).Rows(0).Item("Description") & ": " & grdMain.Columns(i).DataField & ": " & grdMain.Columns(i).FilterText & vbCrLf
                Else
                    str_text &= "Main: " & grdMain.Columns(i).DataField & ": " & grdMain.Columns(i).FilterText & vbCrLf
                End If
            End If
        Next

        writeToLog(sAS_StartupPath & "\error\errorMargins_" & Format(Now(), "yyyy-MM-dd hhmmss") & "_" & UserId & ".txt", str_text)
        calculateMargin()
        writeDevNoToTable()
    End Sub


    ' Public Sub GridRefresh()
        ' 'Dim i As Long
        ' 'grdMain.Visible = False
        ' Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ' 'For i = 0 To grdMain.RowCount - 1
        ' 'grdMain.Delete(0)
        ' 'Next

        ' 'grdMain.ClearFields()
        ' GridData.Clear()
        ' GridAdapter.Dispose()

        ' GridAdapter.SelectCommand = dcNAPA.CreateCommand
        ' GridAdapter.SelectCommand.CommandText = "SELECT * From NewGrid WHERE (ISNULL(IsDeleted,0) = 0) order by DevNo Desc"
        ' cmdBuilder = New SqlCommandBuilder(GridAdapter)
        ' GridAdapter.Fill(GridData)
        ' grdMain.DataSource = GridData
        ' grdMain.DataMember = GridData.Tables(0).ToString
        ' grdMain.Rebind(True)
        ' grdMain.Visible = True
        ' grdMain_RowColChange(Nothing, Nothing)
        ' Me.Cursor = System.Windows.Forms.Cursors.Default
        ' 'calculateMargin()
    ' End Sub 





    Public Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'Dim i As Long
        grdMain.Visible = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'For i = 0 To grdMain.RowCount - 1
        'grdMain.Delete(0)
        'Next

        'grdMain.ClearFields()
        GridData.Clear()
        GridAdapter.Dispose()

        GridAdapter.SelectCommand = dcNAPA.CreateCommand
        GridAdapter.SelectCommand.CommandText = "SELECT * From NewGrid WHERE (ISNULL(IsDeleted,0) = 0) order by DevNo Desc"
        cmdBuilder = New SqlCommandBuilder(GridAdapter)
        GridAdapter.Fill(GridData)
        grdMain.DataSource = GridData
        grdMain.DataMember = GridData.Tables(0).ToString
        grdMain.Rebind(True)
        grdMain.Visible = True
        'grdMain_RowColChange(Nothing, Nothing)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        calculateMargin()
    End Sub

    Private Sub btnCompOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompOK.Click
        Dim Total As Integer
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim str_tmp As String
        Dim sql As String
        Dim MyID As Long
        Dim adapter As New SqlDataAdapter

        If txtComp1.Text <> "" Then
            Total = Total + Val(txtComp1.Text)
        End If
        If txtComp2.Text <> "" Then
            Total = Total + Val(txtComp2.Text)
        End If
        If txtComp3.Text <> "" Then
            Total = Total + Val(txtComp3.Text)
        End If
        If txtComp4.Text <> "" Then
            Total = Total + Val(txtComp4.Text)
        End If
        If txtComp5.Text <> "" Then
            Total = Total + Val(txtComp5.Text)
        End If
        If txtComp6.Text <> "" Then
            Total = Total + Val(txtComp6.Text)
        End If
        If Total <> 100 Then
            msg = "Your Total must be 100%"
            style = MsgBoxStyle.DefaultButton2 Or _
               MsgBoxStyle.Critical Or MsgBoxStyle.Critical
            title = "Attention"
            MsgBox(msg, style, title)
            Exit Sub
        End If
        str_tmp = vbNullString

        If Val(txtComp1.Text) > 0 Then
            str_tmp = str_tmp & txtComp1.Text & "%" & Trim(cmbComp1.Text) & ","
        End If
        If Val(txtComp2.Text) > 0 Then
            str_tmp = str_tmp & txtComp2.Text & "%" & Trim(cmbComp2.Text) & ","
        End If
        If Val(txtComp3.Text) > 0 Then
            str_tmp = str_tmp & txtComp3.Text & "%" & Trim(cmbComp3.Text) & ","
        End If
        If Val(txtComp4.Text) > 0 Then
            str_tmp = str_tmp & txtComp4.Text & "%" & Trim(cmbComp4.Text) & ","
        End If
        If Val(txtComp5.Text) > 0 Then
            str_tmp = str_tmp & txtComp5.Text & "%" & Trim(cmbComp5.Text) & ","
        End If
        If Val(txtComp6.Text) > 0 Then
            str_tmp = str_tmp & txtComp6.Text & "%" & Trim(cmbComp6.Text) & ","
        End If
        If Microsoft.VisualBasic.Right(str_tmp, 1) = "," Then str_tmp = Microsoft.VisualBasic.Left(str_tmp, Len(str_tmp) - 1)
        MyID = grdMain.Item(grdMain.Row, 0)
        grdMain.Item(grdMain.Row, grdMain.Col) = str_tmp
        sql = "Update NewGrid set GarmentComposition = '" & str_tmp & "'" _
        & "Where DevNo = " & MyID
        Try
            If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
            adapter.InsertCommand = dcNAPA.CreateCommand
            adapter.InsertCommand.CommandText = sql
            adapter.InsertCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        pnlComposition.Visible = False
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Dim sheet As XLSheet = C1XLBook1.Sheets(0)
        Dim style1 As New XLStyle(C1XLBook1)

        style1.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Dim str_columns_NewGrid As String = ""
        Dim str_columns_SalesYouth As String = ""

        If ComboBox1.SelectedValue Is Nothing Then
            MsgBox("Please select an Excel Template first...")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        'Get columns and columnHeaders
        Dim sql As String = "SELECT TOP (100) PERCENT dbo.CustomExcel.ColumnDescription, dbo.CustomExcel.ColumnName, ISNULL(dbo.GridPerUser_new.split, 99) as Split, " & _
                                    "ISNULL(dbo.GridPerUser_new.position, 999) as Position, dbo.CustomExcel.showValue, dbo.CustomExcel.showDescription, " & _
                                    "dbo.CustomExcel.seperator , i.data_type as datatype " & _
                            "FROM dbo.CustomExcel " & _
                            "LEFT OUTER JOIN dbo.GridPerUser_new ON dbo.CustomExcel.ColumnName = dbo.GridPerUser_new.columnID " & _
                            "LEFT OUTER JOIN information_schema.columns i ON dbo.CustomExcel.ColumnName= i.Column_Name " & _
                            "WHERE (dbo.CustomExcel.ToExcel = 1) AND (dbo.CustomExcel.templateID = " & ComboBox1.SelectedValue & ")  " & _
                                    "AND (ISNULL(dbo.GridPerUser_new.gridID, 'grdmain') = 'grdmain') " & _
                                    "AND i.TABLE_NAME='newgrid' " & _
                                    "AND (ISNULL(dbo.GridPerUser_new.userID, '" & UserId & "') = '" & UserId & "') and dbo.CustomExcel.ColumnName<>'DevNo' ORDER BY split, position"
        Dim SearchData As DataSet = getSelectDataSet(sql)
        'Get the columns of the salesYouth table
        Dim SearchDataYouth As DataSet = getSelectDataSet("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns WHERE UPPER(TABLE_NAME) = 'SalesYouth'")
        'Get the settings of the selected template
        Dim templateSettings As DataSet = getSelectDataSet("SELECT * FROM param_excelTemplates WHERE ID = " & ComboBox1.SelectedValue)
        Dim b_addYouth As Boolean = IIf(templateSettings.Tables(0).Rows(0).Item("ShowYouth").ToString = "True", True, False)
        Dim b_addNP As Boolean = IIf(templateSettings.Tables(0).Rows(0).Item("ShowNP").ToString = "True", True, False)
        'Write DevNo and SizeMask headers
        sheet.Item(0, 0).Value = "DevNo"
        sheet(0, 0).Style = style1
        Dim columnCount As Integer = 1
        If b_addYouth = True Then
            sheet.Item(0, columnCount).Value = "DevNoYouth"
            sheet(0, columnCount).Style = style1
            columnCount += 1
        End If
        sheet.Item(0, columnCount).Value = "SizeMask"
        sheet(0, columnCount).Style = style1
        columnCount += 1
        Dim a_columns_show_and_seperator(SearchData.Tables(0).Rows.Count + 3) As String
        Dim i_columns As Integer = 0
        Do While i_columns < columnCount
            a_columns_show_and_seperator(i_columns) = "True~False~"
            i_columns += 1
        Loop
        For i As Integer = 0 To SearchData.Tables(0).Rows.Count - 1
            If Not SearchData.Tables(0).Rows(i).Item("columnName").ToString = "DevNo" And Not SearchData.Tables(0).Rows(i).Item("columnName").ToString = "SizeMask" And Not SearchData.Tables(0).Rows(i).Item("columnName").ToString.ToUpper.StartsWith("FREEZE") Then
                If str_columns_NewGrid.ToString.Length > 0 Then str_columns_NewGrid &= ","
                If str_columns_SalesYouth.ToString.Length > 0 Then str_columns_SalesYouth &= ","
                'Write the columnHeaders to excel
                sheet(0, columnCount).Value = IIf(SearchData.Tables(0).Rows(i).Item("columnDescription").ToString = "", SearchData.Tables(0).Rows(i).Item("columnName").ToString, SearchData.Tables(0).Rows(i).Item("columnDescription").ToString)
                sheet(0, columnCount).Style = style1
                'Debug.WriteLine(sheet(0, columnCount).Value)
                columnCount += 1
                'Create an sql statement with the columns of NewGrid
                If (SearchData.Tables(0).Rows(i).Item("datatype") = "text") Then
                    str_columns_NewGrid &= "CONVERT(VARCHAR(MAX), dbo.NewGrid." & SearchData.Tables(0).Rows(i).Item("columnName") & ")  as " & SearchData.Tables(0).Rows(i).Item("columnName") & " "
                Else
                    str_columns_NewGrid &= "dbo.NewGrid." & SearchData.Tables(0).Rows(i).Item("columnName") & " "
                End If
                'Create an sql statement with the columns of NewGrid, combined with the columns of SalesYouth
                If SearchDataYouth.Tables(0).Select("COLUMN_NAME = '" & SearchData.Tables(0).Rows(i).Item("columnName") & "'").Length > 0 And b_addYouth = True Then
                    str_columns_SalesYouth &= "dbo.salesYouth." & SearchData.Tables(0).Rows(i).Item("columnName") & " "
                Else

                    If (SearchData.Tables(0).Rows(i).Item("datatype") = "text") Then
                        str_columns_SalesYouth &= "CONVERT(VARCHAR(MAX), dbo.NewGrid." & SearchData.Tables(0).Rows(i).Item("columnName") & ")  as " & SearchData.Tables(0).Rows(i).Item("columnName") & " "
                    Else
                        str_columns_SalesYouth &= "dbo.NewGrid." & SearchData.Tables(0).Rows(i).Item("columnName") & " "
                    End If
                End If
                'Create an array which specifies wheither value or/and description should be shown and with which seperator
                a_columns_show_and_seperator(i_columns) = Replace(SearchData.Tables(0).Rows(i).Item("showValue").ToString, "~", "-") & "~" & _
                                                        Replace(SearchData.Tables(0).Rows(i).Item("showDescription").ToString, "~", "-") & "~" & _
                                                        Replace(SearchData.Tables(0).Rows(i).Item("seperator").ToString, "~", "-")
            Else
                a_columns_show_and_seperator(i_columns) = "True~False~"
            End If
            i_columns += 1
        Next

        sql = "SELECT * FROM ( " & _
                "SELECT	CONVERT(varchar, DevNo) AS DevNo, " & IIf(b_addYouth, "'' as DevNoYouth, ", "") & "'Main' AS Sizemask " & IIf(str_columns_NewGrid.Length > 0, ", " & str_columns_NewGrid, "") & "FROM dbo.NewGrid WHERE IsDeleted = 0 " & IIf(b_addNP, "", " AND StyleStatus = 0 ")
        If b_addYouth = True Then
            sql &= "UNION SELECT CONVERT(varchar, dbo.salesYouth.Mother) as Devno, SUBSTRING('         ', 1, LEN(dbo.salesYouth.DevNo)) + CONVERT(varchar, dbo.salesYouth.DevNo)  as DevNoYouth, " & _
                    "dbo.salesYouth.Sizemask " & IIf(str_columns_SalesYouth.Length > 0, ", " & str_columns_SalesYouth, "") & "FROM dbo.salesYouth " & _
                    "INNER JOIN dbo.NewGrid ON dbo.salesYouth.Mother = dbo.NewGrid.DevNo " & _
                    "WHERE dbo.salesYouth.Mother IN (SELECT DevNo From NewGrid WHERE (IsDeleted = 0) AND (StyleStatus = 0))"
        End If
        sql &= ") as derived_tbl WHERE DevNo in (SELECT CONVERT(varchar, DevNo) as DevNo FROM dbo.tmp_NewGridAfterFilter WHERE userNaam='" & UserId & "') ORDER BY DevNo " & IIf(b_addYouth, ", DevNoYouth", "")
        SearchData = getSelectDataSet(sql)
        For i As Integer = 0 To SearchData.Tables(0).Rows.Count - 1
            For j As Integer = 0 To SearchData.Tables(0).Columns.Count - 1
                Dim str_cellValue As String = ""

                If a_columns_show_and_seperator(j).Split("~")(0).ToString = "True" Then
                    str_cellValue &= SearchData.Tables(0).Rows(i).Item(j)
                End If

                If a_columns_show_and_seperator(j).Split("~")(1).ToString = "True" Or str_cellValue.Length = 0 Then
                    Dim gridLayoutData As DataSet = getSelectDataSet("SELECT * FROM dbo.GridLayout WHERE columnName ='" & SearchData.Tables(0).Columns(j).ColumnName & "' AND gridName='grdmain'")
                    If gridLayoutData.Tables(0).Rows.Count > 0 Then
                        Dim str_sql As String = gridLayoutData.Tables(0).Rows(0).Item("DropDownSQL").ToString
                        If str_sql.Length > 0 Then
                            Dim str_display As String = gridLayoutData.Tables(0).Rows(0).Item("DropDownDisplayMember").ToString
                            Dim str_value As String = gridLayoutData.Tables(0).Rows(0).Item("DropDownValueMember").ToString
                            Dim cellData As DataSet = getSelectDataSet("SELECT " & str_display & " FROM (" & str_sql & ") as Sub WHERE " & str_value & "='" & SearchData.Tables(0).Rows(i).Item(j) & "'")
                            If cellData.Tables(0).Rows.Count > 0 Then
                                If a_columns_show_and_seperator(j).Split("~")(0).ToString = "True" And str_cellValue.Length > 0 Then str_cellValue &= a_columns_show_and_seperator(j).Split("~")(2).ToString
                                str_cellValue &= cellData.Tables(0).Rows(0).Item(0).ToString
                            Else
                                If a_columns_show_and_seperator(j).Split("~")(0).ToString = "True" And str_cellValue.Length > 0 Then str_cellValue &= a_columns_show_and_seperator(j).Split("~")(2).ToString
                                str_cellValue &= SearchData.Tables(0).Rows(i).Item(j)
                            End If
                        Else
                            If a_columns_show_and_seperator(j).Split("~")(0).ToString = "True" And str_cellValue.Length > 0 Then str_cellValue &= a_columns_show_and_seperator(j).Split("~")(2).ToString
                            str_cellValue &= SearchData.Tables(0).Rows(i).Item(j)
                            If SearchData.Tables(0).Columns(j).ColumnName.StartsWith("Notes") Then
                                str_cellValue = str_cellValue.Replace(vbCr, "") '.ToString.Trim
                                str_cellValue = str_cellValue.Replace(vbCrLf, "")
                                str_cellValue = str_cellValue.Replace(System.Environment.NewLine, "")
                                str_cellValue = System.Text.RegularExpressions.Regex.Replace(str_cellValue, "\t", "")
                                str_cellValue = System.Text.RegularExpressions.Regex.Replace(str_cellValue, "\n", "")
                                str_cellValue = System.Text.RegularExpressions.Regex.Replace(str_cellValue, "\r", "")
                            End If
                        End If
                        Dim str_format As String = gridLayoutData.Tables(0).Rows(0).Item("formatExport").ToString
                        If str_format.Length > 0 Then
                            Dim styleMargin As New XLStyle(C1XLBook1)
                            styleMargin.Format = str_format
                            sheet(i + 1, j).Style = styleMargin
                        End If
                    Else
                        If a_columns_show_and_seperator(j).Split("~")(0).ToString = "True" And str_cellValue.Length > 0 Then str_cellValue &= a_columns_show_and_seperator(j).Split("~")(2).ToString
                        str_cellValue &= SearchData.Tables(0).Rows(i).Item(j)
                    End If
                End If
                sheet(i + 1, j).Value = str_cellValue
            Next
        Next

        Try
            Dim MyAttr As FileAttribute = GetAttr(sAS_NAPARootFolder & "NAPAEXCEL")
        Catch
            MkDir(sAS_NAPARootFolder & "NAPAEXCEL")
        End Try

        Dim fileName As String = sAS_NAPARootFolder & "NAPAEXCEL\NAPALineList_" & UserId & "_" & Format(Now, "yyyyMMdd_hhmmss") & ".xls"
        C1XLBook1.Save(fileName)
        C1XLBook1.Clear()
        Me.Cursor = Cursors.Default
        System.Diagnostics.Process.Start(fileName)
    End Sub

    Private Sub btnCompCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompCancel.Click
        pnlComposition.Visible = False
    End Sub

    Private Sub btnCompClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompClear.Click
        Dim sql As String
        Dim MyID As Long
        Dim adapter As New SqlDataAdapter

        MyID = grdMain.Item(grdMain.Row, 0)
        grdMain.Item(grdMain.Row, grdMain.Col) = ""
        sql = "Update NewGrid set GarmentComposition = NULL WHERE DevNo = " & MyID
        Try
            If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
            adapter.InsertCommand = dcNAPA.CreateCommand
            adapter.InsertCommand.CommandText = sql
            adapter.InsertCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        pnlComposition.Visible = False
    End Sub

    Private Sub btnClearFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters.Click
        Dim i As Integer
        For i = 0 To grdMain.Columns.Count - 1
            If Not grdMain.Columns(i).FilterText.Length = 0 And Not grdMain.Columns(i).FilterText = "(All)" Then
                grdMain.Columns(i).FilterText = ""
            End If
        Next
    End Sub


    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim adapter As New SqlDataAdapter
        Dim sql As String
        If ListBox1.Text <> "" Then
            grdMain.Item(grdMain.Row, grdMain.Col) = "N0C" & ListBox1.Text
            sql = "Update tbl_UsedFabricCodes set InUse = 1, UsedFirstTime = CONVERT(DATETIME,'" & Now & "', 102) where FabricCode = '" & ListBox1.Text & "'"
            Try
                If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                adapter.InsertCommand = dcNAPA.CreateCommand
                adapter.InsertCommand.CommandText = sql
                adapter.InsertCommand.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        ListBox1.Visible = False
    End Sub

    Private Sub ListBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.LostFocus
        ListBox1.Visible = False
    End Sub


    Private Sub txtComp1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComp1.TextChanged, txtComp2.TextChanged, txtComp3.TextChanged, txtComp4.TextChanged, txtComp5.TextChanged, txtComp6.TextChanged
        lblTotal.Text = "0"
        lblTotal.Text = Val(lblTotal.Text) + Val(txtComp1.Text) + Val(txtComp2.Text) + Val(txtComp3.Text) + Val(txtComp4.Text) + Val(txtComp5.Text) + Val(txtComp6.Text)
    End Sub


    Private Sub ComboBox1_MouseDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ComboBox1.MouseDown
        ComboBox1.ContextMenuStrip = Nothing
        If e.Button = Windows.Forms.MouseButtons.Right And ComboBox1.Items.Count > 0 Then
            Dim searchData As DataSet = getSelectDataSet("SELECT TemplateName from param_excelTemplates Where ID='" & ComboBox1.SelectedValue & "' AND owner = '" & UserId & "'")
            If searchData.Tables(0).Rows.Count > 0 Or userGroup = "SUPER" Then
                ComboBox1.ContextMenuStrip = New ContextMenuStrip
                ComboBox1.ContextMenuStrip.Items.Add("Edit Template", Nothing, New EventHandler(AddressOf mnuTemplate_mnuClick)).Tag = ComboBox1.SelectedValue
                ComboBox1.ContextMenuStrip.Items.Add("Delete Template", Nothing, New EventHandler(AddressOf mnuTemplate_mnuClick)).Tag = ComboBox1.SelectedValue
            End If
        End If
    End Sub

    Private Sub ComboBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ComboBox2.MouseDown
        ComboBox2.ContextMenuStrip = Nothing
        If e.Button = Windows.Forms.MouseButtons.Right And ComboBox2.Items.Count > 0 Then
            ComboBox2.ContextMenuStrip = New ContextMenuStrip
            ComboBox2.ContextMenuStrip.Items.Add("Remove filter", Nothing, New EventHandler(AddressOf mnuFilter_mnuClick)).Tag = ComboBox2.SelectedItem
        End If
    End Sub

    Private Sub DeleteDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteDateToolStripMenuItem.Click
        Dim c As Integer
        Dim MyId As Long
        Dim adapter As New SqlDataAdapter

        c = Int(Val(Mid(grdMain.FocusedSplit.Name, 9, 1)))
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdMain.Splits(c).DisplayColumns(Me.grdMain.Col).DataColumn
        MyId = grdMain.Item(grdMain.Row, 0)
        grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField) = ""
        executeSQL("Update Newgrid set " & grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField & " = NULL Where DevNo = " & MyId)
    End Sub

    Private Sub CopyDownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopyDownToolStripMenuItem.Click
        Dim i As Integer
        Dim c As Integer
        Dim newValueCol, newValueDB As String
        Dim newTextCol As String
        Dim Sql As String
        Dim adapter As New SqlDataAdapter

        c = Int(Val(Mid(grdMain.FocusedSplit.Name, 9, 1)))
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdMain.Splits(c).DisplayColumns(Me.grdMain.Col).DataColumn

        newValueCol = IIf(col.CellValue(grdMain.Row).ToString.Length = 0, "", col.CellValue(grdMain.Row))
        newTextCol = IIf(col.CellText(grdMain.Row).ToString.Length = 0, "", col.CellText(grdMain.Row))
        Sql = "Update Newgrid set " & grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField & " = "
        If newValueCol = "To Be Defined" Then ' Or newValueCol = "000 NEUTRO" removed to accomodate 000 NEUTRO Color
            newValueCol = ""
            newValueDB = "NULL"
        Else
            Select Case grdMain.Splits(c).DisplayColumns(col).DataColumn.DataType.ToString
                Case "System.Boolean"
                    newValueDB = IIf(grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField) = True, "-1", "0")
                Case "System.Int32", "System.Decimal"
                    newValueDB = IIf(IsDBNull(grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField)), " NULL", grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField))
                Case "System.String"
                    newValueDB = IIf(IsDBNull(grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField)), " NULL", " '" & grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField) & "'")
                Case "System.DateTime"
                    newValueDB = IIf(IsDBNull(grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField)), " NULL", " CONVERT(DATETIME, '" & Format(CDate(grdMain.Item(grdMain.Row, grdMain.Splits(c).DisplayColumns(col).DataColumn.DataField)), "yyyy-MM-dd") & "', 102)")
                Case Else
                    MsgBox("Something is Wrong. Your data can be lost. Please Contact Patrick Waeytens", MsgBoxStyle.Critical)
                    newValueDB = "NULL"
            End Select
        End If

        For i = grdMain.Row + 1 To grdMain.RowCount - 1
            grdMain.Row = i
            oldValue = col.Value.ToString
            col.Text = newValueCol

            If Not grdMain.Item(grdMain.Row, "StyleStatus").ToString = "1" Then
                If col.DataField = "SmsProducer" Or col.DataField = "VendorBulk" Then
                    SplitAndUpdate_Producers(col.DataField, newTextCol, newValueCol, grdMain.Item(grdMain.Row, "DevNo"), grdMain.Row)
                Else
                    executeSQL(Sql & newValueDB & " WHERE DevNo = " & grdMain.Item(grdMain.Row, 0).ToString)
                End If
                executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & grdMain.Item(grdMain.Row, "DevNo") & "', '" & col.DataField & "', '" & oldValue & "', " & newValueCol & ", '" & UserId & "')")
            End If
        Next
    End Sub

    Private Sub ContextMenuStrip1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStrip1.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim HistoryData As DataSet = getSelectDataSet("SELECT * From SwaCheck WHERE DevNo=" & grdMain.Item(grdMain.Row, "DevNo") & " ORDER BY changedate DESC")
        frmHistory.grdHistory.DataSource = Nothing
        frmHistory.grdHistory.DataSource = HistoryData
        frmHistory.grdHistory.DataMember = HistoryData.Tables(0).ToString
        frmHistory.grdHistory.Rebind(True)
        frmHistory.grdHistory.Columns("changedate").NumberFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern.ToString
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmHistory.Show()
    End Sub

    Private Sub ContextMenuStrip1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStrip1.LostFocus
        ContextMenuStrip.Visible = False
    End Sub


    Private Sub picStyle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picStyle.Click
        If picStyle.Top = picT And picStyle.Left = picL Then
            picH = picStyle.Height
            picW = picStyle.Width
            picStyle.BringToFront()

            If picH > picW Then
                picStyle.Height = 700
                picStyle.Width = picW * picStyle.Height / picH
            Else
                picStyle.Width = 700
                picStyle.Height = picH * picStyle.Width / picW
            End If
            picStyle.Top = picStyle.Top - picStyle.Height + picH
            picStyle.Left = picStyle.Left - picStyle.Width + picW
            picStyle.Refresh()
        End If
    End Sub

    Private Sub picStyle_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picStyle.MouseLeave
        If picStyle.Top < picT Then
            picStyle.Height = picH
            picStyle.Width = picW
            picStyle.Top = picT
            picStyle.Left = picL
            picStyle.Refresh()
        End If
    End Sub

    Public Sub updateColorRow(ByVal DevNo As Integer, ByVal useGrid As Boolean, Optional ByVal myRow As Integer = 0)
        Dim k As Integer
        Dim adapter As New SqlDataAdapter
        Dim sql As String = "UPDATE NewGrid SET "
        Dim myCounter As Integer = 0
        Dim mycounter_active As Integer = 0
        Dim colorCell As New clsColorCell()
        Dim str_columnName As String

        Dim rowData As New DataSet
        If Not useGrid Then rowData = getSelectDataSet("SELECT * FROM newGrid WHERE devNo=" & DevNo)
        Dim season As String = ""

        If Not useGrid Then
            season = rowData.Tables(0).Rows(0).Item("Season").ToString
        Else
            season = grdMain.Item(myRow, "Season").ToString
        End If

        Dim colorData As DataSet = getSelectDataSet("SELECT QUESTPDMNAPA.dbo.INTCOLOR.COLOURART, QUESTPDMNAPA.dbo.INTCOLOR.COLOURART + ' ' + QUESTPDMNAPA.dbo.INTCOLOR.COLOURTEXT AS COLOURTEXT FROM QUESTPDMNAPA.dbo.INTCOLOR INNER JOIN dbo.Param_Seasons_Napa ON RIGHT(QUESTPDMNAPA.dbo.INTCOLOR.COLOURNO, " & season.Length & ") = dbo.Param_Seasons_Napa.Season WHERE dbo.Param_Seasons_Napa.Season = '" & season & "'")
        Dim colorText As String

        For k = 1 To i_ColorCount
            str_columnName = IIf(k <= i_smsColorCount, "smsColor" & k, "prodColor" & IIf(k < 10, "0" & k, k))
            If useGrid Then
                If colorData.Tables(0).Select("COLOURART='" & grdMain.Item(myRow, str_columnName).ToString & "'").Length > 0 Then
                    colorText = colorData.Tables(0).Select("COLOURART='" & grdMain.Item(myRow, str_columnName).ToString & "'")(0).Item("COLOURTEXT")
                Else
                    colorText = ""
                End If
                colorCell.setValues(colorText, grdMain.Item(myRow, str_columnName).ToString, IIf(grdMain.Item(myRow, str_columnName & "_dropped").ToString = "True", True, False))
            Else
                If colorData.Tables(0).Select("COLOURART='" & rowData.Tables(0).Rows(0).Item(str_columnName).ToString & "'").Length > 0 Then
                    colorText = colorData.Tables(0).Select("COLOURART='" & rowData.Tables(0).Rows(0).Item(str_columnName).ToString & "'")(0).Item("COLOURTEXT")
                Else
                    colorText = ""
                End If
                colorCell.setValues(colorText, rowData.Tables(0).Rows(0).Item(str_columnName).ToString, rowData.Tables(0).Rows(0).Item(str_columnName & "_dropped").ToString)
            End If
            'If colorCell.cellText.Length > 0 And Not colorCell.cellText = "TDB" And Not colorCell.cellText = "000" And Not colorCell.cellText = "To Be Defined" And Not colorCell.cellText = "000 NEUTRO" Then ' commented to allow 000 NEUTRO, color code change as below
            If colorCell.cellText.Length > 0 And Not colorCell.cellText = "TDB" And Not colorCell.cellText = "To Be Defined" Then
                myCounter += 1
                If Not colorCell.colorDropped Then mycounter_active += 1
                If useGrid Then
                    grdMain.Item(myRow, "prodColor" & IIf(k < 10, "0" & k, k)) = colorCell.cellValue 'condition needed here
                    grdMain.Item(myRow, "Color" & myCounter) = colorCell.cellText
                    If Not colorCell.colorDropped Then grdMain.Item(myRow, "color_undropped" & mycounter_active) = colorCell.cellText
                End If
                sql &= "prodColor" & IIf(k < 10, "0" & k, k) & " = '" & colorCell.cellValue & "', " 'condition needed here
                sql &= "Color" & myCounter & " = '" & colorCell.cellText & "', "
                If Not colorCell.colorDropped Then sql &= "color_undropped" & mycounter_active & " = '" & colorCell.cellText & "', "
            Else
                If useGrid Then grdMain.Item(myRow, "prodColor" & IIf(k < 10, "0" & k, k)) = ""
                sql &= "prodColor" & IIf(k < 10, "0" & k, k) & " = NULL, "
            End If
        Next
        If useGrid Then grdMain.Item(myRow, "ActualLotNumber") = IIf(myCounter = 0, "", myCounter)
        If useGrid Then grdMain.Item(myRow, "ActualLotNumber_active") = IIf(mycounter_active = 0, "", mycounter_active)
        For k = myCounter + 1 To i_ColorCount
            sql &= "Color" & k & "=NULL, "
            If useGrid Then grdMain.Item(myRow, "Color" & k) = ""
        Next
        For k = mycounter_active + 1 To i_ColorCount
            sql &= "color_undropped" & k & "=NULL, "
            If useGrid Then grdMain.Item(myRow, "color_undropped" & k) = ""
        Next
        executeSQL(sql & "ActualLotNumber=" & IIf(myCounter > 0, myCounter, "NULL") &
                        ", ActualLotNumber_active=" & IIf(mycounter_active > 0, mycounter_active, "NULL") &
                        " WHERE DevNo=" & DevNo)
        calculateMargin()
    End Sub

    Public Sub gridCalculations(ByVal grdName As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal tableName As String)
        Dim SelectData As New DataSet
        Dim MyId As Long = grdName.Item(grdName.Row, "DevNo")
        Dim i_counter As Integer
        Dim str_Value, str_ValueSQL, str_Value1 As String

        'Debug.WriteLine("gridCalculations for " & grdName.Name)
        str_Value = ""
        str_ValueSQL = "NULL"
        If Not IsDBNull(grdName.Item(grdName.Row, "Season")) And Not IsDBNull(grdName.Item(grdName.Row, "Currency")) And Not IsDBNull(grdName.Item(grdName.Row, "FinalFob")) Then
            SelectData = getSelectDataSet("SELECT Season, Currency, Rate FROM NAPA.dbo.tbl_sourcing_Rates WHERE (Season = '" & grdName.Item(grdName.Row, "Season") & "') AND (Currency = '" & grdName.Item(grdName.Row, "Currency") & "' AND Coalition='DP')")
            If SelectData.Tables(0).Rows.Count > 0 Then
                str_Value = Math.Round(grdName.Item(grdName.Row, "FinalFob") / SelectData.Tables(0).Rows(0).Item("Rate"), 2)
                str_ValueSQL = str_Value
            End If
        End If
        grdName.Item(grdName.Row, "FobInEuro") = str_Value
        executeSQL("Update " & tableName & " set FobInEuro = " & str_ValueSQL & " where DevNo = " & MyId)

        str_Value = "0.0"
        str_ValueSQL = "0.0"
        If Not IsDBNull(grdName.Item(grdName.Row, "SourceLocation")) And Not IsDBNull(grdName.Item(grdName.Row, "LavMat")) And Not IsDBNull(grdName.Item(grdName.Row, "FobInEuro")) And Not IsDBNull(grdName.Item(grdName.Row, "Season")) Then
            If grdName.Item(grdName.Row, "SourceLocation") = "AS" Then
                SelectData = getSelectDataSet("SELECT SourcingAsia FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & grdName.Item(grdName.Row, "Season") & "')")
                If SelectData.Tables(0).Rows.Count > 0 Then
                    str_Value = Math.Round((grdName.Item(grdName.Row, "FobInEuro") + grdName.Item(grdName.Row, "LavMat")) * SelectData.Tables(0).Rows(0).Item("SourcingAsia"), 2)
                    str_ValueSQL = str_Value
                End If
            End If
        End If
        grdName.Item(grdName.Row, "SourcingAsia") = str_Value
        executeSQL("Update " & tableName & " set SourcingAsia = " & str_ValueSQL & " where DevNo = " & MyId)

        Dim str_SelectFields() As String = {"Freight", "AirFreight"}
        For i_counter = 0 To str_SelectFields.Length - 1
            str_Value = "0.0"
            str_ValueSQL = "0.0"
            If Not IsDBNull(grdName.Item(grdName.Row, "Major")) And Not IsDBNull(grdName.Item(grdName.Row, "LavMat")) And Not IsDBNull(grdName.Item(grdName.Row, "FobInEuro")) And Not IsDBNull(grdName.Item(grdName.Row, "Season")) Then
                'If grdName.Item(grdName.Row, "Major") <> "BS" Then '--Commented as per mail from Alfonso
                SelectData = getSelectDataSet("SELECT " & str_SelectFields(i_counter) & " FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & grdName.Item(grdName.Row, "Season") & "')")
                If SelectData.Tables(0).Rows.Count > 0 Then
                    str_Value = Math.Round((grdName.Item(grdName.Row, "FobInEuro") + grdName.Item(grdName.Row, "LavMat")) * SelectData.Tables(0).Rows(0).Item(str_SelectFields(i_counter)), 2)
                    str_ValueSQL = str_Value
                End If
                'End If
            End If
            grdName.Item(grdName.Row, str_SelectFields(i_counter)) = str_Value
            executeSQL("Update " & tableName & " set " & str_SelectFields(i_counter) & " = " & str_ValueSQL & " where DevNo = " & MyId)
        Next

        str_Value = "0.0"
        str_ValueSQL = "0.0"
        If Not IsDBNull(grdName.Item(grdName.Row, "Duty")) And Not IsDBNull(grdName.Item(grdName.Row, "LavMat")) And Not IsDBNull(grdName.Item(grdName.Row, "FobInEuro")) And Not IsDBNull(grdName.Item(grdName.Row, "Freight")) And Not IsDBNull(grdName.Item(grdName.Row, "AirFreight")) Then
            str_Value = Math.Round((grdName.Item(grdName.Row, "LavMat") + grdName.Item(grdName.Row, "FobInEuro") + grdName.Item(grdName.Row, "Freight") + grdName.Item(grdName.Row, "AirFreight")) * grdName.Item(grdName.Row, "Duty") / 100, 2)
            str_ValueSQL = str_Value
        End If
        grdName.Item(grdName.Row, "Duty2") = str_Value
        executeSQL("Update " & tableName & " set Duty2 = " & str_ValueSQL & " where DevNo = " & MyId)

        '{DatabaseColumnName, GridColumnName}
        Dim str_SelectFields2(,) As String = {{"Operations", "Operations"}, {"Planning", "Planning"}, {"SourcingEurope", "SourcingEurope"}, {"QualityControl", "QualityControl"}, {"Reserve", "Reserve"}, {"TurinBuilding", "TurinBuilding"}, {"TrimWarehouse", "TurinWarehouse"}, {"Turin_RMSourcing", "Turin_RMSourcing"}}
        For i_counter = 0 To (str_SelectFields2.Length / 2) - 1
            str_Value = "0.0"
            str_ValueSQL = "0.0"
            If Not IsDBNull(grdName.Item(grdName.Row, "LavMat")) And Not IsDBNull(grdName.Item(grdName.Row, "FobInEuro")) And Not IsDBNull(grdName.Item(grdName.Row, "SourcingAsia")) And Not IsDBNull(grdName.Item(grdName.Row, "Season")) And Not IsDBNull(grdName.Item(grdName.Row, "Freight")) And Not IsDBNull(grdName.Item(grdName.Row, "AirFreight")) And Not IsDBNull(grdName.Item(grdName.Row, "Duty2")) Then
                SelectData = getSelectDataSet("SELECT " & str_SelectFields2(i_counter, 0) & " FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & grdName.Item(grdName.Row, "Season") & "')")
                If SelectData.Tables(0).Rows.Count > 0 Then
                    'grdName.Item(grdName.Row, str_SelectFields2(i_counter, 1)) = Math.Round((grdName.Item(grdName.Row, "LavMat") + grdName.Item(grdName.Row, "FobInEuro") + grdName.Item(grdName.Row, "SourcingAsia") + grdName.Item(grdName.Row, "Freight") + grdName.Item(grdName.Row, "AirFreight") + grdName.Item(grdName.Row, "Duty2")) * SelectData.Tables(0).Rows(0).Item(str_SelectFields2(i_counter, 0)), 2)
                    grdName.Item(grdName.Row, str_SelectFields2(i_counter, 1)) = Math.Round(grdName.Item(grdName.Row, "FobInEuro") * SelectData.Tables(0).Rows(0).Item(str_SelectFields2(i_counter, 0)), 2)
                    str_ValueSQL = grdName.Item(grdName.Row, str_SelectFields2(i_counter, 1))
                End If
            End If
            executeSQL("Update " & tableName & " set " & str_SelectFields2(i_counter, 1) & " = " & str_ValueSQL & " where DevNo = " & MyId)
        Next

        str_Value = ""
        str_ValueSQL = "NULL"
        If Not IsDBNull(grdName.Item(grdName.Row, "SourcingAsia")) And Not IsDBNull(grdName.Item(grdName.Row, "Freight")) And Not IsDBNull(grdName.Item(grdName.Row, "AirFreight")) And Not IsDBNull(grdName.Item(grdName.Row, "NotUsed")) And Not IsDBNull(grdName.Item(grdName.Row, "Duty2")) And Not IsDBNull(grdName.Item(grdName.Row, "SourcingUS")) And Not IsDBNull(grdName.Item(grdName.Row, "Operations")) And Not IsDBNull(grdName.Item(grdName.Row, "Planning")) And Not IsDBNull(grdName.Item(grdName.Row, "SourcingEurope")) And Not IsDBNull(grdName.Item(grdName.Row, "QualityControl")) And Not IsDBNull(grdName.Item(grdName.Row, "NotUsed2")) And Not IsDBNull(grdName.Item(grdName.Row, "Reserve")) And Not IsDBNull(grdName.Item(grdName.Row, "TurinBuilding")) And Not IsDBNull(grdName.Item(grdName.Row, "TurinWarehouse")) And Not IsDBNull(grdName.Item(grdName.Row, "NotUsed3")) And Not IsDBNull(grdName.Item(grdName.Row, "Turin_RMSourcing")) And Not IsDBNull(grdName.Item(grdName.Row, "NotUsed4")) And Not IsDBNull(grdName.Item(grdName.Row, "FobInEuro")) And Not IsDBNull(grdName.Item(grdName.Row, "LavMat")) Then
            str_Value = grdName.Item(grdName.Row, "SourcingAsia") + grdName.Item(grdName.Row, "Freight") + grdName.Item(grdName.Row, "AirFreight") + grdName.Item(grdName.Row, "NotUsed") + grdName.Item(grdName.Row, "Duty2") + grdName.Item(grdName.Row, "SourcingUS") + grdName.Item(grdName.Row, "Operations") + grdName.Item(grdName.Row, "Planning") + grdName.Item(grdName.Row, "SourcingEurope") + grdName.Item(grdName.Row, "QualityControl") + grdName.Item(grdName.Row, "NotUsed2") + grdName.Item(grdName.Row, "Reserve") + grdName.Item(grdName.Row, "TurinBuilding") + grdName.Item(grdName.Row, "TurinWarehouse") + grdName.Item(grdName.Row, "NotUsed3") + grdName.Item(grdName.Row, "Turin_RMSourcing") + grdName.Item(grdName.Row, "NotUsed4") + grdName.Item(grdName.Row, "FobInEuro") + grdName.Item(grdName.Row, "LavMat")
            str_ValueSQL = str_Value
        End If
        grdName.Item(grdName.Row, "StandardCost") = str_Value
        executeSQL("Update " & tableName & " set StandardCost = " & str_ValueSQL & " where DevNo = " & MyId)

        str_Value = ""
        str_ValueSQL = "NULL"
        If Not IsDBNull(grdName.Item(grdName.Row, "ProposedRetail")) And Not IsDBNull(grdName.Item(grdName.Row, "MarkUp")) Then
            If grdName.Item(grdName.Row, "MarkUp") <> 0 Then
                ' str_Value = Math.Floor(grdName.Item(grdName.Row, "ProposedRetail") / grdName.Item(grdName.Row, "MarkUp"))
                str_Value = grdName.Item(grdName.Row, "ProposedRetail") / grdName.Item(grdName.Row, "MarkUp")
                str_Value = Decimal.Round(str_Value, 1, MidpointRounding.AwayFromZero)
                str_Value1 = Decimal.Round(str_Value, 2, MidpointRounding.AwayFromZero)
                'MsgBox(str_Value1)
                str_ValueSQL = str_Value1
            End If
        End If
        grdName.Item(grdName.Row, "WholeSale") = str_Value
        executeSQL("Update " & tableName & " set WholeSale = " & str_ValueSQL & " where DevNo = " & MyId)

        str_Value = ""
        str_ValueSQL = "NULL"
        If Not IsDBNull(grdName.Item(grdName.Row, "WholeSale")) And Not IsDBNull(grdName.Item(grdName.Row, "StandardCost")) Then
            If Not grdName.Item(grdName.Row, "WholeSale").ToString = "0" Then
                str_Value = (grdName.Item(grdName.Row, "WholeSale") - grdName.Item(grdName.Row, "StandardCost")) / grdName.Item(grdName.Row, "WholeSale")
                str_ValueSQL = str_Value
            End If
        End If
        grdName.Item(grdName.Row, "GrossMargin") = str_Value
        executeSQL("Update " & tableName & " set GrossMargin = " & str_ValueSQL & " where DevNo = " & MyId)

        str_Value = ""
        str_ValueSQL = "NULL"
        If Not IsDBNull(grdName.Item(grdName.Row, "WholeSale")) And Not IsDBNull(grdName.Item(grdName.Row, "ActualSales")) Then
            str_Value = (grdName.Item(grdName.Row, "WholeSale") * grdName.Item(grdName.Row, "ActualSales"))
            str_ValueSQL = str_Value
        End If
        grdName.Item(grdName.Row, "ActualsSales") = str_Value
        executeSQL("Update " & tableName & " set ActualsSales = " & str_ValueSQL & " where DevNo = " & MyId)

        calculateMargin()

        '   New Standard cost calcultion 
        '   -------------------------------------

        'Try


        '    Dim SelectDataT As DataTable
        '    SelectDataT = getSelectDataTable("SELECT [DevNo], [JbaNo], [Freeze] ,[FinalFob],[Season] ,[SourcingAsia],[Freight],[AirFreight],[Duty]   ,[Duty2],[Operations],[Planning],[SourcingEurope],[SourceLocation],[QualityControl],[Reserve],[StandardCost], [Wholesale], [GrossMargin], [Currency] FROM [NAPA].[dbo].[NewGrid]  where DevNo = " & MyId)

        '    If IsNothing(SelectDataT.Rows(0).Item("FinalFob")) Then
        '        GoTo Last1
        '    End If
        '    MsgBox(SelectDataT.Rows(0).Item("FinalFob"))

        '    If String.IsNullOrEmpty(SelectDataT.Rows(0).Item("FinalFob")) Then

        '        MsgBox(" Fob is null or empty")
        '        GoTo Last1
        '    End If



        '    Dim FOB As Double = SelectDataT.Rows(0).Item("FinalFob")
        '    Dim AsiaSourcing As Double


        '    Dim DutyP As Double = SelectDataT.Rows(0).Item("Duty")

        '    If IsNothing(SelectDataT.Rows(0).Item("Duty")) Then
        '        DutyP = 0
        '    Else
        '        DutyP = SelectDataT.Rows(0).Item("Duty")
        '    End If


        '    Dim FOBEUR As Double
        '    Dim AsiaSourc As Double
        '    Dim Freight As Double
        '    Dim AirFreight As Double
        '    Dim Duty As Double
        '    Dim SourcingUS As Double
        '    Dim Operations As Decimal
        '    Dim Planning As Double
        '    Dim EUSourc As Double
        '    Dim QualityControl As Double
        '    Dim Reserves As Double
        '    Dim Wholesal As Double

        '    AsiaSourcing = 0.0605 For FY21  only If currency Is USD 
        '    AsiaSourcing = 0.0605


        '    If (SelectDataT.Rows(0).Item("Currency") = "USD") Then

        '        FOBEUR = Math.Round(FOB / 1.15, 2, MidpointRounding.AwayFromZero)
        '        AsiaSourc = Math.Round(FOBEUR * AsiaSourcing, 2, MidpointRounding.AwayFromZero)
        '        Freight = Math.Round(FOBEUR * 0.019, 2, MidpointRounding.AwayFromZero)
        '        AirFreight = Math.Round(FOBEUR * 0.55 / 100, 2, MidpointRounding.AwayFromZero)
        '        Duty = Math.Round((FOBEUR + Freight + AirFreight) * DutyP / 100, 2, MidpointRounding.AwayFromZero)
        '        SourcingUS = 0
        '        Operations = Math.Round(FOBEUR * 2.5 / 100, 2, MidpointRounding.AwayFromZero)
        '        Planning = Math.Round(FOBEUR * 1.15 / 100, 2)
        '        EUSourc = Math.Round(FOBEUR * 0.45 / 100, 2)
        '        QualityControl = Math.Round(FOBEUR * 0.5 / 100, 2)
        '        Reserves = Math.Round(FOBEUR * 2.8 / 100, 2)
        '    Else
        '        FOBEUR = Math.Round(FOB, 2)
        '        If (SelectDataT.Rows(0).Item("SourceLocation") = "AS") Then
        '            AsiaSourc = Math.Round(FOBEUR * 0.0605, 2)
        '        Else
        '            AsiaSourc = 0
        '        End If


        '        Freight = Math.Round(FOBEUR * 1.9 / 100, 2)
        '        AirFreight = Math.Round(FOBEUR * 0.55 / 100, 2)
        '        Duty = Math.Round((FOBEUR + Freight + AirFreight) * DutyP / 100, 2)
        '        SourcingUS = 0
        '        Operations = Math.Round(FOBEUR * 2.5 / 100, 2, MidpointRounding.AwayFromZero)
        '        Planning = Math.Round((FOBEUR * 1.15 / 100), 2)
        '        EUSourc = Math.Round((FOBEUR * 0.45 / 100), 2)
        '        QualityControl = Math.Round((FOBEUR * 0.5 / 100), 2)
        '        Reserves = Math.Round((FOBEUR * 2.8 / 100), 2)


        '    End If

        '    Dim StdCoast As Double = FOBEUR + AsiaSourc + Freight + AirFreight + Duty + SourcingUS + Operations + Planning + EUSourc + QualityControl + Reserves
        '    Wholesal = SelectDataT.Rows(0).Item("WholeSale")

        '    Dim GrosMargin As Double = (Wholesal - StdCoast) / Wholesal
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [StandardCost] = " & StdCoast & " where DevNo = " & MyId)
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [GrossMargin] = " & GrosMargin & " where DevNo = " & MyId)
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [FobInEuro] = " & FOBEUR & " where DevNo = " & MyId)

        '    MsgBox("STD COST :    " & StdCoast)
        '    If tableName <> "SalesYouth" Then GridRefresh()

        'Catch ex As Exception



        'End Try

        '   New Standard cost calcultion 
        '   -------------------------------------

        Try


            Dim SelectDataT As DataTable
            SelectDataT = getSelectDataTable("SELECT [DevNo], [JbaNo], [Freeze] ,[FinalFob],[Season] ,[SourcingAsia],[Freight],[AirFreight],[Duty]   ,[Duty2],[Operations],[Planning],[SourcingEurope],[SourceLocation],[QualityControl],[Reserve],[StandardCost], [Wholesale], [GrossMargin], [Currency] FROM [NAPA].[dbo].[NewGrid]  where DevNo = " & MyId)
            Dim Ssn As String
            Ssn = SelectDataT.Rows(0).Item("Season")
            Dim DTBuckets As DataTable
            Dim DTRateUS As DataTable
            DTBuckets = getSelectDataTable("Select [Coalition],[Season],[SourcingAsia],[Freight],[AirFreight],[Operations],[Planning],[SourcingEurope],[QualityControl],[Reserve],[TurinBuilding],[TrimWarehouse],[Turin_RMSourcing],[Markup],[Brand],[HandlingFee] from [NAPA].[dbo].[Param_CostBuckets] where season='" & Ssn & "'")
            DTRateUS = getSelectDataTable("Select [Season],[Currency],[Rate],[Coalition] from [NAPA].[dbo].[tbl_sourcing_Rates] where Season='" & Ssn & "' and currency ='USD'")

            If IsNothing(SelectDataT.Rows(0).Item("FinalFob")) Then
                GoTo Last1
            End If
            ' MsgBox(SelectDataT.Rows(0).Item("FinalFob"))

            If String.IsNullOrEmpty(SelectDataT.Rows(0).Item("FinalFob")) Then

                MsgBox(" Fob is null or empty")
                GoTo Last1
            End If



            Dim FOB As Double = SelectDataT.Rows(0).Item("FinalFob")
            Dim AsiaSourcing As Double


            Dim DutyP As Double = SelectDataT.Rows(0).Item("Duty")

            If IsNothing(SelectDataT.Rows(0).Item("Duty")) Then
                DutyP = 0
            Else
                DutyP = SelectDataT.Rows(0).Item("Duty")
            End If


            Dim FOBEUR As Double
            Dim AsiaSourc As Double
            Dim Freight As Double
            Dim AirFreight As Double
            Dim Duty As Double
            Dim SourcingUS As Double
            Dim Operations As Decimal
            Dim Planning As Double
            Dim EUSourc As Double
            Dim QualityControl As Double
            Dim Reserves As Double
            Dim Wholesal As Double
            Dim Rate As Double = 1

            '  AsiaSourcing = 0.0605 for FY21  only if currency is USD 
            AsiaSourcing = DTBuckets.Rows(0).Item("SourcingAsia")
            SourcingUS = 0
            If (SelectDataT.Rows(0).Item("Currency") = "USD") Then
                FOBEUR = Math.Round(FOB / DTRateUS.Rows(0).Item("Rate"), 2, MidpointRounding.AwayFromZero)
                AsiaSourc = Math.Round(FOBEUR * AsiaSourcing, 2, MidpointRounding.AwayFromZero)
                Freight = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("Freight"), 2, MidpointRounding.AwayFromZero)
                AirFreight = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("AirFreight"), 2, MidpointRounding.AwayFromZero)
                Duty = Math.Round((FOBEUR + Freight + AirFreight) * DutyP / 100, 2, MidpointRounding.AwayFromZero)
            Else
                FOBEUR = Math.Round(FOB, 2)
                If (SelectDataT.Rows(0).Item("SourceLocation") = "AS") Then
                    AsiaSourc = Math.Round(FOBEUR * AsiaSourcing, 2)
                Else
                    AsiaSourc = 0
                End If
                Freight = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("Freight"), 2)
                AirFreight = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("AirFreight"), 2)
                Duty = Math.Round((FOBEUR + Freight + AirFreight) * DutyP / 100, 2)
            End If
            Operations = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("Operations"), 2, MidpointRounding.AwayFromZero)
            Planning = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("Planning"), 2)
            EUSourc = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("SourcingEurope"), 2)
            QualityControl = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("QualityControl"), 2)
            Reserves = Math.Round(FOBEUR * DTBuckets.Rows(0).Item("Reserve"), 2)

            Dim StdCoast As Double = Math.Round(FOBEUR + AsiaSourc + Freight + AirFreight + Duty + SourcingUS + Operations + Planning + EUSourc + QualityControl + Reserves, 2)
            Wholesal = SelectDataT.Rows(0).Item("WholeSale")

            Dim GrosMargin As Double = (Wholesal - StdCoast) / Wholesal
            'MsgBox(SelectDataT.Rows(0).Item("StandardCost"))

            executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [StandardCost] = " & StdCoast & " where DevNo = " & MyId)
            executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [GrossMargin] = " & GrosMargin & " where DevNo = " & MyId)
            executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [FobInEuro] = " & FOBEUR & " where DevNo = " & MyId)

            'MsgBox("STD COST :    " & StdCoast)
            'MsgBox("GROSS MARGIN :    " & GrosMargin)
            ' If tableName <> "SalesYouth" Then GridRefresh()
            'End If
        Catch ex As Exception



        End Try

        'Try


        '    Dim SelectDataT As DataTable
        '    SelectDataT = getSelectDataTable("SELECT [DevNo], [JbaNo], [Freeze] ,[FinalFob],[Season] ,[SourcingAsia],[Freight],[AirFreight],[Duty]   ,[Duty2],[Operations],[Planning],[SourcingEurope],[SourceLocation],[QualityControl],[Reserve],[StandardCost], [Wholesale], [GrossMargin], [Currency] FROM [NAPA].[dbo].[NewGrid]  where DevNo = " & MyId)
        '    Dim Ssn As String
        '    Ssn = SelectDataT.Rows(0).Item("Season")
        '    Dim DTBuckets As DataTable
        '    Dim DTRateUS As DataTable
        '    DTBuckets = getSelectDataTable("Select [Coalition],[Season],[SourcingAsia],[Freight],[AirFreight],[Operations],[Planning],[SourcingEurope],[QualityControl],[Reserve],[TurinBuilding],[TrimWarehouse],[Turin_RMSourcing],[Markup],[Brand],[HandlingFee] from [NAPA].[dbo].[Param_CostBuckets] where season='" & Ssn & "'")
        '    DTRateUS = getSelectDataTable("Select [Season],[Currency],[Rate],[Coalition] from tbl_sourcing_Rates where Season='" & Ssn & "' and currency ='USD'")

        '    If IsNothing(SelectDataT.Rows(0).Item("FinalFob")) Then
        '        GoTo Last1
        '    End If
        '    ' MsgBox(SelectDataT.Rows(0).Item("FinalFob"))

        '    If String.IsNullOrEmpty(SelectDataT.Rows(0).Item("FinalFob")) Then

        '        MsgBox(" Fob is null or empty")
        '        GoTo Last1
        '    End If



        '    Dim FOB As Double = SelectDataT.Rows(0).Item("FinalFob")
        '    Dim AsiaSourcing As Double


        '    Dim DutyP As Double = SelectDataT.Rows(0).Item("Duty")

        '    If IsNothing(SelectDataT.Rows(0).Item("Duty")) Then
        '        DutyP = 0
        '    Else
        '        DutyP = SelectDataT.Rows(0).Item("Duty")
        '    End If


        '    Dim FOBEUR As Double
        '    Dim AsiaSourc As Double
        '    Dim Freight As Double
        '    Dim AirFreight As Double
        '    Dim Duty As Double
        '    Dim SourcingUS As Double
        '    Dim Operations As Decimal
        '    Dim Planning As Double
        '    Dim EUSourc As Double
        '    Dim QualityControl As Double
        '    Dim Reserves As Double
        '    Dim Wholesal As Double
        '    Dim Rate As Double = 1

        '    '  AsiaSourcing = 0.0605 for FY21  only if currency is USD 
        '    AsiaSourcing = DTBuckets.Rows(0).Item("SourcingAsia")
        '    SourcingUS = 0
        '    FOBEUR = FOB
        '    AsiaSourc = FOBEUR * AsiaSourcing
        '    If (SelectDataT.Rows(0).Item("Currency") = "USD") Then
        '        FOBEUR = FOB / DTRateUS.Rows(0).Item("Rate")
        '    Else
        '        If (SelectDataT.Rows(0).Item("SourceLocation") <> "AS") Then AsiaSourc = 0
        '    End If
        '    FOBEUR = Math.Round(FOBEUR, 2, MidpointRounding.AwayFromZero)
        '    Freight = FOBEUR * DTBuckets.Rows(0).Item("Freight")
        '    AirFreight = FOBEUR * DTBuckets.Rows(0).Item("AirFreight")
        '    Duty =( FOBEUR + Freight + AirFreight) * DutyP / 100

        '    Operations = FOBEUR * DTBuckets.Rows(0).Item("Operations")
        '    Planning = FOBEUR * DTBuckets.Rows(0).Item("Planning")
        '    EUSourc = FOBEUR * DTBuckets.Rows(0).Item("SourcingEurope")
        '    QualityControl = FOBEUR * DTBuckets.Rows(0).Item("QualityControl")
        '    Reserves = FOBEUR * DTBuckets.Rows(0).Item("Reserve")

        '    Dim StdCoast As Double = FOBEUR + AsiaSourc + Freight + AirFreight + Duty + SourcingUS + Operations + Planning + EUSourc + QualityControl + Reserves
        '    Wholesal = SelectDataT.Rows(0).Item("WholeSale")
        '    StdCoast = Math.Round(FOBEUR, 2, MidpointRounding.AwayFromZero)
        '    Dim GrosMargin As Double = (Wholesal - StdCoast) / Wholesal
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [StandardCost] = " & StdCoast & " where DevNo = " & MyId)
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [GrossMargin] = " & GrosMargin & " where DevNo = " & MyId)
        '    executeSQL("Update  [NAPA].[dbo].[NewGrid]  set [FobInEuro] = " & FOBEUR & " where DevNo = " & MyId)

        '    ' MsgBox("STD COST :    " & StdCoast)
        '    ' If tableName <> "SalesYouth" Then GridRefresh()

        'Catch ex As Exception



        'End Try

Last1:

    End Sub

    Public Function getSelectDataTable(ByVal str_sql As String) As DataTable
        Try
            Dim SelectAdapter As New SqlDataAdapter
            Dim SelectData As New DataTable

            If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
            SelectAdapter.SelectCommand = dcNAPA.CreateCommand
            SelectAdapter.SelectCommand.CommandText = str_sql
            'SelectAdapter.SelectCommand.CommandTimeout = 0
            SelectAdapter.Fill(SelectData)
            SelectData.CreateDataReader()
            getSelectDataTable = SelectData

        Catch ex As Exception

            Throw
        End Try

        '-------


    End Function



    Public Sub calculateMargin()
        Dim i_row As Integer
        Dim i_GrossMCounter As Integer = 0
        Dim i_nrOfColors As Integer = 0
        Dim i_nrOfColorRows As Integer = 0
        Dim i_nrOfActiveColors As Integer = 0
        Dim i_activeStyles As Integer = 0
        Dim i_Actuals As Integer = 0

        Dim d_sumActualSales As Double = 0
        Dim d_TotalCost As Double = 0
        Dim d_Turnover As Double = 0
        Dim d_TotalCostOne As Double = 0
        Dim d_TurnoverOne As Double = 0
        Dim i_forcastCounter As Integer = 0
        Dim i_sumForecasts As Integer = 0
        Dim i_sumForecastsAll As Integer = 0

        If Not b_formLoading Then
            For i_row = 0 To grdMain.RowCount - 1
                If grdMain.Item(i_row, "Stylestatus").ToString = "0" Then
                    If Not IsDBNull(grdMain.Item(i_row, "RevisedForecastFinal")) Then i_sumForecastsAll += grdMain.Item(i_row, "RevisedForecastFinal")
                    If Not IsDBNull(grdMain.Item(i_row, "GrossMargin")) Then
                        i_GrossMCounter += 1
                        If Not IsDBNull(grdMain.Item(i_row, "RevisedForecastFinal")) Then
                            If grdMain.Item(i_row, "RevisedForecastFinal") > 0 Then
                                If Not IsDBNull(grdMain.Item(i_row, "StandardCost")) Then
                                    d_TotalCost += (grdMain.Item(i_row, "StandardCost") * grdMain.Item(i_row, "RevisedForecastFinal"))
                                    d_TotalCostOne += grdMain.Item(i_row, "StandardCost")
                                End If
                                If Not IsDBNull(grdMain.Item(i_row, "WholeSale")) Then
                                    d_Turnover += (grdMain.Item(i_row, "WholeSale") * grdMain.Item(i_row, "RevisedForecastFinal"))
                                    d_TurnoverOne += grdMain.Item(i_row, "WholeSale")
                                End If
                                i_sumForecasts += grdMain.Item(i_row, "RevisedForecastFinal")
                            End If
                        End If
                    End If
                    If Not IsDBNull(grdMain.Item(i_row, "StandardCost")) Then
                        d_TotalCostOne += grdMain.Item(i_row, "StandardCost")
                    End If
                    If Not IsDBNull(grdMain.Item(i_row, "WholeSale")) Then
                        d_TurnoverOne += grdMain.Item(i_row, "WholeSale")
                    End If
                    i_forcastCounter += 1
                    If grdMain.Item(i_row, "ActualsSales").ToString.Length > 0 Then
                        d_sumActualSales += grdMain.Item(i_row, "ActualSales")
                        i_Actuals += 1
                    End If
                    If grdMain.Item(i_row, "ActualLotNumber").ToString.Length > 0 And Not grdMain.Item(i_row, "ActualLotNumber").ToString = "0" Then
                        If grdMain.Item(i_row, "ActualLotNumber").ToString.Length > 0 Then
                            i_nrOfColors += grdMain.Item(i_row, "ActualLotNumber")
                        End If
                        If grdMain.Item(i_row, "ActualLotNumber_active").ToString.Length > 0 Then
                            i_nrOfActiveColors += grdMain.Item(i_row, "ActualLotNumber_active")
                        End If
                        i_nrOfColorRows += 1
                    End If
                    'If grdMain.Item(i_row, "ActualLotNumber_active").ToString.Length > 0 And Not grdMain.Item(i_row, "ActualLotNumber_active").ToString = "0" Then
                    '    If grdMain.Item(i_row, "ActualLotNumber_active").ToString.Length > 0 Then
                    '        i_nrOfActiveColors += grdMain.Item(i_row, "ActualLotNumber_active")
                    '    End If
                    '    If grdMain.Item(i_row, "ActualLotNumber").ToString.Length > 0 Then
                    '        i_nrOfColors += grdMain.Item(i_row, "ActualLotNumber")
                    '    End If
                    '    i_nrOfColorRows += 1
                    'End If
                    i_activeStyles += 1
                End If
            Next
            lblGrossMarginAll.Text = i_sumForecastsAll.ToString("#,#0") & " pcs"
            lblGrossMarginFF.Text = Format((d_Turnover - d_TotalCost) / If(d_Turnover = 0, 1, d_Turnover), "0.0%") & " (" & i_sumForecasts.ToString("#,#0") & " pcs)"
            lblGrossMarginOne.Text = Format((d_TurnoverOne - d_TotalCostOne) / If(d_TurnoverOne = 0, 1, d_TurnoverOne), "0.0%") & " (" & i_forcastCounter.ToString("#,#0") & " styles)"
            lblActualsSales.Text = Format(d_sumActualSales / IIf(i_Actuals = 0, 1, i_Actuals), "0.0%") & " (" & i_Actuals.ToString("#,#0") & " pcs)"
            If i_nrOfColorRows > 0 Then
                lblNrOfColors.Text = CInt(i_nrOfColors / i_nrOfColorRows).ToString & "/" & CInt(i_nrOfActiveColors / i_nrOfColorRows).ToString
            Else
                lblNrOfColors.Text = "0/0"
            End If
            lblNrOfColors.Text &= " (All/Active for " & i_nrOfColorRows & " rows)"
            grdMain.Splits(0).Caption = "Main     Records: " & grdMain.RowCount & " - " & i_activeStyles & " active"
            'lblGrossMarginCount.Text = i_GrossMCounter & " records filled in"
        End If
       


    End Sub

    Private Sub writeDevNoToTable()
        If Not b_formLoading Then
            Try
                executeSQL("DELETE FROM tmp_NewGridAfterFilter WHERE userNaam ='" & UserId & "'")
            Finally
                For i_row As Integer = 0 To grdMain.RowCount - 1
                    executeSQL("INSERT INTO tmp_NewGridAfterFilter (userNaam, styleNaam, DevNo) VALUES ('" & UserId & "', '" & grdMain.Item(i_row, "Name") & "', " & grdMain.Item(i_row, "DevNo") & ")")
                Next
            End Try
        End If
    End Sub

    Public Sub fillTemplateCombo()
        'ComboBox1.Items.Clear()
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = getSelectDataSet("SELECT ID, TemplateName FROM param_excelTemplates WHERE owner='" & UserId & "' OR ID IN(SELECT ID From param_excelTemplates_access WHERE userGroup = (SELECT usergroup FROM param_users WHERE usernaam='" & UserId & "') OR upper(usergroup) = '(ALL)')").Tables(0)
        ComboBox1.ValueMember = "ID"
        ComboBox1.DisplayMember = "TemplateName"
        ComboBox1.Update()
        ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
    End Sub

    Private Sub SplitAndUpdate_Producers(ByVal strColumn As String, ByVal strSelected As String, ByVal strValue As String, ByVal devNo As Integer, ByVal rowNr As Integer)
        Dim FirstPart As String = ""
        Dim SecondPart As String = ""
        Dim SearchPart As String = Mid(strSelected, InStr(strSelected, "-") + 1, Len(strSelected))

        If SearchPart <> "" And Not strValue = "TBD" Then
            FirstPart = Trim(Mid(SearchPart, 1, InStr(SearchPart, "-") - 1))
            SecondPart = Mid(SearchPart, InStr(SearchPart, "-") + 1, Len(SearchPart))
        End If
        Select Case strColumn
            Case "SmsProducer"
                grdMain.Item(rowNr, "SmsProducer") = strValue
                grdMain.Item(rowNr, "SmsMadeIn") = FirstPart
                executeSQL("Update NewGrid set SmsProducer=" & IIf(strValue = "TBD", "NULL", "'" & strValue & "'") & ", SmsMadeIn = " & IIf(FirstPart.Length = 0, "NULL", "'" & FirstPart & "'") & " where DevNo = " & devNo)
            Case "VendorBulk"
                grdMain.Item(rowNr, "VendorBulk") = strValue
                grdMain.Item(rowNr, "MadeInBulk") = FirstPart
                grdMain.Item(rowNr, "BulkProducer") = IIf(strValue = "TBD", "", strSelected)
                grdMain.Item(rowNr, "BulkProducerMadeIn") = FirstPart
                executeSQL("Update NewGrid set VendorBulk=" & IIf(strValue = "TBD", "NULL", "'" & strValue & "'") & ", MadeInBulk = " & IIf(FirstPart.Length = 0, "NULL", "'" & FirstPart & "'") & ", BulkProducer = " & IIf(strValue = "TBD", "NULL", "'" & strValue & "'") & ", BulkProducerMadeIn = " & IIf(FirstPart.Length = 0, "NULL", "'" & FirstPart & "'") & " where DevNo = " & devNo)
        End Select
    End Sub

    Private Sub mnuPicAndLabelA_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        Select Case sender.Text
            Case "Update Picture"
                If MsgBox("This will update the picture on the main screen in Quest." & vbCrLf & "Do you want to continue?", vbYesNo, "Update Picture") = MsgBoxResult.Yes Then
                    Dim str_picture As String = getNewPicture(grdMain.Item(sender.Tag, "DevNo").ToString)
                    If Not str_picture = "" Then
                        If str_picture.ToUpper <> grdMain.Item(sender.Tag, "picture").ToString.ToUpper Then
                            executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & grdMain.Item(sender.Tag, "DevNo").ToString & "', 'picture', '" & grdMain.Item(sender.Tag, "picture").ToString & "', '" & str_picture & "', '" & UserId & "')")
                        End If
                        executeSQL("UPDATE NewGrid SET picture='" & str_picture & "' WHERE DevNo=" & grdMain.Item(sender.Tag, "DevNo").ToString)
                        executeSQL("UPDATE questpdmnapa.dbo.STYLES set pictmoda01='" & str_picture & "' WHERE moc091=" & grdMain.Item(sender.Tag, "DevNo").ToString)
                        grdMain.Item(sender.Tag, "picture") = str_picture
                        grdMain_RowColChange(Nothing, Nothing)
                    End If
                End If
            Case "Remove Picture"
                If MsgBox("This will update the picture on the main screen in Quest." & vbCrLf & "Are you sure you want to remove the picture for style " & grdMain.Item(sender.Tag, "Name").ToString & "?", MsgBoxStyle.YesNo, "Remove Picture") = MsgBoxResult.Yes Then
                    executeSQL("UPDATE NewGrid SET Picture=NULL, pictureDate=NULL WHERE DevNo ='" & grdMain.Item(sender.Tag, "DevNo").ToString & "'")
                    executeSQL("DELETE FROM NAPA.dbo.vf_Sketches WHERE Lotnumber ='" & grdMain.Item(sender.Tag, "DevNo").ToString & "'")
                    executeSQL("UPDATE questpdmnapa.dbo.STYLES set pictmoda01='NULL' WHERE moc091=" & grdMain.Item(sender.Tag, "DevNo").ToString)
                    grdMain.Item(sender.Tag, "picture") = ""
                    grdMain_RowColChange(Nothing, Nothing)
                End If
        End Select
    End Sub

    Private Sub mnuTemplate_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        Select Case sender.Text
            Case "Edit Template"
                frmCustomExcel.updateExcel = True
                frmCustomExcel.MdiParent = frmMain
                frmCustomExcel.Show()
            Case "Delete Template"
                'Delete the template properties
                executeSQL("DELETE FROM dbo.param_excelTemplates WHERE ID=" & sender.Tag)
                'Delete the template access rights
                executeSQL("DELETE FROM dbo.param_excelTemplates_access WHERE ID=" & sender.Tag)
                'Delete the template columns
                executeSQL("Delete From CustomExcel where TemplateID = " & sender.Tag)
                fillTemplateCombo()
        End Select
    End Sub

    Private Sub mnuFilter_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        If sender.Tag.ToString.Length > 0 Then
            Dim str_filters() As String = Split(sender.Tag, ":")
            If str_filters.Length = 3 Then
                grdMain.Columns(Trim(str_filters(1))).FilterText = ""
                executeSQL("UPDATE " & GridUserTable & " SET filter= NULL WHERE formID='" & Me.Name & "' AND gridID='" & grdMain.Name & "' AND userID='" & UserId & "' AND ColumnID = '" & grdMain.Columns(Trim(str_filters(1))).DataField & "' AND Brand ='N'")
            End If
        End If
    End Sub


    Private Sub grdMain_ColMove(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColMoveEventArgs) Handles grdMain.ColMove
        SaveGridLayout(Me, grdMain)
    End Sub

    Private Sub btnNotesOk_Click(sender As Object, e As EventArgs) Handles btnNotesOk.Click
        Try
            CurrNotesColumn.Value = txtNotes.Text
            pnlNotes.Hide()
            CurrNotesColumn.DataChanged = True

            Dim Sql As String
            Dim MyId As String = grdMain.Item(grdMain.Row, "DevNo").ToString
            Dim myTotal As Integer = 0
            Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = CurrNotesColumn 'e.Column.DataColumn 

            If grdMain.Columns(col.DataField).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox Then Exit Sub

            Dim NewValue As String = grdMain.Item(grdMain.Row, col.DataField).ToString
            If NewValue <> oldValue Then
                executeSQL("Insert Into SwaCheck (DevNo, FieldName, OldValue, NewValue, myUser) Values ('" & MyId & "', '" & col.DataField & "', '" & oldValue & "', '" & NewValue & "', '" & UserId & "')")
            End If

            Sql = "Update Newgrid set " & col.DataField & " = "
            Select Case col.DataType.ToString
                Case "System.Boolean"
                    Sql = Sql & IIf(NewValue = "True", "-1", "0")
                Case "System.Int32", "System.Decimal"
                    Sql = Sql & IIf(NewValue.Length = 0, "NULL", NewValue)
                Case "System.String"
                    Sql = Sql & IIf(NewValue.Length = 0 Or NewValue = "TBD", "NULL", "'" & NewValue & "'")
                Case "System.DateTime"
                    Sql = Sql & IIf(NewValue.Length = 0, "NULL", "CONVERT(DATETIME, '" & Format(CDate(NewValue), "yyyy-MM-dd") & "', 102)")
                Case Else
                    MsgBox("Something is Wrong. Your data can be lost. Please Contact Patrick Waeytens", MsgBoxStyle.Critical)
                    Exit Sub
            End Select
            'MsgBox(Sql & " WHERE DevNo = " & MyId)
            executeSQL(Sql & " WHERE DevNo = " & MyId)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMain_BeforeColEdit(sender As Object, e As C1.Win.C1TrueDBGrid.BeforeColEditEventArgs) Handles grdMain.BeforeColEdit
        Try
            If (e.Column.DropDownList = False) Then

                If (grdMain.FocusedSplit.Caption.ToLower = "notes on the collection") Then
                    Dim oSender As C1.Win.C1TrueDBGrid.C1TrueDBGrid = sender
                    If oSender.FilterActive = False Then

                        CurrNotesColumn = e.Column.DataColumn
                        e.Column.AllowFocus = True
                        lblBreadCrum.Text = "You are editing notes - " & e.Column.DataColumn.Caption & " column, with DevNo:" & grdMain.Item(grdMain.Row, "DevNo").ToString
                        If (Not IsDBNull(e.Column.DataColumn.Value)) Then
                            txtNotes.Text = e.Column.DataColumn.Value
                        Else
                            txtNotes.Text = ""
                        End If
                        pnlNotes.Show()
                        pnlNotes.Width = Me.Width
                        pnlNotes.Height = Me.Height
                        txtNotes.Focus()
                        pnlNotes.BackColor = Color.Transparent
                    End If

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub grdMain_DoubleClick(sender As Object, e As EventArgs) Handles grdMain.DoubleClick
    '    Try

    '        If (grdMain.FocusedSplit.Caption.ToLower = "notes on the collection" And e.Column.DropDownList = False) Then

    '            CurrNotesColumn = e.Column.DataColumn
    '            e.Column.AllowFocus = True
    '            lblBreadCrum.Text = "You are editing notes - " & e.Column.DataColumn.Caption & " column, with DevNo:" & grdMain.Item(grdMain.Row, "DevNo").ToString
    '            If (Not IsDBNull(e.Column.DataColumn.Value)) Then
    '                txtNotes.Text = e.Column.DataColumn.Value
    '            Else
    '                txtNotes.Text = ""
    '            End If
    '            pnlNotes.Show()
    '            pnlNotes.Width = Me.Width
    '            pnlNotes.Height = Me.Height
    '            txtNotes.Focus()
    '            pnlNotes.BackColor = Color.Transparent

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub updateDroppedDate(ByVal sColumnName As String, ByVal sDevno As String, ByVal bChecked As Boolean)
        sColumnName = sColumnName & "Date"
        If bChecked Then
            executeSQL("update NewGrid set " & sColumnName & "=GETDATE() where devno= " & sDevno)
        Else
            executeSQL("update NewGrid set " & sColumnName & "=NULL where devno= " & sDevno)
        End If
    End Sub

    Private Sub applyFormulas(ByVal ColumnName As String, ByVal value As String, ByVal devno As String)
        Dim sql As String
        Dim odt As DataTable
        Try
            sql = "select columnname,formulastring from gridlayout where formulastring like '%[[" & ColumnName & "]]%'"
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
                    dtNewgrid = getSelectDataSet("select " & sformulaFields & " from newgrid where devno=" & devno).Tables(0)

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
                                sUpdateSql = "update newgrid set " & supdateColumn & " = NULL where devno=" & devno
                                executeSQL(sUpdateSql)
                                grdMain.Item(grdMain.Row, supdateColumn) = ""
                                'MsgBox("The Column '" & aryFormulaFields(i) & "' is Null, so unable to calcuate '" & supdateColumn & "'." & vbCrLf & "Formula:" & sFormula)
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
                                sUpdateSql = "update newgrid set " & supdateColumn & " = " & sVal & " where devno=" & devno
                                executeSQL(sUpdateSql)
                                grdMain.Item(grdMain.Row, supdateColumn) = sVal
                            End If

                        End If

                    End If
                Next

            End If

        Catch ex As Exception
            'SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", "Napalinelist Error Apply formula", "Error Applying Formula Devno:" & devno & " column:" & ColumnName)
        End Try

    End Sub

End Class