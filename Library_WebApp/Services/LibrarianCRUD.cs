using Library_WebApp.Model;
using LibraryData.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Library_WebApp.Services
{
    public class LibrarianCRUD : ILibrarianCRUD
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LibrarianCRUD> _logger;
        public LibrarianCRUD(IHttpClientFactory httpClientFactory, ILogger<LibrarianCRUD> logger)
        {
            _httpClient = httpClientFactory.CreateClient("baseUrl");
            _logger = logger;
        }
        public async Task<(HttpStatusCode, bool)> AddBook<T>(Uri BaseUrl, T entity) where T : class
        {
            string json = JsonSerializer.Serialize(entity);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = BaseUrl,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public Task CheckAvailability(Uri BaseUrl, SearchBooks search)
        {
            throw new NotImplementedException();
        }

        public Task<T> IssueBook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> ReturnedBook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode, bool)> AddLibrarian<T>(Uri BaseUrl, T entity) where T : class
        {
            string json = JsonSerializer.Serialize(entity);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = BaseUrl,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public async Task<(HttpStatusCode, bool)> GetLibrarian(Uri BaseUrl, string username, string password)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Username"] = username;
            query["Password"] = password;
            Console.WriteLine(username);
            Console.WriteLine(password);
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }
    }
}
