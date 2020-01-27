using System.Threading;
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
        InputSimulator sim = new InputSimulator();

        public Note[] Notes { get; private set; }
        public bool IsHighNote { get; private set; }

        public MultiNote(Note[] notes, bool isHighNote)
        {
            Notes = notes;
            IsHighNote = isHighNote;
        }

        public void Play()
        {
            //EXPERIMENTAL SOLUTION
            //This method is a better solution as you can define a delay. However, this may result in unexpected behaviour should the program be terminated before KeyUp is run!
            //if (IsHighNote)
            //{
            //    sim.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);
            //    Thread.Sleep(8);
            //    foreach (Note note in Notes)
            //    {
            //        sim.Keyboard.KeyDown(note.NoteToPlay);
            //    }
            //    Thread.Sleep(8);
            //    foreach (Note note in Notes)
            //    {
            //        sim.Keyboard.KeyUp(note.NoteToPlay);
            //    }
            //    sim.Keyboard.KeyPress(VirtualKeyCode.LSHIFT);
            //}
            //else
            //{
            //    foreach (Note note in Notes)
            //    {
            //        sim.Keyboard.KeyDown(note.NoteToPlay);
            //    }
            //    Thread.Sleep(8);
            //    foreach (Note note in Notes)
            //    {
            //        sim.Keyboard.KeyUp(note.NoteToPlay);
            //    }
            //}

            foreach (Note note in Notes)
            {
                note.Play();
            }
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
