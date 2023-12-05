using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.exception
{
    public class IncorrectInputData : System.Exception
    {
        public IncorrectInputData() { }

        public IncorrectInputData(string message) : base(message) { }
    }
}
