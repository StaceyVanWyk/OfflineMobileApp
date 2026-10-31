
using OfflineMobileApp.Services;
using OfflineMobileApp.ViewModels;

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
            var cartItems = await _databaseService
                .GetCartDetailsAsync(_customerId);

            CartItemsView.ItemsSource = cartItems;

            decimal total = cartItems.Sum(item => item.Subtotal);

            TotalLabel.Text = $"R {total:F2}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Cart Error", ex.Message, "OK");
        }
    }

    private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        if (sender is not Button button ||
            button.CommandParameter is not CartItemViewModel item)
            return;

        try
        {
            if (item.Quantity >= item.AvailableStock)
            {
                await DisplayAlert(
                    "Stock Limit",
                    "You have reached the available stock.",
                    "OK");

                return;
            }

            await _databaseService.UpdateCartItemQuantityAsync(
                item.CartItemID,
                item.Quantity + 1);

            await LoadCartAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        if (sender is not Button button ||
            button.CommandParameter is not CartItemViewModel item)
            return;

        try
        {
            if (item.Quantity <= 1)
                return;

            await _databaseService.UpdateCartItemQuantityAsync(
                item.CartItemID,
                item.Quantity - 1);

            await LoadCartAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void OnRemoveItemClicked(object sender, EventArgs e)
{
    if (sender is not Button button ||
        button.CommandParameter is not CartItemViewModel item)
        return;

    bool confirm = await DisplayAlert(
        "Remove Item",
        $"Are you sure you want to remove {item.ProductName} from your cart?",
        "Yes, Remove",
        "Cancel");

    if (!confirm)
        return;

    try
    {
        await _databaseService.RemoveFromCartAsync(item.CartItemID);

        await LoadCartAsync();

        await DisplayAlert(
            "Item Removed",
            $"{item.ProductName} has been removed from your cart.",
            "OK");
    }
    catch (Exception ex)
    {
        await DisplayAlert("Error", ex.Message, "OK");
    }
}
private async void OnCheckoutClicked(object sender, EventArgs e)
{
    bool confirm = await DisplayAlert(
        "Confirm Order",
        "Are you sure you want to complete this purchase?",
        "Confirm",
        "Cancel");

    if (!confirm)
        return;

    try
    {
        var order = await _databaseService.CheckoutAsync(_customerId);

        await LoadCartAsync();

        await DisplayAlert(
            "Order Successful!",
            $"Your order has been placed successfully.\n\n" +
            $"Order Number: {order.OrderID}\n" +
            $"Total: R {order.TotalAmount:F2}",
            "Continue Shopping");
    }
    catch (Exception ex)
    {
        await DisplayAlert(
            "Checkout Failed",
            ex.Message,
            "OK");
    }
}
}
