﻿using Models;
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
using ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : UserControl
    {
        public event Action<string> SongSelected;
        public PlaylistPage()
        {
            InitializeComponent();
            DataContext = new PlaylistPageViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SongsInPlaylist_Listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SongsInPlaylist_Listview.SelectedItem is SongModel selectedSong)
            {
                SongSelected?.Invoke(selectedSong.songPath);
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.PlaySong(selectedSong.songPath);
                }
            }
        }
    }
}
