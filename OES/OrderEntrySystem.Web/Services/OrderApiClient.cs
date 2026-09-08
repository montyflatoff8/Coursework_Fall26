using Microsoft.AspNetCore.Http.HttpResults;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Services
{
    public class OrderApiClient
    {
        private readonly HttpClient http;

        public OrderApiClient(HttpClient httpClient)
        {
            this.http = httpClient;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await http.GetFromJsonAsync<IEnumerable<Order>>("https://localhost:7007/api/orders");
        }

        public async Task<Order?> GetOrderAsync(int id)
        {
            var response = await http.GetAsync($"https://localhost:7007/api/orders/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Order>();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            var orders = await GetOrdersAsync();
            return orders.Where(o => o.CustomerId == customerId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var response = await http.PostAsJsonAsync("https://localhost:7007/api/orders", order);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Order>();
            return created!;
        }

        public async Task<Order> UpdateOrderAsync(int id, Order order)
        {
            var response = await http.PutAsJsonAsync($"https://localhost:7007/api/orders/{id}", order);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Order>();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var response = await http.DeleteAsync($"https://localhost:7007/api/orders/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // nothing to delete — treat as a no-op rather than throwing
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}