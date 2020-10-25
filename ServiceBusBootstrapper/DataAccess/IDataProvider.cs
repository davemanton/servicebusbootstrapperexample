using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusBootstrapper
{
    public interface IDataProvider<T>
    {
        Task<ICollection<T>> Read();
    }
}