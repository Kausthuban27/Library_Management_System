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

namespace LibraryData.Functions.Book
{
    public class AddBook
    {
        private readonly ILogger<AddBook> _logger;
        private readonly IBook _book;
        public AddBook(ILogger<AddBook> logger, IBook book)
        {
            _logger = logger;
            _book = book;
        }

        [OpenApiOperation(operationId: "AddBook", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BookDetail), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("AddBook")]
        public async Task<HttpResponseData> AddNewBook([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var request = await JsonHelper.DesrializeRequest<BookDetail>(req);
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(req));
                }
                var response = await _book.AddBook(request);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Following Exception Occured {ex}");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
