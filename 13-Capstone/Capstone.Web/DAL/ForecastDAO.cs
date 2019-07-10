using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ForecastDAO : IForecastDAO
    {
        private string connectionString;

        public ForecastDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Forecast> GetWeatherByParkCode(string parkCode)
        {
            IList<Forecast> forcasts = new List<Forecast>();
            string sqlQuery = "SELECT * FROM weather WHERE parkCode = @parkCode";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Forecast forecast = new Forecast();
                        forecast.ParkCode = reader["parkCode"] as string;
                        forecast.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        forecast.Low = Convert.ToInt32(reader["low"]);
                        forecast.High = Convert.ToInt32(reader["high"]);
                        forecast.ForecastString = reader["forecast"] as string;
                        forcasts.Add(forecast);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return forcasts;
        }
    }
}
