namespace OfflineMobileApp.Models;


public class Product
{
    public int ProductID {get; set;}
    public int CategoryID {get; set;}
    public string ProductName {get; set;} = string.Empty;
    public string ? Description {get; set;}
    public decimal Price {get;set;}
    public int StockQuantity {get; set;}

}