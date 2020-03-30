using System;

namespace Tower_Unite_Instrument_Player_GUI.Exceptions
{
    /// <summary>
    /// This exception is thrown if an error occurs in the band server
    /// </summary>
    public class BandServerException : BandException
    {
        public BandServerException(){}
        public BandServerException(string message) : base(message){}
        public BandServerException(string message, Exception innerException) : base(message, innerException){}
    }
}
