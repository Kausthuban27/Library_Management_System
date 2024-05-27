using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;

namespace LibraryData.Utilities
{
    public static class JsonHelper
    {
        public static async Task<T> DesrializeRequest<T>(HttpRequestData request)
        {
            request.Body.Position = 0;
            using (var reader = new StreamReader(request.Body))
            {
                var body = await reader.ReadToEndAsync();
                return JsonSerializer.Deserialize<T>(body)!;
            }
        }
    }
}
