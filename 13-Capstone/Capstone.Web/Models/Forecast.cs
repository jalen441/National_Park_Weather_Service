using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Forecast
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string ForecastString { get; set; }
        public List<string> Messages { get; set; }

        public List<string> GenerateForecastMessages()
        {
            List<string> messages = new List<string>();

            if(ForecastString == "snow")
            {
                messages.Add("Pack Snowshoes");
            }
            else if (ForecastString == "rain")
            {
                messages.Add("Pack rain gear and wear waterproof shoes");
            }
            else if (ForecastString == "thunderstorms")
            {
                messages.Add("Seek shelter and avoid hiking on exposed ridges");
            }
            else if (ForecastString == "sunny")
            {
                messages.Add("Pack sunblock");
            }
            else if ((High > 75) || (Low > 75))
            {
                messages.Add("Bring an extra gallon of water");
            }
            else if ((High - Low) > 20)
            {
                messages.Add("Wear breathable layers");
            }
            else if ((High < 20) || (Low < 20))
            {
                messages.Add("Exposure to frigid temperatures for extended periods can cause frostbite");
            }

            return messages;
        }


    }
}
