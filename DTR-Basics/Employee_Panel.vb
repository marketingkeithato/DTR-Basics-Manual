Imports MySql.Data.MySqlClient
Public Class Employee_Panel

    Private Sub Load_view_of_Employee()
        Dim connetionString As String
        Dim sqlCnn As MySqlConnection
        Dim sqlCmd As MySqlCommand
        Dim sql As String
        connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
        sql = "select employee_id,employee_name from employee"
        Try
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            ListView1.Items.Clear()
            Do While sqlReader.Read = True
                Dim lvi As New ListViewItem(sqlReader(0).ToString)
                lvi.SubItems.AddRange(New String() {sqlReader(1).ToString})
                ListView1.Items.Add(lvi)
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            MsgBox("Cannot Connect to the Database")
        End Try
    End Sub
    Private Sub Render_ID_Employee()
        Dim connetionString As String
        Dim sqlCnn As MySqlConnection
        Dim sqlCmd As MySqlCommand
        Dim sql As String
        connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
        sql = "select employee_id from employee order by employee_id desc"
        Try
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read = True Then
                TextBox2.Text = Val(sqlReader("employee_id").ToString) + 1
            Else
                TextBox2.Text = 1
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception

            MsgBox("Cannot connect to the Database ! ")
        End Try
    End Sub
    Private Function Render_Password_With_Validation()
        If TextBox4.Text = TextBox5.Text Then
            Return 1
        Else
            MsgBox("Please Match the Password of Enter Password and Confirm Password")
            Return 0

        End If
    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If Render_Password_With_Validation() = 1 Then
                If Button4.Text = "Add New Employee" Then
                    Dim connString As String
                    connString = "server=localhost;user=root;password=;database=dtr"
                    Dim conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim InsertCommand As String
                    InsertCommand = "insert into employee(employee_id,employee_name,employee_dtr_password) VALUES(@employee_id,@employee_name,@employee_dtr_password)"
                    Dim cmd = New MySqlCommand(InsertCommand, conn)
                    cmd.Parameters.AddWithValue("@employee_id", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@employee_name", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@employee_dtr_password", TextBox4.Text)
                    cmd.Prepare()
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                    Render_ID_Employee()
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                    Load_view_of_Employee()
                    MsgBox("Sucessfully Added New Employee")
                ElseIf Button4.Text = "Edit Employee Data" Then
                    Dim connString As String
                    connString = "server=localhost;user=root;password=;database=dtr"
                    Dim conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim InsertCommand As String
                    InsertCommand = "UPDATE employee SET employee_name=@employee_name,employee_dtr_password=@employee_dtr_password WHERE employee_id=@employee_id"
                    Dim cmd = New MySqlCommand(InsertCommand, conn)
                    cmd.Parameters.AddWithValue("@employee_id", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@employee_name", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@employee_dtr_password", TextBox4.Text)
                    cmd.Prepare()
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                    Load_view_of_Employee()
                    MsgBox("Sucessfully Editted New Employee Data")
                End If
            Else
                    ' Do Nothing
                End If
        Catch ex As Exception
            MsgBox("Cannot Connect to the Database")
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button4.Text = "Add Employee"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        Render_ID_Employee()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button4.Text = "Edit Employee Data"
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Render_Password_With_Validation() = 1 Then
            Try
                Dim connString As String
                connString = "server=localhost;user=root;password=;database=dtr"
                Dim conn As New MySqlConnection(connString)
                conn.Open()
                Dim InsertCommand As String
                InsertCommand = "delete from employee WHERE employee_id=@employee_id"
                Dim cmd = New MySqlCommand(InsertCommand, conn)
                cmd.Parameters.AddWithValue("@employee_id", ListView1.SelectedItems(0).SubItems(0).Text)
                cmd.Prepare()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
                MsgBox("Sucessfully Deleted Existing New Employee")
            Catch ex As Exception
                MsgBox("Cannot Connect to the Database")
            End Try
            Load_view_of_Employee()

        End If
    End Sub

    Private Sub Employee_Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_view_of_Employee()
        Render_ID_Employee()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim connetionString As String
        Dim sqlCnn As MySqlConnection
        Dim sqlCmd As MySqlCommand
        Dim sql As String
        connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
        sql = "select employee_id,employee_name from employee where employee_name like '%" & TextBox1.Text & "%'"
        Try
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            ListView1.Items.Clear()
            Do While sqlReader.Read = True
                Dim lvi As New ListViewItem(sqlReader(0).ToString)
                lvi.SubItems.AddRange(New String() {sqlReader(1).ToString})
                ListView1.Items.Add(lvi)
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            MsgBox("Cannot Connect to the Database")
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 And Button4.Text = "Edit Employee Data" Then
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select employee_id,employee_name,employee_dtr_password from employee where employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "'"
            Try
                sqlCnn = New MySqlConnection(connetionString)
                sqlCnn.Open()
                sqlCmd = New MySqlCommand(sql, sqlCnn)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
                If sqlReader.Read <> 0 Then
                    TextBox2.Text = sqlReader(0).ToString
                    TextBox3.Text = sqlReader(1).ToString
                    TextBox4.Text = sqlReader(2).ToString
                    TextBox5.Text = sqlReader(2).ToString
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