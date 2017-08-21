using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This exception is thrown if you are interacting with a custom delay in a wrong way
    /// </summary>
    public class AutoplayerCustomException : AutoplayerException
    {
        public AutoplayerCustomException(){}
        public AutoplayerCustomException(string message) : base(message){}
        public AutoplayerCustomException(string message, Exception innerException) : base(message, innerException){}
    }
}
