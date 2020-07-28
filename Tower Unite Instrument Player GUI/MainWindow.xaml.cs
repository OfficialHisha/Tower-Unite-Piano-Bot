using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tower_Unite_Instrument_Player;
using Tower_Unite_Instrument_Player.Exceptions;
using Tower_Unite_Instrument_Player.Notes;
using Tower_Unite_Instrument_Player_GUI.Band;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DrWPF.Windows.Data;
using NHotkey;
using NHotkey.Wpf;

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
        INote[] song;
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

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
        ObservableDictionary<char, int> p_delays = new ObservableDictionary<char, int>();

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
            get => p_delays;
            set
            {
                if (p_delays.Equals(value)) return;

                p_delays = value;

                OnPropertyChanged("Delays");
            }
        }

        private KeyValuePair<string, string> p_selectedMusicPiece;

        public KeyValuePair<string, string> SelectedMusicPiece
        {
            get => p_selectedMusicPiece;
            set
            {
                if (p_selectedMusicPiece.Equals(value)) return;
                p_selectedMusicPiece = value;
                HasMusicPieceSelected = !string.IsNullOrEmpty(p_selectedMusicPiece.Key);
                OnPropertyChanged("SelectedMusicPiece");
            }
        }

        private bool p_hasMusicPieceSelected;

        public bool HasMusicPieceSelected
        {
            get => p_hasMusicPieceSelected;
            set
            {
                if (p_hasMusicPieceSelected == value) return;
                p_hasMusicPieceSelected = value;
                OnPropertyChanged("HasMusicPieceSelected");
         
            }
        }

        private string p_musicPieceNotes = "Notes";

        public string MusicPieceNotes
        {
            get => p_musicPieceNotes;
            set
            {
                if (p_musicPieceNotes == value) return;
                p_musicPieceNotes = value;
                OnPropertyChanged("MusicPieceNotes");
            }
        }

        private ObservableDictionary<string, string> p_musicPieces = new ObservableDictionary<string, string>();
        public ObservableDictionary<string, string> MusicPieces
        {
            get => p_musicPieces;
            set
            {
                if (p_musicPieces.Equals(value)) return;

                p_musicPieces = value;
                OnPropertyChanged("MusicPieces");
            }
        }

        public string NoteViewText
        {
            get => p_noteViewText;
            set
            {
                if (p_noteViewText == value) return;
                p_noteViewText = value;

                CompileSong();

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

                try
                {
                    NoteParser parser = new NoteParser();
                    song = parser.ParseNotes(NoteViewText, new Dictionary<char, int>(Delays));
                    CanPlay = true;
                }
                catch (AutoplayerException ex)
                {
                    CanPlay = false;
                    SetError(ex.Message);
                }
            };

            try
            {
                HotkeyManager.Current.AddOrReplace("Play", Key.F2, ModifierKeys.None, (sender, args) => PlaySong());
                HotkeyManager.Current.AddOrReplace("Stop", Key.F3, ModifierKeys.None, (sender, args) => _cancellationTokenSource.Cancel());
                HotkeyManager.Current.AddOrReplace("BandPlay", Key.F4, ModifierKeys.None, (sender, args) =>
                {
                    BandServer.Broadcast("play");
                    PlaySong();
                });
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                // Hotkeys are already registered
                //Application.Current.Shutdown();
            }
            

            Application.Current.Exit += (sender, args) =>
            {
                // Ensure that we stop any song that may be playing when the application exits
                _cancellationTokenSource.Cancel();
            };

            DataContext = this;
        }

        private void CompileSong()
        {
            CanPlay = false;

            r_compileTimer.Interval = c_compileTimerCooldown;
            r_compileTimer.Start();
        }

        private async Task PlaySong()
        {
            if (!CanPlay) return;

            _cancellationTokenSource.Cancel();
            await Task.Delay(1000);
            _cancellationTokenSource = new CancellationTokenSource();
            await Autoplayer.PlaySong(song, _cancellationTokenSource.Token);
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
            _cancellationTokenSource.Cancel();
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

            if (Delays.Keys.Contains(delayChar))
                Delays[delayChar] = DelayDuration;
            else
                Delays.Add(delayChar, DelayDuration);

            CompileSong();
        }

        private void RemoveDelayButton_Click(object sender, RoutedEventArgs e)
        {
            Delays.Remove(SelectedDelay.Key);
            SelectedDelay = default;

            CompileSong();
        }

        private void AddMusicPieceButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void RemoveMusicPieceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {

            BandServer.Start(6000);
        }

        private void JoinServer_Click(object sender, RoutedEventArgs e)
        {
            BandClient.OnServerPlay += () => PlaySong();
            BandClient.Join("127.0.0.1", 6000);
        }

        private void ServerPlay_Click(object sender, RoutedEventArgs e)
        {
            BandServer.Broadcast("play");
            PlaySong();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveNotesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                //This sets the save dialog to filter on .json files
                Filter = "JSON|*.json"
            };

            if (fileDialog.ShowDialog() == true)
            {
                JObject json = new JObject
                {
                    ["delays"] = JObject.FromObject(Delays),
                    ["normal_speed"] = NormalDelay,
                    ["fast_speed"] = FastDelay,
                    ["notes"] = NoteViewText
                };

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
                try
                {
                    if (fileDialog.FileName.ToLower().EndsWith(".txt"))
                        fileDialog.FileName = ConvertSaveFormat(fileDialog.FileName);

                    dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(fileDialog.FileName));

                    Delays = JsonConvert.DeserializeObject<ObservableDictionary<char, int>>(json.delays.ToString());

                    NormalDelay = int.Parse(json.normal_speed.ToString());
                    FastDelay = int.Parse(json.fast_speed.ToString());

                    NoteViewText = json.notes.ToString();

                    MessageBox.Show("Loading completed");
                }
                catch (IOException error)
                {
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
    }
}
