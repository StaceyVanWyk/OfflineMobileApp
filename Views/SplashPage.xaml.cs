

using OfflineMobileApp.Services;
using OfflineMobileApp.Views;
namespace OfflineMobileApp.Views;

public partial class SplashPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly CustomerPage _customerPage;
    private bool _hasNavigated;

    public SplashPage(
        DatabaseService databaseService,
        CustomerPage customerPage)
    {
        InitializeComponent();

        _databaseService = databaseService;
        _customerPage = customerPage;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_hasNavigated)
            return;

        _hasNavigated = true;

        try
        {
            await _databaseService.InitializeAsync();
            await _databaseService.SeedCategoriesAsync();
            await _databaseService.SeedCustomersAsync();
            await _databaseService.SeedProductsAsync();

            await Task.Delay(2000);

            await Navigation.PushAsync(_customerPage);
            Navigation.RemovePage(this);
        }
        catch (Exception ex)
        {
            _hasNavigated = false;
            await DisplayAlert("Startup Error", ex.Message, "OK");
        }
    }
}
