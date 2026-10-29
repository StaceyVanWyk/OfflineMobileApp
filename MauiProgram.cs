using Microsoft.Extensions.Logging;
using OfflineMobileApp.Services;
using Microsoft.Extensions.DependencyInjection;
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
