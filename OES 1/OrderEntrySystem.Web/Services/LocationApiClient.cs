using Microsoft.AspNetCore.Http.HttpResults;
using OrderEntrySystem.Core;
using OrderEntrySystem.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace OrderEntrySystem.Web.Services
{
    public class LocationApiClient
    {
        private readonly HttpClient http;

        public LocationApiClient(HttpClient httpClient)
        {
            this.http = httpClient;
        }

        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            return await http.GetFromJsonAsync<IEnumerable<Location>>("https://localhost:7007/api/locations");
        }

        public async Task<Location?> GetLocationAsync(int id)
        {
            var response = await http.GetAsync($"https://localhost:7007/api/locations/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Location>();
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            var response = await http.PostAsJsonAsync("https://localhost:7007/api/locations", location);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Location>();
            return created!;
        }

        public async Task<Location> UpdateLocationAsync(int id, Location updatedLocation)
        {
            var response = await http.PutAsJsonAsync($"https://localhost:7007/api/locations/{id}", updatedLocation);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Location>();
        }

        public async Task DeleteLocationAsync(int id)
        {
            var response = await http.DeleteAsync($"https://localhost:7007/api/locations/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // nothing to delete — treat as a no-op rather than throwing
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}