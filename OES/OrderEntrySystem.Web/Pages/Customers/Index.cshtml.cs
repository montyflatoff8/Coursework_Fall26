using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CustomerApiClient customerClient;

        private readonly OrderApiClient orderClient;

        public IndexModel(CustomerApiClient customerClient, OrderApiClient orderClient)
        {
            this.customerClient = customerClient;
            this.orderClient = orderClient;
        }

        public IEnumerable<Customer> Customers { get; private set; } = [];

        public HashSet<int> CustomerIdsWithOrders { get; set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await customerClient.GetCustomersAsync();

            var allOrders = await orderClient.GetOrdersAsync();
            CustomerIdsWithOrders = allOrders.Select(o => o.CustomerId).ToHashSet();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customerOrders = await orderClient.GetOrdersByCustomerAsync(id);

            if (customerOrders.Any())
            {
                StatusMessage = "This customer cannot be deleted because they have existing orders.";
                return RedirectToPage("./Index");
            }

            await customerClient.DeleteCustomerAsync(id);
            StatusMessage = "Customer deleted.";
            return RedirectToPage("./Index");
        }
    }
}