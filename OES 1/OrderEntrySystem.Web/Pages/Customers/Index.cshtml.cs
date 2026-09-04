using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CustomerApiClient apiClient;

        public IndexModel(CustomerApiClient client)
        {
            this.apiClient = client;
        }

        public IEnumerable<Customer> Customers { get; private set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await this.apiClient.GetCustomersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.apiClient.DeleteCustomerAsync(id);
            return RedirectToPage("./Index");
        }
    }
}