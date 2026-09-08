using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Orders
{
    public class CreatedModel : PageModel
    {
        private readonly OrderApiClient client;
        public CreatedModel(OrderApiClient client)
        {
            this.client = client;
        }
        public Order Order { get; set; } = new(); // Starts as an empty Order object when page loads.

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await this.client.GetOrderAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            this.Order = order;

            return Page();
        }
    }
}
