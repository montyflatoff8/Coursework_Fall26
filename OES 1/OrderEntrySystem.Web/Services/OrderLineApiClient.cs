using OrderEntrySystem.Core.Models;
using System.Net.Http.Json;

namespace OrderEntrySystem.Web.Services
{
    public class OrderLineApiClient
    {
        private readonly HttpClient httpClient;

        public OrderLineApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLinesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<IEnumerable<OrderLine>>("api/OrderLines");
            return result ?? new List<OrderLine>();
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLinesByOrderAsync(int orderId)
        {
            var lines = await GetOrderLinesAsync();
            return lines.Where(ol => ol.OrderId == orderId);
        }

        public async Task<OrderLine?> GetOrderLineAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/OrderLines/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OrderLine>();
        }

        public async Task<HashSet<int>> GetOrderIdsWithLinesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<IEnumerable<int>>("api/OrderLines/order-ids");
            return result?.ToHashSet() ?? new HashSet<int>();
        }

        public async Task<OrderLine?> CreateOrderLineAsync(OrderLine orderLine)
        {
            var response = await httpClient.PostAsJsonAsync("api/OrderLines", orderLine);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OrderLine>();
        }

        public async Task<OrderLine?> UpdateOrderLineAsync(int id, OrderLine orderLine)
        {
            var response = await httpClient.PutAsJsonAsync($"api/OrderLines/{id}", orderLine);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<OrderLine>();
        }

        public async Task DeleteOrderLineAsync(int id)
        {
            await httpClient.DeleteAsync($"api/OrderLines/{id}");
        }
    }
}