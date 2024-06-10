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

namespace LibraryData.Functions.Book
{
    public class GetIssuedBooks
    {
        private readonly ILogger<GetIssuedBooks> _logger;
        private readonly IBook _bookCRUD;
        public GetIssuedBooks(ILogger<GetIssuedBooks> logger, IBook book)
        {
            _logger = logger;
            _bookCRUD = book;
        }

        [OpenApiOperation(operationId: "GetIssuedBooks", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BookIssue>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("GetIssuedBooks")]
        public async Task<HttpResponseData> IssuedBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _bookCRUD.IssuedBooksUser(req.Query["Username"]!);
            if(response == null)
            {
                _logger.LogError("The response is null");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            var responseContent = req.CreateResponse();
            if(response.Any())
            {
                await responseContent.WriteAsJsonAsync(response);
                return responseContent;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
