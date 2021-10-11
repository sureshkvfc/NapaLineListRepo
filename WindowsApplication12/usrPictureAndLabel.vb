Public Class usrPictureAndLabel
    Private imgBox As Image
    Public StyleName As String
    Public DevNo As String
    Public itemNr As Integer
    Public Event uMouseDown(ByVal uControl As Object, ByVal StyleName As String, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event uDoubleClick(ByVal uControl As Object, ByVal StyleName As String)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub updateStyle(ByVal str_DevNo As String, ByVal str_StyleName As String, ByVal i_itemNr As Integer)
        StyleName = str_StyleName
        DevNo = str_DevNo
        lblLabel.Text = str_StyleName
        imgBox = getPicture(str_StyleName)
        pictBox.Image = imgBox
        usrPictureAndLabel_Resize(Nothing, Nothing)
        itemNr = i_itemNr
    End Sub

    Private Function getPicture(ByVal StyleName As String) As Image
        Dim dataSet As DataSet = getSelectDataSet("SELECT picBlob, picWidth, picHeight FROM dbo.vf_Sketches WHERE lotnumber='" & DevNo & "'")
        If dataSet.Tables(0).Rows.Count > 0 Then
            Dim imageAsBytes As Byte() = dataSet.Tables(0).Rows(0).Item("picBlob")
            Dim picWidth As Integer = dataSet.Tables(0).Rows(0).Item("picWidth")
            Dim picHeight As Integer = dataSet.Tables(0).Rows(0).Item("picHeight")

            getPicture = Image.FromStream(New System.IO.MemoryStream(imageAsBytes))
        Else
            getPicture = Image.FromFile(sAS_StartupPath & "\Resources\napa logo.gif")
        End If
    End Function

    Private Sub usrPictureAndLabel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        lblLabel.Height = 30
        lblLabel.Width = Me.Width
        pictBox.Size = fitPictureBox(imgBox, Me.Width - 5, Me.Height - lblLabel.Height - 5)
        pictBox.Top = (Me.Height - lblLabel.Height - pictBox.Height) / 2
        pictBox.Left = (Me.Width - pictBox.Width) / 2
        lblLabel.Top = Me.Height - lblLabel.Height - 5
    End Sub

    Private Function fitPictureBox(ByVal boxImage As Image, ByVal availWidth As Integer, ByVal availHeight As Integer) As Size
        Dim destWidth, destHeight As Integer

        If Not boxImage Is Nothing Then
            destWidth = boxImage.Width / (boxImage.Height / availHeight)
            If destWidth < availWidth Then
                destHeight = availHeight
            Else
                destWidth = availWidth
                destHeight = boxImage.Height / (boxImage.Width / availWidth)
            End If
            fitPictureBox = New Size(destWidth, destHeight)
        End If
    End Function

    Private Sub Control_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pictBox.MouseDown, lblLabel.MouseDown, Me.MouseDown
        RaiseEvent uMouseDown(Me, StyleName, e)
    End Sub

    Private Sub Control_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pictBox.DoubleClick, lblLabel.DoubleClick, Me.DoubleClick
        RaiseEvent uDoubleClick(Me, StyleName)
    End Sub
End Class
