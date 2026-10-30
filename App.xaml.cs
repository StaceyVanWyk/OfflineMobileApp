using Microsoft.Extensions.DependencyInjection;
using OfflineMobileApp.Services;
namespace OfflineMobileApp;

public partial class App : Application
{
	private readonly DatabaseService _databaseService;
	public App(DatabaseService databaseService)
	{
		InitializeComponent();
		_databaseService = databaseService;
		
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}

	protected override async void OnStart()
    {
        base.OnStart();

        await _databaseService.InitializeAsync();
		await _databaseService.SeedCategoriesAsync();
		Console.WriteLine("ABOUT TO SEED PRODUCTS");

		await _databaseService.SeedProductsAsync();
		Console.WriteLine("SIX PRODUCTS INSERTED");
		
    }
}