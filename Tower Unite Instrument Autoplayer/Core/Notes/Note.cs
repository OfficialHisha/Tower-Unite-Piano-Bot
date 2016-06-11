﻿using System.Windows.Forms;

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

        public Note(char note, bool isHighNote)
        {
            Character = note;
            IsHighNote = isHighNote;
        }

        public void Play()
        {
            //This method is used until a better solution is found. This will NOT play black keys :(
            SendKeys.SendWait(Character.ToString());
        }
    }
}
