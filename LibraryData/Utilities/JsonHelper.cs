using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using LibraryData.Models;

namespace LibraryData.Utilities
{
    public static class JsonHelper
    {
        public static async Task<T> DesrializeRequest<T>(HttpRequestData request)
        {
            var requestContent = await request.ReadAsStringAsync();
            if(requestContent == null)
            {
                throw new ArgumentNullException(nameof(requestContent));
            }
            var requestData = JsonConvert.DeserializeObject<T>(requestContent);
            return requestData!;
        }

        public static async Task<SearchBooks> SerializeRequest(HttpRequestData request)
        {
            var bookname = request.Query["Bookname"];
            var authorname = request.Query["Authorname"];
            var publishername = request.Query["Publishername"];
            var cateogryname = request.Query["Categoryname"];

            return new SearchBooks { bookName = bookname, authorName = authorname, publisherName = publishername, categoryName = cateogryname };
        }
    }
}
