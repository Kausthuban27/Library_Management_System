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

namespace LibraryData.Functions.Book
{
    public class EBookRent
    {
        private readonly ILogger<EBookRent> _logger;
        private readonly IStudent _student;
        public EBookRent(ILogger<EBookRent> logger, IStudent student)
        {
            _logger = logger;
            _student = student;
        }

        [OpenApiOperation(operationId: "EBookRent", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "Bookname", In = ParameterLocation.Query, Required = true, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string))]
        [Function("EBookRent")]
        public async Task<HttpResponseData> EBookRenting([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _student.RentEBook(req.Query["Bookname"]!);
            if (response.Item2)
            {
                return req.CreateResponse(HttpStatusCode.OK);
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
