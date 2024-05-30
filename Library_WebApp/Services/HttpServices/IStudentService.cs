using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public interface IStudentService
    {
        public Task<(HttpStatusCode, bool)> GetExistingStudent(string username);
        public Task<(HttpStatusCode, bool)> AddNewStudent<T>(Uri BaseUrl, T entity) where T : class;
    }
}
