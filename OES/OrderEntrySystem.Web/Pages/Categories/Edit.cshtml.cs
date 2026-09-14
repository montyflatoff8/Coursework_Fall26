using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly CategoryApiClient categoryClient;

        public EditModel(CategoryApiClient categoryClient)
        {
            this.categoryClient = categoryClient;
        }

        [BindProperty]
        public Category Category { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await categoryClient.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound(); // returns not found page, stops all logic here
            }

            this.Category = category;
            return Page(); // re-renders current page
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updated = await categoryClient.UpdateCategoryAsync(id, this.Category);

            if (updated == null)
            {
                return NotFound();
            }

            TempData["StatusMessage"] = "Changes saved";

            return RedirectToPage("./Index");
        }
    }
}
