
using System.Collections.ObjectModel;
using OfflineMobileApp.Models;
using OfflineMobileApp.Services;

namespace OfflineMobileApp.ViewModels;

public class ProductViewModel
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Product> Products { get; set; } = new();

    public ProductViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task LoadProductsAsync()
    {
        var products = await _databaseService.GetProductsAsync();

        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }
}
