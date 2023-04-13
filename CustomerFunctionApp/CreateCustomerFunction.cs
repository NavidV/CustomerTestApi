using CustomerFunctionApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;

namespace Company.Function
{
    public static class CreateCustomerFunction
    {
        [FunctionName("CreateCustomer")]
        public static CreatedResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createcustomer")] HttpRequest req,
            ILogger log,
            [Sql("dbo.Customer",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "ConnectionStrings:get")] out Customer customer)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();

            customer = JsonConvert.DeserializeObject<Customer>(requestBody);

            log.LogInformation("CreateCustomer function processed a request.");
            return new CreatedResult($"/api/createcustomer", customer);
        }
    }
}
