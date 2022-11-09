using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexadecimators.BreazyFit.Models
{

    public class LogModel
    {

        public DateTime TimeStamp { get; set; }

        public string LogLevel { get; set; }

        public string UserPerforming { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

    }
}