using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models;
using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services;
using Sca.Tech.Prototypes.DynamoApi.Framework.Repositories;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IDynamoDataRepository _repository;
        public AdministrationService(IDynamoDataRepository repository)
        {
            _repository = repository;
        }

        #region Networks
        public async Task<List<Network>> GetNetworks()
        {
            return await _repository.List<Network>();
        }

        public async Task<List<Network>> SelectNetwork(string id)
        {
            return await _repository.Get<Network>(Guid.Parse(id));
        }
        #endregion

        #region Stations
        public async Task<List<Station>> GetStations()
        {
            return await _repository.List<Station>();
        }

        public async Task<List<Station>> SelectStation(string id)
        {
            return await _repository.Get<Station>(Guid.Parse(id));
        }
        
        #endregion

        #region Shows
        public async Task<List<Show>> GetShows()
        {
            return await _repository.List<Show>();
        }

        public async Task<List<Show>> SelectShow(string id)
        {
            return await _repository.Get<Show>(Guid.Parse(id));
        }
        #endregion

        public async Task AddOrUpdate<T>(T objToSave)
        {
            await _repository.AddOrUpdate<T>(objToSave);
        }

        #region WIP - Junks
        //public async Task<bool> UpdateNetwork(Network model)
        //{
        //    var filter = Builders<Network>.Filter.Eq(GlobalVariables.MasterId, model.MasterId);

        //    var update = Builders<Network>.Update
        //                                  .Set(x => x.Name, model.Name)
        //                                  .Set(x => x.IsActive, model.IsActive)
        //                                  .Set(x => x.ModifiedDate, DateTime.UtcNow);

        //    await _repository.Collection<Network>().FindOneAndUpdateAsync(filter, update);
        //    return true;
        //}

        //public async Task<DeleteResult> RemoveNetwork(string masterId)
        //{
        //    var filter = Builders<Network>.Filter.Eq(GlobalVariables.MasterId, name);
        //    return await _repository.Collection<Network>().DeleteOneAsync(filter);
        //}

        //public async Task<DeleteResult> RemoveAllNetworks()
        //{
        //    return await _repository.Collection<Network>().DeleteManyAsync(new BsonDocument());
        //}
        #endregion
    }
}
