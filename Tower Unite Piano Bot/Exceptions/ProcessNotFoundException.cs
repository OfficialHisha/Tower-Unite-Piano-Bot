using System;

namespace Tower_Unite_Piano_Bot.Exceptions
{
    class ProcessNotFoundException : BotException
    {
        public ProcessNotFoundException() { }
        public ProcessNotFoundException(string message) : base(message) { }
        public ProcessNotFoundException(string message, Exception innerException) : base(message, innerException){ }
    }
}
