Public Class frmServerMoved
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "The Napa LineList is moved to a new server." & vbCrLf & "Please enter your IP address" & vbCrLf & "and press 'Send and Close'" & vbCrLf & "to be moved to the new server..."
        Form1_Resize(Nothing, Nothing)
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetUser()
        If (TextBox1.Text.Length > 0) Then
            SendSMTPMail(UserId & "@vfc.com", "meremap@vfc.com", "", "", "Move from a22 to ats03", "Move the shortcut on " & TextBox1.Text & " to the ATS03 server.")
        End If
        Me.Close()
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Label1.Width = Me.Width
        Label1.Height = Me.Height - 150
        Label1.Left = Me.Width / 2 - Label1.Width / 2
        TextBox1.Top = Label1.Height
        TextBox1.Left = Me.Width / 2 - TextBox1.Width / 2
        Button1.Top = Label1.Height + 20
        Button1.Left = Me.Width / 2 - Button1.Width / 2
    End Sub
End Class