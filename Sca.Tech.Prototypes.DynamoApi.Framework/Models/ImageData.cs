using Amazon.DynamoDBv2.DataModel;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    public partial class ImageData : IImageData
    {
        [DynamoDBProperty]
        public string Url { get; set; }
        [DynamoDBProperty]
        public string Type { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
    }
}
