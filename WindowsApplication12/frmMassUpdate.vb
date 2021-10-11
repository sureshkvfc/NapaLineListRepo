Imports System.Xml
Imports C1.C1Excel

Public Class frmMassUpdate
    Private m_xmld As XmlDocument = New XmlDocument()
    Private dsOld As DataSet = Nothing
    Private dsNew As DataSet = Nothing
    Private cboSearch0 As String = ""
    Private cboInto0 As String = ""
    Private b_updateDone As Boolean = False

    Private Sub frmMassUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tblView.Visible = False

        m_xmld.Load(sAS_StartupPath & "\PDMUpdateSettings.xml")

        Dim tblGroups As New System.Windows.Forms.TableLayoutPanel
        tblGroups.ColumnCount = 1
        tblGroups.RowCount = 0
        tblGroups.Dock = DockStyle.Fill
        tblGroups.Name = "tblGroups"
        Dim m_nodelist As XmlNodeList = m_xmld.SelectNodes("/Settings/Frames/Frame")
        For Each m_node As XmlNode In m_nodelist
            Dim grpNode As New System.Windows.Forms.GroupBox
            grpNode.Text = m_node.Attributes("Name").Value
            grpNode.Dock = DockStyle.Fill
            grpNode.Height = 15
            Dim tblNode As New System.Windows.Forms.TableLayoutPanel
            tblNode.Name = "tbl" & m_node.Attributes("Name").Value
            tblNode.ColumnCount = 2
            tblNode.RowCount = 0
            tblNode.Dock = DockStyle.Fill
            grpNode.Controls.Add(tblNode)
            Dim str_Table As String = m_node.SelectNodes("table").Item(0).InnerText
            Dim m_nodelistSub As XmlNodeList = m_node.SelectNodes("Fields/Field")
            For Each m_nodeSub As XmlNode In m_nodelistSub
                Dim lblNode As New System.Windows.Forms.Label
                lblNode.Text = m_nodeSub.SelectNodes("FldDesc").Item(0).InnerText
                lblNode.Tag = m_nodeSub.SelectNodes("FldSql").Item(0).InnerText
                lblNode.Visible = True
                lblNode.Dock = DockStyle.Fill
                Dim cboNode As New System.Windows.Forms.ComboBox
                Dim str_sql As String = "SELECT DISTINCT "
                Dim str_where As String = " WHERE "
                str_sql &= m_nodeSub.SelectNodes("FldSql").Item(0).InnerText & ", UPPER(" & m_nodeSub.SelectNodes("FldSql").Item(0).InnerText & ") "
                str_where &= " NOT " & m_nodeSub.SelectNodes("FldSql").Item(0).InnerText & " IS NULL "
                str_sql &= " FROM " & str_Table & str_where & " ORDER BY UPPER(" & m_nodeSub.SelectNodes("FldSql").Item(0).InnerText & ")"
                With cboNode
                    .DropDownStyle = ComboBoxStyle.DropDown
                    .DataSource = getSelectDataSet(str_sql).Tables(0) 'Replace(m_node.SelectNodes("sql").Item(0).InnerText, "%tag", m_nodeSub.ChildNodes.Item(1).InnerText)).Tables(0)
                    .DisplayMember = m_nodeSub.SelectNodes("FldSql").Item(0).InnerText
                    .ValueMember = m_nodeSub.SelectNodes("FldSql").Item(0).InnerText
                    .Name = "cbo" & m_node.Attributes("Name").Value & "|" & tblNode.RowCount & "|" & tblGroups.RowCount
                    .Tag = m_nodeSub.ChildNodes.Item(1).InnerText
                    .Dock = DockStyle.Fill
                    AddHandler .DropDownClosed, AddressOf combobox_DropDownClosed
                    AddHandler .LostFocus, AddressOf combobox_DropDownClosed
                End With
                tblNode.RowCount = tblNode.RowCount + 1
                tblNode.Controls.Add(lblNode, 0, tblNode.RowCount - 1)
                tblNode.Controls.Add(cboNode, 1, tblNode.RowCount - 1)
                grpNode.Height += cboNode.Height + 8
            Next
            tblGroups.RowCount = tblGroups.RowCount + 1
            tblGroups.Controls.Add(grpNode, 0, tblGroups.RowCount - 1)
        Next
        tblGroups.RowCount += 1
        Dim tblButtons As New System.Windows.Forms.TableLayoutPanel
        tblButtons.ColumnCount = 2 'IIf(IsAdmin, 3, 2)
        tblButtons.RowCount = 1
        tblButtons.Dock = DockStyle.Fill
        Dim cmdOK As New System.Windows.Forms.Button
        cmdOK.Name = "cmdOK"
        cmdOK.Text = "OK"
        AddHandler cmdOK.Click, AddressOf ButtonOK_Click
        tblButtons.Controls.Add(cmdOK, 0, 0)
        Dim cmdCancel As New System.Windows.Forms.Button
        cmdCancel.Name = "cmdCancel"
        cmdCancel.Text = "Cancel"
        AddHandler cmdCancel.Click, AddressOf ButtonCancel_Click
        tblButtons.Controls.Add(cmdCancel, 1, 0)
        'If IsAdmin Then
        'Dim cmdUndo As New System.Windows.Forms.Button
        'cmdUndo.Name = "cmdUndo"
        'cmdUndo.Text = "Undo"
        'AddHandler cmdUndo.Click, AddressOf ButtonUndo_Click
        'tblButtons.Controls.Add(cmdUndo, 2, 0)
        'End If
        tblGroups.Controls.Add(tblButtons, 0, tblGroups.RowCount - 1)
        Me.Controls.Add(tblGroups)


        m_nodelist = m_xmld.SelectNodes("/Settings/Frames/Frame")
        For Each m_node As XmlNode In m_nodelist
            Dim tblName As String = "tbl" & m_node.Attributes("Name").Value
            If Me.Controls.Find(tblName, True).Length > 0 Then
                Dim tblCntrl As TableLayoutPanel = Me.Controls.Find(tblName, True)(0)
                For i As Integer = 0 To tblCntrl.RowCount - 1
                    Dim str_name As String = "cbo" & m_node.Attributes("Name").Value & "|" & i & "|2"
                    If Me.Controls.Find(str_name, True).Length > 0 Then
                        Dim cntrl As ComboBox = Me.Controls.Find(str_name, True)(0)
                        cntrl.SelectedIndex = -1
                        cntrl.Text = ""
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub combobox_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case Split(sender.Name, "|")(0).Substring(3)
            Case "Search", "Into"
                sender.text = sender.text.toupper
                findMatch(sender, True, False)
        End Select
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim b_error As Boolean = False
        Me.Controls.Find("cmdOK", True)(0).Focus()
        If Me.Controls.Find("cboSearch|1", True).Length > 0 And b_error = False Then
            Dim cntrl As ComboBox = Me.Controls.Find("cboSearch|1", True)(0)
            If Not findMatch(cntrl, False, True) Then
                b_error = True
                MsgBox("Fields for Search do not match!", vbExclamation, "Update")
                cntrl.Focus()
            End If
        End If
        If Me.Controls.Find("cboInto|1", True).Length > 0 And b_error = False Then
            Dim cntrl As ComboBox = Me.Controls.Find("cboInto|1", True)(0)
            If Not findMatch(cntrl, False, True) Then
                MsgBox("Fields for Into do not match!", vbExclamation, "Update")
                cntrl.Focus()
            End If
        End If
        If Not b_error Then
            generateView()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Function findMatch(ByVal cboSender As ComboBox, ByVal b_setText As Boolean, ByVal b_checkMatch As Boolean) As Boolean
        If cboSender.Text.Length > 0 Then
            Dim i As String = IIf(Split(cboSender.Name, "|")(1) = 1, "0", "1")
            'If i = "1" And cboSender.Text.Length > 11 Then Str = cboSender.Text.Substring(3, 8)

            Dim xmlFrame As XmlNode = m_xmld.SelectNodes("/Settings/Frames/Frame").Item(Split(cboSender.Name, "|")(2))

            Dim str_sql As String = "SELECT UPPER(" & xmlFrame.SelectNodes("Fields/Field").Item(Split(cboSender.Name, "|")(1)).SelectNodes("FldValue").Item(0).InnerText & ") "
            Dim str_where As String = " WHERE " & xmlFrame.SelectNodes("Fields/Field").Item(Split(cboSender.Name, "|")(1)).SelectNodes("FldSql").Item(0).InnerText & " = '" & cboSender.Text & "'"
            str_sql &= " FROM " & xmlFrame.SelectNodes("table").Item(0).InnerText & str_where

            findMatch = False
            If Me.Controls.Find(Split(cboSender.Name, "|")(0).ToString & "|" & i & "|" & Split(cboSender.Name, "|")(2).ToString, True).Length > 0 Then
                Dim cntrl As ComboBox = Me.Controls.Find(Split(cboSender.Name, "|")(0).ToString & "|" & i & "|" & Split(cboSender.Name, "|")(2).ToString, True)(0)
                Dim dsResult As DataSet = getSelectDataSet(str_sql)
                If dsResult.Tables(0).Rows.Count > 0 Then
                    If dsResult.Tables(0).Rows(0).Item(0).ToString.Length > 0 Then
                        If b_setText Then cntrl.Text = dsResult.Tables(0).Rows(0).Item(0).ToString
                        If b_checkMatch Then findMatch = IIf(cntrl.Text = dsResult.Tables(0).Rows(0).Item(0).ToString, True, False)
                    Else
                        If b_setText Then cntrl.Text = ""
                        If b_checkMatch Then findMatch = IIf(cntrl.Text = dsResult.Tables(0).Rows(0).Item(0).ToString, True, False)
                    End If
                Else
                    If b_setText Then cntrl.Text = ""
                    If b_checkMatch Then findMatch = IIf(cntrl.Text = dsResult.Tables(0).Rows(0).Item(0).ToString, True, False)
                End If
                'If cntrl.DataSource.select(cntrl.Tag & " LIKE '" & str & "'").length > 0 Then
                'If b_setText Then cntrl.Text = cntrl.DataSource.select(cntrl.Tag & " LIKE '" & str & "'")(0)(cntrl.Tag)
                'If b_checkMatch Then findMatch = IIf(cntrl.Text = cntrl.DataSource.select(cntrl.Tag & " LIKE '" & str & "'")(0)(cntrl.Tag), True, False)
                'End If
            End If
        End If

        'If Not m_nodeSub.SelectNodes("FldValue").Count = 0 Then
        'str_sql &= ", " & m_nodeSub.SelectNodes("FldValue").Item(0).InnerText & ", UPPER(" & m_nodeSub.SelectNodes("FldValue").Item(0).InnerText & ") "
        'str_where &= " AND NOT " & m_nodeSub.SelectNodes("FldValue").Item(0).InnerText & " IS NULL "
        'End If
    End Function

    Private Sub generateView()
        tblView.Visible = True
        tblView.BringToFront()

        Dim str_sql As String = m_xmld.SelectNodes("/Settings/view/sql").Item(0).InnerText
        str_sql = "SELECT CAST(0 as BIT) as ' ', CAST(0 as BIT) as recUpdated , " & str_sql.Substring(6)

        'Search field in WHERE clause
        If Me.Controls.Find("cboSearch|0|0", True).Length > 0 Then
            Dim cntrl As ComboBox = Me.Controls.Find("cboSearch|0|0", True)(0)
            str_sql &= " WHERE " & cntrl.Tag & "='" & cntrl.Text & "'"
        End If

        'Add filter fields to WHERE clause
        Dim m_nodelist As XmlNodeList = m_xmld.SelectNodes("/Settings/Frames/Frame")
        For Each m_node As XmlNode In m_nodelist
            'If m_node.Attributes("isFilter").Value = "1" Then
            str_sql &= getWhereForFrame(m_node.Attributes("Name").Value, str_sql)
            'End If
        Next

        'Add ORDER BY to SQL
        str_sql &= " " & m_xmld.SelectNodes("/Settings/view/orderby").Item(0).InnerText

        'Fill Grid
        grdView.DataSource = Nothing
        If str_sql.ToUpper.IndexOf(" WHERE ") > 0 Then
            grdView.DataSource = getSelectDataSet(str_sql).Tables(0)
            For i As Integer = 1 To grdView.Splits(0).DisplayColumns.Count - 1
                grdView.Splits(0).DisplayColumns(i).Locked = True
            Next
            grdView.Splits(0).DisplayColumns("recUpdated").Visible = False
            grdView.FetchRowStyles = True
        End If

        'Fill Update textbox
        txtUpdate.Text = ""
        If Me.Controls.Find("cboSearch|0|0", True).Length > 0 Then
            Dim cntrl As ComboBox = Me.Controls.Find("cboSearch|0|0", True)(0)
            cboSearch0 = cntrl.Text
            dsOld = getSelectDataSet(Replace(Replace(m_xmld.SelectNodes("/Settings/view/valSQL").Item(0).InnerText, "%tag", cntrl.Tag), "%val", cntrl.Text))
        End If
        If Me.Controls.Find("cboInto|0|1", True).Length > 0 Then
            Dim cntrl As ComboBox = Me.Controls.Find("cboInto|0|1", True)(0)
            cboInto0 = cntrl.Text
            dsNew = getSelectDataSet(Replace(Replace(m_xmld.SelectNodes("/Settings/view/valSQL").Item(0).InnerText, "%tag", cntrl.Tag), "%val", cntrl.Text))
        End If
        m_nodelist = m_xmld.SelectNodes("/Settings/Update0/Fields/Field")
        If cboSearch0.Length > 0 And cboInto0.Length > 0 Then
            For Each m_nodelistUpdate0 As XmlNode In m_xmld.SelectNodes("/Settings/Updates/Update")
                txtUpdate.Text &= m_nodelistUpdate0.SelectNodes("Fields").Item(0).Attributes("table").Value & vbCrLf
                For Each m_node As XmlNode In m_nodelistUpdate0.SelectNodes("Fields/Field")
                    Dim str_field As String = m_node.SelectNodes("ArtFlds").Item(0).InnerText
                    txtUpdate.Text &= "    Replace " & str_field & " '" & dsOld.Tables(0).Rows(0).Item(str_field) & "' with '" & dsNew.Tables(0).Rows(0).Item(str_field) & "'" & vbCrLf
                Next
            Next
            m_nodelist = m_xmld.SelectNodes("/Settings/Frames/Frame")
            For Each m_node As XmlNode In m_nodelist
                If m_node.Attributes("Name").Value = "Extra" Then
                    Dim m_nodelistSub As XmlNodeList = m_node.SelectNodes("Fields/Field")
                    Dim i As Integer = 0
                    For Each m_nodeSub As XmlNode In m_nodelistSub
                        If Me.Controls.Find("cboExtra|" & i, True).Length > 0 Then
                            Dim cntrl As ComboBox = Me.Controls.Find("cboExtra|" & i, True)(0)
                            If cntrl.Text.Length > 0 Then
                                txtUpdate.Text &= "New " & Replace(m_nodeSub.ChildNodes.Item(0).InnerText, ":", "") & " = " & cntrl.Text & vbCrLf
                            End If
                        End If
                        i += 1
                    Next
                End If
            Next
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        tblView.Visible = False
    End Sub

    Private Function getWhereForFrame(ByVal str_frame As String, ByVal str_sql As String) As String
        getWhereForFrame = ""
        If Me.Controls.Find("tbl" & str_frame, True).Length > 0 Then
            Dim tblCntrl As TableLayoutPanel = Me.Controls.Find("tbl" & str_frame, True)(0)
            For i As Integer = 0 To tblCntrl.RowCount - 1
                Dim str_name As String = "cbo" & str_frame & "|" & i & "|2"
                If Me.Controls.Find(str_name, True).Length > 0 Then
                    If Not str_sql.IndexOf(" WHERE ") > 0 Then getWhereForFrame &= " WHERE "
                    Dim cntrl As ComboBox = Me.Controls.Find(str_name, True)(0)

                    getWhereForFrame &= IIf(cntrl.Text.Length > 0, " AND " & cntrl.Tag & "='" & cntrl.Text & "'", "")
                End If
            Next
        End If
    End Function

    Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click, btnDeselectAll.Click
        For i As Integer = 0 To grdView.RowCount - 1
            grdView.Item(i, 0) = sender.tag
        Next
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim C1XLBook As New C1.C1Excel.C1XLBook
        Dim sheet As XLSheet = C1XLBook.Sheets(0)
        Dim style As New XLStyle(C1XLBook)

        Dim fileDialog As New SaveFileDialog()
        With fileDialog
            .InitialDirectory = sAS_NAPARootFolder & "NAPAFiles\"
            .FileName = "NAPALineList_PDM_Update_" & UserId & "_" & Format(Now, "yyyyMMdd_hhmmss") & ".xls"
            .Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
            .FilterIndex = 1
            .RestoreDirectory = True
            .Title = "Export Excel file"
            If Not .ShowDialog() = DialogResult.OK Then Exit Sub
        End With

        style.Font = New Font("Tahoma", 9, FontStyle.Bold)

        Me.Cursor = Cursors.WaitCursor
        'Write column headers to excel
        For i As Integer = 0 To grdView.Splits(0).DisplayColumns.Count - 1
            sheet.Item(0, i).Value = grdView.Splits(0).DisplayColumns(i).DataColumn.Caption
            sheet.Item(0, i).Style = style
        Next
        For i As Integer = 0 To grdView.RowCount - 1
            For j As Integer = 0 To grdView.Splits(0).DisplayColumns.Count - 1
                sheet.Item(i + 1, j).Value = grdView.Item(i, j)
            Next
        Next

        C1XLBook.Save(fileDialog.FileName)
        C1XLBook.Clear()
        Me.Cursor = Cursors.Default
        System.Diagnostics.Process.Start(fileDialog.FileName)
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Not MsgBox("Are you sure you want to change the selected records ?", vbYesNo + vbDefaultButton2 + vbQuestion, "Update") = vbYes Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        Dim str_logFile As String = sAS_StartupPath & "\Logs\PDMUpdateLog.txt"
        writeToLog(str_logFile, "Update on " & Format(Now, "dd/mm/yyyy hh:mm:ss") & " by " & UserId)
        writeToLog(str_logFile, "____________________________________________")
        writeToLog(str_logFile, txtUpdate.Text)
        For i_grdRow As Integer = 0 To grdView.RowCount - 1
            Dim b_locked As Boolean = False
            If Not IsNothing(m_xmld.SelectNodes("/Settings/view").Item(0).Attributes("lockColumn")) Then
                If m_xmld.SelectNodes("/Settings/view").Item(0).Attributes("lockColumn").Value.Length > 0 Then
                    If grdView.Item(i_grdRow, m_xmld.SelectNodes("/Settings/view").Item(0).Attributes("lockColumn").Value.ToString).ToString.Length > 0 Then b_locked = True
                End If
            End If
            If grdView.Item(i_grdRow, 0).ToString = "True" And Not b_locked Then
                For Each m_nodelistUpdate0 As XmlNode In m_xmld.SelectNodes("/Settings/Updates/Update")
                    Dim str_sql As String = m_nodelistUpdate0.SelectNodes("sql").Item(0).InnerText & " "
                    Dim str_where As String = " WHERE "
                    Dim str_styles_where As String = " WHERE "
                    For Each m_node As XmlNode In m_nodelistUpdate0.SelectNodes("Fields/Field")
                        Dim str_value As String = dsNew.Tables(0).Rows(0).Item(m_node.ChildNodes.Item(0).InnerText).ToString
                        Select Case dsNew.Tables(0).Columns(m_node.ChildNodes.Item(0).InnerText).DataType.ToString
                            Case "System.Boolean"
                                str_sql &= m_node.SelectNodes("DesFlds").Item(0).InnerText & " = " & IIf(str_value = "True", "-1", "0") & ", "
                            Case "System.Int32", "System.Decimal"
                                str_sql &= m_node.SelectNodes("DesFlds").Item(0).InnerText & " = " & Replace(str_value, ",", ".") & ", "
                            Case Else
                                str_sql &= m_node.SelectNodes("DesFlds").Item(0).InnerText & " = '" & str_value & "', "
                        End Select
                    Next
                    If m_nodelistUpdate0.Attributes("XTR").Value = "1" Then
                        Dim m_nodelist = m_xmld.SelectNodes("/Settings/Frames/Frame")
                        For Each m_node As XmlNode In m_nodelist
                            If m_node.Attributes("Name").Value = "Extra" Then
                                Dim m_nodelistSub As XmlNodeList = m_node.SelectNodes("Fields/Field")
                                Dim j As Integer = 0
                                For Each m_nodeSub As XmlNode In m_nodelistSub
                                    If Me.Controls.Find("cboExtra|" & j, True).Length > 0 Then
                                        Dim cntrl As ComboBox = Me.Controls.Find("cboExtra|" & j, True)(0)
                                        If cntrl.Text.Length > 0 Then
                                            If m_nodelistSub.Item(2).InnerText = "NUM" Then
                                                str_sql &= cntrl.Tag & " = " & Replace(cntrl.Text, ",", ".") & ", "
                                            Else
                                                str_sql &= cntrl.Tag & " = '" & cntrl.Text & "', "
                                            End If
                                        End If
                                    End If
                                    j += 1
                                Next
                            End If
                        Next
                    End If

                    str_sql = str_sql.Substring(0, IIf(str_sql.LastIndexOf(",") = str_sql.Length - 2, str_sql.Length - 2, str_sql.Length)) & " "
                    Dim m_nodelistWhere As XmlNodeList = m_nodelistUpdate0.SelectNodes("sqlwhereView/Field")
                    Dim k As Integer = 1
                    For Each m_nodeSub As XmlNode In m_nodelistWhere
                        str_where &= m_nodeSub.SelectNodes("TABLEFLD").Item(0).InnerText & " = "
                        str_where &= "'" & grdView.Item(i_grdRow, m_nodeSub.SelectNodes("VWFLD").Item(0).InnerText) & "'"
                        str_where &= IIf(k < m_nodelistWhere.Count, " AND ", "")
                        k += 1
                    Next
                    str_sql &= str_where
                    'str_sql &= "'" & grdView.Item(i_grdRow, m_nodelistUpdate0.Item(0).SelectNodes("sqlvwfld").Item(0).InnerText) & "'"
                    'get oldvalues
                    Dim str_sql_tmp As String = str_sql.Substring(0, str_sql.ToUpper.IndexOf(" SET ")).ToUpper.Replace("UPDATE", "SELECT * FROM").ToString & str_sql.Substring(str_sql.ToUpper.IndexOf(" WHERE "))
                    Dim b_fout As Boolean = False
                    Dim str_sql_undo As String = ""
                    Dim dsTMP As DataSet = getSelectDataSet(str_sql_tmp)
                    If Not b_fout Then
                        If dsTMP.Tables(0).Rows.Count > 0 Then
                            writeToLog(str_logFile, str_sql)
                            For i_dsRow As Integer = 0 To dsTMP.Tables(0).Rows.Count - 1
                                str_sql_undo = str_sql.Substring(0, str_sql.ToUpper.IndexOf(" SET ") + 4)
                                For i_dsCol As Integer = 0 To dsTMP.Tables(0).Columns.Count - 1
                                    Select Case dsTMP.Tables(0).Columns(i_dsCol).DataType.ToString
                                        Case "System.DateTime"
                                            str_sql_undo &= dsTMP.Tables(0).Columns(i_dsCol).Caption & " = CONVERT(datetime,'" & Format(vbNullString & dsTMP.Tables(0).Rows(i_dsRow).Item(i_dsCol), "dd/mm/yyyy hh:mm:ss") & "','dd/mm/yyyy hh24:mi:ss'), "
                                        Case "System.Int32", "System.Decimal"
                                            str_sql_undo &= dsTMP.Tables(0).Columns(i_dsCol).Caption & " = " & Replace(dsTMP.Tables(0).Rows(i_dsRow).Item(i_dsCol), ",", ".") & ","
                                        Case Else
                                            str_sql_undo &= dsTMP.Tables(0).Columns(i_dsCol).Caption & " = '" & dsTMP.Tables(0).Rows(i_dsRow).Item(i_dsCol) & "',"
                                    End Select
                                Next
                                str_sql_undo = str_sql_undo.Substring(0, str_sql_undo.Length - 2) & str_sql.Substring(str_sql.ToUpper.IndexOf(" WHERE ")).Replace(cboSearch0, cboInto0)
                                str_sql_undo &= " and RECNO = '" & dsTMP.Tables(0).Rows(i_dsRow).Item("RecNo") & "'"
                                writeToLog(str_logFile, "Undo : " & str_sql_undo)
                            Next
                        End If
                    End If

                    If str_sql_undo.Length > 0 Then
                        'Debug.WriteLine("Execute: " & str_sql)
                        writeToLog(str_logFile, str_sql)
                        executeSQL(str_sql)

                        'trigger Lastchange in vf.styles
                        If m_nodelistUpdate0.SelectNodes("style").Item(0).InnerText.Length > 0 Then
                            Dim m_nodelistWhereStyle As XmlNodeList = m_nodelistUpdate0.SelectNodes("sqlwhereStyles/Field")
                            Dim l As Integer = 1
                            For Each m_nodeSub As XmlNode In m_nodelistWhereStyle
                                str_styles_where &= m_nodeSub.SelectNodes("TABLEFLD").Item(0).InnerText & " = "
                                str_styles_where &= "'" & grdView.Item(i_grdRow, m_nodeSub.SelectNodes("VWFLD").Item(0).InnerText) & "'"
                                str_styles_where &= IIf(l < m_nodelistWhereStyle.Count, " AND ", "")
                                l += 1
                            Next
                            'Debug.WriteLine("Execute: " & m_nodelistUpdate0.SelectNodes("style").Item(0).InnerText & " " & str_styles_where)
                            writeToLog(str_logFile, m_nodelistUpdate0.SelectNodes("style").Item(0).InnerText & " " & str_styles_where)
                            executeSQL(m_nodelistUpdate0.SelectNodes("style").Item(0).InnerText & " " & str_styles_where)
                        End If

                        'log
                        Dim str_comment As String = ""
                        If Me.Controls.Find("cboSearch|1|0", True).Length > 0 Then
                            Dim cntrl As ComboBox = Me.Controls.Find("cboSearch|1|0", True)(0)
                            str_comment = "Attention '" & cntrl.Text
                        End If
                        If Me.Controls.Find("cboInto|1|1", True).Length > 0 Then
                            Dim cntrl As ComboBox = Me.Controls.Find("cboInto|1|1", True)(0)
                            str_comment &= " changed to '" & cntrl.Text & "'"
                        End If

                        If m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("log").Value.Length > 0 Then
                            Dim dsLogMax As DataSet = getSelectDataSet(m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("maxnum").Value)
                            str_sql = m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("log").Value.Replace("%MAXLOG", IIf(dsLogMax.Tables(0).Rows(0).Item(0).ToString.Length = 0, 0, dsLogMax.Tables(0).Rows(0).Item(0)) + 1)
                            str_sql = str_sql.Replace("%KEY", grdView.Item(i_grdRow, m_nodelistWhere.Item(0).SelectNodes("VWFLD").Item(0).InnerText))
                            str_sql = str_sql.Replace("%OLINE", grdView.Item(i_grdRow, m_nodelistWhere.Item(1).SelectNodes("VWFLD").Item(0).InnerText))
                            str_sql = str_sql.Replace("%DATA", str_comment.Replace("'", """"))
                            str_sql = str_sql.Replace("%RECNO", dsTMP.Tables(0).Rows(0).Item("RecNo"))
                            'Debug.WriteLine("Execute: " & str_sql)
                            writeToLog(str_logFile, str_sql)
                            executeSQL(str_sql)
                        End If
                        If m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("logPDM").Value.Length > 0 Then
                            Dim dsLogMax As DataSet = getSelectDataSet(m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("maxnumPDM").Value)
                            str_sql = m_xmld.SelectNodes("/Settings/Log").Item(0).Attributes("logPDM").Value.Replace("%MAXLOG", IIf(dsLogMax.Tables(0).Rows(0).Item(0).ToString.Length = 0, 0, dsLogMax.Tables(0).Rows(0).Item(0)) + 1)
                            str_sql = str_sql.Replace("%USER", UserId)
                            str_sql = str_sql.Replace("%COMMENT", str_comment.Replace("'", """"))
                            str_sql = str_sql.Replace("%MAINKEY", grdView.Item(i_grdRow, m_nodelistWhere.Item(0).SelectNodes("VWFLD").Item(0).InnerText))
                            str_sql = str_sql.Replace("%NO_IN_ROW", grdView.Item(i_grdRow, m_nodelistWhere.Item(1).SelectNodes("VWFLD").Item(0).InnerText))
                            'Debug.WriteLine("Execute: " & str_sql)
                            writeToLog(str_logFile, str_sql)
                            executeSQL(str_sql)
                        End If
                        grdView.Item(i_grdRow, "recUpdated") = True
                    End If
                Next
            End If
        Next
        Me.Cursor = Cursors.Default
        MessageBox.Show("Update done!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        b_updateDone = True
        btnUpdate.Enabled = False
    End Sub

    Private Sub frmMassUpdate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        For Each cntrl As Control In Me.Controls
            If cntrl.Name.ToString = "tblGroups" Then
                setValueCombobox(cntrl, "")
            End If
        Next
    End Sub

    Private Sub setValueCombobox(ByVal mainControl As Control, ByVal str_value As String)
        For Each cntrl As Control In mainControl.Controls
            'Debug.WriteLine("=>" & cntrl.Name & " - " & cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(",")) & "<=")
            If cntrl.ToString.Substring(0, cntrl.ToString.IndexOf(",")) = "System.Windows.Forms.ComboBox" Then
                cntrl.Text = str_value
            Else
                setValueCombobox(cntrl, str_value)
            End If
        Next
    End Sub

    Private Sub grdView_FetchRowStyle(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs) Handles grdView.FetchRowStyle
        If grdView.RowCount = 0 Then Exit Sub
        With e.CellStyle
            If grdView.Item(e.Row, "recUpdated") = True Then
                .BackColor = Color.Aqua
            ElseIf grdView.Item(e.Row, 0) = True And b_updateDone Then
                .BackColor = Color.Red
            Else
                .BackColor = grdView.BackColor
            End If
        End With
    End Sub
End Class