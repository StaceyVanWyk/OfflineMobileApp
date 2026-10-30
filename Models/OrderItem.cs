using SQLite;
namespace OfflineMobileApp.Models;

public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public int OrderItemID { get; set; }

    public int OrderID { get; set; }

    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}


