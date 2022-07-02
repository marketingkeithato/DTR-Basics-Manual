Imports MySql.Data.MySqlClient
Public Class Login
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("✖Please Enter the Following Fields")

        Else
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String

            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select staff_username,staff_password from staff where staff_username='" & TextBox1.Text & "' and staff_password='" & TextBox2.Text & "'"
            Try
                sqlCnn = New MySqlConnection(connetionString)
                sqlCnn.Open()
                sqlCmd = New MySqlCommand(sql, sqlCnn)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
                If sqlReader.Read <> 0 Then
                    Me.Hide()
                    MainPanel.Show()
                Else
                    MsgBox("✖Incorrect Username and Password")
                End If
                sqlReader.Close()
                sqlCmd.Dispose()
                sqlCnn.Close()
            Catch ex As Exception
                MsgBox("Cannot Connect to the Database")
            End Try
        End If
    End Sub
End Class