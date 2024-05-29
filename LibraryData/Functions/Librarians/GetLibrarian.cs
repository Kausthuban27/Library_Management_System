using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using LibraryData.Interface;

namespace LibraryData.Functions.Librarians
{
    public class GetLibrarian
    {
        private readonly ILogger<GetLibrarian> _logger;
        private readonly ILibrarian _librarian;
        public GetLibrarian(ILogger<GetLibrarian> logger, ILibrarian librarian)
        {
            _logger = logger;
            _librarian = librarian;
        }

        [OpenApiOperation(operationId: "GetLibrarian", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType : typeof(IActionResult), Description = "Librarian Exists")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(IActionResult), Description = "Invalid Details")]
        [Function("GetLibrarian")]
        public async Task<IActionResult> GetLibrarianData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return await _librarian.GetLibrarian(req.Query["Username"]!);
        }
    }
}
