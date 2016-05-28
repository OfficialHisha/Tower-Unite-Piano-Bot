using System;
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

        public string song = "{123456789QWERTYUIOPASDFGHJLZXCVBNM}";

        public int normalDelay = 200;
        public int fastDelay = 100;

        public bool stop = true;

        public Form1()
        {
            InitializeComponent();
            SongTextBox.Text = song;
            NormalDelayBox.Value = normalDelay;
            FastDelayBox.Value = fastDelay;
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
                            SendKeys.SendWait(multiPressNotes);
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

                    SendKeys.SendWait(note.ToString());
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
    }
}
