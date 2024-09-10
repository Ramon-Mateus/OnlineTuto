Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection("Data Source=DESKTOP-SUU6P5E;Initial Catalog=OnlineTuto;Integrated Security=True")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        Try
            Dim sqlQuery As String = "SELECT username, password, role FROM [UserEmployee] WHERE username=@username"
            Dim cmd As New SqlCommand(sqlQuery, con)
            cmd.Parameters.AddWithValue("@username", usernameBox.Text)

            If con.State = System.Data.ConnectionState.Closed Then
                con.Open()
            End If

            Dim rdr As SqlDataReader = cmd.ExecuteReader()

            While rdr.Read()
                Dim passwordFromDb As String = rdr.GetValue(1).ToString().Trim()
                Dim role As String = rdr.GetValue(2).ToString().Trim()

                If passwordBox.Text.Equals(passwordFromDb) Then
                    If role.Equals("ADMIN") Then
                        Session("role") = role
                        Response.Redirect("EmployeeMgt.aspx")
                    Else
                        Session("role") = role
                        Response.Redirect("Default.aspx")
                    End If
                Else
                    MessageLbl.Text = "Invalid username or password, please try again."
                End If
            End While
        Catch ex As SqlException
            MessageLbl.Text = "There's a problem: " + ex.Message.ToString()
        Finally
            con.Close()
        End Try
    End Sub
End Class