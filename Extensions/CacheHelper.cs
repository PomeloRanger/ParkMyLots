using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Extensions
{
    public static class CacheHelper
    {
        public static readonly TimeSpan DefaultCachedDuration = TimeSpan.FromDays(1);

        public static string GenerateCarParkCacheKey()
        {
            return "carpark";
        }
    }
}
