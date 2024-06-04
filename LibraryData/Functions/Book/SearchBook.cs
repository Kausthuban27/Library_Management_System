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
using Microsoft.OpenApi.Models;
using System.Net;

namespace LibraryData.Functions.Book
{
    public class SearchBook
    {
        private readonly ILogger<SearchBook> _logger;
        private readonly IBook _book;
        public SearchBook(ILogger<SearchBook> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "SearchBook", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Bookname", In = ParameterLocation.Query, Required = false, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Authorname", In = ParameterLocation.Query, Required = false, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Publishername", In = ParameterLocation.Query, Required = false, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Categoryname", In = ParameterLocation.Query, Required = false, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BookDetail>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("SearchBook")]
        public async Task<HttpResponseData> SearchBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var request = await JsonHelper.SerializeRequest(req);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(req));
            }
            var response =await _book.SearchTheBook(request);
            var responseContent = req.CreateResponse();
            if (response.Any())
            {
                await responseContent.WriteAsJsonAsync(response);
                return responseContent;
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
