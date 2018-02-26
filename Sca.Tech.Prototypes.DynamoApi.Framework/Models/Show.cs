using System.Collections.Generic;

using Amazon.DynamoDBv2.DataModel;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    [DynamoDBTable("Show")]
    public class Show : BaseAuditableEntity<string>
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty]
        public string NetworkCode { get; set; }
        [DynamoDBProperty]
        public string[] StationCodes { get; set; }
        [DynamoDBProperty]
        public string Timeslot { get; set; }
        [DynamoDBProperty]
        public string Url { get; set; }
        [DynamoDBProperty]
        public IEnumerable<ImageData> Images { get; set; }
    }
}
