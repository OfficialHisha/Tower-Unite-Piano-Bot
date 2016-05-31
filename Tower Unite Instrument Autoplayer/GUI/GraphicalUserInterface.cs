using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//This is how to tell the form application to use the core
//You will need to use this if you want to make your own GUI
//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
using Tower_Unite_Instrument_Autoplayer.Core;

namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    public partial class GraphicalUserInterface : Form
    {
        public GraphicalUserInterface()
        {
            InitializeComponent();
            VersionLabel.Text = Program.Version;
            NoteTextBox.Leave += DisablePlayButton;
            NoteTextBox.Leave += MakeSong;
            Program.AddingNoteFinished += EnablePlayButton;
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
            foreach (Delay delay in Program.Delays)
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
            foreach (INote note in Program.Song)
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
                    NoteTextBox.Text += ((Note)note).Key.ToString();
                }
                else if (note is MultiNote)
                {
                    NoteTextBox.Text += "[";
                    foreach (Note multiNote in ((MultiNote)note).Notes)
                    {
                        NoteTextBox.Text += multiNote.Key.ToString();
                    }
                    NoteTextBox.Text += "]";
                }
            }
        }

        private void MakeSong(object sender, EventArgs e)
        {
            Program.AddNotesFromString(NoteTextBox.Text);
        }

        private void DisablePlayButton(object sender, EventArgs e)
        {
            PlayButton.Enabled = false;
        }
        private void EnablePlayButton()
        {
            PlayButton.Enabled = true;
        }

        private void AddDelayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(Program.CheckDelayExists(CustomDelayCharacterBox.Text.ToCharArray()[0]))
                {
                    Program.ChangeDelay(CustomDelayCharacterBox.Text.ToCharArray()[0], (int)CustomDelayTimeBox.Value);
                }
                else
                {
                    Program.AddDelay(CustomDelayCharacterBox.Text.ToCharArray()[0], (int)CustomDelayTimeBox.Value);
                }
                UpdateDelayListBox();
            }
            catch (AutoplayerCustomDelayException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void RemoveDelayButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.RemoveDelay(CustomDelayCharacterBox.Text.ToCharArray()[0]);
                UpdateDelayListBox();
            }
            catch (AutoplayerCustomDelayException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void ClearNotesButton_Click(object sender, EventArgs e)
        {
            Program.ClearAllNotes();
            UpdateNoteBox();
        }
        private void PlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Played");
            Program.PlaySong();
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            Program.StopSong();
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            //This sets the load dialog to filter on .txt files.
            fileDialog.Filter = "Text File | *.txt";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Program.LoadSong(fileDialog.FileName);
                    UpdateEverything();
                    MessageBox.Show("Loading completed");
                }
                catch (AutoplayerLoadFailedException error)
                {
                    MessageBox.Show($"Loading failed: {error.Message}");
                }
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            //This sets the save dialog to filter on .txt files.
            fileDialog.Filter = "Text File | *.txt";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.SaveSong(fileDialog.FileName);
                MessageBox.Show($"Notes saved as {fileDialog.FileName}.txt");
            }
        }
    }
}
