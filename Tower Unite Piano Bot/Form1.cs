using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Utilities;

namespace Tower_Unite_Piano_Bot
{
    public partial class Form1 : Form
    {
        //This imports a method used in the PlayButton_Click method to choose active window.
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        #region Main
        public string song = "{123456789QWERTYUIOPASDFGHJLZXCVBNM}";
        public int normalDelay = 200;
        public int fastDelay = 100;
        public bool stop = true;
        public bool loop = false;
        Keys startKey;
        Keys stopKey;
        #endregion

        #region Save and load
        OpenFileDialog loadFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        #endregion

        GlobalKeyboardHook gkh = new GlobalKeyboardHook();

        public Form1()
        {
            InitializeComponent();
            SongTextBox.Text = song;
            NormalDelayBox.Value = normalDelay;
            FastDelayBox.Value = fastDelay;

            //This sets the save dialog to filter on .txt files.
            saveFileDialog.Filter = "Text File | *.txt";

            //This adds options for the ProcessChooserBox.
            ProcessChooserBox.Items.Add("TU");
            ProcessChooserBox.Items.Add("GMT");
            ProcessChooserBox.SelectedItem = "TU";

            //Subscribe the method "GKS_KeyDown" to the KeyDown event of the GlobalKeyboardHook.
            gkh.KeyDown += new KeyEventHandler(GKS_KeyDown);

            //This converts the text from the keybind settings window to actual keys.
            //Then we add them to the global hook (This is done so the keypresses will be detected when the application is not in focus)
            KeysConverter keysConverter = new KeysConverter();
            startKey = (Keys)keysConverter.ConvertFromString(StartKeyTextBox.Text.ToString());
            stopKey = (Keys)keysConverter.ConvertFromString(StopKeyTextBox.Text.ToString());
            gkh.HookedKeys.Add(startKey);
            gkh.HookedKeys.Add(stopKey);
            
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (!stop)
            {
                return;
            }

            Text = "Tower Unite Piano Bot - Playing";
            stop = false;
            song = SongTextBox.Text;
            normalDelay = (int)NormalDelayBox.Value;
            fastDelay = (int)FastDelayBox.Value;

            Process p;
            if ((string)ProcessChooserBox.SelectedItem == "GMT")
                p = Process.GetProcessesByName("hl2").FirstOrDefault();
            else
                p = Process.GetProcessesByName("Tower").FirstOrDefault();

            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);

                if (loop)
                {
                    while (loop)
                    {
                        if (stop)
                            return;
                        PlayFromTheStart();
                    }
                }
                else
                {
                    PlayFromTheStart();
                }


                Thread.Sleep(1000);
                Text = "Tower Unite Piano Bot";
                stop = true;
            }
            else
            {
                MessageBox.Show("ERROR: The targeted game could not be found! Please ensure that the game you are trying to target is running");
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (stop)
            {
                return;
            }
            Text = "Tower Unite Piano Bot - Stopped";
            stop = true;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (loadFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(loadFileDialog.FileName);
                if (sr.ReadLine() == "NORMAL DELAY")
                {
                    if (int.TryParse(sr.ReadLine(), out normalDelay))
                    {
                        NormalDelayBox.Value = normalDelay;
                        if (sr.ReadLine() == "FAST DELAY")
                        {
                            if (int.TryParse(sr.ReadLine(), out fastDelay))
                            {
                                FastDelayBox.Value = fastDelay;
                                if (sr.ReadLine() == "NOTES")
                                {
                                    string tempReadNotes = sr.ReadToEnd();
                                    if (!string.IsNullOrWhiteSpace(tempReadNotes))
                                    {
                                        SongTextBox.Text = tempReadNotes;
                                        sr.Close();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Load Failed");
                sr.Close();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);

                sw.WriteLine("NORMAL DELAY");
                sw.WriteLine(normalDelay.ToString());
                sw.WriteLine("FAST DELAY");
                sw.WriteLine(fastDelay.ToString());
                sw.WriteLine("NOTES");
                sw.WriteLine(SongTextBox.Text);
                sw.Dispose();
                sw.Close();
            }
        }

        private void LoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // The CheckBox control's Text property is changed each time the 
            // control is clicked, indicating a checked or unchecked state.
            if (LoopCheckBox.Checked)
            {
                loop = true;
            }
            else
            {
                loop = false;
            }
        }

        private void ProcessChooserBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ProcessChooserBox.SelectedItem == "GMT")
            {
                MessageBox.Show("WARNING! Choosing GMT will set the software to target Garry's Mod. This is completely untested and potentially dangerous!");
            }
        }

        private void StartKey_TextChanged(object sender, EventArgs e)
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

        private void StopKey_TextChanged(object sender, EventArgs e)
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

        private void PlayFromTheStart()
        {
            bool isMultiPress = false;
            string multiPressNotes = "";

            int delay = normalDelay;

            foreach (char note in song)
            {
                if (stop)
                    return;
                if (note == '\n' || note == '\r')
                    continue;

                #region Multiple notes
                if (isMultiPress)
                {
                    if (note != ']')
                    {
                        if (note == '½')
                        {
                            multiPressNotes += ' ';
                            continue;
                        }
                        else
                        {
                            multiPressNotes += note;
                            continue;
                        }
                    }
                    else
                    {
                        isMultiPress = false;
                        foreach (char currentNote in multiPressNotes)
                        {
                            if(currentNote == '½')
                            {
                                SendKeys.SendWait(" ");
                                continue;
                            }
                            SendKeys.SendWait("{" + currentNote.ToString() + "}");
                        }
                        multiPressNotes = "";
                        Thread.Sleep(delay);
                        continue;
                    }
                }
                else if (note == '[')
                {
                    isMultiPress = true;
                    continue;
                }
                #endregion
                #region Really fast
                if (note == '{')
                {
                    delay = fastDelay;
                    continue;
                }
                else if (note == '}')
                {
                    delay = normalDelay;
                    continue;
                }
                #endregion

                if (note == '½')
                {
                    SendKeys.SendWait(" ");
                    continue;
                }

                    //Normal pause
                    if (note == ' ')
                {
                    Thread.Sleep(normalDelay);
                    continue;
                }
                SendKeys.SendWait("{" + note.ToString() + "}");
                Thread.Sleep(delay);
            }
        }

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
    }
}
