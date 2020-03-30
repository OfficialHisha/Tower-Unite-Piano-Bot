using System;

namespace Tower_Unite_Instrument_Player_GUI.Exceptions
{
    /// <summary>
    /// This exception is thrown if an error occurs in the band client
    /// </summary>
    public class BandClientException : BandException
    {
        public BandClientException(){}
        public BandClientException(string message) : base(message){}
        public BandClientException(string message, Exception innerException) : base(message, innerException){}
    }
}
