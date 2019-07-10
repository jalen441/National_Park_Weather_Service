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
            string sqlQuery = "SELECT * FROM park";

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
                        park.ParkCode = reader["parkCode"] as string;
                        park.ParkName = reader["parkName"] as string;
                        park.State = reader["state"] as string;
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = reader["climate"] as string;
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = reader["inspirationalQuote"] as string;
                        park.InspirationalQuoteSource = reader["inspirationalQuoteSource"] as string;
                        park.ParkDescription = reader["parkDescription"] as string;
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                        parks.Add(park);
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
            Park park = new Park();
            string sqlQuery = "SELECT * FROM park";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        park.ParkCode = reader["parkCode"] as string;
                        park.ParkName = reader["parkName"] as string;
                        park.State = reader["state"] as string;
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToDecimal(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = reader["climate"] as string;
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = reader["inspirationalQuote"] as string;
                        park.InspirationalQuoteSource = reader["inspirationalQuoteSource"] as string;
                        park.ParkDescription = reader["parkDescription"] as string;
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return park;
        }
    }
}
