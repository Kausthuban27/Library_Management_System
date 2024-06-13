using Library_WebApp.Model;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public interface ILibrarianService
    {
        public Task<(HttpStatusCode, bool)> GetExistingLibrarian(string username, string password);
        public Task<(HttpStatusCode, bool)> AddNewLibrarian<T>(T entity) where T : class;
        public Task<AddNewLibrarian> RetrieveLibrarian(string username);
        public Task<IActionResult> UpdateExistingLibrarian<T>(T entity) where T : class;
        public Task<List<StudentRentedBook>> studentRentedBooks(string bookname);
        public Task<(HttpStatusCode, bool)> AddNewBook<T>(T entity) where T : class;
        public Task<(HttpStatusCode, bool)> IssueBook(string bookname, string username);
        public Task<List<BookIssue>> AllBooks();
    }
}
