using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.exception
{
    public class InsertEntityException : Exception
    {
        public InsertEntityException() { }

        public InsertEntityException(string message) : base(message) { }
    }
}
