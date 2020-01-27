using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tower_Unite_Instrument_Player;
using Tower_Unite_Instrument_Player.Exceptions;

namespace Tower_Unite_Instrument_Player_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        bool _loop = false;
        bool _isLoading = false;
        Brush _defaultButtonBrush;

        double p_normalDelay = 200;
        double p_fastDelay = 100;
        bool p_hasError = false;
        string p_errorMessage = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasError
        {
            get => p_hasError;
            set
            {
                if (p_hasError == value) return;
                p_hasError = value;
                OnPropertyChanged("HasError");
            }
        }
        public string ErrorMessage
        { 
            get => p_errorMessage;
            set
            {
                if (p_errorMessage == value) return;
                p_errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public double NormalDelay
        {
            get => p_normalDelay;
            set
            {
                if (p_normalDelay == value) return;
                p_normalDelay = value;
                OnPropertyChanged("NormalDelay");
            }
        }

        public double FastDelay
        {
            get => p_fastDelay;
            set
            {
                if (p_fastDelay == value) return;
                p_fastDelay = value;
                OnPropertyChanged("FastDelay");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            VersionText.Text = Autoplayer.Version;
            _defaultButtonBrush = LoopButton.Background;

            DataContext = this;
        }

        private void SetError(string errorMsg)
        {
            ErrorMessage = $"ERROR: {errorMsg}";
            HasError = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _loop = !_loop;
            LoopButton.Background = _loop ? Brushes.Green : _defaultButtonBrush;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveNotesButton_Click(object sender, RoutedEventArgs e)
        {
            SetError("This is error!");
        }

        private void LoadNotesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                //This sets the load dialog to filter on .txt files.
                Filter = "Text File | *.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    _isLoading = true;
                    Autoplayer.ResetBreaks();
                    Autoplayer.LoadSong(fileDialog.FileName);

                    //Update everything when we are done loading
                    UpdateEverything();
                    _isLoading = false;
                    MessageBox.Show("Loading completed");
                }
                catch (AutoplayerLoadFailedException error)
                {
                    _isLoading = false;
                    MessageBox.Show($"Loading failed: {error.Message}");
                }
            }
        }

        private void UpdateEverything()
        {
            NormalDelay = Autoplayer.NormalSpeed;
            FastDelay = Autoplayer.FastSpeed;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
