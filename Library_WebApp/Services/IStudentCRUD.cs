using Library_WebApp.Model;
using LibraryData.Models;
using System.Net;

namespace Library_WebApp.Services
{
    public interface IStudentCRUD
    {
        public Task<(HttpStatusCode, bool)> GetStudent(Uri BaseUrl, string username, string password);
        public Task<(HttpStatusCode, bool)> AddStudent<T>(Uri BaseUrl, T entity) where T : class;
        public Task<(HttpStatusCode, bool)> Rentbook(Uri BaseUrl, string bookname);
        public Task<List<BookDetail>> searchBook(Uri BaseUrl, string bookname, string authorname, string publishername, string categoryname);
    }
}
