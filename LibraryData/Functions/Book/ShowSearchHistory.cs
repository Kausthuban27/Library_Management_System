using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LibraryData.Functions.Book
{
    public class ShowSearchHistory
    {
        private readonly ILogger<ShowSearchHistory> _logger;

        public ShowSearchHistory(ILogger<ShowSearchHistory> logger)
        {
            _logger = logger;
        }

        [Function("ShowSearchHistory")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
