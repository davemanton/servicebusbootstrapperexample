using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBusBootstrapper
{
    public static class BootstrapperFunction
    {
        [FunctionName("Bootstrapper")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var bootstrapper = new ServiceBusBootstrapper();

            await bootstrapper.Bootstrap();

            return new OkResult();
        }
    }
}
