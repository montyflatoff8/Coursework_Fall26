using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Enumerations;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly OrderApiClient orderClient;

        private readonly CustomerApiClient customerClient;

        public EditModel(OrderApiClient orderClient, CustomerApiClient customerClient)
        {
            this.orderClient = orderClient;
            this.customerClient = customerClient;
        }

        [BindProperty]
        public Order Order { get; set; } = new();

        public SelectList CustomerOptions { get; set; }
        public SelectList OrderStatusOptions { get; set; } = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await orderClient.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound(); // returns not found page, stops all logic here
            }

            var customers = await customerClient.GetCustomersAsync();
            CustomerOptions = new SelectList(customers, "Id", "Name", Order.CustomerId);

            this.Order = order;
            return Page(); // re-renders current page
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                // repopulate the dropdown before redisplaying — it won't survive postback on its own
                var customers = await customerClient.GetCustomersAsync();
                CustomerOptions = new SelectList(customers, "Id", "Name", Order.CustomerId);
                return Page();
            }

            var updated = await orderClient.UpdateOrderAsync(id, this.Order);

            if (updated == null)
            {
                return NotFound();
            }

            TempData["StatusMessage"] = "Changes saved";

            return RedirectToPage("./Index");
        }
    }
}
