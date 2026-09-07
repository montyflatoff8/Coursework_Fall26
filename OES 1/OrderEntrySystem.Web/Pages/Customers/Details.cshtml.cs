using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerApiClient customerClient;
        private readonly OrderApiClient orderClient;

        public DetailsModel(CustomerApiClient customerClient, OrderApiClient orderClient)
        {
            this.customerClient = customerClient;
            this.orderClient = orderClient;
        }

        public Customer Customer { get; set; } = new();
        public IEnumerable<Order> Orders { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await customerClient.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            Customer = customer;
            Orders = await orderClient.GetOrdersByCustomerAsync(id);

            return Page();
        }
    }
}