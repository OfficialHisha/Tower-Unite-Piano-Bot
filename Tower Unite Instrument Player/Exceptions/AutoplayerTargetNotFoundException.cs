using System;

namespace Tower_Unite_Instrument_Player.Exceptions
{
    public class AutoplayerTargetNotFoundException : AutoplayerException
    {
        public AutoplayerTargetNotFoundException(){}
        public AutoplayerTargetNotFoundException(string message) : base(message){}
        public AutoplayerTargetNotFoundException(string message, Exception innerException) : base(message, innerException){}
    }
}
