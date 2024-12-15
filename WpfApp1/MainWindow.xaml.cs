using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MaterialDesignThemes;
using MahApps;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using Repositories;
using Models;
using System.IO;
using ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainViewModel ViewModel { get; }
        private SongRepo _songRepo;
        private DispatcherTimer _timer;
        private bool _isDragging = false;
        private DispatcherTimer _timerWatch; //  Thuộc tính của bộ đếm ngược
        private int _remainingSeconds;
        private bool _isCounting;
        private string CurrentSongPath { get; set; }
        private bool isShuffleEnable;
        private bool isReplayEnable;
        private List<SongModel> songQueue;
        private int currentSongIndex;
        private Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeMediaTimer();
            ViewModel = new MainViewModel();
            ContentControl.Content = new HomePage();
            var directoryPage = new DirectoryPage();
            _timerWatch = new DispatcherTimer();
            _timerWatch.Interval = TimeSpan.FromSeconds(1);
            _timerWatch.Tick += Timer_Tick;
            _isCounting = false;
            //directoryPage.SongSelected += OnSongSelected;
            
            _songRepo = new SongRepo("Server=IDEAPAD5PRO;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True;");
            MediaPlayer.MediaOpened += (s, args) =>
            {
                if (MediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    SongProgress_Slider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }
            };

            PlaySong_Button.Visibility = Visibility.Collapsed;
        }

        private string _currentSongName;
        public string CurrentSongName
        {
            get => _currentSongName;
            set
            {
                _currentSongName = value;
                OnPropertyChanged(); // Thông báo cập nhật
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            if(_timer != null) _timer.Stop();
            if(_timerWatch != null)_timerWatch.Stop();
            this.Close();
        }

        private void MinimiseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPopup.IsOpen = !SettingsPopup.IsOpen;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            
            ContentControl.Content = new HomePage();
        }
        private void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new DirectoryPage();
        }

        private void PlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            var playlistPageViewModel = new PlaylistPageViewModel();
            var playlistPage = new PlaylistPage
            {
                DataContext = playlistPageViewModel
            };

            ContentControl.Content = playlistPage;

            // Lắng nghe sự kiện SelectedPlaylistChanged
            playlistPageViewModel.SelectedPlaylistChanged += (songs) =>
            {
                // Gán danh sách bài hát vào songsQueue
                songQueue = new List<SongModel>(songs);
                currentSongIndex = 0; // Đặt bài hát đầu tiên làm mặc định
            };
        }

        //private void MouseEnter(object sender, RoutedEventArgs e) {

        //}

        //private void SliderSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{

        //}

        //private bool isPlaying = false;

        private void PlaySong_Button_Click(object sender, RoutedEventArgs e)
        {
            
            PlaySong_Button.Visibility = Visibility.Collapsed;
            PauseSong_Button.Visibility = Visibility.Visible;
            if (MediaPlayer.Source == null)
            {
                MediaPlayer.Source = new Uri(CurrentSongPath); 
            }
            if (MediaPlayer.Position > TimeSpan.Zero)
            {
                MediaPlayer.Play(); 
            }
            else
            {
                MediaPlayer.Play();
            }
            if (_timer != null)
            {
                _timer.Start();
            }
        }


        private void PauseSong_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSongPath == null) return;
            PauseSong_Button.Visibility = Visibility.Collapsed;
            PlaySong_Button.Visibility = Visibility.Visible;
            MediaPlayer.Pause();
            if (_timer != null)
            {
                _timer.Stop();
            }
        }







        private void SongProgress_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan && SongProgress_Slider.IsMouseCaptureWithin)
            {
                MediaPlayer.Position = TimeSpan.FromSeconds(SongProgress_Slider.Value);
            }
        }


        private void InitializeMediaTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += UpdateSongProgress;
        }

        private void UpdateSongProgress(object sender, EventArgs e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                double totalSecond = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                double currentSecond = MediaPlayer.Position.TotalSeconds;
                if(totalSecond > 0)
                {
                    SongProgress_Slider.Maximum = totalSecond;
                    SongProgress_Slider.Value = currentSecond;
                }
                
            }
        }

        private TimeSpan GetMediaDuration(string songPath)
        {
            return new TimeSpan(0);
        }


        public void PlaySong(string _songPath)
        {
            if (CurrentSongPath != _songPath)
            {
                CurrentSongPath = _songPath;
                MediaPlayer.Source = new Uri(_songPath);
                MediaPlayer.Position = TimeSpan.Zero; 
            }

            MediaPlayer.Play();
            if (_timer != null)
            {
                _timer.Start();
            }
            CurrentSongName = System.IO.Path.GetFileNameWithoutExtension(_songPath);
            var song = new SongModel
            {
                songName = System.IO.Path.GetFileName(_songPath),
                songPath = _songPath,
                lastAccessDate = DateTime.Now,
                songSize = (int)new FileInfo(_songPath).Length / 1024,
                songDuration = GetMediaDuration(_songPath)
            };
            _songRepo.SaveSongToDatabase(song);
        }
        private void Replay()
        {
            
        }

        private void FocusFrame_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!_isCounting)
            {
                StartCountdown(288000); 
            }
            else
            {
                StopCountdown(); 
            }
        }

        private void StartCountdown(int seconds)
        {
            _remainingSeconds = seconds;
            _isCounting = true;
            var timeSpan = TimeSpan.FromSeconds(_remainingSeconds);
            CountdownText.Text = timeSpan.ToString(@"hh\:mm\:ss");
            _timerWatch.Start();
        }

        private void StopCountdown()
        {
            _isCounting = false;
            _timerWatch.Stop();
            CountdownText.Text = "Focus Area";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                var timeSpan = TimeSpan.FromSeconds(_remainingSeconds);
                CountdownText.Text = timeSpan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                StopCountdown();
                CountdownText.Text = "Done!";
                MessageBox.Show("Countdown completed!", "Focus Area", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Shuffle_Button_Click(object sender, RoutedEventArgs e)
        {
            isShuffleEnable = !isShuffleEnable;
            if (isShuffleEnable) MessageBox.Show("Shuffle is on", "Info", MessageBoxButton.OK);
            else MessageBox.Show("Shuffle is off", "Info", MessageBoxButton.OK);
            if (songQueue == null || !songQueue.Any())
            {
                MessageBox.Show("No songs available to shuffle.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var random = new Random();
            songQueue = songQueue.OrderBy(_ => random.Next()).ToList(); // Xáo trộn danh sách
            currentSongIndex = 0; // Reset lại bài hát đầu tiên
            //MessageBox.Show("Playlist shuffled!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Replay_Button_Click(object sender, RoutedEventArgs e)
        {
            isReplayEnable = !isReplayEnable;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NextSong_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayNextSong();
        }

        private void PrevSong_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousSong();
        }

        private void UpdateCurrentlyPlayingSong(string songName)
        {
            CurrentSongName = songName;
        }

        private void PlayNextSong()
        {
            if (songQueue == null || !songQueue.Any())
            {
                MessageBox.Show("The selected playlist does not contain any songs.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            currentSongIndex++; 
            if (currentSongIndex >= songQueue.Count)
                currentSongIndex = 0;
            var nextSong = songQueue[currentSongIndex];
            MediaPlayer.Source = new Uri(nextSong.songPath);
            MediaPlayer.Play();
            UpdateCurrentlyPlayingSong(nextSong.songName);
            PlaySong(songQueue[currentSongIndex].songPath); 
        }

        private void PlayPreviousSong()
        {
            if (songQueue == null || !songQueue.Any())
            {
                MessageBox.Show("The selected playlist does not contain any songs.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            currentSongIndex--; 
            if (currentSongIndex < 0)
                currentSongIndex = songQueue.Count - 1;

            PlaySong(songQueue[currentSongIndex].songPath); 
        }

        private async void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (Shuffle_Button.IsChecked == true) // Nếu AutoPlay bật
            {
                //MessageBox.Show("Shuffle is on", "Info", MessageBoxButton.OK);
                await Task.Delay(2000);
                PlayNextSong();
            }
            else
            {
                //MessageBox.Show("Shuffle is off", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
