using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly CustomerApiClient customerClient;
        private readonly OrderApiClient orderClient;
        private readonly OrderLineApiClient orderLineClient;

        public DetailsModel(CustomerApiClient customerClient, OrderApiClient orderClient, OrderLineApiClient orderLineClient)
        {
            this.customerClient = customerClient;
            this.orderClient = orderClient;
            this.orderLineClient = orderLineClient;
        }

        public Customer Customer { get; set; } = new();

        public OrdersListViewModel OrdersListViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await customerClient.GetCustomerAsync(id);
            if (customer == null) return NotFound();

            Customer = customer;

            var orders = await orderClient.GetOrdersByCustomerAsync(id);
            var orderIdsWithLines = await orderLineClient.GetOrderIdsWithLinesAsync();

            OrdersListViewModel = new OrdersListViewModel
            {
                Orders = orders,
                OrderIdsWithLines = orderIdsWithLines
            };

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteOrderAsync(int orderId, int customerId)
        {
            await orderClient.DeleteOrderAsync(orderId);
            return RedirectToPage(new { id = customerId });
        }
    }
}