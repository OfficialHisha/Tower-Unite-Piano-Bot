using System;
using System.Windows.Forms;
using Tower_Unite_Piano_Bot.Exceptions;

namespace Tower_Unite_Piano_Bot
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(BotException e)
            {
                MessageBox.Show($"ERROR: {e.Message}");
            }
        }
    }
}
