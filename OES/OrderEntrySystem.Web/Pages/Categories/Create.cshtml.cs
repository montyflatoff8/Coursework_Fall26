using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly CategoryApiClient client;

        public CreateModel(CategoryApiClient client)
        {
            this.client = client;
        }

        [BindProperty]
        public Category Category { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var created = await this.client.CreateCategoryAsync(this.Category);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
