using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBedsAssignment.Models
{

    public class HotelInfo
    {
        public Hotel Hotel { get; set; }
        public List<Rate> Rates { get; set; }
    }

    public class Hotel
    {
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public int GeoId { get; set; }
        public int Rating { get; set; }
    }

    public class Rate
    {
        public string RateType { get; set; }
        public string BoardType { get; set; }
        public double Value { get; set; }
    }
}
