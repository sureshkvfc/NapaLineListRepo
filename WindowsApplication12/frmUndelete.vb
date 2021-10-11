Imports System.Data.SqlClient
Public Class frmUndelete
    Dim UndeleteAdapter As New SqlDataAdapter
    Dim UndeleteData As New DataSet

    Private Sub frmUndelete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ComboAdapter As New SqlDataAdapter
        Dim i As Integer
        Dim j As Integer
        Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection

        UndeleteAdapter.SelectCommand = dcNAPA.CreateCommand
        UndeleteAdapter.SelectCommand.CommandText = "SELECT * From NewGrid WHERE IsDeleted = 1 order by DevNo Desc"
        UndeleteAdapter.Fill(UndeleteData)
        grdUnDeleteRecords.DataSource = UndeleteData
        grdUnDeleteRecords.DataMember = UndeleteData.Tables(0).ToString
        grdUnDeleteRecords.Rebind(True)
        'LayoutAdapter.SelectCommand = dcNAPA.CreateCommand
        'LayoutAdapter.SelectCommand.CommandText = "SELECT * From GridLayout"
        'LayoutAdapter.Fill(LayoutData)
        LayoutData = getSelectDataSet("SELECT * From GridLayout")
        Dim foundRows() As Data.DataRow
        For i = 0 To frmMainGrid.grdMain.Columns.Count - 1
            foundRows = LayoutData.Tables(0).Select("ColumnName = '" & grdUnDeleteRecords.Columns(i).DataField & "'")
            If foundRows IsNot Nothing Then
                grdUnDeleteRecords.Columns(i).Caption = foundRows(0).ItemArray(1).ToString()
            End If
        Next
        foundRows = Nothing
        ComboAdapter.SelectCommand = dcNAPA.CreateCommand

        For i = 0 To grdUnDeleteRecords.Columns.Count - 1
            foundRows = LayoutData.Tables(0).Select("ColumnName = '" & grdUnDeleteRecords.Columns(i).DataField & "' And HasDropDown = True")
            If foundRows.Length > 0 Then
                Dim ComboData As New DataSet
                ComboAdapter.SelectCommand.CommandText = foundRows(0).ItemArray(3)
                ComboAdapter.Fill(ComboData)
                ComboData.CreateDataReader()
                v = grdUnDeleteRecords.Columns(foundRows(0).ItemArray(0)).ValueItems.Values
                For j = 0 To ComboData.Tables(0).Rows.Count - 1
                    v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(5)), ComboData.Tables(0).Rows(j).Item(foundRows(0).ItemArray(4))))
                Next
                grdUnDeleteRecords.Columns(foundRows(0).ItemArray(0)).ValueItems.Translate = True
                grdUnDeleteRecords.Columns(i).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                grdUnDeleteRecords.Columns(i).FilterDropdown = True
                grdUnDeleteRecords.Splits(0).DisplayColumns(i).DropDownList = True
                ComboData = Nothing
            End If
        Next
        grdUnDeleteRecords.Splits(0).DisplayColumns("IsDeleted").Visible = False

    End Sub

    Private Sub frmUndelete_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Button2.Top = Me.Height - Button2.Height - 50
        grdUnDeleteRecords.Width = Me.Width - 25
        grdUnDeleteRecords.Height = Me.Height - 65 - Button2.Height - 25
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim lstSorted As New ArrayList
        Dim sql As String
        Dim adapter As New SqlDataAdapter
        Dim myid As Long
        Dim i As Integer

        If grdUnDeleteRecords.SelectedRows.Count > 0 Then
            msg = "Are you sure to UnDelete this records?"
            style = MsgBoxStyle.DefaultButton2 Or _
               MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
            title = "Attention"
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.No Then
                Exit Sub
            Else
                For Each row As Object In grdUnDeleteRecords.SelectedRows
                    lstSorted.Add(CType(row, Integer))
                Next
                lstSorted.Sort()
                For intIndex As Integer = lstSorted.Count - 1 To 0 Step -1
                    grdUnDeleteRecords.Row = lstSorted.Item(intIndex)
                    myid = grdUnDeleteRecords.Item(grdUnDeleteRecords.Row, 0)
                    sql = "Update NewGrid set IsDeleted = 0 where DevNo = " & myid
                    Try
                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                        adapter.InsertCommand = dcNAPA.CreateCommand
                        adapter.InsertCommand.CommandText = sql
                        adapter.InsertCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    grdUnDeleteRecords.Delete()
                Next
                For i = 0 To frmMainGrid.grdMain.RowCount - 1
                    frmMainGrid.grdMain.Delete(0)
                Next
                GridAdapter.SelectCommand = dcNAPA.CreateCommand
                GridAdapter.SelectCommand.CommandText = "SELECT * From NewGrid WHERE (IsDeleted IS NULL) OR (IsDeleted = 0) order by DevNo Desc"
                cmdBuilder = New SqlCommandBuilder(GridAdapter)

                GridAdapter.Fill(GridData)
                frmMainGrid.grdMain.DataSource = GridData
                frmMainGrid.grdMain.DataMember = GridData.Tables(0).ToString
                frmMainGrid.grdMain.Rebind(True)
            End If
        End If
    End Sub
End Class