using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;
using System.IO;
using System.Windows.Input;
using System.Security.Permissions;
using System.Windows;
using System.Data;
using Repositories;
using System.Data.Common;

namespace ViewModels
{

    public class DirectoryPageViewModel : INotifyPropertyChanged
    {
        private string connectionString;
        private readonly FolderRepo _folderRepo;
        public ICommand BrowseFolderCommand { get; }
        public ICommand RemoveDirCommand { get; }
        
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

            connectionString = "Server=IDEAPAD5PRO;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True;";
            _folderRepo = new FolderRepo(connectionString);
            Folders = new ObservableCollection<FolderItem>();
            Songs = new ObservableCollection<SongModel>();
            BrowseFolderCommand = new DirectoryRelayCommand(OpenFolderBrowser);
            RemoveDirCommand = new DirectoryRelayCommand(RemoveFolderFromData, CanRemoveFolder); // tiep tuc lam 
            LoadFoldersFromData();
        }

        private FolderItem _selectedFolder;
        public FolderItem SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnPropertyChanged();
                LoadSongs();
            }
        }


        private void LoadFoldersFromData()
        {
            var allFolders = _folderRepo.GetAllFolders();
            Folders.Clear();
            foreach(var folder in allFolders)
            {
                Folders.Add(folder);
            }
        }



        private ObservableCollection<SongModel> _songs;
        public ObservableCollection<SongModel> Songs
        {
            get => _songs;
            set
            {
                _songs = value;
                OnPropertyChanged();
            }
        }
        private void OpenFolderBrowser()
        {
            
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    //Folders.Clear();
                    if(!Folders.Any(f => f.FolderPath == selectedPath))
                    {
                        int folderID = _folderRepo.AddFolder(Path.GetFileName(selectedPath), selectedPath);
                        Folders.Add(new FolderItem
                        {
                            FolderPath = selectedPath,
                            FolderName = Path.GetFileName(selectedPath)
                        });
                    }
                }
            }
        }

        private bool CanRemoveFolder() => SelectedFolder != null;

        private void RemoveFolderFromData()
        {
            // tiep tuc hoan thanh
            if (SelectedFolder == null) return;
            try
            {
                var repo = new FolderRepo(connectionString);
                repo.RemoveFolder(SelectedFolder);
                Folders.Remove(SelectedFolder);
            }
            catch
            {
                throw new Exception("Loi xoa thu muc");
            }

        }

        private void LoadSongs()
        {
            Songs.Clear();
            if(SelectedFolder != null && Directory.Exists(SelectedFolder.FolderPath))
            {
                var mp3Files = Directory.GetFiles(SelectedFolder.FolderPath, "*.mp3");
                foreach(var mp3File in mp3Files)
                {
                    Songs.Add(new SongModel
                    {
                        songName = Path.GetFileName(mp3File),
                        songPath = mp3File
                    });
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DirectoryRelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public DirectoryRelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}