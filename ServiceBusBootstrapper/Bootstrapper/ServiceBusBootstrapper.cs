using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;

namespace ServiceBusBootstrapper
{
    public class ServiceBusBootstrapper : IBootstrapServiceBus
    {
        private readonly IDataProvider<Topic> _topicDataProvider;
        private readonly ManagementClient _serviceBusClient;

        public ServiceBusBootstrapper(IDataProvider<Topic> topicDataProvider,
                                      ManagementClient serviceBusClient)
        {
            _topicDataProvider = topicDataProvider;
            _serviceBusClient = serviceBusClient;
        }

        public async Task Bootstrap()
        {
            var topics = await _topicDataProvider.Read();

            var topicTasks = topics.Select(
                async topic =>
                {
                    var topicExists = await _serviceBusClient.TopicExistsAsync(topic.Name);

                    if (!topicExists)
                        await _serviceBusClient.CreateTopicAsync(topic.Name);

                    var subscriptionTasks = topic.Subscribers.Select(
                        async subscriber =>
                        {
                            var subscriptionExists = await _serviceBusClient.SubscriptionExistsAsync(topic.Name, subscriber.Name);

                            if (subscriptionExists)
                                return;

                            var subscriptionDescription = new SubscriptionDescription(topic.Name, subscriber.Name)
                            {
                                DefaultMessageTimeToLive = TimeSpan.FromDays(7),
                                EnableDeadLetteringOnMessageExpiration = true,
                                LockDuration = TimeSpan.FromMinutes(2),
                                MaxDeliveryCount = 1
                            };

                            await _serviceBusClient.CreateSubscriptionAsync(subscriptionDescription);
                        });

                    await Task.WhenAll(subscriptionTasks);
                });

            await Task.WhenAll(topicTasks);
        }
    }
}