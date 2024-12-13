using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Models;
using ViewModels;
using System.Windows.Threading;
using System.Windows.Resources;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DirectoryPage.xaml
    /// </summary>
    public partial class DirectoryPage : UserControl
    {
        public event Action<string> SongSelected;
        public DirectoryPage()
        {
            InitializeComponent();
            DataContext = new DirectoryPageViewModel();
        }

        

        private void Songs_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Songs_ListView.SelectedItem is SongModel selectedSong)
            {
                SongSelected?.Invoke(selectedSong.songPath);
                if(Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.PlaySong(selectedSong.songPath);
                }
            }
        }

        
    }

}
