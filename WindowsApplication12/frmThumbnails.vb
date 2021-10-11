Public Class frmThumbnails
    Private picDataSet As DataSet
    Private rowCounter As Integer
    Private picAndLabelA() As usrPictureAndLabel
    Private picsH As Integer = 0
    Private picsV As Integer = 0

    Private Sub frmThumbnails_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMainGrid.btnRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub frmThumbnails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        picDataSet = getSelectDataSet("SELECT * FROM dbo.tmp_NewGridAfterFilter WHERE userNaam='" & UserId & "'")
        rowCounter = 0
        If picDataSet.Tables(0).Rows.Count > 0 Then
            alterZoom(0)
        End If
    End Sub

    Private Sub alterZoom(ByVal zoomLevel As Integer)
        Dim itemNr As Integer
        For itemNr = 0 To (picsH * picsV) - 1
            Me.Controls.Remove(picAndLabelA(itemNr))
        Next
        Select Case zoomLevel
            Case 0
                fillFormWithPictures(1, 1)
            Case 1
                fillFormWithPictures(2, 1)
            Case 2
                fillFormWithPictures(2, 2)
            Case 3
                fillFormWithPictures(3, 2)
            Case 4
                fillFormWithPictures(3, 3)
            Case 5
                fillFormWithPictures(4, 3)
            Case 6
                fillFormWithPictures(4, 4)
            Case 7
                fillFormWithPictures(5, 4)
            Case 8
                fillFormWithPictures(5, 5)
            Case 9
                fillFormWithPictures(6, 5)
            Case 10
                fillFormWithPictures(6, 6)
        End Select
    End Sub

    Private Sub fillFormWithPictures(ByVal i_picsH As Integer, ByVal i_picsV As Integer)
        Dim itemNr As Integer
        Dim picAndLabel As usrPictureAndLabel
        ReDim picAndLabelA(i_picsH * i_picsV)

        picsH = i_picsH
        picsV = i_picsV

        For itemNr = 0 To (picsH * picsV) - 1
            picAndLabel = New usrPictureAndLabel
            picAndLabel.Parent = Me
            picAndLabel.Visible = True
            picAndLabelA(itemNr) = picAndLabel
            AddHandler picAndLabelA(itemNr).uMouseDown, AddressOf picAndLabelA_MouseDown
            AddHandler picAndLabelA(itemNr).uDoubleClick, AddressOf picAndLabelA_DoubleClick
            'rowCounter += 1
        Next
        btnClick(0)
    End Sub

    Private Sub picAndLabelA_MouseDown(ByVal uControl As Object, ByVal StyleName As String, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            uControl.ContextMenuStrip = New ContextMenuStrip
            If userGroup = "SUPER" Then
                uControl.ContextMenuStrip.Items.Add("Update Picture", Nothing, New EventHandler(AddressOf mnuPicAndLabelA_mnuClick)).Tag = uControl.devno
                uControl.ContextMenuStrip.Items.Add("Remove Picture", Nothing, New EventHandler(AddressOf mnuPicAndLabelA_mnuClick)).Tag = uControl.devno
            End If
            uControl.ContextMenuStrip.Items.Add("Go To Style", Nothing, New EventHandler(AddressOf mnuPicAndLabelA_mnuClick)).Tag = uControl.devno
        End If
    End Sub

    Private Sub mnuPicAndLabelA_mnuClick(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        Select Case sender.Text
            Case "Update Picture"
                If MsgBox("This will update the picture on the main screen in Quest." & vbCrLf & "Do you want to continue?", vbYesNo, "Update Picture") = MsgBoxResult.Yes Then
                    Dim str_picture As String = getNewPicture(sender.Tag)
                    If Not str_picture = "" Then
                        executeSQL("UPDATE questpdmnapa.dbo.STYLES set pictmoda01='" & str_picture & "' WHERE moc091=" & sender.Tag)
                        btnClick(0)
                    End If
                End If
            Case "Remove Picture"
                If MsgBox("This will update the picture on the main screen in Quest." & vbCrLf & "Do you want to continue?", vbYesNo, "Update Picture") = MsgBoxResult.Yes Then
                    If MsgBox("Are you sure you want to remove the picture?", MsgBoxStyle.YesNo, "Remove Picture") = MsgBoxResult.Yes Then
                        executeSQL("UPDATE NewGrid SET Picture=NULL, pictureDate=NULL WHERE DevNo ='" & sender.Tag & "'")
                        executeSQL("DELETE FROM NAPA.dbo.vf_Sketches WHERE Lotnumber ='" & sender.Tag & "'")
                        executeSQL("UPDATE questpdmnapa.dbo.STYLES set pictmoda01='NULL' WHERE moc091=" & sender.Tag)
                        btnClick(0)
                    End If
                End If
            Case "Go To Style"
                Dim i As Integer
                For i = frmMainGrid.grdMain.FirstRow To frmMainGrid.grdMain.RowCount - 1
                    If frmMainGrid.grdMain.Item(i, "DevNo") = sender.Tag Then
                        Me.Close()
                        frmMainGrid.grdMain.Bookmark = i
                        Exit For
                    End If
                Next
        End Select
    End Sub

    Private Sub picAndLabelA_DoubleClick(ByVal uControl As Object, ByVal StyleName As String)
        rowCounter = uControl.itemnr
        alterZoom(0)
        trackBar.Value = 0
    End Sub

    Private Sub frmThumbnails_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim itemNr As Integer
        Dim iTop As Integer
        Dim iLeft As Integer

        btnPrevious.Left = 5
        btnPrevious.Top = (Me.Height - btnPrevious.Height) / 2
        btnNext.Left = btnPrevious.Left + btnPrevious.Width + 10
        btnNext.Top = btnPrevious.Top
        trackBar.Top = 5
        trackBar.Left = btnPrevious.Left
        trackBar.Height = btnPrevious.Top - trackBar.Top - 10

        If picsH > 0 And picsV > 0 Then
            For itemNr = 0 To (picsH * picsV) - 1
                picAndLabelA(itemNr).Width = (Me.Width - btnNext.Left - btnNext.Width - 20 - (picsH * 10)) / picsH
                picAndLabelA(itemNr).Height = (Me.Height - 20 - (picsV * 10)) / picsV
                If itemNr = 0 Then
                    iLeft = btnNext.Left + btnNext.Width + 10
                    iTop = 10
                ElseIf itemNr Mod picsH = 0 Then
                    iLeft = btnNext.Left + btnNext.Width + 10
                    iTop += picAndLabelA(itemNr).Height
                Else
                    iLeft += picAndLabelA(itemNr).Width
                End If
                picAndLabelA(itemNr).Left = iLeft
                picAndLabelA(itemNr).Top = iTop
            Next
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        btnClick(1)
    End Sub
    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        btnClick(-1)
    End Sub

    Private Sub btnClick(ByVal i_move As Integer)
        Dim itemNr As Integer

        rowCounter += picsH * picsV * i_move
        If rowCounter < 0 Then rowCounter = 0

        If rowCounter - (picsH * picsV) >= 0 Or rowCounter > 0 Then
            btnPrevious.Visible = True
        Else
            btnPrevious.Visible = False
        End If
        If rowCounter + (picsH * picsV) <= picDataSet.Tables(0).Rows.Count - 1 Then
            btnNext.Visible = True
        Else
            btnNext.Visible = False
        End If

        For itemNr = 0 To (picsH * picsV) - 1
            If (rowCounter + itemNr) < picDataSet.Tables(0).Rows.Count Then
                picAndLabelA(itemNr).updateStyle(picDataSet.Tables(0).Rows(rowCounter + itemNr).Item("DevNo"), picDataSet.Tables(0).Rows(rowCounter + itemNr).Item("styleNaam"), rowCounter + itemNr)
                picAndLabelA(itemNr).Visible = True
            Else
                picAndLabelA(itemNr).Visible = False
            End If
        Next
        frmThumbnails_Resize(Nothing, Nothing)
    End Sub

    Private Sub trackBar_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackBar.ValueChanged
        alterZoom(trackBar.Value)
    End Sub
End Class