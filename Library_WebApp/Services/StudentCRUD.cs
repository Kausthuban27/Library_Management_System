using Library_WebApp.Model;
using LibraryData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<EbookRent>> ebookRents(Uri BaseUrl, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["username"] = username;
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            Console.WriteLine(response);
            Console.WriteLine(response.Content);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var bookDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EbookRent>>(jsonResponse);
                return bookDetails!;
            }
            return new List<EbookRent> { };
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

        public async Task<(HttpStatusCode, bool)> Rentbook(Uri BaseUrl, string bookname, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Bookname"] = bookname;
            query["Username"] = username;
            uriBuilder.Query = query.ToString();
            var content = new StringContent(string.Empty);
            HttpResponseMessage response = await _httpClient.PostAsync(uriBuilder.Uri, content);
            if (response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public async Task<(HttpStatusCode, BookIssue)> ReturnBorrowedBook(Uri BaseUrl, string bookname, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Bookname"] = bookname;
            query["Username"] = username; 
            uriBuilder.Query = query.ToString();
            var content = new StringContent(string.Empty);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uriBuilder.Uri, content);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var bookDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<BookIssue>(jsonResponse);
                return (HttpStatusCode.OK, bookDetails!);
            }
            return (HttpStatusCode.BadRequest, new BookIssue { });
        }

        public async Task<List<BookDetail>> searchBook(Uri BaseUrl, string? bookname, string? authorname, string? publishername, string? categoryname)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Bookname"] = bookname;
            query["Authorname"] = authorname;
            query["Publishername"] = publishername;
            query["Categoryname"] = categoryname;
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var bookDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookDetail>>(jsonResponse);
                return bookDetails!;
            }
            return new List<BookDetail> { };
        }

        public async Task<List<BookIssue>> userIssuedBooks(Uri BaseUrl, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query["username"] = username;
            uriBuilder.Query = query.ToString();

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var bookDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookIssue>>(jsonResponse);
                return bookDetails!;
            }
            return new List<BookIssue> { };
        }
    }
}
