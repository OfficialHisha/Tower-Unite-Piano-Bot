using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using NHotkey.Wpf;
using DrWPF.Windows.Data;

namespace Tower_Unite_Instrument_Player_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const int c_msPerMinute = 60000;
        const int c_compileTimerCooldown = 2000;

        bool _loop = false;
        Brush _defaultButtonBrush;
        Thread _songThread;

        int p_normalDelay = 200;
        int p_fastDelay = 100;
        int p_delayDuration;
        bool p_hasError = false;
        bool p_canPlay = false;
        bool p_hasDelaySelected = false;
        bool p_canAddDelay = false;
        string p_errorMessage = "";
        string p_noteViewText = "Load a song or input notes here..";
        string p_delayCharacter;
        KeyValuePair<char, int> p_selectedDelay;
        ObservableDictionary<char, int> p_Delays = new ObservableDictionary<char, int>();

        public event PropertyChangedEventHandler PropertyChanged;

        readonly System.Timers.Timer r_compileTimer = new System.Timers.Timer();

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

        public int NormalDelay
        {
            get => p_normalDelay;
            set
            {
                if (p_normalDelay == value) return;
                p_normalDelay = value;
                Autoplayer.NormalSpeed = BpmToMs(value);
                OnPropertyChanged("NormalDelay");
            }
        }

        public int FastDelay
        {
            get => p_fastDelay;
            set
            {
                if (p_fastDelay == value) return;
                p_fastDelay = value;
                Autoplayer.FastSpeed = BpmToMs(value);
                OnPropertyChanged("FastDelay");
            }
        }

        public KeyValuePair<char, int> SelectedDelay
        {
            get => p_selectedDelay;
            set
            {
                if (p_selectedDelay.Equals(value)) return;
                p_selectedDelay = value;
                HasDelaySelected = p_selectedDelay.Key != '\0';
                OnPropertyChanged("SelectedDelay");
            }
        }

        public bool HasDelaySelected
        {
            get => p_hasDelaySelected;
            set
            {
                if (p_hasDelaySelected == value) return;
                p_hasDelaySelected = value;
                OnPropertyChanged("HasDelaySelected");
            }
        }

        public bool CanAddDelay
        {
            get => p_canAddDelay;
            set
            {
                if (p_canAddDelay == value) return;
                p_canAddDelay = value;
                OnPropertyChanged("CanAddDelay");
            }
        }

        public string DelayCharacter
        {
            get => p_delayCharacter;
            set
            {
                if (p_delayCharacter == value) return;
                p_delayCharacter = value;
                CanAddDelay = !string.IsNullOrWhiteSpace(p_delayCharacter) && DelayDuration > 0;
                OnPropertyChanged("DelayCharacter");
            }
        }

        public int DelayDuration
        {
            get => p_delayDuration;
            set
            {
                if (p_delayDuration == value) return;
                p_delayDuration = value;
                CanAddDelay = p_delayDuration > 0 && !string.IsNullOrWhiteSpace(DelayCharacter);
                OnPropertyChanged("DelayDuration");
            }
        }

        public ObservableDictionary<char, int> Delays
        {
            get => p_Delays;
            set
            {
                if (p_Delays.Equals(value)) return;

                p_Delays = value;
                OnPropertyChanged("Delays");
            }
        }


        public string NoteViewText
        {
            get => p_noteViewText;
            set
            {
                if (p_noteViewText == value) return;
                CanPlay = false;
                p_noteViewText = value;

                r_compileTimer.Interval = c_compileTimerCooldown;
                r_compileTimer.Start();

                OnPropertyChanged("NoteViewText");
            }
        }

        public bool CanPlay
        {
            get => p_canPlay;
            set
            {
                if (p_canPlay == value) return;
                p_canPlay = value;
                OnPropertyChanged("CanPlay");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            VersionText.Text = $"Version: {Autoplayer.Version}";
            Autoplayer.SongWasInteruptedByException += ExceptionHandler;
            _defaultButtonBrush = LoopButton.Background;

            r_compileTimer.AutoReset = false;
            r_compileTimer.Elapsed += (timer, args) =>
            {
                ResetError();

                if (string.IsNullOrEmpty(NoteViewText))
                    return;

                Autoplayer.AddNotesFromString(NoteViewText);
                CanPlay = !HasError;
            };

            HotkeyManager.Current.AddOrReplace("Play", Key.F2, ModifierKeys.None, (sender, args) => PlaySong());
            HotkeyManager.Current.AddOrReplace("Stop", Key.F3, ModifierKeys.None, (sender, args) => Autoplayer.StopSong());

            Application.Current.Exit += (sender, args) =>
            {
                Autoplayer.StopSong();// Ensure that we stop any song that may be playing when the application exits
            };

            DataContext = this;
        }

        private void PlaySong()
        {
            if (CanPlay)
            {
                Autoplayer.StopSong();// Stop the song in case a song might still be playing
                Thread.Sleep(1000);// wait a second to ensure it has finished execution
                _songThread = new Thread(Autoplayer.PlaySong);// Start a new song in a new thread
                _songThread.Start();
            }
        }

        private void ExceptionHandler(AutoplayerException exception)
        {
            SetError(exception.Message);
        }

        private void ResetError()
        {
            ErrorMessage = "";
            HasError = false;
        }

        private void SetError(string errorMsg)
        {
            ErrorMessage = $"ERROR: {errorMsg}";
            HasError = true;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlaySong();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Autoplayer.StopSong();
        }

        private void LoopButton_Click(object sender, RoutedEventArgs e)
        {
            _loop = !_loop;
            LoopButton.Background = _loop ? Brushes.Green : _defaultButtonBrush;
            Autoplayer.Loop = _loop;
        }

        private void ClearNotesButton_Click(object sender, RoutedEventArgs e)
        {
            NoteViewText = "";
        }

        private void AddDelayButton_Click(object sender, RoutedEventArgs e)
        {
            char delayChar = DelayCharacter[0];

            if (Autoplayer.Breaks.ContainsKey(delayChar))
                Autoplayer.ChangeBreak(delayChar, DelayDuration);
            else
                Autoplayer.AddBreak(delayChar, DelayDuration);

            Delays = new ObservableDictionary<char, int>(Autoplayer.Breaks);
        }

        private void RemoveDelayButton_Click(object sender, RoutedEventArgs e)
        {
            Autoplayer.RemoveBreak(SelectedDelay.Key);

            if (Autoplayer.Breaks.Count == 0)
            {
                Delays.Clear();
                return;
            }

            Delays = new ObservableDictionary<char, int>(Autoplayer.Breaks);
            SelectedDelay = default;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveNotesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            //This sets the save dialog to filter on .json files
            fileDialog.Filter = "JSON|*.json";

            if (fileDialog.ShowDialog() == true)
            {
                JObject json = new JObject();
                json["delays"] = JObject.FromObject(new object());
                json["normal_speed"] = NormalDelay;
                json["fast_speed"] = FastDelay;
                json["notes"] = NoteViewText;

                File.WriteAllText(fileDialog.FileName, json.ToString());

                MessageBox.Show($"Notes saved to '{fileDialog.FileName}'");
            }
        }

        private void LoadNotesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                //This sets the load dialog to filter on .json and .txt files
                //.txt files will be converted to .json files when loaded for backwards compatibility
                Filter = "JSON|*.json|Text File|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                bool fallbackCanPlay = CanPlay;
                try
                {
                    CanPlay = false;

                    if (fileDialog.FileName.ToLower().EndsWith(".txt"))
                        fileDialog.FileName = ConvertSaveFormat(fileDialog.FileName);

                    Autoplayer.ResetBreaks();

                    dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(fileDialog.FileName));

                    foreach (JObject delay in json.delays)
                    {
                        Delays.Add(char.Parse(delay[0].ToString()), int.Parse(delay[1].ToString()));
                    }

                    NormalDelay = int.Parse(json.normal_speed.ToString());
                    FastDelay = int.Parse(json.fast_speed.ToString());

                    NoteViewText = json.notes.ToString();
                    CanPlay = true;

                    MessageBox.Show("Loading completed");
                }
                catch (IOException error)
                {
                    CanPlay = fallbackCanPlay;
                    MessageBox.Show($"Loading failed: {error.Message}");
                }
                
            }
        }

        private string ConvertSaveFormat(string filePath)
        {
            JObject json = new JObject();

            using (StreamReader sr = new StreamReader(filePath))
            {
                if (sr.ReadLine() == "DELAYS")
                {
                    Dictionary<char, int> delayDict = new Dictionary<char, int>();

                    if (int.TryParse(sr.ReadLine(), out int delayCount) && delayCount > 0)
                    {
                        for (int i = 0; i < delayCount; i++)
                        {
                            if (char.TryParse(sr.ReadLine(), out char delayChar))
                            {
                                if (int.TryParse(sr.ReadLine(), out int delayTime))
                                {
                                    delayDict.Add(delayChar, delayTime);
                                }
                            }
                        }
                    }

                    json["delays"] = JObject.FromObject(delayDict);
                }
                if (sr.ReadLine() == "SPEEDS")
                {
                    int.TryParse(sr.ReadLine(), out int normalSpeed);
                    int.TryParse(sr.ReadLine(), out int fastSpeed);
                    json["normal_speed"] = normalSpeed;
                    json["fast_speed"] = fastSpeed;
                }
                if (sr.ReadLine() == "NOTES")
                {
                    if (int.TryParse(sr.ReadLine(), out int noteCount) && noteCount > 0)
                        json["notes"] = sr.ReadToEnd();
                }
            }

            string jsonPath = filePath.Replace(".txt", ".json");

            try
            {
                File.WriteAllText(jsonPath, json.ToString());
                return jsonPath;
            }
            catch (IOException e)
            {
                MessageBox.Show($"Failed to write json file: {e.Message}");
                return filePath;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        int BpmToMs(int bpm)
        {
            return c_msPerMinute / bpm;
        }

        private void AddMusicPieceButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RemoveMusicPieceButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
