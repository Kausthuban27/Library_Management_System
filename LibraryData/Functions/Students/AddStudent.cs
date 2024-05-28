using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using LibraryData.Models;
using System.Net;
using LibraryData.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryData.Utilities;
using Microsoft.IdentityModel.Tokens;
using LibraryData.Interface;
using Azure.Core;
using Newtonsoft.Json;

namespace LibraryData.Functions.Students
{
    public class AddStudent
    {
        private readonly ILogger<AddStudent> _logger;
        private readonly IStudent _student;

        public AddStudent(ILogger<AddStudent> logger, IStudent student)
        {
            _logger = logger;
            _student = student;
        }

        [Function("AddStudent")]
        [OpenApiOperation(operationId: "AddStudent", tags: new [] {""}, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Student), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Student Added Successfully")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Invalid Details")]
        public async Task<HttpResponseData> AddStudentData([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var student = await JsonHelper.DesrializeRequest<Student>(req);
                var (statusCode, result) = await _student.AddStudent(student);
                if(result)
                {
                    return req.CreateResponse(HttpStatusCode.OK);
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception) 
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
