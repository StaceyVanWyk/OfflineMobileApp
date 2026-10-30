using SQLite;

namespace OfflineMobileApp.Models;

public class CartItem
{
    [PrimaryKey, AutoIncrement]
    public int CartItemID {get; set;}
    public int CustomerID {get; set;}
    public int ProductID {get;set;}
    public int Quantity {get; set;}
}