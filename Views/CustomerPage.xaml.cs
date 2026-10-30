
using OfflineMobileApp.ViewModels;

namespace OfflineMobileApp.Views;

public partial class CustomerPage : ContentPage
{
    private readonly CustomerViewModel _viewModel;

    public CustomerPage(CustomerViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await _viewModel.LoadCustomersAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnContinueShoppingClicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedCustomer == null)
        {
            await DisplayAlert(
                "Select Customer",
                "Please select your name before continuing.",
                "OK");

            return;
        }

        await Navigation.PushAsync(
            new ProductPage(
                App.Current!.Handler!.MauiContext!.Services
                    .GetRequiredService<ProductViewModel>()));
    }
}
