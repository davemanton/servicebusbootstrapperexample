using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceBusBootstrapper;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ServiceBusBootstrapper
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;

            var localSettingsPath = Path.Combine(executionContextOptions.AppDirectory, "local.settings.json");

            var config = new ConfigurationBuilder().AddEnvironmentVariables().AddJsonFile(localSettingsPath, true, false).Build();

            builder.Services.AddOptions<CosmosDbOptions>().Bind(config);
            builder.Services.AddOptions<SubscriberOptions>().Bind(config);

            builder.Services
                .AddSingleton<IDataProvider<Topic>, TopicDataProvider>()
                .AddSingleton(service => new ManagementClient(config["ServiceBusManagementConnectionString"]))
                .AddSingleton(
                    service =>
                    {
                        var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                            config["BootstrapperIdentityClientId"],
                            config["BootstrapperIdentityClientSecret"],
                            config["AzureTenantId"],
                            AzureEnvironment.AzureGlobalCloud);

                        return Microsoft.Azure.Management.Fluent.Azure.Configure()
                            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                            .Authenticate(credentials)
                            .WithSubscription(config["AzureSubscriptionId"]);
                    })
                .AddScoped<IBootstrapServiceBus, ServiceBusBootstrapper>()
                .AddScoped<IBootstrapSubscribers, LogicAppBootstrapper>()
                .AddScoped<IFileReader, FileReader>(service => new FileReader(executionContextOptions.AppDirectory))
                ;
        }
    }
}