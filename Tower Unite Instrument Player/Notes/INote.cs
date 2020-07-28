using System.Threading.Tasks;

namespace Tower_Unite_Instrument_Player.Notes
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
        Task Play();
        void Stop();
    }
}
