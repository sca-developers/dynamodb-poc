using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    [DynamoDBTable("Network")]
    public class Network : BaseAuditableEntity<string>
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Code { get; set; }
        [DynamoDBProperty]
        public SiteConfiguration Settings { get; set; }
        [DynamoDBProperty]
        public List<string> Tags { get; set; }
    }
}
