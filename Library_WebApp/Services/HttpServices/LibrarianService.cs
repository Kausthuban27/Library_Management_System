using Library_WebApp.Model;
using LibraryData;
using LibraryData.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public class LibrarianService : ILibrarianService
    {
        private readonly LibraryDataConfiguration _libraryconfig;
        private readonly ILibrarianCRUD _librarianCRUD;
        private readonly Uri _registerUrl, _loginUrl, _newBookUrl, _issueBookUrl,_AllBooks;
        public LibrarianService(IOptions<LibraryDataConfiguration> libraryOptions, ILibrarianCRUD librarianCRUD)
        {
            _libraryconfig = libraryOptions.Value;
            _librarianCRUD = librarianCRUD;
            if (_libraryconfig.baseUrl == null)
            {
                throw new ArgumentNullException((nameof(libraryOptions)));
            }
            var BaseUrl = new Uri(_libraryconfig.baseUrl, UriKind.Absolute);
            _registerUrl = new(BaseUrl, RouteConstants.RegisterLibrarian);
            _loginUrl = new(BaseUrl, RouteConstants.LoginLibrarian);
            _newBookUrl = new(BaseUrl, RouteConstants.AddBook);
            _issueBookUrl = new(BaseUrl, RouteConstants.IssueBook);
            _AllBooks = new(BaseUrl, RouteConstants.AllIssuedBooks);
        }

        public async Task<(HttpStatusCode, bool)> AddNewBook<T>(T entity) where T : class
        {
            return await _librarianCRUD.AddBook(_newBookUrl, entity);
        }

        public async Task<(HttpStatusCode, bool)> AddNewLibrarian<T>(T entity) where T : class
        {
            return await _librarianCRUD.AddLibrarian(_registerUrl, entity);
        }

        public async Task<List<BookIssue>> AllBooks()
        {
            return await _librarianCRUD.AllbooksIssued(_AllBooks);
        }

        public async Task<(HttpStatusCode, bool)> GetExistingLibrarian(string username, string password)
        {
            return await _librarianCRUD.GetLibrarian(_loginUrl, username, password);
        }

        public async Task<(HttpStatusCode, bool)> IssueBook(string bookname, string username)
        {
            return await _librarianCRUD.IssueBook(_issueBookUrl, bookname, username);
        }
    }
}
