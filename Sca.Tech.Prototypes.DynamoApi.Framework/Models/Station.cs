using Amazon.DynamoDBv2.DataModel;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    [DynamoDBTable("Station")]
    public class Station : BaseAuditableEntity<string>
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Code { get; set; }
        [DynamoDBProperty]
        public string NetworkCode { get; set; }
        [DynamoDBProperty]
        public string StationType { get; set; }
        [DynamoDBProperty]
        public string State { get; set; }
        [DynamoDBProperty]
        public string Timezone { get; set; }
        [DynamoDBProperty]
        public string Latitude { get; set; }
        [DynamoDBProperty]
        public string Longitude { get; set; }
        [DynamoDBProperty]
        public SiteConfiguration Settings { get; set; }
    }
}
