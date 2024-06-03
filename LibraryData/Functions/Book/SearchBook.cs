using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LibraryData.Functions.Book
{
    public class SearchBook
    {
        private readonly ILogger<SearchBook> _logger;

        public SearchBook(ILogger<SearchBook> logger)
        {
            _logger = logger;
        }

        [Function("SearchBook")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
