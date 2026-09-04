using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductApiClient apiClient;

        public IndexModel(ProductApiClient client)
        {
            this.apiClient = client;
        }

        public IEnumerable<Product> Products { get; private set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Products = await this.apiClient.GetProductAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.apiClient.DeleteProductAsync(id);
            return RedirectToPage("./Index");
        }
    }
}