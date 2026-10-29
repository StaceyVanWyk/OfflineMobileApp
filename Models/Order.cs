namespace OfflineMobileApp.Models;

// Complete the Order class with properties for OrderID, CustomerID, OrderDate, and TotalAmount
public class Order
{
    
    public int OrderID {get; set;}
    public int CustomerID {get;set;}
    public DateTime OrderDate {get; set;}
    public decimal TotalAmount {get; set;}

}