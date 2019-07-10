using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            IList<Park> parks = new List<Park>();

            string sqlQuery = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park();
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }

        public Park GetParkByParkCode(string parkCode)
        {
            return null;
        }
    }
}
