using Library_WebApp.Model;
using LibraryData;
using Microsoft.Extensions.Options;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public class StudentService : IStudentService
    {
        private readonly LibraryDataConfiguration _libraryconfig;
        private readonly IStudentCRUD _studentCRUD;
        private readonly Uri _registerUrl, _loginUrl;
        public StudentService(IOptions<LibraryDataConfiguration> libraryOptions, IStudentCRUD studentCRUD)
        {
            _libraryconfig = libraryOptions.Value;
            _studentCRUD = studentCRUD;
            if(_libraryconfig.baseUrl == null)
            {
                throw new ArgumentNullException((nameof(libraryOptions)));
            }
            var BaseUrl = new Uri(_libraryconfig.baseUrl, UriKind.Absolute);
            _registerUrl = new (BaseUrl, RouteConstants.Registerstudent) ;
            _loginUrl = new(BaseUrl, RouteConstants.Loginstudent);
        }

        public async Task<(HttpStatusCode, bool)> AddNewStudent<T>(T entity) where T : class
        {
            return await _studentCRUD.AddStudent(_registerUrl, entity);
        }

        public async Task<(HttpStatusCode, bool)> GetExistingStudent(string username, string password)
        {
            return await _studentCRUD.GetStudent(_loginUrl, username, password);
        }
    }
}
