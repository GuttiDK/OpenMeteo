using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMeteo.Models
{
    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string snowfall { get; set; }
    }

    public class HourlyUnits2
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string snowfall { get; set; }
        public string wind_speed_10m { get; set; }
        public string wind_direction_10m { get; set; }
        public string wind_gusts_10m { get; set; }
    }
}
