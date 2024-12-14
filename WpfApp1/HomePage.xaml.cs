using Models;
using Repositories;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        private SongRepo _songrepo = new SongRepo("Server=IDEAPAD5PRO;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True;");
        public HomePage()
        {
            InitializeComponent();
            LoadRecentSongs();
        }
        
        public void LoadRecentSongs()
        {
            List<SongModel> recentSongs = _songrepo.GetRecentSongs();
            Recent_ListView.ItemsSource = recentSongs;
        }
    }
}
