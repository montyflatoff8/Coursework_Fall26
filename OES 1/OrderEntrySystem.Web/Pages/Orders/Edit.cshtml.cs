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


        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public SelectList OrderStatusOptions { get; set; } = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await orderClient.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            this.Order = order;

            var customer = await customerClient.GetCustomerAsync(CustomerId);
            CustomerName = customer?.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                var customer = await customerClient.GetCustomerAsync(CustomerId);
                CustomerName = customer?.Name;
                return Page();
            }

            var updated = await orderClient.UpdateOrderAsync(id, this.Order);

            if (updated == null)
            {
                return NotFound();
            }

            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }
    }
}
