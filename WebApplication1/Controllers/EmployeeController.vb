Imports System.Web.Mvc

Namespace Controllers
    Public Class EmployeeController
        Inherits Controller

        ' GET: Employee
        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace