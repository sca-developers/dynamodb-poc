using System.Linq;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models;
using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services;
using Sca.Tech.Prototypes.DynamoApi.Framework.Repositories;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDynamoDataRepository _repository;

        public SearchService(IDynamoDataRepository repository)
        {
            _repository = repository;
        }

        public object GetSettings(string[] tags)
        {
            object result = null;
            if (tags == null || !tags.Any())
                return "No search tags";

            result = _repository.SearchByTags<Network>(tags).Result;

            return result;
        }
    }
}
