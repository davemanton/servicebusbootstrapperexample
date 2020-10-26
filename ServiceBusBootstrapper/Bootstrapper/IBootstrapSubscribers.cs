using System.Threading.Tasks;

namespace ServiceBusBootstrapper
{
    public interface IBootstrapSubscribers
    {
        void Bootstrap(Topic topic, Subscriber subscriber);
    }
}