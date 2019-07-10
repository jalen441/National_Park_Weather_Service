using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherDAO : IWeatherDAO
    {
        private string connectionString;

        public WeatherDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
    }
}
