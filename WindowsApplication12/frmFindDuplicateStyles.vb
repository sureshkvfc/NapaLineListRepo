Public Class frmFindDuplicateStyles
    Private clrInNewGrid As Color = Color.DarkGray

    Private Sub frmFindDuplicateStyles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label2.BackColor = clrInNewGrid
        RichTextBox1.Width = Me.Width - 20
        Dim iTop As Integer = RichTextBox1.Top + RichTextBox1.Height + 10

        Dim tblDuplicates As DataTable = getSelectDataSet("SELECT [MOC008], [KEY2] FROM [QUESTPDMNAPA].[dbo].[STYLES] GROUP BY [MOC008], [KEY2] HAVING COUNT([MOC008]) > 1").Tables(0)
        If tblDuplicates.Rows.Count > 0 Then
            For Each rowDuplicates As DataRow In tblDuplicates.Rows
                Dim pnlDuplicate As TableLayoutPanel = New TableLayoutPanel()
                With pnlDuplicate
                    .Top = iTop
                    .Width = Me.Width
                    .Height = 0
                    .BackColor = Color.Silver
                    .ColumnCount = 2

                    Dim tblIsInNewGrid As DataTable = getSelectDataSet("SELECT Name, JbaNo, DevNo FROM dbo.NewGrid WHERE Name='" & rowDuplicates(0) & "'").Tables(0)
                    Dim tblDuplicatesDetails As DataTable = getSelectDataSet("SELECT [KEY2],[MAINKEY],[MOC015],[MOC008],[MOC091] FROM [QUESTPDMNAPA].[dbo].[STYLES] WHERE [MOC008]='" & rowDuplicates(0) & "' AND [Key2]='" & rowDuplicates(1) & "'").Tables(0)
                    If tblDuplicatesDetails.Rows.Count > 0 Then
                        For Each rowDuplicatesDetails As DataRow In tblDuplicatesDetails.Rows
                            Dim radioDuplicatesDetails As RadioButton = New RadioButton()
                            .Height += 26
                            .RowCount += 1
                            radioDuplicatesDetails.Tag = rowDuplicatesDetails("MAINKEY")
                            .Controls.Add(radioDuplicatesDetails, 0, .RowCount - 1)

                            Dim lblDuplicatesDetails As Label = New Label()
                            lblDuplicatesDetails.Width = Me.Width - 150
                            lblDuplicatesDetails.Text = ""
                            lblDuplicatesDetails.TextAlign = ContentAlignment.MiddleLeft
                            For i As Integer = 0 To tblDuplicatesDetails.Columns.Count - 1
                                lblDuplicatesDetails.Text &= rowDuplicatesDetails(i).ToString()
                                If i < tblDuplicatesDetails.Columns.Count - 1 Then lblDuplicatesDetails.Text &= " - "
                            Next
                            If tblIsInNewGrid.Rows.Count > 0 Then
                                If tblIsInNewGrid.Rows(0).Item("JbaNo").ToString().Substring(3, 3) = rowDuplicatesDetails("MAINKEY").ToString().Split("*")(1).Substring(3, 3) Then
                                    lblDuplicatesDetails.BackColor = clrInNewGrid
                                    radioDuplicatesDetails.Tag = "1~" & radioDuplicatesDetails.Tag
                                Else
                                    radioDuplicatesDetails.Tag = "0~" & radioDuplicatesDetails.Tag
                                End If
                            End If
                            .Controls.Add(lblDuplicatesDetails, 1, .RowCount - 1)
                        Next
                    End If
                End With
                Me.Controls.Add(pnlDuplicate)
                iTop += pnlDuplicate.Height + 5
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strTables As String() = getMainKeyTables()
        For Each cntrl As Control In Me.Controls
            If cntrl.ToString().Split(",")(0) = "System.Windows.Forms.TableLayoutPanel" Then
                For Each subCntrl As Control In cntrl.Controls
                    If subCntrl.ToString().Split(",")(0) = "System.Windows.Forms.RadioButton" Then
                        Dim radioCntrl As RadioButton = subCntrl
                        If radioCntrl.Checked Then
                            Dim bInNewGrid As Boolean = IIf(radioCntrl.Tag.ToString().Split("~")(0) = "1", True, False)
                            Dim strMainKey As String = radioCntrl.Tag.ToString().Split("~")(1)
                            Dim radioControlOther As RadioButton = New RadioButton()
                            For Each SubCntrolOther As Control In cntrl.Controls
                                If SubCntrolOther.ToString().Split(",")(0) = "System.Windows.Forms.RadioButton" Then
                                    If Not SubCntrolOther.Tag = radioCntrl.Tag Then
                                        radioControlOther = SubCntrolOther
                                    End If
                                End If
                            Next
                            Dim strMainKeyOther As String = radioControlOther.Tag.ToString().Split("~")(1)

                            If bInNewGrid Then
                                For Each strTable As String In strTables
                                    If strTable.Length > 0 Then
                                        RichTextBox1.Text &= "DELETE FROM QUESTPDMNAPA.dbo." & strTable & " WHERE MAINKEY='" & strMainKeyOther & "'" & vbCrLf
                                    End If
                                Next
                            Else
                                For Each strTable As String In strTables
                                    If strTable.Length > 0 Then
                                        RichTextBox1.Text &= "DELETE FROM QUESTPDMNAPA.dbo." & strTable & " WHERE MAINKEY='" & strMainKey & "'" & vbCrLf
                                        RichTextBox1.Text &= "UPDATE QUESTPDMNAPA.dbo." & strTable & " SET MAINKEY='" & strMainKey & "' WHERE MAINKEY='" & strMainKeyOther & "'" & vbCrLf
                                    End If
                                Next
                            End If
                            RichTextBox1.Text &= "___________________________________________________________________" & vbCrLf
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Function getMainKeyTables() As String()
        Dim tblTables As DataTable = getSelectDataSet("USE QUESTPDMNAPA; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS AS COLUMNS_1 WHERE(TABLE_NAME IN (SELECT name FROM  sys.tables)) AND (COLUMN_NAME = 'MAINKEY')").Tables(0)
        Dim strTables As String = ""
        For Each rowTables As DataRow In tblTables.Rows
            strTables += rowTables(0) & "~"
        Next
        executeSQL("USE NAPA")
        Return strTables.Split("~")
    End Function
End Class