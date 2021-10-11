Public Class clsColorCell
    Public cellText As String = ""
    Public cellValue As String = ""
    Public colorDropped As Boolean = False

    Public Sub New()
        cellText = ""
        cellValue = ""
        colorDropped = False
    End Sub

    Public Sub New(ByVal str_cellText As String, ByVal str_cellValue As String, ByVal b_colorDropped As Boolean)
        cellText = str_cellText
        cellValue = str_cellValue
        colorDropped = b_colorDropped
    End Sub

    Public Sub setValues(ByVal str_cellText As String, ByVal str_cellValue As String, ByVal b_colorDropped As Boolean)
        cellText = str_cellText
        cellValue = str_cellValue
        colorDropped = b_colorDropped
    End Sub
End Class
