Public Class frmReplaceColor
    Private updateData As New DataSet
    Private str_sql_columns As String = "SELECT "

    Private Sub frmReplaceColor_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMainGrid.btnRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub frmReplaceColor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer

        cmbColorToReplace.DataSource = Nothing
        cmbColorToReplaceTo.DataSource = Nothing

        ColorData.Clear()
        ColorData = getSelectDataSet("select * from tbl_colortable WHERE Brand = 'N'")
        For i = 0 To ColorData.Tables(0).Rows.Count - 1
            cmbColorToReplace.Items.Add(ColorData.Tables(0).Rows(i).Item("Color") & " " & ColorData.Tables(0).Rows(i).Item("Description"))
            cmbColorToReplaceTo.Items.Add(ColorData.Tables(0).Rows(i).Item("Color") & " " & ColorData.Tables(0).Rows(i).Item("Description"))
        Next

        grdColors.DataSource = Nothing

        Dim columnsData As DataSet = getSelectDataSet("SELECT ColumnName FROM gridLayout WHERE split = 0 AND Invisible=0")

        For i = 0 To columnsData.Tables(0).Rows.Count - 1
            str_sql_columns &= columnsData.Tables(0).Rows(i).Item("columnName") & ", "
        Next
        For i = 1 To i_ColorCount
            str_sql_columns &= "Color" & i & ", "
        Next
        str_sql_columns = str_sql_columns.Substring(0, str_sql_columns.Length - 2) & " FROM NewGrid WHERE Name in (SELECT StyleNaam FROM tmp_NewGridAfterFilter WHERE userNaam ='" & UserId & "')"
    End Sub

    Private Sub frmReplaceColor_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdColors.Height = Me.Height - grdColors.Top - 40
        grdColors.Width = Me.Width - 25
    End Sub

    Private Sub cmbColorToReplace_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColorToReplace.SelectedIndexChanged
        Dim i As Integer
        Dim str_color As String = cmbColorToReplace.SelectedItem.ToString()
        Dim str_dataset As String = ""

        If Not IsNothing(cmbColorToReplace.SelectedItem) Then
            str_dataset &= " AND ("
            For i = 1 To i_ColorCount
                str_dataset &= "Color" & i & " = '" & cmbColorToReplace.SelectedItem.ToString() & "' OR "
            Next
            str_dataset = str_dataset.Substring(0, str_dataset.Length - 4) & ")"
        End If
        'Debug.WriteLine(str_sql_columns)
        grdColors.DataSource = Nothing
        grdColors.ClearFields()
        updateData.Clear()
        updateData = getSelectDataSet(str_sql_columns & str_dataset)
        grdColors.DataSource = updateData
        grdColors.DataMember = updateData.Tables(0).ToString
        grdColors.Rebind(True)

        grdColors.Splits(0).DisplayColumns.Item("Freeze").Visible = False
    End Sub

    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click
        Dim i As Integer
        Dim str_update As String = "UPDATE NewGrid SET "
        Dim updateFrom() As String
        Dim updateTo() As String

        If Not IsNothing(cmbColorToReplace.SelectedItem) And Not IsNothing(cmbColorToReplaceTo.SelectedItem) Then
            updateFrom = Split(cmbColorToReplace.SelectedItem.ToString(), " ")
            updateTo = Split(cmbColorToReplaceTo.SelectedItem.ToString(), " ")
            For i = 1 To i_smsColorCount
                str_update &= "smsColor" & i & " = CASE WHEN smsColor" & i & " = '" & updateFrom(0) & "' THEN '" & updateTo(0) & "' ELSE smsColor" & i & " END, "
                str_update &= "prodColor" & IIf(i < 10, "0" & i, i) & " = CASE WHEN prodColor" & IIf(i < 10, "0" & i, i) & " = '" & updateFrom(0) & "' THEN '" & updateTo(0) & "' ELSE prodColor" & IIf(i < 10, "0" & i, i) & " END, "
                str_update &= "Color" & i & " = CASE WHEN Color" & i & " = '" & cmbColorToReplace.SelectedItem.ToString() & "' THEN '" & cmbColorToReplaceTo.SelectedItem.ToString() & "' ELSE Color" & i & " END, "
            Next
            For i = i_smsColorCount + 1 To i_ColorCount
                str_update &= "prodColor" & IIf(i < 10, "0" & i, i) & " = CASE WHEN prodColor" & IIf(i < 10, "0" & i, i) & " = '" & updateFrom(0) & "' THEN '" & updateTo(0) & "' ELSE prodColor" & IIf(i < 10, "0" & i, i) & " END, "
                str_update &= "Color" & i & " = CASE WHEN Color" & i & " = '" & cmbColorToReplace.SelectedItem.ToString() & "' THEN '" & cmbColorToReplaceTo.SelectedItem.ToString() & "' ELSE Color" & i & " END, "
            Next
            str_update = str_update.Substring(0, str_update.Length - 2) & " WHERE DevNo ="

            For i = 0 To grdColors.RowCount - 1
                executeSQL(str_update & grdColors.Item(i, "DevNo"))
            Next
        End If
    End Sub
End Class