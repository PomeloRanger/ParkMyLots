using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Models.Traffic
{
    public class CarparkDetails
    {
        public string CarParkId { get; set; }
        public string Area { get; set; }
        public string Development { get; set; }
        public string Location { get; set; }
        public int AvailableLots { get; set; }
        public string LotType { get; set; }
        public string Agency { get; set; }
    }
}
