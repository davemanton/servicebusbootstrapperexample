using ServiceBusBootstrapper.DataAccess;

namespace ServiceBusBootstrapper.Bootstrapper
{
    public interface IBootstrapSubscribers
    {
        void Bootstrap(Topic topic, Subscriber subscriber);
    }
}