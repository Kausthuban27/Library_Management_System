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
using EntityType = LibraryData.Models.SearchBooks;

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

        [OpenApiOperation(operationId: "SearchBook", tags: new[] { nameof(EntityType) }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Properties", In = ParameterLocation.Query, Required = false, Description = $"Any valid property of {nameof(EntityType)}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BookDetail>))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("SearchBook")]
        public async Task<HttpResponseData> SearchBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var res = req.CreateResponse();
            var result = await _book.SearchTheBook<BookDetail>(req.Query["Properties"]!.ToString());
            if (result == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
            }
            await res.WriteAsJsonAsync(result);
            return res;
        }
    }
}
