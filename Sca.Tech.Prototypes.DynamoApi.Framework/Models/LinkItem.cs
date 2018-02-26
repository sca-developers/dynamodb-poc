using Amazon.DynamoDBv2.DataModel;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    public class LinkItem : ILinkItem
    {
        [DynamoDBProperty]
        public string Type { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Url { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
    }
}
