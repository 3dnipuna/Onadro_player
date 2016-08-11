Public Class splash
    Dim p() As Process

    Private Sub CheckIfRunning()
        p = Process.GetProcessesByName("SplashPNG")
        If p.Count > 0 Then
            ' Process is running
            '  MsgBox("working")
            Label1.Text = "Terminal Loaded....."
        Else
            ' Process is not running
            'MsgBox("not working")
            Label1.Text = "Terminal Not Loaded....."
        End If
    End Sub
    Private Sub splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Loading..........."
        CheckIfRunning()
    End Sub


  
End Class