using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This is the main exception class for the program
    /// All exceptions inherents from this
    /// </summary>
    public class AutoplayerException : Exception
    {
        public AutoplayerException(){}
        public AutoplayerException(string message) : base(message){}
        public AutoplayerException(string message, Exception innerException) : base(message, innerException){}
    }
}
