namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts
{
    public interface ILinkItem
    {
        string Type { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string Description { get; set; }
    }
}
