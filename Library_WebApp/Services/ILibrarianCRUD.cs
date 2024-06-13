using Library_WebApp.Model;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library_WebApp.Services
{
    public interface ILibrarianCRUD
    {
        public Task<(HttpStatusCode, bool)> GetLibrarian(Uri BaseUrl, string username, string password);
        public Task<(HttpStatusCode, bool)> AddLibrarian<T>(Uri BaseUrl, T entity) where T : class;
        public Task<AddNewLibrarian> RetrieveExistingLibrarian(Uri BaseUrl, string username);
        public Task<IActionResult> UpdateLibrarian<T>(Uri BaseUrl, T entity) where T : class;
        public Task<(HttpStatusCode, bool)> IssueBook(Uri BaseUrl, string bookname, string username);
        public Task<List<StudentRentedBook>> GetStudentRentedBook(Uri BaseUrl, string bookname);
        public Task<T> ReturnedBook<T>(Uri BaseUrl, T entity) where T: class;
        public Task<(HttpStatusCode, bool)> AddBook<T>(Uri BaseUrl, T entity) where T : class;
        public Task<List<BookIssue>> AllbooksIssued(Uri BaseUrl);
    }
}
