using OfflineMobileApp.Models;
using OfflineMobileApp.ViewModels;

namespace OfflineMobileApp.Views;

public partial class ProductPage : ContentPage
{
    private readonly ProductViewModel _viewModel;
    private readonly int _customerId;

    public ProductPage(ProductViewModel viewModel, int customerId)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _customerId = customerId;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadProductsAsync();
    }

    private async void OnProductSelected(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Product selectedProduct)
            return;

        ((CollectionView)sender).SelectedItem = null;

        await Navigation.PushAsync(
            new ProductDetailsPage(selectedProduct, _customerId));
    }
}