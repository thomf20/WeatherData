using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    public class WeatherData
    {
        public string Datetime { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public double Temprature { get; set; }
        public int Moist { get; set; }
    }
}
