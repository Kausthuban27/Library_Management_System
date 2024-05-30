using Library_WebApp.Model;

namespace Library_WebApp.Services
{
    public class LibrarianCRUD : ILibrarianCRUD
    {
        public Task<T> AddBook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task CheckAvailability(Uri BaseUrl, SearchBook search)
        {
            throw new NotImplementedException();
        }

        public Task<T> IssueBook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> ReturnedBook<T>(Uri BaseUrl, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        Task<T> ILibrarianCRUD.AddLibrarian<T>(Uri BaseUrl, T entity)
        {
            throw new NotImplementedException();
        }

        Task<T> ILibrarianCRUD.GetLibrarian<T>(Uri BaseUrl, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
