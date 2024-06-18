using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Services
{
    public class FunctionService
    {
        public static async Task<HttpResponseData> CreateGetResponse<T>(HttpRequestData req, Func<Task<T?>> provider, ILogger logger) where T : class
        {
            var res = req.CreateResponse();
            try
            {
                var jsonString = JsonConvert.SerializeObject(await provider(), new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
                await res.WriteStringAsync(jsonString);
                return res;
            }
            catch(Exception ex) 
            {
                logger.LogError($"Exception Occured {ex}");
                res.StatusCode = HttpStatusCode.InternalServerError;
                res.Headers.Add("Content-Type","text/plain");
                return res;
            }
        }
    }
}
