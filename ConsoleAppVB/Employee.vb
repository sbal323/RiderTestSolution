
Public Class Employee
        inherits Item

        Public ReadOnly Property Id As Integer

        Public Sub New(id As Integer)
                MyBase.new(id)
                Me.Id = Id
        End Sub
        
End Class