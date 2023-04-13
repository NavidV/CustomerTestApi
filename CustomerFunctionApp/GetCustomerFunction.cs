using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class GetCustomerFunction
    {
        // Visit https://aka.ms/sqlbindingsinput to learn how to use this input binding
    [FunctionName("GetCustomerFunction")]
         public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getcustomer/{id}")] HttpRequest req,
            [Sql("SELECT * FROM [dbo].[Customer] Where CustomerId=@id",
            Parameters ="@id={id}",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "ConnectionStrings:get")] IEnumerable<Object> result,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Input Binding function processed a request.");

            return new OkObjectResult(result);
        }
    }
}
