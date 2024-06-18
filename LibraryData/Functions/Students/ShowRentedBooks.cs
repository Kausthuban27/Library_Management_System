using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using EntityType = LibraryData.Models.EbookRent;

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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EntityType))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("ShowRentedBooks")]
        public async Task<HttpResponseData> BooksRented([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return await FunctionService.CreateGetResponse(req, async () => await _student.RentedBooks<EntityType>(req.Query["username"]!), _logger);
        }
    }
}
