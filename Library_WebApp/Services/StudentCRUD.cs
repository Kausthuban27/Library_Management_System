using Library_WebApp.Model;
using System.Net;
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
        public Task<(HttpStatusCode, bool)> AddStudent<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode, bool)> GetStudent(Uri BaseUrl, string username)
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[username] = username;
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

        public Task<T> searchBook<T>(Uri BaseUrl, SearchBook search)
        {
            throw new NotImplementedException();
        }
    }
}
