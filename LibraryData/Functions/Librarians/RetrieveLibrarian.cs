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

namespace LibraryData.Functions.Librarians
{
    public class RetrieveLibrarian
    {
        private readonly ILogger<RetrieveLibrarian> _logger;
        private readonly ILibrarian _librarian;

        public RetrieveLibrarian(ILogger<RetrieveLibrarian> logger, ILibrarian librarian)
        {
            _logger = logger;
            _librarian = librarian;
        }

        [OpenApiOperation(operationId: "RetrieveLibrarian", tags: new[] {" "}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Librarian))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("RetrieveLibrarian")]
        public async Task<HttpResponseData> RetrieveExistingLibrarian([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var res = await _librarian.RetrieveLibrarianData(req.Query["username"]!);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(res);
            return response;

        }
    }
}
