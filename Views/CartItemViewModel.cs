using OfflineMobileApp.Models;

namespace OfflineMobileApp.ViewModels;

public class CartItemViewModel
{
    public int CartItemID { get; set; }

    public int ProductID { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ImageName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal => Price * Quantity;

    public CartItemViewModel(CartItem cartItem, Product product)
    {
        CartItemID = cartItem.CartItemID;
        ProductID = product.ProductID;
        ProductName = product.ProductName;
        ImageName = product.ImageName;
        Price = product.Price;
        Quantity = cartItem.Quantity;
    }
}