using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly OrderApiClient apiClient;

        public IndexModel(OrderApiClient client)
        {
            this.apiClient = client;
        }

        public IEnumerable<Order> Orders { get; private set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await this.apiClient.GetOrdersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.apiClient.DeleteOrderAsync(id);
            return RedirectToPage("./Index");
        }
    }
}