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

        public SqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Result ExecuteSql(string sql)
        {
            var result = new Result();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insertSql = "INSERT INTO Hexadecimators.BreazyFit.Logs (Message) values(%message)";

                var parameter = new SqlParameter("message", sql);

                var command = new SqlCommand(insertSql, connection);
                command.Parameters.Add(parameter);

                var rows = command.ExecuteNonQuery();

                if(rows == 1)
                {
                    result.IsSuccessful = true;
                    return result;
                }
                result.IsSuccessful = false;
                return result;
            }
        }

        public Result LogData(String message)
        {
            var result = new Result();
            return result;
        }

    }
}
