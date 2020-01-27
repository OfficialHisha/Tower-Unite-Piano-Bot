using System;
using System.Windows.Forms;
using Tower_Unite_Instrument_Autoplayer.GUI;

namespace Tower_Unite_Instrument_Autoplayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GraphicalUserInterface());
        }
    }
}
