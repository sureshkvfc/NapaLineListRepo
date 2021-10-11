Imports System.Data.SqlClient
Public Class frmMaintenance
    Public tableName As String = ""
    Public tableQuery As String = ""
    Public DoCalculation As Boolean = False
    Public LockColumnName As Boolean = False
    Private b_dataChanged As Boolean = False
    Private str_Seasons As String = ""

    Private Sub grdMaintenance_ButtonClick(sender As Object, e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdMaintenance.ButtonClick
        Dim foundrows() As Data.DataRow
        Dim ComboAdapter As New SqlDataAdapter
        Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection
        Dim mySql As String
        Dim j As Integer

        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = e.Column.DataColumn

        ' OldValue = IIf(IsDBNull(grdMain.Item(grdMain.Row, col.DataField)), "", grdMain.Item(grdMain.Row, col.DataField))

        foundrows = LayoutData.Tables(0).Select("ColumnName = '" & col.DataField & "'") '& "' And SeasonLinked = 'True'")
        If foundrows.Length > 0 Then
            ComboAdapter.SelectCommand = dcNAPA.CreateCommand
            Dim ComboData As New DataSet
            mySql = foundrows(0).Item("DropDownSQL")
            'If mySql.ToString.ToUpper.IndexOf("ORDER BY") > 0 Then mySql = mySql.ToString.Substring(0, mySql.ToString.ToUpper.IndexOf("ORDER BY"))
            'mySql &= " INNER JOIN dbo.Param_Seasons_Napa " & _
            '            "ON " & foundrows(0).Item("SeasonName") & " LIKE '%' + dbo.Param_Seasons_Napa.Season + '%'" & _
            '            "WHERE (dbo.Param_Seasons_Napa.Season = '" & grdMaintenance.Columns("Season").CellValue(grdMaintenance.Row) & "') "
            'If Not IsDBNull(foundrows(0).Item("ExtraSeasonField")) Then mySql &= " AND " & foundrows(0).Item("ExtraSeasonField")
            'mySql &= IIf(col.DataField.ToLower.StartsWith("prodcolor") Or col.DataField.ToLower.StartsWith("smscolor"), " order by colourart", "")
            ComboData = getSelectDataSet(mySql)
            v = Me.grdMaintenance.Columns(foundrows(0).Item("columnName")).ValueItems.values
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
                Me.grdMaintenance.Columns(foundrows(0).Item("columnName")).ValueItems.Translate = True
                Me.grdMaintenance.Columns(foundrows(0).Item("columnName")).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                Me.grdMaintenance.Columns(foundrows(0).Item("columnName")).FilterDropdown = True
                Me.grdMaintenance.FocusedSplit.DisplayColumns(foundrows(0).Item("columnName")).DropDownList = True
            End If
        End If
    End Sub


    Private Sub grdMaintenance_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdMaintenance.AfterColUpdate
        If tableName.ToLower = "tbl_approvedsuppliers" Then addSupplierDescription(grdMaintenance.Row)
    End Sub

    Private Sub grdMaintenance_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaintenance.AfterDelete
        AfterEdit(MaintainData.GetChanges(DataRowState.Deleted))
    End Sub

    Private Sub grdMaintenance_AfterInsert(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaintenance.AfterInsert
        AfterEdit(MaintainData.GetChanges(DataRowState.Added))
    End Sub

    Private Sub grdMaintenance_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaintenance.AfterUpdate
        AfterEdit(MaintainData.GetChanges(DataRowState.Modified))
    End Sub

    Private Sub frmMaintenance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.UseWaitCursor = True
        If DoCalculate = True And b_dataChanged = True Then
            Debug.WriteLine(str_Seasons)
            calculateTable("SalesYouth")
            calculateTable("NewGrid")
            frmMainGrid.btnRefresh_Click(Nothing, Nothing)
            DoCalculate = False
        Else
            Dim ComboAdapter As New SqlDataAdapter
            Dim i As Integer
            Dim j As Integer
            Dim foundRows() As Data.DataRow
            Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection
            foundRows = Nothing
            ComboAdapter.SelectCommand = dcNAPA.CreateCommand

            For i = 0 To frmMainGrid.grdMain.Columns.Count - 1
                foundRows = LayoutData.Tables(0).Select("ColumnName = '" & frmMainGrid.grdMain.Columns(i).DataField & "' And HasDropDown = True")
                If foundRows.Length > 0 Then
                    Dim ComboData As New DataSet
                    ComboAdapter.SelectCommand.CommandText = foundRows(0).ItemArray(3)
                    ComboAdapter.Fill(ComboData)
                    ComboData.CreateDataReader()
                    v = frmMainGrid.grdMain.Columns(foundRows(0).ItemArray(0)).ValueItems.Values
                    v.Clear()
                    v.Add(New C1.Win.C1TrueDBGrid.ValueItem("TBD", "To Be Defined"))
                    For j = 0 To ComboData.Tables(0).Rows.Count - 1
                        Try
                            v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(5)), ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(4))))
                        Catch

                        End Try
                        Next
                    frmMainGrid.grdMain.Columns(foundRows(0).ItemArray(0)).ValueItems.Translate = True
                    frmMainGrid.grdMain.Columns(i).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                    frmMainGrid.grdMain.Columns(i).FilterDropdown = True
                    frmMainGrid.grdMain.Splits(0).DisplayColumns(i).DropDownList = True
                    ComboData = Nothing
                End If
            Next
        End If
        Me.UseWaitCursor = False
    End Sub

    Private Sub frmMaintenance_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdMaintenance.Width = Me.Width - 25
        grdMaintenance.Height = Me.Height - 65
    End Sub

    Private Sub AfterEdit(ByVal dsEdit As DataSet)
        If Not IsNothing(dsEdit) Then
            'Try
            b_dataChanged = True
            If tableName = "SOURCINGRATES" Then
                str_Seasons = str_Seasons.Replace(dsEdit.Tables(0).Rows(0).Item("Season") & ", ", "") & dsEdit.Tables(0).Rows(0).Item("Season") & ", "
            End If
            MaintainAdapter.Update(dsEdit)
            MaintainData.AcceptChanges()
            MaintainAdapter.Update(MaintainData, "table")
            dsEdit = Nothing
            'Catch
            'If Err.Number = 5 Then
            'MsgBox("This supplier code is already in the list", MsgBoxStyle.Exclamation, "Error!")
            'Else
            'MsgBox(Err.Number & " - " & Err.Description, MsgBoxStyle.Critical, "Error!")
            'End If
            'End Try
        End If
    End Sub

    Private Sub calculateTable_Old(ByVal tableName As String)
        Dim SelectData As New DataSet
        Dim i_counter As Integer
        Dim str_ValueSQL, str_UpdateNewGrid As String

        Dim SelectDataMain As New DataSet
        Dim i_row As Integer
        Dim MyId As String

        SelectDataMain = getSelectDataSet("SELECT * FROM " & tableName)
        For i_row = 0 To SelectDataMain.Tables(0).Rows.Count - 1
            MyId = SelectDataMain.Tables(0).Rows(i_row).Item("DevNo")
            Dim str_updateSQL As String = "UPDATE " & tableName & " SET "
            'If MyId.ToString = "3855" Then
            ' Debug.WriteLine("yep")
            ' End If
            str_UpdateNewGrid = "UPDATE dbo.NewGrid SET ProtoFob5=SalesYouth.ProtoFob5, ProtoFob4=SalesYouth.ProtoFob4, " & _
                 "ProtoFob3=SalesYouth.ProtoFob3, ProtoFob2=SalesYouth.ProtoFob2, ProtoFob1=SalesYouth.ProtoFob1, " & _
                 "Retail=SalesYouth.Retail, ProposedRetail = SalesYouth.ProposedRetail, FinalFob = SalesYouth.FinalFob " & _
                 "FROM dbo.NewGrid, dbo.SalesYouth  " & _
                 "WHERE NewGrid.devNo = " & MyId & " And SalesYouth.Mother = NewGrid.devNo And SalesYouth.ToJBA = 1"
            If tableName = "NewGrid" Then executeSQL(str_UpdateNewGrid)
            str_ValueSQL = "NULL"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Season")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Currency")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FinalFob")) Then
                SelectData = getSelectDataSet("SELECT Season, Currency, Rate FROM NAPA.dbo.tbl_sourcing_Rates WHERE (Season = '" & SelectDataMain.Tables(0).Rows(i_row).Item("Season") & "') AND (Currency = '" & SelectDataMain.Tables(0).Rows(i_row).Item("Currency") & "' AND Coalition='DP')")
                If SelectData.Tables(0).Rows.Count > 0 Then
                    str_ValueSQL = Math.Round(SelectDataMain.Tables(0).Rows(i_row).Item("FinalFob") / SelectData.Tables(0).Rows(0).Item("Rate"), 2)
                End If
            End If
            str_updateSQL &= " FobInEuro = " & str_ValueSQL

            str_ValueSQL = "0.0"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("SourceLocation")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Season")) Then
                If SelectDataMain.Tables(0).Rows(i_row).Item("SourceLocation") = "AS" Then
                    SelectData = getSelectDataSet("SELECT SourcingAsia FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & SelectDataMain.Tables(0).Rows(i_row).Item("Season") & "')")
                    If SelectData.Tables(0).Rows.Count > 0 Then
                        str_ValueSQL = Math.Round((SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro") + SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) * SelectData.Tables(0).Rows(0).Item("SourcingAsia"), 2)
                    End If
                End If
            End If
            str_updateSQL &= ", SourcingAsia = " & str_ValueSQL

            Dim str_SelectFields() As String = {"Freight", "AirFreight"}
            For i_counter = 0 To str_SelectFields.Length - 1
                str_ValueSQL = "0.0"
                If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Major")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Season")) Then
                    If SelectDataMain.Tables(0).Rows(i_row).Item("Major") <> "BS" Then
                        SelectData = getSelectDataSet("SELECT " & str_SelectFields(i_counter) & " FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & SelectDataMain.Tables(0).Rows(i_row).Item("Season") & "')")
                        If SelectData.Tables(0).Rows.Count > 0 Then
                            str_ValueSQL = Math.Round((SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro") + SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) * SelectData.Tables(0).Rows(0).Item(str_SelectFields(i_counter)), 2)
                        End If
                    End If
                End If
                str_updateSQL &= ", " & str_SelectFields(i_counter) & " = " & str_ValueSQL
            Next

            str_ValueSQL = "0.0"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Duty")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Freight")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight")) Then
                str_ValueSQL = Math.Round((SelectDataMain.Tables(0).Rows(i_row).Item("LavMat") + SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro") + SelectDataMain.Tables(0).Rows(i_row).Item("Freight") + SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight")) * SelectDataMain.Tables(0).Rows(i_row).Item("Duty") / 100, 2)
            End If
            str_updateSQL &= ",  Duty2 = " & str_ValueSQL

            '{DatabaseColumnName, GridColumnName}
            Dim str_SelectFields2(,) As String = {{"Operations", "Operations"}, {"Planning", "Planning"}, {"SourcingEurope", "SourcingEurope"}, {"QualityControl", "QualityControl"}, {"Reserve", "Reserve"}, {"TurinBuilding", "TurinBuilding"}, {"TrimWarehouse", "TurinWarehouse"}, {"Turin_RMSourcing", "Turin_RMSourcing"}}
            For i_counter = 0 To (str_SelectFields2.Length / 2) - 1
                str_ValueSQL = "0.0"
                If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("SourcingAsia")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Season")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Freight")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Duty2")) Then
                    SelectData = getSelectDataSet("SELECT " & str_SelectFields2(i_counter, 0) & " FROM NAPA.dbo.Param_CostBuckets WHERE (Coalition = 'DP') AND (Season = '" & SelectDataMain.Tables(0).Rows(i_row).Item("Season") & "')")
                    If SelectData.Tables(0).Rows.Count > 0 Then
                        str_ValueSQL = Math.Round((SelectDataMain.Tables(0).Rows(i_row).Item("LavMat") + SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro") + SelectDataMain.Tables(0).Rows(i_row).Item("SourcingAsia") + SelectDataMain.Tables(0).Rows(i_row).Item("Freight") + SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight") + SelectDataMain.Tables(0).Rows(i_row).Item("Duty2")) * SelectData.Tables(0).Rows(0).Item(str_SelectFields2(i_counter, 0)), 2)
                    End If
                End If
                str_updateSQL &= ", " & str_SelectFields2(i_counter, 1) & " = " & str_ValueSQL
            Next

            str_ValueSQL = "NULL"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("SourcingAsia")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Freight")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Duty2")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("SourcingUS")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Operations")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Planning")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("SourcingEurope")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("QualityControl")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed2")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Reserve")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("TurinBuilding")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("TurinWarehouse")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed3")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("Turin_RMSourcing")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed4")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")) Then
                str_ValueSQL = SelectDataMain.Tables(0).Rows(i_row).Item("SourcingAsia") + SelectDataMain.Tables(0).Rows(i_row).Item("Freight") + SelectDataMain.Tables(0).Rows(i_row).Item("AirFreight") + SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed") + SelectDataMain.Tables(0).Rows(i_row).Item("Duty2") + SelectDataMain.Tables(0).Rows(i_row).Item("SourcingUS") + SelectDataMain.Tables(0).Rows(i_row).Item("Operations") + SelectDataMain.Tables(0).Rows(i_row).Item("Planning") + SelectDataMain.Tables(0).Rows(i_row).Item("SourcingEurope") + SelectDataMain.Tables(0).Rows(i_row).Item("QualityControl") + SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed2") + SelectDataMain.Tables(0).Rows(i_row).Item("Reserve") + SelectDataMain.Tables(0).Rows(i_row).Item("TurinBuilding") + SelectDataMain.Tables(0).Rows(i_row).Item("TurinWarehouse") + SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed3") + SelectDataMain.Tables(0).Rows(i_row).Item("Turin_RMSourcing") + SelectDataMain.Tables(0).Rows(i_row).Item("NotUsed4") + SelectDataMain.Tables(0).Rows(i_row).Item("FobInEuro") + SelectDataMain.Tables(0).Rows(i_row).Item("LavMat")
            End If
            str_updateSQL &= ", StandardCost = " & str_ValueSQL

            str_ValueSQL = "NULL"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("ProposedRetail")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("MarkUp")) Then
                If SelectDataMain.Tables(0).Rows(i_row).Item("MarkUp") <> 0 Then
                    str_ValueSQL = Math.Floor(SelectDataMain.Tables(0).Rows(i_row).Item("ProposedRetail") / SelectDataMain.Tables(0).Rows(i_row).Item("MarkUp"))
                End If
            End If
            str_updateSQL &= ", WholeSale = " & str_ValueSQL

            str_ValueSQL = "NULL"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("StandardCost")) Then
                If Not SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale") = 0 Then
                    str_ValueSQL = (SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale") - SelectDataMain.Tables(0).Rows(i_row).Item("StandardCost")) / SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale")
                End If
            End If
            str_updateSQL &= ", GrossMargin = " & str_ValueSQL

            str_ValueSQL = "NULL"
            If Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale")) And Not IsDBNull(SelectDataMain.Tables(0).Rows(i_row).Item("ActualSales")) Then
                str_ValueSQL = (SelectDataMain.Tables(0).Rows(i_row).Item("WholeSale") * SelectDataMain.Tables(0).Rows(i_row).Item("ActualSales"))
            End If
            executeSQL(str_updateSQL & ", ActualsSales = " & str_ValueSQL & " where DevNo = " & MyId)
            If tableName = "SalesYouth" Then executeSQL(str_UpdateNewGrid)
        Next
    End Sub

    Public Sub calculateTable(ByVal tableName As String, Optional ByVal devNo As String = "")
        Dim str_UpdateNewGrid As String = "UPDATE dbo.NewGrid SET ProtoFob5=SalesYouth.ProtoFob5, ProtoFob4=SalesYouth.ProtoFob4, " & _
             "ProtoFob3=SalesYouth.ProtoFob3, ProtoFob2=SalesYouth.ProtoFob2, ProtoFob1=SalesYouth.ProtoFob1, " & _
             "Retail=SalesYouth.Retail, ProposedRetail = SalesYouth.ProposedRetail, FinalFob = SalesYouth.FinalFob " & _
             "FROM dbo.NewGrid, dbo.SalesYouth  " & _
             "WHERE  SalesYouth.Mother = NewGrid.devNo And SalesYouth.ToJBA = 1"
        Dim str_whereBrand = "ng.Brand"
        If tableName = "NewGrid" Then
            executeSQL(str_UpdateNewGrid)
        Else
            str_whereBrand = "(SELECT Brand FROM NewGrid WHERE Devno=ng.Mother)"
        End If

        executeSQL("UPDATE " & tableName & " SET " &
                "  FobInEuro =ISNULL(ROUND(FinalFob/(SELECT sr.Rate FROM NAPA.dbo.tbl_sourcing_Rates as sr WHERE (sr.Season = (SELECT ISNULL(RootSeason,Season) FROM dbo.param_season WHERE Season=ng.Season)) AND sr.Currency = ng.Currency AND sr.Coalition='DP'),2),NULL) " &
                "FROM " & tableName & " as ng")

        executeSQL("UPDATE " & tableName & " SET " &
                "   SourcingAsia = CASE WHEN SourceLocation = 'AS' THEN ISNULL(ROUND((ng.FobInEuro + ng.LavMat)*(SELECT SourcingAsia FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) ELSE 0.0 END " &
                ",	Freight = CASE WHEN NOT Major = 'BS' THEN ISNULL(ROUND((ng.FobInEuro + ng.LavMat)*(SELECT Freight FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) ELSE 0.0 END " &
                ",	AirFreight = CASE WHEN NOT Major = 'BS' THEN ISNULL(ROUND((ng.FobInEuro + ng.LavMat)*(SELECT AirFreight FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) ELSE 0.0 END " &
                "FROM " & tableName & " as ng")

        executeSQL("UPDATE " & tableName & " SET " & _
                "   Duty2 = ISNULL(ROUND((FobInEuro + LavMat + Freight + AirFreight) * Duty / 100, 2), 0.0)")

        executeSQL("UPDATE " & tableName & " SET " &
                 "   Operations= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT Operations FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	Planning= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT Planning FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	SourcingEurope= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT SourcingEurope FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	QualityControl= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT QualityControl FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	Reserve= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT Reserve FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	TurinBuilding= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT TurinBuilding FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	TurinWarehouse= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT TrimWarehouse FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 ",	Turin_RMSourcing= ISNULL(ROUND((ng.FobInEuro + ng.LavMat + SourcingAsia + Freight + AirFreight + Duty2) * (SELECT Turin_RMSourcing FROM NAPA.dbo.Param_CostBuckets WHERE Coalition = 'DP' AND Season = ng.Season AND Brand=" & str_whereBrand & "),2),0.0) " &
                 "FROM " & tableName & " as ng")

        executeSQL("UPDATE " & tableName & " SET " & _
                 "  StandardCost = ISNULL(ROUND((FobInEuro + LavMat + SourcingAsia + Freight + AirFreight + Duty2 + NotUsed + SourcingUS + Operations + Planning + SourcingEurope + QualityControl + NotUsed2 + Reserve + TurinBuilding + TurinWarehouse + NotUsed3 + Turin_RMSourcing + NotUsed4), 2), NULL) " & _
                 ",	WholeSale = CASE WHEN NOT MarkUp = 0 THEN ISNULL(FLOOR(ProposedRetail/MarkUp),NULL) ELSE NULL END")

        executeSQL("UPDATE " & tableName & " SET " & _
                "   GrossMargin = CASE WHEN NOT WholeSale = 0 THEN ISNULL((WholeSale-StandardCost)/WholeSale,NULL) ELSE NULL END " & _
                ",	ActualsSales = WholeSale * ActualSales")

        If tableName = "SalesYouth" Then executeSQL(str_UpdateNewGrid)
    End Sub

    
    Private Sub grdMaintenance_OnAddNew(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaintenance.OnAddNew
        Select Case UCase(tableName)
            Case "SOURCINGRATES"
                grdMaintenance.Item(grdMaintenance.RowCount - 1, "Coalition") = "DP"
        End Select
    End Sub

    Private Sub addSupplierDescription(ByVal rowID As Integer)
        Dim searchData As DataSet = getSelectDataSet("SELECT SUPN05, SNAM05 FROM vfmobilexpense.dbo.PLP05 WHERE SUPN05='" & grdMaintenance.Item(rowID, "SupplierCode") & "'")
        If searchData.Tables(0).Rows.Count > 0 Then
            grdMaintenance.Item(rowID, "SupplierDescription") = searchData.Tables(0).Rows(0).Item("SNAM05")
            executeSQL("UPDATE tbl_ApprovedSuppliers SET SupplierDescription='" & searchData.Tables(0).Rows(0).Item("SNAM05") & "' WHERE SupplierCode ='" & grdMaintenance.Item(rowID, "SupplierCode") & "'")
        Else
            MsgBox("Supplier code was not found in approved supplier table", MsgBoxStyle.Critical, "Supplier not found!")
        End If
    End Sub


End Class