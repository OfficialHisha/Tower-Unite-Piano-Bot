using System;

namespace Tower_Unite_Instrument_Player.Exceptions
{
    /// <summary>
    /// This exception is thrown if an error occurs in the band client
    /// </summary>
    public class AutoplayerBandClientException : AutoplayerBandException
    {
        public AutoplayerBandClientException(){}
        public AutoplayerBandClientException(string message) : base(message){}
        public AutoplayerBandClientException(string message, Exception innerException) : base(message, innerException){}
    }
}
