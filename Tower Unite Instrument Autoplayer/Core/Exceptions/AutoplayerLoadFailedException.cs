using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This exception is thrown if the program fails to load a song properly
    /// </summary>
    public class AutoplayerLoadFailedException : AutoplayerException
    {
        public AutoplayerLoadFailedException(){}
        public AutoplayerLoadFailedException(string message) : base(message){}
        public AutoplayerLoadFailedException(string message, Exception innerException) : base(message, innerException){}
    }
}
