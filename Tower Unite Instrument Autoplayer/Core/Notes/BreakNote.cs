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
    public class BreakNote : INote
    {
        public char Character { get; private set; }
        public int Time { get; private set; }

        public BreakNote(char character, int breakTime)
        {
            Character = character;
            Time = breakTime;
        }

        public void Play()
        {
            Thread.Sleep(Time);
        }
    }
}
