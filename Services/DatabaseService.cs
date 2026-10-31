using OfflineMobileApp.Models;
using SQLite;
using OfflineMobileApp.ViewModels;
namespace OfflineMobileApp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    private bool _isInitialized ;

    public DatabaseService(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
        
        
    }

    public async Task InitializeAsync()

    {
        if (_isInitialized)
            return;
        await _database.CreateTableAsync<Customer>();
        await _database.CreateTableAsync<Category>();
        await _database.CreateTableAsync<Product>();
        await _database.CreateTableAsync<CartItem>();
        await _database.CreateTableAsync<Order>();
        await _database.CreateTableAsync<OrderItem>();
        
        _isInitialized = true;


    }
    
    
public async Task<List<Customer>> GetCustomersAsync()
{
    await InitializeAsync();

    return await _database.Table<Customer>().ToListAsync();
}

public async Task SeedCustomersAsync()
{
    await InitializeAsync();

    var customers = await _database.Table<Customer>().ToListAsync();

    if (customers.Count > 0)
        return;

    var sampleCustomers = new List<Customer>
    {
        new Customer { FullName = "Stacey Van Wyk" },
        new Customer { FullName = "Alex Johnson" },
        new Customer { FullName = "Sarah Williams" },
        new Customer { FullName = "Michael Brown" },
        new Customer { FullName = "Emily Davis" }
    };

    await _database.InsertAllAsync(sampleCustomers);
}

public async Task<List<Product>> GetProductsAsync()
{
    await InitializeAsync();

    return await _database.Table<Product>().ToListAsync();
}

public async Task<Product?> GetProductByIdAsync(int productId)
{
    await InitializeAsync();

    return await _database.Table<Product>()
        .Where(product => product.ProductID == productId)
        .FirstOrDefaultAsync();
}

public async Task<int> SaveProductAsync(Product product)
{
    await InitializeAsync();

    if (product.ProductID != 0)
    {
        return await _database.UpdateAsync(product);
    }

    return await _database.InsertAsync(product);
}


public async Task<List<Category>> GetCategoriesAsync()
{
    await InitializeAsync();

    return await _database.Table<Category>().ToListAsync();
}

public async Task<int> SaveCategoryAsync(Category category)
{
    await InitializeAsync();

    if (category.CategoryID != 0)
    {
        return await _database.UpdateAsync(category);
    }

    return await _database.InsertAsync(category);
}


public async Task SeedCategoriesAsync()
{
    await InitializeAsync();

    var categories = await _database.Table<Category>().ToListAsync();

    if (categories.Count > 0)
        return;

    var sampleCategories = new List<Category>
    {
        new Category
        {
            CategoryName = "Clothing",
            Description = "Clothing and fashion items"
        },
        new Category
        {
            CategoryName = "Electronics",
            Description = "Electronic devices and accessories"
        },
        new Category
        {
            CategoryName = "Home & Living",
            Description = "Products for your home"
        }
    };

    await _database.InsertAllAsync(sampleCategories);
}

public async Task SeedProductsAsync()
{
    await InitializeAsync();

    // Get the categories already stored in the database
    var categories = await GetCategoriesAsync();

    // Find each category by its name
    var clothing = categories.FirstOrDefault(c => c.CategoryName == "Clothing");
    var electronics = categories.FirstOrDefault(c => c.CategoryName == "Electronics");
    var homeLiving = categories.FirstOrDefault(c => c.CategoryName == "Home & Living");

    // Stop if the categories do not exist
    if (clothing == null || electronics == null || homeLiving == null)
        return;

    // Check whether products have already been added
    var existingProducts = await _database.Table<Product>().ToListAsync();

    if (existingProducts.Count > 0)
        return;

    // Create sample products
    var products = new List<Product>
    {
        new Product
        {
            CategoryID = clothing.CategoryID,
            ProductName = "Cotton T-Shirt",
            Description = "Comfortable everyday cotton T-shirt",
            Price = 149.99m,
            StockQuantity = 20
        },
        new Product
        {
            CategoryID = clothing.CategoryID,
            ProductName = "Denim Jeans",
            Description = "Classic blue denim jeans",
            Price = 399.99m,
            StockQuantity = 15
        },
        new Product
        {
            CategoryID = electronics.CategoryID,
            ProductName = "Wireless Headphones",
            Description = "Wireless headphones with clear sound",
            Price = 599.99m,
            StockQuantity = 10
        },
        new Product
        {
            CategoryID = electronics.CategoryID,
            ProductName = "USB-C Charger",
            Description = "Fast charging USB-C adapter",
            Price = 199.99m,
            StockQuantity = 25
        },
        new Product
        {
            CategoryID = homeLiving.CategoryID,
            ProductName = "Coffee Mug",
            Description = "Ceramic mug for everyday use",
            Price = 79.99m,
            StockQuantity = 30
        },
        new Product
        {
            CategoryID = homeLiving.CategoryID,
            ProductName = "Throw Pillow",
            Description = "Soft decorative cushion",
            Price = 129.99m,
            StockQuantity = 12
        }
    };

    await _database.InsertAllAsync(products);
}

public async Task<List<CartItem>> GetCartItemsAsync(int customerId)
{
    await InitializeAsync();

    return await _database.Table<CartItem>()
        .Where(item => item.CustomerID == customerId)
        .ToListAsync();
}

public async Task AddToCartAsync(int customerId, int productId, int quantity)
{
    await InitializeAsync();

    if (quantity <= 0)
        throw new ArgumentException("Quantity must be greater than zero.");

    var product = await GetProductByIdAsync(productId);

    if (product == null)
        throw new Exception("Product not found.");

    var existingCartItems = await GetCartItemsAsync(customerId);

    var existingItem = existingCartItems
        .FirstOrDefault(item => item.ProductID == productId);

    int currentQuantity = existingItem?.Quantity ?? 0;

    if (currentQuantity + quantity > product.StockQuantity)
        throw new Exception("You cannot add more than the available stock.");

    if (existingItem != null)
    {
        existingItem.Quantity += quantity;
        await _database.UpdateAsync(existingItem);
    }
    else
    {
        var cartItem = new CartItem
        {
            CustomerID = customerId,
            ProductID = productId,
            Quantity = quantity
        };

        await _database.InsertAsync(cartItem);
    }
}

public async Task RemoveFromCartAsync(int cartItemId)
{
    await InitializeAsync();

    var cartItem = await _database.Table<CartItem>()
        .Where(item => item.CartItemID == cartItemId)
        .FirstOrDefaultAsync();

    if (cartItem != null)
        await _database.DeleteAsync(cartItem);
}

public async Task<List<CartItemViewModel>> GetCartDetailsAsync(int customerId)
{
    await InitializeAsync();

    var cartItems = await GetCartItemsAsync(customerId);

    var cartDetails = new List<CartItemViewModel>();

    foreach (var cartItem in cartItems)
    {
        var product = await GetProductByIdAsync(cartItem.ProductID);

        if (product != null)
        {
            cartDetails.Add(new CartItemViewModel(cartItem, product));
        }
    }

    return cartDetails;
}
}