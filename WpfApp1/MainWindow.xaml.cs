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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentControl.Content = new HomePage();
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

        private bool isPlaying = false;

        private void PlaySong_Button_Click(object sender, RoutedEventArgs e)
        {
            PlaySong_Button.Visibility = Visibility.Collapsed;
            PauseSong_Button.Visibility = Visibility.Visible;
            
        }

        private void PauseSong_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseSong_Button.Visibility = Visibility.Collapsed;
            PlaySong_Button.Visibility = Visibility.Visible;
        }

        private void FocusButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new FocusPage();
        }
    }
}
