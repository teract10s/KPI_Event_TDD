using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.exception
{
    public class DeleteEntityException : Exception
    {
        public DeleteEntityException() { }
        
        public DeleteEntityException(string message) : base(message) { }
    }
}
