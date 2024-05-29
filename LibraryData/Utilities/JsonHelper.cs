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
    }
}
