using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceBusBootstrapper.Bootstrapper;

namespace ServiceBusBootstrapper
{
    public class BootstrapperFunction
    {
        public readonly IBootstrapServiceBus _serviceBusBootstrapper;
        public BootstrapperFunction(IBootstrapServiceBus serviceBusBootstrapper)
        {
            _serviceBusBootstrapper = serviceBusBootstrapper;
        }

        [FunctionName("Bootstrapper")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await _serviceBusBootstrapper.Bootstrap();

            return new OkResult();
        }
    }
}
