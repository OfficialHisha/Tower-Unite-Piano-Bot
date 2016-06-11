using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This exception is thrown if a note that cannot be in a MultiNote is found in a MultiNote definition
    /// </summary>
    public class AutoplayerInvalidMultiNoteException : AutoplayerNoteCreationFailedException
    {
        public AutoplayerInvalidMultiNoteException(){}
        public AutoplayerInvalidMultiNoteException(string message) : base(message){}
        public AutoplayerInvalidMultiNoteException(string message, Exception innerException) : base(message, innerException){}
    }
}
