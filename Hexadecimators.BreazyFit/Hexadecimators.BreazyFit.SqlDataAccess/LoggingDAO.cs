using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexadecimators.BreazyFit.Models;
using Microsoft.Data.SqlClient;

namespace Hexadecimators.BreazyFit.SqlDataAccess
{
    public class LoggingDAO
    {
        private string _connectionString;

        public LoggingDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result> LogData(LogModel log)
        {
            var result = new Result();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery =  "Insert into Logs(TimeStamp,LogLevel,UserPerforming,Category,Description) Values('" + log.TimeStamp + "','" + log.LogLevel + "','" + log.UserPerforming + "','" + log.Category + "','" + log.Description + "')";

                var command = new SqlCommand(sqlQuery, connection);

                var rows = command.ExecuteNonQuery();


                if (rows == 1)
                {
                    result.IsSuccessful = true;
                    return result;
                }


                result.IsSuccessful = false;
                result.ErrorMessage = $"Rows affected not 1. rows affected {rows}";

                return result;
            }
        }

    }
}