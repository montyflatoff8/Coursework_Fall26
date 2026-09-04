using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Locations
{
    public class CreateModel : PageModel
    {
        private readonly LocationApiClient client;

        public CreateModel(LocationApiClient client)
        {
            this.client = client;
        }

        [BindProperty]
        public Location Location { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var created = await this.client.CreateLocationAsync(this.Location);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
