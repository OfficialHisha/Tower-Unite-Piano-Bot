using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Utilities;
using ABC_Utility;
//This is how to tell the form application to use the core
//You will need to use this if you want to make your own GUI
//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
using Tower_Unite_Instrument_Autoplayer.Core;

namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    public partial class GraphicalUserInterface : Form
    {
        //Here we create a thread that will be used to play the song
        //This way the program will not freeze while playing
        List<Thread> songThreads = new List<Thread>();

        //This is the hook for the key bindings, until I find a better solution
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();

        //The key bind to start playing
        Keys startKey;
        //The key bind to stop playing
        Keys stopKey;

        //This variable is used to ignore changes to the note box when loading a saved file
        bool isLoading = false;

        /// <summary>
        /// This is the constructor for the GUI
        /// It is called when the GUI starts
        /// </summary>
        public GraphicalUserInterface()
        {
            InitializeComponent();
            ErrorTextBox.Hide();
            VersionLabel.Text = Autoplayer.Version;
            Autoplayer.AddingNoteFinished += EnablePlayButton;
            Autoplayer.SongFinishedPlaying += EnableClearButton;
            Autoplayer.SongWasStopped += EnableClearButton;
            Autoplayer.SongWasStopped += SongStopped;
            Autoplayer.SongWasInteruptedByException += ExceptionHandler;

            //Subscribe the method "GKS_KeyDown" to the KeyDown event of the GlobalKeyboardHook
            gkh.KeyDown += new KeyEventHandler(GKS_KeyDown);

            //This converts the text from the keybind settings window to actual keys
            //Then we add them to the global hook (This is done so the keypresses will be detected when the application is not in focus)
            KeysConverter keysConverter = new KeysConverter();
            startKey = (Keys)keysConverter.ConvertFromString(StartKeyTextBox.Text.ToString());
            stopKey = (Keys)keysConverter.ConvertFromString(StopKeyTextBox.Text.ToString());
            gkh.HookedKeys.Add(startKey);
            gkh.HookedKeys.Add(stopKey);
        }

        /// <summary>
        /// This method handles the key bind presses
        /// NOTE: This might trigger anti-virus software
        /// as this is a popular method used in keyloggers
        /// </summary>
        private void GKS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == startKey)
            {
                PlayButton.PerformClick();
            }
            else if (e.KeyCode == stopKey)
            {
                StopButton.PerformClick();
            }
            e.Handled = true;
        }

        /// <summary>
        /// This method will run all the update methods.
        /// </summary>
        public void UpdateEverything()
        {
            UpdateNoteBox();
            UpdateDelayListBox();
            NormalDelayBox.Value = Autoplayer.DelayAtNormalSpeed;
            FastDelayBox.Value = Autoplayer.DelayAtFastSpeed;
            isLoading = false;
        }
        /// <summary>
        /// This method updates the DelayListBox with all delays from the Delays list in the main program
        /// Each custom delay has its own line
        /// </summary>
        public void UpdateDelayListBox()
        {
            DelayListBox.Clear();
            foreach (Delay delay in Autoplayer.Delays)
            {
                DelayListBox.Text += $"Character: '{delay.Character}' : Delay: '{delay.Time}'\n";
            }
        }
        /// <summary>
        /// This method updates the CustomNotesListBox with all notes from the CustomNotes list in the main program
        /// Each custom note has its own line
        /// </summary>
        public void UpdateCustomNoteListBox()
        {
            CustomNoteListBox.Clear();
            foreach (Note note in Autoplayer.CustomNotes.Keys)
            {
                Note newNote;
                Autoplayer.CustomNotes.TryGetValue(note, out newNote);
                CustomNoteListBox.Text += $"Changed from '{note}' to '{newNote}'\n";
            }
        }
        /// <summary>
        /// This method updates the NoteTextBox with all notes from the Song list in the main program
        /// </summary>
        public void UpdateNoteBox()
        {
            NoteTextBox.Clear();
            foreach (INote note in Autoplayer.Song)
            {
                if (note is DelayNote)
                {
                    NoteTextBox.Text += (((DelayNote)note).Character);
                }
                else if (note is SpeedChangeNote)
                {
                    if (((SpeedChangeNote)note).TurnOnFast)
                    {
                        NoteTextBox.Text += "{";
                    }
                    else
                    {
                        NoteTextBox.Text += "}";
                    }
                }
                else if (note is Note)
                {
                    NoteTextBox.Text += ((Note)note).Character;
                }
                else if (note is MultiNote)
                {
                    NoteTextBox.Text += "[";
                    foreach (Note multiNote in ((MultiNote)note).Notes)
                    {
                        NoteTextBox.Text += multiNote.Character;
                    }
                    NoteTextBox.Text += "]";
                }
            }
        }
        /// <summary>
        /// This method makes or remakes the song by clearing all notes and adding the ones from NoteTextBox
        /// </summary>
        private void MakeSong()
        {
            try
            {
                ErrorTextBox.Hide();
                DisablePlayButton();
                Autoplayer.ClearAllNotes();
                Autoplayer.AddNotesFromString(NoteTextBox.Text);
            }
            catch (AutoplayerNoteCreationFailedException e)
            {
                ErrorTextBox.Text = $"ERROR: {e.Message}";
                ErrorTextBox.Show();
            }
        }
        
        /// <summary>
        /// This method disables the play button so the user cannot press it
        /// </summary>
        private void DisablePlayButton()
        {
            PlayButton.Enabled = false;
        }
        /// <summary>
        /// This method enables the play button and makes it interactable
        /// </summary>
        private void EnablePlayButton()
        {
            PlayButton.Enabled = true;
        }

        /// <summary>
        /// This method disables the clear button so the user cannot press it
        /// </summary>
        private void DisableClearButton()
        {
            ClearNotesButton.Enabled = false;
        }
        /// <summary>
        /// This method enables the clear button and makes it interactable
        /// </summary>
        private void EnableClearButton()
        {
            if (ClearNotesButton.InvokeRequired)
            {
                MethodInvoker methodInvokerDelegate = delegate () { ClearNotesButton.Enabled = true; };
                ClearNotesButton.Invoke(methodInvokerDelegate);
            }
            else
            {
                ClearNotesButton.Enabled = true;
            }
        }
        
        /// <summary>
        /// This method will handle exceptions thrown from other threads than the current one
        /// This was added because I had some problems with exceptions from other threads not being catched
        /// </summary>
        private void ExceptionHandler(AutoplayerException exception)
        {
            ErrorTextBox.Text = exception.Message;
            ErrorTextBox.Show();
        }

        #region Custom delay buttons
        /// <summary>
        /// This is called when we click the AddDelayButton
        /// </summary>
        private void AddDelayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(Autoplayer.CheckDelayExists(CustomDelayCharacterBox.Text.ToCharArray()[0]))
                {
                    //If the delay already exists, just update the time value
                    Autoplayer.ChangeDelay(CustomDelayCharacterBox.Text.ToCharArray()[0], (int)CustomDelayTimeBox.Value);
                }
                else
                {
                    //If the delay does not exist, add a new entry
                    Autoplayer.AddDelay(CustomDelayCharacterBox.Text.ToCharArray()[0], (int)CustomDelayTimeBox.Value);
                }
                //Update the GUI element to show the delays in the GUI
                UpdateDelayListBox();

                //Update the current notes to with the new rules
                MakeSong();
            }
            catch (AutoplayerCustomDelayException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// This is called when we click the RemoveDelayButton
        /// </summary>
        private void RemoveDelayButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Remove the delay from the list
                Autoplayer.RemoveDelay(CustomDelayCharacterBox.Text.ToCharArray()[0]);
                //Update the GUI
                UpdateDelayListBox();

                //Update the current notes wtih the new rules
                MakeSong();
            }
            catch (AutoplayerCustomDelayException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// This is called when we click the RemoveAllDelayButton
        /// </summary>
        private void RemoveAllDelayButton_Click(object sender, EventArgs e)
        {
            //Clear the list of delays
            Autoplayer.ResetDelays();

            //Update the GUI
            UpdateDelayListBox();

            //Update the current notes wtih the new rules
            MakeSong();
        }
        #endregion

        #region Custom note buttons
        /// <summary>
        /// This is called when we click the AddNoteButton
        /// </summary>
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            try
            {
                char character = CustomNoteCharacterBox.Text.ToCharArray()[0];
                char newCharacter = CustomNoteNewCharacterBox.Text.ToCharArray()[0];

                WindowsInput.Native.VirtualKeyCode vkOld;
                WindowsInput.Native.VirtualKeyCode vkNew;
                try
                {
                    Autoplayer.VirtualDictionary.TryGetValue(character, out vkOld);
                    Autoplayer.VirtualDictionary.TryGetValue(newCharacter, out vkNew);

                    if (vkOld == 0 || vkNew == 0)
                    {
                        return;
                    }
                }
                catch (ArgumentNullException)
                {
                    return;
                }

                //This will check if the note is an uppercase letter, or if the note is in the list of high notes
                bool isOldHighNote = char.IsUpper(character) || Autoplayer.AlwaysHighNotes.Contains(character);
                bool isNewHighNote = char.IsUpper(newCharacter) || Autoplayer.AlwaysHighNotes.Contains(newCharacter);

                Note note = new Note(character, vkOld, isOldHighNote);
                Note newNote = new Note(newCharacter, vkNew, isNewHighNote);

                if (Autoplayer.CheckNoteExists(note))
                {
                    //If the note already exists, just update it
                    Autoplayer.ChangeNote(note, newNote);
                }
                else
                {
                    //If the note does not exist, add a new entry
                    Autoplayer.AddNote(note, newNote);
                }
                //Update the GUI element to show the delays in the GUI
                UpdateCustomNoteListBox();

                //Update the current notes to with the new rules
                MakeSong();
            }
            catch (AutoplayerCustomNoteException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// This is called when we click the RemoveNoteButton
        /// </summary>
        private void RemoveNoteButton_Click(object sender, EventArgs e)
        {
            try
            {
                char character = CustomNoteCharacterBox.Text.ToCharArray()[0];

                WindowsInput.Native.VirtualKeyCode vk;
                try
                {
                    Autoplayer.VirtualDictionary.TryGetValue(character, out vk);

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
                bool isHighNote = char.IsUpper(character) || Autoplayer.AlwaysHighNotes.Contains(character);

                Note note = new Note(character, vk, isHighNote);

                //Remove the note from the dictonary
                Autoplayer.RemoveNote(note);
                //Update the GUI
                UpdateCustomNoteListBox();

                //Update the current notes wtih the new rules
                MakeSong();
            }
            catch (AutoplayerCustomNoteException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /// <summary>
        /// This is called when we click the RemoveAllNotesButton
        /// </summary>
        private void RemoveAllNotesButton_Click(object sender, EventArgs e)
        {
            //Clear the dictonary of custom notes
            Autoplayer.ResetNotes();

            //Update the GUI
            UpdateCustomNoteListBox();

            //Update the current notes wtih the new rules
            MakeSong();
        }
        #endregion

        /// <summary>
        /// This is called when we click the ClearNotesButton
        /// </summary>
        private void ClearNotesButton_Click(object sender, EventArgs e)
        {
            Autoplayer.ClearAllNotes();
            //Updates the note box GUI element to show the notes in the GUI
            UpdateNoteBox();
        }
        /// <summary>
        /// This is called when we click the PlayButton
        /// </summary>
        private void PlayButton_Click(object sender, EventArgs e)
        {
            //Disable the clear notes button so we don't clear the notes
            //while trying to play them
            DisableClearButton();
            //Disable the play button so we don't create another thread
            //while one is running
            //DisablePlayButton();
            //Start the song in a new thread so we can do other things in
            //the program when the song is playing. Stopping the song for example
            songThreads.Insert(0, new Thread(Autoplayer.PlaySong));
            songThreads[0].Start();
        }
        /// <summary>
        /// This is called when we click the StopButton
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            Autoplayer.StopSong();
            //Close the thread(s) and enable the ClearNotesButton as well as the PlayButton
            //songThreads.ForEach(x => x.Abort());
            //songThreads.Clear();
            //EnableClearButton();
        }
        private void SongStopped()
        {
            //Close the thread(s) and enable the ClearNotesButton as well as the PlayButton
            songThreads.ForEach(x => x.Abort());
            songThreads.Clear();
            EnableClearButton();
        }
        /// <summary>
        /// This is called when we click the LoadButton
        /// </summary>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            //This sets the load dialog to filter on .txt files.
            fileDialog.Filter = "Text File | *.txt";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    isLoading = true;
                    Autoplayer.ResetDelays();
                    Autoplayer.LoadSong(fileDialog.FileName);
                    //Update everything when we are done loading
                    UpdateEverything();
                    MessageBox.Show("Loading completed");
                }
                catch (AutoplayerLoadFailedException error)
                {
                    isLoading = false;
                    MessageBox.Show($"Loading failed: {error.Message}");
                }
            }
        }
        /// <summary>
        /// This is called when we click the SaveButton
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            //This sets the save dialog to filter on .txt files.
            fileDialog.Filter = "Text File | *.txt";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Autoplayer.SaveSong(fileDialog.FileName);
                MessageBox.Show($"Notes saved to {fileDialog.FileName}");
            }
        }

        /// <summary>
        /// This is called when we change the state of the loop checkbox
        /// </summary>
        private void LoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Autoplayer.Loop = LoopCheckBox.Checked;
        }

        /// <summary>
        /// This is called when the text in StartKeyTextBox is changed
        /// </summary>
        private void StartKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            //Using a try catch here in case the user inputs wrong key binds.
            KeysConverter keysConverter = new KeysConverter();
            try
            {
                //Remember to reset the hooked key to the new one.
                gkh.HookedKeys.Remove(startKey);
                startKey = (Keys)keysConverter.ConvertFromString(StartKeyTextBox.Text.ToString());
                if (startKey == stopKey)
                    MessageBox.Show("This key is already bound to another action!");
                gkh.HookedKeys.Add(startKey);
                PlayButton.Text = $"Start ({StartKeyTextBox.Text.ToString()})";
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"ERROR: Invalid key {StartKeyTextBox.Text.ToString()}");
            }
        }
        /// <summary>
        /// This is called when the text in StopKeyTextBox is changed
        /// </summary>
        private void StopKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            //Using a try catch here in case the user inputs wrong key binds.
            KeysConverter keysConverter = new KeysConverter();
            try
            {
                //Remember to reset the hooked key to the new one.
                gkh.HookedKeys.Remove(stopKey);
                stopKey = (Keys)keysConverter.ConvertFromString(StopKeyTextBox.Text.ToString());
                if (stopKey == startKey)
                    MessageBox.Show("This key is already bound to another action!");
                gkh.HookedKeys.Add(stopKey);
                StopButton.Text = $"Stop ({StopKeyTextBox.Text.ToString()})";
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"ERROR: Invalid key {StopKeyTextBox.Text.ToString()}");
            }
        }

        /// <summary>
        /// This is called when the value of NormalDelayBox is changed
        /// </summary>
        private void NormalDelayBox_ValueChanged(object sender, EventArgs e)
        {
            Autoplayer.DelayAtNormalSpeed = (int)NormalDelayBox.Value;
        }
        /// <summary>
        /// This is called when the value of FastDelayBox is changed
        /// </summary>
        private void FastDelayBox_ValueChanged(object sender, EventArgs e)
        {
            Autoplayer.DelayAtFastSpeed = (int)FastDelayBox.Value;
        }

        private void NoteTextBox_TextChanged(object sender, EventArgs e)
        {
            if(!isLoading)
            {
                MakeSong();
            }
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ImportMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            //This sets the load dialog to filter on .txt files.
            fileDialog.Filter = "ABC File | *.abc";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    isLoading = true;
                    Autoplayer.ResetDelays();
                    ABCObject abcObject = ABCTool.ImportABC(fileDialog.FileName);
                    //Update everything when we are done loading
                    UpdateEverything();
                    MessageBox.Show("Import completed");
                }
                catch (ABCException error)
                {
                    isLoading = false;
                    MessageBox.Show($"Importing failed: {error.Message}");
                }
            }
        }
    }
}
