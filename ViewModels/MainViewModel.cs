using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
     public class MainViewModel
    {
        public ObservableCollection<SongModel> Songs { get; set; }
        public SongModel SelectedSong { get; set; }

    }
}
