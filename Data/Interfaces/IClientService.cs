using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Interfaces
{
    public interface IClientService
    {
        Task<T> GetAsync<T>(Uri uri);
    }
}
