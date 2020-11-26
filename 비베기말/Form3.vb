Public Class Form3
    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("button21")
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        Form2.Show()
        Me.Close()
    End Sub
End Class