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
        private readonly LocationApiClient locationClient;

        public EditModel(ProductApiClient productClient, CategoryApiClient categoryClient, LocationApiClient locationClient)
        {
            this.productClient = productClient;
            this.categoryClient = categoryClient;
            this.locationClient = locationClient;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public IEnumerable<Category> AvailableCategories { get; set; } = new List<Category>();
        public SelectList ConditionOptions { get; set; } = new SelectList(Enum.GetValues(typeof(Condition)).Cast<Condition>());
        public SelectList LocationOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await productClient.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Pre-select this Product's current, active Categories
            product.CategoryIds = product.ProductCategories
                .Where(pc => !pc.IsArchived)
                .Select(pc => pc.CategoryId)
                .ToList();

            AvailableCategories = await categoryClient.GetCategoriesAsync();

            var locations = await locationClient.GetLocationsAsync();
            LocationOptions = new SelectList(locations, "Id", "Name", product.LocationId);

            this.Product = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                AvailableCategories = await categoryClient.GetCategoriesAsync();

                var locations = await locationClient.GetLocationsAsync();
                LocationOptions = new SelectList(locations, "Id", "Name", Product.LocationId);
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