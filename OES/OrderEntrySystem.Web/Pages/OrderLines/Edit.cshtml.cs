using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderEntrySystem.Core.Models;
using OrderEntrySystem.Web.Services;

namespace OrderEntrySystem.Web.Pages.OrderLines
{
    public class EditModel : PageModel
    {
        private readonly OrderLineApiClient orderLineClient;
        private readonly ProductApiClient productClient;
        private readonly OrderApiClient orderClient;

        public EditModel(OrderLineApiClient orderLineClient, ProductApiClient productClient, OrderApiClient orderClient)
        {
            this.orderLineClient = orderLineClient;
            this.productClient = productClient;
            this.orderClient = orderClient;
        }

        [BindProperty]
        public OrderLine OrderLine { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public string OrderSummary { get; set; } = string.Empty;

        public SelectList ProductOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var orderLine = await orderLineClient.GetOrderLineAsync(Id);
            if (orderLine == null) return NotFound();

            OrderLine = orderLine;
            OrderId = orderLine.OrderId;

            var order = await orderClient.GetOrderAsync(OrderId);
            OrderSummary = order == null ? string.Empty : $"Order #{order.Id} — {order.Customer?.Name}";

            var products = await productClient.GetProductAsync();
            ProductOptions = new SelectList(products, "Id", "Name", OrderLine.ProductId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            OrderLine.Id = Id;
            OrderLine.OrderId = OrderId;
            OrderLine.Order = null;
            OrderLine.Product = null;

            if (!ModelState.IsValid)
            {
                var products = await productClient.GetProductAsync();
                ProductOptions = new SelectList(products, "Id", "Name", OrderLine.ProductId);

                var order = await orderClient.GetOrderAsync(OrderId);
                OrderSummary = order == null ? string.Empty : $"Order #{order.Id} — {order.Customer?.Name}";

                return Page();
            }

            await orderLineClient.UpdateOrderLineAsync(Id, OrderLine);

            return RedirectToPage("/Orders/Edit", new { id = OrderId });
        }
    }
}