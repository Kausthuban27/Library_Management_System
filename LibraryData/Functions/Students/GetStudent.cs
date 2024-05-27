using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using LibraryData.Services;

namespace LibraryData.Functions.Students
{
    public class GetStudent
    {
        private readonly ILogger<GetStudent> _logger;
        private readonly StudentService _studentService;
        public GetStudent(ILogger<GetStudent> logger, StudentService studentService)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [OpenApiOperation(operationId: "GetStudent", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Type = typeof(string), Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "password", In = ParameterLocation.Query, Type = typeof(string), Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(bool), Description = "Student Exists")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(bool), Description = "Invalid Details")]
        [Function("GetStudent")]
        public async Task<IActionResult> GetStudentData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string username = req.Query["username"]!;
            string password = req.Query["password"]!;
            return await _studentService.GetStudent(username, password);
        }
    }
}
