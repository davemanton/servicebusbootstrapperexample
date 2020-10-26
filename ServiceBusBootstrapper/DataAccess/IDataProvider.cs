using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusBootstrapper.DataAccess
{
    public interface IDataProvider<T>
    {
        Task<ICollection<T>> Read();
    }
}