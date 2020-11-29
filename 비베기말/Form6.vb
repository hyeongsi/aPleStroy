Public Class Form6
    Dim i = 0
    Dim t = 0
    Dim check As Boolean = False
    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        Button1.BackColor = Color.Silver
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        Button1.BackColor = Color.Transparent
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Computer.Audio.Play("sound\MuruengHill.wav", AudioPlayMode.BackgroundLoop)

        PictureBox2.Visible = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If i Mod 2 = 0 Then
            Label1.ForeColor = Color.Red
            If check = True Then
                PictureBox2.BackgroundImage = My.Resources.ResourceManager.GetObject("merry (3)")
            End If
        Else
            Label1.ForeColor = Color.Yellow
            If check = True Then
                PictureBox2.BackgroundImage = My.Resources.ResourceManager.GetObject("merry")
            End If
        End If
        i += 1
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If t Mod 2 = 0 Then
            Label3.ForeColor = Color.Red
        Else
            Label3.ForeColor = Color.Yellow
        End If

        t += 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox1.Visible = False
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Button1.Visible = False
        Button2.Visible = False

        Timer2.Enabled = False

        check = True

        PictureBox2.Visible = True
        Me.BackgroundImage = My.Resources.ResourceManager.GetObject("chrismas")
        My.Computer.Audio.Play("sound\Happyville.wav", AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub Form6_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        If check = True Then
            check = False
            Timer2.Enabled = True
            PictureBox2.Visible = False

            Me.BackgroundImage = My.Resources.ResourceManager.GetObject("Maple_tree_hill")
            PictureBox1.Visible = True
            Label1.Visible = True
            Label2.Visible = True
            Label3.Visible = True
            Button1.Visible = True
            Button2.Visible = True
            My.Computer.Audio.Play("sound\MuruengHill.wav", AudioPlayMode.BackgroundLoop)
        End If
    End Sub
End Class