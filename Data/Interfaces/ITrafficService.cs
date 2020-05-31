using ParkMyLots.Data.Models;
using ParkMyLots.Data.Models.Traffic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Interfaces
{
    public interface ITrafficService
    {
        Task<Carpark> GetCachedCarparks();
        Task<Carpark> GetAllCarparks();
        Task<CarparkDetails> GetCarparkByID(string id);
        Task<Carpark> GetCarparkInRadius(Position position, double radius = 0.5);
    }
}
