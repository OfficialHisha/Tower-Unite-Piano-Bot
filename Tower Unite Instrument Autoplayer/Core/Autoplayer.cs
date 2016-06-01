using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    public delegate void AutoplayerEvent();
    public delegate void AutoplayerExceptionEvent(AutoplayerException e);

    static class Autoplayer
    {
        #region Events
        public static event AutoplayerEvent AddingNoteFinished;
        public static event AutoplayerEvent AddingNotesFailed;
        public static event AutoplayerEvent NotesCleared;
        public static event AutoplayerEvent LoadCompleted;
        public static event AutoplayerEvent LoadFailed;
        public static event AutoplayerEvent SaveCompleted;
        public static event AutoplayerEvent SongFinishedPlaying;
        public static event AutoplayerEvent SongWasStopped;

        public static event AutoplayerExceptionEvent SongWasInteruptedByException;
        #endregion

        #region Field and property declarations
        //This can be accessed to get the version text
        public static string Version { get; private set; } = "Version: 2.0";

        //This property tells the program if we should play the next note in the song
        public static bool Stop { get; set; } = true;
        //This property tells the program to replay the song until a stop event happens
        public static bool Loop { get; set; } = false;

        //The note we generate will go in here to form the song
        public static List<INote> Song { get; private set; } = new List<INote>();
        //The delays we generate will go in this list
        public static List<Delay> Delays { get; private set; } = new List<Delay>();
        //This buffer is used when we generate multinotes based on input
        static List<Note> multiNoteBuffer = new List<Note>();

        //This boolean informs the program if we're currently in the process of generating a multinote
        static bool buildingMultiNote = false;
        //This boolean informs the program if it should use the fast delay or not when playing notes
        static bool isFastSpeed = false;
        //This is the delay (in milliseconds) at normal speed
        public static int DelayAtNormalSpeed { get; set; } = 200;
        //This is the delay (in milliseconds) at fast speed
        public static int DelayAtFastSpeed { get; set; } = 100;
        #endregion

        #region Note handling
        /// <summary>
        /// This method will clear the song from all notes
        /// </summary>
        public static void ClearAllNotes()
        {
            Song.Clear();
            NotesCleared?.Invoke();
        }
        /// <summary>
        /// This method will take a string of notes and break it down into single notes
        /// and send them to the AddNoteFromChar method to process them
        /// </summary>
        public static void AddNotesFromString(string rawNotes)
        {
            foreach (char note in rawNotes)
            {
                AddNoteFromChar(note);
            }
            AddingNoteFinished?.Invoke();
        }
        /// <summary>
        /// This method will process a given character and add a corrosponding note to the song
        /// </summary>
        public static void AddNoteFromChar(char note)
        {
            Delay delay = Delays.Find(x => x.Character == note);

            //Start multinote building if the input is a [
            if(note == '[')
            {
                if (buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerInvalidMultiNoteException("A multi note cannot be defined whithin a multinote definition!");
                }
                buildingMultiNote = true;
            }
            //Stop multinote building if the input is a ] and add a multinote
            else if(note == ']')
            {
                if (!buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerMultiNoteNotDefinedException("A multi note must be have a start before the end!");
                }
                buildingMultiNote = false;
                Song.Add(new MultiNote(multiNoteBuffer.ToArray()));
                multiNoteBuffer.Clear();
            }
            //Start fast speed if the input is a {
            else if(note == '{')
            {
                //If it's part of a multinote we want to add it to the multinote buffer
                //Otherwise add it as a single note
                if(buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerInvalidMultiNoteException("A speed change note cannot be defined whithin a multinote definition!");
                }
                else
                {
                    Song.Add(new SpeedChangeNote(true));
                }
            }
            //Stop fast speed if the input is a }
            else if(note == '}')
            {
                if (buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerInvalidMultiNoteException("A speed change note cannot be defined whithin a multinote definition!");
                }
                else
                {
                    Song.Add(new SpeedChangeNote(false));
                }
            }
            //Interpret % as spacebar
            else if(note == '%')
            {
                if(buildingMultiNote)
                {
                    multiNoteBuffer.Add(new Note(' ', false));
                }
                else
                {
                    Song.Add(new Note(' ', false));
                }
            }
            //If the input is registered as a delay, add a delay note
            else if(delay != null)
            {
                if(buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerInvalidMultiNoteException("A delay note cannot be defined whithin a multinote definition!");
                }
                else
                {
                    Song.Add(new DelayNote(delay.Character, delay.Time));
                }
            }
            //If we reach a newline, we just ignore it
            else if (note == '\n' || note == '\r')
            {
                return;
            }
            //If the input is not a special character, add it as a normal note
            else
            {
                if(buildingMultiNote)
                {
                    multiNoteBuffer.Add(new Note(note, char.IsUpper(note)));
                }
                else
                {
                    Song.Add(new Note(note, char.IsUpper(note)));
                }
            }
        }
        #endregion
        
        /// <summary>
        /// This method will check if a delay exists in the list of delays
        /// If it does, it returns true, otherwise it returns false
        /// </summary>
        public static bool CheckDelayExists(char character)
        {
            return (Delays.Find(x => x.Character == character) != null);
        }
        /// <summary>
        /// This method will add a delay to the list of delays
        /// </summary>
        public static void AddDelay(char character, int time)
        {
            if (!CheckDelayExists(character))
            {
                Delays.Add(new Delay(character, time));
            }
            else
            {
                throw new AutoplayerCustomDelayException("Trying to add already existing delay");
            }
            
        }
        /// <summary>
        /// This method will remove a delay from the list of delays
        /// </summary>
        public static void RemoveDelay(char character)
        {
            if (CheckDelayExists(character))
            {
                Delays.Remove(Delays.Find(x => x.Character == character));
            }
            else
            {
                throw new AutoplayerCustomDelayException("Trying to remove non-existent delay");
            }
        }
        /// <summary>
        /// This method will set a new time to a specified delay from the list of delays
        /// </summary>
        public static void ChangeDelay(char character, int newTime)
        {
            if (CheckDelayExists(character))
            {
                Delays.Find(x => x.Character == character).Time = newTime;
            }
            else
            {
                throw new AutoplayerCustomDelayException("Trying to modify non-existent delay");
            }
        }
        
        /// <summary>
        /// This method will change the speed according to the changeToFast boolean
        /// </summary>
        public static void ChangeSpeed(bool changeToFast)
        {
            if(changeToFast)
            {
                isFastSpeed = true;
            }
            else
            {
                isFastSpeed = false;
            }
        }

        /// <summary>
        /// This method will play the notes in the song in sequence
        /// </summary>
        public static void PlaySong()
        {
            Stop = false;
            foreach (INote note in Song)
            {
                if (!Stop)
                {
                    try
                    {
                        note.Play();
                        if (isFastSpeed)
                        {
                            Thread.Sleep(DelayAtFastSpeed);
                        }
                        else
                        {
                            Thread.Sleep(DelayAtNormalSpeed);
                        }
                    }
                    catch (AutoplayerTargetNotFoundException error)
                    {
                        Stop = true;
                        SongWasInteruptedByException?.Invoke(error);
                    }
                }
                else
                {
                    SongWasStopped?.Invoke();
                }
            }
            if(Loop)
            {
                PlaySong();
            }
            SongFinishedPlaying?.Invoke();
        }

        /// <summary>
        /// This method sets the Stop property which will (hopefully) stop the song from playing any further
        /// </summary>
        public static void StopSong()
        {
            Stop = true;
        }
        
        /// <summary>
        /// This method will save the song and its settings to a file at the "path" variable's destination
        /// </summary>
        public static void SaveSong(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("DELAYS");
            sw.WriteLine(Delays.Count);
            if (Delays.Count != 0)
            {
                foreach (Delay delay in Delays)
                {
                    sw.WriteLine(delay.Character);
                    sw.WriteLine(delay.Time);
                }
            }
            sw.WriteLine("NOTES");
            sw.WriteLine(Song.Count);
            if (Song.Count != 0)
            {
                foreach (INote note in Song)
                {
                    if(note is DelayNote)
                    {
                        sw.Write(((DelayNote)note).Character);
                    }
                    else if (note is SpeedChangeNote)
                    {
                        if(((SpeedChangeNote)note).TurnOnFast)
                        {
                            sw.Write("{");
                        }
                        else
                        {
                            sw.Write("}");
                        }
                    }
                    else if (note is Note)
                    {
                        sw.Write(((Note)note).Character);
                    }
                    else if (note is MultiNote)
                    {
                        sw.Write("[");
                        foreach (Note multiNote in ((MultiNote)note).Notes)
                        {
                            sw.Write(multiNote.Character);
                        }
                        sw.Write("]");
                    }
                }
            }
            sw.Dispose();
            sw.Close();
            SaveCompleted?.Invoke();
        }
        /// <summary>
        /// This method will load a song and its settings from a file at the "path" variable's destination
        /// This loading method handles all previous save formats for backwards compatibility
        /// </summary>
        public static void LoadSong(string path)
        {
            Song.Clear();
            bool errorWhileLoading = true;
            StreamReader sr = new StreamReader(path);
            string firstLine = sr.ReadLine();
            
            #region New save format
            if (firstLine == "DELAYS")
            {
                int delayCount = 0;
                if (int.TryParse(sr.ReadLine(), out delayCount) && delayCount > 0)
                {
                    for (int i = 0; i < delayCount; i++)
                    {
                        char delayChar;
                        int delayTime = 0;
                        if (char.TryParse(sr.ReadLine(), out delayChar))
                        {
                            if (int.TryParse(sr.ReadLine(), out delayTime))
                            {
                                Delays.Add(new Delay(delayChar, delayTime));
                            }
                        }
                    }
                }
                if (sr.ReadLine() == "NOTES")
                {
                    int noteCount = 0;
                    if (int.TryParse(sr.ReadLine(), out noteCount) && noteCount > 0)
                    {
                        AddNotesFromString(sr.ReadToEnd());
                    }
                    errorWhileLoading = false;
                }
            }
            #endregion
            #region 1.2.2 save format (For backwards compatibility)
            else if (firstLine == "CUSTOM DELAYS")
            {
                int delayCount = 0;
                if (int.TryParse(sr.ReadLine(), out delayCount))
                {
                    if (sr.ReadLine() == "NORMAL DELAY")
                    {
                        if (int.TryParse(sr.ReadLine(), out DelayAtNormalSpeed))
                        {
                            Delays.Add(new Delay(' ', DelayAtNormalSpeed));
                            if (sr.ReadLine() == "FAST DELAY")
                            {
                                if (int.TryParse(sr.ReadLine(), out DelayAtFastSpeed))
                                {
                                    if (delayCount != 0)
                                    {
                                        for (int i = 0; i < delayCount; i++)
                                        {
                                            int customDelayIndex = 0;
                                            int customDelayTime = 0;
                                            char customDelayChar;

                                            if (sr.ReadLine() == "CUSTOM DELAY INDEX")
                                            {
                                                if (int.TryParse(sr.ReadLine(), out customDelayIndex))
                                                {
                                                    if (sr.ReadLine() == "CUSTOM DELAY CHARACTER")
                                                    {
                                                        if (char.TryParse(sr.ReadLine(), out customDelayChar))
                                                        {
                                                            if (sr.ReadLine() == "CUSTOM DELAY TIME")
                                                            {
                                                                if (int.TryParse(sr.ReadLine(), out customDelayTime))
                                                                {
                                                                    Delays.Add(new Delay(customDelayChar, customDelayTime));
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (sr.ReadLine() == "NOTES")
                                    {
                                        AddNotesFromString(sr.ReadToEnd());
                                        errorWhileLoading = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region Old save format (For backwards compatibility)
            else if (firstLine == "NORMAL DELAY")
            {
                if (int.TryParse(sr.ReadLine(), out DelayAtNormalSpeed))
                {
                    if (sr.ReadLine() == "FAST DELAY")
                    {
                        if (int.TryParse(sr.ReadLine(), out DelayAtFastSpeed))
                        {
                            if (sr.ReadLine() == "NOTES")
                            {
                                AddNotesFromString(sr.ReadToEnd());
                                errorWhileLoading = false;
                            }
                        }
                    }
                }
            }
            #endregion
            sr.Close();
            if (errorWhileLoading)
            {
                LoadFailed?.Invoke();
                throw new AutoplayerLoadFailedException("No compatible save format was found!");
            }
            LoadCompleted?.Invoke();
        }
    }
}