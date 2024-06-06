using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public interface IStudentService
    {
        public Task<(HttpStatusCode, bool)> GetExistingStudent(string username, string password);
        public Task<(HttpStatusCode, bool)> AddNewStudent<T>(T entity) where T : class;
        public Task<(HttpStatusCode, bool)> Rentbook(string bookname, string username);
        public Task<IActionResult> ebookRents(string username);
        public Task<List<BookDetail>> searchBook(string? bookname, string? authorname, string? publishername, string? categoryname);
    }
}
