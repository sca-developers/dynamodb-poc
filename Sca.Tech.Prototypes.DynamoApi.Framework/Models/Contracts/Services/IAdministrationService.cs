using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services
{
    public interface IAdministrationService
    {
        Task AddOrUpdate<T>(T objToSave);

        #region Network
        Task<List<Network>> GetNetworks();
        Task<List<Network>> SelectNetwork(string id);
        #endregion

        #region Station
        Task<List<Station>> GetStations();
        Task<List<Station>> SelectStation(string id);
        #endregion

        #region Show
        Task<List<Show>> GetShows();
        Task<List<Show>> SelectShow(string id);
        #endregion
    }
}
