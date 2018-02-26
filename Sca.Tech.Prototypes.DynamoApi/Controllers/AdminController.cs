using System.Collections.Generic;
using System.Linq;
using System.Net;

using GenFu;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models;
using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services;

namespace Sca.Tech.Prototypes.DynamoApi.Controllers
{
    [Route("api/v2/admin")]
    public class AdminController
    {
        private readonly IAdministrationService _adminService;

        public AdminController(IAdministrationService adminService)
        {
            _adminService = adminService;
        }

        #region Network
        [HttpGet]
        [Route("network/{id?}")]
        public object Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                var result = _adminService.GetNetworks().Result;
                return result;
            }
            else
                return _adminService.SelectNetwork(id).Result?.FirstOrDefault();
        }

        [HttpGet]
        [Route("network/save/{masterId?}")]
        public object SaveNetwork(string masterId)
        {            
            var network = A.New<Network>();
            network.Settings = A.New<SiteConfiguration>();
            network.Settings.Links = A.ListOf<LinkItem>(3);

            // Update Test
            if (!string.IsNullOrWhiteSpace(masterId))
            {
                var existingNetwork = _adminService.SelectNetwork(masterId).Result?.FirstOrDefault();

                if (existingNetwork != null)
                {
                    network = existingNetwork;

                    if (network.Tags == null || !network.Tags.Any())
                    {
                        using (var client = new WebClient())
                        {
                            var downloadedString = client.DownloadString("http://api.wordnik.com:80/v4/words.json/randomWords?hasDictionaryDef=false&minCorpusCount=0&maxCorpusCount=-1&minDictionaryCount=5&maxDictionaryCount=5&minLength=5&maxLength=15&limit=5&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5");
                            var randomWords = JsonConvert.DeserializeObject<List<WordItem>>(downloadedString);

                            network.Tags = randomWords.Select(x => x.Word).ToList();
                        }
                    }
                }
            }

            var updateOperation = _adminService.AddOrUpdate<Network>(network);
            updateOperation.Wait();

            return updateOperation.IsCompleted && !updateOperation.IsFaulted;
        }
        #endregion

        #region Stations
        [HttpGet]
        [Route("station/{id?}")]
        public object GetStations(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return _adminService.GetStations().Result;
            else
                return _adminService.SelectStation(id).Result?.FirstOrDefault();
        }

        [HttpGet]
        [Route("station/save/{masterId?}")]
        public object SaveStation(string masterId)
        {
            var station = A.New<Station>();
            station.Settings = A.New<SiteConfiguration>();
            station.Settings.Links = A.ListOf<LinkItem>(2);

            // Update Test
            if (!string.IsNullOrWhiteSpace(masterId))
            {
                var existingStation = _adminService.SelectStation(masterId).Result?.FirstOrDefault();

                if (existingStation != null)
                {
                    station = existingStation;
                    station.Name += " Updated!";
                }
            }

            var updateOperation = _adminService.AddOrUpdate<Station>(station);
            updateOperation.Wait();

            return updateOperation.IsCompleted && !updateOperation.IsFaulted;
        }
        #endregion

        #region Shows
        [HttpGet]
        [Route("show/{id?}")]
        public object GetShows(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return _adminService.GetShows().Result;
            else
                return _adminService.SelectShow(id).Result?.FirstOrDefault();
        }

        [HttpGet]
        [Route("show/save/{masterId?}")]
        public object SaveShow(string masterId)
        {
            var show = A.New<Show>();

            // Update Test
            if (!string.IsNullOrWhiteSpace(masterId))
            {
                var existingShow = _adminService.SelectShow(masterId).Result?.FirstOrDefault();

                if (existingShow != null)
                {
                    show = existingShow;
                    show.Name += " Updated!";
                }
            }

            var updateOperation = _adminService.AddOrUpdate<Show>(show);
            updateOperation.Wait();

            return updateOperation.IsCompleted && !updateOperation.IsFaulted;
        }
        #endregion
    }
}
