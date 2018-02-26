using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    public class SiteConfiguration
    {
        [DynamoDBProperty]
        public string Logo { get; set; }
        [DynamoDBProperty]
        public string MetaTitle { get; set; }
        [DynamoDBProperty]
        public string MetaDescription { get; set; }
        [DynamoDBProperty]
        public string MetaKeywords { get; set; }
        [DynamoDBProperty]
        public List<LinkItem> Links { get; set; }
    }
}
