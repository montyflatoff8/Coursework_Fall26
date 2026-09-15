using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductApiClient productClient;

        public DetailsModel(ProductApiClient productClient)
        {
            this.productClient = productClient;
        }

        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await productClient.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            this.Product = product;
            return Page();
        }
    }
}