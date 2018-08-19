Imports System.Data.Entity
Imports DevTest2
Imports TestDataGenerator

Public Class AppDbInitializer
    Inherits DropCreateDatabaseAlways(Of AppDbContext)

    Protected Overrides Sub Seed(context As AppDbContext)

        Dim rnd As New Random()
        Dim catalog As New Catalog()
        Dim customers As New List(Of Customer)()

        For i As Integer = 1 To 1000
            Dim c As Customer = catalog.CreateInstance(Of Customer)()
            For Each o In c.Orders
                o.Customer = Nothing
            Next
            customers.Add(c)
        Next

        context.Customers.AddRange(customers)

        MyBase.Seed(context)
    End Sub

End Class
