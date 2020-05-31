using ParkMyLots.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkMyLots.Extensions
{
    public static class StringHelper
    {
        public static Position ConvertStringToPosition(string input)
        {
            string[] splitResult = input.Split("");
            return new Position
            {
                Latitude = double.Parse(splitResult[0]),
                Longitude = double.Parse(splitResult[1])
            };
        }
    }
}
