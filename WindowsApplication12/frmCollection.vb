Imports System.Data.SqlClient
Imports System.Data
Imports C1.C1Excel

Public Class frmCollection
    Private MinorAdapter As New SqlDataAdapter
    Private MinorData As New DataSet
    Private MinorBuilder As SqlCommandBuilder
    Private DeleteGridAdapter As New SqlDataAdapter
    Private DeleteGridData As New DataSet
    Private DeletecmdBuilder As SqlCommandBuilder
    Private DeleteValue As Integer
    Private DWs() As String = {"0", "1", "1b", "2", "3"}

    Private Sub frmCollection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveGridLayout(Me, grdStyles)
        'SaveGridLayout(Me, grdStyles, "GridPerUser", "sp_InsertColumnInUserGrid", dcNAPA, dcNAPAstr, UserId)
    End Sub

    Private Sub frmCollection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim lstSorted As New ArrayList
        Dim adapter As New SqlDataAdapter
        Dim myid As Long
        Dim sql As String

        If e.KeyCode = 46 And grdAddDeleteRecords.SelectedRows.Count > 0 And grdAddDeleteRecords.Focus = True Then
            If grdAddDeleteRecords.SelectedRows.Count <> DeleteValue Then
                msg = "You have Selected " & grdAddDeleteRecords.SelectedRows.Count & " Records."
                msg = msg & Chr(13) & "The Records to Delete are " & DeleteValue & "."
                msg = msg & Chr(13) & "Please adjust"
                MsgBox(msg, MsgBoxStyle.Critical, "Attention")
                Exit Sub
            End If
            msg = "Are you sure to delete this records?"
            style = MsgBoxStyle.DefaultButton2 Or _
               MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
            title = "Attention"
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.No Then
                Exit Sub
            Else

                For Each row As Object In grdAddDeleteRecords.SelectedRows
                    lstSorted.Add(CType(row, Integer))
                Next
                lstSorted.Sort()
                For intIndex As Integer = lstSorted.Count - 1 To 0 Step -1
                    grdAddDeleteRecords.Row = lstSorted.Item(intIndex)
                    myid = grdAddDeleteRecords.Item(grdAddDeleteRecords.Row, 0)
                    sql = "Update NewGrid set IsDeleted = -1 where DevNo = " & myid
                    Try
                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                        adapter.InsertCommand = dcNAPA.CreateCommand
                        adapter.InsertCommand.CommandText = sql
                        adapter.InsertCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    grdAddDeleteRecords.Delete()
                Next
            End If
        End If

    End Sub

    Private Sub frmCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NoSubs = True

        FillTheData()
        GetSelections()
        NoSubs = False
        pnlAddDeleteRecords.Location = New Point(0, 0)
        LoadGridLayout(Me, grdStyles, "GridPerUser", False)
    End Sub

    Private Sub FillTheData()
        Dim SeasonAdapter As New SqlDataAdapter
        Dim SeasonData As New DataSet
        Dim LineAdapter As New SqlDataAdapter
        Dim LineData As New DataSet
        Dim GenderAdapter As New SqlDataAdapter
        Dim GenderData As New DataSet
        Dim MajorAdapter As New SqlDataAdapter
        Dim MajorData As New DataSet
        Dim UseAdapter As New SqlDataAdapter
        Dim UseData As New DataSet

        SeasonAdapter.SelectCommand = dcNAPA.CreateCommand
        SeasonAdapter.SelectCommand.CommandText = "SELECT Season, Description From Param_Seasons_Napa Order By Seq Desc"
        SeasonAdapter.Fill(SeasonData, "Param_Seasons_Napa")
        cmbSeason.DataSource = SeasonData.Tables(0)
        cmbSeason.DisplayMember = "Description"
        cmbSeason.ValueMember = "Season"
        cmbSeason.Update()

        LineAdapter.SelectCommand = dcNAPA.CreateCommand
        LineAdapter.SelectCommand.CommandText = "SELECT Code, Description FROM Param_Line WHERE(Not (Code Is NULL)) ORDER BY Description"
        LineAdapter.Fill(LineData, "Param_Line")
        cmbLine.DataSource = LineData.Tables(0)
        cmbLine.DisplayMember = "Description"
        cmbLine.ValueMember = "Code"
        cmbLine.Update()

        GenderAdapter.SelectCommand = dcNAPAQuest.CreateCommand
        GenderAdapter.SelectCommand.CommandText = "SELECT LEFT(MOC023, 1) AS GenderCode, SUBSTRING(MOC023, 3, 35) AS Gender FROM MAINSTND WHERE(Not(Left(MOC023, 1) Is NULL))"
        GenderAdapter.Fill(GenderData, "QUESTPDMNAPA.dbo.MAINSTND")
        cmbGender.DataSource = GenderData.Tables(0)
        cmbGender.DisplayMember = "Gender"
        cmbGender.ValueMember = "GenderCode"
        cmbGender.Update()

        MajorAdapter.SelectCommand = dcNAPAQuest.CreateCommand
        MajorAdapter.SelectCommand.CommandText = "SELECT MOC039, SUBSTRING(MOC039TXT, 6, 35) AS Major, RECNO FROM MAINSTNO WHERE (NOT (MOC039 IS NULL))"
        MajorAdapter.Fill(MajorData, "QUESTPDMNAPA.dbo.MAINSTNO")
        cmbMajor.DataSource = MajorData.Tables(0)
        cmbMajor.DisplayMember = "Major"
        cmbMajor.ValueMember = "MOC039"
        cmbMajor.Update()

        UseAdapter.SelectCommand = dcNAPA.CreateCommand
        UseAdapter.SelectCommand.CommandText = "SELECT Code, Description FROM Param_Use WHERE(Not (Code Is NULL)) ORDER BY Description"
        UseAdapter.Fill(UseData, "Param_use")
        cmbUse.DataSource = UseData.Tables(0)
        cmbUse.DisplayMember = "Description"
        cmbUse.ValueMember = "Code"
        cmbUse.Update()
    End Sub

    Private Sub frmCollection_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdStyles.Height = Me.Height - 180
        grdStyles.Width = Me.Width - 30
        pnlAddColumn.Left = (Me.Width - pnlAddColumn.Width) / 2
        pnlAddColumn.Top = (Me.Height - pnlAddColumn.Height) / 2
        pnlAddDeleteRecords.Width = Me.Width
        pnlAddDeleteRecords.Height = Me.Height - 65
    End Sub

    Private Sub GetSelections()
        Dim i As Integer
        Dim SearchId As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim UserSettings As New SqlDataAdapter
        Dim UserData As New DataSet
        Dim SearchPresets As New SqlDataAdapter
        Dim PresetsData As New DataSet
        Dim GetId As Integer
        Dim sql As String
        Dim value As Integer
        'Dim sqlConnection1 As New SqlConnection(dcNAPAstr)
        Dim cmd As New SqlCommand()


        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        NoSubs = True
        sql = "SELECT DISTINCT " _
        & "TOP (100) PERCENT dbo.Selections.Id, dbo.Param_Seasons_Napa.Description AS Season, dbo.Param_Line.Description AS Line, " _
        & "dbo.Param_Use.Description AS myUse, SUBSTRING(QUESTPDMNAPA.dbo.MAINSTND.MOC023, 3, 35) AS Gender, " _
        & "SUBSTRING(QUESTPDMNAPA.dbo.MAINSTNO.MOC039TXT, 6, 35) AS Major " _
        & "FROM dbo.Selections INNER JOIN " _
        & "dbo.Param_Seasons_Napa ON dbo.Selections.Season = dbo.Param_Seasons_Napa.Season INNER JOIN " _
        & "dbo.Param_Use ON dbo.Selections.myUse = dbo.Param_Use.Code INNER JOIN " _
        & "dbo.Param_Line ON dbo.Selections.Line = dbo.Param_Line.Code INNER JOIN " _
        & "QUESTPDMNAPA.dbo.MAINSTND ON dbo.Selections.Gender = SUBSTRING(QUESTPDMNAPA.dbo.MAINSTND.MOC023, 1, 2) INNER JOIN " _
        & "QUESTPDMNAPA.dbo.MAINSTNO ON dbo.Selections.Major = QUESTPDMNAPA.dbo.MAINSTNO.MOC039 " _
        & "ORDER BY dbo.Selections.Id DESC"

        SearchPresets.SelectCommand = dcNAPA.CreateCommand
        SearchPresets.SelectCommand.CommandText = sql
        SearchPresets.Fill(PresetsData, "Selections")
        If PresetsData.Tables(0).Rows.Count = 0 Then
            Label4.Visible = False
            ComboBox4.Visible = False
        Else
            ComboBox4.Items.Clear()
            PresetsData.CreateDataReader()
            For i = 0 To PresetsData.Tables(0).Rows.Count - 1
                ComboBox4.Items.Add(PresetsData.Tables(0).Rows(i).Item("Id") & " / " & PresetsData.Tables(0).Rows(i).Item("Season") & " / " & PresetsData.Tables(0).Rows(i).Item("Line") & " / " & PresetsData.Tables(0).Rows(i).Item("myUse") & " / " & PresetsData.Tables(0).Rows(i).Item("Gender") & " / " & PresetsData.Tables(0).Rows(i).Item("Major"))
            Next
            Label4.Visible = True
            ComboBox4.Visible = True
        End If

        SearchId.SelectCommand = dcNAPA.CreateCommand
        SearchId.SelectCommand.CommandText = "SELECT MAX(Id) AS Id FROM Selections"
        SearchId.Fill(SearchData, "Selections")
        If SearchData.Tables(0).Rows.Count = 0 Then
            GetId = 1
        Else
            If Not IsDBNull(SearchData.Tables(0).Rows(0).Item("Id")) Then
                GetId = SearchData.Tables(0).Rows(0).Item("Id")
            Else
                GetId = 1
            End If
        End If
        If GetId > 0 Then
            UserSettings.SelectCommand = dcNAPA.CreateCommand
            UserSettings.SelectCommand.CommandText = "SELECT UserName, Season, myUse, Line, Gender, Major, Minor, NoOfSyles, Selected, Id FROM Selections WHERE (Id = " & GetId & ")"
            UserSettings.Fill(UserData, "Selections")
            If UserData.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Season")) Then cmbSeason.SelectedValue = UserData.Tables(0).Rows(0).Item("Season")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Gender")) Then cmbGender.SelectedValue = UserData.Tables(0).Rows(0).Item("Gender")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Major")) Then cmbMajor.SelectedValue = UserData.Tables(0).Rows(0).Item("Major")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Line")) Then cmbLine.SelectedValue = UserData.Tables(0).Rows(0).Item("Line")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("myUse")) Then cmbUse.SelectedValue = UserData.Tables(0).Rows(0).Item("myUse")
            End If
        End If

        MinorAdapter.SelectCommand = dcNAPA.CreateCommand
        sql = "SELECT QUESTPDMNAPA.dbo.MAINSTNO.MOC024, QUESTPDMNAPA.dbo.MAINSTNO.MOC024TXT, sel.* " _
        & "FROM  dbo.tbl_Minor_hrcy INNER JOIN " _
        & "QUESTPDMNAPA.dbo.MAINSTNO ON dbo.tbl_Minor_hrcy.MinorCode = QUESTPDMNAPA.dbo.MAINSTNO.MOC024 LEFT OUTER JOIN " _
        & "(SELECT * FROM dbo.Selections " _
        & "WHERE (Id = " & GetId & ")) AS sel ON QUESTPDMNAPA.dbo.MAINSTNO.MOC024 = sel.Minor " _
        & "WHERE (NOT (QUESTPDMNAPA.dbo.MAINSTNO.MOC024 IS NULL)) AND (dbo.tbl_Minor_hrcy.MajorCode = '" & cmbMajor.SelectedValue & "')"
        MinorAdapter.SelectCommand.CommandText = sql
        MinorBuilder = New SqlCommandBuilder(MinorAdapter)
        MinorAdapter.Fill(MinorData)
        For i = 0 To grdStyles.RowCount - 1
            grdStyles.Delete(0)
        Next
        grdStyles.DataSource = MinorData
        grdStyles.DataMember = MinorData.Tables(0).ToString
        grdStyles.Rebind(True)
        grdStyles.Splits(0).DisplayColumns("MOC024").Visible = False
        grdStyles.Splits(0).DisplayColumns("MOC024TXT").Locked = True
        grdStyles.Splits(0).DisplayColumns("NoOfSyles").Locked = True
        grdStyles.Splits(0).DisplayColumns("NoOfSyles").DataColumn.Caption = "N° of Styles"
        If ComboBox4.Items.Count > 0 Then
            'grdStyles.Splits(0).DisplayColumns("dw0").Locked = True
            'grdStyles.Splits(0).DisplayColumns("dw1").Locked = True
            'grdStyles.Splits(0).DisplayColumns("dw2").Locked = True
            'grdStyles.Splits(0).DisplayColumns("dw3").Locked = True
        End If
        grdStyles.Splits(0).DisplayColumns("ActiveStyles").DataColumn.Caption = "Original N° of Styles"
        grdStyles.Splits(0).DisplayColumns("ActiveStyles").Locked = True
        grdStyles.Splits(0).DisplayColumns("UserName").Visible = False
        grdStyles.Splits(0).DisplayColumns("Season").Visible = False
        grdStyles.Splits(0).DisplayColumns("myUse").Visible = False
        grdStyles.Splits(0).DisplayColumns("Line").Visible = False
        grdStyles.Splits(0).DisplayColumns("Gender").Visible = False
        grdStyles.Splits(0).DisplayColumns("Major").Visible = False
        grdStyles.Splits(0).DisplayColumns("Minor").Visible = False
        grdStyles.Splits(0).DisplayColumns("Selected").Visible = False
        grdStyles.Splits(0).DisplayColumns("Id").Visible = False
        grdStyles.Splits(0).DisplayColumns("MOC024TXT").DataColumn.Caption = "Minors"
        NoSubs = False
        For i = 0 To grdStyles.RowCount - 1
            If Not IsDBNull(grdStyles.Columns("NoOfSyles").CellValue(i)) Then
                value = value + grdStyles.Columns("NoOfSyles").CellValue(i)
            End If
        Next
        grdStyles.Splits(0).Caption = "Total Styles: " & value
        If ComboBox4.Items.Count > 0 Then
            ComboBox4.SelectedIndex = 0
        End If
        btnSavePrest.Enabled = False
        'btnCreateRecords.Enabled = False
    End Sub

    Private Sub cmbSeason_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSeason.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            For j = 2 To grdStyles.Columns.Count - 1
                grdStyles.Item(i, j) = ""
            Next j
        Next
        btnSavePrest.Enabled = True
        btnCreateRecords.Enabled = True
        For Each col As String In DWs
            grdStyles.Splits(0).DisplayColumns("dw" & col).Locked = False
        Next
    End Sub

    Private Sub cmbLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLine.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            For j = 2 To grdStyles.Columns.Count - 1
                grdStyles.Item(i, j) = ""
            Next j
        Next
        For Each col As String In DWs
            grdStyles.Splits(0).DisplayColumns("dw" & col).Locked = False
        Next
        btnSavePrest.Enabled = True
        btnCreateRecords.Enabled = True
    End Sub

    Private Sub cmbUse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUse.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            For j = 2 To grdStyles.Columns.Count - 1
                grdStyles.Item(i, j) = ""
            Next j
        Next
        For Each col As String In DWs
            grdStyles.Splits(0).DisplayColumns("dw" & col).Locked = False
        Next
        btnSavePrest.Enabled = True
        btnCreateRecords.Enabled = True
    End Sub

    Private Sub cmbGender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGender.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            For j = 2 To grdStyles.Columns.Count - 1
                grdStyles.Item(i, j) = ""
            Next j
        Next
        For Each col As String In DWs
            grdStyles.Splits(0).DisplayColumns("dw" & col).Locked = False
        Next
        btnSavePrest.Enabled = True
        btnCreateRecords.Enabled = True
    End Sub

    Private Sub cmbMajor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMajor.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        Dim sql As String
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            grdStyles.Delete(0)
        Next
        MinorAdapter.SelectCommand = dcNAPA.CreateCommand
        sql = "SELECT QUESTPDMNAPA.dbo.MAINSTNO.MOC024, QUESTPDMNAPA.dbo.MAINSTNO.MOC024TXT, sel.* " _
       & "FROM  dbo.tbl_Minor_hrcy with (nolock) INNER JOIN " _
       & "QUESTPDMNAPA.dbo.MAINSTNO with (nolock) ON dbo.tbl_Minor_hrcy.MinorCode = QUESTPDMNAPA.dbo.MAINSTNO.MOC024 LEFT OUTER JOIN " _
       & "(SELECT * FROM dbo.Selections with (nolock) " _
       & "WHERE (Id = " & 1 & ")) AS sel ON QUESTPDMNAPA.dbo.MAINSTNO.MOC024 = sel.Minor " _
       & "WHERE (NOT (QUESTPDMNAPA.dbo.MAINSTNO.MOC024 IS NULL)) AND (dbo.tbl_Minor_hrcy.MajorCode = '" & cmbMajor.SelectedValue & "')"
        MinorAdapter.SelectCommand.CommandText = sql
        MinorAdapter.Fill(MinorData)

        grdStyles.Rebind(True)

        setGridStylesLayout()

        For i = 0 To grdStyles.RowCount - 1
            For j = 2 To grdStyles.Columns.Count - 1
                grdStyles.Item(i, j) = ""
            Next j
        Next
        btnSavePrest.Enabled = True
        'btnCreateRecords.Enabled = True
    End Sub

    Private Sub setGridStylesLayout()
        grdStyles.Splits(0).DisplayColumns("MOC024").Visible = False
        grdStyles.Splits(0).DisplayColumns("MOC024TXT").Locked = True
        grdStyles.Splits(0).DisplayColumns("NoOfSyles").Locked = True
        grdStyles.Splits(0).DisplayColumns("NoOfSyles").DataColumn.Caption = "N° of Styles"
        For Each col As String In DWs
            grdStyles.Splits(0).DisplayColumns("dw" & col).Locked = False
        Next
        grdStyles.Splits(0).DisplayColumns("ActiveStyles").DataColumn.Caption = "Original N° of Styles"
        grdStyles.Splits(0).DisplayColumns("ActiveStyles").Locked = True
        grdStyles.Splits(0).DisplayColumns("UserName").Visible = False
        grdStyles.Splits(0).DisplayColumns("Season").Visible = False
        grdStyles.Splits(0).DisplayColumns("myUse").Visible = False
        grdStyles.Splits(0).DisplayColumns("Line").Visible = False
        grdStyles.Splits(0).DisplayColumns("Gender").Visible = False
        grdStyles.Splits(0).DisplayColumns("Major").Visible = False
        grdStyles.Splits(0).DisplayColumns("Minor").Visible = False
        grdStyles.Splits(0).DisplayColumns("Selected").Visible = False
        grdStyles.Splits(0).DisplayColumns("Id").Visible = False
        grdStyles.Splits(0).DisplayColumns("MOC024TXT").DataColumn.Caption = "Minors"
        grdStyles.Splits(0).DisplayColumns("Review").DataColumn.Caption = "Review"
        grdStyles.Splits(0).DisplayColumns("Review").Locked = True
        grdStyles.Splits(0).DisplayColumns("PLM").DataColumn.Caption = "PLM"
        grdStyles.Splits(0).DisplayColumns("PLM").Locked = True
        grdStyles.Splits(0).DisplayColumns("EditingMeeting").DataColumn.Caption = "Editing Meeting"
        grdStyles.Splits(0).DisplayColumns("EditingMeeting").Locked = True
        grdStyles.Splits(0).DisplayColumns("StartOfSC").DataColumn.Caption = "Start of S.C."
        grdStyles.Splits(0).DisplayColumns("StartOfSC").Locked = True

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim i As Integer
        Dim UserSettings As New SqlDataAdapter
        Dim UserData As New DataSet
        Dim sql As String
        Dim GetId As Integer
        Dim value As Integer
        If NoSubs = True Then Exit Sub
        For i = 0 To grdStyles.RowCount - 1
            grdStyles.Delete(0)
        Next

        GetId = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))
        If GetId > 0 Then
            UserSettings.SelectCommand = dcNAPA.CreateCommand
            UserSettings.SelectCommand.CommandText = "SELECT * FROM Selections WHERE (Id = " & GetId & ")"
            UserSettings.Fill(UserData, "Selections")
            If UserData.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Season")) Then cmbSeason.SelectedValue = UserData.Tables(0).Rows(0).Item("Season")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Gender")) Then cmbGender.SelectedValue = UserData.Tables(0).Rows(0).Item("Gender")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Major")) Then cmbMajor.SelectedValue = UserData.Tables(0).Rows(0).Item("Major")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("Line")) Then cmbLine.SelectedValue = UserData.Tables(0).Rows(0).Item("Line")
                If Not IsDBNull(UserData.Tables(0).Rows(0).Item("myUse")) Then cmbUse.SelectedValue = UserData.Tables(0).Rows(0).Item("myUse")
            End If
        End If
        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        MinorAdapter.SelectCommand = dcNAPA.CreateCommand
        MinorData.Clear()
        sql = "SELECT QUESTPDMNAPA.dbo.MAINSTNO.MOC024, QUESTPDMNAPA.dbo.MAINSTNO.MOC024TXT, sel.* " _
        & "FROM  dbo.tbl_Minor_hrcy INNER JOIN " _
        & "QUESTPDMNAPA.dbo.MAINSTNO ON dbo.tbl_Minor_hrcy.MinorCode = QUESTPDMNAPA.dbo.MAINSTNO.MOC024 LEFT OUTER JOIN " _
        & "(SELECT * FROM dbo.Selections " _
        & "WHERE (Id = " & GetId & ")) AS sel ON QUESTPDMNAPA.dbo.MAINSTNO.MOC024 = sel.Minor " _
        & "WHERE (NOT (QUESTPDMNAPA.dbo.MAINSTNO.MOC024 IS NULL)) AND (dbo.tbl_Minor_hrcy.MajorCode = '" & cmbMajor.SelectedValue & "')"
        MinorAdapter.SelectCommand.CommandText = sql
        MinorAdapter.Fill(MinorData)

        grdStyles.Rebind(True)

        setGridStylesLayout()

        For i = 0 To grdStyles.RowCount - 1
            If Not IsDBNull(grdStyles.Columns("NoOfSyles").CellValue(i)) Then
                value = value + grdStyles.Columns("NoOfSyles").CellValue(i)
            End If
        Next
        grdStyles.Splits(0).Caption = "Total Styles: " & value
        btnSavePrest.Enabled = False

        'GetId()
        Dim sqlDA1 As New SqlCommand
        sqlDA1.CommandText = "select * from napa.dbo.newgrid where isdeleted=0 and collectionid=" & GetId
        sqlDA1.CommandType = CommandType.Text
        sqlDA1.Connection = dcNAPA
        With sqlDA1
            Dim sqlDr2 As SqlDataReader = .ExecuteReader
            'i = 1
            If sqlDr2.HasRows Then
                btnCreateRecords.Enabled = False
            Else
                If btnSavePrest.Enabled = True Then
                    btnCreateRecords.Enabled = False
                Else
                    btnCreateRecords.Enabled = True
                End If

            End If
            sqlDr2.Close()
        End With

        sqlDA1.Dispose()

    End Sub

    Private Sub btnSavePrest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePrest.Click
        Dim adapter As New SqlDataAdapter
        Dim SearchId As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim sql As String
        Dim NewId As Integer
        Dim i As Integer
        Dim SearchPresets As New SqlDataAdapter
        Dim PresetsData As New DataSet
        If cmbSeason.SelectedValue = "" Then
            MsgBox("Please Select a Season", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbUse.SelectedValue = "" Then
            MsgBox("Please Select a Use", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbLine.SelectedValue = "" Then
            MsgBox("Please Select a Line", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbGender.SelectedValue = "" Then
            MsgBox("Please Select a Gender", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbMajor.SelectedValue = "" Then
            MsgBox("Please Select a Major", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        SearchId.SelectCommand = dcNAPA.CreateCommand
        SearchId.SelectCommand.CommandText = "SELECT MAX(Id) AS Id FROM Selections"
        SearchId.Fill(SearchData, "Selections")
        If SearchData.Tables(0).Rows.Count = 0 Then
            NewId = 1
        Else
            If Not IsDBNull(SearchData.Tables(0).Rows(0).Item("Id")) Then
                NewId = SearchData.Tables(0).Rows(0).Item("Id") + 1
            Else
                NewId = 1
            End If
        End If

        For i = 0 To grdStyles.RowCount - 1
            If Not IsDBNull(grdStyles.Item(i, "NoOfSyles")) Then
                If Val(grdStyles.Item(i, "NoOfSyles").ToString) > 0 Then
                    sql = "Insert INTO Selections (Username, Season, myUse, Line, Gender, Major, Minor, dw0, dw1, dw1b, dw2, dw3, NoOfSyles, Id, Selected, ActiveStyles) " _
                                   & "VALUES ('" & UserId & "', '" & cmbSeason.SelectedValue & "', '" & cmbUse.SelectedValue & "', '" & cmbLine.SelectedValue & "', '" & cmbGender.SelectedValue & "', '" & cmbMajor.SelectedValue & "', '" _
                    & grdStyles.Item(i, 0) & "', " & Val(grdStyles.Item(i, "dw0").ToString) & ", " & Val(grdStyles.Item(i, "dw1").ToString) & ", " & Val(grdStyles.Item(i, "dw1b").ToString) & ", " & Val(grdStyles.Item(i, "dw2").ToString) & ", " & Val(grdStyles.Item(i, "dw3").ToString) & ", " & Val(grdStyles.Item(i, "ActiveStyles").ToString) & ", " & NewId & ", -1, " & Val(grdStyles.Item(i, "ActiveStyles").ToString) & ")"
                    Try
                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                        adapter.InsertCommand = dcNAPA.CreateCommand
                        adapter.InsertCommand.CommandText = sql
                        adapter.InsertCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        btnSavePrest.Enabled = False
                        btnCreateRecords.Enabled = False
                    End Try
                End If
            End If
        Next
        GetSelections()

    End Sub

    Private Sub btnCreateRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateRecords.Click
        Dim i As Integer
        Dim j As Integer
        Dim adapter As New SqlDataAdapter
        Dim sql As String
        Dim GetId As Integer
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim myMarkup As Double

        If cmbSeason.SelectedValue = "" Then
            MsgBox("Please Select a Season", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbUse.SelectedValue = "" Then
            MsgBox("Please Select a Use", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbLine.SelectedValue = "" Then
            MsgBox("Please Select a Line", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbGender.SelectedValue = "" Then
            MsgBox("Please Select a Gender", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If cmbMajor.SelectedValue = "" Then
            MsgBox("Please Select a Major", MsgBoxStyle.Critical)
            Exit Sub
        End If

        GetId = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))

        sql = "SELECT Markup FROM NAPA.dbo.Param_CostBuckets WHERE (Season = '" & cmbSeason.SelectedValue & "')"
        SearchAdapter.SelectCommand = dcNAPA.CreateCommand
        SearchAdapter.SelectCommand.CommandText = sql
        SearchData.Clear()
        SearchAdapter.Fill(SearchData)
        SearchData.CreateDataReader()
        If SearchData.Tables(0).Rows.Count > 0 Then
            myMarkup = SearchData.Tables(0).Rows(0).Item("Markup")
        End If

        For i = 0 To grdStyles.RowCount - 1
            If Not IsDBNull(grdStyles.Item(i, "NoOfSyles")) Then
                If Val(grdStyles.Item(i, "NoOfSyles").ToString) > 0 Then
                    If IsDBNull(grdStyles.Item(i, "NoOfSyles")) Then
                        MsgBox("Please give the N° of Styles for " & grdStyles.Item(i, "Minor"))
                        Exit For
                    End If

                    'new logic for markup generation start
                    Dim sNewMarkupSQL As String
                    sNewMarkupSQL = "select markup from Param_Markup where season='" & cmbSeason.SelectedValue & "' and gender='" & cmbGender.SelectedValue & "'"
                    sNewMarkupSQL = sNewMarkupSQL & " and minor='" & grdStyles.Item(i, 0) & "'"
                    Dim dtNewMarkup As DataTable
                    dtNewMarkup = getSelectDataSet(sNewMarkupSQL).Tables(0)
                    If dtNewMarkup.Rows.Count > 0 Then
                        myMarkup = dtNewMarkup.Rows(0)("markup").ToString()
                    Else
                        SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", " Markup table not filled", "Markup value not found:" & sNewMarkupSQL)
                    End If
                    'new logic for markup generation end

                    For Each col As String In DWs
                        If Not IsDBNull(grdStyles.Item(i, "dw" & col)) Then
                            If Val(grdStyles.Item(i, "dw" & col).ToString) > 0 Then
                                For j = 1 To Val(grdStyles.Item(i, "dw" & col).ToString)
                                    sql = "Insert INTO Newgrid (Season, myUse, Line, Gender, Major, Minor, Dw, myUser, CollectionID, MarkUp) " _
                                                   & "VALUES ('" & cmbSeason.SelectedValue & "', '" & cmbUse.SelectedValue & "', '" & cmbLine.SelectedValue & "', '" & cmbGender.SelectedValue & "', '" & cmbMajor.SelectedValue & "', '" _
                                    & grdStyles.Item(i, 0) & "', '" & col & "', '" & UserId & "', " & GetId & ", " & myMarkup & ")"
                                    Try
                                        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                                        adapter.InsertCommand = dcNAPA.CreateCommand
                                        adapter.InsertCommand.CommandText = sql
                                        adapter.InsertCommand.ExecuteNonQuery()
                                    Catch ex As Exception
                                        MsgBox(ex.ToString)
                                    End Try
                                Next
                            End If
                        End If
                    Next
                End If
            End If
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
        Label11.Text = "Records Saved"
        Label11.Visible = True
        btnCreateRecords.Enabled = False
    End Sub

    Private Sub btnAddColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddColumn.Click
        pnlAddColumn.Visible = True
        pnlAddColumn.BringToFront()
    End Sub

    Private Sub btnSaveColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveColumn.Click
        Dim cmd As New SqlCommand()
        Dim rowsAffected As Integer
        Dim MinorData As New DataSet
        If ComboBox4.Items.Count = 0 Then
            MsgBox("You can only Add Colums After Saving a Selection")
            Exit Sub
        End If
        If TextBox1.Text = "" Or (pnlAddColumn.Visible = True And TextBox1.Text.ToLower().StartsWith("freeze")) Then
            MsgBox("Please give a Name for this Column (Not starting with 'Freeze')")
            Exit Sub
        End If
        If NumericUpDown1.Value = 0 And RadioButton1.Checked = True Then
            MsgBox("Please give a Length for this Column")
            Exit Sub
        End If
        cmd.Connection = dcNAPA
        cmd.CommandText = "sp_InsertColumnInSelections"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("@name", SqlDbType.VarChar)).Value = Replace(TextBox1.Text, " ", "")
        If RadioButton1.Checked = True Then
            cmd.Parameters.Add(New SqlParameter("@string", SqlDbType.SmallInt)).Value = 1
            cmd.Parameters.Add(New SqlParameter("@lenght", SqlDbType.VarChar)).Value = NumericUpDown1.Value
            cmd.Parameters.Add(New SqlParameter("@numeric", SqlDbType.SmallInt)).Value = 0
            cmd.Parameters.Add(New SqlParameter("@date", SqlDbType.SmallInt)).Value = 0
        End If
        If RadioButton2.Checked = True Then
            cmd.Parameters.Add(New SqlParameter("@string", SqlDbType.SmallInt)).Value = 0
            cmd.Parameters.Add(New SqlParameter("@lenght", SqlDbType.VarChar)).Value = "0"
            cmd.Parameters.Add(New SqlParameter("@numeric", SqlDbType.SmallInt)).Value = 1
            cmd.Parameters.Add(New SqlParameter("@date", SqlDbType.SmallInt)).Value = 0
        End If
        If RadioButton3.Checked = True Then
            cmd.Parameters.Add(New SqlParameter("@string", SqlDbType.SmallInt)).Value = 0
            cmd.Parameters.Add(New SqlParameter("@lenght", SqlDbType.VarChar)).Value = "0"
            cmd.Parameters.Add(New SqlParameter("@numeric", SqlDbType.SmallInt)).Value = 0
            cmd.Parameters.Add(New SqlParameter("@date", SqlDbType.SmallInt)).Value = 1
        End If
        rowsAffected = cmd.ExecuteNonQuery()

        Dim i As Integer
        Dim UserSettings As New SqlDataAdapter
        Dim UserData As New DataSet
        Dim sql As String
        Dim GetId As Integer
        For i = 0 To grdStyles.RowCount - 1
            grdStyles.Delete(0)
        Next

        If ComboBox4.SelectedText = "" Then
            ComboBox4.SelectedIndex = 0
        End If
        GetId = Val(Microsoft.VisualBasic.Left(ComboBox4.Text, 2))
        MinorData.Clear()
        Try
            MinorData.Tables(0).Columns.Clear()
        Catch
        End Try
        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        MinorAdapter.SelectCommand = dcNAPA.CreateCommand
        sql = "SELECT QUESTPDMNAPA.dbo.MAINSTNO.MOC024, QUESTPDMNAPA.dbo.MAINSTNO.MOC024TXT, sel.* " _
        & "FROM  dbo.tbl_Minor_hrcy INNER JOIN " _
        & "QUESTPDMNAPA.dbo.MAINSTNO ON dbo.tbl_Minor_hrcy.MinorCode = QUESTPDMNAPA.dbo.MAINSTNO.MOC024 LEFT OUTER JOIN " _
        & "(SELECT * FROM dbo.Selections " _
        & "WHERE (Id = " & GetId & ")) AS sel ON QUESTPDMNAPA.dbo.MAINSTNO.MOC024 = sel.Minor " _
        & "WHERE (NOT (QUESTPDMNAPA.dbo.MAINSTNO.MOC024 IS NULL)) AND (dbo.tbl_Minor_hrcy.MajorCode = '" & cmbMajor.SelectedValue & "')"
        MinorAdapter.SelectCommand.CommandText = sql
        MinorAdapter.Fill(MinorData)


        grdStyles.DataSource = MinorData
        grdStyles.DataMember = MinorData.Tables(0).ToString
        grdStyles.Rebind(False)

        setGridStylesLayout()

        pnlAddColumn.Visible = False
    End Sub

    Private Sub btnCancelColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelColumn.Click
        pnlAddColumn.Visible = False
    End Sub

    Private Sub grdStyles_AfterColEdit(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdStyles.AfterColEdit
        Dim msg As String
        Dim title As String
        Dim myId As Integer = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim value As Integer
        Dim i As Integer
        Dim j As Integer
        Dim adapter As New SqlDataAdapter
        Dim sql As String
        Dim SearchAdapter As New SqlDataAdapter
        Dim SearchData As New DataSet
        Dim myMarkup As Double

        Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdStyles.Splits(0).DisplayColumns(Me.grdStyles.Col).DataColumn
        If pnlAddDeleteRecords.Visible = True Then Exit Sub
        If ComboBox4.Items.Count = 0 Then Exit Sub

        If col.DataField = "dw0" Or col.DataField = "dw1" Or col.DataField = "dw1b" Or col.DataField = "dw2" Or col.DataField = "dw3" Then
            Dim col2 As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdStyles.Splits(0).DisplayColumns(grdStyles.Columns("NoOfSyles")).DataColumn
            Dim col3 As C1.Win.C1TrueDBGrid.C1DataColumn = Me.grdStyles.Splits(0).DisplayColumns(grdStyles.Columns("ActiveStyles")).DataColumn
            grdStyles.Row = Me.grdStyles.Row
            If Not IsDBNull(grdStyles.Columns("dw0").CellValue(Me.grdStyles.Row)) Then
                value = value + grdStyles.Columns("dw0").CellValue(Me.grdStyles.Row)
            End If
            If Not IsDBNull(grdStyles.Columns("dw1").CellValue(Me.grdStyles.Row)) Then
                value = value + grdStyles.Columns("dw1").CellValue(Me.grdStyles.Row)
            End If
            If Not IsDBNull(grdStyles.Columns("dw1b").CellValue(Me.grdStyles.Row)) Then
                value = value + grdStyles.Columns("dw1b").CellValue(Me.grdStyles.Row)
            End If
            If Not IsDBNull(grdStyles.Columns("dw2").CellValue(Me.grdStyles.Row)) Then
                value = value + grdStyles.Columns("dw2").CellValue(Me.grdStyles.Row)
            End If
            If Not IsDBNull(grdStyles.Columns("dw3").CellValue(Me.grdStyles.Row)) Then
                value = value + grdStyles.Columns("dw3").CellValue(Me.grdStyles.Row)
            End If
            col2.Text = value
            If btnSavePrest.Enabled = True Then
                col3.Text = value
            End If

            value = 0
            For i = 0 To grdStyles.RowCount - 1
                If Not IsDBNull(grdStyles.Columns("NoOfSyles").CellValue(i)) Then
                    value = value + grdStyles.Columns("NoOfSyles").CellValue(i)
                End If
            Next
            grdStyles.Splits(0).Caption = "Total Styles: " & value
        End If

        If btnSavePrest.Enabled = True Then Exit Sub

        If col.DataField <> "dw0" And col.DataField <> "dw1" And col.DataField <> "dw1b" And col.DataField <> "dw2" And col.DataField <> "dw3" Then
            NoSubs = True
            If ComboBox4.SelectedText = "" Then
                ComboBox4.SelectedIndex = 0
            End If
            Dim myStyle As Object
            myStyle = col.DataField.GetType.ToString
            If myStyle = "System.String" Then
                sql = "Update Selections set " & col.DataField & " = '" & grdStyles.Columns(Me.grdStyles.Col).CellValue(Me.grdStyles.Row) & "'"
            Else
                sql = "Update Selections set " & col.DataField & " = " & grdStyles.Columns(Me.grdStyles.Col).CellValue(Me.grdStyles.Row)
            End If
            executeSQL(sql & " Where Id = " & myId & " And Minor = '" & grdStyles.Item(i, 0) & "'")
            NoSubs = False
        Else
            SearchAdapter.SelectCommand = dcNAPA.CreateCommand
            SearchAdapter.SelectCommand.CommandText = "SELECT " & col.DataField & " From Selections Where Id = " & myId & " And Minor = '" & grdStyles.Item(grdStyles.Row, 0) & "'"
            SearchAdapter.Fill(SearchData)
            SearchData.CreateDataReader()
            If SearchData.Tables(0).Rows.Count > 0 Then
                If grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row) > Val(SearchData.Tables(0).Rows(0).Item(col.DataField).ToString) Then
                    value = grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row) - Val(SearchData.Tables(0).Rows(0).Item(col.DataField).ToString)
                    msg = "Should I Add " & value & " Styles for " & col.DataField & "?"
                    style = MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
                    title = "More Active Styles"
                    response = MsgBox(msg, style, title)
                    If response = MsgBoxResult.Yes Then
                        executeSQL("Update Selections set " & col.DataField & " = '" & grdStyles.Columns(Me.grdStyles.Col).CellValue(Me.grdStyles.Row) & "'" & _
                                        " Where Id = " & myId & " And Minor = '" & grdStyles.Item(grdStyles.Row, 0) & "'")
                        For j = 1 To value

                            'new logic for markup generation start
                            Dim sNewMarkupSQL As String
                            sNewMarkupSQL = "select markup from Param_Markup where season='" & cmbSeason.SelectedValue & "' and gender='" & cmbGender.SelectedValue & "'"
                            sNewMarkupSQL = sNewMarkupSQL & " and minor='" & grdStyles.Item(grdStyles.Row, 0) & "'"
                            Dim dtNewMarkup As DataTable
                            dtNewMarkup = getSelectDataSet(sNewMarkupSQL).Tables(0)
                            If dtNewMarkup.Rows.Count > 0 Then
                                myMarkup = dtNewMarkup.Rows(0)("markup").ToString()
                            Else
                                SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", " Markup table not filled", "Markup value not found:" & sNewMarkupSQL)
                            End If
                            'new logic for markup generation end

                            executeSQL("Insert INTO Newgrid (Season, myUse, Line, Gender, Major, Minor, Dw, myUser, CollectionID, markup) " & _
                                            "VALUES ('" & cmbSeason.SelectedValue & "', '" & cmbUse.SelectedValue & "', '" & cmbLine.SelectedValue & "', '" & cmbGender.SelectedValue & "', '" & cmbMajor.SelectedValue & "', '" & _
                                            grdStyles.Item(grdStyles.Row, 0) & "', '" & Microsoft.VisualBasic.Right(col.DataField, 1).ToString & "', '" & UserId & "', " & myId & ", " & myMarkup & ")")
                        Next
                        For i = 0 To frmMainGrid.grdMain.RowCount - 1
                            frmMainGrid.grdMain.Delete(0)
                        Next
                        GridAdapter.SelectCommand = dcNAPA.CreateCommand
                        GridAdapter.SelectCommand.CommandText = "SELECT * From NewGrid order by DevNo Desc"
                        cmdBuilder = New SqlCommandBuilder(GridAdapter)

                        GridAdapter.Fill(GridData)
                        frmMainGrid.grdMain.DataSource = GridData
                        frmMainGrid.grdMain.DataMember = GridData.Tables(0).ToString
                        frmMainGrid.grdMain.Rebind(True)
                    End If
                End If
                If grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row) < Val(SearchData.Tables(0).Rows(0).Item(col.DataField).ToString) Then
                    DeleteValue = Val(SearchData.Tables(0).Rows(0).Item(col.DataField).ToString) - grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row)
                    sql = "SELECT * From NewGrid Where CollectionId = " & myId & " And Dw = '" & Microsoft.VisualBasic.Right(col.DataField, 1).ToString & "' And Minor = '" & grdStyles.Item(grdStyles.Row, 0) & "'"
                    For i = 0 To grdAddDeleteRecords.RowCount - 1
                        grdAddDeleteRecords.Delete(0)
                    Next
                    DeleteGridAdapter.SelectCommand = dcNAPA.CreateCommand
                    DeleteGridAdapter.SelectCommand.CommandText = sql
                    DeletecmdBuilder = New SqlCommandBuilder(DeleteGridAdapter)
                    DeleteGridAdapter.Fill(DeleteGridData)
                    grdAddDeleteRecords.DataSource = DeleteGridData
                    grdAddDeleteRecords.DataMember = DeleteGridData.Tables(0).ToString
                    grdAddDeleteRecords.Rebind(True)
                    SearchAdapter.SelectCommand = dcNAPA.CreateCommand
                    SearchAdapter.SelectCommand.CommandText = "SELECT * From GridLayout"
                    SearchAdapter.Fill(SearchData)
                    SearchData.CreateDataReader()
                    Dim foundRows() As Data.DataRow
                    For i = 0 To grdAddDeleteRecords.Columns.Count - 1
                        foundRows = SearchData.Tables(0).Select("ColumnName = '" & grdAddDeleteRecords.Columns(i).DataField & "'")
                        If foundRows IsNot Nothing Then
                            grdAddDeleteRecords.Columns(i).Caption = foundRows(0).ItemArray(1)
                        End If
                    Next
                    executeSQL("Update Selections set " & col.DataField & " = '" & grdStyles.Columns(Me.grdStyles.Col).CellValue(Me.grdStyles.Row) & "'" & _
                                    " Where Id = " & myId & " And Minor = '" & grdStyles.Item(grdStyles.Row, 0) & "'")
                    pnlAddDeleteRecords.Visible = True
                    MsgBox("You have to Delete " & DeleteValue & " Records!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            Else
                If IsDBNull(grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row)) Then Exit Sub
                value = grdStyles.Columns(col.DataField).CellValue(Me.grdStyles.Row)
                If value > 0 Then
                    msg = "Should I Add " & value & " Styles for " & col.DataField & "?"
                    style = MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
                    title = "More Active Styles"
                    response = MsgBox(msg, style, title)
                    If response = MsgBoxResult.Yes Then
                        executeSQL("Insert INTO Selections (Username, Season, myUse, Line, Gender, Major, Minor, dw0, dw1, dw2, dw3, NoOfSyles, Id, Selected, ActiveStyles) " & _
                                     "VALUES ('" & UserId & "', '" & cmbSeason.SelectedValue & "', '" & cmbUse.SelectedValue & "', '" & cmbLine.SelectedValue & "', '" & cmbGender.SelectedValue & "', '" & cmbMajor.SelectedValue & "', '" & _
                                     grdStyles.Item(grdStyles.Row, 0) & "', " & Val(grdStyles.Item(grdStyles.Row, "dw0").ToString) & ", " & Val(grdStyles.Item(grdStyles.Row, "dw1").ToString) & ", " & Val(grdStyles.Item(grdStyles.Row, "dw2").ToString) & ", " & Val(grdStyles.Item(grdStyles.Row, "dw3").ToString) & ", " & Val(grdStyles.Item(grdStyles.Row, "NoOfSyles").ToString) & ", " & myId & ", -1, " & Val(grdStyles.Item(grdStyles.Row, "ActiveStyles").ToString) & ")")
                        For j = 1 To value
                            'new logic for markup generation start
                            Dim sNewMarkupSQL As String
                            sNewMarkupSQL = "select markup from Param_Markup where season='" & cmbSeason.SelectedValue & "' and gender='" & cmbGender.SelectedValue & "'"
                            sNewMarkupSQL = sNewMarkupSQL & " and minor='" & grdStyles.Item(grdStyles.Row, 0) & "'"
                            Dim dtNewMarkup As DataTable
                            dtNewMarkup = getSelectDataSet(sNewMarkupSQL).Tables(0)
                            If dtNewMarkup.Rows.Count > 0 Then
                                myMarkup = dtNewMarkup.Rows(0)("markup").ToString()
                            Else
                                SendSMTPMail("Application@vfc.com", "lakshms@vfc.com", "", "", " Markup table not filled", "Markup value not found:" & sNewMarkupSQL)
                            End If
                            'new logic for markup generation end
                            executeSQL("Insert INTO Newgrid (Season, myUse, Line, Gender, Major, Minor, Dw, myUser, CollectionID, Markup) " & _
                                            "VALUES ('" & cmbSeason.SelectedValue & "', '" & cmbUse.SelectedValue & "', '" & cmbLine.SelectedValue & "', '" & cmbGender.SelectedValue & "', '" & cmbMajor.SelectedValue & "', '" & _
                                            grdStyles.Item(grdStyles.Row, 0) & "', '" & Microsoft.VisualBasic.Right(col.DataField, 1).ToString & "', '" & UserId & "', " & myId & ", " & myMarkup & ")")
                        Next
                        For i = 0 To frmMainGrid.grdMain.RowCount - 1
                            frmMainGrid.grdMain.Delete(0)
                        Next
                        GridAdapter.SelectCommand = dcNAPA.CreateCommand
                        GridAdapter.SelectCommand.CommandText = "SELECT * From NewGrid order by DevNo Desc"
                        cmdBuilder = New SqlCommandBuilder(GridAdapter)

                        GridAdapter.Fill(GridData)
                        frmMainGrid.grdMain.DataSource = GridData
                        frmMainGrid.grdMain.DataMember = GridData.Tables(0).ToString
                        frmMainGrid.grdMain.Rebind(True)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        pnlAddDeleteRecords.Visible = False
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        pnlAddDeleteRecords.Visible = False
    End Sub

    Private Sub grdAddDeleteRecords_AfterDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAddDeleteRecords.AfterDelete
        Dim dsDeleted As DataSet
        dsDeleted = DeleteGridData.GetChanges(DataRowState.Deleted)
        If Not IsNothing(dsDeleted) Then
            DeleteGridAdapter.Update(dsDeleted)
            DeleteGridData.AcceptChanges()
            DeleteGridAdapter.Update(DeleteGridData, "table")
        End If
    End Sub

    Private Sub grdStyles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdStyles.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If grdStyles.SelectedCols.Count = 1 Then
                If grdStyles.SelectedCols(0).DataField <> "MOC024TXT" And grdStyles.SelectedCols(0).DataField <> "NoOfSyles" And grdStyles.SelectedCols(0).DataField <> "ActiveStyles" And grdStyles.SelectedCols(0).DataField <> "dw0" And grdStyles.SelectedCols(0).DataField <> "dw1" And grdStyles.SelectedCols(0).DataField <> "dw2" And grdStyles.SelectedCols(0).DataField <> "dw3" Then
                    grdStyles.ContextMenuStrip = ContextMenuStrip1
                Else
                    grdStyles.ContextMenuStrip = Nothing
                End If
            Else
                grdStyles.ContextMenuStrip = Nothing
            End If
        End If
    End Sub

    Private Sub DeleteThisColumnToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteThisColumnToolStripMenuItem.Click
        Dim cmd As New SqlCommand()
        Dim rowsAffected As Integer
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim MinorData As New DataSet
        msg = "Should I Delete " & grdStyles.SelectedCols(0).DataField & "?"
        style = MsgBoxStyle.DefaultButton2 Or _
           MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        title = "Delete Columns"
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            cmd.Connection = dcNAPA
            cmd.CommandText = "sp_DeleteColumnInSelections"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@name", SqlDbType.VarChar)).Value = grdStyles.SelectedCols(0).DataField

            rowsAffected = cmd.ExecuteNonQuery()

            Dim i As Integer
            Dim UserSettings As New SqlDataAdapter
            Dim UserData As New DataSet
            Dim sql As String
            Dim GetId As Integer
            For i = 0 To grdStyles.RowCount - 1
                grdStyles.Delete(0)
            Next

            If ComboBox4.SelectedText = "" Then
                ComboBox4.SelectedIndex = 0
            End If
            GetId = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))
            MinorData.Clear()
            Try
                MinorData.Tables(0).Columns.Clear()
            Catch
            End Try
            If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
            MinorAdapter.SelectCommand = dcNAPA.CreateCommand
            sql = "SELECT QUESTPDMNAPA.dbo.MAINSTNO.MOC024, QUESTPDMNAPA.dbo.MAINSTNO.MOC024TXT, sel.* " _
            & "FROM  dbo.tbl_Minor_hrcy with (nolock) INNER JOIN " _
            & "QUESTPDMNAPA.dbo.MAINSTNO with (nolock) ON dbo.tbl_Minor_hrcy.MinorCode = QUESTPDMNAPA.dbo.MAINSTNO.MOC024 LEFT OUTER JOIN " _
            & "(SELECT * FROM dbo.Selections with (nolock) " _
            & "WHERE (Id = " & GetId & ")) AS sel ON QUESTPDMNAPA.dbo.MAINSTNO.MOC024 = sel.Minor " _
            & "WHERE (NOT (QUESTPDMNAPA.dbo.MAINSTNO.MOC024 IS NULL)) AND (dbo.tbl_Minor_hrcy.MajorCode = '" & cmbMajor.SelectedValue & "')"
            MinorAdapter.SelectCommand.CommandText = sql
            MinorAdapter.Fill(MinorData)

            grdStyles.DataSource = MinorData
            grdStyles.DataMember = MinorData.Tables(0).ToString
            grdStyles.Rebind(False)
            grdStyles.Splits(0).DisplayColumns(0).Visible = False
            grdStyles.Splits(0).DisplayColumns(1).DataColumn.Caption = "Minor"
            grdStyles.Splits(0).DisplayColumns(1).Locked = True
            grdStyles.Splits(0).DisplayColumns(2).DataColumn.Caption = "N° of Styles"
            grdStyles.Splits(0).DisplayColumns(2).Locked = True
            grdStyles.Splits(0).DisplayColumns(3).DataColumn.Caption = "Active Styles"
            grdStyles.Splits(0).DisplayColumns(4).Visible = False
            grdStyles.Splits(0).DisplayColumns(5).Visible = False
            grdStyles.Splits(0).DisplayColumns(6).Visible = False
            grdStyles.Splits(0).DisplayColumns(7).Visible = False
            grdStyles.Splits(0).DisplayColumns(8).Visible = False
            grdStyles.Splits(0).DisplayColumns(9).Visible = False
            grdStyles.Splits(0).DisplayColumns(10).Visible = False
            grdStyles.Splits(0).DisplayColumns(11).Visible = False
            grdStyles.Splits(0).DisplayColumns(12).Visible = False
        End If

    End Sub

    Private Sub btnDeleteCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCollection.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        Dim sql As String
        Dim myID As Integer
        Dim adapter As New SqlDataAdapter

        msg = "Should I Delete This Collection and all His Records?"
        style = MsgBoxStyle.DefaultButton2 Or _
           MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        title = "Delete Collection"
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            myID = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))
            sql = "Delete From NewGrid WHERE (collectionid = " & myID & ")"
            Try
                If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                adapter.DeleteCommand = dcNAPA.CreateCommand
                adapter.DeleteCommand.CommandText = sql
                adapter.DeleteCommand.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            sql = "Delete From selections WHERE (id = " & myID & ")"
            Try
                If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
                adapter.DeleteCommand = dcNAPA.CreateCommand
                adapter.DeleteCommand.CommandText = sql
                adapter.DeleteCommand.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End If
        GetSelections()
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim i As Integer
        Dim j As Integer
        Dim sheet As XLSheet = C1XLBook1.Sheets(0)
        Dim style1 As New XLStyle(C1XLBook1)
        Dim MyAttr As FileAttribute

        style1.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Dim sqlDA As New SqlCommand
        sqlDA.CommandText = "select * from napa.dbo.vw_Selection_Overview order by season,description,major,gender,line,minor"
        sqlDA.CommandType = CommandType.Text
        sqlDA.Connection = dcNAPA
        With sqlDA
            Dim sqlDr As SqlDataReader = .ExecuteReader
            'i = 1
            If sqlDr.HasRows Then
                For i = 0 To sqlDr.FieldCount - 1
                    Select Case UCase(sqlDr.GetName(i).ToString)
                        Case "NOOFSYLES"
                            sheet.Item(0, i).Value = "N°Of Styles"
                            sheet(0, i).Style = style1

                        Case Else
                            sheet.Item(0, i).Value = sqlDr.GetName(i).ToString
                            sheet(0, i).Style = style1
                    End Select
                Next
                i = 0
                While sqlDr.Read()

                    For j = 0 To sqlDr.FieldCount - 1
                        Select Case UCase(sqlDr.GetName(j).ToString)
                            Case "DW0", "DW1", "DW1B", "DW2", "DW3", "ORIGINALSTYLES", "ACTIVESTYLES"
                                sheet.Item(i + 1, j).Value = sqlDr.Item(j)
                            Case Else
                                sheet.Item(i + 1, j).Value = sqlDr.Item(j).ToString
                        End Select

                    Next
                    i = i + 1
                End While
            End If
            sqlDr.Close()
        End With
        sqlDA.Dispose()
        'For i = 0 To grdStyles.Columns.Count - 1
        'sheet.Item(0, i).Value = grdStyles.Columns(i).Caption
        'sheet(0, i).Style = style1
        'Next
        'For i = 0 To grdStyles.RowCount - 1
        'For j = 0 To grdStyles.Columns.Count - 1
        'sheet.Item(i + 1, j).Value = grdStyles.Columns(j).CellText(i)
        'Next
        'Next

        Try
            MyAttr = GetAttr(sAS_NAPARootFolder & "NAPAEXCEL")
        Catch
            MkDir(sAS_NAPARootFolder & "NAPAEXCEL")
        End Try

        C1XLBook1.Save(sAS_NAPARootFolder & "NAPAEXCEL\NAPALineList_" & UserId & "_" & Format(Now, "yyyyMMdd_hhmm") & ".xls")

        System.Diagnostics.Process.Start(sAS_NAPARootFolder & "NAPAEXCEL\NAPALineList_" & UserId & "_" & Format(Now, "yyyyMMdd_hhmm") & ".xls")
    End Sub

    Private Sub cmdUpdateFreeze_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdateFreeze.Click
        pnlUpdateFreeze.Visible = True
        pnlUpdateFreeze.BringToFront()
        cmbFreeze.Items.Add("Review")
        cmbFreeze.Items.Add("PLM")
        cmbFreeze.Items.Add("Editing Meeting")
        cmbFreeze.Items.Add("Start Of SC")
    End Sub

    Private Sub cmdCancelFreeze_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancelFreeze.Click
        pnlUpdateFreeze.Visible = False
    End Sub

    Private Sub cmdSaveFreeze_Click(sender As System.Object, e As System.EventArgs) Handles cmdSaveFreeze.Click
        Dim col As String = cmbFreeze.SelectedItem.ToString().Replace(" ", "")
        Dim myId As Integer = Val(Mid(ComboBox4.Text, 1, InStr(ComboBox4.Text, "/") - 2))
        For i As Integer = 0 To grdStyles.RowCount - 1
            If grdStyles.Columns("NoOfSyles").CellValue(i).ToString().Length > 0 Then
                grdStyles.Item(i, col) = grdStyles.Columns("NoOfSyles").CellValue(i)
                executeSQL("Update Selections set " & col & " = '" & grdStyles.Columns("NoOfSyles").CellValue(i) & "' Where Id = " & myId & " And Minor = '" & grdStyles.Item(i, "Minor") & "'")
            End If
        Next
        pnlUpdateFreeze.Visible = False
    End Sub
End Class