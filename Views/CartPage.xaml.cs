
using OfflineMobileApp.Services;

namespace OfflineMobileApp.Views;

public partial class CartPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly int _customerId;

    public CartPage(DatabaseService databaseService, int customerId)
    {
        InitializeComponent();

        _databaseService = databaseService;
        _customerId = customerId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadCartAsync();
    }

    private async Task LoadCartAsync()
    {
        try
        {
            // Retrieve the selected customer's cart items
            var cartItems = await _databaseService
                .GetCartDetailsAsync(_customerId);

            // Display the cart products
            CartItemsView.ItemsSource = cartItems;

            // Calculate the total amount
            decimal total = cartItems.Sum(item => item.Subtotal);

            
            TotalLabel.Text = $"R {total:F2}";
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Cart Error",
                ex.Message,
                "OK");
        }
    }
}
