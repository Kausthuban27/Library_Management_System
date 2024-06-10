using LibraryData.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace LibraryData.Functions.BorrowBooks
{
    public class ReturnBook
    {
        private readonly ILogger<ReturnBook> _logger;
        private readonly IBook _book;
        public ReturnBook(ILogger<ReturnBook> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "ReturnBook", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Bookname", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("ReturnBook")]
        public async Task<HttpResponseData> ReturnTheBook([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _book.ReturnABook(req.Query["Bookname"]!, req.Query["Username"]!);
            var responseContent = req.CreateResponse();
            if (response.Item2 != null)
            { 
                await responseContent.WriteAsJsonAsync(response.Item2);
                return responseContent;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
