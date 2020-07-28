using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Tower_Unite_Instrument_Player.Notes
{
    /// <summary>
    /// This class defines a Note
    /// This is where the behaviour of the note is defined
    /// The Key property holds information about which key
    /// on the keyboard the note corrosponds to
    /// </summary>
    public class Note : INote
    {
        readonly InputSimulator r_sim = new InputSimulator();

        public VirtualKeyCode NoteToPlay { get; private set; }
        public char Character { get; private set; }
        public bool IsHighNote { get; private set; }
        public int NoteLength { get; set; }

        public Note(char character, VirtualKeyCode note, bool isHighNote) : this(character, note, isHighNote, Autoplayer.NormalSpeed){}
        public Note(char character, VirtualKeyCode note, bool isHighNote, int noteLength)
        {
            NoteToPlay = note;
            Character = character;
            IsHighNote = isHighNote;
            NoteLength = noteLength;
        }

        public async Task Play()
        {
            if (IsHighNote)
            {
                await Task.Run(() =>
                {
                    r_sim.Keyboard
                    .KeyDown(VirtualKeyCode.LSHIFT)
                    .KeyDown(NoteToPlay)
                    .Sleep(NoteLength)
                    .KeyUp(NoteToPlay)
                    .KeyUp(VirtualKeyCode.LSHIFT);
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    r_sim.Keyboard
                    .KeyDown(NoteToPlay)
                    .Sleep(NoteLength)
                    .KeyUp(NoteToPlay);
                });
            }
        }

        public void Stop()
        {
            r_sim.Keyboard
            .KeyUp(VirtualKeyCode.LSHIFT)
            .KeyUp(NoteToPlay);
        }

        public override string ToString()
        {
            return Character.ToString();
        }

        public override bool Equals(object obj)
        {
            Note other = obj as Note;

            if(other != null)
            {
                if (Character == other.Character && IsHighNote == other.IsHighNote)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Character.GetHashCode();
        }
    }
}
