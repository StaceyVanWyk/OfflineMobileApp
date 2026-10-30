using SQLite;
namespace OfflineMobileApp.Models;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderID {get; set;}
    public int CustomerID {get;set;}
    public DateTime OrderDate {get; set;}
    public decimal TotalAmount {get; set;}

}