using Microsoft.Extensions.Caching.Memory;
using ParkMyLots.Data.Interfaces;
using ParkMyLots.Data.Models;
using ParkMyLots.Data.Models.Traffic;
using ParkMyLots.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ParkMyLots.Data.Services
{
    public class TrafficService : ITrafficService
    {
        private readonly IClientService _clientService;
        private readonly IMemoryCache _cache;

        public TrafficService(IClientService clientService, IMemoryCache cache)
        {
            _clientService = clientService;
            _cache = cache;
        }

        public async Task<Carpark> GetCachedCarparks()
        {
            return await _cache.GetOrCreateAsync(CacheHelper.GenerateCarParkCacheKey(), async entry =>
            {
                /*
                 *  DefaultCachedDuration is defaulted at once per day.
                 *  Carpark will not suddenly change location. The only details that will only be
                 *  available lots for parking spaces
                 */
                entry.SlidingExpiration = CacheHelper.DefaultCachedDuration;
                return await GetAllCarparks();
            });
        }

        public async Task<Carpark> GetAllCarparks()
        {
            Carpark Carpark = new Carpark();

            while (true)
            {
                var builder = new UriBuilder(Constant.TRAFFIC_CARKPARKAVAILABILITY);
                var query = HttpUtility.ParseQueryString(builder.Query);
                query.Add("$skip", Carpark.Value.Count.ToString());
                builder.Query = query.ToString();

                var newResult = await _clientService.GetAsync<Carpark>(builder.Uri);

                if (newResult.Value.Count == 0)
                {
                    //Finally add Odata (Just to maintain the integrity)
                    Carpark.OdataMetadata = newResult.OdataMetadata;
                    break;
                }
                Carpark.Value.AddRange(newResult.Value);
            }
            return Carpark;
        }

        public async Task<CarparkDetails> GetCarparkByID(string id)
        {
            int position = (await GetCachedCarparks()).Value.FindIndex(items => items.CarParkId == id);
            //Add possibility that the carpark ID is invalid later on :)
            
            var newResult = await GetCarparkAvailability(position);
            return newResult.Value.FirstOrDefault(items => items.CarParkId == id);
        }

        public async Task<Carpark> GetCarparkInRadius(Position position, double radius = 0.5)
        {
            var result = await GetAllCarparks();
            
            result.Value
                .Select(item => (item, distance : Haversine.Distance(position, StringHelper.ConvertStringToPosition(item.Location))))
                .Where(x => x.distance <= radius)
                .OrderBy(x => x.distance)
                .Select(x => x.item);

            return result;
        }

        private async Task<Carpark> GetCarparkAvailability(int records)
        {
            var builder = new UriBuilder(Constant.TRAFFIC_CARKPARKAVAILABILITY);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("$skip", records.ToString());
            builder.Query = query.ToString();

            return await _clientService.GetAsync<Carpark>(builder.Uri);
        }
    }
}
