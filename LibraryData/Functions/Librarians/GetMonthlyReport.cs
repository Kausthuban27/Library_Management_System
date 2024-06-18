using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using EntityType = LibraryData.Models.EbookRent;

namespace LibraryData.Functions.Librarians
{
    public class GetMonthlyReport
    {
        private readonly ILogger<GetMonthlyReport> _logger;
        private readonly IStudent studentService;
        public GetMonthlyReport(ILogger<GetMonthlyReport> logger, IStudent student)
        {
            _logger = logger;
            studentService = student;
        }

        [OpenApiOperation(operationId: "GetMonthlyReport", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<EbookRentReport>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("GetMonthlyReport")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            return await FunctionService.CreateGetResponse(req, async () => await studentService.ExecuteMyStoredProcedureAsync<EntityType>(), _logger);
        }
    }
}
