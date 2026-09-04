using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly LocationApiClient client;
        public IndexModel(LocationApiClient client)
        {
            this.client = client;
        }
        public IEnumerable<Location> Locations { get; private set; } = [];

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            this.Locations = await this.client.GetLocationsAsync();
        }
    }
}
