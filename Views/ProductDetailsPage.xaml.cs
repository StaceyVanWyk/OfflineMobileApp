using OfflineMobileApp.Models;

namespace OfflineMobileApp.Views;

public partial class ProductDetailsPage : ContentPage
{
    public ProductDetailsPage(Product product)
    {
        InitializeComponent();
        BindingContext = product;
    }
}