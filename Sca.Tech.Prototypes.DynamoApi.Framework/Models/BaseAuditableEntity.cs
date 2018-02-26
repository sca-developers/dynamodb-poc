using System;

using Amazon.DynamoDBv2.DataModel;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models
{
    public abstract class BaseAuditableEntity<T> : BaseDataEntity<T>, IAuditableEntity
    {
        [DynamoDBProperty]
        public virtual bool IsActive { get; set; }
        [DynamoDBProperty]
        public virtual DateTime CreatedDate { get; set; }
        [DynamoDBProperty]
        public virtual string CreatedBy { get; set; }
        [DynamoDBProperty]
        public virtual DateTime ModifiedDate { get; set; }
        [DynamoDBProperty]
        public virtual string ModifiedBy { get; set; }        
    }
}