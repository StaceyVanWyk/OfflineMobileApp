
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using OfflineMobileApp.Services;
using OfflineMobileApp.ViewModels;
using OfflineMobileApp.Views;


namespace OfflineMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        string databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "OfflineShopping.db3");

        builder.Services.AddSingleton<DatabaseService>(
            _ => new DatabaseService(databasePath));

        builder.Services.AddTransient<ProductViewModel>();
        builder.Services.AddTransient<CustomerViewModel>();
        builder.Services.AddTransient<ProductPage>();
        builder.Services.AddTransient<CustomerPage>();
        builder.Services.AddTransient<CartPage>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
