using Library_WebApp.Model;
using System.Net;

namespace Library_WebApp.Services
{
    public interface ILibrarianCRUD
    {
        public Task<(HttpStatusCode, bool)> GetLibrarian(Uri BaseUrl, string username, string password);
        public Task<(HttpStatusCode, bool)> AddLibrarian<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> IssueBook<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> ReturnedBook<T>(Uri BaseUrl, T entity) where T: class;
        public Task<T> AddBook<T>(Uri BaseUrl, T entity) where T : class;
        public Task CheckAvailability(Uri BaseUrl, SearchBook search);
    }
}
