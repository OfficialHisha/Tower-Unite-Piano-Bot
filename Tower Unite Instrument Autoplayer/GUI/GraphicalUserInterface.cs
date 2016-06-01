﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
//This is how to tell the form application to use the core
//You will need to use this if you want to make your own GUI
//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
using Tower_Unite_Instrument_Autoplayer.Core;
using Utilities;

namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    public partial class GraphicalUserInterface : Form
    {
        //Here we create a thread that will be used to play the song
        //This way the program will not freeze while playing
        Thread songThread;

        //This is the hook for the key bindings, until I find a better solution
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();

        //The key bind to start playing
        Keys startKey;
        //The key bind to stop playing
        Keys stopKey;

        /// <summary>
        /// This is the constructor for the GUI
        /// It is called when the GUI starts
        /// </summary>
        public GraphicalUserInterface()
        {
            InitializeComponent();
            VersionLabel.Text = Autoplayer.Version;
            Autoplayer.AddingNoteFinished += EnablePlayButton;
            Autoplayer.SongFinishedPlaying += EnableClearButton;
            Autoplayer.SongWasStopped += EnableClearButton;
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

            //Activate the input interceptor that will send the keys
            Autoplayer.InterceptorInput.KeyboardFilterMode = Interceptor.KeyboardFilterMode.All;
            Autoplayer.InterceptorInput.Load();
        }

        /// <summary>
        /// This method handles the key bind presses
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
        /// TODO: Add comment
        /// </summary>
        private void MakeSong(object sender, EventArgs e)
        {
            Autoplayer.AddNotesFromString(NoteTextBox.Text);
        }
        
        /// <summary>
        /// TODO: Add comment
        /// </summary>
        private void DisablePlayButton()
        {
            PlayButton.Enabled = false;
        }
        /// <summary>
        /// TODO: Add comment
        /// </summary>
        private void EnablePlayButton()
        {
            PlayButton.Enabled = true;
        }
        
        /// <summary>
        /// TODO: Add comment
        /// </summary>
        private void DisableClearButton()
        {
            ClearNotesButton.Enabled = false;
        }
        /// <summary>
        /// TODO: Add comment
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
        /// TODO: Add comment
        /// </summary>
        private void ExceptionHandler(AutoplayerException exception)
        {
            MessageBox.Show(exception.Message);
        }

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
                Autoplayer.RemoveDelay(CustomDelayCharacterBox.Text.ToCharArray()[0]);
                UpdateDelayListBox();
            }
            catch (AutoplayerCustomDelayException error)
            {
                MessageBox.Show(error.Message);
            }
        }
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
            //Start the song in a new thread so we can do other things in
            //the program when the song is playing. Stopping the song for example
            songThread = new Thread(Autoplayer.PlaySong);
            songThread.Start();
        }
        /// <summary>
        /// This is called when we click the StopButton
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            Autoplayer.StopSong();
            //Close the thread and enable the ClearNotesButton
            songThread.Abort();
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
                    Autoplayer.LoadSong(fileDialog.FileName);
                    //Update everything when we are done loading
                    UpdateEverything();
                    MessageBox.Show("Loading completed");
                }
                catch (AutoplayerLoadFailedException error)
                {
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
                MessageBox.Show($"Notes saved at {fileDialog.FileName}");
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
            DisablePlayButton();
            Autoplayer.AddNotesFromString(NoteTextBox.Text);
        }
    }
}