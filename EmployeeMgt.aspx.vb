Imports System.Data.SqlClient

Public Class EmployeeMgt
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection("Data Source=DESKTOP-SUU6P5E;Initial Catalog=OnlineTuto;Integrated Security=True")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("role") IsNot Nothing Then
            If Not Session("role").ToString().Equals("ADMIN") Then
                Response.Redirect("Login.aspx")
            End If
        Else
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            DisplayEmployees()
        End If
    End Sub

    Public Sub DisplayEmployees()

        Try
            Dim sqlQuery As String = "SELECT emp_id, fullname, email, phone, date_of_birth, level_of_education, salary FROM Employee"
            Dim cmd As New SqlCommand(sqlQuery, con)

            If con.State = System.Data.ConnectionState.Closed Then
                con.Open()
            End If

            Dim sda As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            sda.Fill(dt)
            GridViewList.DataSource = dt
            GridViewList.DataBind()

        Catch ex As SqlException
            MessageLbl.Text = "There's a problem displaying employees: " + ex.Message.ToString()
        Finally
            con.Close()
        End Try
    End Sub

    Protected Sub GridViewList_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "ViewDetails" Then
            Dim emp_id As String = e.CommandArgument.ToString()
            PopulateInputs(emp_id)
        End If
    End Sub

    Public Sub PopulateInputs(ByVal emp_id As String)
        Try
            Dim sqlQuery As String = "SELECT emp_id, fullname, email, phone, date_of_birth, level_of_education, salary FROM Employee WHERE emp_id=@emp_id"
            Dim cmd As New SqlCommand(sqlQuery, con)
            cmd.Parameters.AddWithValue("@emp_id", emp_id)

            If con.State = System.Data.ConnectionState.Closed Then
                con.Open()
            End If

            Dim rdr As SqlDataReader = cmd.ExecuteReader()

            While rdr.Read()
                empIdBox.Text = rdr.GetValue(0).ToString()
                fullnameBox.Text = rdr.GetValue(1).ToString()
                emailBox.Text = rdr.GetValue(2).ToString()
                phoneBox.Text = rdr.GetValue(3).ToString()
                dateOfBirthBox.Text = Convert.ToDateTime(rdr.GetValue(4)).ToString("yyyy-MM-dd")
                levelOfEducationList.SelectedValue = rdr.GetValue(5).ToString()
                salaryBox.Text = rdr.GetValue(6).ToString()
            End While
        Catch ex As SqlException
            MessageLbl.Text = "There's a problem: " + ex.Message.ToString()
        Finally
            con.Close()
        End Try
    End Sub

    Protected Sub UpdateBtn_Click(sender As Object, e As EventArgs) Handles UpdateBtn.Click
        If empIdBox.Text.Trim().Length >= 1 Then
            Try
                Dim sqlQuery As String = "EXEC UpdateEmployee @emp_id, @fullname, @email, @phone, @dob, @loe, @salary"
                Dim cmd As New SqlCommand(sqlQuery, con)

                cmd.Parameters.AddWithValue("@emp_id", empIdBox.Text)
                cmd.Parameters.AddWithValue("@fullname", fullnameBox.Text)
                cmd.Parameters.AddWithValue("@email", emailBox.Text)
                cmd.Parameters.AddWithValue("@phone", phoneBox.Text)
                cmd.Parameters.AddWithValue("@dob", dateOfBirthBox.Text)
                cmd.Parameters.AddWithValue("@loe", levelOfEducationList.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("@salary", salaryBox.Text)

                If con.State = System.Data.ConnectionState.Closed Then
                    con.Open()
                End If

                Dim rowsAffected = cmd.ExecuteNonQuery()

                If rowsAffected >= 1 Then
                    MessageLbl.Text = "Employee updated successfully."
                    DisplayEmployees()
                Else
                    MessageLbl.Text = "Employee not updated."
                End If

            Catch ex As SqlException
                MessageLbl.Text = "There's a problem: " + ex.Message.ToString()
            Finally
                con.Close()
            End Try
        Else
            MessageLbl.Text = "Please select an employee to update."
        End If
    End Sub

    Protected Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If empIdBox.Text.Trim().Length >= 1 Then
            Try
                Dim sqlQuery As String = "EXEC DeleteEmployee @emp_id"
                Dim cmd As New SqlCommand(sqlQuery, con)

                cmd.Parameters.AddWithValue("@emp_id", empIdBox.Text)

                If con.State = System.Data.ConnectionState.Closed Then
                    con.Open()
                End If

                Dim rowsAffected = cmd.ExecuteNonQuery()

                If rowsAffected >= 1 Then
                    MessageLbl.Text = "Employee deleted successfully."
                    DisplayEmployees()
                Else
                    MessageLbl.Text = "Employee not deleted."
                End If

            Catch ex As SqlException
                MessageLbl.Text = "There's a problem: " + ex.Message.ToString()
            Finally
                con.Close()
            End Try
        Else
            MessageLbl.Text = "Please select an employee to delete."
        End If
    End Sub
End Class