using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexadecimators.BreazyFit.Models;

namespace Hexadecimators.BreazyFit.Logging.Abstractions
{
    internal interface ILogger
    {
        Task<Result> Log(string message);
    }
}
