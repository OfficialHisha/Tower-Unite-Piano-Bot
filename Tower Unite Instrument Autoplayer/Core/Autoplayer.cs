using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
        public static string Version { get; private set; } = "Version: 3.0";
        //This will be used to define compatibility of save files between versions
        public static List<string> SupportedVersionsSave { get; } = new List<string>() { "Version: 2.1", "Version: 2.2a", "Version: 2.2b", "Version: 2.2c", "Version: 3.0"};
        
        //This property tells the program if we should play the next note in the song
        public static bool Stop { get; set; } = true;
        //This property tells the program to replay the song until a stop event happens
        public static bool Loop { get; set; } = false;

        //The note we generate will go in here to form the song
        public static List<INote> Song { get; private set; } = new List<INote>();
        //The delays we generate will go in this list
        public static List<Delay> Delays { get; private set; } = new List<Delay>();
        //The custom notes goes in this dictionary
        public static Dictionary<Note, Note> CustomNotes { get; private set; } = new Dictionary<Note, Note>();
        //This buffer is used when we generate multinotes based on input
        static List<Note> multiNoteBuffer = new List<Note>();
        
        //This boolean informs the program if we're currently in the process of generating a multinote
        static bool buildingMultiNote = false;
        //This boolean informs the program whether the current multinote consists of high notes or not
        static bool multiNoteIsHighNote = false;
        //This boolean informs the program if it should use the fast delay or not when playing notes
        static bool isFastSpeed = false;
        //This is the delay (in milliseconds) at normal speed
        private static int delayAtNormalSpeed = 250;
        public static int DelayAtNormalSpeed { get { return delayAtNormalSpeed; } set { delayAtNormalSpeed = value; } }
        //This is the delay (in milliseconds) at fast speed
        private static int delayAtFastSpeed = 125;
        public static int DelayAtFastSpeed { get { return delayAtFastSpeed; } set { delayAtFastSpeed = value; } }

        //TESTING: This dictionary will serve as a virtual key lookup. So when a note is created, it will check the dictionary for the virtual keycode of the character
        public static Dictionary<char, WindowsInput.Native.VirtualKeyCode> VirtualDictionary { get; private set; } = new Dictionary<char, WindowsInput.Native.VirtualKeyCode>()
        {
            #region Characters
            ['A'] = WindowsInput.Native.VirtualKeyCode.VK_A,
            ['B'] = WindowsInput.Native.VirtualKeyCode.VK_B,
            ['C'] = WindowsInput.Native.VirtualKeyCode.VK_C,
            ['D'] = WindowsInput.Native.VirtualKeyCode.VK_D,
            ['E'] = WindowsInput.Native.VirtualKeyCode.VK_E,
            ['F'] = WindowsInput.Native.VirtualKeyCode.VK_F,
            ['G'] = WindowsInput.Native.VirtualKeyCode.VK_G,
            ['H'] = WindowsInput.Native.VirtualKeyCode.VK_H,
            ['I'] = WindowsInput.Native.VirtualKeyCode.VK_I,
            ['J'] = WindowsInput.Native.VirtualKeyCode.VK_J,
            ['K'] = WindowsInput.Native.VirtualKeyCode.VK_K,
            ['L'] = WindowsInput.Native.VirtualKeyCode.VK_L,
            ['M'] = WindowsInput.Native.VirtualKeyCode.VK_M,
            ['N'] = WindowsInput.Native.VirtualKeyCode.VK_N,
            ['O'] = WindowsInput.Native.VirtualKeyCode.VK_O,
            ['P'] = WindowsInput.Native.VirtualKeyCode.VK_P,
            ['Q'] = WindowsInput.Native.VirtualKeyCode.VK_Q,
            ['R'] = WindowsInput.Native.VirtualKeyCode.VK_R,
            ['S'] = WindowsInput.Native.VirtualKeyCode.VK_S,
            ['T'] = WindowsInput.Native.VirtualKeyCode.VK_T,
            ['U'] = WindowsInput.Native.VirtualKeyCode.VK_U,
            ['V'] = WindowsInput.Native.VirtualKeyCode.VK_V,
            ['W'] = WindowsInput.Native.VirtualKeyCode.VK_W,
            ['X'] = WindowsInput.Native.VirtualKeyCode.VK_X,
            ['Y'] = WindowsInput.Native.VirtualKeyCode.VK_Y,
            ['Z'] = WindowsInput.Native.VirtualKeyCode.VK_Z,
            #endregion
            #region Numbers
            ['0'] = WindowsInput.Native.VirtualKeyCode.VK_0,
            ['1'] = WindowsInput.Native.VirtualKeyCode.VK_1,
            ['2'] = WindowsInput.Native.VirtualKeyCode.VK_2,
            ['3'] = WindowsInput.Native.VirtualKeyCode.VK_3,
            ['4'] = WindowsInput.Native.VirtualKeyCode.VK_4,
            ['5'] = WindowsInput.Native.VirtualKeyCode.VK_5,
            ['6'] = WindowsInput.Native.VirtualKeyCode.VK_6,
            ['7'] = WindowsInput.Native.VirtualKeyCode.VK_7,
            ['8'] = WindowsInput.Native.VirtualKeyCode.VK_8,
            ['9'] = WindowsInput.Native.VirtualKeyCode.VK_9,
            #endregion
            #region Symbols
            [' '] = WindowsInput.Native.VirtualKeyCode.SPACE,
            ['='] = WindowsInput.Native.VirtualKeyCode.VK_0,
            ['!'] = WindowsInput.Native.VirtualKeyCode.VK_1,
            ['"'] = WindowsInput.Native.VirtualKeyCode.VK_2,
            ['#'] = WindowsInput.Native.VirtualKeyCode.VK_3,
            ['¤'] = WindowsInput.Native.VirtualKeyCode.VK_4,
            ['%'] = WindowsInput.Native.VirtualKeyCode.VK_5,
            ['&'] = WindowsInput.Native.VirtualKeyCode.VK_6,
            ['/'] = WindowsInput.Native.VirtualKeyCode.VK_7,
            ['('] = WindowsInput.Native.VirtualKeyCode.VK_8,
            [')'] = WindowsInput.Native.VirtualKeyCode.VK_9
            #endregion
        };
        //TESTING: This list will contain notes that should always be considered a high note
        public static List<char> AlwaysHighNotes = new List<char>()
        {
            '=',
            '!',
            '"',
            '#',
            '¤',
            '%',
            '&',
            '/',
            '(',
            ')',
        };
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
            buildingMultiNote = false;
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
                multiNoteBuffer.Clear();
            }
            //Stop multinote building if the input is a ] and add a multinote
            else if(note == ']')
            {
                if (!buildingMultiNote)
                {
                    AddingNotesFailed?.Invoke();
                    throw new AutoplayerMultiNoteNotDefinedException("A multi note must have a start before the end!");
                }
                buildingMultiNote = false;
                Song.Add(new MultiNote(multiNoteBuffer.ToArray(), multiNoteIsHighNote));
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
            //If it didn't match any case, it must be a normal note
            else
            {
                WindowsInput.Native.VirtualKeyCode vk;
                try
                {
                    VirtualDictionary.TryGetValue(char.ToUpper(note), out vk);

                    if (vk == 0)
                    {
                        return;
                    }
                }
                catch (ArgumentNullException)
                {
                    return;
                }

                //This will check if the note is an uppercase letter, or if the note is in the list of high notes
                bool isHighNote = char.IsUpper(note) || AlwaysHighNotes.Contains(note);

                if (buildingMultiNote)
                {
                    if (multiNoteBuffer.Count == 0)
                    {
                        multiNoteIsHighNote = char.IsUpper(note) || AlwaysHighNotes.Contains(note);
                    }

                    if (isHighNote != multiNoteIsHighNote)
                    {
                        throw new AutoplayerNoteCreationFailedException($"A multinote cannot contain both high and low keys!");
                    }

                    multiNoteBuffer.Add(new Note(note, vk, isHighNote));
                }
                else
                {
                    Song.Add(new Note(note, vk, isHighNote));
                }
            }
        }
        #endregion

        #region Custom delay handling
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
        /// This method will remove all delays from the list of delays
        /// </summary>
        public static void ResetDelays()
        {
            Delays.Clear();
        }
        #endregion

        #region Custom note handling
        /// <summary>
        /// This method will check if a note exists in the list of custom notes
        /// If it does, it returns true, otherwise it returns false
        /// </summary>
        public static bool CheckNoteExists(Note note)
        {
            return CustomNotes.ContainsKey(note);
        }
        /// <summary>
        /// This method will check if a note exists in the list of custom notes
        /// as well as the connection between the note and the new note of the pair
        /// </summary>
        public static bool CheckNoteExists(Note note, Note newNote)
        {
            Note checkNote;
            CustomNotes.TryGetValue(note, out checkNote);
            if(checkNote != null && checkNote == newNote)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// This method will add a note to the list of custom notes
        /// </summary>
        public static void AddNote(Note note, Note newNote)
        {
            if (!CheckNoteExists(note, newNote))
            {
                CustomNotes.Add(note, newNote);
            }
            else
            {
                throw new AutoplayerCustomNoteException("Trying to add already existing note");
            }
        }
        /// <summary>
        /// This method will remove a note from the list of custom notes
        /// </summary>
        public static void RemoveNote(Note note)
        {
            if (CheckNoteExists(note))
            {
                CustomNotes.Remove(note);
            }
            else
            {
                throw new AutoplayerCustomNoteException("Trying to remove non-existent note");
            }
        }
        /// <summary>
        /// This method will set a new note to the value of a specified note from the list of custom notes
        /// </summary>
        public static void ChangeNote(Note note, Note newNote)
        {
            if (CheckNoteExists(note))
            {
                CustomNotes.Remove(note);
                CustomNotes.Add(note, newNote);
            }
            else
            {
                throw new AutoplayerCustomNoteException("Trying to modify non-existent note");
            }
        }
        /// <summary>
        /// This method will remove all notes from the list of custom notes
        /// </summary>
        public static void ResetNotes()
        {
            CustomNotes.Clear();
        }
        #endregion

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
                        if(note is Note)
                        {
                            if(CustomNotes.ContainsKey((Note)note))
                            {
                                Note customNote;
                                CustomNotes.TryGetValue((Note)note, out customNote);
                                customNote.Play();
                            }
                            else
                            {
                                note.Play();
                            }
                        }
                        else
                        {
                            note.Play();
                        }
                        if (note is Note || note is MultiNote)
                        {
                            if (isFastSpeed)
                            {
                                Thread.Sleep(DelayAtFastSpeed);
                            }
                            else
                            {
                                Thread.Sleep(DelayAtNormalSpeed);
                            }
                        }
                    }
                    catch (AutoplayerTargetNotFoundException error)
                    {
                        Stop = true;
                        SongWasInteruptedByException?.Invoke(error);
                    }
                    catch (ArgumentException)
                    {
                        Stop = true;
                        SongWasInteruptedByException?.Invoke(new AutoplayerException($"The program encountered an invalid note. Please inform the developer of this incident so it can be added to the list of invalid characters. Info: '{note.ToString()}'"));
                    }
                }
                else
                {
                    //The foreach loop here is to avoid any keys getting stuck when the song is stopped by the stop keybinding
                    foreach (INote n in Song)
                    {
                        if (n is Note)
                        {
                            ((Note)n).Stop();
                        }
                        if (n is MultiNote)
                        {
                            ((MultiNote)n).Stop();
                        }
                    }
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
        /// This method sets the Stop property which will stop the song from playing any further
        /// </summary>
        public static void StopSong()
        {
            Stop = true;
        }

        #region Save & Load
        /// <summary>
        /// This method will save the song and its settings to a file at the "path" variable's destination
        /// </summary>
        public static void SaveSong(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(Version);
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
            sw.WriteLine("CUSTOM NOTES");
            sw.WriteLine(CustomNotes.Count);
            if (CustomNotes.Count != 0)
            {
                foreach (KeyValuePair<Note, Note> note in CustomNotes)
                {
                    sw.WriteLine(note.Value.Character);
                    sw.WriteLine(note.Key.Character);
                }
            }
            sw.WriteLine("SPEEDS");
            sw.WriteLine(DelayAtNormalSpeed);
            sw.WriteLine(DelayAtFastSpeed);
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

            #region 2.1+ save format
            if (SupportedVersionsSave.Contains(firstLine))
            {
                if(sr.ReadLine() == "DELAYS")
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
                }
                if(sr.ReadLine() == "CUSTOM NOTES")
                {
                    int noteCount = 0;
                    if (int.TryParse(sr.ReadLine(), out noteCount) && noteCount > 0)
                    {
                        for (int i = 0; i < noteCount; i++)
                        {
                            char origNoteChar;
                            char replaceNoteChar;
                            if (char.TryParse(sr.ReadLine(), out origNoteChar))
                            {
                                if (char.TryParse(sr.ReadLine(), out replaceNoteChar))
                                {
                                    WindowsInput.Native.VirtualKeyCode vkOld;
                                    WindowsInput.Native.VirtualKeyCode vkNew;
                                    try
                                    {
                                        VirtualDictionary.TryGetValue(origNoteChar, out vkOld);
                                        VirtualDictionary.TryGetValue(replaceNoteChar, out vkNew);

                                        if (vkOld == 0 || vkNew == 0)
                                        {
                                            return;
                                        }
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        return;
                                    }

                                    CustomNotes.Add(new Note(origNoteChar, vkOld, char.IsUpper(origNoteChar)), new Note(replaceNoteChar, vkNew, char.IsUpper(replaceNoteChar)));
                                }
                            }
                        }
                    }
                }
                if(sr.ReadLine() == "SPEEDS")
                {
                    int normalSpeed, fastSpeed;
                    int.TryParse(sr.ReadLine(), out normalSpeed);
                    int.TryParse(sr.ReadLine(), out fastSpeed);
                    DelayAtNormalSpeed = normalSpeed;
                    DelayAtFastSpeed = fastSpeed;
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
            #region 2.0 save format (for backwards compatibility)
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
                        if (int.TryParse(sr.ReadLine(), out delayAtNormalSpeed))
                        {
                            Delays.Add(new Delay(' ', DelayAtNormalSpeed));
                            if (sr.ReadLine() == "FAST DELAY")
                            {
                                if (int.TryParse(sr.ReadLine(), out delayAtFastSpeed))
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
            #region 1.0 save format (For backwards compatibility)
            else if (firstLine == "NORMAL DELAY")
            {
                if (int.TryParse(sr.ReadLine(), out delayAtNormalSpeed))
                {
                    if (sr.ReadLine() == "FAST DELAY")
                    {
                        if (int.TryParse(sr.ReadLine(), out delayAtFastSpeed))
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
        #endregion
    }
}