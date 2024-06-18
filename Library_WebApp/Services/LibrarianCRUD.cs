using Azure;
using Library_WebApp.Model;
using LibraryData.Models;
using LibraryData.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Diagnostics.Eventing.Reader;
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

        public async Task<(HttpStatusCode, bool)> IssueBook(Uri BaseUrl, string bookname, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Bookname"] = bookname;
            query["Username"] = username;
            uriBuilder.Query = query.ToString();
            var content = new StringContent(string.Empty);
            HttpResponseMessage response = await _httpClient.PostAsync(uriBuilder.Uri, content);
            if(response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
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

            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public async Task<List<BookIssue>> AllbooksIssued(Uri BaseUrl)
        {
            UriBuilder uri = new UriBuilder(BaseUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(uri.ToString());
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var bookDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookIssue>>(jsonResponse);
                return bookDetails!;
            }
            return new List<BookIssue> { };
        }

        public async Task<AddNewLibrarian> RetrieveExistingLibrarian(Uri BaseUrl, string username)
        {
            UriBuilder uri = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["username"] = username;

            uri.Query = query.ToString();
            HttpResponseMessage res = await _httpClient.GetAsync(uri.Uri); 
            if(res.IsSuccessStatusCode)
            {
                string jsonResponse = await res.Content.ReadAsStringAsync();
                var librarian = Newtonsoft.Json.JsonConvert.DeserializeObject<Librarian>(jsonResponse);
                if (librarian != null)
                {
                    var librarianData = LibrarianMapper.MapRetrievedLibrarian<AddNewLibrarian>(librarian!);
                    return librarianData;
                }
                else
                {
                    throw new Exception("Response is null");
                }
            }
            return new AddNewLibrarian { };
        }

        public async Task<IActionResult> UpdateLibrarian<T>(Uri BaseUrl, T entity) where T : class
        {
            string json = JsonSerializer.Serialize(entity);

            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = BaseUrl,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage res = await _httpClient.SendAsync(req);
            if (res.IsSuccessStatusCode)
            {
                return new OkObjectResult("Successfully Updated");
            }
            return new BadRequestObjectResult("Bad Request");
        }

        public async Task<List<StudentRentedBook>> GetStudentRentedBook(Uri BaseUrl, string authorname)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Authorname"] = authorname;

            uriBuilder.Query = query.ToString();
            HttpResponseMessage res = await _httpClient.GetAsync(uriBuilder.Uri);
            if (res.IsSuccessStatusCode)
            {
                string jsonResponse = await res.Content.ReadAsStringAsync();
                var bookData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentRentedBook>>(jsonResponse);
                return bookData!;
            }
            return new List<StudentRentedBook> { };
        }
    }
}
