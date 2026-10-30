using SQLite;
namespace OfflineMobileApp.Models;


public class Product
{
    [PrimaryKey, AutoIncrement]
    public int ProductID {get; set;}
    public int CategoryID {get; set;}
    public string ProductName {get; set;} = string.Empty;
    public string ? Description {get; set;}
    public decimal Price {get;set;}
    public int StockQuantity {get; set;}

    [Ignore]
public string ImageName => ProductName switch
{
    "Cotton T-Shirt" => "cotton_tshirt.png",
    "Denim Jeans" => "denim_jeans.png",
    "Wireless Headphones" => "wireless_headphones.png",
    "USB-C Charger" => "usb_c_charger.png",
    "Coffee Mug" => "coffee_mug.png",
    "Throw Pillow" => "throw_pillow.png",
    _ => "dotnet_bot.png"
};

}