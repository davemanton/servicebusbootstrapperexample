using System.Threading.Tasks;

namespace ServiceBusBootstrapper
{
    public interface IBootstrapServiceBus
    {
        Task Bootstrap();
    }
}