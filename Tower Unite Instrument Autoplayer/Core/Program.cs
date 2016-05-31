using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tower_Unite_Instrument_Autoplayer.GUI;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    static class Program
    {
        #region Field and property declarations
        //This can be accessed to get the version text
        public static string Version { get; private set; } = "Version 2.0";

        //This property tells the program if we should play the next note in the song
        public static bool Stop { get; set; } = true;

        //The note we generate will go in here to form the song
        static List<INote> song = new List<INote>();
        //The delays we generate will go in this list
        static List<Delay> delays = new List<Delay>();
        //This buffer is used when we generate multinotes based on input
        static List<Note> multiNoteBuffer = new List<Note>();

        //This boolean informs the program if we're currently in the process of generating a multinote
        static bool buildingMultiNote = false;
        //This boolean informs the program if it should use the fast delay or not when playing notes
        static bool isFastSpeed = false;
        //This is the delay at normal speed
        static int delayAtNormalSpeed = 200;
        //This is the delay at fast speed
        static int delayAtFastSpeed = 100;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GraphicalUserInterface());
        }

        #region Note handling
        /// <summary>
        /// This method will clear the song from all notes
        /// </summary>
        static void ClearAllNotes()
        {
            song.Clear();
        }
        /// <summary>
        /// This method will take a string of notes and break it down into single notes
        /// and send them to the AddNoteFromChar method to process them
        /// </summary>
        static void AddNotesFromString(string rawNotes)
        {
            foreach (char note in rawNotes)
            {
                AddNoteFromChar(note);
            }
        }
        /// <summary>
        /// This method will process a given character and add a corrosponding note to the song
        /// </summary>
        static void AddNoteFromChar(char note)
        {
            Delay delay = delays.Find(x => x.Character == note);

            //Start multinote building if the input is a [
            if(note == '[')
            {
                if (buildingMultiNote)
                {
                    //TODO: Give an error!
                }
                buildingMultiNote = true;
            }
            //Stop multinote building if the input is a ] and add a multinote
            else if(note == ']')
            {
                if (!buildingMultiNote)
                {
                    //TODO: Give an error!
                }
                buildingMultiNote = false;
                song.Add(new MultiNote(multiNoteBuffer.ToArray()));
                multiNoteBuffer.Clear();
            }
            //Start fast speed if the input is a {
            else if(note == '{')
            {
                //If it's part of a multinote we want to add it to the multinote buffer
                //Otherwise add it as a single note
                if(buildingMultiNote)
                {
                    //TODO: Give an error
                }
                else
                {
                    song.Add(new SpeedChangeNote(true));
                }
            }
            //Stop fast speed if the input is a }
            else if(note == '}')
            {
                if (buildingMultiNote)
                {
                    //TODO: Give an error
                }
                else
                {
                    song.Add(new SpeedChangeNote(false));
                }
            }
            //Interpret % as spacebar
            else if(note == '%')
            {
                if(buildingMultiNote)
                {
                    multiNoteBuffer.Add(new Note(' '));
                }
                else
                {
                    song.Add(new Note(' '));
                }
            }
            //If the input is registered as a delay, add a delay note
            else if(delay != null)
            {
                if(buildingMultiNote)
                {
                    //TODO: Give an error
                }
                else
                {
                    song.Add(new DelayNote(delay.Character, delay.Time));
                }
            }
            //If the input is not a special character, add it as a normal note
            else
            {
                if(buildingMultiNote)
                {
                    multiNoteBuffer.Add(new Note(note));
                }
                else
                {
                    song.Add(new Note(note));
                }
            }
        }
        #endregion

        /// <summary>
        /// This method will add a delay to the list of delays
        /// </summary>
        static void AddDelay(char character, int time)
        {
            delays.Add(new Delay(character, time));
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
        static void PlaySong()
        {
            Stop = false;
            foreach (INote note in song)
            {
                if (!Stop)
                {
                    note.Play();
                    if(isFastSpeed)
                    {
                        Thread.Sleep(delayAtFastSpeed);
                    }
                    else
                    {
                        Thread.Sleep(delayAtNormalSpeed);
                    }
                }
            }
        }

        /// <summary>
        /// This method sets the Stop property which will (hopefully) stop the song from playing any further
        /// </summary>
        static void StopSong()
        {
            Stop = true;
        }

        #region Save and load
        /// <summary>
        /// This method will save the song and its settings to a file at the "path" variable's destination
        /// </summary>
        static void SaveSong(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("DELAYS");
            sw.WriteLine(delays.Count);
            if (delays.Count != 0)
            {
                foreach (Delay delay in delays)
                {
                    sw.WriteLine(delay.Character);
                    sw.WriteLine(delay.Time);
                }
            }
            sw.WriteLine("NOTES");
            sw.WriteLine(song.Count);
            if (song.Count != 0)
            {
                foreach (INote note in song)
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
                        sw.Write(((Note)note).Key.ToString());
                    }
                    else if (note is MultiNote)
                    {
                        sw.Write("[");
                        foreach (Note multiNote in ((MultiNote)note).Notes)
                        {
                            sw.Write(multiNote.Key.ToString());
                        }
                        sw.Write("]");
                    }
                }
            }
            sw.Dispose();
            sw.Close();
        }
        /// <summary>
        /// This method will load a song and its settings from a file at the "path" variable's destination
        /// This loading method handles all previous save formats for backwards compatibility
        /// </summary>
        static void LoadSong(string path)
        {
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
                                delays.Add(new Delay(delayChar, delayTime));
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
                if (int.TryParse(sr.ReadLine(), out delayCount) && delayCount > 0)
                {
                    if (sr.ReadLine() == "NORMAL DELAY")
                    {
                        if (int.TryParse(sr.ReadLine(), out delayAtNormalSpeed))
                        {
                            delays.Add(new Delay(' ', delayAtNormalSpeed));
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
                                                                    delays.Add(new Delay(customDelayChar, customDelayTime));
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
                //TODO: Give error
            }
        }
        #endregion
    }
}