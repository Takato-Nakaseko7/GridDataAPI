using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GridDataAPI
{
    public class GridDataController
    {
        private SqlConnectionStringBuilder builder { get;  set; }
        
        public GridDataController()
        {
            builder = new SqlConnectionStringBuilder();

            // When testing on other machine, change properties below to mactch your environment.
            builder.DataSource = "DESKTOP-HG8TVFE\\SQLEXPRESS"; 
            builder.UserID = "sa";
            builder.Password = "sa";
            builder.InitialCatalog = "grid_data";
        }

        /// <summary>
        /// Get the latest value within the given time range
        /// </summary>
        /// <param name="startTime">
        /// Start datetime
        /// </param>
        /// <param name="endTime">
        /// End datetime
        /// </param>
        /// <returns>
        /// The latest value within the two given datetime. When failed, return null.
        /// </returns>
        public string? GetLatestValue(DateTime startTime, DateTime endTime)
        {
            // SQL Query
            string query = $"select\r\n\ttop 1 value\r\n  from \r\n\tMeasure\r\n  where\r\n\trecord_time >= '{startTime}' and record_time <= '{endTime}'\r\n  order by\r\n\trecord_time desc";

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();

                            return reader["value"].ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        /// <summary>
        /// Get a list of values corresponding to the collected time and between start and end time.
        /// </summary>
        /// <param name="startTime">
        /// Start datetime
        /// </param>
        /// <param name="endTime">
        /// End datetime
        /// </param>
        /// <param name="collectedTime">
        /// Collected datetime
        /// </param>
        /// <returns>
        /// A list of values corresponding to the collected time and between start time and end time. When failed, return null.
        /// </returns>
        public List<string>? GetCollectedData(DateTime startTime, DateTime endTime, DateTime collectedTime)
        {
            // SQL Query
            string query = $"select \r\n\tnode_id, value, record_time\r\n  from \r\n\tMeasure\r\n  where\r\n\trecord_time >= '{startTime}' and record_time <= '{endTime}'\r\n    and\r\n\ttarget_time = '{collectedTime}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            List<string> values = new List<string>();

                            // While there is data, keep adding to the list that will be returned
                            while (dr.Read())
                            {
                                if (!string.IsNullOrEmpty(dr["value"].ToString()))
                                {
                                    values.Add(dr["value"].ToString());
                                }
                            }
                            return values;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
