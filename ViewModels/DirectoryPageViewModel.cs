using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;

namespace ViewModels
{
    
    public class DirectoryPageViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ObservableCollection<FolderItem> _folders;
        public ObservableCollection<FolderItem> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged();
            }
        }
        public DirectoryPageViewModel()
        {
            Folders = new ObservableCollection<FolderItem>();
        }

        private void AddFolder(string folderPath) // 
        {
            var folderName = System.IO.Path.GetFileName(folderPath);
            Folders.Add(new FolderItem{ FolderName = folderName,FolderPath = folderPath});
        }
    }
}
