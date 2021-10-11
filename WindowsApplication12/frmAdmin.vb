Imports System.Data.SqlClient

Public Class frmAdmin
    Dim adminAdapter As New SqlDataAdapter
    Dim adminData As New DataSet

    Private Sub grdAdmin_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAdmin.AfterDelete
        saveGridChanges(adminData.GetChanges(DataRowState.Deleted))
    End Sub

    Private Sub grdAdmin_AfterInsert(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAdmin.AfterInsert
        saveGridChanges(adminData.GetChanges(DataRowState.Added))
    End Sub

    Private Sub grdAdmin_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAdmin.AfterUpdate
        saveGridChanges(adminData.GetChanges(DataRowState.Modified))
    End Sub

    Private Sub frmAdmin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveGridLayout(Me, grdAdmin)
    End Sub

    Private Sub frmAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer

        adminData.Clear()
        adminAdapter.SelectCommand = dcNAPA.CreateCommand
        adminAdapter.SelectCommand.CommandText = "SELECT * From param_users"
        cmdBuilder = New SqlCommandBuilder(adminAdapter)
        adminAdapter.Fill(adminData)

        grdAdmin.DataSource = adminData
        grdAdmin.DataMember = adminData.Tables(0).ToString
        grdAdmin.Rebind(True)

        grdAdmin.Splits(0).Name = "Split0"
        grdAdmin.Splits(1).Name = "Split1"
        LoadGridLayout(Me, grdAdmin, "param_users", False)
        For i = 1 To grdAdmin.Columns.Count - 1
            grdAdmin.Splits(0).DisplayColumns(i).Visible = False
        Next
        grdAdmin.Splits(1).DisplayColumns(0).Visible = False
        grdAdmin.Splits(0).SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact
        grdAdmin.Splits(0).SplitSize = 120
    End Sub

    Private Sub saveGridChanges(ByVal dsChanged As DataSet)
        If Not IsNothing(dsChanged) Then
            adminAdapter.Update(dsChanged)
            adminData.AcceptChanges()
            adminAdapter.Update(adminData)
        End If
    End Sub

    Private Sub grdAdmin_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAdmin.MouseDown
        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdAdmin.FocusedSplit.DisplayColumns(Me.grdAdmin.Col).DataColumn
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Select Case col.DataField
                Case "UserNaam"
                    grdAdmin.ContextMenuStrip = New ContextMenuStrip
                    grdAdmin.ContextMenuStrip.Items.Add("Clear Grid Layout", Nothing, New EventHandler(AddressOf grdAdmin_mnuClick)).Tag = grdAdmin.Row
            End Select
        End If
    End Sub

    Private Sub grdAdmin_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        Select Case sender.Text
            Case "Clear Grid Layout"
                If MsgBox("This will clear the selected user's grid layout." & vbCrLf & "Do you want to continue?", vbYesNo, "Clear Grid Layout") = MsgBoxResult.Yes Then
                    executeSQL("DELETE FROM " & GridUserTable & " WHERE [userID] = '" & grdAdmin.Item(sender.Tag, "UserNaam").ToString & "' AND [Brand] = 'N'")
                End If
        End Select
    End Sub
End Class