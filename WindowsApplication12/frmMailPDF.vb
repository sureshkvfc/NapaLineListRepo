Imports System.Data.SqlClient
Imports System.IO

Public Class frmMailPDF
    Public contractorsAdapter As New SqlDataAdapter
    Public contractorsData As New DataSet
    Public contractorsBuilder As SqlCommandBuilder

    Public patternAdapter As New SqlDataAdapter
    Public patternData As New DataSet
    Public patternBuilder As SqlCommandBuilder

    Private Const DownloadPath = "\\vebb2a15\wwwroot\questpdmpdf\download\"
    Private Const RepPath = "\\vebb2a15\wwwroot\vfdms\repository\QPDM\"

    Private Sub btnFTP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFTP.Click
        Me.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        File1.Pattern = "*.PDF"
        File1.Path = DownloadPath
        File1.Refresh()
        While File1.Items.Count > 0
            Threading.Thread.Sleep(10000)
            File1.Refresh()
        End While

        VFdms_Run()
        'UpdateOutofSyncPDFS()
        Me.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmMailPDF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        List1.Visible = False
        txtStartdate.Text = "04/03/10 00:00:00"
        Dim str_Startdate As String = String.Format(255, 0)
        If False Then 'If comline = "DIRECTFTP" Then
            btnPrintserver.Enabled = False
            btnRetrieveOrders.Enabled = False
            btnFTP.Enabled = False
        End If
        'lng_l = GetPrivateProfileString("GLOBAL", "PrintServer", vbNullString, str_Startdate, 255, inifile)
        'If lng_l = 0 Then
        'str_Startdate = txtStartdate.Text
        'Else
        'str_Startdate = VBA.Left(str_Startdate, lng_l)
        'txtStartdate.Text = str_Startdate
        'End If
        'str_tmp = VBA.String(255, 0)
        'lng_l = GetPrivateProfileString("GLOBAL", "LastFTP", vbNullString, str_tmp, 255, inifile)
        'If lng_l = 0 Then
        'txtLastFTP.Text = vbNullString
        'Else
        'str_tmp = VBA.Left(str_tmp, lng_l)
        'txtLastFTP.Text = str_tmp
        'End If

        Dir1.Path = txtPath.Text

        tmrPrintserver_Tick(Nothing, Nothing)

        'Add seasons from Quest to param_seasons in VFLinesheets
        executeSQL("INSERT INTO vflinesheets.dbo.param_season (code , questuse,questactive) " & _
                     "select distinct key2, 1 ,1 from QUESTPDMNAPA.dbo.styles WHERE ISNULL(key2,'') <> '' AND " & _
                         "key2 not in (select code from vflinesheets.dbo.param_season with (nolock))")

        Dim dtaSeasons As DataSet = getSelectDataSet("SELECT * FROM vfspecstar.dbo.questseasons WHERE Active=1")
        Dim ActSsn As String = ""
        For Each rowSeason As DataRow In dtaSeasons.Tables(0).Rows
            ActSsn = ActSsn & ",'" & rowSeason.Item("Season") & "'"
        Next
        ActSsn = Mid(ActSsn, 2)

        contractorsData = Nothing
        contractorsData = New DataSet
        contractorsAdapter.SelectCommand = dcNAPA.CreateCommand
        contractorsAdapter.SelectCommand.CommandText = "select * from VFSPECSTAR.dbo.QuestSuppliers with (nolock) order by SupplierName"
        contractorsBuilder = New SqlCommandBuilder(contractorsAdapter)
        contractorsAdapter.Fill(contractorsData)
        contractorsData.CreateDataReader()
        With grdContractors
            .DataSource = contractorsData
            .DataMember = contractorsData.Tables(0).ToString
            .Rebind(False)
            .AllowAddNew = True
            .AllowDelete = True
            .AllowUpdate = True
        End With

        'Select Case comline
        '    Case "PRINTSERVER"
        '    Case Else
        'TimerPrintserver.Enabled = True
        'End Select

        executeSQL("INSERT INTO vfpattern.dbo.refnames (PatternName) " & _
                    " SELECT distinct rtrim(vfpattern.dbo.PATTERN.Plant) as Plant " & _
                    " FROM vfpattern.dbo.PATTERN with (nolock) LEFT OUTER JOIN " & _
                    " vfpattern.dbo.RefNames with (nolock) ON vfpattern.dbo.PATTERN.Plant = vfpattern.dbo.RefNames.PatternName " & _
                    " Where (vfpattern.dbo.RefNames.PatternName Is Null) ")


        patternData = Nothing
        patternData = New DataSet
        patternAdapter.SelectCommand = dcNAPA.CreateCommand
        patternAdapter.SelectCommand.CommandText = "SELECT * FROM vfpattern.dbo.Refnames WITH (nolock) ORDER BY PatternName"
        patternBuilder = New SqlCommandBuilder(patternAdapter)
        patternAdapter.Fill(patternData)
        patternData.CreateDataReader()
        With grdRefnames
            .DataSource = patternData
            .DataMember = patternData.Tables(0).ToString
            .Rebind(False)
            .AllowAddNew = True
            .AllowDelete = True
            .AllowUpdate = True
            .Columns("Patternname").Caption = "Name"
            .Splits(0).DisplayColumns("Patternname").Width = 3500
            .Splits(0).DisplayColumns("JBACode").Width = 1250
            .Splits(0).DisplayColumns("JBAName").Visible = False
        End With
    End Sub

    Private Sub tmrPrintserver_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrPrintserver.Tick
        Static isRunning As Boolean
        Static sndml As Boolean
        Static lng_counter As Integer

        On Error GoTo theError

        'If comline = "DIRECTFTP" Then
        ' TimerPrintserver.Enabled = False
        ' tmrFTP.Enabled = True
        ' Exit Sub
        'End If
        If isRunning Then Exit Sub
        isRunning = True
        Dim dtaJobs As DataSet = getSelectDataSet("select sum(ctr) from (select count(*) as ctr from QUESTPDMNAPA.dbo.printjob where processed <> 1) as tblDerived")
        lblPrintBuffer.Text = dtaJobs.Tables(0).Rows(0).Item(0).ToString & " records in printbuffer"
        If Not dtaJobs.Tables(0).Rows(0).Item(0).ToString = "0" Then
            lng_counter = lng_counter + 1
        Else
            lng_counter = 0
        End If
        If lng_counter = 20 Then
            If Not sndml Then
                SendSMTPMail("VF_BornemAutoMailer@vfc.com", "waeytep@vfc.com", "", "", "Problem with Quest PrintServer", "")
                sndml = True
            End If
            lng_counter = 0
        End If
        isRunning = False
        Exit Sub
