using Interceptor;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This class defines a Note
    /// This is where the behaviour of the note is defined
    /// The Key property holds information about which key
    /// on the keyboard the note corrosponds to
    /// </summary>
    class Note : INote
    {
        public char Character { get; private set; }
        public bool IsHighNote { get; private set; }
        private Keys key;

        public Note(char note, bool isHighNote)
        {
            Character = note;
            key = (Keys)char.ToUpper(note);
            IsHighNote = isHighNote;
        }

        public void Play()
        {
            if(IsHighNote)
            {
                Autoplayer.InterceptorInput.SendKey(Keys.LeftShift, KeyState.Down);
                Autoplayer.InterceptorInput.SendKey(key);
                Autoplayer.InterceptorInput.SendKey(Keys.LeftShift, KeyState.Up);
            }
            else
            {
                Autoplayer.InterceptorInput.SendKey(key);
            }
        }
    }
}
