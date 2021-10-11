Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Net

Imports System.Text.ASCIIEncoding
Imports System.Net.Sockets
Imports System.Configuration
Imports System.Resources
Imports System.Threading

Imports IWshRuntimeLibrary
Imports System.Runtime.InteropServices

Module Module1
    Public LayoutData As New DataSet
    Public GridAdapter As New SqlDataAdapter
    Public GridData As New DataSet
    Public cmdBuilder As SqlCommandBuilder
    Public MaintainAdapter As New SqlDataAdapter
    Public MaintainData As New DataSet
    Public MaintainBuilder As SqlCommandBuilder
    Public ColorAdapter As New SqlDataAdapter
    Public ColorData As New DataSet
    Public ColorBuilder As SqlCommandBuilder

    Public SeasonTable As DataTable

    Public UserId As String = ""
    Public userGroup As String = ""
    Public IsAdmin As Boolean = False
    Public NoSubs As Boolean
    Public DoCalculate As Boolean
    Public bl_styleAdmin As Boolean
    Public AllowYouthCosting As Boolean
    Public b_showCosting As Boolean = False
    Public isLoadingSplit As Boolean = False
    Public lngInputLanguage As InputLanguage = InputLanguage.DefaultInputLanguage
    Public str_A3Printer As String = ""

    'Get General Settings from Application Settings XML file
    Private cs_JBALibrary = My.Settings.JBADefaultLibrary

    Public dcNAPA As New SqlClient.SqlConnection(My.Settings.NAPAConnectionString)
    Public dcNAPAQuest As New SqlClient.SqlConnection(My.Settings.NAPAQuestConnectionString)
    Public dcJBA As New OleDb.OleDbConnection(My.Settings.JBAConnectionString)

    Public str_picDirectory As String = My.Settings.LocalPicLocation
    Public c_picDir As String = My.Settings.ThumbnailLocation
    Public i_smsColorCount As Integer = My.Settings.smsColorCount
    Public i_ColorCount As Integer = My.Settings.ColorCount

    Public Const GridUserTable As String = "GridPerUser_new"
    Public Const cStr_inputlanguage As String = "English (United States)"

    Public sAS_StartupPath As String = Application.StartupPath
    Public AllowUpdatequestfreeze As Boolean
    Public sAS_NAPARootFolder As String = My.Settings.NAPARootFolder

    'Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, ByRef nSize As Integer) As Integer

    Sub ListFiles(ByVal Dir As DirectoryInfo, Optional ByVal Recursive As Boolean = True)
        If Dir.Exists Then
            For Each File As FileInfo In Dir.GetFiles
                Console.WriteLine(File.FullName)
            Next

            For Each subDir As DirectoryInfo In Dir.GetDirectories
                Console.WriteLine("Direcotry : {0}", subDir.FullName)
                If Recursive Then ListFiles(subDir, True)
            Next
        End If
    End Sub

    Public Sub GetUser()
        UserId = Environment.UserName.ToString()

        'Dim t As Thread
        't = New Thread(AddressOf BackgroundProcessGetHostName)
        't.Start()
        'UserId = "SCOLOMB"
        'UserId = "KHORNER"
        'UserId = "GIUDICV"
        'UserId = "vanbrim"
        'UserId = "leungsa"
        'UserId = "tolfov"
        UserId = "DA1"
        'UserId = "trevisf"
        'UserId = "HODGSOM"
        'UserId = "bianchf"
        'UserId = "fusariv"
        'UserId = "Dibuccc"
        'UserId = "DIVIEST"
        'UserId = "fossatr"
        'UserId = "Dibuccc"
        'UserId = "franzif"
        'UserId = "lamy3"
        'UserId = "eruggim"
        'UserId = "KOWALCJ"
        'UserId = "PROVASL"
        'UserId = "PERACCD"
        'UserId = "abruzza"
        'UserId = "KOOE"
        'UserId = "ciotton"
        'UserId = "maranic"
        ' UserId = "SDIPAOL"
        'UserId = "AARYADI"
        'UserId = "ngs2"
        'UserId = "Shekf"
        'UserId = "tesaure"
        'UserId = "ALIBERC"
        'UserId = "SVERGAL"
        'UserId = "MASONES"
        'UserId = "IZITO"
        'UserId = "FORCELM"
        'UserId = "FFERREI"
        'UserId = "teutebm"
        'UserId = "perucca"
        'UserId = "darsie"
        'UserId = "wongro"
        'UserId = "wanglel"
        'UserId = "FNERI"
        'UserId = "PYNAETG"
        'UserId = "SCOLOMB"
        'UserId = "SUHENDE"
        'UserId = "MSGAREL"
        'UserId = "cchoy"
        'UserId = "ACHARYA"
        'UserId = "SDIPAOL"
        'UserId = "waeytep"
        'UserId = "tesaure"
        'UserId = "MBELKOV"
        ' UserId = "BENZONF"
        'UserId = "CFOMIAT"
        'UserId = "GDAGOST"
    End Sub

    Private Sub BackgroundProcessGetHostName()
        Dim strHost As String = ""
        Try
            Dim psi As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo("netstat", "")
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True

            Dim proc As System.Diagnostics.Process = Nothing
            proc = System.Diagnostics.Process.Start(psi)
            proc.WaitForExit()

            For i As Integer = 0 To 4
                proc.StandardOutput.ReadLine()
                While True
                    Dim strLine As String = proc.StandardOutput.ReadLine()
                    If strLine = Nothing Then
                        Exit While
                    ElseIf strLine.Contains(":microsoft-ds") Then
                        'writeToLog(sAS_StartupPath & "\Logs\connLog.txt", UserId & " - " & strLine)
                        strHost = strLine.Split("  ")(8).Split(":")(0)
                        Exit For
                    End If
                End While
            Next
        Catch
        End Try

        executeSQL("UPDATE dbo.Param_users SET RDPHost='" & strHost & "', lastLogin='" & Now() & "', RDPServer='" & Environment.MachineName.ToString() & "' WHERE usernaam='" & UserId & "'")
    End Sub

    Public Sub setInputLanguage()
        For Each Lng As InputLanguage In InputLanguage.InstalledInputLanguages
            'Debug.WriteLine(Lng.Culture.DisplayName.ToString)
            If Lng.Culture.DisplayName.ToString = cStr_inputlanguage Then
                lngInputLanguage = Lng
                InputLanguage.CurrentInputLanguage = Lng
                Exit For
            End If
        Next
    End Sub

    Public Sub SaveGridLayout(ByVal myform As Form, ByVal myGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        executeSQL("UPDATE param_users SET picDirectory = '" & str_picDirectory & "' WHERE userNaam='" & UserId & "'")

        For Each mySplit As C1.Win.C1TrueDBGrid.Split In myGrid.Splits
            SaveSplitLayout(myform, myGrid, mySplit)
        Next
    End Sub

    Public Sub SaveSplitLayout(ByVal myform As Form, ByVal myGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal mySplit As C1.Win.C1TrueDBGrid.Split)
        Dim iSplitGrid As String = Replace(mySplit.Name.ToString, "Split", "")
        If iSplitGrid Is Nothing Then iSplitGrid = "0"

        Dim SelectData As DataSet = getSelectDataSet("SELECT * From " & GridUserTable & " WHERE formID='" & myform.Name & "' AND gridID='" & myGrid.Name & "' AND userID='" & UserId & "' AND [Brand] = 'N'")
        For Each myColumn As C1.Win.C1TrueDBGrid.C1DisplayColumn In mySplit.DisplayColumns
            With myColumn
                If .Visible = True Then
                    Dim foundRows() As Data.DataRow = SelectData.Tables(0).Select("ColumnID = '" & .DataColumn.DataField & "'")
                    If foundRows.Length > 0 Then
                        executeSQL("UPDATE " & GridUserTable & " SET width =" & .Width & ", split = " & iSplitGrid & ", position=" & mySplit.DisplayColumns.IndexOf(myColumn) & ", filter='" & .DataColumn.FilterText & "' WHERE formID='" & myform.Name & "' AND gridID='" & myGrid.Name & "' AND userID='" & UserId & "' AND ColumnID = '" & .DataColumn.DataField & "' AND Brand ='N'")
                    Else
                        executeSQL("INSERT INTO " & GridUserTable & " (formID, gridID, userID, columnID, width, split, position, filter, brand) VALUES ('" & myform.Name & "', '" & myGrid.Name & "', '" & UserId & "', '" & .DataColumn.DataField & "', " & .Width & ", " & iSplitGrid & ", " & mySplit.DisplayColumns.IndexOf(myColumn) & ", '" & .DataColumn.FilterText & "','N')")
                    End If
                End If
            End With
        Next
    End Sub

    Public Sub LoadGridLayout(ByVal myform As Form, ByVal myGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal myGridTable As String, ByVal b_filterCombo As Boolean)
        executeSQL("DELETE FROM " & GridUserTable & " WHERE formID = '" & myform.Name & "' AND gridID = '" & myGrid.Name & "' AND [Brand] = 'N' AND columnID NOT IN (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns WHERE UPPER(TABLE_NAME) = '" & myGridTable & "')")

        For i_splitCounter As Integer = 0 To myGrid.Splits.Count - 1
            LoadSplitLayout(myform, myGrid, myGridTable, b_filterCombo, i_splitCounter)
        Next
        Dim dsFilter As DataSet = getSelectDataSet("SELECT * From " & GridUserTable & " WHERE formID='" & myform.Name & "' AND gridID='" & myGrid.Name & "' AND userID='" & UserId & "' AND Brand = 'N' AND ISNULL(filter,'') <> '' ORDER BY position ASC")
        For Each dsRow As DataRow In dsFilter.Tables(0).Rows
            Dim col As C1.Win.C1TrueDBGrid.C1DataColumn = myGrid.Columns(dsRow("ColumnID"))
            col.FilterText = dsRow("filter")
        Next

        isLoadingSplit = False
        frmMainGrid.grdMain_FilterChange(Nothing, Nothing)
    End Sub

    Public Sub LoadSplitLayout(ByVal myform As Form, ByVal myGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal myGridTable As String, ByVal b_filterCombo As Boolean, ByVal iSplit As Integer, Optional ByVal srcForm As Form = Nothing, Optional ByVal srcGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid = Nothing)
        Dim i_columncounter As Integer
        Dim i_posCounter As Integer = 0
        Dim iSplitGrid As String = Replace(myGrid.Splits(iSplit).Name.ToString, "Split", "")

        isLoadingSplit = True
        Dim str_gridName, str_formName As String
        If Not srcGrid Is Nothing Then
            str_formName = srcForm.Name
            str_gridName = srcGrid.Name
        Else
            str_formName = myform.Name
            str_gridName = myGrid.Name
        End If
        Dim SelectData As DataSet = getSelectDataSet("SELECT * From " & GridUserTable & " WHERE formID='" & str_formName & "' AND gridID='" & str_gridName & "' AND userID='" & UserId & "' AND Split='" & iSplitGrid & "' AND Brand = 'N' ORDER BY position ASC")
        For i_columncounter = 0 To SelectData.Tables(0).Rows.Count - 1
            Dim dc As C1.Win.C1TrueDBGrid.C1DisplayColumn = myGrid.Splits(iSplit).DisplayColumns(SelectData.Tables(0).Rows(i_columncounter).Item("columnID"))
            With dc
                If .Visible = True Then
                    .Width = SelectData.Tables(0).Rows(i_columncounter).Item("width")
                    .DataColumn.FilterText = SelectData.Tables(0).Rows(i_columncounter).Item("filter").ToString
                    If myGrid.Splits(iSplit).DisplayColumns.IndexOf(dc) <> CInt(SelectData.Tables(0).Rows(i_columncounter).Item("position")) And CInt(SelectData.Tables(0).Rows(i_columncounter).Item("position")) < myGrid.Splits(iSplit).DisplayColumns.Count Then
                        myGrid.Splits(iSplit).DisplayColumns.RemoveAt(myGrid.Splits(iSplit).DisplayColumns.IndexOf(dc))
                        myGrid.Splits(iSplit).DisplayColumns.Insert(CInt(SelectData.Tables(0).Rows(i_columncounter).Item("position")), dc)
                    End If
                End If
            End With
        Next
        isLoadingSplit = False
    End Sub

    Public Sub executeSQL(ByVal str_sql As String)
        Dim adapter As New SqlDataAdapter
        Try
            If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
            adapter.UpdateCommand = dcNAPA.CreateCommand
            adapter.UpdateCommand.CommandText = str_sql
            adapter.UpdateCommand.ExecuteScalar()
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function getSelectDataSet(ByVal str_sql As String) As DataSet
        Dim SelectAdapter As New SqlDataAdapter
        Dim SelectData As New DataSet

        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        SelectAdapter.SelectCommand = dcNAPA.CreateCommand
        SelectAdapter.SelectCommand.CommandText = str_sql
        SelectAdapter.Fill(SelectData)
        SelectData.CreateDataReader()
        getSelectDataSet = SelectData
    End Function

    Public Function ResizeImage(ByVal sourceImage As String, ByVal destImage As String, ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal destWidth_square As Integer, ByVal destHeight_square As Integer, Optional ByVal onlySquare As Boolean = False) As Size
        Try
            Dim srcImage As Image = Image.FromFile(sourceImage)
            Dim Original As Image = DirectCast(srcImage.Clone(), Image)
            Dim x As Single = 0
            Dim y As Single = 0
            Dim origDestWidth As Integer = destWidth_square
            Dim origDestHeight As Integer = destHeight_square

            If Original.Height > Original.Width Then
                destWidth = Original.Width * destHeight / Original.Height
                x = (origDestWidth - destWidth) / 2
            Else
                destHeight = Original.Height * destWidth / Original.Width
                y = (origDestHeight - destHeight) / 2
            End If
            If Not onlySquare Then
                'Add a +1 white border to image to prevent size errors
                ResizeImage = createImage(Original, destWidth + 1, destHeight + 1, 1, 1, destWidth + 1, destHeight + 1, destImage)
                createImage(Original, origDestWidth, origDestHeight, x, y, destWidth, destHeight, destImage.Substring(0, destImage.IndexOf(".")) & "_square.jpg")
            Else
                ResizeImage = createImage(Original, origDestWidth, origDestHeight, x, y, destWidth, destHeight, destImage.Substring(0, destImage.IndexOf(".")) & "_square.jpg")
            End If
            Original.Dispose()
        Catch e As Exception
            'ResizeImage = Nothing
            'MsgBox(e.Message & vbCrLf & e.InnerException.ToString)
            Try
                writeToLog(sAS_StartupPath & "\Logs\ThumbLog.txt", sourceImage & " is too large, out of memory")
            Catch ex As Exception

            End Try
        End Try
    End Function

    Private Function createImage(ByVal Original As Image, ByVal origDestWidth As Integer, ByVal origDestHeight As Integer, ByVal x As Integer, ByVal y As Integer, ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal savePath As String) As Size
        Try
            'Dim iPixFormat As System.Drawing.Graphics
            Dim ResizedImageWhite As Image = New Bitmap(origDestWidth, origDestHeight, Original.PixelFormat) 'original
            'Dim ResizedImageWhite As Image = New Bitmap(origDestWidth, origDestHeight)
            Dim oGraphicWhite As Graphics = Graphics.FromImage(ResizedImageWhite)
            Dim oRectangleWhite As Rectangle = New Rectangle(0, 0, origDestWidth, origDestHeight)

            oGraphicWhite.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            oGraphicWhite.SmoothingMode = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            oGraphicWhite.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            oGraphicWhite.FillRectangle(Brushes.White, oRectangleWhite)
            oGraphicWhite.DrawImage(Original, x, y, destWidth, destHeight)

            createImage = ResizedImageWhite.Size
            oGraphicWhite.Dispose()
            ResizedImageWhite.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg)
            ResizedImageWhite.Dispose()

        Catch ex1 As Exception
            'Try
            '    Dim ResizedImageWhite As Image = New Bitmap(origDestWidth, origDestHeight)
            '    Dim oGraphicWhite As Graphics = Graphics.FromImage(ResizedImageWhite)
            '    Dim oRectangleWhite As Rectangle = New Rectangle(0, 0, origDestWidth, origDestHeight)

            '    oGraphicWhite.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            '    oGraphicWhite.SmoothingMode = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            '    oGraphicWhite.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            '    oGraphicWhite.FillRectangle(Brushes.White, oRectangleWhite)
            '    oGraphicWhite.DrawImage(Original, x, y, destWidth, destHeight)

            '    createImage = ResizedImageWhite.Size
            '    oGraphicWhite.Dispose()
            '    ResizedImageWhite.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg)
            '    ResizedImageWhite.Dispose()

            'Catch ex As Exception
            '    'Throw ex
            'End Try

        End Try
    End Function

    Private Function createRGBImage(ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal savePath As String, ByVal i_red As Integer, ByVal i_green As Integer, ByVal i_blue As Integer) As String
        Dim RGBImage As Image = New Bitmap(destWidth, destHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        Dim oGraphic As Graphics = Graphics.FromImage(RGBImage)
        oGraphic.FillRectangle(New SolidBrush(Color.FromArgb(i_red, i_green, i_blue)), New Rectangle(0, 0, destWidth, destHeight))

        createRGBImage = savePath
        RGBImage.Save(createRGBImage, System.Drawing.Imaging.ImageFormat.Jpeg)
        RGBImage.Dispose()
    End Function


    Private Function replacePicturePath(ByVal str_path As String) As String
        Dim astr_Paths() As String = Split(My.Settings.PictureLocationsToReplace, ",")

        replacePicturePath = str_path
        For i As Integer = 0 To astr_Paths.Length - 1
            replacePicturePath = Replace(replacePicturePath.ToUpper, astr_Paths(i).ToUpper, "O:\")
        Next
    End Function

    Public Function getNewPicture(ByVal DevNo As String) As String
        Dim fileDialog As New OpenFileDialog()

        getNewPicture = ""
        With fileDialog
            .InitialDirectory = str_picDirectory
            .Filter = "picture files (*.jpg)|*.jpg|All files (*.*)|*.*"
            .FilterIndex = 1
            .RestoreDirectory = True
            .Multiselect = False
            .Title = "Select a picture file"
            If .ShowDialog() = DialogResult.OK Then
                Dim str_picDate As String = Format(System.IO.File.GetLastWriteTime(.FileName), "yyyy-MM-dd HH:mm:ss")
                Dim picSize As Size = ResizeImage(.FileName, c_picDir & DevNo & ".jpg", 800, 600, 800, 800)
                If Not picSize = Nothing Then
                    getNewPicture = replacePicturePath(.FileName)
                    insertPicture(DevNo, c_picDir & DevNo & ".jpg", c_picDir & DevNo & "_square.jpg", str_picDate, picSize)
                End If
                str_picDirectory = .FileName.Substring(0, .FileName.LastIndexOf("\"))
            End If
        End With
    End Function

    Public Sub checkThumbnails(Optional ByVal str_where As String = "", Optional ByVal bNoLog As Boolean = False)
        Dim str_pic, str_picTh, str_picTh_square, str_picDate, str_picFileName As String
        Dim picSize As Size
        Try

            If Not bNoLog Then writeToLog(sAS_StartupPath & "\Logs\ThumbLog.txt", "New Run " & Format(Now, "yyyy-MM-dd HH:mm:ss"))
            If Not bNoLog Then writeToLog(sAS_StartupPath & "\Logs\PicLog.txt", "New Run " & Format(Now, "yyyy-MM-dd HH:mm:ss"))
            Dim selectData As DataSet = getSelectDataSet("SELECT devNo, Name, picture, pictureDate FROM newGrid WHERE LEN(isnull(picture,'')) > 0 " & IIf(str_where.Length > 0, "AND " & str_where, ""))
            For Each selectRow As DataRow In selectData.Tables(0).Rows
                str_pic = replacePicturePath(selectRow("picture").ToString) 'Replace(Replace(Replace(Replace(selectRow("picture").ToString.ToUpper, "\\TSCLIENT\P\", "\\VEVP2A02\PUBLIC\"), "P:\", "\\VEVP2A02\PUBLIC\"), "\\TSCLIENT\PUBLIC\", "\\VEVP2A02\PUBLIC\"), "\\VEVP2A02\PUBLIC\", "O:\")
                str_picFileName = selectRow("DevNo").ToString
                If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", str_picFileName & " - " & str_pic & vbCrLf & "________")
                If System.IO.File.Exists(str_pic) Then
                    Try
                        str_picTh = c_picDir & str_picFileName & ".jpg"
                        str_picTh_square = c_picDir & str_picFileName & "_square.jpg"
                        str_picDate = Format(System.IO.File.GetLastWriteTime(str_pic), "yyyy-MM-dd HH:mm:ss")
                        'If Not File.Exists(str_picTh) Or Not str_picDate = selectRow("pictureDate").ToString Then
                        picSize = ResizeImage(str_pic, str_picTh, 800, 600, 800, 800, False)
                        If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", str_pic & " exists => " & str_picDate & " - " & str_picTh & " - " & str_picTh_square & " - " & IIf(picSize = Nothing, "Nothing", picSize.Width & " x " & picSize.Height))
                        If Not picSize = Nothing Then
                            executeSQL("UPDATE NewGrid SET PictureDate='" & str_picDate & "' WHERE devNo=" & selectRow("devNo").ToString)
                            insertPicture(selectRow("DevNo").ToString, str_picTh, str_picTh_square, str_picDate, picSize)
                        End If
                        'End If
                    Catch ex As Exception
                        If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", str_picFileName & " Failed Img processing" & vbCrLf & "________")
                    End Try
                ElseIf System.IO.File.Exists(c_picDir & str_picFileName & ".jpg") Then
                    Try
                        str_picTh = c_picDir & str_picFileName & ".jpg"
                        str_picTh_square = c_picDir & str_picFileName & "_square.jpg"
                        str_picDate = Format(System.IO.File.GetLastWriteTime(c_picDir & str_picFileName & ".jpg"), "yyyy-MM-dd HH:mm:ss")
                        picSize = ResizeImage(c_picDir & str_picFileName & ".jpg", str_picTh, 800, 600, 800, 800, True)
                        If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", c_picDir & str_picFileName & ".jpg" & " exists => " & str_picDate & " - " & str_picTh & " - " & str_picTh_square & " - " & IIf(picSize = Nothing, "Nothing", picSize.Width & " x " & picSize.Height))
                        If Not picSize = Nothing Then
                            executeSQL("UPDATE NewGrid SET PictureDate='" & str_picDate & "' WHERE devNo=" & selectRow("devNo").ToString)
                            insertPicture(selectRow("DevNo").ToString, str_picTh, str_picTh_square, str_picDate, picSize)
                        End If
                    Catch ex As Exception
                        If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", str_picFileName & " Failed Img processing" & vbCrLf & "________")
                    End Try

                Else
                    If Not bNoLog Then writeToLog(sAS_StartupPath & "\PicLog.txt", str_pic & " does not exist!")
                    If Not bNoLog Then writeToLog(sAS_StartupPath & "\ThumbLog.txt", str_pic & " does not exist!")
                End If
                If Not bNoLog Then writeToLog(sAS_StartupPath & "\ThumbLog.txt", " " & vbCrLf)
            Next
            If Not bNoLog Then writeToLog(sAS_StartupPath & "\Logs\ThumbLog.txt", "End Run " & Format(Now, "yyyy-MM-dd HH:mm:ss") & vbCrLf & vbCrLf)
            If Not bNoLog Then writeToLog(sAS_StartupPath & "\Logs\PicLog.txt", "End Run " & Format(Now, "yyyy-MM-dd HH:mm:ss") & vbCrLf & vbCrLf)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub insertPicture(ByVal picLotNumber As String, ByVal picPath As String, ByVal picPath_white As String, ByVal picDate As String, ByVal picSize As Size)
        Dim curImage As Image = Nothing
        Dim fs As FileStream = New FileStream(picPath, FileMode.OpenOrCreate, FileAccess.Read)
        Dim rawData() As Byte = New Byte(fs.Length) {}
        fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length))
        fs.Close()
        Dim fs_white As FileStream = New FileStream(picPath_white, FileMode.OpenOrCreate, FileAccess.Read)
        Dim rawData_white() As Byte = New Byte(fs_white.Length) {}
        fs_white.Read(rawData_white, 0, System.Convert.ToInt32(fs_white.Length))
        fs_white.Close()

        Dim cmdSQL As New SqlCommand

        If dcNAPA.State = ConnectionState.Closed Then dcNAPA.Open()
        If getSelectDataSet("SELECT Lotnumber FROM NAPA.dbo.vf_Sketches WHERE Lotnumber ='" & picLotNumber & "'").Tables(0).Rows.Count > 0 Then
            cmdSQL = New SqlCommand("UPDATE NAPA.dbo.vf_Sketches SET PicBlob=@PicBlob, pictureTime=@pictureTime, picWidth=@picWidth, picHeight=@picHeight, Picblob_square=@Picblob_square WHERE Lotnumber ='" & picLotNumber & "'", dcNAPA)
        Else
            cmdSQL = New SqlCommand("INSERT INTO NAPA.dbo.vf_Sketches (Lotnumber, PicBlob, pictureTime, picWidth, picHeight, Picblob_square) VALUES (@lotNumber, @PicBlob, @pictureTime, @picWidth, @picHeight, @Picblob_square)", dcNAPA)
        End If
        cmdSQL.CommandType = CommandType.Text

        Dim LotNumber = New SqlParameter("@lotNumber", SqlDbType.VarChar, 32)
        LotNumber.Value = picLotNumber
        cmdSQL.Parameters.Add(LotNumber)
        Dim pic As SqlParameter = New SqlParameter("@PicBlob", SqlDbType.Image)
        pic.Value = rawData
        cmdSQL.Parameters.Add(pic)
        Dim pictureTime = New SqlParameter("@pictureTime", SqlDbType.DateTime)
        pictureTime.Value = CDate(picDate)
        cmdSQL.Parameters.Add(pictureTime)
        Dim picWidth = New SqlParameter("@picWidth", SqlDbType.Int)
        picWidth.Value = picSize.Width
        cmdSQL.Parameters.Add(picWidth)
        Dim picHeight = New SqlParameter("@picHeight", SqlDbType.Int)
        picHeight.Value = picSize.Height
        cmdSQL.Parameters.Add(picHeight)
        Dim Picblob_square As SqlParameter = New SqlParameter("@Picblob_square", SqlDbType.Image)
        Picblob_square.Value = rawData_white
        cmdSQL.Parameters.Add(Picblob_square)

        cmdSQL.ExecuteNonQuery()
        cmdSQL.Dispose()
    End Sub

    Public Sub writeToLog(ByVal filePath As String, ByVal text As String)
        Try

            Dim oWrite As IO.TextWriter
            If System.IO.File.Exists(filePath) Then
                oWrite = IO.File.AppendText(filePath)
            Else
                oWrite = IO.File.CreateText(filePath)
            End If
            oWrite.WriteLine(text)
            oWrite.Flush()
            oWrite.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetDefaultPrinter(ByVal PrinterName As String)
        Try
            'Declare WMI Variables
            Dim MgmtObject As System.Management.ManagementObject
            Dim MgmtCollection As System.Management.ManagementObjectCollection
            Dim MgmtSearcher As System.Management.ManagementObjectSearcher

            'Perform the search for printers and return the listing as a collection
            MgmtSearcher = New System.Management.ManagementObjectSearcher("Select * from Win32_Printer")
            MgmtCollection = MgmtSearcher.Get

            'Enumerate Objects To Find Printer
            For Each MgmtObject In MgmtCollection
                'Look for a match
                If MgmtObject.Item("name").ToString = PrinterName Then
                    'Set Default Printer
                    Dim TempObject() As Object = Nothing 'Temporary Object for InvokeMethod. Holds no purpose.
                    MgmtObject.InvokeMethod("SetDefaultPrinter", TempObject)

                    'Set Success Value and Exit For..Next Loop
                    Exit For
                End If
            Next

            executeSQL("UPDATE param_users SET A3Printer='" & PrinterName & "' WHERE usernaam='" & UserId & "'")
        Catch
            writeToLog(sAS_StartupPath & "\Logs\SetDefaultPrinterErrorLog.txt", Err.Number & " - " & Err.Description)
        End Try
    End Sub

    Public Sub checkColumnsGridLayout(ByVal gridName As String, ByVal tableName As String)
        Dim str_MissingColumns As String = "INSERT INTO gridLayout (gridName, columnName)SELECT '" & gridName & "', COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " & _
                                        "WHERE TABLE_CATALOG = 'NAPA' AND UPPER(TABLE_NAME) ='" & tableName & "' AND " & _
                                        "COLUMN_NAME NOT IN (SELECT columnName FROM gridLayout WHERE UPPER(gridName) = '" & gridName.ToUpper & "')"
        executeSQL(str_MissingColumns)
        str_MissingColumns = "DELETE FROM gridLayout WHERE UPPER(gridName) = '" & gridName & "' AND " & _
                                "columnName NOT IN (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'NAPA' AND UPPER(TABLE_NAME) ='" & tableName & "')"
        executeSQL(str_MissingColumns)
    End Sub

    Private Function getUniqueFileName(ByVal str_filePath As String) As String
        str_filePath = Replace(str_filePath, "/", "\")
        Dim str_FileName As String = str_filePath.Substring(str_filePath.LastIndexOf("\") + 1)
        Dim str_Directory As String = str_filePath.Substring(0, str_filePath.LastIndexOf("\") + 1)
        Dim str_extension As String = str_FileName.Substring(str_FileName.LastIndexOf("."))
        str_FileName = str_FileName.Substring(0, str_FileName.LastIndexOf("."))
        If System.IO.File.Exists(str_filePath) Then
            If str_FileName.Contains("(") And str_FileName.Contains(")") And str_FileName.LastIndexOf("(") < str_FileName.LastIndexOf(")") Then
                Dim str_count As String = str_FileName.Substring(str_FileName.LastIndexOf("(") + 1, str_FileName.LastIndexOf(")") - str_FileName.LastIndexOf("(") - 1)
                If IsNumeric(str_count) Then
                    getUniqueFileName = getUniqueFileName(str_Directory & str_FileName.Substring(0, str_FileName.LastIndexOf("(")) & "(" & CInt(str_count) + 1 & ")" & str_extension)
                Else
                    getUniqueFileName = getUniqueFileName(str_Directory & str_FileName & "(1)" & str_extension)
                End If
            Else
                getUniqueFileName = getUniqueFileName(str_Directory & str_FileName & "(1)" & str_extension)
            End If
        Else
            getUniqueFileName = str_filePath
        End If
    End Function

    Public Sub uploadLinelistToBrio()
        FTP_File_To_JBA("VSBRLNLST", "NAPA.dbo.vw_Brio")
    End Sub

    Public Sub FTP_File_To_JBA(ByVal str_JBAFile As String, ByVal str_view As String)
        writeToLog(sAS_StartupPath & "\Logs\UploadTo" & str_JBAFile & "Log.txt", "Upload of Linelist info to JBA started on " & Format(Now, "dd/MM/yyyy HH:mm:ss"))

        Dim dataView As DataSet = getSelectDataSet("SELECT * FROM " & str_view)
        Dim dataColumns As DataSet = getSelectDataSet("SELECT * FROM NAPA.dbo.param_FTP WHERE FTPFile='" & str_JBAFile & "' ORDER BY columnSequence")

        If Not dataView.Tables(0).Columns.Count = dataColumns.Tables(0).Rows.Count Then
            MsgBox("The view has changed and has an incorrect number of columns." & vbCrLf & "Please check NAPA.dbo.param_FTP for the correct column sequence for FTPfile " & str_JBAFile, MsgBoxStyle.Critical, "Incorrect")
        Else
            Dim str_FTPFile As String = getUniqueFileName(sAS_StartupPath & "\FTP\FTP_" & str_JBAFile & ".txt")
            Dim oWrite As IO.TextWriter = IO.File.CreateText(str_FTPFile)
            For i As Integer = 0 To dataView.Tables(0).Rows.Count - 1
                For j As Integer = 0 To dataColumns.Tables(0).Rows.Count - 1
                    Dim i_destLength As Integer = dataColumns.Tables(0).Rows(j).Item("columnLength")
                    Dim i_length As Integer = IIf(dataView.Tables(0).Rows(i).Item(j).ToString.Length > i_destLength, i_destLength, dataView.Tables(0).Rows(i).Item(j).ToString.Length)
                    oWrite.Write(dataView.Tables(0).Rows(i).Item(j).ToString.Substring(0, i_length) & Space(i_destLength - i_length))
                Next
                oWrite.WriteLine("")
            Next
            oWrite.Flush()
            oWrite.Close()

            executeJBA("DELETE FROM " & str_JBAFile)

            uploadFTPFileShell("ITGC600A", cs_JBALibrary, "dtatfr", "dtatfrx", str_FTPFile, str_JBAFile)
        End If

        writeToLog(sAS_StartupPath & "\Logs\UploadTo" & str_JBAFile & "Log.txt", "Upload of Linelist info to JBA finished on " & Format(Now, "dd/MM/yyyy HH:mm:ss"))
    End Sub

    Public Sub executeJBA(ByVal str_sql As String)
        Dim JBAAdapter As New OleDb.OleDbDataAdapter
        Try
            If dcJBA.State = ConnectionState.Closed Then dcJBA.Open()
            JBAAdapter.UpdateCommand = dcJBA.CreateCommand
            JBAAdapter.UpdateCommand.CommandText = str_sql
            JBAAdapter.UpdateCommand.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function getSelectDataSetJBA(ByVal str_sql As String) As DataSet
        If dcJBA.State = ConnectionState.Closed Then dcJBA.Open()

        Dim oBindingSource As New BindingSource()
        Dim JBAData As New DataSet
        Dim JBAAdapter As New OleDb.OleDbDataAdapter(str_sql, dcJBA)
        JBAAdapter.Fill(JBAData)
        getSelectDataSetJBA = JBAData
    End Function

    Public Sub setDisplayColumns(ByVal grdName As C1.Win.C1TrueDBGrid.C1TrueDBGrid, Optional ByVal b_lockColumns As Boolean = False, Optional ByVal b_shownDropdowns As Boolean = True)
        Dim collectionColumns As DataTable = getSelectDataSet("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'NAPA' AND UPPER(TABLE_NAME) ='SELECTIONS'").Tables(0)

        Dim foundRows() As Data.DataRow

        ' Try
        For i As Integer = 0 To grdName.Columns.Count - 1
            foundRows = LayoutData.Tables(0).Select("ColumnName = '" & grdName.Columns(i).DataField & "'")
            If foundRows.Length > 0 Then
                grdName.Columns(i).Caption = IIf(foundRows(0).Item("columnDescription").ToString.Length = 0, grdName.Columns(i).DataField, foundRows(0).Item("columnDescription").ToString)
                If foundRows(0).Item("HasDropDown").ToString = "True" Then
                    Dim ComboData As New DataSet
                    ComboData = getSelectDataSet(foundRows(0).Item("DropDownSQL"))
                    Dim v As C1.Win.C1TrueDBGrid.ValueItemCollection = grdName.Columns(foundRows(0).Item("columnName")).ValueItems.Values
                    If Not grdName.Columns(i).DataField = "StyleStatus" Then
                        'v.Add(New C1.Win.C1TrueDBGrid.ValueItem("", ""))
                        v.Add(New C1.Win.C1TrueDBGrid.ValueItem("TBD", "To Be Defined"))
                    End If
                    For j As Integer = 0 To ComboData.Tables(0).Rows.Count - 1
                        If grdName.Columns(i).DataField.StartsWith("ProdColor") Or grdName.Columns(i).DataField.StartsWith("SmsColor") Then
                            v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRows(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundRows(0).Item("DropDownValueMember")) & " " & ComboData.Tables(0).Rows(j).Item(foundRows(0).Item("DropDownDisplayMember"))))
                        Else
                            Try
                                v.Add(New C1.Win.C1TrueDBGrid.ValueItem(ComboData.Tables(0).Rows(j).Item(foundRows(0).Item("DropDownValueMember")), ComboData.Tables(0).Rows(j).Item(foundRows(0).Item("DropDownDisplayMember"))))
                            Catch

                            End Try
End If

                    Next
                    grdName.Columns(foundRows(0).Item("columnName")).ValueItems.Translate = True
                    If b_shownDropdowns = True Then
                        grdName.Columns(i).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox
                        grdName.Splits(0).DisplayColumns(i).DropDownList = True
                    End If
                    grdName.Columns(i).FilterDropdown = True
                    ComboData = Nothing
                End If
                If foundRows(0).Item("Locked").ToString = "True" Or grdName.Columns(i).DataField = "Picture" Or b_lockColumns = True Or _
                    ((grdName.Name = "grdMain" Or grdName.Name = "grdYouth") And collectionColumns.Select("COLUMN_NAME = '" & foundRows(0).Item("columnName") & "'").Length > 0) Then
                    grdName.Splits(0).DisplayColumns(i).Locked = True
                End If
                If foundRows(0).Item("Split").ToString = "0" And Not foundRows(0).Item("InVisible").ToString = "True" Then
                    grdName.Splits(0).DisplayColumns(i).Visible = True
                Else
                    grdName.Splits(0).DisplayColumns(i).Visible = False
                End If
                If foundRows(0).Item("formatNumber").ToString.Length > 0 Then
                    grdName.Columns(i).NumberFormat = foundRows(0).Item("formatNumber").ToString
                End If
                If grdName.Columns(i).DataType.Name.ToString = "DateTime" Then grdName.Columns(i).EnableDateTimeEditor = True
                If foundRows(0).Item("fetchCellStyle").ToString = "True" Then grdName.Splits(0).DisplayColumns(i).FetchStyle = True
                If foundRows(0).Item("RiversandMandatory").ToString = "True" Then grdName.Splits(0).DisplayColumns(i).HeadingStyle.BackColor = Color.DeepSkyBlue

            End If
        Next

        'Catch

        'End Try
    End Sub

    Public Function getValue(ByVal str_devNo As String, ByVal str_Column As String, ByVal str_value As String) As String
        'Dim dsNewGrid As DataSet = getSelectDataSet("SELECT " & str_Column & " FROM Newgrid WHERE DevNo=" & str_devNo)
        Dim dsGridLayout As DataSet = getSelectDataSet("SELECT * FROM GridLayout WHERE ColumnName='" & str_Column & "'")

        If dsGridLayout.Tables(0).Rows.Count = 0 Then
            Return str_value
        End If

        If dsGridLayout.Tables(0).Rows(0).Item("DropDownSql").ToString.Length > 0 And Not str_value = "TBD" Then 'And dsNewGrid.Tables(0).Rows(0).Item(0).ToString.Length > 0 Then
            Dim dsDropDown As DataSet = getSelectDataSet(dsGridLayout.Tables(0).Rows(0).Item("DropDownSql").ToString)
            Dim dRow As DataRow() = dsDropDown.Tables(0).Select(dsGridLayout.Tables(0).Rows(0).Item("DropDownValueMember").ToString & "='" & str_value & "'") '.Tables(0).Rows(0).Item(0).ToString & "'")(0)
            If dRow.Length > 0 Then
                getValue = dRow(0).Item(dsGridLayout.Tables(0).Rows(0).Item("DropDownDisplayMember").ToString)
            Else
                getValue = ""
            End If
        Else
            getValue = str_value 'dsNewGrid.Tables(0).Rows(0).Item(0).ToString
        End If
        If dsGridLayout.Tables(0).Rows(0).Item("isCurrency").ToString = "True" Then
            Dim dsRate As DataTable = getSelectDataSet("SELECT NAPA.dbo.tbl_sourcing_Rates.Rate FROM NAPA.dbo.tbl_sourcing_Rates " &
                                                        "INNER(Join) dbo.NewGrid ON NAPA.dbo.tbl_sourcing_Rates.Season = dbo.NewGrid.Season " &
                                                        "AND NAPA.dbo.tbl_sourcing_Rates.Currency = dbo.NewGrid.Currency " &
                                                        "WHERE (NAPA.dbo.tbl_sourcing_Rates.Coalition = 'DP') AND " &
                                                        "(dbo.NewGrid.DevNo = " & str_devNo & ")").Tables(0)
            If dsRate.Rows.Count > 0 Then
                getValue = getValue / dsRate.Rows(0).Item(0)
            End If
        End If
        Dim str_format As String = dsGridLayout.Tables(0).Rows(0).Item("formatExport").ToString
        If str_format.Length > 0 Then
            getValue = Format(getValue, str_format)
        End If
    End Function

    Public Sub MapDrive(ByVal driveLetter As String, ByVal networkPath As String, ByVal isPersistent As Boolean)
        Dim networkShell As New WshNetwork()
        Try
            DisconnectDrive(driveLetter, True, True)
            networkShell.MapNetworkDrive(driveLetter, networkPath, isPersistent)
        Finally
            If Not networkShell Is Nothing Then
                Marshal.ReleaseComObject(networkShell)
                networkShell = Nothing
            End If
        End Try
    End Sub

    Public Sub DisconnectDrive(ByVal driveLetter As String, ByVal willForce As Boolean, ByVal isPersistent As Boolean)
        Dim networkShell As New WshNetwork()

        If IO.Directory.Exists(driveLetter) Then
            Try
                networkShell.RemoveNetworkDrive(driveLetter, willForce, isPersistent)
            Finally
                If Not networkShell Is Nothing Then
                    Marshal.ReleaseComObject(networkShell)
                    networkShell = Nothing
                End If
            End Try
        End If
    End Sub

    Public Sub uploadFTPFileShell(ByVal str_server As String, ByVal str_library As String, ByVal str_username As String, ByVal str_password As String, ByVal str_FTPfilename As String, ByVal str_JBAFilename As String)
        Try
            Dim str_FTPCommandFileName As String = sAS_NAPARootFolder & "NAPAFiles\ftpCommand.ftp"
            If System.IO.File.Exists(str_FTPCommandFileName) Then IO.File.Delete(str_FTPCommandFileName)
            Dim oWrite As IO.TextWriter = IO.File.CreateText(str_FTPCommandFileName)
            oWrite.WriteLine("open " & str_server)
            oWrite.WriteLine("user " & str_username)
            oWrite.WriteLine(str_password)
            oWrite.WriteLine("cd " & str_library)
            oWrite.WriteLine("put """ & str_FTPfilename & """ " & str_JBAFilename)
            oWrite.WriteLine("bye")
            oWrite.Flush()
            oWrite.Close()
            oWrite = Nothing
            Try
                Shell("ftp -n -s:" & str_FTPCommandFileName, AppWinStyle.MinimizedNoFocus)
            Finally
                'If System.IO.File.Exists(str_FTPCommandFileName) Then IO.File.Delete(str_FTPCommandFileName)
            End Try
        Catch ex As Exception
            ' Some thing goes wrong
        End Try
    End Sub

    Public Sub createRGBPicturesQuest()
        Dim dsColors As DataSet = getSelectDataSet("SELECT * FROM QUESTPDMNAPA.dbo.INTCOLOR")
        For Each dsRow As DataRow In dsColors.Tables(0).Rows
            createRGBImage(10, 10, My.Settings.RGBPictureLocation & IIf(My.Settings.RGBPictureLocation.EndsWith("\"), "", "\") & dsRow("colartno") & ".jpg", dsRow("red"), dsRow("green"), dsRow("blue"))
        Next
    End Sub

    Public Sub updateColors(Optional ByVal WhereClause As String = "")
        Dim tableAdapter As New SqlDataAdapter
        Dim tableData As New DataSet
        Dim rowCounter As Integer

        tableAdapter.SelectCommand = dcNAPA.CreateCommand
        tableAdapter.SelectCommand.CommandText = "SELECT DevNo FROM newGrid " & IIf(WhereClause.Length > 0, WhereClause, "")
        tableAdapter.Fill(tableData)
        tableData.CreateDataReader()
        For rowCounter = 0 To tableData.Tables(0).Rows.Count - 1
            frmMainGrid.updateColorRow(tableData.Tables(0).Rows(rowCounter).Item("DevNo"), False)
        Next
        MsgBox("Colors updated!", MsgBoxStyle.Information, "Updated!")
    End Sub


    Public Sub writetomyLogFile(sString As String)
        Dim sFilePath As String
        Dim myFile As StreamWriter

        sFilePath = "C:\\VFFILES\\lakshms\\TestSupport\\NapaLogs\\Napamylog.txt"
        myFile = New StreamWriter(sFilePath, True)

        myFile.WriteLine("              ")
        myFile.WriteLine(sString)
        myFile.WriteLine("              ")
        myFile.Close()

    End Sub

    Public Function getFormulaFields(ByVal sFields As String) As String
        'sFields = sFields.Replace("", "")
        sFields = sFields.Replace("+", ",")
        sFields = sFields.Replace("-", ",")
        sFields = sFields.Replace("*", ",")
        sFields = sFields.Replace("/", ",")
        sFields = sFields.Replace("%", ",")
        sFields = sFields.Replace("(", "")
        sFields = sFields.Replace(")", "")
        sFields = sFields.Replace("[", "")
        sFields = sFields.Replace("]", "")
        sFields = sFields.Replace("{", "")
        sFields = sFields.Replace("}", "")
        sFields = sFields.Replace("=", "")
        sFields = Trim(sFields)
        Return sFields
    End Function

    Public Sub setAppsettings()
        Dim dtSetting As DataTable
        dtSetting = getSelectDataSet("Select * from ll_appsettings").Tables(0)
        If dtSetting.Rows.Count > 0 Then
            sAS_StartupPath = (dtSetting.Select("settingname = 'StartupPath'"))(0)("settingvalue")
        End If
    End Sub
End Module
