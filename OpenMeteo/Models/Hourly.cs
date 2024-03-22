using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMeteo.Models
{
    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<double> snowfall { get; set; }
    }

    public class Hourly2
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<double> snowfall { get; set; }
        public List<double> wind_speed_10m { get; set; }
        public List<int> wind_direction_10m { get; set; }
        public List<double> wind_gusts_10m { get; set; }
    }
}
