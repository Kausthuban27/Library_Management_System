using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using LibraryData.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LibraryData.Functions.Librarians
{
    public class UpdateLibrarian
    {
        private readonly ILogger<UpdateLibrarian> _logger;
        private readonly ILibrarian _librarian;
        public UpdateLibrarian(ILogger<UpdateLibrarian> logger, ILibrarian librarian)
        {
            _logger = logger;
            _librarian = librarian;
        }

        [OpenApiOperation(operationId: "UpdateLibrarian", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddNewLibrarian), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("UpdateLibrarian")]
        public async Task<IActionResult> UpdateExistingLibrarian([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var request = await JsonHelper.DesrializeRequest<AddNewLibrarian>(req);

           return await _librarian.UpdateLibrarian(request);
        }
    }
}
