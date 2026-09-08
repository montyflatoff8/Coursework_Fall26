using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Enumerations;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly OrderApiClient orderClient;

        private readonly CustomerApiClient customerClient;

        public CreateModel(OrderApiClient orderClient, CustomerApiClient customerClient)
        {
            this.orderClient = orderClient;
            this.customerClient = customerClient;
        }

        [BindProperty]
        public Order Order { get; set; } = new(); // Starts as an empty Order object when page loads.

        public SelectList OrderStatusOptions { get; set; } = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());

        public SelectList CustomerOptions { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public async Task OnGetAsync()
        {
            var customers = await customerClient.GetCustomersAsync();
            CustomerOptions = new SelectList(customers, "Id", "Name", CustomerId);

            if (CustomerId.HasValue)
            {
                Order.CustomerId = CustomerId.Value;
                var customer = await customerClient.GetCustomerAsync(CustomerId.Value);
                CustomerName = customer?.Name;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var customers = await customerClient.GetCustomersAsync();
                CustomerOptions = new SelectList(customers, "Id", "Name", Order.CustomerId);
                return Page();
            }

            var created = await orderClient.CreateOrderAsync(this.Order);

            if (CustomerId.HasValue)
            {
                return RedirectToPage("/OrderLines/Create", new { orderId = created.Id });
            }

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
