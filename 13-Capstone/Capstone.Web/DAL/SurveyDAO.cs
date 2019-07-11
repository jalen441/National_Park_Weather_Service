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

                parks = GetFavoriteCount(parks);
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
                FavoriteCount = 1
            };

            return park;
        }

        /// <summary>
        /// Condenses park list to one instance of each park, updates FavoriteCount to the total number of votes per park
        /// </summary>
        /// <param name="parks"></param>
        /// <returns></returns>
        public List<Park> GetFavoriteCount(List<Park> parks)
        {
            for(int i = 0; i < parks.Count - 1; i++)
            {
                // removes duplicate parks from parks list and updates FavoriteCount if there is more than one element
                if(parks.Count > 1 && parks[i].ParkCode == parks[i + 1].ParkCode)
                {
                    parks[i].FavoriteCount++;
                    parks.Remove(parks[i + 1]);
                    i--;
                }
                else // runs if the survey_results table has one or no entries
                {
                    return parks;
                }
            }

            // checks last element in parks list if there is more than 1 element
            if(parks.Count > 1 && parks[parks.Count - 1].ParkCode == parks[parks.Count - 2].ParkCode)
            {
                parks[parks.Count - 1].FavoriteCount++;
                parks.Remove(parks[parks.Count - 2]);
            }

            return parks;
        }
    }
}
