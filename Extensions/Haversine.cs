using ParkMyLots.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Extensions
{
    public static class Haversine
    {
        public static double Distance(Position A, Position B)
        {
            const double r = 6371; // meters

            var sdlat = Math.Sin((B.Latitude - A.Latitude) / 2);
            var sdlon = Math.Sin((B.Longitude - B.Longitude) / 2);
            var q = sdlat * sdlat + Math.Cos(A.Latitude) * Math.Cos(A.Longitude) * sdlon * sdlon;
            var d = 2 * r * Math.Asin(Math.Sqrt(q));

            return d;
        }
    }
}
