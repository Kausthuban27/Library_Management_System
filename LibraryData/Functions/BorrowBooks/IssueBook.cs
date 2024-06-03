using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LibraryData.Functions.BorrowBooks
{
    public class IssueBook
    {
        private readonly ILogger<IssueBook> _logger;

        public IssueBook(ILogger<IssueBook> logger)
        {
            _logger = logger;
        }

        [Function("IssueBook")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
