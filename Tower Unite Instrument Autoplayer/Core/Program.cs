using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Tower_Unite_Instrument_Autoplayer.GUI;

namespace Tower_Unite_Instrument_Autoplayer.Core
{
    static class Program
    {
        //This can be accessed to get the version text
        public static string Version { get; private set; } = "Version 2.0";

        //The note we generate will go in here to form the song
        static List<INote> song = new List<INote>();
        //The delays we generate will go in this list
        static List<Delay> delays = new List<Delay>();
        //This buffer is used when we generate multinotes based on input
        static List<INote> multiNoteBuffer = new List<INote>();

        //This boolean informs the program if we're currently in the process of generating a multinote
        static bool buildingMultiNote = false;
        //This boolean informs the program if it should use the fast delay or not when playing notes
        static bool isFastSpeed = false;
        //This is the delay at normal speed
        static int delayAtNormalSpeed = 200;
        //This is the delay at fast speed
        static int delayAtFastSpeed = 100;

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
                buildingMultiNote = true;
            }
            //Stop multinote building if the input is a ] and add a multinote
            else if(note == ']')
            {
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
                    multiNoteBuffer.Add(new SpeedChangeNote(true));
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
                    multiNoteBuffer.Add(new SpeedChangeNote(false));
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
                    multiNoteBuffer.Add(new DelayNote(delay.Time));
                }
                else
                {
                    song.Add(new DelayNote(delay.Time));
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
            foreach (INote note in song)
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
}