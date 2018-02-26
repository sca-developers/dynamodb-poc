using System;

using Amazon.DynamoDBv2.DataModel;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    public abstract class BaseDataEntity<T> : IDataEntity<T>
    {
        [DynamoDBProperty]
        public virtual T Id { get; set; }
        [DynamoDBHashKey]
        public virtual Guid MasterId { get; set; }
    }
}