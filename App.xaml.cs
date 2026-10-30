
using OfflineMobileApp.Services;
using OfflineMobileApp.Views;

namespace OfflineMobileApp;

public partial class App : Application
{
    private readonly DatabaseService _databaseService;
    private readonly CustomerPage _customerPage;

    public App(DatabaseService databaseService, CustomerPage customerPage)
    {
        InitializeComponent();

        _databaseService = databaseService;
        _customerPage = customerPage;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(_customerPage));
    }

    protected override async void OnStart()
    {
        base.OnStart();

        await _databaseService.InitializeAsync();
        await _databaseService.SeedCategoriesAsync();
        await _databaseService.SeedCustomersAsync();
        await _databaseService.SeedProductsAsync();
    }
}
