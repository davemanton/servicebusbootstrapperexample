using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Management;

namespace ServiceBusBootstrapper
{
    public class ServiceBusBootstrapper : IBootstrapServiceBus
    {
        private readonly IDataProvider<Topic> _topicDataProvider;
        private readonly ManagementClient _serviceBusClient;
        private readonly IBootstrapSubscribers _subscriberBootstrapper;

        public ServiceBusBootstrapper(IDataProvider<Topic> topicDataProvider,
                                      ManagementClient serviceBusClient,
                                      IBootstrapSubscribers subscriberBootstrapper)
        {
            _topicDataProvider = topicDataProvider;
            _serviceBusClient = serviceBusClient;
            _subscriberBootstrapper = subscriberBootstrapper;
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

                            if (!subscriptionExists)
                            {
                                var subscriptionDescription = new SubscriptionDescription(topic.Name, subscriber.Name)
                                {
                                    DefaultMessageTimeToLive = TimeSpan.FromDays(7),
                                    EnableDeadLetteringOnMessageExpiration = true,
                                    LockDuration = TimeSpan.FromMinutes(2),
                                    MaxDeliveryCount = 1
                                };

                                await _serviceBusClient.CreateSubscriptionAsync(subscriptionDescription);
                            }

                            _subscriberBootstrapper.Bootstrap(topic, subscriber);
                        }).ToList();

                    await Task.WhenAll(subscriptionTasks);
                }).ToList();

            await Task.WhenAll(topicTasks);
        }
    }
}