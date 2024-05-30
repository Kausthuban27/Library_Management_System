using Library_WebApp.Model;
using LibraryData.Models;
using System.Net;

namespace Library_WebApp.Services
{
    public interface IStudentCRUD
    {
        public Task<(HttpStatusCode, bool)> GetStudent(Uri BaseUrl, string username, string password);
        public Task<(HttpStatusCode, bool)> AddStudent<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> Rentbook<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> searchBook<T>(Uri BaseUrl, SearchBook search);
    }
}
