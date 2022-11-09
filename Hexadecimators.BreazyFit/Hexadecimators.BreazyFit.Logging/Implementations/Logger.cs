using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexadecimators.BreazyFit.Models;
using Hexadecimators.BreazyFit.SqlDataAccess;

namespace Hexadecimators.BreazyFit.Logging.Implementations
{
    public class Logger
    {
        private readonly SqlDAO _dao;

        public Logger(SqlDAO dao)
        {
            _dao = dao;
        }

        public async Task<Result> Log(string message)
        {
            var result = new Result();

            #region Step 1: Input Validation
            if (message == null)
            {
                result.IsSuccessful = true;
                return result;
            }
            if (message.Length > 200)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Message is longer than 200";
                return result;
            }
            if (message.Contains("<"))
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Message contains an invalid character";
                return result;
            }
            #endregion
            var daoResult = await _dao.LogData(message).ConfigureAwait(false);

            if (daoResult.IsSuccessful)
            {
                result.IsSuccessful = true;
                return result;
            }

            return result;

        }
    }
}