theError:
        'swfout = True
        Resume Next
    End Sub

    Private Sub VFdms_Run()
        'update password
        'executeSQL("exec vfdms.dbo.sp_start_job 'vfdms merge passwords'")
        VFdms_UpdateDocs()
        'VFdms_MoveToObsolete()
        'replicate data from sql01 to the States, truncates table
        'executeSQL("exec vfdms.dbo.sp_start_job 'vfdms rep2'")
        'executeSQL("exec vfdms.dbo.sp_start_job 'vfdms rep1'")

        'SendSMTPMail("vfdms@vfc.com", "waeytep@vfc.com", "", "", "VFDMS Run completed", "Done")
    End Sub

    Private Sub VFdms_UpdateDocs()
        'Dim orars As New ADODB.Recordset
        'Dim sqlrs As New ADODB.Recordset
        'Dim docid As Long
        'Dim DocSize As Long
        'Dim MainParent As Long
        'Dim swfout As Boolean
        'Dim str As String
        'Dim SsnParent As Long
        'Dim fldrid As Long
        'Dim fldr As String
        'Dim qall As Long

        Dim MainParent As Long = GetFolderId(0, " Quest PDM")
        If MainParent = 0 Then
            MsgBox("Quest Pdm Folder does not exist")
            Exit Sub
        End If
        ReFillStyleInfo()

        'clear foldercontent
        executeSQL("VFdms.DMS_DROPQPDMCONTENT")
        'VFdms_conn.Execute("DMS_DROPQPDMCONTENT")

        'SPStar_conn = New ADODB.Connection
        'With SPStar_conn
        ' .ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Password=Instow06;Initial Catalog=VFSPECSTAR;Data Source=VEBB2Asql01"
        ' .ConnectionTimeout = 0
        ' .open()
        ' End With

        'add and update documents
        Dim dtaPDF As DataSet = getSelectDataSet("select distinct sl.filename,si.lastchange,si.mainkey,si.docsize,si.pdfdate,sl.season,sl.style,sl.fabric from printserver.styleinfo si inner join vf_style_lotnumber sl on si.mainkey = sl.mainkey")
        Dim swfout As Boolean = False
        If Not swfout Then
            executeSQL("DMS_SETDOCSIZETOZERO")
            Dim dtaGroups As DataSet = getSelectDataSet("select groupid from VFdms.groups where name = 'qpdmall'")
            Dim qall As Long = -1
            If Not dtaGroups.Tables(0).Rows.Count <= 0 Then
                qall = dtaGroups.Tables(0).Rows(0).Item("groupid")
            End If
            dtaGroups = Nothing
            For Each rowPDF As DataRow In dtaPDF.Tables(0).Rows
                If rowPDF.Item("DocSize") <> 0 Then
                    swfout = False
                    Dim dtaDoc As DataSet = getSelectDataSet("select * from documents where docname = '" & rowPDF.Item("FileName").ToString() & "'and filename like 'repository\qpdm\%'")
                    'new document
                    Dim docid As Long = -1
                    If dtaDoc.Tables(0).Rows.Count > 0 Then
                        ' docid = AddDoc(rowPDF.Item("FileName").ToString, rowPDF.Item("FileName").ToString.Substring(0, rowPDF.Item("FileName").ToString.Length - 4), _
                        '                "repository\QPDM\" & rowPDF.Item("FileName").ToString, 100, rowPDF.Item("DocSize").ToString, _
                        '               rowPDF.Item("lastchange").ToString, rowPDF.Item("PdfDate").ToString, rowPDF.Item("lastchange").ToString)
                    Else
                        docid = dtaDoc.Tables(0).Rows(0).Item("docid")
                        'existing document
                        If rowPDF.Item("PdfDate").ToString <> dtaDoc.Tables(0).Rows(0).Item("DocDate") Or rowPDF.Item("DocSize").ToString <> dtaDoc.Tables(0).Rows(0).Item("DocSize") Then
                            'sqlrs!replstatus = 1
                            executeSQL("UPDATE documents SET dateinserted='" & rowPDF.Item("lastchange").ToString & "',DocDate='" & rowPDF.Item("PDFdate").ToString & "', " & _
                                       "datemodified='" & rowPDF.Item("lastchange").ToString & "', DocSize='" & rowPDF.Item("DocSize").ToString & "' " & _
                                       "where docname='" & rowPDF.Item("FileName").ToString() & "'and filename like 'repository\qpdm\%'")
                        End If
                    End If
                    If docid > -1 Then
                        'get seasonparentid
                        Dim SsnParent As Long = GetFolderId(MainParent, rowPDF.Item("FileName").ToString.Substring(0, 4).ToUpper)
                        'On Error GoTo 0
                        'If SsnParent = 0 Then SsnParent = CreateFolder(VBA.UCase(VBA.Left(orars!FileName, 4)), MainParent)
                        'getfolderid by Brand
                        'If SsnParent = 0 Then

                        '                   Else

                        ' fldr = GetIniString("vfdms", VBA.Mid(orars!FileName, 6, 1))
                        ' If fldr <> "" Then
                        ' fldrid = GetFolderId(SsnParent, fldr)
                        ' If fldrid = 0 Then fldrid = CreateFolder(fldr, SsnParent)
                        ' 'add to webaccess & Qall
                        ' AddWaQall(fldrid, docid, qall)
                        ' 'add to suppliers
                        ' SetFolderContent(fldrid, docid, orars!FileName, orars!season & "-" & orars!Style & orars!fabric)
                        '                   End If

                        '                  End If
                    End If
                    'sqlrs.Close()
                End If
                'orars.MoveNext()
            Next
            'orars.Close()
        End If
        'SPStar_conn.Close()
        'SPStar_conn = Nothing
        'delete old documents & Links
        'DelOldLinks()
        'sqlrs = Nothing
        'orars = Nothing
    End Sub

    Private Function GetFolderId(ByVal Parent As Long, ByVal FolderN As String) As Long
        Dim cmdSQL As New SqlCommand("DMS_GetFolderId", dcNAPA)
        cmdSQL.CommandType = CommandType.StoredProcedure
        Dim myParam As New SqlParameter
        myParam = New SqlParameter("@ParentFldrId", SqlDbType.BigInt)
        myParam.value = Parent
        cmdSQL.Parameters.Add(MyParam)
        myParam = New SqlParameter("@folderName", SqlDbType.VarChar, 255)
        myParam.value = FolderN
        cmdSQL.Parameters.Add(MyParam)
        myParam = New SqlParameter("@FolderID", SqlDbType.Int)
        myParam.value = 0
        cmdSQL.Parameters.Add(MyParam)

        cmdSQL.ExecuteNonQuery()
        cmdSQL.Dispose()

        GetFolderId = Val("0" & myParam.ToString())
        cmdSQL = Nothing
        myParam = Nothing
    End Function


    Public Sub ReFillStyleInfo()
        clearpath(DownloadPath & "obsolete\", Me.filpdfs)
        clearpath(RepPath, Me.filpdfs)

        executeSQL("delete from printserver.styleinfo")

        'Only take the statusses that are marked as to print
        'Change made for BOM upload as vf_style_lotnumber contains all styles now
        'mr.open "select distinct mainkey, filename, lastchange from vf_style_lotnumber where substr(status,1,4) <> 'GRID'", obj_conn
        Dim dtaMR As DataSet = getSelectDataSet("select distinct vf.mainkey, vf.filename, vf.lastchange from vf_style_lotnumber vf,vf_statusses stats " & _
                " where upper(vf.status)=upper(stats.status) and vf.season in (select seas02 from routecodes) and stats.printserver=1")
        For Each rowMR As DataRow In dtaMR.Tables(0).Rows
            Dim dateFile As Date = CDate("01/01/1971 23:59:00")
            Dim docSize As Long = 0
            If System.IO.File.Exists(RepPath & rowMR.Item("FileName").ToString()) Then
                Try
                    Dim mFile As New FileInfo(RepPath & rowMR.Item("FileName").ToString())
                    dateFile = mFile.LastWriteTime
                    docSize = mFile.Length
                    mFile = Nothing
                Catch
                End Try
            End If
            Dim strLastWrite As String = dateFile.Day.ToString("00") & dateFile.Month.ToString("00") & dateFile.Year.ToString() & " " & dateFile.Hour.ToString("00") & dateFile.Minute.ToString("00") & dateFile.Second.ToString("00")
            'Str = Right("0" & Day(md), 2) & Right("0" & Month(md), 2) & Year(md) & " " & Right("0" & Hour(md), 2) & Right("0" & Minute(md), 2) & Right("0" & Second(md), 2)
            dateFile = rowMR.Item("lastchange").ToString()
            Dim strLastChange As String = dateFile.Day.ToString("00") & dateFile.Month.ToString("00") & dateFile.Year.ToString() & " " & dateFile.Hour.ToString("00") & dateFile.Minute.ToString("00") & dateFile.Second.ToString("00")
            'strlc = Right("0" & Day(md), 2) & Right("0" & Month(md), 2) & Year(md) & " " & Right("0" & Hour(md), 2) & Right("0" & Minute(md), 2) & Right("0" & Second(md), 2)
            executeSQL("INSERT INTO printserver.styleinfo (mainkey, pdfdate, lastchange,docsize) " & _
                        "VALUES ('" & rowMR.Item("MainKey").ToString() & "',to_date('" & strLastWrite & "','ddmmyyyy hh24miss'),to_date('" & strLastChange & "','ddmmyyyy hh24miss')," & docSize & ")")
        Next
    End Sub

    Private Sub clearpath(ByVal str_path As String, ByVal obj As Microsoft.VisualBasic.Compatibility.VB6.FileListBox)
        obj.Path = str_path
        obj.Refresh()
        For int_i As Integer = obj.Items.Count To 0 Step -1
            If obj.Items.Item(int_i) <> "" Then
                Dim dtaResult As DataSet = getSelectDataSet("select * from vf_style_lotnumber where filename='" & obj.Items.Item(int_i) & "'")
                If dtaResult.Tables(0).Rows.Count > 0 Then
                    Kill(str_path & obj.Items.Item(int_i))
                End If
                dtaResult = Nothing
            End If
        Next int_i
    End Sub
End Class