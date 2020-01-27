using System;

namespace Tower_Unite_Instrument_Player.Exceptions
{
    /// <summary>
    /// This exception is thrown if an error occurs in the band server
    /// </summary>
    public class AutoplayerBandServerException : AutoplayerBandException
    {
        public AutoplayerBandServerException(){}
        public AutoplayerBandServerException(string message) : base(message){}
        public AutoplayerBandServerException(string message, Exception innerException) : base(message, innerException){}
    }
}
