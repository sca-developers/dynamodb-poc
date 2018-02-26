namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts
{
    public interface IDataEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}