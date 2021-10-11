Public Class frmPickColors
    Private Sub frmPickColors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer

        grdMinors.DataSource = Nothing
        grdMinors.DataSource = getSelectDataSet("SELECT * From vw_Param_Minor_Colors order by Minor").Tables(0)
        For i = 0 To grdMinors.Splits(0).DisplayColumns.Count - 1
            grdMinors.Splits(0).DisplayColumns(i).Visible = False
        Next
        grdMinors.Splits(0).DisplayColumns("Minor").Visible = True
        grdMinors.Splits(0).DisplayColumns("Minor").Width = grdMinors.Width - 40
        grdMinors.Splits(0).DisplayColumns("Minor").Locked = True
        grdMinors.Splits(0).DisplayColumns("Minor").FetchStyle = True
    End Sub

    Private Sub frmPickColors_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdMinors.Height = Me.Height - (grdMinors.Top * 2) - 30
    End Sub

    Private Sub grdMinors_FetchRowStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs) Handles grdMinors.FetchRowStyle
        If IsDBNull(grdMinors.Item(e.Row, "Red")) Then grdMinors.Item(e.Row, "Red") = 0
        If IsDBNull(grdMinors.Item(e.Row, "Green")) Then grdMinors.Item(e.Row, "Green") = 0
        If IsDBNull(grdMinors.Item(e.Row, "Blue")) Then grdMinors.Item(e.Row, "Blue") = 0

        If IsDBNull(grdMinors.Item(e.Row, "ForRed")) Then grdMinors.Item(e.Row, "ForRed") = 255
        If IsDBNull(grdMinors.Item(e.Row, "ForGreen")) Then grdMinors.Item(e.Row, "ForGreen") = 255
        If IsDBNull(grdMinors.Item(e.Row, "ForBlue")) Then grdMinors.Item(e.Row, "ForBlue") = 255

        e.CellStyle.BackColor = Color.FromArgb(255, grdMinors.Item(e.Row, "Red"), grdMinors.Item(e.Row, "Green"), grdMinors.Item(e.Row, "Blue"))
        e.CellStyle.ForeColor = Color.FromArgb(255, grdMinors.Item(e.Row, "ForRed"), grdMinors.Item(e.Row, "ForGreen"), grdMinors.Item(e.Row, "ForBlue"))
    End Sub

    Private Sub grdMinors_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMinors.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            grdMinors.ContextMenuStrip = New ContextMenuStrip
            grdMinors.ContextMenuStrip.Items.Add("Change Forecolor", Nothing, New EventHandler(AddressOf grdMinors_mnuClick)).Tag = grdMinors.Item(grdMinors.Row, "Code")
            grdMinors.ContextMenuStrip.Items.Add("Change BackColor", Nothing, New EventHandler(AddressOf grdMinors_mnuClick)).Tag = grdMinors.Item(grdMinors.Row, "Code")
        End If
    End Sub

    Private Sub grdMinors_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        Select Case sender.Text
            Case "Change Forecolor"
                clrDialog.Color = Color.FromArgb(255, grdMinors.Item(grdMinors.Row, "ForRed"), grdMinors.Item(grdMinors.Row, "ForGreen"), grdMinors.Item(grdMinors.Row, "ForBlue"))
                If clrDialog.ShowDialog = DialogResult.OK Then
                    executeSQL("UPDATE Param_Minor_Colors SET ForRed=" & clrDialog.Color.R.ToString & _
                                ", ForGreen=" & clrDialog.Color.G.ToString & _
                                ", ForBlue= " & clrDialog.Color.B.ToString & " WHERE Code='" & sender.Tag & "'")
                    grdMinors.Item(grdMinors.Row, "ForRed") = clrDialog.Color.R
                    grdMinors.Item(grdMinors.Row, "ForGreen") = clrDialog.Color.G
                    grdMinors.Item(grdMinors.Row, "ForBlue") = clrDialog.Color.B
                    grdMinors.RefreshRow()
                End If
            Case "Change BackColor"
                clrDialog.Color = Color.FromArgb(255, grdMinors.Item(grdMinors.Row, "Red"), grdMinors.Item(grdMinors.Row, "Green"), grdMinors.Item(grdMinors.Row, "Blue"))
                If clrDialog.ShowDialog = DialogResult.OK Then
                    executeSQL("UPDATE Param_Minor_Colors SET Red=" & clrDialog.Color.R.ToString & _
                                ", Green=" & clrDialog.Color.G.ToString & _
                                ", Blue= " & clrDialog.Color.B.ToString & " WHERE Code='" & sender.Tag & "'")
                    grdMinors.Item(grdMinors.Row, "Red") = clrDialog.Color.R
                    grdMinors.Item(grdMinors.Row, "Green") = clrDialog.Color.G
                    grdMinors.Item(grdMinors.Row, "Blue") = clrDialog.Color.B
                End If
        End Select
    End Sub
End Class