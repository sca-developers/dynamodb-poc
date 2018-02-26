using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Repositories
{
    public interface IDynamoDataRepository
    {
        Task<List<T>> List<T>();
        Task<List<T>> Get<T>(object key);
        Task<List<T>> SearchByTags<T>(string[] tags);
        Task AddOrUpdate<T>(T obj);
    }
}
