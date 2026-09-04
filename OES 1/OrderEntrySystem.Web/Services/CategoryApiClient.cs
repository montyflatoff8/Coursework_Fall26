using OrderEntrySystem.Core;

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
    }
}
