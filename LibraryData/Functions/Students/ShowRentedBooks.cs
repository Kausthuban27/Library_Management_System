using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace LibraryData.Functions.Students
{
    public class ShowRentedBooks
    {
        private readonly ILogger<ShowRentedBooks> _logger;
        private readonly IStudent _student;
        public ShowRentedBooks(ILogger<ShowRentedBooks> logger, IStudent student)
        {
            _logger = logger;
            _student = student;
        }

        [OpenApiOperation(operationId: "RentedBooks", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<EbookRent>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("ShowRentedBooks")]
        public async Task<IActionResult> BooksRented([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _student.RentedBooks(req.Query["username"]!);
            try
            {
                if (response.Any())
                {
                    return new OkObjectResult(response);
                }
                return new NotFoundObjectResult("No object is Found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Occured {ex} ");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
