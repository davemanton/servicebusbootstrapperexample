using System.Threading.Tasks;

namespace ServiceBusBootstrapper.Bootstrapper
{
    public interface IBootstrapServiceBus
    {
        Task Bootstrap();
    }
}