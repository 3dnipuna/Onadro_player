Imports System.IO
Public Class Form1

    Dim current As String
    Dim file As System.IO.StreamWriter
    Dim reader As System.IO.StreamReader
    Dim listcount As String
    Dim timestamp As String
    Dim nowtime As String
    Dim webcount As Integer
    Dim imgcount As Integer
    Dim img_timing As Integer



    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

        file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
       ' file.WriteLine(timestamp & "|" & "Player Disposed")
        file.Close()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
        file.WriteLine(timestamp & "|" & "Player form Closed")
        file.Close()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)

        file.WriteLine(timestamp & "|" & "Player Closing")
        file.Close()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Public Sub read_playsettings()

        Dim fs3 As System.IO.FileStream
        Dim path3 As String

        path3 = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\play_settings.txt"
        fs3 = New System.IO.FileStream(path3,
                                      IO.FileMode.Open, IO.FileAccess.Read)

        Dim plsetting_reader As StreamReader = New StreamReader(fs3)
        Dim plsettgin_line As String

        plsettgin_line = plsetting_reader.ReadLine()

        Label4.Text = plsettgin_line.ToString

        plsetting_reader.Close()


    End Sub
    Public Sub pros_showKey()
        If Keys.ControlKey + Keys.ShiftKey + Keys.S Then
            Label1.Visible = True
            Label2.Visible = True
            Label3.Visible = True
            Label4.Visible = True
            Label5.Visible = True
            ListBox1.Visible = True

           
        End If
    End Sub

    Public Sub pros_hideKey()
        If Keys.ControlKey + Keys.ShiftKey + Keys.H Then
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
            ListBox1.Visible = False


        End If
    End Sub

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        pros_showKey()
        pros_hideKey()

    End Sub

    Public Sub creat_folderset()
        Try

       
        If (Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Logs\")) Then
            System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Logs\")
        End If

        If (Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\")) Then
            System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\")
        End If

        If (Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\temp_down\")) Then
            System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\temp_down\")
            End If

            Dim filepath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\play_settings.txt"
            If Not System.IO.File.Exists(filepath) Then
                System.IO.File.Create(filepath).Dispose()

                Dim filewrite As StreamWriter = New StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\play_settings.txt")
                filewrite.WriteLine("no")
                filewrite.Close()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load


        creat_folderset()


        Panel1.Width = Me.Width
        Panel1.Height = Me.Height

        WebBrowser1.Visible = False
        PictureBox1.Visible = False

        ' Panel1.Visible = False

        Dim timenow As String = DateTime.Now.ToString("H:mm")

        Label5.Text = timenow

        Timer4.Start()


        read_playsettings()




        Label3.Text = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Logs\" & DateTime.Now.ToString("yyyy-MM-dd")
        timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:tt")

        file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
        file.WriteLine(timestamp & "|" & "Player Started")
        file.Close()

        Timer1.Start()
        Timer2.Start()

        Me.WindowState = FormWindowState.Maximized ' full screen window------

        AxWindowsMediaPlayer1.Width = Me.Width
        AxWindowsMediaPlayer1.Height = Me.Height
        AxWindowsMediaPlayer1.uiMode = "none" ' media player controller hide..........



        ' Label1.Text = ""

        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
        Label10.Visible = False
        ListBox1.Visible = False

        If Not System.IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\playelist.txt") Then

            ' MsgBox("Contact admin")
            file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
            file.WriteLine(timestamp & "|" & "Error-playelist.txt not found")
            file.Close()

        Else

            reader = My.Computer.FileSystem.OpenTextFileReader(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\playelist.txt")

            reader.ReadLine()
            reader.ReadLine()
            reader.ReadLine()


            Do While reader.Peek() <> -1
                'AxWindowsMediaPlayer1.URL = "Data\" & Label2.Text
                ListBox1.Items.Add(reader.ReadLine().ToString) ' adding all videos to list box with "do while" loop
            Loop

            ListBox1.SelectedIndex = 0 ' auto select listbox first item-----------------------------
            listcount = ListBox1.Items.Count.ToString
            reader.Close() 'closed text file----------

            'AxWindowsMediaPlayer1.settings.setMode("loop", True)

            Timer3.Start()

        End If
    End Sub

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        AxWindowsMediaPlayer1.Width = Me.Width
        AxWindowsMediaPlayer1.Height = Me.Height

        Panel1.Width = Me.Width
        Panel1.Height = Me.Height
    End Sub

    Private Sub AxWindowsMediaPlayer1_KeyPressEvent(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_KeyPressEvent) Handles AxWindowsMediaPlayer1.KeyPressEvent
        pros_showKey()
        pros_hideKey()


    End Sub

    Private Sub AxWindowsMediaPlayer1_StatusChange(sender As Object, e As EventArgs) Handles AxWindowsMediaPlayer1.StatusChange



        'current = AxWindowsMediaPlayer1.URL.ToString
        current = ListBox1.SelectedItem.ToString

        If (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying) Then

            Label1.Text = timestamp & "|" & current & "|Playing"
            Label2.Text = "Playing"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped) Then

            Label1.Text = timestamp & "|" & current & "|Stopped"
            Label2.Text = "Stopped"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsMediaEnded) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media ended"
            Label2.Text = "media ended"
            
            ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1
            AxWindowsMediaPlayer1.Ctlcontrols.play()

            

        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsReady) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media ready"
            Label2.Text = "media ready"
            

           

        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsBuffering) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media Buffering"
            Label2.Text = "media Buffering"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsLast) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media Last"
            Label2.Text = "media Last"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPaused) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media Paused"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsReconnecting) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media Reconecting"
            


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsWaiting) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media waiting"
           


        ElseIf (AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsUndefined) Then

            Label1.Text = timestamp & "|" & Microsoft.VisualBasic.Right(current, 6) & "|media Undifined"
            

        End If

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Dim fs As System.IO.FileStream
        Dim path As String

        If (ListBox1.SelectedItem.ToString.Substring(0, 3) = "vid") Then

            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\" & ListBox1.SelectedItem.ToString.Substring(4, ListBox1.SelectedItem.ToString.Length - 4)) Then
                AxWindowsMediaPlayer1.URL = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\" & ListBox1.SelectedItem.ToString.Substring(4, ListBox1.SelectedItem.ToString.Length - 4)
            Else

                file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
                file.WriteLine(timestamp & "|" & "Error- " & ListBox1.SelectedItem.ToString & " not found")
                file.Close()
                ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1
            End If

        End If
       

        ' Label3.Text = AxWindowsMediaPlayer1.URL.ToString

        If (ListBox1.SelectedIndex = listcount - 1) Then
            ListBox1.SelectedIndex = 0
        End If

        If (ListBox1.SelectedItem.ToString.Substring(0, 3) = "web") Then
            'MsgBox(ListBox1.SelectedItem.ToString.Substring(0, 3))

            WebBrowser1.Dock = DockStyle.Fill
            webcount = 0

            WebBrowser1.Navigate(New Uri(ListBox1.SelectedItem.ToString.Substring(9, ListBox1.SelectedItem.ToString.Length - 9)))
            WebBrowser1.Refresh()
            WebBrowser1.Visible = True
            Label9.Text = Integer.Parse(ListBox1.SelectedItem.ToString.Substring(4, 4))
            web_timer.Start()

            file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
            file.WriteLine(timestamp & "|" & "Web Plaing")
            file.Close()

            ' MsgBox(ListBox1.SelectedItem.ToString.Substring(4, ListBox1.SelectedItem.ToString.Length - 4))
        End If

        If (ListBox1.SelectedItem.ToString.Substring(0, 3) = "img") Then

            

            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\" & ListBox1.SelectedItem.ToString.Substring(9, ListBox1.SelectedItem.ToString.Length - 9)) Then

                path = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\" & ListBox1.SelectedItem.ToString.Substring(9, ListBox1.SelectedItem.ToString.Length - 9)
                fs = New System.IO.FileStream(path,
                                          IO.FileMode.Open, IO.FileAccess.Read)

                PictureBox1.Visible = True
                PictureBox1.Dock = DockStyle.Fill
                PictureBox1.Image = System.Drawing.Image.FromStream(fs)
                fs.Close()
                Label8.Text = Integer.Parse(ListBox1.SelectedItem.ToString.Substring(4, 4))


                img_timer.Start()
                file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
                file.WriteLine(timestamp & "|" & "img Plaing ->" & ListBox1.SelectedItem.ToString.Substring(9, ListBox1.SelectedItem.ToString.Length - 9))
                file.Close()
            Else

                file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
                file.WriteLine(timestamp & "|" & "Error- " & ListBox1.SelectedItem.ToString & " not found")
                file.Close()
                ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1
            End If


            

        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer2.Tick

       

        Me.TopMost = True

        If (Label2.Text = "media ready") Then
            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\" & ListBox1.SelectedItem.ToString.Substring(4, ListBox1.SelectedItem.ToString.Length - 4)) Then
                AxWindowsMediaPlayer1.Ctlcontrols.play()


            Else

                file = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Logs\" & DateTime.Now.ToString("yyyy-MM-dd") & "-log.txt", True)
                file.WriteLine(timestamp & "|" & ListBox1.SelectedItem.ToString & "|Error-File not in Folder")
                file.Close()
                ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1
                AxWindowsMediaPlayer1.Ctlcontrols.play()
            End If


        End If

       
        



    End Sub

    Public Sub reAddto_list()

        reader = My.Computer.FileSystem.OpenTextFileReader(My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Data\playelist.txt")

        reader.ReadLine()
        reader.ReadLine()
        reader.ReadLine()

        ListBox1.Items.Clear()


        Do While reader.Peek() <> -1
            'AxWindowsMediaPlayer1.URL = "Data\" & Label2.Text
            ListBox1.Items.Add(reader.ReadLine().ToString) ' adding all videos to list box with "do while" loop
        Loop

        ListBox1.SelectedIndex = 0 ' auto select listbox first item-----------------------------
        listcount = ListBox1.Items.Count.ToString
        reader.Close() 'closed text file----------
    End Sub


    Private Sub Label1_TextChanged(sender As Object, e As EventArgs) Handles Label1.TextChanged

        timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:tt")
        file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
        file.WriteLine(Label1.Text)
        file.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\Logs\" & DateTime.Now.ToString("yyyy-MM-dd")
    End Sub

    Private Sub AxWindowsMediaPlayer1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AxWindowsMediaPlayer1.Enter

    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Dim fs2 As System.IO.FileStream
        Dim path2 As String

        If Label4.Text = "yes" Then

            reAddto_list()
            path2 = My.Computer.FileSystem.SpecialDirectories.MyDocuments.ToString & "\Onadro\play_settings.txt"

            fs2 = New System.IO.FileStream(path2,
                                      IO.FileMode.Open, IO.FileAccess.ReadWrite)

            Dim plsetting_write As StreamWriter = New StreamWriter(fs2)

            plsetting_write.WriteLine("no")

            plsetting_write.Close()

            fs2.Close()

        End If

        read_playsettings()


    End Sub
    Public Sub mytmer()

        Dim now As DateTime = DateTime.Now
        Dim starttime As New DateTime(now.Year, now.Month, now.Day, 7, 0, 0)
        Dim offtime As New DateTime(now.Year, now.Month, now.Day, 22, 0, 0)

        If starttime <= now AndAlso now <= offtime Then
            ' Do something
            Panel1.Visible = False
            Label10.Text = "on"
        Else
            Panel1.Visible = True
            Panel1.BringToFront()

            Label10.Text = "off"

        End If

    End Sub
    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick

        ' nowtime = DateTime.Now.ToString("H:mm")

        ' If nowtime > "7:00" Or nowtime < "22:00" Then
        'Panel1.Visible = False
        ' Else
        '  Panel1.Visible = True
        '  End If

        mytmer()


        Label5.Text = nowtime
    End Sub

  
    Private Sub web_timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles web_timer.Tick

        webcount = webcount + 1
        Label6.Text = webcount

    End Sub


    Private Sub Label6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label6.TextChanged

        If Label6.Text = Label9.Text Then
            WebBrowser1.Visible = False
            web_timer.Stop()
            Label6.Text = "0"
            Label9.Text = "0"
            webcount = 0
            ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1

        End If
    End Sub

    Private Sub Label7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label7.TextChanged

        If Label7.Text = Label8.Text Then
            PictureBox1.Visible = False
            img_timer.Stop()
            Label7.Text = "0"
            Label8.Text = "0"
            imgcount = 0
            ListBox1.SelectedIndex = Me.ListBox1.SelectedIndex + 1
        End If

    End Sub

    Private Sub img_timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles img_timer.Tick
        imgcount = imgcount + 1
        Label7.Text = imgcount
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub


    Private Sub Label10_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label10.TextChanged
        If Label10.Text = "on" Then
            file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
            file.WriteLine(timestamp & "|" & "--------------Screen On-----------")
            file.Close()

        ElseIf Label10.Text = "off" Then

            file = My.Computer.FileSystem.OpenTextFileWriter(Label3.Text & "-log.txt", True)
            file.WriteLine(timestamp & "|" & "--------------Screen Off-----------")
            file.Close()

        End If
    End Sub
End Class
