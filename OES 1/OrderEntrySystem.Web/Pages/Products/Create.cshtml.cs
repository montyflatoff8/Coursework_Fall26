using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductApiClient productClient;

        private readonly CategoryApiClient categoryClient;

        public CreateModel(ProductApiClient productClient, CategoryApiClient categoryClient)
        {
            this.productClient = productClient;
            this.categoryClient = categoryClient;
        }

        [BindProperty]
        public Product Product { get; set; } = new(); // Starts as an empty Product object when page loads.

        public SelectList CategoryOptions { get; set; }

        public async Task OnGetAsync()
        {
            var categories = await categoryClient.GetCategoriesAsync();
            CategoryOptions = new SelectList(categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // dropdown data doesn't survive postback on its own — rebuild it before redisplaying
                var categories = await categoryClient.GetCategoriesAsync();
                CategoryOptions = new SelectList(categories, "Id", "Name", Product.CategoryId);
                return Page();
            }

            var created = await productClient.CreateProductAsync(this.Product);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
