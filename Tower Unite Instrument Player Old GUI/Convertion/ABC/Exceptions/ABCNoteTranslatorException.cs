using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Unite_Instrument_Autoplayer.ABC
{
    /// <summary>
    /// This exception describes an error with the translation of notes from the ABC Note translator class
    /// </summary>
    class ABCNoteTranslatorException : Exception
    {
        public ABCNoteTranslatorException() { }
        public ABCNoteTranslatorException(string message) : base(message) { }
        public ABCNoteTranslatorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
