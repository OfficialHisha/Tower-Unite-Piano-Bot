using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Utility
{
    /// <summary>
    /// This is the main exception class for the program
    /// All exceptions inherents from this
    /// </summary>
    public class ABCException : Exception
    {
        public ABCException() { }
        public ABCException(string message) : base(message) { }
        public ABCException(string message, Exception innerException) : base(message, innerException) { }
    }
}
