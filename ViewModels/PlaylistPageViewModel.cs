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
using System.Windows.Forms;



namespace ViewModels
{
    public class PlaylistPageViewModel : INotifyPropertyChanged
    {
        private string connectionString;
        private PlaylistRepo _playlistRepo;
        private ObservableCollection<PlaylistModel> _playlists;
        public ObservableCollection<PlaylistModel> Playlists
        {
            get => _playlists;
            set
            {
                _playlists = value;
                OnPropertyChanged();
            }
        }

        public PlaylistPageViewModel()
        {
            connectionString = "Server=IDEAPAD5PRO;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True;";

            _playlistRepo = new PlaylistRepo(connectionString);
            Playlists = new ObservableCollection<PlaylistModel>(_playlistRepo.GetAllPlaylist());

            AddPlaylistCommand = new PlaylistRelayCommand(AddPlaylist);
            RemovePlaylistCommand = new PlaylistRelayCommand(RemovePlaylist, CanRemovePlaylist);

        }

        

        private PlaylistModel _selectedPlaylist;
        public PlaylistModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                _selectedPlaylist = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddPlaylistCommand { get; }
        public ICommand RemovePlaylistCommand { get; }

        private string _newPlaylistName;
        public string NewPlaylistName
        {
            get => _newPlaylistName;
            set
            {
                _newPlaylistName = value;
                OnPropertyChanged();
            }
        }

        private void AddPlaylist()
        {
            if (string.IsNullOrWhiteSpace(NewPlaylistName))
            {
                MessageBox.Show("Please enter a valid playlist name.", "Error", MessageBoxButtons.OK);
                return;
            }

            string baseDirectory = @"F:\UIT-K18\workspace\PlaylistManager";
            string newDirectory = Path.Combine(baseDirectory, NewPlaylistName);

            try
            {
                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating playlist folder: {ex.Message}", "Error", MessageBoxButtons.OK);
                return;
            }

            var dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3",
                Multiselect = true,
                Title = "Select MP3 files"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    foreach (string file in dialog.FileNames)
                    {
                        string destination = Path.Combine(newDirectory, Path.GetFileName(file));
                        File.Copy(file, destination, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying files: {ex.Message}", "Error", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                MessageBox.Show("No files were selected.", "Info", MessageBoxButtons.OK);
                return;
            }

            try
            {
                int playlistID = _playlistRepo.AddPlayList(NewPlaylistName);
                Playlists.Add(new PlaylistModel
                {
                    playlistName = NewPlaylistName,
                    playlistID = playlistID
                });

                MessageBox.Show($"Playlist '{NewPlaylistName}' created successfully!", "Success", MessageBoxButtons.OK);
                NewPlaylistName = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding playlist to database: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }


        private void RemovePlaylist()
        {
            if(SelectedPlaylist != null)
            {
                _playlistRepo.RemovePlaylist(SelectedPlaylist.playlistID);
                Playlists.Remove(SelectedPlaylist);
            }
        }

        private bool CanRemovePlaylist()
        {
            return SelectedPlaylist != null;
        }


    }
    public class PlaylistRelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public PlaylistRelayCommand(Action execute, Func<bool> canExecute = null)
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
