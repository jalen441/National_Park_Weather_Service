using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyDAO : ISurveyDAO
    {
        private string connectionString;

        public SurveyDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
    }
}
