using Grpc.Core;
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
    public class BooksWithFineAmount
    {
        private readonly ILogger<BooksWithFineAmount> _logger;
        private readonly IBook _book;

        public BooksWithFineAmount(ILogger<BooksWithFineAmount> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "BooksWithFineAmount", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BooksWithFine>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("BooksWithFineAmount")]
        public async Task<HttpResponseData> BooksWithFine([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _book.BooksReturnWithFine();
            var responseContent = req.CreateResponse();
            if(response != null)
            {
                await responseContent.WriteAsJsonAsync(response);
                return responseContent;
            }
            return responseContent;
        }
    }
}
