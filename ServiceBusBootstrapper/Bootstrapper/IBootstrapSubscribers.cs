using System.Threading.Tasks;
using ServiceBusBootstrapper.DataAccess;

namespace ServiceBusBootstrapper.Bootstrapper
{
    public interface IBootstrapSubscribers
    {
        Task Bootstrap(Topic topic, Subscriber subscriber);
    }
}