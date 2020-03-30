using System;

namespace Tower_Unite_Instrument_Player_GUI.Exceptions
{
    /// <summary>
    /// Parent class of exceptions related to the band functionality
    /// </summary>
    public class BandException : Exception
    {
        public BandException(){}
        public BandException(string message) : base(message){}
        public BandException(string message, Exception innerException) : base(message, innerException){}
    }
}
