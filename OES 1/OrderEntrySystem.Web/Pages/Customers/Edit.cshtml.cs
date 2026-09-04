using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly CustomerApiClient customerClient;

        public EditModel(CustomerApiClient customerClient)
        {
            this.customerClient = customerClient;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await customerClient.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound(); // returns not found page, stops all logic here
            }

            this.Customer = customer;
            return Page(); // re-renders current page
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updated = await customerClient.UpdateCustomerAsync(id, this.Customer);

            if (updated == null)
            {
                return NotFound();
            }

            TempData["StatusMessage"] = "Changes saved";

            return RedirectToPage("./Index");
        }
    }
}
