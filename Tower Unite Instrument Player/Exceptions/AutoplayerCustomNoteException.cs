using System;

namespace Tower_Unite_Instrument_Player.Exceptions
{
    /// <summary>
    /// This exception is thrown if you are interacting with a custom note in a wrong way
    /// </summary>
    public class AutoplayerCustomNoteException : AutoplayerException
    {
        public AutoplayerCustomNoteException() { }
        public AutoplayerCustomNoteException(string message) : base(message) { }
        public AutoplayerCustomNoteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
