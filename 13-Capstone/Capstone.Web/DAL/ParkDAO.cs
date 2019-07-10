using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkDAO : IParkDAO
    {
        private string connectionString;

        public ParkDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> GetParks()
        {
            return null;
        }

        public Park GetParkByParkCode(string parkCode)
        {
            return null;
        }
    }
}
