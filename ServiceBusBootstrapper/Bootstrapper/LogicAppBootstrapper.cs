using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.Extensions.Options;
using ServiceBusBootstrapper.DataAccess;
using ServiceBusBootstrapper.FileManagers;

namespace ServiceBusBootstrapper.Bootstrapper
{
    public class LogicAppBootstrapper : IBootstrapSubscribers
    {
        private readonly SubscriberOptions _config;
        private readonly IAzure _azure;
        private readonly string _logicAppJsonTemplate;

        public LogicAppBootstrapper(
            IOptions<SubscriberOptions> config,
            IAzure azure,
            IFileReader fileReader)
        {
            _config = config.Value;
            _azure = azure;

            _logicAppJsonTemplate = fileReader.ReadFileAsString("ArmTemplates\\logicAppTemplate.json");
        }

        public async Task Bootstrap(Topic topic, Subscriber subscriber)
        {
            var deploymentName = $"ccmq-{topic.Name}-{subscriber.Name}";

            var deploymentExists = _azure.Deployments.CheckExistence(_config.SubscribersResourceGroupName, deploymentName);

            //if (deploymentExists)
            //    return;

            await _azure.Deployments.Define(deploymentName)
                .WithExistingResourceGroup(_config.SubscribersResourceGroupName)
                .WithTemplate(_logicAppJsonTemplate)
                .WithParameters(CreateParameters(deploymentName, topic, subscriber))
                .WithMode(DeploymentMode.Incremental)
                .CreateAsync();
        }

        private LogicAppSubscriberParameters CreateParameters(string deploymentName,
                                                              Topic topic,
                                                              Subscriber subscriber)
            => new LogicAppSubscriberParameters
            {
                LogicAppName = new DeploymentParameter
                {
                    Value = deploymentName
                },
                LogicAppLocation = new DeploymentParameter
                {
                    Value = _config.DeploymentRegion
                },
                ApiUrl = new DeploymentParameter
                {
                    Value = subscriber.Url
                },
                FrequencyIntervalSeconds = new DeploymentParameter
                {
                    Value = topic.FrequencyIntervalSeconds
                },
                ServiceBusConnectionName = new DeploymentParameter
                {
                    Value = _config.ServiceBusConnectionName
                },
                ServiceBusConnectionString = new DeploymentParameter
                {
                    Value = _config.ServiceBusListenConnectionString
                },
                ServiceBusDisplayName = new DeploymentParameter
                {
                    Value = _config.ServiceBusConnectionDisplayName
                },
                SubscriberName = new DeploymentParameter
                {
                    Value = subscriber.Name
                },
                TopicName = new DeploymentParameter
                {
                    Value = topic.Name
                }
            };
    }
}