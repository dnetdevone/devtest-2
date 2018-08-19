Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult

        Return View()
    End Function

    Function CsvReport() As ActionResult

        Response.ContentType = "text/plain"

        Dim result As New StringBuilder
        result.AppendLine("CustomerId, Name, OrderCount, SalesTotal")

        Using db As New AppDbContext()


            'get customers
            Dim customers = (From c In db.Customers
                             Order By c.CustomerId
                             Select c).ToList

            For Each c In customers

                result.Append(String.Format("{0}, {1}, ", c.CustomerId, c.Name))

                'get orders
                Dim orders = From o In db.Orders
                             Where o.Customer.CustomerId = c.CustomerId
                             Select o

                Dim orderCount As Integer = 0
                Dim salesTotal As Decimal = 0
                For Each o In orders
                    orderCount += 1
                    salesTotal += o.SalesTotal
                Next

                result.AppendLine(String.Format("{0}, {1}", orderCount, salesTotal.ToString("0.##")))

            Next

        End Using

        Response.Write(result.ToString)
        Response.End()

        Return Nothing
    End Function

End Class
