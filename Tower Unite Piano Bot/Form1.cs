using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Tower_Unite_Piano_Bot.Exceptions;

namespace Tower_Unite_Piano_Bot
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        #region Main
        public string song = "{123456789QWERTYUIOPASDFGHJLZXCVBNM}";
        public int normalDelay = 200;
        public int fastDelay = 100;
        public bool stop = true;
        #endregion

        #region Save and load
        OpenFileDialog loadFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        #endregion

        public Form1()
        {
            InitializeComponent();
            SongTextBox.Text = song;
            NormalDelayBox.Value = normalDelay;
            FastDelayBox.Value = fastDelay;
            saveFileDialog.Filter = "Text File | *.txt";
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

            Process p = Process.GetProcessesByName("Tower").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);

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
                            multiPressNotes += note;
                            continue;
                        }
                        else
                        {
                            isMultiPress = false;
                            foreach (char currentNote in multiPressNotes)
                            {
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

                    //Normal pause
                    if (note == ' ')
                    {
                        Thread.Sleep(normalDelay);
                        continue;
                    }

                    SendKeys.SendWait("{" + note.ToString() + "}");
                    Thread.Sleep(delay);
                }
                Thread.Sleep(1000);
                Text = "Tower Unite Piano Bot";
                stop = true;
            }
            else
            {
                throw new ProcessNotFoundException("The process could not be found!");
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
    }
}
