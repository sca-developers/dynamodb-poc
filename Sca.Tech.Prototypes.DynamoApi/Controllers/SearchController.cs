using Microsoft.AspNetCore.Mvc;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services;

namespace Sca.Tech.Prototypes.DynamoApi.Controllers
{
    [Route("api/v2/search")]
    public class SearchController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [Route("tags")]
        public object SearchTags(string[] tags)
        {
            return _searchService.GetSettings(tags);
        }
    }
}
