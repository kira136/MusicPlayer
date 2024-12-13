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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private bool _isDragging = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeMediaTimer();
            ContentControl.Content = new HomePage();
            var directoryPage = new DirectoryPage();
            //directoryPage.SongSelected += OnSongSelected;
            MediaPlayer.MediaOpened += (s, args) =>
            {
                if (MediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    SongProgress_Slider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }
            };

            PauseSong_Button.Visibility = Visibility.Collapsed;
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
            _timer.Stop();
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
            ContentControl.Content = new PlaylistPage();
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
            MediaPlayer.Play();
            _timer.Start();
        }

        private void PauseSong_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseSong_Button.Visibility = Visibility.Collapsed;
            PlaySong_Button.Visibility = Visibility.Visible;
            MediaPlayer.Stop();
            _timer.Stop();
        }

        private void FocusButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new FocusPage();
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

        public void PlaySong(string songPath)
        {
            MediaPlayer.Source = new Uri(songPath);
            MediaPlayer.Play();
            _timer.Start();
        }

    }
}
