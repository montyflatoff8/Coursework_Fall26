using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Categories
{
    public class CreatedModel : PageModel
    {
        private readonly CategoryApiClient client;

        public CreatedModel(CategoryApiClient client)
        {
            this.client = client;
        }

        public Category Category { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await this.client.GetCategoryAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            this.Category = category;
            return Page();
        }
    }
}
