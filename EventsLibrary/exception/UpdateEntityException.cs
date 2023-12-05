using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.exception
{
    public class UpdateEntityException : Exception
    {
        public UpdateEntityException() { }

        public UpdateEntityException(string message) : base(message) { }
    }
}
