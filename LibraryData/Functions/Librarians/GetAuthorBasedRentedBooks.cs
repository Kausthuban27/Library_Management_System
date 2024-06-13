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

namespace LibraryData.Functions.Librarians
{
    public class GetAuthorBasedRentedBooks
    {
        private readonly ILogger<GetAuthorBasedRentedBooks> _logger;
        private readonly ReportService _report;
        public GetAuthorBasedRentedBooks(ILogger<GetAuthorBasedRentedBooks> logger, ReportService reportService)
        {
            _logger = logger;
            _report = reportService;
        }

        [OpenApiOperation(operationId: "GetAuthorBasedRentedBooks", tags: new[] {" "}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Bookname", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(StudentRentedBook))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("GetAuthorBasedRentedBooks")]
        public async Task<HttpResponseData> GetRentedBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var res = await _report.Get_Student_Rented_Books(req.Query["Bookname"]!);

            var response = req.CreateResponse();
            await response.WriteAsJsonAsync(res);
            return response;
        }
    }
}
