using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.OrderLines
{
    public class IndexModel : PageModel
    {
        private readonly OrderLineApiClient orderLineClient;
        private readonly OrderApiClient orderClient;

        public IndexModel(OrderLineApiClient orderLineClient, OrderApiClient orderClient)
        {
            this.orderLineClient = orderLineClient;
            this.orderClient = orderClient;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public string OrderSummary { get; set; } = string.Empty;

        public IEnumerable<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public async Task<IActionResult> OnGetAsync()
        {
            var order = await orderClient.GetOrderAsync(OrderId);
            if (order == null) return NotFound();

            OrderSummary = $"Order #{order.Id} — {order.Customer?.Name}";
            OrderLines = await orderLineClient.GetOrderLinesByOrderAsync(OrderId);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await orderLineClient.DeleteOrderLineAsync(id);
            return RedirectToPage(new { orderId = OrderId });
        }
    }
}