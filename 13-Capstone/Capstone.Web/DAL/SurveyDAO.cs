using Capstone.Web.Models;
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

        public void AddSurvey(Survey survey)
        {
            // todo add results of survey to survey table
        }

        public List<Park> GetFavorites()
        {
            List<Park> parks = new List<Park>();

            // todo search for parks based on survey table
            return parks;
        }
    }
}
