using System.Net.Http.Json;
using VivesBlog.Model;

namespace VivesBlog.Sdk
{
    public class PersonSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Person>> FindAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/people";
            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var people = await response.Content.ReadFromJsonAsync<IList<Person>>();
            if (people is null)
            {
                return new List<Person>();
            }

            return people;
        }

        public async Task<Person?> GetAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";
            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> CreateAsync(Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/people";
            var response = await httpClient.PostAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> UpdateAsync(int id, Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";
            var response = await httpClient.PutAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";
            var response = await httpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}