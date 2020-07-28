using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Tower_Unite_Instrument_Player.Notes
{
    /// <summary>
    /// This class defines a MultiNote
    /// A MultiNote is a collection of notes to be played at once
    /// or, at least close to "at once".
    /// </summary>
    public class MultiNote : INote
    {
        readonly InputSimulator r_sim = new InputSimulator();

        public Note[] Notes { get; private set; }
        public bool IsHighNote { get; private set; }

        public MultiNote(Note[] notes, bool isHighNote)
        {
            Notes = notes;
            IsHighNote = isHighNote;
        }

        public async Task Play()
        {
            if (IsHighNote)
                r_sim.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);

            await Task.Run(() =>
            {
                foreach (Note note in Notes)
                    r_sim.Keyboard.KeyDown(note.NoteToPlay);
            });
                
            r_sim.Keyboard.Sleep(Notes[0].NoteLength);

            await Task.Run(() =>
            {
                foreach (Note note in Notes)
                    r_sim.Keyboard.KeyUp(note.NoteToPlay);
            });

            r_sim.Keyboard.KeyUp(VirtualKeyCode.LSHIFT);
        }

        public void Stop()
        {
            foreach (Note note in Notes)
            {
                note.Stop();
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (Note note in Notes)
            {
                str += note.ToString();
            }
            return str;
        }
    }
}
