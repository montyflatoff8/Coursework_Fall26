using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Products
{
    public class CreatedModel : PageModel
    {
        private readonly ProductApiClient client;
        public CreatedModel(ProductApiClient client)
        {
            this.client = client;
        }
        public Product Product { get; set; } = new(); // Starts as an empty Product object when page loads.

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await this.client.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            this.Product = product;

            return Page();
        }
    }
}
