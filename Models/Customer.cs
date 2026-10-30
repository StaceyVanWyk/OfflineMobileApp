using SQLite;
namespace OfflineMobileApp.Models;

public class Customer
{
    [PrimaryKey, AutoIncrement]
    public int CustomerID {get; set;}
    public string FullName {get; set;} = string.Empty;
    
}
