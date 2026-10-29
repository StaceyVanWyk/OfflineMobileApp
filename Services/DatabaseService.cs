using OfflineMobileApp.Models;
using SQLite;

namespace OfflineMobileApp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    private bool _isInitialized ;

    public DatabaseService(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
        
    }

    public async Task InitializeAsync()

    {
        if (_isInitialized)
            return;
        await _database.CreateTableAsync<Customer>();
        await _database.CreateTableAsync<Category>();
        await _database.CreateTableAsync<Product>();
        await _database.CreateTableAsync<CartItem>();
        await _database.CreateTableAsync<Order>();
        await _database.CreateTableAsync<OrderItem>();
        
        _isInitialized = true;


    }
}