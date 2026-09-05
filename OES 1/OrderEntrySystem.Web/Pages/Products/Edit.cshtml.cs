using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Enumerations;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductApiClient productClient;

        private readonly CategoryApiClient categoryClient;

        public EditModel(ProductApiClient productClient, CategoryApiClient categoryClient)
        {
            this.productClient = productClient;
            this.categoryClient = categoryClient;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public SelectList CategoryOptions { get; set; }
        public SelectList ConditionOptions { get; set; } = new SelectList(Enum.GetValues(typeof(Condition)).Cast<Condition>());


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await productClient.GetProductAsync(id);

            if (product == null)
            {
                return NotFound(); // returns not found page, stops all logic here
            }

            var categories = await categoryClient.GetCategoriesAsync();
            CategoryOptions = new SelectList(categories, "Id", "Name", Product.CategoryId);

            this.Product = product;
            return Page(); // re-renders current page
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                // repopulate the dropdown before redisplaying — it won't survive postback on its own
                var categories = await categoryClient.GetCategoriesAsync();
                CategoryOptions = new SelectList(categories, "Id", "Name", Product.CategoryId);
                return Page();
            }

            var updated = await productClient.UpdateProductAsync(id, this.Product);

            if (updated == null)
            {
                return NotFound();
            }

            TempData["StatusMessage"] = "Changes saved";

            return RedirectToPage("./Index");
        }
    }
}
