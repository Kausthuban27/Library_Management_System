using Library_WebApp.Model;
using LibraryData.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Library_WebApp.Services
{
    public class StudentCRUD : IStudentCRUD
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StudentCRUD> _logger;
        public StudentCRUD(IHttpClientFactory httpClientFactory, ILogger<StudentCRUD> logger)
        {
            _httpClient = httpClientFactory.CreateClient("baseUrl");
            _logger = logger;
        }
        public async Task<(HttpStatusCode, bool)> AddStudent<T>(Uri BaseUrl, T entity) where T : class
        {
            string json = JsonSerializer.Serialize(entity);

            Console.WriteLine(json);
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
            Console.WriteLine(response.Content);
            return (HttpStatusCode.BadRequest, false);
        }

        public async Task<(HttpStatusCode, bool)> GetStudent(Uri BaseUrl, string username, string password)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["username"] = username;
            query["password"] = password;
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            if(response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public Task<T> Rentbook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> searchBook<T>(Uri BaseUrl, SearchBooks search)
        {
            throw new NotImplementedException();
        }
    }
}
