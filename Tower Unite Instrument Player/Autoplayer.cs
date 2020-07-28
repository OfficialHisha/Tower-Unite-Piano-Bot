using System;
using System.Threading;
using System.Threading.Tasks;
using Tower_Unite_Instrument_Player.Exceptions;
using Tower_Unite_Instrument_Player.Notes;

namespace Tower_Unite_Instrument_Player
{
    public static class Autoplayer
    {
        public static event Action SongFinishedPlaying;
        public static event Action SongWasStopped;
        public static event Action<AutoplayerException> SongWasInteruptedByException;

        //This can be accessed to get the version text
        public static string Version { get; private set; } = "3.0.0";
        
        //This property tells the program to replay the song until a stop event happens
        public static bool Loop { get; set; } = false;

        //This is the delay (in milliseconds) at normal speed
        public static int NormalSpeed { get; set; } = 250;
        //This is the delay (in milliseconds) at fast speed
        public static int FastSpeed { get; set; } = 125;
        
        /// <summary>
        /// This method will play the notes in the song in sequence
        /// </summary>
        public static async Task PlaySong(INote[] song, CancellationToken cancellationToken = default)
        {
            foreach (INote note in song)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    //The foreach loop here is to avoid any keys getting stuck when the song is stopped by the stop keybinding
                    foreach (INote n in song)
                    {
                        n.Stop();
                    }
                    SongWasStopped?.Invoke();
                    return;
                }
                
                try
                {
                    await note.Play();
                }
                catch (AutoplayerTargetNotFoundException error)
                {
                    SongWasInteruptedByException?.Invoke(error);
                    return;
                }
                catch (ArgumentException)
                {
                    SongWasInteruptedByException?.Invoke(new AutoplayerException($"The program encountered an invalid note. Please inform the developer of this incident so it can be added to the list of invalid characters. Info: '{note}'"));
                    return;
                }   
            }
            if(Loop)
            {
                await PlaySong(song);
            }
            SongFinishedPlaying?.Invoke();
        }
    }
}