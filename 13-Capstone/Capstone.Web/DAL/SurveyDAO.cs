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
        private string getFavoritesSql = @"SELECT s.parkCode, p.parkName, COUNT(*) AS faves
                                           FROM survey_result s
                                           JOIN park p
                                           ON p.parkCode = s.parkCode
                                           GROUP BY s.parkCode, p.parkName
                                           ORDER BY p.parkName";

        public SurveyDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Uses form input to add an entry to the survey_result table
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Reads the survey_results table and returns a list of parks to be displayed
        /// </summary>
        /// <returns></returns>
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
            }

            return parks;
        }

        /// <summary>
        /// Maps values from reader to Park object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Park MapResultToPark(SqlDataReader reader)
        {
            Park park = new Park
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                FavoriteCount = Convert.ToInt32(reader["faves"])
            };

            return park;
        }
    }
}
