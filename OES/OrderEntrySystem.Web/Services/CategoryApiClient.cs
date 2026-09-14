using OrderEntrySystem.Core.Models;

namespace OrderEntrySystem.Web.Services
{
    public class CategoryApiClient
    {
        private readonly HttpClient http;

        public CategoryApiClient(HttpClient httpClient)
        {
            this.http = httpClient;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await http.GetFromJsonAsync<IEnumerable<Category>>("https://localhost:7007/api/categories");
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            var response = await http.GetAsync($"https://localhost:7007/api/categories/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var response = await http.PostAsJsonAsync("https://localhost:7007/api/categories", category);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Category>();
            return created!;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category updatedCategory)
        {
            var response = await http.PutAsJsonAsync($"https://localhost:7007/api/categories/{id}", updatedCategory);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await http.DeleteAsync($"https://localhost:7007/api/categories/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // nothing to delete — treat as a no-op rather than throwing
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
