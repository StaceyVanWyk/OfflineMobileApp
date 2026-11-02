
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
        // Show a loading screen while the database is prepared.
        var loadingPage = new ContentPage
        {
            BackgroundColor = Color.FromArgb("#F8F3EB"),
            Content = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 15,
                Children =
                {
                    new ActivityIndicator
                    {
                        IsRunning = true,
                        Color = Color.FromArgb("#F58220")
                    },
                    new Label
                    {
                        Text = "Preparing your shopping experience...",
                        TextColor = Color.FromArgb("#342C25"),
                        FontSize = 14
                    }
                }
            }
        };

        var window = new Window(loadingPage);

        _ = InitializeAppAsync(window);

        return window;
    }

    private async Task InitializeAppAsync(Window window)
    {
        try
        {
            await _databaseService.InitializeAsync();
            await _databaseService.SeedCategoriesAsync();
            await _databaseService.SeedCustomersAsync();
            await _databaseService.SeedProductsAsync();

            // Open the customer page only after seeding is complete.
            window.Page = new NavigationPage(_customerPage);
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current!.Windows[0].Page!
                    .DisplayAlert("Startup Error", ex.Message, "OK");
            });
        }
    }
}
