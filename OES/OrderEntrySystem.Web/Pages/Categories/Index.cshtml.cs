using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryApiClient apiClient;

        public IndexModel(CategoryApiClient client)
        {
            this.apiClient = client;
        }

        public IEnumerable<Category> Categories { get; private set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await this.apiClient.GetCategoriesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.apiClient.DeleteCategoryAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
