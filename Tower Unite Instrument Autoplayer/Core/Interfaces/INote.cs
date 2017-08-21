namespace Tower_Unite_Instrument_Autoplayer.Core
{
    /// <summary>
    /// This interface is a set of rules that all notes has to follow
    /// in order to qualify as a note
    /// </summary>
    public interface INote
    {
        /// <summary>
        /// This method is invoked when the note is reached in the song
        /// </summary>
        void Play();
    }
}
