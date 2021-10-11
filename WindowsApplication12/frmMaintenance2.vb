Imports System.Data.SqlClient
Public Class frmMaintenance2
    Public tableName As String = ""
    Public tableQuery As String = ""
    Public DoCalculation As Boolean = False
    Public LockColumnName As Boolean = False
    Private b_dataChanged As Boolean = False
    Private str_Seasons As String = ""


    Public Sub btnRefresh()
        Dim odt As DataTable
        odt = getSelectDataSet("SELECT * From param_measurementTemplate").Tables(0)
        grdMeasurementTemplate.DataSource = odt
        grdMeasurementTemplate.DataMember = odt.ToString
        grdMeasurementTemplate.Rebind(True)
        grdMeasurementTemplate.Visible = True
        'grdMeasurementTemplate_RowColChange(Nothing, Nothing)
        LayoutData.Clear()
        LayoutData = getSelectDataSet("SELECT * From GridLayout WHERE gridName = 'grdMeasurementTemplate' ORDER BY Split")
        setDisplayColumns(grdMeasurementTemplate)
    End Sub

    Private Sub grdMaintenance_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMeasurementTemplate.AfterDelete
        AfterEdit(MaintainData.GetChanges(DataRowState.Deleted))
    End Sub

    Private Sub grdMaintenance_AfterInsert(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMeasurementTemplate.AfterInsert
        AfterEdit(MaintainData.GetChanges(DataRowState.Added))
    End Sub

    Private Sub grdMaintenance_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMeasurementTemplate.AfterUpdate
        AfterEdit(MaintainData.GetChanges(DataRowState.Modified))
    End Sub

    Private Sub frmMaintenance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        Me.UseWaitCursor = True
        If DoCalculate = True And b_dataChanged = True Then
            Debug.WriteLine(str_Seasons)
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
                        v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(5)), ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(4))))
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

    Private Sub frmMaintenance2_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        btnRefresh()
    End Sub

    Private Sub frmMaintenance_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        grdMeasurementTemplate.Width = Me.Width - 25
        grdMeasurementTemplate.Height = Me.Height - 65
    End Sub

    Private Sub AfterEdit(ByVal dsEdit As DataSet)
        If Not IsNothing(dsEdit) Then
            b_dataChanged = True
            MaintainAdapter.Update(dsEdit)
            MaintainData.AcceptChanges()
            MaintainAdapter.Update(MaintainData, "table")
            dsEdit = Nothing
        End If
    End Sub

    Private Sub grdMeasurementTemplate_RowColChange(sender As System.Object, e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles grdMeasurementTemplate.RowColChange

    End Sub
End Class