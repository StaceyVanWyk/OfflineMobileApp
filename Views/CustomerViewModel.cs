
using System.Collections.ObjectModel;
using OfflineMobileApp.Models;
using OfflineMobileApp.Services;

namespace OfflineMobileApp.ViewModels;

public class CustomerViewModel
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Customer> Customers { get; set; } = new();

    public Customer? SelectedCustomer { get; set; }

    public CustomerViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task LoadCustomersAsync()
    {
        var customers = await _databaseService.GetCustomersAsync();

        Customers.Clear();

        foreach (var customer in customers)
        {
            Customers.Add(customer);
        }
    }
}
