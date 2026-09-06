using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Enumerations;
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

        private readonly LocationApiClient locationClient;

        public CreateModel(ProductApiClient productClient, CategoryApiClient categoryClient, LocationApiClient locationClient)
        {
            this.productClient = productClient;
            this.categoryClient = categoryClient;
            this.locationClient = locationClient;
        }

        [BindProperty]
        public Product Product { get; set; } = new(); // Starts as an empty Product object when page loads.

        public SelectList CategoryOptions { get; set; }

        public SelectList ConditionOptions { get; set; } = new SelectList(Enum.GetValues(typeof(Condition)).Cast<Condition>());

        public SelectList LocationOptions { get; set; }

        public async Task OnGetAsync()
        {
            var categories = await categoryClient.GetCategoriesAsync();
            CategoryOptions = new SelectList(categories, "Id", "Name");

            var locations = await locationClient.GetLocationsAsync();
            LocationOptions = new SelectList(locations, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // dropdown data doesn't survive postback on its own — rebuild it before redisplaying
                var categories = await categoryClient.GetCategoriesAsync();
                CategoryOptions = new SelectList(categories, "Id", "Name", Product.CategoryId);

                var locations = await locationClient.GetLocationsAsync();
                LocationOptions = new SelectList(locations, "Id", "Name", Product.LocationId);
                return Page();
            }

            var created = await productClient.CreateProductAsync(this.Product);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
