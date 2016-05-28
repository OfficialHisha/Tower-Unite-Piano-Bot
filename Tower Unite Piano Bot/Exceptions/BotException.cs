using System;

namespace Tower_Unite_Piano_Bot.Exceptions
{
    class BotException : Exception
    {
        public BotException(){}
        public BotException(string message) : base(message) {}
        public BotException(string message, Exception innerException) : base(message, innerException){}
    }
}
