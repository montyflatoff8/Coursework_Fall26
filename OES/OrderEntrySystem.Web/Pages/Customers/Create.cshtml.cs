using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CustomerApiClient customerClient;

        public CreateModel(CustomerApiClient customerClient)
        {
            this.customerClient = customerClient;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new(); // Starts as an empty Customer object when page loads.

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var created = await customerClient.CreateCustomerAsync(this.Customer);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
