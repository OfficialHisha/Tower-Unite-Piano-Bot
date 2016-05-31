using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This exception is thrown if a MultiNote end has been reached without having a start definition
    /// </summary>
    class AutoplayerMultiNoteNotDefinedException : AutoplayerException
    {
        public AutoplayerMultiNoteNotDefinedException(){}
        public AutoplayerMultiNoteNotDefinedException(string message) : base(message){}
        public AutoplayerMultiNoteNotDefinedException(string message, Exception innerException) : base(message, innerException){}
    }
}
