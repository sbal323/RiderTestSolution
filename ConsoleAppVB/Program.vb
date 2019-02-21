Imports System

Module Program
    
    
    
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
        Dim procedure1 As new Action(Of Employee)(Sub(employee as Employee)
            Console.Writeline("First sub")
            Console.Writeline(employee.Id)
        End Sub)
        Dim procedure2 As new Action(Of Employee)(Sub(employee as Employee)
            Console.Writeline("Second sub")
            Console.Writeline(employee.Id)
        End Sub)
       
        
        SecurityHelper.Execute([Delegate].Combine(procedure1, procedure2))
    End Sub
End Module
