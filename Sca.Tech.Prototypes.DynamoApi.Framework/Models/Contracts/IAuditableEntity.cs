using System;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts
{
    public interface IAuditableEntity : IEntity
    {        
        Guid MasterId { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}