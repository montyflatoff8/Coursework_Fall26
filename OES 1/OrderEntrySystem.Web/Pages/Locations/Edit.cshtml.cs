using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Locations
{
    public class EditModel : PageModel
    {
        private readonly LocationApiClient locationClient;

        public EditModel(LocationApiClient locationClient)
        {
            this.locationClient = locationClient;
        }

        [BindProperty]
        public Location Location { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var location = await locationClient.GetLocationAsync(id);

            if (location == null)
            {
                return NotFound(); // returns not found page, stops all logic here
            }

            this.Location = location;
            return Page(); // re-renders current page
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updated = await locationClient.UpdateLocationAsync(id, this.Location);

            if (updated == null)
            {
                return NotFound();
            }

            TempData["StatusMessage"] = "Changes saved";

            return RedirectToPage("./Index");
        }
    }
}
