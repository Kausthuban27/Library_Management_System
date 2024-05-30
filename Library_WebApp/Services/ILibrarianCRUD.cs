using Library_WebApp.Model;

namespace Library_WebApp.Services
{
    public interface ILibrarianCRUD
    {
        public Task<T> GetLibrarian<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> AddLibrarian<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> IssueBook<T>(Uri BaseUrl, T entity) where T : class;
        public Task<T> ReturnedBook<T>(Uri BaseUrl, T entity) where T: class;
        public Task<T> AddBook<T>(Uri BaseUrl, T entity) where T : class;
        public Task CheckAvailability(Uri BaseUrl, SearchBook search);
    }
}
