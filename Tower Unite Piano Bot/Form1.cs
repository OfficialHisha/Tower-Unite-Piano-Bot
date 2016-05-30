using System;
using System.Collections.Generic;
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

        List<CustomDelay> customDelayOptions = new List<CustomDelay>();
        List<CustomDelay> activeCustomDelays = new List<CustomDelay>();

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

            #region Initialize custom delay options
            customDelayOptions.Add(new CustomDelay(DelayCheckBox0, DelayCharacter0, DelayTime0, 0));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox1, DelayCharacter1, DelayTime1, 1));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox2, DelayCharacter2, DelayTime2, 2));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox3, DelayCharacter3, DelayTime3, 3));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox4, DelayCharacter4, DelayTime4, 4));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox5, DelayCharacter5, DelayTime5, 5));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox6, DelayCharacter6, DelayTime6, 6));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox7, DelayCharacter7, DelayTime7, 7));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox8, DelayCharacter8, DelayTime8, 8));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox9, DelayCharacter9, DelayTime9, 9));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox10, DelayCharacter10, DelayTime10, 10));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox11, DelayCharacter11, DelayTime11, 11));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox12, DelayCharacter12, DelayTime12, 12));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox13, DelayCharacter13, DelayTime13, 13));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox14, DelayCharacter14, DelayTime14, 14));
            customDelayOptions.Add(new CustomDelay(DelayCheckBox15, DelayCharacter15, DelayTime15, 15));
            #endregion
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

            //p = Process.GetProcessesByName("notepad").FirstOrDefault();//For development testing.
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

                string firstLine = sr.ReadLine();

                if (firstLine == "CUSTOM DELAYS")
                {
                    int customDelayAmount = 0;
                    if (int.TryParse(sr.ReadLine(), out customDelayAmount))
                    {
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
                                        if (customDelayAmount != 0)
                                        {
                                            for (int i = 0; i < customDelayAmount; i++)
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
                                                                        CustomDelay customDelay = customDelayOptions.Find(x => x.Index == customDelayIndex);
                                                                        if(customDelay != null)
                                                                        {
                                                                            customDelay.EnabledBox.Checked = true;
                                                                            customDelay.CharacterBox.Text.ToCharArray()[0] = customDelayChar;
                                                                            customDelay.DelayBox.Value = customDelayTime;
                                                                            activeCustomDelays.Add(customDelay);
                                                                        }
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
                                            string tempReadNotes = sr.ReadToEnd();
                                            if (!string.IsNullOrWhiteSpace(tempReadNotes))
                                            {
                                                SongTextBox.Text = tempReadNotes;
                                                sr.Close();
                                                return;
                                            }
                                        }
                                        MessageBox.Show("Load Failed");
                                        sr.Close();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                //Legacy load (Saved for backwards combatibility)
                else if (firstLine == "NORMAL DELAY")
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
                sw.WriteLine("CUSTOM DELAYS");
                sw.WriteLine(activeCustomDelays.Count);
                sw.WriteLine("NORMAL DELAY");
                sw.WriteLine(normalDelay.ToString());
                sw.WriteLine("FAST DELAY");
                sw.WriteLine(fastDelay.ToString());
                if (activeCustomDelays.Count != 0)
                {
                    foreach (CustomDelay delay in activeCustomDelays)
                    {
                        sw.WriteLine("CUSTOM DELAY INDEX");
                        sw.WriteLine(delay.Index);
                        sw.WriteLine("CUSTOM DELAY CHARACTER");
                        sw.WriteLine(delay.CharacterBox.Text);
                        sw.WriteLine("CUSTOM DELAY TIME");
                        sw.WriteLine(delay.DelayBox.Value);
                    }
                }
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

                #region Pauses                
                //Normal pause
                if (note == ' ')
                {
                    Thread.Sleep(normalDelay);
                    continue;
                }

                //Custom pauses
                if(activeCustomDelays.Count != 0)
                {
                    CustomDelay customDelay = activeCustomDelays.Find(x => x.CharacterBox.Text.ToCharArray()[0] == note);
                    if(customDelay != null)
                    {
                        Thread.Sleep((int)customDelay.DelayBox.Value);
                        continue;
                    }
                }
                #endregion
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
                            if(currentNote == ' ')
                            {
                                 SendKeys.SendWait(" ");
                                continue;
                            }
                            else
                            {
                                SendKeys.SendWait("{" + currentNote.ToString() + "}");
                                continue;
                            }
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

        #region Custom delays
        private void DelayCheckBox0_CheckedChanged(object sender, EventArgs e)
        {
            if(!DelayCheckBox0.Checked)
            {
                CustomDelayCharLabel0.Enabled = false;
                DelayCharacter0.Enabled = false;
                CustomDelayDelayLabel0.Enabled = false;
                DelayTime0.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 0));
            }
            else
            {
                CustomDelayCharLabel0.Enabled = true;
                DelayCharacter0.Enabled = true;
                CustomDelayDelayLabel0.Enabled = true;
                DelayTime0.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 0));
            }
        }
        private void DelayCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox1.Checked)
            {
                CustomDelayCharLabel1.Enabled = false;
                DelayCharacter1.Enabled = false;
                CustomDelayDelayLabel1.Enabled = false;
                DelayTime1.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 1));
            }
            else
            {
                CustomDelayCharLabel1.Enabled = true;
                DelayCharacter1.Enabled = true;
                CustomDelayDelayLabel1.Enabled = true;
                DelayTime1.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 1));
            }
        }
        private void DelayCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox2.Checked)
            {
                CustomDelayCharLabel2.Enabled = false;
                DelayCharacter2.Enabled = false;
                CustomDelayDelayLabel2.Enabled = false;
                DelayTime2.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 2));
            }
            else
            {
                CustomDelayCharLabel2.Enabled = true;
                DelayCharacter2.Enabled = true;
                CustomDelayDelayLabel2.Enabled = true;
                DelayTime2.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 2));
            }
        }
        private void DelayCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox3.Checked)
            {
                CustomDelayCharLabel3.Enabled = false;
                DelayCharacter3.Enabled = false;
                CustomDelayDelayLabel3.Enabled = false;
                DelayTime3.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 3));
            }
            else
            {
                CustomDelayCharLabel3.Enabled = true;
                DelayCharacter3.Enabled = true;
                CustomDelayDelayLabel3.Enabled = true;
                DelayTime3.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 3));
            }
        }
        private void DelayCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox4.Checked)
            {
                CustomDelayCharLabel4.Enabled = false;
                DelayCharacter4.Enabled = false;
                CustomDelayDelayLabel4.Enabled = false;
                DelayTime4.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 4));
            }
            else
            {
                CustomDelayCharLabel4.Enabled = true;
                DelayCharacter4.Enabled = true;
                CustomDelayDelayLabel4.Enabled = true;
                DelayTime4.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 4));
            }
        }
        private void DelayCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox5.Checked)
            {
                CustomDelayCharLabel5.Enabled = false;
                DelayCharacter5.Enabled = false;
                CustomDelayDelayLabel5.Enabled = false;
                DelayTime5.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 5));
            }
            else
            {
                CustomDelayCharLabel5.Enabled = true;
                DelayCharacter5.Enabled = true;
                CustomDelayDelayLabel5.Enabled = true;
                DelayTime5.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 5));
            }
        }
        private void DelayCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox6.Checked)
            {
                CustomDelayCharLabel6.Enabled = false;
                DelayCharacter6.Enabled = false;
                CustomDelayDelayLabel6.Enabled = false;
                DelayTime6.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 6));
            }
            else
            {
                CustomDelayCharLabel6.Enabled = true;
                DelayCharacter6.Enabled = true;
                CustomDelayDelayLabel6.Enabled = true;
                DelayTime6.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 6));
            }
        }
        private void DelayCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox7.Checked)
            {
                CustomDelayCharLabel7.Enabled = false;
                DelayCharacter7.Enabled = false;
                CustomDelayDelayLabel7.Enabled = false;
                DelayTime7.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 7));
            }
            else
            {
                CustomDelayCharLabel7.Enabled = true;
                DelayCharacter7.Enabled = true;
                CustomDelayDelayLabel7.Enabled = true;
                DelayTime7.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 7));
            }
        }
        private void DelayCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox8.Checked)
            {
                CustomDelayCharLabel8.Enabled = false;
                DelayCharacter8.Enabled = false;
                CustomDelayDelayLabel8.Enabled = false;
                DelayTime8.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 8));
            }
            else
            {
                CustomDelayCharLabel8.Enabled = true;
                DelayCharacter8.Enabled = true;
                CustomDelayDelayLabel8.Enabled = true;
                DelayTime8.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 8));
            }
        }
        private void DelayCheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox9.Checked)
            {
                CustomDelayCharLabel9.Enabled = false;
                DelayCharacter9.Enabled = false;
                CustomDelayDelayLabel9.Enabled = false;
                DelayTime9.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 9));
            }
            else
            {
                CustomDelayCharLabel9.Enabled = true;
                DelayCharacter9.Enabled = true;
                CustomDelayDelayLabel9.Enabled = true;
                DelayTime9.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 9));
            }
        }
        private void DelayCheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox10.Checked)
            {
                CustomDelayCharLabel10.Enabled = false;
                DelayCharacter10.Enabled = false;
                CustomDelayDelayLabel10.Enabled = false;
                DelayTime10.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 10));
            }
            else
            {
                CustomDelayCharLabel10.Enabled = true;
                DelayCharacter10.Enabled = true;
                CustomDelayDelayLabel10.Enabled = true;
                DelayTime10.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 10));
            }
        }
        private void DelayCheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox11.Checked)
            {
                CustomDelayCharLabel11.Enabled = false;
                DelayCharacter11.Enabled = false;
                CustomDelayDelayLabel11.Enabled = false;
                DelayTime11.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 11));
            }
            else
            {
                CustomDelayCharLabel11.Enabled = true;
                DelayCharacter11.Enabled = true;
                CustomDelayDelayLabel11.Enabled = true;
                DelayTime11.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 11));
            }
        }
        private void DelayCheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox12.Checked)
            {
                CustomDelayCharLabel12.Enabled = false;
                DelayCharacter12.Enabled = false;
                CustomDelayDelayLabel12.Enabled = false;
                DelayTime12.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 12));
            }
            else
            {
                CustomDelayCharLabel12.Enabled = true;
                DelayCharacter12.Enabled = true;
                CustomDelayDelayLabel12.Enabled = true;
                DelayTime12.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 12));
            }
        }
        private void DelayCheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox13.Checked)
            {
                CustomDelayCharLabel13.Enabled = false;
                DelayCharacter13.Enabled = false;
                CustomDelayDelayLabel13.Enabled = false;
                DelayTime13.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 13));
            }
            else
            {
                CustomDelayCharLabel13.Enabled = true;
                DelayCharacter13.Enabled = true;
                CustomDelayDelayLabel13.Enabled = true;
                DelayTime13.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 13));
            }
        }
        private void DelayCheckBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox14.Checked)
            {
                CustomDelayCharLabel14.Enabled = false;
                DelayCharacter14.Enabled = false;
                CustomDelayDelayLabel14.Enabled = false;
                DelayTime14.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 14));
            }
            else
            {
                CustomDelayCharLabel14.Enabled = true;
                DelayCharacter14.Enabled = true;
                CustomDelayDelayLabel14.Enabled = true;
                DelayTime14.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 14));
            }
        }
        private void DelayCheckBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (!DelayCheckBox15.Checked)
            {
                CustomDelayCharLabel15.Enabled = false;
                DelayCharacter15.Enabled = false;
                CustomDelayDelayLabel15.Enabled = false;
                DelayTime15.Enabled = false;
                activeCustomDelays.Remove(activeCustomDelays.Find(x => x.Index == 15));
            }
            else
            {
                CustomDelayCharLabel15.Enabled = true;
                DelayCharacter15.Enabled = true;
                CustomDelayDelayLabel15.Enabled = true;
                DelayTime15.Enabled = true;
                activeCustomDelays.Add(customDelayOptions.Find(x => x.Index == 15));
            }
        }
        #endregion
    }
}
