Imports System.Data.SqlClient

Public Class Signup
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection("Data Source=DESKTOP-SUU6P5E;Initial Catalog=OnlineTuto;Integrated Security=True")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub SignupBtn_Click(sender As Object, e As EventArgs) Handles SignupBtn.Click
        If passwordBox.Text.Equals(passwordConfirmBox.Text) Then
            Try
                Dim sqlQuery As String = "EXEC CreateEmployee @username, @password, @fullname, @email, @phone, @dob, @loe"
                Dim cmd As New SqlCommand(sqlQuery, con)
                cmd.Parameters.AddWithValue("@username", usernameBox.Text)
                cmd.Parameters.AddWithValue("@password", passwordBox.Text)
                cmd.Parameters.AddWithValue("@fullname", fullnameBox.Text)
                cmd.Parameters.AddWithValue("@email", emailBox.Text)
                cmd.Parameters.AddWithValue("@phone", phoneBox.Text)
                cmd.Parameters.AddWithValue("@dob", dateOfBirthBox.Text)
                cmd.Parameters.AddWithValue("@loe", LevelOfEducationList.SelectedValue.ToString())

                If con.State = System.Data.ConnectionState.Closed Then
                    con.Open()
                End If

                Dim rowsAffected = cmd.ExecuteNonQuery()

                If rowsAffected >= 1 Then
                    MessageLbl.Text = "Account created. You can login now."
                Else
                    MessageLbl.Text = "Account not created."
                End If

            Catch ex As SqlException
                MessageLbl.Text = "There's a problem: " + ex.Message.ToString()
            Finally
                con.Close()
            End Try
        Else
            MessageLbl.Text = "Password don't match"
        End If
    End Sub
End Class