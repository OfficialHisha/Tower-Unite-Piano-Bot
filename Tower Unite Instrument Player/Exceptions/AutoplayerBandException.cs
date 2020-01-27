using System;

namespace Tower_Unite_Instrument_Player.Exceptions
{
    /// <summary>
    /// Parent class of exceptions related to the band functionality
    /// </summary>
    public class AutoplayerBandException : AutoplayerException
    {
        public AutoplayerBandException(){}
        public AutoplayerBandException(string message) : base(message){}
        public AutoplayerBandException(string message, Exception innerException) : base(message, innerException){}
    }
}
