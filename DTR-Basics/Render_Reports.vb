Imports MySql.Data.MySqlClient
Public Class Render_Reports

    Public add_hours_for_late As Integer = 0
    Public Sub Render_Total_Hours()
        Try
            Dim total_hours_rendered = 0
            Dim autocatch_12_afternoon As String = "12"
            Dim total_hours_of_overtime = 0
            Dim total_minutes_to_minus = 0
            Dim autofill_blank_dtr_with_0 = "00:00"
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select * from dtr where employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "'"

            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            Do While sqlReader.Read = True
                Dim split_time_meridiem_start_morning() As String = sqlReader(2).ToString.Split(" ")
                Dim split_hour_minutes_start_morning() As String = split_time_meridiem_start_morning(0).ToString.Split(":")
                Dim split_time_meridiem_end_morning() As String = sqlReader(3).ToString.Split(" ")
                Dim split_hour_minutes_end_morning() As String = split_time_meridiem_end_morning(0).ToString.Split(":")
                Dim split_time_meridiem_start_afternoon() As String = sqlReader(4).ToString.Split(" ")
                Dim split_hour_minutes_start_afternoon() As String = split_time_meridiem_start_afternoon(0).ToString.Split(":")
                If split_hour_minutes_start_afternoon(0) = autocatch_12_afternoon Then
                    split_hour_minutes_start_afternoon(0) = "0"
                End If
                Dim split_time_meridiem_end_afternoon() As String = sqlReader(5).ToString.Split(" ")
                Dim split_hour_minutes_end_afternoon() As String = split_time_meridiem_end_afternoon(0).ToString.Split(":")
                Dim split_time_meridiem_start_overtime() As String = sqlReader(6).ToString.Split(" ")
                Dim split_hour_minutes_start_overtime() As String = split_time_meridiem_start_overtime(0).ToString.Split(":")
                Dim split_time_meridiem_end_overtime() As String = sqlReader(7).ToString.Split(" ")
                Dim split_hour_minutes_end_overtime() As String = split_time_meridiem_end_overtime(0).ToString.Split(":")
                total_hours_of_overtime = total_hours_of_overtime + (Val(split_hour_minutes_end_overtime(0)) - Val(split_hour_minutes_start_overtime(0)))
                total_hours_rendered = total_hours_rendered + (Val(split_hour_minutes_end_morning(0)) - Val(split_hour_minutes_start_morning(0)) + (Val(split_hour_minutes_end_afternoon(0))) - Val(split_hour_minutes_start_afternoon(0)))
                total_minutes_to_minus = total_minutes_to_minus + (Val(split_hour_minutes_end_morning(1)) - Val(split_hour_minutes_start_morning(1)) + (Val(split_hour_minutes_end_afternoon(1)) - Val(split_hour_minutes_start_afternoon(1))))
                total_minutes_to_minus = total_minutes_to_minus / 60
                If total_minutes_to_minus < 0 Then
                    total_minutes_to_minus = 0
                Else
                    add_hours_for_late = CStr(Math.Round(total_minutes_to_minus))
                End If
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            TextBox4.Text = total_hours_rendered + total_hours_of_overtime + total_minutes_to_minus
            TextBox5.Text = total_hours_of_overtime
        Catch ex As Exception

        End Try

    End Sub
    Public Sub Render_Absent()
        Try
            Dim total_days_absent = 0
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select * from dtr where dtr_id NOT IN(select dtr_id from dtr where employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "')"

            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            Do While sqlReader.Read = True
                total_days_absent = total_days_absent + 1
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            TextBox7.Text = total_days_absent
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Total_of_Late_Hours()
        Try
            Dim total_hours_Late = 0
            Dim starting_in_morning = 8
            Dim starting_in_afternoon = 12
            Dim connetionString As String
            Dim sqlCnn As MySqlConnection
            Dim sqlCmd As MySqlCommand
            Dim sql As String
            connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
            sql = "select * from dtr where employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "'"

            sqlCnn = New MySqlConnection(connetionString)
            sqlCnn.Open()
            sqlCmd = New MySqlCommand(sql, sqlCnn)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
            Do While sqlReader.Read = True
                total_hours_Late = total_hours_Late - (8 - Val(sqlReader("dtr_morning_in").ToString())) + (Val(sqlReader("dtr_afternoon_in").ToString()) - 1)
            Loop
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
            TextBox6.Text = total_hours_Late + add_hours_for_late
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Display_DTR_By_Date()
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim connetionString As String
                Dim sqlCnn As MySqlConnection
                Dim sqlCmd As MySqlCommand
                Dim sql As String
                connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
                sql = "select employee.employee_id,employee.employee_name,dtr.* from employee,dtr where employee.employee_id=dtr.employee_id and employee.employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "' and dtr.dtr_date between '" & DateTimePicker1.Text.Replace(" ", "") & "' and '" & DateTimePicker2.Text.Replace(" ", "") & "'"
                sqlCnn = New MySqlConnection(connetionString)
                sqlCnn.Open()
                sqlCmd = New MySqlCommand(sql, sqlCnn)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
                ListView3.Items.Clear()
                If sqlReader.Read <> 0 Then
                    TextBox2.Text = sqlReader(0).ToString
                    TextBox3.Text = sqlReader(1).ToString
                    Dim lvi As New ListViewItem(sqlReader(10).ToString)
                    lvi.SubItems.AddRange(New String() {sqlReader(4).ToString, sqlReader(5).ToString, sqlReader(6).ToString, sqlReader(7).ToString, sqlReader(8).ToString, sqlReader(9).ToString})
                    ListView3.Items.Add(lvi)
                End If
                sqlReader.Close()
                sqlCmd.Dispose()
                sqlCnn.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Load_view_of_employee()
        Try
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
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Render_Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_view_of_employee()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.SelectedItems.Count Then
                Dim connetionString As String
                Dim sqlCnn As MySqlConnection
                Dim sqlCmd As MySqlCommand
                Dim sql As String
                connetionString = "Data Source=localhost;Initial Catalog=dtr;user=root;password="
                sql = "select employee.employee_id,employee.employee_name,dtr.* from employee,dtr where employee.employee_id=dtr.employee_id and employee.employee_id='" & ListView1.SelectedItems(0).SubItems(0).Text & "'"

                sqlCnn = New MySqlConnection(connetionString)
                sqlCnn.Open()
                sqlCmd = New MySqlCommand(sql, sqlCnn)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader()
                ListView3.Items.Clear()
                If sqlReader.Read <> 0 Then
                    TextBox2.Text = sqlReader(0).ToString
                    TextBox3.Text = sqlReader(1).ToString
                    Dim lvi As New ListViewItem(sqlReader(10).ToString)
                    lvi.SubItems.AddRange(New String() {sqlReader(4).ToString, sqlReader(5).ToString, sqlReader(6).ToString, sqlReader(7).ToString, sqlReader(8).ToString, sqlReader(9).ToString})
                    ListView3.Items.Add(lvi)
                End If
                sqlReader.Close()
                sqlCmd.Dispose()
                sqlCnn.Close()

                Render_Total_Hours()
                Total_of_Late_Hours()
                Render_Absent()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Display_DTR_By_Date()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Display_DTR_By_Date()
    End Sub

End Class