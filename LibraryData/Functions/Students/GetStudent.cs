using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace LibraryData.Functions.Students
{
    public class GetStudent
    {
        private readonly ILogger<GetStudent> _logger;

        public GetStudent(ILogger<GetStudent> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "GetStudent", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Type = typeof(string), Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "password", In = ParameterLocation.Query, Type = typeof(string), Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(bool), Description = "Student Exists")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(bool), Description = "Invalid Details")]
        [Function("GetStudent")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string username = req.Query["username"]!;
            string password = req.Query["password"]!;
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
