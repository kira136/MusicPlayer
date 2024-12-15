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
        public event Action<ObservableCollection<SongModel>> SelectedPlaylistChanged;
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
            SelectedPlaylistSongs = new ObservableCollection<SongModel>();

            AddPlaylistCommand = new PlaylistRelayCommand(AddPlaylist);
            RemovePlaylistCommand = new PlaylistRelayCommand(RemovePlaylist, CanRemovePlaylist);

        }

        private ObservableCollection<SongModel> _selectedPlaylistSongs;
        public ObservableCollection<SongModel> SelectedPlaylistSongs
        {
            get => _selectedPlaylistSongs;
            set
            {
                _selectedPlaylistSongs = value;
                OnPropertyChanged();
            }
        }

        private PlaylistModel _selectedPlaylist;
        public PlaylistModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                _selectedPlaylist = value;
                OnPropertyChanged();
                LoadSongFromPlaylist();
                SelectedPlaylistChanged?.Invoke(SelectedPlaylistSongs);
            }
        }

        

        private void LoadSongFromPlaylist()
        {
            // Kiểm tra nếu SelectedPlaylist không null và có đường dẫn hợp lệ
            if (SelectedPlaylist == null || string.IsNullOrWhiteSpace(SelectedPlaylist.playlistPath))
            {
                MessageBox.Show("Playlist path is invalid or not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SelectedPlaylistSongs.Clear(); // Xóa danh sách bài hát nếu không có playlist hợp lệ
                return;
            }

            // Kiểm tra nếu thư mục tồn tại
            if (!Directory.Exists(SelectedPlaylist.playlistPath))
            {
                MessageBox.Show($"The directory '{SelectedPlaylist.playlistPath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SelectedPlaylistSongs.Clear();
                return;
            }

            try
            {
                // Lấy danh sách tệp .mp3 từ thư mục playlist
                var mp3files = Directory.GetFiles(SelectedPlaylist.playlistPath, "*.mp3");

                // Xóa danh sách bài hát hiện tại
                SelectedPlaylistSongs.Clear();

                // Thêm từng bài hát vào danh sách
                foreach (var file in mp3files)
                {
                    SelectedPlaylistSongs.Add(new SongModel
                    {
                        songName = Path.GetFileName(file),
                        songPath = file
                    });
                }

                // Hiển thị thông báo nếu không có bài hát nào
                if (SelectedPlaylistSongs.Count == 0)
                {
                    MessageBox.Show("No MP3 files found in the selected playlist.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có lỗi khi tải tệp
                MessageBox.Show($"Error loading songs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    playlistID = playlistID,
                    playlistPath = newDirectory
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
            if (SelectedPlaylist != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete the playlist '{SelectedPlaylist.playlistName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (Directory.Exists(SelectedPlaylist.playlistPath))
                        {
                            Directory.Delete(SelectedPlaylist.playlistPath, true); // Xóa tất cả nội dung trong thư mục
                        }
                        _playlistRepo.RemovePlaylist(SelectedPlaylist.playlistID);
                        Playlists.Remove(SelectedPlaylist);
                        SelectedPlaylistSongs.Clear();
                        SelectedPlaylist = null;
                        //MessageBox.Show($"Playlist '{SelectedPlaylist.playlistName}' deleted successfully.", "Success", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting playlist: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("No playlist selected to delete.", "Error", MessageBoxButtons.OK);
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
