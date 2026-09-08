using Microsoft.AspNetCore.Http.HttpResults;
using OrderEntrySystem.Core.Interfaces;
using OrderEntrySystem.Core.Models;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Services
{
    public class ProductApiClient
    {
        private readonly HttpClient http;

        public ProductApiClient(HttpClient httpClient)
        {
            this.http = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await http.GetFromJsonAsync<IEnumerable<Product>>("https://localhost:7007/api/products");
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var response = await http.GetAsync($"https://localhost:7007/api/products/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var response = await http.PostAsJsonAsync("https://localhost:7007/api/products", product);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Product>();
            return created!;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var response = await http.PutAsJsonAsync($"https://localhost:7007/api/products/{id}", product);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await http.DeleteAsync($"https://localhost:7007/api/products/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // nothing to delete — treat as a no-op rather than throwing
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}