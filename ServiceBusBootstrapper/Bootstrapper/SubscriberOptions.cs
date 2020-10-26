namespace ServiceBusBootstrapper.Bootstrapper
{
    public class SubscriberOptions
    {
        public string SubscribersResourceGroupName { get; set; }
        public string DeploymentRegion { get; set; }
        public string ServiceBusConnectionName { get; set; }
        public string ServiceBusListenConnectionString { get; set; }
        public string ServiceBusConnectionDisplayName { get; set; }
    }
}