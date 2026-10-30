
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
            await DisplayAlert(
                "Error",
                $"Could not load customers: {ex.Message}",
                "OK");
        }
    }
}
