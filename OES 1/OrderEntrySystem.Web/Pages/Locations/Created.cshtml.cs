using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Locations
{
    public class CreatedModel : PageModel
    {
        private readonly LocationApiClient client;

        public CreatedModel(LocationApiClient client)
        {
            this.client = client;
        }

        public Location Location { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var location = await this.client.GetLocationAsync(id);

            if (location is null)
            {
                return NotFound();
            }

            this.Location = location;
            return Page();
        }
    }
}
