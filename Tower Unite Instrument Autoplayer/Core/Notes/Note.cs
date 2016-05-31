using System;
using System.Windows.Forms;
using Tower_Unite_Instrument_Autoplayer.Imported;

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
        //These constants are used with the PostMessage method
        private const int WM_SETTEXT = 0x000c;
        private const int WM_KEYDOWN = 0x0100;


        public Keys Key { get; private set; }

        public Note(char key)
        {
            Key = (Keys)char.ToUpper(key);
        }

        public void Play()
        {
            //TODO: Figure out what any of this does..
            IntPtr hWndNotepad = NativeMethods.FindWindow("Notepad", null);

            IntPtr hWndEdit = NativeMethods.FindWindowEx(hWndNotepad, IntPtr.Zero, "Edit", null);

            NativeMethods.SendMessage(hWndEdit, WM_SETTEXT, 0, " key");

            NativeMethods.PostMessage(hWndEdit, WM_KEYDOWN, Key, IntPtr.Zero);
        }
    }
}
