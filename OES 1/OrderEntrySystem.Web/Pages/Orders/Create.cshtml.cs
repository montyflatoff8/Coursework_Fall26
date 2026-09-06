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

        public async Task OnGetAsync()
        {
            var customers = await customerClient.GetCustomersAsync();
            CustomerOptions = new SelectList(customers, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // dropdown data doesn't survive postback on its own — rebuild it before redisplaying
                var customers = await customerClient.GetCustomersAsync();
                CustomerOptions = new SelectList(customers, "Id", "Name", Order.CustomerId);

                return Page();
            }

            var created = await orderClient.CreateOrderAsync(this.Order);

            return RedirectToPage("./Created", new { id = created.Id });
        }
    }
}
