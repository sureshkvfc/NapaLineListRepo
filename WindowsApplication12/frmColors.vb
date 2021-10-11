Imports System.Data.SqlClient
Public Class frmColors

    Private Sub frmColors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdColors.DataSource = Nothing
        grdColors.ClearFields()
        ColorData.Clear()
        ColorAdapter.SelectCommand = dcNAPA.CreateCommand
        ColorAdapter.SelectCommand.CommandText = "select * from tbl_colortable" '### Commented 11/8/18 to River Sand Integration 1 - Removed
        'ColorAdapter.SelectCommand.CommandText = "SELECT Brand, Color, Description, SeasonsUsed, SeasonsSamples, SpecialColor FROM tbl_ColorTable" '### Commented 11/8/18 to River Sand Integration 1 - Added
        ColorBuilder = New SqlCommandBuilder(ColorAdapter)
        ColorAdapter.Fill(ColorData)
        grdColors.DataSource = ColorData
        grdColors.DataMember = ColorData.Tables(0).ToString
        grdColors.Rebind(True)
        grdColors.Splits(0).DisplayColumns("Color").Locked = True   '### Commented 11/8/18 to River Sand Integration 1 - Removed
        grdColors.Splits(0).DisplayColumns("Description").Locked = True '### Commented 11/8/18 to River Sand Integration 1 - Removed
    End Sub

    Private Sub grdColors_AfterInsert1(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdColors.AfterInsert
        Dim dsAdded As DataSet
        Dim adapter As New SqlDataAdapter
        Dim i As Integer
        Dim j As Integer
        Dim str As String
        Dim SearchString As String
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim IDString As Long
        Dim IDAdapter As New SqlDataAdapter
        Dim IDData As New DataSet
        Dim mySql As String
        Dim mySeason() As String
        Dim COLOURART As String
        Dim COLOURNO As String
        Dim COLOURTEXT As String
        Dim COLOURARTNO As String
        Dim COLNOTEXT As String
        Dim COLNODESC As String
        Dim Usql As String
        Dim IDsql As String
        dsAdded = ColorData.GetChanges(DataRowState.Added)
        If Not IsNothing(dsAdded) Then
            dsAdded.CreateDataReader()
            If dsAdded.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsAdded.Tables(0).Rows.Count - 1
                    If IsDBNull(dsAdded.Tables(0).Rows(i).Item("SeasonsUsed")) Then GoTo nextrecord

                    str = dsAdded.Tables(0).Rows(i).Item("SeasonsUsed")
                    mySeason = Split(str, ",")
                    For j = 0 To mySeason.GetUpperBound(0)
                        SearchString = dsAdded.Tables(0).Rows(i).Item("Color") & "N" & mySeason(j)
                        SearchAdapter.SelectCommand = dcNAPA.CreateCommand
                        mySql = "SELECT COLARTNO FROM QUESTPDMNAPA.dbo.INTCOLOR WHERE (COLARTNO = '" & SearchString & "')"
                        SearchData.Clear()
                        SearchAdapter.SelectCommand.CommandText = mySql
                        SearchAdapter.Fill(SearchData)
                        SearchData.CreateDataReader()
                        COLOURART = Trim(dsAdded.Tables(0).Rows(i).Item("Color"))
                        COLOURNO = "N" & mySeason(j)
                        COLOURTEXT = Trim(dsAdded.Tables(0).Rows(i).Item("Description"))
                        COLOURARTNO = Trim(dsAdded.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j)
                        COLNOTEXT = Trim(dsAdded.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j) & "-" & Trim(dsAdded.Tables(0).Rows(i).Item("Description")) & "-"
                        COLNODESC = Trim(dsAdded.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j) & "-" & Trim(dsAdded.Tables(0).Rows(i).Item("Description"))
                        If SearchData.Tables(0).Rows.Count > 0 Then
                            Usql = "Update QUESTPDMNAPA.dbo.INTCOLOR Set COLOURART = '" & COLOURART & "', " _
                            & "COLOURNO = '" & COLOURNO & "', " _
                            & "COLOURTEXT = '" & COLOURTEXT & "', " _
                            & "COLARTNO = '" & COLOURARTNO & "', " _
                            & "COLNOTEXT = '" & COLNOTEXT & "', " _
                            & "COLNODESC = '" & COLNODESC & "', " _
                            & "CHANGEINIT = 'SA', " _
                            & "CHANGEDATE = CONVERT(DATETIME," & Format(Now, "dd/mm/yyyy") & ", 102), " _
                            & "CHANGETIME = '" & Format(Now, "hh:mm tt") & "' "
                            If Not IsDBNull(dsAdded.Tables(0).Rows(i).Item("SeasonsUsed")) Then
                                If dsAdded.Tables(0).Rows(i).Item("SeasonsUsed") <> "" Then
                                    Usql = Usql & "LANGUAGE9 = '1', "
                                End If
                            End If
                            If Not IsDBNull(dsAdded.Tables(0).Rows(i).Item("SeasonsSamples")) Then
                                If dsAdded.Tables(0).Rows(i).Item("SeasonsSamples") <> "" Then
                                    Usql = Usql & "LANGUAGE10 = '1', "
                                End If
                            End If
                            Usql = Usql & "WHERE (COLARTNO = '" & SearchString & "')"
                            Try
                                If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                                adapter.UpdateCommand = dcNAPA.CreateCommand
                                adapter.UpdateCommand.CommandText = Usql
                                adapter.UpdateCommand.ExecuteNonQuery()
                            Catch ex As Exception
                                MsgBox(ex.ToString)
                            End Try
                        Else
                            '### Commented 11/8/18 to River Sand Integration 1 - Start
                            'IDAdapter.SelectCommand = dcNAPA.CreateCommand
                            'IDsql = "SELECT MAX(RECNO) AS LatestID FROM QUESTPDMNAPA.dbo.INTCOLOR"
                            'IDData.Clear()
                            'IDAdapter.SelectCommand.CommandText = IDsql
                            'IDAdapter.Fill(IDData)
                            'IDData.CreateDataReader()
                            'If IDData.Tables(0).Rows.Count > 0 Then
                            '    IDString = IDData.Tables(0).Rows(i).Item("LatestID") + 1
                            '    Usql = "Insert Into QUESTPDMNAPA.dbo.INTCOLOR (RECNO, COLOURART ,COLOURNO, COLOURTEXT, " _
                            '    & "COLARTNO, COLNOTEXT, COLNODESC, FIRSTINIT, FIRSTDATE, FIRSTTIME, LANGUAGE9, LANGUAGE10) Values(" _
                            '    & IDString & ", " _
                            '    & "'" & COLOURART & "', " _
                            '    & "'" & COLOURNO & "', " _
                            '    & "'" & COLOURTEXT & "', " _
                            '    & "'" & COLOURARTNO & "', " _
                            '    & "'" & COLNOTEXT & "', " _
                            '    & "'" & COLNODESC & "', " _
                            '    & "'SA', " _
                            '    & "CONVERT(DATETIME," & Format(Now, "dd/mm/yyyy") & ", 102), '" _
                            '    & Format(Now, "hh:mm tt") & "', "
                            '    If Not IsDBNull(dsAdded.Tables(0).Rows(i).Item("SeasonsUsed")) Then
                            '        If dsAdded.Tables(0).Rows(i).Item("SeasonsUsed") <> "" Then
                            '            Usql = Usql & "'1', "
                            '        Else
                            '            Usql = Usql & "NULL, "
                            '        End If
                            '    Else
                            '        Usql = Usql & "NULL, "
                            '    End If
                            '    If Not IsDBNull(dsAdded.Tables(0).Rows(i).Item("SeasonsSamples")) Then
                            '        If dsAdded.Tables(0).Rows(i).Item("SeasonsSamples") <> "" Then
                            '            Usql = Usql & "'1') "
                            '        Else
                            '            Usql = Usql & "NULL)"
                            '        End If
                            '    Else
                            '        Usql = Usql & "NULL)"
                            '    End If
                            '    Try
                            '        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                            '        adapter.InsertCommand = dcNAPA.CreateCommand
                            '        adapter.InsertCommand.CommandText = Usql
                            '        adapter.InsertCommand.ExecuteNonQuery()
                            '    Catch ex As Exception
                            '        MsgBox(ex.ToString)
                            '    End Try
                            'End If
                            '### Commented 11/8/18 to River Sand Integration 1 - End
                        End If
                    Next
nextrecord:
                Next
            End If
            Try
                ColorAdapter.Update(dsAdded)
                ColorData.AcceptChanges()
                ColorAdapter.Update(ColorData, "table")
            Catch

            End Try
        End If
    End Sub

    Private Sub grdColors_AfterUpdate1(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdColors.AfterUpdate
        Dim dsUpdated As DataSet
        Dim adapter As New SqlDataAdapter
        Dim i As Integer
        Dim j As Integer
        Dim str As String
        Dim SearchString As String
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim IDString As Long
        Dim IDAdapter As New SqlDataAdapter
        Dim IDData As New DataSet
        Dim mySql As String
        Dim mySeason() As String
        Dim COLOURART As String
        Dim COLOURNO As String
        Dim COLOURTEXT As String
        Dim COLOURARTNO As String
        Dim COLNOTEXT As String
        Dim COLNODESC As String
        Dim Usql As String
        Dim IDsql As String

        dsUpdated = ColorData.GetChanges(DataRowState.Modified)
        If Not IsNothing(dsUpdated) Then
            dsUpdated.CreateDataReader()
            If dsUpdated.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsUpdated.Tables(0).Rows.Count - 1
                    If IsDBNull(dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed")) Then GoTo nextrecord
                    str = dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed")
                    mySeason = Split(str, ",")
                    For j = 0 To mySeason.GetUpperBound(0)
                        mySeason(j) = Trim(mySeason(j))
                        SearchString = Trim(dsUpdated.Tables(0).Rows(i).Item("Color") & "N" & mySeason(j))
                        SearchAdapter.SelectCommand = dcNAPA.CreateCommand
                        mySql = "SELECT COLARTNO FROM QUESTPDMNAPA.dbo.INTCOLOR WHERE (COLARTNO = '" & SearchString & "')"
                        SearchData.Clear()
                        SearchAdapter.SelectCommand.CommandText = mySql
                        SearchAdapter.Fill(SearchData)
                        SearchData.CreateDataReader()
                        COLOURART = Trim(dsUpdated.Tables(0).Rows(i).Item("Color"))
                        COLOURNO = "N" & mySeason(j)
                        COLOURTEXT = Trim(dsUpdated.Tables(0).Rows(i).Item("Description"))
                        COLOURARTNO = Trim(dsUpdated.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j)
                        COLNOTEXT = Trim(dsUpdated.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j) & "-" & Trim(dsUpdated.Tables(0).Rows(i).Item("Description")) & "-"
                        COLNODESC = Trim(dsUpdated.Tables(0).Rows(i).Item("Color")) & "N" & mySeason(j) & "-" & Trim(dsUpdated.Tables(0).Rows(i).Item("Description"))
                        If SearchData.Tables(0).Rows.Count > 0 Then
                            Usql = "Update QUESTPDMNAPA.dbo.INTCOLOR Set COLOURART = '" & COLOURART & "', " _
                            & "COLOURNO = '" & COLOURNO & "', " _
                            & "COLOURTEXT = '" & COLOURTEXT & "', " _
                            & "COLARTNO = '" & COLOURARTNO & "', " _
                            & "COLNOTEXT = '" & COLNOTEXT & "', " _
                            & "COLNODESC = '" & COLNODESC & "', " _
                            & "CHANGEINIT = 'SA', " _
                            & "CHANGEDATE = CONVERT(DATETIME," & Format(Now, "dd/mm/yyyy") & ", 102), " _
                            & "CHANGETIME = '" & Format(Now, "hh:mm tt") & "', "
                            If Not IsDBNull(dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed")) Then
                                If dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed") <> "" Then
                                    Usql = Usql & "LANGUAGE9 = '1', "
                                End If
                            End If
                            If Not IsDBNull(dsUpdated.Tables(0).Rows(i).Item("SeasonsSamples")) Then
                                If dsUpdated.Tables(0).Rows(i).Item("SeasonsSamples") <> "" Then
                                    Usql = Usql & "LANGUAGE10 = '1' "
                                End If
                            End If
                            Usql = Usql & "WHERE (COLARTNO = '" & SearchString & "')"
                            Try
                                If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                                adapter.UpdateCommand = dcNAPA.CreateCommand
                                adapter.UpdateCommand.CommandText = Usql
                                adapter.UpdateCommand.ExecuteNonQuery()
                            Catch ex As Exception
                                MsgBox(ex.ToString)
                            End Try
                        Else
                            IDAdapter.SelectCommand = dcNAPA.CreateCommand
                            IDsql = "SELECT MAX(RECNO) AS LatestID FROM QUESTPDMNAPA.dbo.INTCOLOR"
                            IDData.Clear()
                            IDAdapter.SelectCommand.CommandText = IDsql
                            IDAdapter.Fill(IDData)
                            IDData.CreateDataReader()
                            If IDData.Tables(0).Rows.Count > 0 Then
                                IDString = IDData.Tables(0).Rows(i).Item("LatestID") + 1
                                Usql = "Insert Into QUESTPDMNAPA.dbo.INTCOLOR (RECNO, COLOURART ,COLOURNO, COLOURTEXT, " _
                                & "COLARTNO, COLNOTEXT, COLNODESC, FIRSTINIT, FIRSTDATE, FIRSTTIME, LANGUAGE9, LANGUAGE10) Values(" _
                                & IDString & ", " _
                                & "'" & COLOURART & "', " _
                                & "'" & COLOURNO & "', " _
                                & "'" & COLOURTEXT & "', " _
                                & "'" & COLOURARTNO & "', " _
                                & "'" & COLNOTEXT & "', " _
                                & "'" & COLNODESC & "', " _
                                & "'SA', " _
                                & "CONVERT(DATETIME," & Format(Now, "dd/mm/yyyy") & ", 102), '" _
                                & Format(Now, "hh:mm tt") & "', "
                                If Not IsDBNull(dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed")) Then
                                    If dsUpdated.Tables(0).Rows(i).Item("SeasonsUsed") <> "" Then
                                        Usql = Usql & "'1', "
                                    Else
                                        Usql = Usql & "NULL, "
                                    End If
                                Else
                                    Usql = Usql & "NULL, "
                                End If
                                If Not IsDBNull(dsUpdated.Tables(0).Rows(i).Item("SeasonsSamples")) Then
                                    If dsUpdated.Tables(0).Rows(i).Item("SeasonsSamples") <> "" Then
                                        Usql = Usql & "'1')"
                                    Else
                                        Usql = Usql & "NULL)"
                                    End If
                                Else
                                    Usql = Usql & "NULL)"
                                End If
                                Try
                                    If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                                    adapter.InsertCommand = dcNAPA.CreateCommand
                                    adapter.InsertCommand.CommandText = Usql
                                    adapter.InsertCommand.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                End Try
                            End If
                        End If
                    Next
nextrecord:
                Next
            End If
            Try
                ColorAdapter.Update(dsUpdated)
                ColorData.AcceptChanges()
                ColorAdapter.Update(ColorData, "table")
            Catch

            End Try
        End If
    End Sub

    Private Sub frmColors_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        Button2.Top = Me.Height - Button2.Height - 50

        grdColors.Width = Me.Width - 25
        grdColors.Height = Me.Height - 65 - Button2.Height - 25
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim lstSorted As New ArrayList
        Dim adapter As New SqlDataAdapter
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim IDAdapter As New SqlDataAdapter
        Dim IDData As New DataSet
        Dim Usql As String

        If grdColors.SelectedRows.Count > 0 Then
            msg = "Are you sure to delete this records?"
            style = MsgBoxStyle.DefaultButton2 Or _
               MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
            title = "Attention"
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.No Then
                Exit Sub
            Else

                For Each row As Object In grdColors.SelectedRows
                    lstSorted.Add(CType(row, Integer))
                Next
                lstSorted.Sort()
                For intIndex As Integer = lstSorted.Count - 1 To 0 Step -1
                    grdColors.Row = lstSorted.Item(intIndex)
                    Usql = "Delete From QUESTPDMNAPA.dbo.INTCOLOR " _
                    & "WHERE (COLOURART = '" & grdColors.Item(grdColors.Row, "Color") & "')"
                    Try
                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                        adapter.DeleteCommand = dcNAPA.CreateCommand
                        adapter.DeleteCommand.CommandText = Usql
                        adapter.DeleteCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    Usql = "Delete From tbl_colortable " _
                    & "WHERE COLOR = '" & grdColors.Item(grdColors.Row, "Color") & "'"
                    Try
                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                        adapter.DeleteCommand = dcNAPA.CreateCommand
                        adapter.DeleteCommand.CommandText = Usql
                        adapter.DeleteCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    grdColors.Delete()
                Next
            End If
        End If

    End Sub
End Class