using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Greatings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string surname = req.Query["surname"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            surname = surname ?? data?.surname;

            string responseMessage = string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname)
                ? "This HTTP triggered function executed successfully. Pass a name and surname in the query string or in the request body for a personalized response."
                : $"Hello, {name} {surname}!!!";

            return new OkObjectResult(responseMessage);
        }
    }
}