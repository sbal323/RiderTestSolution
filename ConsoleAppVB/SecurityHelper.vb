Public Class SecurityHelper
    Public Shared Sub Execute(procedure As Action(Of Employee))
        Dim employee = new Employee(5)
        procedure.Invoke(employee)
  End Sub
End Class