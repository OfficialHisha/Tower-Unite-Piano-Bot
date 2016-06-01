using System;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

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
        //These constants are used with the SendMessage and PostMessage methods
        private const int WM_SETTEXT = 0x000c;
        private const int WM_KEYDOWN = 0x0100;

        //This instantiates the InputSimulator we'll be using for HighNotes
        InputSimulator inputSimulator = new InputSimulator();

        public char Character { get; private set; }
        public bool IsHighNote { get; private set; }
        private Keys key;

        public Note(char note, bool isHighNote)
        {
            Character = note;
            key = (Keys)char.ToUpper(note);
            IsHighNote = isHighNote;
        }

        public void Play()
        {
            IntPtr hWndNotepad = NativeMethods.FindWindow("Notepad", null);
            if(hWndNotepad != IntPtr.Zero)
            {
                IntPtr hWndEdit = NativeMethods.FindWindowEx(hWndNotepad, IntPtr.Zero, "Edit", null);

                NativeMethods.SendMessage(hWndEdit, WM_SETTEXT, 0, string.Empty);

                if(IsHighNote)
                {
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                    NativeMethods.PostMessage(hWndEdit, WM_KEYDOWN, key, IntPtr.Zero);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                }
                else
                {
                    NativeMethods.PostMessage(hWndEdit, WM_KEYDOWN, key, IntPtr.Zero);
                }
            }
            else
            {
                throw new AutoplayerTargetNotFoundException("The targeted application could not be found! Ensure that the application is running");
            }
        }
    }
}
