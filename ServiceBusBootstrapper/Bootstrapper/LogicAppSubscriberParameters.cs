using Newtonsoft.Json;

namespace ServiceBusBootstrapper
{
    public class LogicAppSubscriberParameters
    {
        [JsonProperty("logicAppName")]
        public DeploymentParameter LogicAppName { get; set; }
        [JsonProperty("logicAppLocation")]
        public DeploymentParameter LogicAppLocation { get; set; }
        [JsonProperty("topicName")]
        public DeploymentParameter TopicName { get; set; }
        [JsonProperty("subscriberName")]
        public DeploymentParameter SubscriberName { get; set; }
        [JsonProperty("frequencyIntervalSeconds")]
        public DeploymentParameter FrequencyIntervalSeconds { get; set; }
        [JsonProperty("apiUrl")]
        public DeploymentParameter ApiUrl { get; set; }
        [JsonProperty("serviceBusConnectionName")]
        public DeploymentParameter ServiceBusConnectionName { get; set; }
        [JsonProperty("serviceBusDisplayName")]
        public DeploymentParameter ServiceBusDisplayName { get; set; }
        [JsonProperty("serviceBusConnectionString")]
        public DeploymentParameter ServiceBusConnectionString { get; set; }
    }
}