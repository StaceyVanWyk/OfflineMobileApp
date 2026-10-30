using SQLite;

namespace OfflineMobileApp.Models;

//
public class Category
{
    [PrimaryKey, AutoIncrement]
    public int CategoryID {get; set;}

    public string CategoryName {get; set;}  = string.Empty;

    public string? Description {get; set;} 
}