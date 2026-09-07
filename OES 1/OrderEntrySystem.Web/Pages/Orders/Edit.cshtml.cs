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

        private readonly OrderLineApiClient orderLineClient;

        public EditModel(OrderApiClient orderClient, CustomerApiClient customerClient, OrderLineApiClient orderLineClient)
        {
            this.orderClient = orderClient;
            this.customerClient = customerClient;
            this.orderLineClient = orderLineClient;
        }

        [BindProperty]
        public Order Order { get; set; } = new();


        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public IEnumerable<OrderLine> OrderLines { get; set; }

        public SelectList OrderStatusOptions { get; set; } = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await orderClient.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            this.Order = order;



            OrderLines = await orderLineClient.GetOrderLinesByOrderAsync(Order.Id);
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

            return RedirectToPage("/Customers/Details", new { id = Order.CustomerId });
        }

        public async Task<IActionResult> OnPostDeleteLineAsync(int id, int lineId)
        {
            await orderLineClient.DeleteOrderLineAsync(lineId);
            return RedirectToPage(new { id });
        }
    }
}
