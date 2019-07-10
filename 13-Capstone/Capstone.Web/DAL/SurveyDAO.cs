using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyDAO : ISurveyDAO
    {
        private string connectionString;
        private string addSurveySql = @"INSERT INTO survey_result(parkCode, emailAddress, state, activityLevel)
                                        VALUES (@parkCode, @emailAddress, @state, @activityLevel)";
        private string getFavoritesSql = @"SELECT s.parkCode, p.parkName
                                           FROM survey_result s
                                           JOIN park p
                                           ON p.parkCode = s.parkCode
                                           ORDER BY p.parkName";

        public SurveyDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int AddSurvey(Survey survey)
        {
            int surveyAdded;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(addSurveySql, conn);

                cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", survey.State);
                cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                conn.Open();

                surveyAdded = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return surveyAdded;
        }

        public List<Park> GetFavorites()
        {
            List<Park> parks = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(getFavoritesSql, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = MapResultToPark(reader);

                    parks.Add(park);
                }

                parks = GetFavoriteCount(parks);
            }

            return parks;
        }

        public Park MapResultToPark(SqlDataReader reader)
        {
            Park park = new Park
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
            };

            return park;
        }

        public List<Park> GetFavoriteCount(List<Park> parks)
        {
            List<Park> talliedParks = new List<Park>();
            HashSet<string> parkCodes = new HashSet<string>();
            Dictionary<string, int> parkVotes = new Dictionary<string, int>();
            Dictionary<string, string> parkNames = new Dictionary<string, string>();
            foreach (Park park in parks)
            {
                parkCodes.Add(park.ParkCode);
            }
            foreach (string str in parkCodes)
            {
                parkVotes[str] = 0;
            }
            foreach (Park park in parks)
            {
                if (parkCodes.Contains(park.ParkCode))
                {
                    parkVotes[park.ParkCode]++;
                    parkNames[park.ParkCode] = park.ParkName;
                }
            }

            foreach(string str in parkCodes)
            {
                Park park = new Park
                {
                    ParkCode = str,
                    ParkName = parkNames[str],
                    FavoriteCount = parkVotes[str]
                };

                talliedParks.Add(park);
            }

            return talliedParks;
        }
    }
}
