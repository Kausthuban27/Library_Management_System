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

namespace LibraryData.Functions.BorrowBooks
{
    public class IssueBook
    {
        private readonly ILogger<IssueBook> _logger;
        private readonly IBook _book;
        public IssueBook(ILogger<IssueBook> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "IssueBook", tags: new[] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Bookname", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Username", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<IssueBook>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("IssueBook")]
        public async Task<HttpResponseData> IssueBooks([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = _book.IssueABook(req.Query["Bookname"]!, req.Query["Username"]!);
            var responseContent = req.CreateResponse();
            if (response != null)
            {
                await responseContent.WriteAsJsonAsync(response.Result);
                return responseContent;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
