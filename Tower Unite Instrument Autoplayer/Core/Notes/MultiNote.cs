﻿namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This class defines a MultiNote
    /// A MultiNote is a collection of notes to be played at once
    /// or, at least close to "at once".
    /// </summary>
    public class MultiNote : INote
    {
        public Note[] Notes { get; set; }

        public MultiNote(Note[] notes)
        {
            Notes = notes;
        }

        public void Play()
        {
            foreach (Note note in Notes)
            {
                note.Play();
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
