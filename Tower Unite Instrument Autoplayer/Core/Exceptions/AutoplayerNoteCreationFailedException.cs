using System;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    public class AutoplayerNoteCreationFailedException : AutoplayerException
    {
        public AutoplayerNoteCreationFailedException(){}
        public AutoplayerNoteCreationFailedException(string message) : base(message){}
        public AutoplayerNoteCreationFailedException(string message, Exception innerException) : base(message, innerException){}
    }
}
