Public Class Form1
    Dim g As Drawing.Graphics
    Dim x1, y1, x2, y2 As Integer
    Dim flag As Integer = 0

    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        flag = 1
        x1 = e.X
        y1 = e.Y
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If flag = 0 Then
            Exit Sub
        Else
            Dim pen1 As New Drawing.Pen(Color.CadetBlue)
            x2 = e.X
            y2 = e.Y
            g = PictureBox1.CreateGraphics
            g.DrawLine(pen1, x1, y1, x2, y2)
            x1 = x2
            y1 = y2
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        flag = 0
    End Sub
End Class
