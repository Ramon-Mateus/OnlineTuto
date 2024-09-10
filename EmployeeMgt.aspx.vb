Public Class EmployeeMgt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("role") IsNot Nothing Then
            If Not Session("role").ToString().Equals("ADMIN") Then
                Response.Redirect("Login.aspx")
            End If
        Else
            Response.Redirect("Login.aspx")
        End If
    End Sub

End Class