using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class CreatedModel : PageModel
    {
        private readonly CustomerApiClient client;
        public CreatedModel(CustomerApiClient client)
        {
            this.client = client;
        }
        public Customer Customer { get; set; } = new(); // Starts as an empty Customer object when page loads.

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await this.client.GetCustomerAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            this.Customer = customer;

            return Page();
        }
    }
}
