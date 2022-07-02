Public Class MainPanel
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Employee_Panel.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Date_Time_Record_Entry.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Render_Reports.Show()
    End Sub

    Private Sub MainPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class