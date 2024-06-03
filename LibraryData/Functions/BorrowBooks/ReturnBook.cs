using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LibraryData.Functions.BorrowBooks
{
    public class ReturnBook
    {
        private readonly ILogger<ReturnBook> _logger;

        public ReturnBook(ILogger<ReturnBook> logger)
        {
            _logger = logger;
        }

        [Function("ReturnBook")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
