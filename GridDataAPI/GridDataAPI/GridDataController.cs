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
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        
        public GridDataController()
        {
            builder.DataSource = "DESKTOP-HG8TVFE\\SQLEXPRESS";
            builder.UserID = "sa";
            builder.Password = "sa";
            builder.InitialCatalog = "grid_data";
        }


        public string? GetLatestValue(DateTime startTime, DateTime endTime)
        {
            string query = $"Select\r\n\ttop 1 value\r\n  from \r\n\tMeasure\r\n  where\r\n\trecord_time >= '{startTime}' and record_time <= '{endTime}'\r\n  order by\r\n\trecord_time desc";

            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection succeeded");
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

    }
}
