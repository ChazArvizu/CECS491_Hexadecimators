using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexadecimators.BreazyFit.Models;
using Microsoft.Data.SqlClient;

namespace Hexadecimators.BreazyFit.SqlDataAccess
{
    public class SqlDAO
    {
        private string _connectionString;
        public Boolean rowExists;

        public SqlDAO(string connectionString)
        {
            rowExists = false;
            _connectionString = connectionString;
        }

        public Result ExecuteSql(string sql)
        {
            var result = new Result();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sql, connection);

                //int rows =command.ExecuteNonQuery();
                int rows = 1;
                

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    rowExists = true;
                }

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

        public async Task<Result> LogData(string message)
        {
            var result = new Result();
            return result;
        }

    }
}