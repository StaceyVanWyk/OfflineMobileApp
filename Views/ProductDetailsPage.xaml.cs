using OfflineMobileApp.Services;
using Microsoft.Extensions.DependencyInjection;
using OfflineMobileApp.Models;

namespace OfflineMobileApp.Views;

public partial class ProductDetailsPage : ContentPage
{
    private readonly Product _product;
    private int _quantity = 1;
    private readonly int _customerId;
private readonly DatabaseService _databaseService;

    public ProductDetailsPage(Product product, int customerId)
    {
        InitializeComponent();

        _product = product;
        _customerId = customerId;
        _databaseService = App.Current!.Handler!.MauiContext!.Services
    .GetRequiredService<DatabaseService>();
        BindingContext = _product;

        QuantityLabel.Text = _quantity.ToString();
    }

    private void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        if (_quantity < _product.StockQuantity)
        {
            _quantity++;
            QuantityLabel.Text = _quantity.ToString();
            
        }
        else
        {
            DisplayAlert(
                "Stock Limit",
                "You have reached the available stock.",
                "OK");
        }
    }

    private void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        if (_quantity > 1)
        {
            _quantity--;
            QuantityLabel.Text = _quantity.ToString();
        }
    }
    private async void OnAddToCartClicked(object sender, EventArgs e)
{
    try
    {
        await _databaseService.AddToCartAsync(
            _customerId,
            _product.ProductID,
            _quantity);

        await DisplayAlert(
            "Added to Cart",
            $"{_product.ProductName} has been added to your cart.",
            "OK");
    }
    catch (Exception ex)
    {
        await DisplayAlert("Unable to Add Item", ex.Message, "OK");
    }
}
}
