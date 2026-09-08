using Microsoft.AspNetCore.Http.HttpResults;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Services
{
    public class CustomerApiClient
    {
        private readonly HttpClient http;

        public CustomerApiClient(HttpClient httpClient)
        {
            this.http = httpClient;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await http.GetFromJsonAsync<IEnumerable<Customer>>("https://localhost:7007/api/customers");
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            var response = await http.GetAsync($"https://localhost:7007/api/customers/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var response = await http.PostAsJsonAsync("https://localhost:7007/api/customers", customer);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Customer>();
            return created!;
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            var response = await http.PutAsJsonAsync($"https://localhost:7007/api/customers/{id}", customer);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var response = await http.DeleteAsync($"https://localhost:7007/api/customers/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // nothing to delete — treat as a no-op rather than throwing
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}