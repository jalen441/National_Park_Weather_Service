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
        private string getFavoritesSql = @"SELECT p.parkCode, p.parkName, COUNT(s.parkCode) as favoriteCount
                                           FROM park p
                                           JOIN survey_result s
                                           ON p.parkCode = s.parkCode
                                           GROUP BY p.parkName, p.parkCode
                                           HAVING favoriteCount > = 1
                                           ORDER BY parkName";

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

                surveyAdded = cmd.ExecuteNonQuery();
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

                while(reader.Read())
                {
                    Park park = MapResultToPark(reader);

                    parks.Add(park);
                }
            }

            return parks;
        }

        public Park MapResultToPark(SqlDataReader reader)
        {
            Park park = new Park
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                FavoriteCount = Convert.ToInt32(reader["favoriteCount"])
            };

            return park;
        }
    }
}
