using System;

namespace Tower_Unite_Instrument_Player_GUI.Exceptions
{
    /// <summary>
    /// Exception thrown on band connection failures
    /// </summary>
    public class BandConnectionException : BandException
    {
        public BandConnectionException(){}
        public BandConnectionException(string message) : base(message){}
        public BandConnectionException(string message, Exception innerException) : base(message, innerException){}
    }
}
