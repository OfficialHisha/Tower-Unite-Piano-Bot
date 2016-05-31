namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This class defines a MultiNote
    /// A MultiNote is a collection of notes to be played at once
    /// or, at least close to "at once".
    /// </summary>
    class MultiNote : INote
    {
        public INote[] Notes { get; set; }

        public MultiNote(INote[] notes)
        {
            Notes = notes;
        }

        public void Play()
        {
            foreach (INote note in Notes)
            {
                note.Play();
            }
        }
    }
}
