Imports MySql.Data.MySqlClient
Public Class Date_Time_Record_Entry

    Dim impede_entry As Integer = 0
    Private Function Find_DTR_ID(ByVal employee_id As String)
        Try
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String

            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select dtr.dtr_id from employee,dtr where employee.employee_id=dtr.employee_id and dtr.employee_id='" & employee_id & "' order by dtr.dtr_id desc"

            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read <> 0 Then
                employee_id = sqlReader(0).ToString
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            Return employee_id
        Catch ex As Exception
            Return employee_id
        End Try
    End Function
    Private Function Render_ID_DTR()
        Dim connetionString As String
        Dim sqlCnn As MySqlConnection
        Dim sqlCmd As MySqlCommand
        Dim sql As String
        Dim dtr_id As String
        connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
        sql = "select dtr_id from dtr order by dtr_id desc"
        Try
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read = True Then
                dtr_id = Val(sqlReader("dtr_id").ToString) + 1
            Else
                dtr_id = 1
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            Return dtr_id
        Catch ex As Exception
            MsgBox("Exception:", ex.ToString)
            Return 0
        End Try
    End Function
    Private Sub Loading_Employee_Names()
        Dim connetionString As String
        Dim sqlCnn As MySqlConnection
        Dim sqlCmd As MySqlCommand
        Dim sql As String
        connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
        sql = "select concat('(',employee_id,')',employee_name) from employee"
        Try
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            ComboBox1.Items.Clear()
            Do While sqlReader.Read = True
                ComboBox1.Items.Add(sqlReader(0).ToString)
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            MsgBox("Cannot Connect to the Database")
        End Try
    End Sub

    Private Function Access_DTr_Employee(ByVal employee_id As String, ByVal DTR_Password As String)
        Try
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            Dim access_dtr As Boolean = 0
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select employee_id from employee where employee_id='" & employee_id & "' and employee_dtr_password='" & DTR_Password & "'"
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read <> 0 Then
                access_dtr = 1
            Else
                access_dtr = 0
                MsgBox("Invalid Password")
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            Return access_dtr

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub Date_Time_Record_Entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Loading_Employee_Names()
        If ComboBox1.Items.Count = 0 Then

        Else
            ComboBox1.SelectedIndex = 0
        End If

        ComboBox2.SelectedIndex = 0
    End Sub
    Private Sub Update_Dtr(ByVal id As String, ByVal dtr_method As String)
        Try
            If impede_entry = 0 Then
                Dim dtr_id As String = Find_DTR_ID(id)
                Dim connString As String
                connString = "server=localhost;user=root;password=;database=dtr"
                Dim conn As New MySqlConnection(connString)
                conn.Open()
                Dim InsertCommand As String
                InsertCommand = "Update dtr Set " + dtr_method + "=@time_now WHERE dtr_id=@dtr_id"
                Dim cmd = New MySqlCommand(InsertCommand, conn)
                cmd.Parameters.AddWithValue("@time_now", Label1.Text)
                cmd.Parameters.AddWithValue("@dtr_id", dtr_id)
                cmd.Prepare()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function Lock_Entry(ByVal employee_id As String, ByVal string_method_number As Integer)
        Try
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            Dim dtr_check_boolean As Boolean = 1
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select * from dtr where dtr_date='" & Format(Now, "short Date") & "' and employee_id='" & employee_id & "'"
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read <> 0 Then

                If String.IsNullOrEmpty(sqlReader(string_method_number).ToString) Or Not String.IsNullOrEmpty(sqlReader(string_method_number - 1).ToString) Then
                    impede_entry = 0
                    dtr_check_boolean = 0
                Else
                    impede_entry = 1
                    dtr_check_boolean = 1
                End If
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()

            Return dtr_check_boolean
        Catch ex As Exception
            Return 1
        End Try
    End Function
    Private Function Check_Dtr_if_Time_In_Out_Exist(ByVal employee_id As String, ByVal string_method As String)
        Try
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            Dim dtr_check_boolean As Boolean = 1
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select " + string_method + " from dtr where dtr_date='" & Format(Now, "short Date") & "' and employee_id='" & employee_id & "'"
            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            If sqlReader.Read <> 0 Then
                dtr_check_boolean = 1
                If Len(sqlReader(0).ToString) = 0 Then
                    dtr_check_boolean = 0
                End If
            Else
                dtr_check_boolean = 0
            End If

            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            Return dtr_check_boolean
        Catch ex As Exception
            Return 1
        End Try
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            impede_entry = 0
            Dim split_id_name() As String = ComboBox1.SelectedItem.ToString.Split(")")
            Dim split_time_meridiem() As String = Label1.Text.Split(" ")
            Dim split_time_minutes() As String = split_time_meridiem(0).Split(":")
            split_id_name(0) = split_id_name(0).Remove(0, 1)

            If Access_DTr_Employee(split_id_name(0), TextBox1.Text) = True Then
                If Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_morning_in") = False And ComboBox2.Text = "Morning Time In" Then
                    Dim connString As String
                    connString = "server=localhost;user=root;password=;database=dtr"
                    Dim conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim InsertCommand As String
                    InsertCommand = "insert into dtr(dtr_id,employee_id,dtr_morning_in,dtr_date) VALUES(@dtr_id,@employee_id,@dtr_morning_in,@dtr_date)"
                    Dim cmd = New MySqlCommand(InsertCommand, conn)
                    cmd.Parameters.AddWithValue("@dtr_id", Render_ID_DTR)
                    cmd.Parameters.AddWithValue("@employee_id", split_id_name(0))
                    cmd.Parameters.AddWithValue("@dtr_morning_in", Label1.Text)
                    cmd.Parameters.AddWithValue("@dtr_date", Format(Now, "short Date"))
                    cmd.Prepare()
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                ElseIf Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_morning_out") = False And ComboBox2.Text = "Morning Time Out" And Lock_Entry(split_id_name(0), 3) = 0 Then
                    Update_Dtr(split_id_name(0), "dtr_morning_out")
                ElseIf Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_afternoon_in") = False And ComboBox2.Text = "Afternoon Time In" And split_time_meridiem(1) = "PM" And Lock_Entry(split_id_name(0), 4) = 0 Then
                    Update_Dtr(split_id_name(0), "dtr_afternoon_in")
                ElseIf Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_afternoon_out") = False And ComboBox2.Text = "Afternoon Time Out" And split_time_meridiem(1) = "PM" And Lock_Entry(split_id_name(0), 5) = 0 Then
                    Update_Dtr(split_id_name(0), "dtr_afternoon_out")
                ElseIf Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_overtime_in") = False And ComboBox2.Text = "OverTime In" And split_time_meridiem(1) = "PM" And Lock_Entry(split_id_name(0), 6) = 0 And Val(split_time_minutes(0)) >= 5 Then
                    Update_Dtr(split_id_name(0), "dtr_overtime_in")
                ElseIf Check_Dtr_if_Time_In_Out_Exist(split_id_name(0), "dtr_overtime_out") = False And ComboBox2.Text = "OverTime Out" And split_time_meridiem(1) = "PM" And Lock_Entry(split_id_name(0), 7) = 0 And Val(split_time_minutes(0)) >= 5 Then
                    Update_Dtr(split_id_name(0), "dtr_overtime_out")
                Else
                    MsgBox("Invalid DTR Entry")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, "M/d/yyyy")
        Label1.Text = Format(Now, "hh:mm tt")
    End Sub
End Class