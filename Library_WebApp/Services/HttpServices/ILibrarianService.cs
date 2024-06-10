using LibraryData.Models;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public interface ILibrarianService
    {
        public Task<(HttpStatusCode, bool)> GetExistingLibrarian(string username, string password);
        public Task<(HttpStatusCode, bool)> AddNewLibrarian<T>(T entity) where T : class;
        public Task<(HttpStatusCode, bool)> AddNewBook<T>(T entity) where T : class;
        public Task<(HttpStatusCode, bool)> IssueBook(string bookname, string username);
        public Task<List<BookIssue>> AllBooks();
    }
}
