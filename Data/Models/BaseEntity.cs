using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Models
{
    public abstract class BaseEntity<T>
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }
        public List<T> Value { get; set; }

        public BaseEntity()
        {
            Value = new List<T>();
        }
    }
}
