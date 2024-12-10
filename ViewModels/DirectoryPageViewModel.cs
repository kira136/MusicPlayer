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


namespace ViewModels
{

    public class DirectoryPageViewModel : INotifyPropertyChanged
    {
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

        public ICommand BrowseFolderCommand { get; }

        public DirectoryPageViewModel()
        {
            Folders = new ObservableCollection<FolderItem>();
            BrowseFolderCommand = new RelayCommand(OpenFolderBrowser);
        }

        private FolderItem _selectedFolder;
        public FolderItem SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnPropertyChanged();
                LoadSongs(); // Load danh sách file MP3 khi thay đổi folder
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
                        Folders.Add(new FolderItem
                        {
                            FolderPath = selectedPath,
                            FolderName = Path.GetFileName(selectedPath)
                        });
                    }
                }
            }
        }


        public void RemoveFolder(FolderItem folder)
        {
            if (folder != null)
            {
                Folders.Remove(folder);
            }
        }

        private void LoadSongs()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public RelayCommand(Action execute, Func<bool> canExecute = null)
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