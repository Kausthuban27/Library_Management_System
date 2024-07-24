using LibraryData.Interface;
using LibraryData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LibraryData.Functions.Librarians
{
    public class Stored_Procedure_Performance
    {
        private readonly ILogger<Stored_Procedure_Performance> _logger;
        public Stored_Procedure_Performance(ILogger<Stored_Procedure_Performance> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "GetStoredProcedurePerformance", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("Stored_Procedure_Performance")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            try
            {
                await PerformanceAnalyzer.GetStoredProcedurePerformance();
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured {ex}");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
