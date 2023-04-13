using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CustomerFunctionApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class CreateCustomerFunction
    {
        [FunctionName("CreateCustomer")]
        public static CreatedResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateCustomer")] HttpRequest req,
            ILogger log,
            [Sql("dbo.Customer", ConnectionStringSetting = "ConnectionStrings:get")] out Customer customer)
        {
            string requestBody =  new StreamReader(req.Body).ReadToEnd();
            
            customer = JsonConvert.DeserializeObject<Customer>(requestBody);

         
            return new CreatedResult($"/api/CreateCustomer", customer);
        }
    }
}
