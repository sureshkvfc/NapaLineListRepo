Imports System.Data.SqlClient
Public Class frmUnFreeze
    Private UnfreezeAdapter As New SqlDataAdapter
    Private UnfreezeData As New DataSet
    Private UnfreezeBuilder As SqlCommandBuilder

    Private Sub frmUnFreeze_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sql As String

        UnfreezeAdapter.SelectCommand = dcNAPA.CreateCommand
        sql = "SELECT DevNo, JbaNo, Name, Season, Freeze From NewGrid where Freeze = 1"
        UnfreezeAdapter.SelectCommand.CommandText = sql
        UnfreezeBuilder = New SqlCommandBuilder(UnfreezeAdapter)
        UnfreezeAdapter.Fill(UnfreezeData)
        grdUnFreeze.DataSource = Nothing

        grdUnFreeze.DataSource = UnfreezeData
        grdUnFreeze.DataMember = UnfreezeData.Tables(0).ToString
        grdUnFreeze.Rebind(True)
        grdUnFreeze.Splits(0).DisplayColumns("DevNo").Locked = True
        grdUnFreeze.Splits(0).DisplayColumns("JbaNo").Locked = True
        grdUnFreeze.Splits(0).DisplayColumns("Name").Locked = True
        grdUnFreeze.Splits(0).DisplayColumns("Season").Locked = True
    End Sub

    Private Sub frmUnFreeze_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdUnFreeze.Width = Me.Width - 25
        grdUnFreeze.Height = Me.Height - 65
    End Sub

    Private Sub grdMaintenance_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUnFreeze.AfterUpdate
        Dim dsUpdated As DataSet
        dsUpdated = UnfreezeData.GetChanges(DataRowState.Modified)
        If Not IsNothing(dsUpdated) Then
            UnfreezeAdapter.Update(dsUpdated)
            UnfreezeData.AcceptChanges()
            UnfreezeAdapter.Update(UnfreezeData, "table")
        End If
    End Sub

End Class