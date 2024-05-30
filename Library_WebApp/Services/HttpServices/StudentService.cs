using Library_WebApp.Model;
using LibraryData;
using Microsoft.Extensions.Options;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public class StudentService
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
                throw new ArgumentNullException();
            }
            var BaseUrl = new Uri(_libraryconfig.baseUrl, UriKind.Absolute);
            _registerUrl = new (BaseUrl, RouteConstants.Registerstudent) ;
            _loginUrl = new(BaseUrl, RouteConstants.Loginstudent);
        }

        public async Task<(HttpStatusCode, bool)> GetExistingStudent(string username)
        {
            return await _studentCRUD.GetStudent(_loginUrl, username);
        }
    }
}
