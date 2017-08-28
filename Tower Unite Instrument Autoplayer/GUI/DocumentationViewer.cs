using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    public partial class DocumentationViewer : Form
    {
        public DocumentationViewer()
        {
            InitializeComponent();
            
            foreach (string item in Properties.Resources._include.Replace("\r", string.Empty).Replace("\n", string.Empty).Split(','))
            {
                TopicListBox.Items.Add(item);
            }
        }

        private void TopicListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string infoText = (string)Properties.Resources.ResourceManager.GetObject((string)TopicListBox.SelectedItem);


            if (infoText != "")
            {
                InfoTextBox.Text = infoText;
            }
            else
            {
                InfoTextBox.Text = "There is currently no documentation available for this feature";
            }
        }
    }
}
