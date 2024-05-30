using System.Net;

namespace Library_WebApp.Services.HttpServices
{
    public interface IStudentService
    {
        public Task<(HttpStatusCode, bool)> GetExistingStudent(string username, string password);
        public Task<(HttpStatusCode, bool)> AddNewStudent<T>(T entity) where T : class;
    }
}
