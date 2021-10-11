Imports System.Data.SqlClient

Public Class frmCustomExcel
    Public updateExcel As Boolean = False

    Private Sub frmCustomExcel_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim templName As String

        If UpdateExcel = True Then
            UpdateExcel = False
            saveTemplate(frmMainGrid.ComboBox1.SelectedValue.ToString, frmMainGrid.ComboBox1.Text, False)
        Else
            templName = getTemplateName()
            If templName.Length > 0 Then
                Dim maxID As String = getSelectDataSet("SELECT MAX(ID)+1 FROM dbo.param_excelTemplates").Tables(0).Rows(0).Item(0).ToString

                saveTemplate(maxID, templName, True)
                frmMainGrid.fillTemplateCombo()
                frmMainGrid.ComboBox1.SelectedItem = maxID
            End If
        End If
    End Sub

    Private Sub frmCustomExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SearchData As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim userGroupsData As DataSet

        chkLUserGroups.Items.Clear()
        If UpdateExcel = True Then
            sql = "SELECT dbo.GridLayout.ColumnName, ISNULL(dbo.GridLayout.ColumnDescription, dbo.GridLayout.ColumnName) AS Description, dbo.SplitHeaders.Description AS [Belong To], " & _
                            "CAST(ISNULL((SELECT ToExcel FROM dbo.CustomExcel WHERE (templateID = " & frmMainGrid.ComboBox1.SelectedValue & ") AND (ColumnName = dbo.GridLayout.ColumnName)), 0) AS BIT) AS ToExcel, " & _
                            "CAST(ISNULL((SELECT showValue FROM dbo.CustomExcel WHERE (templateID = " & frmMainGrid.ComboBox1.SelectedValue & ") AND (ColumnName = dbo.GridLayout.ColumnName)), 0) AS BIT) AS showValue, " & _
                            "CAST(ISNULL((SELECT showDescription FROM dbo.CustomExcel WHERE (templateID = " & frmMainGrid.ComboBox1.SelectedValue & ") AND (ColumnName = dbo.GridLayout.ColumnName)), 1) AS BIT) AS showDescription, " & _
                            "CAST(ISNULL((SELECT seperator FROM dbo.CustomExcel WHERE (templateID = " & frmMainGrid.ComboBox1.SelectedValue & ") AND (ColumnName = dbo.GridLayout.ColumnName)), ' - ') AS NVARCHAR(10)) AS Seperator " & _
                        "FROM dbo.GridLayout LEFT OUTER JOIN  dbo.SplitHeaders ON dbo.GridLayout.Split = dbo.SplitHeaders.Split " & _
                        "WHERE (ISNULL(invisible,0) = 0 OR CAST(ISNULL((SELECT ToExcel FROM dbo.CustomExcel WHERE (templateID = " & frmMainGrid.ComboBox1.SelectedValue & ") AND (ColumnName = dbo.GridLayout.ColumnName)), 0) AS BIT) = 1) AND (UPPER(gridName) = 'GRDMAIN') AND (NOT (UPPER(dbo.GridLayout.ColumnName) LIKE 'FREEZE%')) AND (dbo.GridLayout.Split IN (" & getSplits(UserId) & ") AND columnName IN (SELECT columnID FROM gridPerUser_new WHERE userID='" & UserId & "' AND gridID = 'grdMain'))"
            userGroupsData = getSelectDataSet("SELECT DISTINCT Usergroup, CASE WHEN Usergroup IN (SELECT Usergroup FROM dbo.param_excelTemplates_access WHERE ID = " & frmMainGrid.ComboBox1.SelectedValue & ") THEN 1 ELSE 0 END AS checked FROM  dbo.Param_Users WHERE (NOT (Usergroup IS NULL))")
        Else
            sql = "SELECT dbo.GridLayout.ColumnName, ISNULL(dbo.GridLayout.ColumnDescription, dbo.GridLayout.ColumnName) AS Description, dbo.SplitHeaders.Description AS [Belong To], CAST(0 AS BIT) as ToExcel, CAST(0 AS BIT) as showValue, CAST(1 AS BIT) as showDescription, CAST(' - ' AS NVARCHAR(10)) AS Seperator " & _
                        "FROM dbo.GridLayout LEFT OUTER JOIN  dbo.SplitHeaders ON dbo.GridLayout.Split = dbo.SplitHeaders.Split " & _
                        "WHERE (ISNULL(invisible,0) = 0 OR CAST(ISNULL((SELECT ToExcel FROM dbo.CustomExcel WHERE (templateID = " & IIf(frmMainGrid.ComboBox1.Items.Count > 0, frmMainGrid.ComboBox1.SelectedValue, -1) & ") AND (ColumnName = dbo.GridLayout.ColumnName)), 0) AS BIT) = 1) AND  (UPPER(gridName) = 'GRDMAIN') AND (NOT (UPPER(dbo.GridLayout.ColumnName) LIKE 'FREEZE%')) AND (dbo.GridLayout.Split IN (" & getSplits(UserId) & ") )"
            userGroupsData = getSelectDataSet("SELECT DISTINCT Usergroup, 0 AS checked FROM  dbo.Param_Users WHERE (NOT (Usergroup IS NULL))")
        End If
        For i = 0 To userGroupsData.Tables(0).Rows.Count - 1
            chkLUserGroups.Items.Add(userGroupsData.Tables(0).Rows(i).Item("usergroup"), IIf(userGroupsData.Tables(0).Rows(i).Item("checked") = "1", True, False))
        Next
        If frmMainGrid.ComboBox1.Items.Count > 0 Then
            chkYouth.Checked = getSelectDataSet("SELECT ISNULL(ShowYouth,0) FROM dbo.param_excelTemplates WHERE ID=" & frmMainGrid.ComboBox1.SelectedValue).Tables(0).Rows(0).Item(0)
            chkIncludeNP.Checked = getSelectDataSet("SELECT ISNULL(ShowNP,0) FROM dbo.param_excelTemplates WHERE ID=" & frmMainGrid.ComboBox1.SelectedValue).Tables(0).Rows(0).Item(0)
        Else
            chkYouth.Checked = False
            chkIncludeNP.Checked = False
        End If

        SearchData.Clear()
        SearchData = Nothing
        SearchData = getSelectDataSet(sql)
        grdCustomExcel.DataSource = Nothing
        grdCustomExcel.DataSource = SearchData
        grdCustomExcel.DataMember = SearchData.Tables(0).ToString
        grdCustomExcel.Rebind(True)
        grdCustomExcel.Splits(0).DisplayColumns("ColumnName").Visible = False
        grdCustomExcel.FilterActive = True
        grdCustomExcel.FilterBar = True
    End Sub

    Private Sub frmCustomExcel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grdCustomExcel.Width = Me.Width - 25
        grdCustomExcel.Height = Me.Height - grdCustomExcel.Top - 55
    End Sub

    Private Function countChecked() As Integer
        Dim i As Integer

        countChecked = 0
        For i = 0 To grdCustomExcel.RowCount - 1
            countChecked += IIf(grdCustomExcel.Item(i, "ToExcel").ToString = "True", 1, 0)
        Next
    End Function

    Private Sub grdCustomExcel_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdCustomExcel.AfterColUpdate
        'Debug.WriteLine(grdCustomExcel.Columns(grdCustomExcel.Col).DataField)
        If grdCustomExcel.Columns(grdCustomExcel.Col).DataField = "ToExcel" Then
            If grdCustomExcel.Item(grdCustomExcel.Row, grdCustomExcel.Col).ToString = "True" And countChecked() > 255 Then
                grdCustomExcel.Item(grdCustomExcel.Row, grdCustomExcel.Col) = False
                MsgBox("You can only select a maximum of 255 columns to export to Excel", MsgBoxStyle.Critical, "To many rows")
            End If
        End If
    End Sub

    Private Function getSplits(ByVal userNaam As String) As String
        Dim splitShow As DataSet = getSelectDataSet("SELECT 'Show'+DBField as fieldDB, Split From SplitHeaders ORDER BY Split")
        Dim i As Integer
        getSplits = "0"
        For i = 0 To splitShow.Tables(0).Rows.Count - 1
            Dim bCheck As Boolean = False
            Dim val As Object = getSelectDataSet("SELECT " & splitShow.Tables(0).Rows(i).Item("fieldDB") & " FROM param_users WHERE usernaam= '" & userNaam & "'").Tables(0).Rows(0).Item(0)
            If Not IsDBNull(val) Then
                If Not CBool(val) = False Then
                    bCheck = True
                End If
            End If
            If bCheck Then getSplits &= IIf(getSplits.Length > 0, ", ", "") & splitShow.Tables(0).Rows(i).Item("Split")
        Next
    End Function

    Private Function getTemplateName() As String
        Dim searchData As DataSet
        Dim iAnswer As Integer
        Dim templName As String = InputBox("Give a Name for this Template (Use only Letters):", "Template Name")

        If Not templName.ToString.Length = 0 Then
            searchData = getSelectDataSet("SELECT Distinct TemplateName from CustomExcel Where Username ='" & UserId & "' And TemplateName = '" & templName & "'")
            If searchData.Tables(0).Rows.Count > 0 Then
                iAnswer = MsgBox("This Template already Exist, Should I Overwrite?", MsgBoxStyle.YesNoCancel)
            Else
                iAnswer = vbYes
            End If
        Else
            iAnswer = vbCancel
        End If

        Select Case iAnswer
            Case vbYes
                executeSQL("Delete From CustomExcel where UserName ='" & UserId & "' And TemplateName = '" & templName & "'")
                getTemplateName = templName
            Case vbNo
                getTemplateName = getTemplateName()
            Case Else
                getTemplateName = ""
        End Select
    End Function

    Private Sub saveTemplate(ByVal templateID As String, ByVal templateName As String, Optional ByVal newTemplate As Boolean = False)
        Dim i As Integer

        'Save the template properties
        If newTemplate Then
            executeSQL("INSERT INTO dbo.param_excelTemplates VALUES (" & templateID & ",'" & templateName & "','" & UserId & "'," & IIf(chkYouth.Checked, 1, 0) & "," & IIf(chkIncludeNP.Checked, 1, 0) & ")")
        Else
            executeSQL("UPDATE dbo.param_excelTemplates SET ShowYouth=" & IIf(chkYouth.Checked, 1, 0) & ", ShowNP=" & IIf(chkIncludeNP.Checked, 1, 0) & " WHERE ID=" & templateID)
        End If
        'Save the template access rights
        If Not newTemplate Then
            executeSQL("DELETE FROM dbo.param_excelTemplates_access WHERE ID=" & templateID)
            executeSQL("Delete From CustomExcel where TemplateID = " & frmMainGrid.ComboBox1.SelectedValue)
        End If
        For i = 0 To chkLUserGroups.CheckedItems.Count - 1
            executeSQL("INSERT INTO dbo.param_excelTemplates_access (ID, userGroup, Brand) VALUES (" & templateID & ",'" & chkLUserGroups.CheckedItems.Item(i).ToString & "','N')")
        Next

        'Save the template columns
        For i = 0 To grdCustomExcel.Splits(0).DisplayColumns.Count - 1
            grdCustomExcel.Splits(0).DisplayColumns(i).DataColumn.FilterText = ""
        Next

        For i = 0 To grdCustomExcel.RowCount - 1
            If grdCustomExcel.Item(i, "ToExcel").ToString = "True" Then
                'Debug.WriteLine("INSERT INTO CustomExcel (UserName, ColumnName, ColumnDescription, ToExcel, TemplateName, TemplateID) " & _
                '            "VALUES ('" & UserId & "', '" & grdCustomExcel.Item(i, "ColumnName") & "', '" & grdCustomExcel.Item(i, "Description") & "'" & _
                '                        ", " & IIf(grdCustomExcel.Item(i, "ToExcel").ToString = "True", 1, 0) & ", '" & templateName & "'," & templateID & ")")
                executeSQL("INSERT INTO CustomExcel (UserName, ColumnName, ColumnDescription, ToExcel, TemplateName, TemplateID, Brand, showValue, showDescription, seperator) " & _
                            "VALUES ('" & UserId & "', '" & grdCustomExcel.Item(i, "ColumnName") & "', '" & grdCustomExcel.Item(i, "Description") & "'" & _
                                        ", " & IIf(grdCustomExcel.Item(i, "ToExcel").ToString = "True", 1, 0) & ", '" & templateName & "'," & templateID & ",'N'" & _
                                        ", " & IIf(grdCustomExcel.Item(i, "showValue").ToString = "True", 1, 0) & _
                                        ", " & IIf((grdCustomExcel.Item(i, "showDescription").ToString = "True") Or (Not grdCustomExcel.Item(i, "showValue").ToString = "True" And Not grdCustomExcel.Item(i, "showDescription").ToString = "True"), 1, 0) & _
                                        ", '" & grdCustomExcel.Item(i, "Seperator").ToString & "'" & _
                                    ")")
            End If
        Next
        frmMainGrid.fillTemplateCombo()
        frmMainGrid.ComboBox1.SelectedValue = templateID
    End Sub
End Class