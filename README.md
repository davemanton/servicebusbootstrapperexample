# Service Bus Bootstrapper

This example application is for the management of many subscribers to Azure Service Bus subscriptions.

For full details read the blogpost here - <insert link>

To make the solution work you will need some setup in your azure portal as described in the blog post. You will also need to add a local.settings.json file with the following parameters set:

* CosmosConnectionString
* CosmosDatabaseName
* CosmosContainerName
* ServiceBusManagementConnectionString
* ServiceBusListenConnectionString
* ServiceBusConnectionName
* ServiceBusConnectionDisplayName
* BootstrapperIdentityClientId
* BootstrapperIdentityClientSecret
* AzureTenantId
* AzureSubscriptionId
* SubscribersResourceGroupName
* DeploymentRegion
