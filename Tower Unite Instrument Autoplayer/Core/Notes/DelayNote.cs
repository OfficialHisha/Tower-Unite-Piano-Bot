using System.Threading;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This class defines a DelayNote
    /// A DelayNote is a note that extends the default delay
    /// Do note that this delay will be added to two times the default delay
    /// The reason for this is that a default delay is added after each note
    /// including a delay note
    /// </summary>
    public class DelayNote : INote
    {
        public char Character { get; private set; }
        public int Time { get; private set; }

        public DelayNote(char character, int delayTime)
        {
            Character = character;
            Time = delayTime;
        }

        public void Play()
        {
            Thread.Sleep(Time);
        }
    }
}
