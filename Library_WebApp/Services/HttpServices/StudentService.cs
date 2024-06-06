using Library_WebApp.Model;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public class StudentService : IStudentService
    {
        private readonly LibraryDataConfiguration _libraryconfig;
        private readonly IStudentCRUD _studentCRUD;
        private readonly Uri _registerUrl, _loginUrl, _rentUrl, _searchUrl, _rentedBooksUrl;
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
            _rentUrl = new(BaseUrl, RouteConstants.RentBook);
            _searchUrl = new(BaseUrl, RouteConstants.SearchBook);
            _rentedBooksUrl = new(BaseUrl, RouteConstants.RentedBooks);
        }

        public async Task<(HttpStatusCode, bool)> AddNewStudent<T>(T entity) where T : class
        {
            return await _studentCRUD.AddStudent(_registerUrl, entity);
        }

        public async Task<(HttpStatusCode, bool)> GetExistingStudent(string username, string password)
        {
            return await _studentCRUD.GetStudent(_loginUrl, username, password);
        }

        public async Task<(HttpStatusCode, bool)> Rentbook(string bookname, string username)
        {
            return await _studentCRUD.Rentbook(_rentUrl, bookname, username);
        }

        public async Task<List<BookDetail>> searchBook(string? bookname, string? authorname, string? publishername, string? categoryname)
        {
            return await _studentCRUD.searchBook(_searchUrl, bookname, authorname, publishername, categoryname);
        }
        
        public async Task<IActionResult> ebookRents(string username)
        {
            return await _studentCRUD.ebookRents(_rentedBooksUrl, username);
        }
    }
}
