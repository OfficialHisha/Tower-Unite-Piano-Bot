using System.Windows.Forms;
namespace Tower_Unite_Piano_Bot
{
    class CustomDelay
    {
        public CheckBox EnabledBox { get; private set; }
        public TextBox CharacterBox { get; private set; }
        public NumericUpDown DelayBox { get; private set; }

        public CustomDelay(CheckBox checkBox, TextBox textBox, NumericUpDown numberBox)
        {
            EnabledBox = checkBox;
            CharacterBox = textBox;
            DelayBox = numberBox;
        }
    }
}
