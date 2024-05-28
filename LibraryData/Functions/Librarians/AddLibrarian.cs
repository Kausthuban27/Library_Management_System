using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Utilities;
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
    public class AddLibrarian
    {
        private readonly ILogger<AddLibrarian> _logger;
        private readonly ILibrarian _librarian;
        public AddLibrarian(ILogger<AddLibrarian> logger, ILibrarian librarian)
        {
            _logger = logger;
            _librarian = librarian;
        }

        [Function("AddLibrarian")]
        [OpenApiOperation(operationId: "AddLibrarian", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Librarian), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        public async Task<HttpResponseData> AddLibrarianData([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var librarian = await JsonHelper.DesrializeRequest<Librarian>(req);
            if(librarian == null)
            {
                _logger.LogError("Invalid Details are provided");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            var (statusCode, result) = await _librarian.AddLibrarian(librarian);
            return req.CreateResponse(statusCode);
        }
    }
}
