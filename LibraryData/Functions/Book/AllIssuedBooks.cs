using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LibraryData.Functions.Book
{
    public class AllIssuedBooks
    {
        private readonly ILogger<AllIssuedBooks> _logger;
        private readonly IBook _book;

        public AllIssuedBooks(ILogger<AllIssuedBooks> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "AllIssuedBooks", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BookIssue>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("AllIssuedBooks")]
        public async Task<HttpResponseData> AllBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _book.AllIssuedBooks();
            var responseContent = req.CreateResponse();
            if (response != null)
            {
                await responseContent.WriteAsJsonAsync(response);
                return responseContent;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
