Imports System.IO
Imports System.Data.SqlClient

'Use:
'       SendSMTPMail FromAddress, ToAddresses, CCAddresses, BCCAddresses, Subject, Body (, Attachments, AttachmentDelimiter)
'   ex: SendSMTPMail "Application@vfc.com", "Recip1@vfc.com,Recip2@vfc.com", "RecipCC1@vfc.com,RecipCC2@vfc.com", "RecipBCC1@vfc.com,RecipBCC2@vfc.com", "mail subject", "mail body, containing the body of the e-mail", "c:\text.txt~c:\text2.txt", "~"
'   ex: SendSMTPMail "Application@vfc.com", "Recip1@vfc.com", "", "", "mail subject", "mail body, containing the body of the e-mail"

Module VBNET_SMTPMail
    Private connSQL As New SqlClient.SqlConnection("data source=vebb2asql03\live;initial catalog=VFRepository;user id=Appl_sa;password=Appl2011")

    Public Sub SendSMTPMail(ByVal str_From As String, ByVal str_To As String, ByVal str_CC As String, ByVal str_BCC As String, ByVal str_subject As String, ByVal str_message As String, Optional ByVal str_file As String = "", Optional ByVal str_file_delimiter As String = "~")
        Dim ID As Integer '= getSelectDataSet("SELECT MAX(ID)+1 FROM tbl_SMTPMail").Tables(0).Rows(0).Item(0)

        If connSQL.State = ConnectionState.Closed Then connSQL.Open()
        Dim cmdSQL As SqlCommand = New SqlCommand("INSERT INTO tbl_SMTPMail (dateTransmitted, fromAddress, toAddress, ccAddress, bccAddress, subject, message) VALUES (CONVERT(datetime,'" & Format(Now(), "yyyyMMdd HH:mm:ss") & "'),@str_From,@str_To,@str_CC,@str_BCC,@str_subject,@str_message)", connSQL)
        cmdSQL.CommandType = CommandType.Text
        With cmdSQL.Parameters
            .Clear()
            .AddWithValue("@str_From", str_From)
            .AddWithValue("@str_To", str_To)
            .AddWithValue("@str_CC", str_CC)
            .AddWithValue("@str_BCC", str_BCC)
            .AddWithValue("@str_subject", str_subject)
            .AddWithValue("@str_message", str_message)
        End With
        cmdSQL.ExecuteNonQuery()
        cmdSQL.Parameters.Clear()
        cmdSQL.CommandText = "SELECT @@IDENTITY"
        ID = Convert.ToInt32(cmdSQL.ExecuteScalar())
        cmdSQL.Dispose()

        If str_file.Length > 0 Then
            Dim strFiles() As String = Split(str_file, IIf(str_file_delimiter.Length > 0, str_file_delimiter, "~"))
            For Each strFile As String In strFiles
                SaveFileToSQLTable(ID, strFile)
            Next
        End If
    End Sub

    Private Sub SaveFileToSQLTable(ByVal ID As String, ByVal strFileName As String)
        If File.Exists(strFileName) = False Then Exit Sub

        Dim curImage As Image = Nothing
        Dim fs As FileStream = New FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Read)
        Dim rawData() As Byte = New Byte(fs.Length) {}
        fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length))
        fs.Close()
        Dim cmdSQL As New SqlCommand

        If connSQL.State = ConnectionState.Closed Then connSQL.Open()
        cmdSQL = New SqlCommand("INSERT INTO tbl_SMTPMail_Attachments(ID, fileName, attachment) VALUES (@ID, @filename, @bFile)", connSQL)
        cmdSQL.CommandType = CommandType.Text
        With cmdSQL.Parameters
            .Clear()
            .AddWithValue("@ID", ID)
            .AddWithValue("@filename", strFileName.Substring(strFileName.LastIndexOf("\") + 1))
            .AddWithValue("@bFile", rawData)
        End With
        cmdSQL.ExecuteNonQuery()
        cmdSQL.Dispose()
    End Sub

    Private Sub executeSQL(ByVal str_sql As String)
        Dim adapter As New SqlDataAdapter
        Try
            If connSQL.State = ConnectionState.Closed Then connSQL.Open()
            adapter.UpdateCommand = connSQL.CreateCommand
            adapter.UpdateCommand.CommandText = str_sql
            adapter.UpdateCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function getSelectDataSet(ByVal str_sql As String) As DataSet
        Dim SelectAdapter As New SqlDataAdapter
        Dim SelectData As New DataSet

        If connSQL.State = ConnectionState.Closed Then connSQL.Open()
        SelectAdapter.SelectCommand = connSQL.CreateCommand
        SelectAdapter.SelectCommand.CommandText = str_sql
        SelectAdapter.Fill(SelectData)
        SelectData.CreateDataReader()
        getSelectDataSet = SelectData
    End Function
End Module
